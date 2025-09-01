using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PalladiumPayroll.DTOs.DTOs.Employees;
using PalladiumPayroll.DTOs.Miscellaneous;
using PalladiumPayroll.Helper;
using PalladiumPayroll.Repositories.Employees;
using System.Data;
using static PalladiumPayroll.Helper.Constants.AppConstants;
using static PalladiumPayroll.Helper.Constants.AppEnums;

namespace PalladiumPayroll.Services.Employees
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly DirectoryPathSetting _directoryPathSetting;
        public EmployeeService(IEmployeeRepository employeeRepository, AppSettingPathHelper directoryPathSetting)
        private readonly PasswordHasher<object> _passwordHasher;
        public EmployeeService(IEmployeeRepository employeeRepository, AppSettingDirectoryPath directoryPathSetting)
        {
            _employeeRepository = employeeRepository;
            _directoryPathSetting = directoryPathSetting.GetAppSettingDirectoryPath();
        }

        public async Task<JsonResult> GetEmployeeFilters(int companyId)
        {
            return await _employeeRepository.GetEmployeeFilters(companyId);
        }

        public async Task<JsonResult> GetEmployeeList(EmployeeFilterViewModel reqModel)
        {
            var data = await _employeeRepository.GetEmployeeList(reqModel);
            return HttpStatusCodeResponse.SuccessResponse(data, string.Format(ResponseMessages.Success, ResponseMessages.Employee, ActionType.Retrieved));
        }

        public async Task<byte[]> ExportEmployeeList(EmployeeFilterViewModel reqModel)
        {
            reqModel.CurrentPage = -1;
            reqModel.Search = string.Empty;
            var data = await _employeeRepository.GetEmployeeList(reqModel);
            var dt = new DataTable();
            dt.Columns.Add("Employee Code", typeof(string));
            dt.Columns.Add("Employee Name", typeof(string));
            dt.Columns.Add("Department", typeof(string));
            dt.Columns.Add("Designation", typeof(string));
            dt.Columns.Add("IDNumber", typeof(string));
            dt.Columns.Add("Dob", typeof(DateTime));
            foreach(var item in data.DataList)
            {
                dt.Rows.Add(item.EmployeeCode, item.EmployeeName, item.Department, item.Designation, item.IDNumber, item.Dob);
            }
            return ExcelHelper.ExportToExcel(new Dictionary<string, DataTable> { { "Sheet 1", dt } });
        }

        public async Task<JsonResult> DeleteEmployee(int employeeId)
        {
            var res = await _employeeRepository.DeleteEmployee(employeeId);
            if (res)
            {
                return HttpStatusCodeResponse.SuccessResponse(string.Empty, string.Format(ResponseMessages.Success, ResponseMessages.Employee, ActionType.Deleted));
            }
            return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.UnexpectedError);
        }
        public async Task<JsonResult> GetEmployeePaymentDetail(int employeeId)
        {
            return await _employeeRepository.GetEmployeePaymentDetail(employeeId);
        }
        public async Task<JsonResult> GetEmployeePersonalInfo(int employeeId)
        {
            return await _employeeRepository.GetEmployeePersonalInfo(employeeId);
        }
        public async Task<JsonResult> GetEmployeePersonalInfoDropDown(int companyId)
        {
            return await _employeeRepository.GetEmployeePersonalInfoDropDown(companyId);
        }
        public async Task<JsonResult> SaveEmployeePersonalInfo(EmployeePersonalInformation reqModel)
        {
            var res = await _employeeRepository.SaveEmployeePersonalInfo(reqModel);
            if (res)
            {
                return HttpStatusCodeResponse.SuccessResponse(string.Empty, string.Format(ResponseMessages.Success, string.Concat(ResponseMessages.Employee, " ", " Personal Information"), ActionType.Saved));
            }
            return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.UnexpectedError);
        }
        public async Task<JsonResult> EmployeePaymentDetailSave(EmployeePaymentDetail reqModel)
        {
            var res = await _employeeRepository.EmployeePaymentDetailSave(reqModel);
            if (res)
            {
                return HttpStatusCodeResponse.SuccessResponse(string.Empty, string.Format(ResponseMessages.Success, string.Concat(ResponseMessages.Employee, " ", " Payment Detail"), ActionType.Saved));
            }
            return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.UnexpectedError);
        }

        public async Task<JsonResult> GetEmployeeWorkDropDown(int companyId)
        {
            return await _employeeRepository.GetEmployeeWorkDropDown(companyId);
        }

        public async Task<JsonResult> GetEmployeeWorkInformation(int employeeId)
        {
            return await _employeeRepository.GetEmployeeWorkInformation(employeeId);
        }

        public async Task<JsonResult> EmployeeWorkInfoSave(EmployeeWorkInformation reqModel)
        {
            reqModel.WorkingDay?.Sort();
            var res = await _employeeRepository.EmployeeWorkInfoSave(reqModel);
            if (res)
            {
                return HttpStatusCodeResponse.SuccessResponse(string.Empty, string.Format(ResponseMessages.Success, string.Concat(ResponseMessages.Employee, " ", " Work Information"), ActionType.Saved));
            }
            return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.UnexpectedError);
        }

        public async Task<JsonResult> GetEmployeeWorkOrganizationalDropdownData(long companyId)
        {
            return await _employeeRepository.GetEmployeeWorkOrganizationalDropdownData(companyId);
        }

        public async Task<JsonResult> AddWorkOrganizationalDropdownItem(WorkOrgnizationItem reqItem)
        {
            var result = await _employeeRepository.AddWorkOrganizationalDropdownItem(reqItem);
            if (result.Count > 0 && result.FirstOrDefault()?.Id > 0)
            {
                return HttpStatusCodeResponse.SuccessResponse(result, string.Format(ResponseMessages.Success, "Item", ActionType.Saved));
            }
            return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.UnexpectedError);
        }

        public async Task<JsonResult> DeleteWorkOrganizationalDropdownItem(int id, int type)
        {
            var result = await _employeeRepository.DeleteWorkOrganizationalDropdownItem(id, type);
            if (result)
            {
                return HttpStatusCodeResponse.SuccessResponse(string.Empty, string.Format(ResponseMessages.Success, "Item", ActionType.Deleted));
            }
            return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.UnexpectedError);
        }

        public async Task<JsonResult> GetEmployeeWorkOrganizationalData(long employeeId)
        {
            return await _employeeRepository.GetEmployeeWorkOrganizationalData(employeeId);
        }

        public async Task<JsonResult> SaveEmployeeWorkOrganizationalData(EmployeeOrgnizationalModel reqModel)
        {
            var result = await _employeeRepository.SaveEmployeeWorkOrganizationalData(reqModel);
            if (result)
            {
                return HttpStatusCodeResponse.SuccessResponse(string.Empty, string.Format(ResponseMessages.Success, ResponseMessages.Employee + " organization", ActionType.Saving));
            }
            return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.UnexpectedError);
        }

        public async Task<JsonResult> GetEmployeeTimeSheetSetup(long employeeId)
        {
            return await _employeeRepository.GetEmployeeTimeSheetSetup(employeeId);
        }

        public async Task<JsonResult> SaveEmployeeTimeSheetSetup(TimeSheetSetup timeSheetSetup)
        {
            var result = await _employeeRepository.SaveEmployeeTimeSheetSetup(timeSheetSetup);
            if (result)
            {
                return HttpStatusCodeResponse.SuccessResponse(result, string.Format(ResponseMessages.Success, ResponseMessages.Employee + " Time Sheet", ActionType.Saved));
            }
            return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.UnexpectedError);
        }

        public async Task<JsonResult> GetCasualWageInformation(int employeeId)
        {
            return await _employeeRepository.GetCasualWageInformation(employeeId);
        }

        public async Task<JsonResult> UpdateCasualWageInformation(CasualWageInformation reqModel)
        {
            var result = await _employeeRepository.UpdateCasualWageInformation(reqModel);
            if (result)
            {
                return HttpStatusCodeResponse.SuccessResponse(string.Empty, string.Format(ResponseMessages.Success, ResponseMessages.CasualWageInformation, ActionType.Updated));
            }
            return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.UnexpectedError);
        }

        public async Task<TransactionTypeDropdownsDTO> GetTransactionTypesDropdownData(long companyId)
        {
            var data = await _employeeRepository.GetTransactionTypesDropdownData(companyId);
            return data;
        }

        public async Task<JsonResult> AddDirective(DirectiveRequest reqModel)
        {
            var result = await _employeeRepository.AddDirective(reqModel);
            if (result)
            {
                return HttpStatusCodeResponse.SuccessResponse(
                    string.Empty,
                    string.Format(ResponseMessages.Success,ResponseMessages.DirectiveInformation, ActionType.Saved));
            }
            return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.UnexpectedError);
        }

        public async Task<List<GetDirectiveResponse>> GetDirectivesByEmployeeId(long employeeId)
        {
            return await _employeeRepository.GetDirectivesByEmployeeId(employeeId);
        }

        public async Task<JsonResult> UpdateDirective(long directiveId, DirectiveRequest reqModel)
        {
            var result = await _employeeRepository.UpdateDirective(directiveId, reqModel);
            if (result)
            {
                return HttpStatusCodeResponse.SuccessResponse(
                    string.Empty,
                    string.Format(ResponseMessages.Success, ResponseMessages.DirectiveInformation, ActionType.Updated));
            }

            return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.UnexpectedError);
        }

        public async Task<JsonResult> DeleteDirective(long directiveId)
        {
            var result = await _employeeRepository.DeleteDirective(directiveId);
            if (result)
            {
                return HttpStatusCodeResponse.SuccessResponse(
                    string.Empty,
                    string.Format(ResponseMessages.Success,ResponseMessages.DirectiveInformation, ActionType.Deleted));
            }

            return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.UnexpectedError);
        }

        public async Task<TaxInformationDropdownData> GetTaxInformationDropdownData()
        {
            var data = await _employeeRepository.GetTaxInformationDropdownData();
            return data;
        }


        public async Task<JsonResult> GetTaxInformation(int employeeId)
        {
            return await _employeeRepository.GetTaxInformation(employeeId);
        }

        public async Task<JsonResult> UpdateTaxInformation(TaxInformation reqModel)
        {
            var result = await _employeeRepository.UpdateTaxInformation(reqModel);
            if (result == true)
            {
                return HttpStatusCodeResponse.SuccessResponse(string.Empty, string.Format(ResponseMessages.Success, ResponseMessages.TaxInformation, ActionType.Updated));
            }
            return HttpStatusCodeResponse.InternalServerErrorResponse(string.Format(ResponseMessages.ExceptionMessage, ActionType.Updating, ResponseMessages.TaxInformation));
        }


        public async Task<JsonResult> GetPayrollTransactionList(TransactionReqModel reqModel)
        {
            var transactionList = await _employeeRepository.GetPayrollTransactionList(reqModel);
            return HttpStatusCodeResponse.SuccessResponse(transactionList, string.Format(ResponseMessages.Success, ResponseMessages.Transaction, ActionType.Retrieved));
        }

        public async Task<JsonResult> SaveEmployeeTakeOnBalance(TransactionSaveModel reqModel)
        {
            var result = await _employeeRepository.SaveEmployeeTakeOnBalance(reqModel);
            if (result)
            {
                return HttpStatusCodeResponse.SuccessResponse(string.Empty, string.Format(ResponseMessages.Success, ResponseMessages.Transaction, ActionType.Saved));
            }
            return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.UnexpectedError);
        }

        public async Task<JsonResult> GetEmployeeTakeOnBalance(int employeeId, int allowanceType)
        {
            var data = await _employeeRepository.GetEmployeeTakeOnBalance(employeeId, allowanceType);
            return HttpStatusCodeResponse.SuccessResponse(data, string.Format(ResponseMessages.Success, ResponseMessages.Transaction, ActionType.Retrieved));
        }

        public async Task<JsonResult> DeleteEmployeeTakeOnBalance(List<int> takeOnBalanceIds)
        {
            var result = await _employeeRepository.DeleteEmployeeTakeOnBalance(takeOnBalanceIds);
            if (result)
            {
                return HttpStatusCodeResponse.SuccessResponse(string.Empty, string.Format(ResponseMessages.Success, "Take on Balance " + ResponseMessages.Transaction, ActionType.Deleted));
            }
            return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.UnexpectedError);
        }

        public async Task<JsonResult> SetTakeOnComplete(int employeeId)
        {
            var result = await _employeeRepository.SetTakeOnComplete(employeeId);
            if (result)
            {
                return HttpStatusCodeResponse.SuccessResponse(string.Empty, string.Format(ResponseMessages.Success, "Take on complete", ActionType.Saved));
            }
            return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.UnexpectedError);
        }

        public async Task<JsonResult> DeleteEmployeeLoan(int employeeLoanId)
        {
            bool isDeleted = await _employeeRepository.DeleteEmployeeLoan(employeeLoanId);

            if (!isDeleted)
            {
                return HttpStatusCodeResponse.NotFoundResponse(ResponseMessages.LoanNotFound);
            }
            return HttpStatusCodeResponse.SuccessResponse(string.Empty, string.Format(ResponseMessages.Success, ResponseMessages.EmployeeLoan, ActionType.Deleted));
        }

        public async Task<JsonResult> GetEmployeeLoanDetail(long employeeId)
        {
            EmployeeLoanResponse? data = await _employeeRepository.GetEmployeeLoanDetail(employeeId);
            return HttpStatusCodeResponse.SuccessResponse(data, string.Format(ResponseMessages.Success, ResponseMessages.EmployeeLoan, ActionType.Retrieved));
        }
        public async Task<JsonResult> GetGarnisheeDropdownData(long companyId)
        {
            GarnisheeDropdownListDto? data = await _employeeRepository.GetGarnisheeDropdownData(companyId);
            return HttpStatusCodeResponse.SuccessResponse(data, string.Format(ResponseMessages.Success, ResponseMessages.Garnishee + " DropDowns", ActionType.Retrieved));
        }

        public async Task<JsonResult> GetGarnisheeDetails(long employeeId)
        {
            List<GarnishDetails>? data = await _employeeRepository.GetGarnisheeDetails(employeeId);
            return HttpStatusCodeResponse.SuccessResponse(data, string.Format(ResponseMessages.Success, ResponseMessages.Garnishee, ActionType.Retrieved));
        }

        public async Task<JsonResult> UpsertGarnishee(EmployeeGarnisheeRequest request)
        {
            bool isSaved = await _employeeRepository.UpsertGarnishee(request);
            if (!isSaved)
            {
                return HttpStatusCodeResponse.NotFoundResponse(ResponseMessages.GarnisheeSavedFailed);
            }
            return HttpStatusCodeResponse.SuccessResponse(string.Empty, string.Format(ResponseMessages.Success, ResponseMessages.Garnishee, ActionType.Saved));
        }

        public async Task<JsonResult> GetSavingsDetails(long employeeId)
        {
            List<SavingsDetails>? data = await _employeeRepository.GetSavingsDetails(employeeId);
            return HttpStatusCodeResponse.SuccessResponse(data, string.Format(ResponseMessages.Success, ResponseMessages.Savings, ActionType.Retrieved));
        }

        public async Task<JsonResult> UpsertSaving(EmployeeSavingsRequest request)
        {
            bool isSaved = await _employeeRepository.UpsertSaving(request);
            if (!isSaved)
            {
                return HttpStatusCodeResponse.NotFoundResponse(ResponseMessages.SavingsSavedFailed);
            }
            return HttpStatusCodeResponse.SuccessResponse(string.Empty, string.Format(ResponseMessages.Success, ResponseMessages.Savings, ActionType.Saved));
        }

        public async Task<JsonResult> GetEmployeeDocument(int employeeId)
        {
            var data = await _employeeRepository.GetEmployeeDocument(employeeId);
            return HttpStatusCodeResponse.SuccessResponse(data, string.Format(ResponseMessages.Success, "Employee Document", ActionType.Retrieved));
        }

        public async Task<JsonResult> UploadDocuments(EmployeeDocumentUpload employeeDocument)
        {
            var basePath = _directoryPathSetting.EmployeeDocument;
            if (!string.IsNullOrEmpty(basePath))
            {
                List<EmployeeDocuments> dbFileList = new List<EmployeeDocuments>();
                var employeeFolder = $"Employee_{employeeDocument.EmployeeId}";
                var finalPath = FileHandler.CombinePath(basePath, employeeFolder);
                FileHandler.CreateDirectory(finalPath);

                foreach (var file in employeeDocument.Document)
                {
                    var isFileReplaced = false;
                    var filePath = Path.Combine(finalPath, file.FileName);
                    isFileReplaced =  FileHandler.DeleteFile(filePath);
                    await FileHandler.UploadFile(filePath, file);

                    if(!isFileReplaced)
                    {
                        var relativePath = Path.Combine(employeeFolder, file.FileName).Replace(Path.DirectorySeparatorChar.ToString(), "/");
                        dbFileList.Add(new EmployeeDocuments()
                        {
                            DocumentName = file.FileName,
                            DocumentUrl = relativePath,
                            DocumentType = file.ContentType,
                            DocumentSize = file.Length
                        });
                    }
                }
                var result = await _employeeRepository.UploadDocumentsSave(dbFileList, employeeDocument.EmployeeId);
                if (result)
                {
                    return HttpStatusCodeResponse.SuccessResponse(string.Empty, string.Format(ResponseMessages.Success, "Employee Document", ActionType.uploaded));
                }
                return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.SomethingWrong);
            }
            return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.UnexpectedError);
        }

        public async Task<JsonResult> DeleteDocuments(EmployeeDocumentDelete reqModel)
        {
            var result = await _employeeRepository.DeleteDocuments(reqModel.DocumentId);
            if (result)
            {
                var basePath = _directoryPathSetting.EmployeeDocument;
                FileHandler.DeleteFile(FileHandler.CombinePath(basePath, reqModel.DocumentUrl));
                return HttpStatusCodeResponse.SuccessResponse(string.Empty, string.Format(ResponseMessages.Success, "Document", ActionType.Deleted));
            }
            return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.UnexpectedError);
        }

        public async Task<byte[]> DownloadDocument(string documentUrl)
        {
            var basePath = _directoryPathSetting.EmployeeDocument;
            return await FileHandler.ReadFileBytes(FileHandler.CombinePath(basePath, documentUrl));
        }

        public async Task<JsonResult> GetEmployeeByEmployeeId(long employeeId, long companyId)
        {
            return await _employeeRepository.GetEmployeeByEmployeeId(employeeId, companyId);
        }

        public async Task<JsonResult> GetSecondApprovalEmployeeListByCompanyId(long companyId)
        {
            return await _employeeRepository.GetSecondApprovalEmployeeListByCompanyId(companyId);
        }

        public async Task<JsonResult> UpdateEmployeeSelfService(UpdateEmployeeSelfServiceModel model)
        {
            return await _employeeRepository.UpdateEmployeeSelfService(model);
        }

        public async Task<JsonResult> GetAccessRolesByCompanyId(long companyId)
        {
            return await _employeeRepository.GetAccessRolesByCompanyId(companyId);
        }

        public async Task<JsonResult> UpsertEmployeeUser(UpsertUserRequestDTO request)
        {
            try
            {
                if (request == null || string.IsNullOrWhiteSpace(request.Email) ||
                    string.IsNullOrWhiteSpace(request.Password) || request.CompanyId <= 0 ||
                    request.AccessRoleId <= 0 || request.EmployeeId <= 0)
                {
                    return HttpStatusCodeResponse.BadRequestResponse();
                }

                // Hash password using PasswordHasher
                string passwordHash = new PasswordHasher<object>().HashPassword(null, request.Password);

                var createUserRequest = new UpsertUserRequestDTO
                {
                    AccessRoleId = request.AccessRoleId,
                    Email = request.Email,
                    Password = request.Password,
                    PasswordHash = passwordHash,
                    CompanyId = request.CompanyId,
                    EmployeeId = request.EmployeeId
                };

                return await _employeeRepository.UpsertEmployeeUser(createUserRequest);
            }
            catch (Exception ex)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse($"Error upserting user: {ex.Message}");
            }
        }

        public async Task<JsonResult> GetPreviousService(int employeeId)
        {
            var previousServiceList = await _employeeRepository.GetPreviousService(employeeId);
            return HttpStatusCodeResponse.SuccessResponse(previousServiceList, string.Format(ResponseMessages.Success, "Previous Service", ActionType.Retrieved));
        }
    }
}
