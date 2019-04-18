using Common;
using Common.Exceptions;
using Common.Helpers;
using MDM.UI.Employees.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Web.Controllers;

namespace Admin.Commands.EmployeeCommand
{
    public class UpdateCommandHandler : BaseCommandHandler<UpdateCommand, int>
    {
        private readonly IEmployeeRepository employeeRepository = null;
        private readonly IEmployeeQueries employeeQueries = null;
        public UpdateCommandHandler(IEmployeeRepository employeeRepository, IEmployeeQueries employeeQueries)
        {
            this.employeeRepository = employeeRepository;
            this.employeeQueries = employeeQueries;
        }
        public override async Task<int> HandleCommand(UpdateCommand request, CancellationToken cancellationToken)
        {
            if(request.Employee == null || request.Employee.Id == 0)
            {
                throw new BusinessException("Employee.NotExisted");
            }

            var employee = await employeeQueries.GetEmployee(request.Employee.Id);
            if(employee == null)
            {
                throw new BusinessException("Employee.NotExisted");
            }

            if (request.Employee.ReportTo != null && request.Employee.ReportTo != 0)
            {
                var manager = await employeeQueries.GetEmployee(employee.ReportTo.Value);
                if (manager == null)
                {
                    throw new BusinessException("Employee.NotExistedManager");
                }
            }
            else
            {
                request.Employee.ReportTo = null;
                request.Employee.ReportToCode = null;
            }

            if (request.Employee.ImageData?.Length > Constant.MaxImageLength)
            {
                throw new BusinessException("Image.OutOfLength");
            }

            string oldImageUrl = request.Employee.ImageURL;

            //With ImageData < 100byte. This is a link image. With Image > 100byte, It can a real imageData.
            if (request.Employee.ImageData?.Length > 200)
            {
                string type = CommonHelper.GetImageType(System.Text.Encoding.ASCII.GetBytes(request.Employee.ImageData));
                if (!CommonHelper.IsImageType(type))
                {
                    throw new BusinessException("Image.WrongType");
                }
                string Base64StringData = request.Employee.ImageData.Substring(request.Employee.ImageData.IndexOf(",") + 1);
                string fileName = Guid.NewGuid().ToString().Replace("-", "");
                request.Employee.ImageURL = CommonHelper.SaveImage($"{GlobalConfiguration.EmployeeImagePath}/{DateTime.Now.ToString("yyyyMM")}/", fileName, type, Base64StringData);
            }

            request.Employee.CreatedDate = employee.CreatedDate;
            request.Employee.CreatedBy = employee.CreatedBy;
            request.Employee = UpdateBuild(request.Employee, request.LoginSession);
            request.Employee.Code = employee.Code;
            var rs = await employeeRepository.Update(request.Employee);

            if(rs == 0)
            {
                if (request.Employee.ImageData?.Length > 200)
                {
                    LogHelper.GetLogger().Debug($"ImageData Length: {request.Employee.ImageData.Length}. Deleted Image: {oldImageUrl}");
                    CommonHelper.DeleteImage(oldImageUrl);
                }
            }
            else
            {
                LogHelper.GetLogger().Debug($"Delete Image for process failed. Deleted Image: {request.Employee.ImageURL}");
                CommonHelper.DeleteImage(request.Employee.ImageURL);
            }

            return rs;
        }
    }
}
