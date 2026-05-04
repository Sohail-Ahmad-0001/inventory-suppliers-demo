namespace inventory_suppliers.Controllers;

internal static class PaginationHelper
{
    public static object CreatePayload<T>(
        IReadOnlyList<T> items,
        int pageNumber,
        int pageSize,
        int total)
    {
        return new
        {
            data = items,
            pagination = new
            {
                pageNumber,
                pageSize,
                TotalRecords = total,
                TotalPages = ComputeTotalPages(total, pageSize)
            }
        };
    }

    private static int ComputeTotalPages(int total, int pageSize) =>
        (int)Math.Ceiling(total / (double)pageSize);
}
