namespace LogisticsSolution.Application.Utility
{
    public class Paged<T>
    {

        public List<T> Items { get; private set; }
        public int TotalCount { get; private set; }
        public int PageIndex { get; private set; }
        public int PageSize { get; private set; }
        public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize);
        public bool HasPreviousPage => PageIndex > 1;
        public bool HasNextPage => PageIndex < TotalPages;

        public Paged(List<T> items, int count, int pageIndex, int pageSize)
        {
            Items = items;
            TotalCount = count;
            PageIndex = pageIndex;
            PageSize = pageSize;
        }

        public static Paged<T> PaginatedList(List<T> source, int totalCount, int pageIndex, int pageSize)
        {
            return new Paged<T>(source, totalCount, pageIndex, pageSize);
        }
    }

    public class PaginationDto
    {
        public int PageNumber { get; set; }
        public int NumberOfRecords { get; set; }
    }
}
