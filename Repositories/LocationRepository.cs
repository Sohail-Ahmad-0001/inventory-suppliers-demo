using inventory_suppliers.Data;
using inventory_suppliers.Data.DTOs.Locations;
using inventory_suppliers.Enums;
using inventory_suppliers.Models;
using inventory_suppliers.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace inventory_suppliers.Repositories;

public class LocationRepository(InventoryDbContext dbContext) : ILocationRepository
{
    public async Task<(List<Location> items, int total)> GetAllAsync(
        Guid supplierId,
        int pageNumber,
        int pageSize,
        string? name,
        string? city,
        string? state,
        string? country,
        StatusEnum? status)
    {
        IQueryable<Location> query = dbContext.Locations
            .Where(l => l.SupplierId == supplierId)
            .Include(l => l.Supplier)
            .AsNoTracking();

        if (status.HasValue)
        {
            query = query.Where(l => l.Status == status.Value);
        }

        if (!string.IsNullOrWhiteSpace(name))
        {
            query = query.Where(l => l.Name.Contains(name));
        }

        if (!string.IsNullOrWhiteSpace(city))
        {
            query = query.Where(l => l.City.Contains(city));
        }

        if (!string.IsNullOrWhiteSpace(state))
        {
            query = query.Where(l => l.State.Contains(state));
        }

        if (!string.IsNullOrWhiteSpace(country))
        {
            query = query.Where(l => l.Country.Contains(country));
        }

        int total = await query.CountAsync();

        List<Location> items = await query
            .OrderByDescending(l => l.CreatedAt)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (items, total);
    }

    public Task<Location?> GetByIdAsync(Guid supplierId, Guid id)
    {
        return dbContext.Locations
            .Include(l => l.Supplier)
            .AsNoTracking()
            .FirstOrDefaultAsync(l => l.Id == id && l.SupplierId == supplierId);
    }

    public async Task<Location> CreateAsync(Location location)
    {
        dbContext.Locations.Add(location);
        await dbContext.SaveChangesAsync();

        await dbContext.Entry(location).Reference(l => l.Supplier).LoadAsync();
        return location;
    }

    public async Task<Location?> UpdateAsync(Guid supplierId, Guid id, UpdateLocationDto dto)
    {
        Location? location = await dbContext.Locations
            .Include(l => l.Supplier)
            .FirstOrDefaultAsync(l => l.Id == id && l.SupplierId == supplierId);

        if (location is null || location.Status == StatusEnum.Deleted)
        {
            return null;
        }

        location.Name = dto.Name;
        location.Address = dto.Address;
        location.City = dto.City;
        location.State = dto.State;
        location.Country = dto.Country;
        location.PostalCode = dto.PostalCode;
        location.UpdatedAt = DateTime.UtcNow;

        await dbContext.SaveChangesAsync();
        return location;
    }

    public async Task<Location?> DeleteAsync(Guid supplierId, Guid id)
    {
        Location? location = await dbContext.Locations.FirstOrDefaultAsync(l =>
            l.Id == id && l.SupplierId == supplierId);

        if (location is null || location.Status == StatusEnum.Deleted)
        {
            return null;
        }

        DateTime utcNow = DateTime.UtcNow;
        location.Status = StatusEnum.Deleted;
        location.DeletedAt = utcNow;
        location.UpdatedAt = utcNow;

        await dbContext.SaveChangesAsync();
        return location;
    }
}
