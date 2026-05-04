using inventory_suppliers.Data.DTOs.Locations;
using inventory_suppliers.Enums;
using inventory_suppliers.Mappers;
using inventory_suppliers.Models;
using inventory_suppliers.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace inventory_suppliers.Controllers;

[ApiController]
[Route("api/suppliers/{supplierId:guid}/locations")]

public class LocationsController(
    ILocationRepository locationRepository,
    ISupplierRepository supplierRepository) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll(
        [FromRoute] Guid supplierId,
        [FromQuery] string? name = null,
        [FromQuery] string? city = null,
        [FromQuery] string? state = null,
        [FromQuery] string? country = null,
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 50,
        [FromQuery] StatusEnum? status = null)
    {
        Supplier? supplier = await supplierRepository.GetByIdAsync(supplierId, includeLocations: false);

        if (supplier is null)
        {
            return NotFound();
        }

        (List<Location> items, int total) = await locationRepository.GetAllAsync(
            supplierId,
            pageNumber,
            pageSize,
            name,
            city,
            state,
            country,
            status);

        List<LocationDto> dtos = items.Select(l => LocationMapper.ToDto(l)).ToList();

        return Ok(PaginationHelper.CreatePayload(dtos, pageNumber, pageSize, total));
    }

    [HttpGet("{id:guid}")]

    public async Task<IActionResult> GetById([FromRoute] Guid supplierId, [FromRoute] Guid id)
    {
        Location? location = await locationRepository.GetByIdAsync(supplierId, id);

        if (location is null)
        {
            return NotFound();
        }

        return Ok(LocationMapper.ToDto(location));
    }

    [HttpPost]

    public async Task<IActionResult> Create([FromRoute] Guid supplierId, [FromBody] CreateLocationDto dto)
    {
        Supplier? supplier = await supplierRepository.GetByIdAsync(supplierId, includeLocations: false);

        if (supplier is null)
        {
            return NotFound();
        }

        DateTime utcNow = DateTime.UtcNow;

        var location = new Location
        {
            Id = Guid.NewGuid(),
            SupplierId = supplierId,
            Name = dto.Name,
            Address = dto.Address,
            City = dto.City,
            State = dto.State,
            Country = dto.Country,
            PostalCode = dto.PostalCode,
            CreatedAt = utcNow,
            UpdatedAt = utcNow,
            Status = StatusEnum.Active
        };

        Location created = await locationRepository.CreateAsync(location);
        LocationDto createdDto = LocationMapper.ToDto(created);

        return CreatedAtAction(nameof(GetById), new { supplierId, id = createdDto.Id }, createdDto);
    }

    [HttpPut("{id:guid}")]

    public async Task<IActionResult> Update(
        [FromRoute] Guid supplierId,
        [FromRoute] Guid id,
        [FromBody] UpdateLocationDto dto)
    {
        Location? updated = await locationRepository.UpdateAsync(supplierId, id, dto);

        if (updated is null)
        {
            return NotFound();
        }

        return Ok(LocationMapper.ToDto(updated));
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(typeof(LocationDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete([FromRoute] Guid supplierId, [FromRoute] Guid id)
    {
        Location? deleted = await locationRepository.DeleteAsync(supplierId, id);

        if (deleted is null)
        {
            return NotFound();
        }

        return Ok(LocationMapper.ToDto(deleted));
    }
}
