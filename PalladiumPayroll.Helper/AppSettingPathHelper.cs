using Microsoft.Extensions.Options;

namespace PalladiumPayroll.Helper
{
    public class AppSettingPathHelper
    {
        private readonly DirectoryPathSetting _settings;

        public AppSettingPathHelper(IOptions<DirectoryPathSetting> options)
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
        public string TrainingDocument { get; set; } = null!;
        public string DisplinaryLogDocument { get; set; } = null!;
        public string CustomizeReportDocument  { get; set; } = null!;
    }

}
