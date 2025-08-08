using Microsoft.Extensions.Options;

namespace PalladiumPayroll.DTOs.DTOs.Common
{
    public class AppSettingDirectoryPath
    {
        private readonly DirectoryPathSetting _settings;

        public AppSettingDirectoryPath(IOptions<DirectoryPathSetting> options)
        {
            _settings = options.Value;
        }

        public DirectoryPathSetting GetAppSettingDirectoryPath()
        {
            return _settings;
        }
    }
    public class DirectoryPathSetting
    {
        public string EmployeeDocument { get; set; } = null!;
    }

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
