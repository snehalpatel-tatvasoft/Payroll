using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using PalladiumPayroll.DTOs.DTOs.Common;
using PalladiumPayroll.DTOs.DTOs.Employees;
using PalladiumPayroll.DTOs.Miscellaneous;
using PalladiumPayroll.Repositories.Employees;
using static PalladiumPayroll.Helper.Constants.AppConstants;
using static PalladiumPayroll.Helper.Constants.AppEnums;

namespace PalladiumPayroll.Services.Employees
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly DirectoryPathSetting _directorySettings;
        public EmployeeService(IEmployeeRepository employeeRepository, IOptions<DirectoryPathSetting> settings)
        {
            _employeeRepository = employeeRepository;
            _directorySettings = settings.Value;
        }

        public async Task<JsonResult> GetEmployeeFilters(int companyId)
        {
            return await _employeeRepository.GetEmployeeFilters(companyId);
        }

        public async Task<JsonResult> GetEmployeeList(EmployeeFilterViewModel reqModel)
        {
            return await _employeeRepository.GetEmployeeList(reqModel);
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
            if (timeSheetSetup.TimeSheetPassword != timeSheetSetup.TimeSheetConfirmPassword)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse("Password is mismatch !");
            }
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
                return HttpStatusCodeResponse.SuccessResponse(string.Empty, string.Format(ResponseMessages.Success, string.Concat(ResponseMessages.Employee, " ", "Casual Wage Information"), ActionType.Updated));
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
                    string.Format(ResponseMessages.Success,
                    $"{ResponseMessages.Employee} Directive Information", ActionType.Saved));
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
                    string.Format(ResponseMessages.Success, $"{ResponseMessages.Employee} Directive Information", ActionType.Updated));
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
                    string.Format(ResponseMessages.Success, $"{ResponseMessages.Employee} Directive", ActionType.Deleted));
            }

            return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.UnexpectedError);
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
            try
            {
                bool isDeleted = await _employeeRepository.DeleteEmployeeLoan(employeeLoanId);

                if (!isDeleted)
                {
                    return HttpStatusCodeResponse.NotFoundResponse(ResponseMessages.Employee + " loan not found.");
                }
                return HttpStatusCodeResponse.SuccessResponse(string.Empty, string.Format(ResponseMessages.Success, ResponseMessages.Employee + " Loan", ActionType.Deleted));
            }
            catch (Exception)
            {
                return HttpStatusCodeResponse.BadRequestResponse();
            }
        }

        public async Task<JsonResult> GetEmployeeLoanDetail(long employeeId)
        {
            try
            {
                EmployeeLoanResponse? data = await _employeeRepository.GetEmployeeLoanDetail(employeeId);

                return HttpStatusCodeResponse.SuccessResponse(data, string.Format(ResponseMessages.Success, ResponseMessages.Employee + " Loan", ActionType.Retrieved));
            }
            catch (Exception)
            {
                return HttpStatusCodeResponse.BadRequestResponse();
            }
        }
        public async Task<JsonResult> GetGarnisheeDropdownData(long companyId)
        {
            try
            {
                GarnisheeDropdownListDto? data = await _employeeRepository.GetGarnisheeDropdownData(companyId);

                return HttpStatusCodeResponse.SuccessResponse(data, string.Format(ResponseMessages.Success, ResponseMessages.Employee + " Garnishee DropList", ActionType.Retrieved));
            }
            catch (Exception)
            {
                return HttpStatusCodeResponse.BadRequestResponse();
            }
        }

        public async Task<JsonResult> GetGarnisheeDetails(long employeeId)
        {
            try
            {
                List<GarnishDetails>? data = await _employeeRepository.GetGarnisheeDetails(employeeId);

                return HttpStatusCodeResponse.SuccessResponse(data, string.Format(ResponseMessages.Success, ResponseMessages.Employee + " Garnishes", ActionType.Retrieved));
            }
            catch (Exception)
            {
                return HttpStatusCodeResponse.BadRequestResponse();
            }
        }

        public async Task<JsonResult> UpsertGarnishee(EmployeeGarnisheeRequest request)
        {
            try
            {
                bool isSaved = await _employeeRepository.UpsertGarnishee(request);

                if (!isSaved)
                {
                    return HttpStatusCodeResponse.NotFoundResponse("Failed to save "+ ResponseMessages.Employee + " Garnishee");
                }
                return HttpStatusCodeResponse.SuccessResponse(string.Empty, string.Format(ResponseMessages.Success, ResponseMessages.Employee + " Garnishee", ActionType.Saved));

            }
            catch (Exception)
            {
                return HttpStatusCodeResponse.BadRequestResponse();
            }
        }

        public async Task<JsonResult> GetSavingsDetails(long employeeId)
        {
            try
            {
                List<SavingsDetails>? data = await _employeeRepository.GetSavingsDetails(employeeId);

                return HttpStatusCodeResponse.SuccessResponse(data, string.Format(ResponseMessages.Success, ResponseMessages.Employee + " Savings", ActionType.Retrieved));
            }
            catch (Exception)
            {
                return HttpStatusCodeResponse.BadRequestResponse();
            }
        }

        public async Task<JsonResult> UpsertSaving(EmployeeSavingsRequest request)
        {
            try
            {
                bool isSaved = await _employeeRepository.UpsertSaving(request);

                if (!isSaved)
                {
                    return HttpStatusCodeResponse.NotFoundResponse("Failed to save "+ ResponseMessages.Employee + " Saving");
                }
                return HttpStatusCodeResponse.SuccessResponse(string.Empty, string.Format(ResponseMessages.Success, ResponseMessages.Employee + " Savings", ActionType.Saved));

            }
            catch (Exception)
            {
                return HttpStatusCodeResponse.BadRequestResponse();
            }
        }

        public async Task<JsonResult> GetEmployeeDocument(int employeeId)
        {
            var data = await _employeeRepository.GetEmployeeDocument(employeeId);
            return HttpStatusCodeResponse.SuccessResponse(data, string.Format(ResponseMessages.Success, "Employee Document", ActionType.Retrieved));
        }

        public async Task<JsonResult> UploadDocuments(EmployeeDocumentUpload employeeDocument)
        {
            var basePath = _directorySettings.EmployeeDocument;
            if (!string.IsNullOrEmpty(basePath))
            {
                List<EmployeeDocuments> dbFileList = new List<EmployeeDocuments>();
                var employeeFolder = $"Employee_{employeeDocument.EmployeeId}";
                var finalPath = Path.Combine(Directory.GetCurrentDirectory(), basePath, employeeFolder).Replace("/", Path.DirectorySeparatorChar.ToString());
                if (!Directory.Exists(finalPath))
                {
                    Directory.CreateDirectory(finalPath);
                }
                foreach (var file in employeeDocument.Document)
                {
                    var isFileReplced = false;
                    var filePath = Path.Combine(finalPath, file.FileName);
                    if (File.Exists(filePath))
                    {
                        isFileReplced = true;
                        File.Delete(filePath);
                    }
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    if(!isFileReplced)
                    {
                        var relativePath = Path.Combine(employeeFolder, file.FileName).Replace(Path.DirectorySeparatorChar.ToString(), "/");
                        dbFileList.Add(new EmployeeDocuments() { 
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
                var basePath = _directorySettings.EmployeeDocument.Replace("/", Path.DirectorySeparatorChar.ToString());
                var existFilePath = Path.Combine(Directory.GetCurrentDirectory(), basePath, reqModel.DocumentUrl);
                if (File.Exists(existFilePath))
                {
                    File.Delete(existFilePath);
                }
                return HttpStatusCodeResponse.SuccessResponse(string.Empty, string.Format(ResponseMessages.Success, "Document", ActionType.Deleted));
            }
            return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.UnexpectedError);
        }

        public async Task<byte[]> DownloadDocument(string documentUrl)
        {
            byte[] result = { };
            var basePath = _directorySettings.EmployeeDocument;
            var fullPath = Path.Combine(Directory.GetCurrentDirectory(), basePath, documentUrl).Replace("/", Path.DirectorySeparatorChar.ToString());
            if (File.Exists(fullPath))
            {
                result = await File.ReadAllBytesAsync(fullPath);
            }
            return result;
        }
    }
}
