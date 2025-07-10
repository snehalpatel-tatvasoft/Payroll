using static PalladiumPayroll.Helper.Constants.AppConstants;

namespace PalladiumPayroll.DTOs.DTOs.Common
{
    public class BankModel
    {
        public int CompanyId { get; set; }
        public string BankName { get; set; }
        public string BranchCode { get; set; }
    }

    public class TableFilterViewModel
    {
        public string? Search { get; set; }
        public int CurrentPage { get; set; } = PageNumber;
        public int PageSize { get; set; } = DefaultPageSize;
        public string? SortBy { get; set; }
        public bool? sortType { get; set; } = false;
    }

    public class TableDataModel<T>
    {
        public List<T> DataList { get; set; } = new List<T>();
        public int TotalCount { get; set; }
    }
}
