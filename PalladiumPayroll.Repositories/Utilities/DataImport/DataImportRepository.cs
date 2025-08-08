using Microsoft.Extensions.Configuration;
using PalladiumPayroll.DataContext;

namespace PalladiumPayroll.Repositories.Utilities.DataImport;

public class DataImportRepository:IDataImportRepository
{
    private readonly DapperContext _dapper;

    public DataImportRepository(IConfiguration configuration)
    {
        _dapper = new DapperContext(configuration);
    }
}
