using Common;
using Common.Exceptions;
using Common.Helpers;
using MDM.UI.Employees.Interfaces;
using MDM.UI.Employees.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Web.Controllers;

namespace Admin.Commands.EmployeeCommand
{
    public class AddCommandHandler : BaseCommandHandler<AddCommand, int>
    {
        private readonly IEmployeeRepository employeeRepository = null;
        private readonly IEmployeeQueries employeeQueries = null;
        public AddCommandHandler(IEmployeeRepository employeeRepository, IEmployeeQueries employeeQueries)
        {
            this.employeeRepository = employeeRepository;
            this.employeeQueries = employeeQueries;
        }
        public override async Task<int> HandleCommand(AddCommand request, CancellationToken cancellationToken)
        {
            if(request.Employee == null)
            {
                throw new BusinessException("AddWrongInformation");
            }

            var employee = (await employeeQueries.GetEmployees($"code = '{request.Employee.Code}' and is_deleted = 0")).FirstOrDefault();
            if(employee != null)
            {
                throw new BusinessException("Employee.ExistedCode");
            }

            if (request.Employee.ReportTo != null && request.Employee.ReportTo.Value != 0)
            {
                var manager = await employeeQueries.GetEmployee(request.Employee.ReportTo.Value);
                if(manager == null)
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

            request.Employee.Code = await employeeQueries.GenarateCode();
            request.Employee = CreateBuild(request.Employee, request.LoginSession);
            var rs = await employeeRepository.Add(request.Employee);
            if(rs != 0)
            {
                CommonHelper.DeleteImage(request.Employee.ImageURL);
            }

            return rs == 0 ? -1 : 0;
        }
    }
}
