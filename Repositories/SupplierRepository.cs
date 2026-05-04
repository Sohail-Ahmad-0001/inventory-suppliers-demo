using inventory_suppliers.Data;
using inventory_suppliers.Data.DTOs.Suppliers;
using inventory_suppliers.Enums;
using inventory_suppliers.Models;
using inventory_suppliers.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace inventory_suppliers.Repositories;

public class SupplierRepository(InventoryDbContext dbContext) : ISupplierRepository
{
    public async Task<(List<Supplier> items, int total)> GetAllAsync(
        int pageNumber,
        int pageSize,
        string? name,
        string? email,
        string? code,
        string? phone,
        bool includeLocations,
        StatusEnum? status)
    {
        IQueryable<Supplier> query = dbContext.Suppliers.AsQueryable();

        if (status.HasValue)
        {
            query = query.Where(s => s.Status == status.Value);
        }

        if (!string.IsNullOrEmpty(name))
        {
            query = query.Where(s => s.Name == name);
        }

        if (!string.IsNullOrEmpty(email))
        {
            query = query.Where(s => s.Email == email);
        }

        if (!string.IsNullOrEmpty(code))
        {
            query = query.Where(s => s.Code == code);
        }

        if (!string.IsNullOrEmpty(phone))
        {
            query = query.Where(s => s.Phone == phone);
        }

        int total = await query.CountAsync();

        List<Supplier> items = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        if (includeLocations && items.Count > 0)
        {
            await AttachLocationsAsync(items, status);
        }

        return (items, total);
    }

    public async Task<Supplier?> GetByIdAsync(Guid id, bool includeLocations)
    {
        Supplier? supplier = await dbContext.Suppliers
            .FirstOrDefaultAsync(s => s.Id == id);

        if (supplier is null)
        {
            return null;
        }

        if (includeLocations)
        {
            supplier.Locations = await dbContext.Locations
                .Where(l => l.SupplierId == id)
                .ToListAsync();
        }

        return supplier;
    }

    public async Task<Supplier> CreateAsync(Supplier supplier)
    {
        dbContext.Suppliers.Add(supplier);
        await dbContext.SaveChangesAsync();
        return supplier;
    }

    public async Task<Supplier?> UpdateAsync(Guid id, UpdateSupplierDto dto)
    {
        Supplier? supplier = await dbContext.Suppliers
            .FirstOrDefaultAsync(s => s.Id == id);

        if (supplier is null || supplier.Status == StatusEnum.Deleted)
        {
            return null;
        }

        supplier.Name = dto.Name;
        supplier.Email = dto.Email;
        supplier.Code = dto.Code;
        supplier.Phone = dto.Phone;
        supplier.UpdatedAt = DateTime.UtcNow;

        await dbContext.SaveChangesAsync();
        return supplier;
    }

    public async Task<Supplier?> DeleteAsync(Guid id)
    {
        Supplier? supplier = await dbContext.Suppliers
            .FirstOrDefaultAsync(s => s.Id == id);

        if (supplier is null || supplier.Status == StatusEnum.Deleted)
        {
            return null;
        }

        DateTime utcNow = DateTime.UtcNow;
        supplier.Status = StatusEnum.Deleted;
        supplier.DeletedAt = utcNow;
        supplier.UpdatedAt = utcNow;

        List<Location> activeLocations = await dbContext.Locations
            .Where(l => l.SupplierId == id && l.Status == StatusEnum.Active)
            .ToListAsync();

        foreach (Location location in activeLocations)
        {
            location.Status = StatusEnum.Deleted;
            location.DeletedAt = utcNow;
            location.UpdatedAt = utcNow;
        }

        await dbContext.SaveChangesAsync();
        return supplier;
    }

    private async Task AttachLocationsAsync(IReadOnlyList<Supplier> suppliers, StatusEnum? locationStatusFilter)
    {
        List<Guid> supplierIds = suppliers.Select(s => s.Id).ToList();

        IQueryable<Location> locationsQuery = dbContext.Locations
            .AsNoTracking()
            .Where(l => supplierIds.Contains(l.SupplierId));

        if (locationStatusFilter.HasValue)
        {
            locationsQuery = locationsQuery.Where(l => l.Status == locationStatusFilter.Value);
        }

        List<Location> allLocations = await locationsQuery.ToListAsync();

        foreach (Supplier supplier in suppliers)
        {
            supplier.Locations = allLocations
                .Where(l => l.SupplierId == supplier.Id)
                .ToList();
        }
    }
}
