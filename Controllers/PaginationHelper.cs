namespace inventory_suppliers.Controllers;

internal static class PaginationHelper
{
    public static object CreatePayload<T>(
        IReadOnlyList<T> items,
        int pageNumber,
        int pageSize,
        int total)
    {
        int totalPages = ComputeTotalPages(total, pageSize);

        return new
        {
            data = items,
            pagination = new
            {
                pageNumber,
                pageSize,
                TotalRecords = total,
                TotalPages = totalPages,
                hasPreviousPage = pageNumber > 1,
                hasNextPage = totalPages > 0 && pageNumber < totalPages
            }
        };
    }

    private static int ComputeTotalPages(int total, int pageSize) =>
        (int)Math.Ceiling(total / (double)pageSize);
}
