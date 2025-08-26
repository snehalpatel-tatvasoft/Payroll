namespace PalladiumPayroll.DTOs.DTOs.Common
{
    public class DropDownViewModel
    {
        public int Id { get; set; }
        public string? Key { get; set; }
        public string? Value { get; set; }
    }

    public class DropDownViewModelWithString
    {
        public string Id { get; set; }
        public string? Key { get; set; }
        public string? Value { get; set; }
    }

    public class TableDataModel<T>
    {
        public List<T> DataList { get; set; } = new List<T>();
        public int TotalCount { get; set; }
    }


}
