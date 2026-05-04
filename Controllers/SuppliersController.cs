using inventory_suppliers.Data.DTOs.Suppliers;
using inventory_suppliers.Enums;
using inventory_suppliers.Mappers;
using inventory_suppliers.Models;
using inventory_suppliers.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace inventory_suppliers.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public sealed class SuppliersController(ISupplierRepository supplierRepository) : ControllerBase
{
    private const string IncludeLocationsQueryValue = "locations";

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll(
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 50,
        [FromQuery] string? name = null,
        [FromQuery] string? email = null,
        [FromQuery] string? code = null,
        [FromQuery] string? phone = null,
        [FromQuery] string? include = null,
        [FromQuery] StatusEnum? status = null)
    {
        bool includeLocations = string.Equals(include, IncludeLocationsQueryValue, StringComparison.Ordinal);

        (List<Supplier> items, int total) = await supplierRepository.GetAllAsync(
            pageNumber,
            pageSize,
            name,
            email,
            code,
            phone,
            includeLocations,
            status);

        List<SupplierDto> dtos = items
            .Select(s => SupplierMapper.ToDto(s, includeLocations))
            .ToList();

        return Ok(PaginationHelper.CreatePayload(dtos, pageNumber, pageSize, total));
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(SupplierDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(
        [FromRoute] Guid id,
        [FromQuery] string? include = null)
    {
        bool includeLocations = string.Equals(include, IncludeLocationsQueryValue, StringComparison.Ordinal);

        Supplier? supplier = await supplierRepository.GetByIdAsync(id, includeLocations);

        if (supplier is null)
        {
            return NotFound();
        }

        return Ok(SupplierMapper.ToDto(supplier, includeLocations));
    }

    [HttpPost]
    [ProducesResponseType(typeof(SupplierDto), StatusCodes.Status201Created)]
    public async Task<IActionResult> Create([FromBody] CreateSupplierDto dto)
    {
        if (await supplierRepository.CodeExistsAsync(dto.Code))
        {
            return Conflict(SupplierCodeConflictProblem());
        }

        DateTime utcNow = DateTime.UtcNow;

        var supplier = new Supplier
        {
            Id = Guid.NewGuid(),
            Name = dto.Name,
            Email = dto.Email,
            Code = dto.Code,
            Phone = dto.Phone,
            CreatedAt = utcNow,
            UpdatedAt = utcNow,
            Status = StatusEnum.Active
        };

        Supplier created = await supplierRepository.CreateAsync(supplier);
        SupplierDto createdDto = SupplierMapper.ToDto(created, includeLocations: false);

        return CreatedAtAction(nameof(GetById), new { id = createdDto.Id }, createdDto);
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(SupplierDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateSupplierDto dto)
    {
        if (await supplierRepository.CodeExistsAsync(dto.Code, excludingSupplierId: id))
        {
            return Conflict(SupplierCodeConflictProblem());
        }

        Supplier? updated = await supplierRepository.UpdateAsync(id, dto);

        if (updated is null)
        {
            return NotFound();
        }

        return Ok(SupplierMapper.ToDto(updated, includeLocations: false));
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(typeof(SupplierDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        Supplier? deleted = await supplierRepository.DeleteAsync(id);

        if (deleted is null)
        {
            return NotFound();
        }

        return Ok(SupplierMapper.ToDto(deleted, includeLocations: false));
    }

    private static ValidationProblemDetails SupplierCodeConflictProblem()
    {
        return new ValidationProblemDetails
        {
            Status = StatusCodes.Status409Conflict,
            Title = "Supplier code already exists.",
            Errors = { [nameof(UpdateSupplierDto.Code)] = new[] { "A supplier with this code already exists." } }
        };
    }
}
