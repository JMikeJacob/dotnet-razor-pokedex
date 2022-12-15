namespace PokedexV2.Models
{
    public class PaginatedList<T> : List<T>
    {
        public int PageIndex { get; private set; } = 1;
        public int TotalPages { get; private set; }
        public int TotalItems { get; private set; }
        public int PageSize { get; private set; }

        public PaginatedList(List<T> items, int count, int pageIndex, int pageSize, string? query)
        {
            PageIndex = pageIndex;
            TotalItems = count;
            PageSize = pageSize;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            QueryFilter = query ?? string.Empty;
            this.AddRange(items);
        }

        public bool HasPreviousPage => PageIndex > 1;

        public bool HasNextPage => PageIndex < TotalPages;

        public bool ShowFirstPage => PageIndex > 1;
        public bool ShowLastPage => PageIndex < TotalPages;

        public int MaximumPageIndicesDisplayed = 3;
        public int MinPageIndexDisplay { get {
            if (TotalPages == 0) {
                return 1;
            } else {
                if (PageIndex <= MaximumPageIndicesDisplayed) {
                    return 1;
                } else {
                    int distanceFromFirstIndex = (PageIndex - 1) % MaximumPageIndicesDisplayed;
                    return PageIndex - distanceFromFirstIndex;
                }
            }
        } set {
            MinPageIndexDisplay = value;
        } }

        public int MaxPageIndexDisplay { get {
            if (TotalPages == 0) {
                return 1;
            } else {
                int distanceFromFirstIndex = (PageIndex - 1) % MaximumPageIndicesDisplayed;
                int startingIndex = PageIndex - distanceFromFirstIndex;
                if (startingIndex + MaximumPageIndicesDisplayed - 1 >= TotalPages) {
                    return TotalPages;
                } else {
                    return startingIndex + MaximumPageIndicesDisplayed - 1;
                }
            }
        } set {
            MinPageIndexDisplay = value;
        } }

        public string QueryFilter { get; set; } = string.Empty;
    }
}