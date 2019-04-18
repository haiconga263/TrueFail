using AriSystem.UI.Storages.Interfaces;
using Common;
using Common.Exceptions;
using Common.Helpers;
using DAL;
using MDM.ScheduleActions.Factories;
using MDM.UI.Addresses.Interfaces;
using MDM.UI.Companies.Interfaces;
using MDM.UI.Companies.Models;
using MDM.UI.Companies.ViewModels;
using MDM.UI.Contacts.Interfaces;
using MDM.UI.ScheduleActions.Interfaces;
using MDM.UI.ScheduleActions.Models;
using MDM.UI.Settings.Enumerations;
using MDM.UI.Settings.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;
using Web.Controllers;

namespace Administrator.Commands.CompanyCommands
{
    public class UpdateCompanyCommand : BaseCommand<int>
    {
        public CompanyViewModel Model { set; get; }
        public UpdateCompanyCommand(CompanyViewModel company)
        {
            Model = company;
        }
    }

    public class UpdateCompanyCommandHandler : BaseCommandHandler<UpdateCompanyCommand, int>
    {
        private readonly ICompanyRepository companyRepository = null;
        private readonly ICompanyQueries companyQueries = null;

        private readonly IAddressRepository addressRepository = null;
        private readonly IAddressQueries addressQueries = null;

        private readonly IContactRepository contactRepository = null;
        private readonly IContactQueries contactQueries = null;

        private readonly IStorageFileProvider storageFile = null;
        private readonly IScheduleActionRepository scheduleActionRepository = null;

        private readonly ISettingQueries settingQueries = null;
        public UpdateCompanyCommandHandler(ICompanyRepository companyRepository,
                                            ICompanyQueries companyQueries,
                                            IAddressRepository addressRepository,
                                            IAddressQueries addressQueries,
                                            IContactRepository contactRepository,
                                            IContactQueries contactQueries,
                                            IStorageFileProvider storageFile,
                                            IScheduleActionRepository scheduleActionRepository,
                                            ISettingQueries settingQueries)
        {
            this.companyRepository = companyRepository;
            this.companyQueries = companyQueries;

            this.addressRepository = addressRepository;
            this.addressQueries = addressQueries;

            this.contactRepository = contactRepository;
            this.contactQueries = contactQueries;

            this.storageFile = storageFile;
            this.scheduleActionRepository = scheduleActionRepository;

            this.settingQueries = settingQueries;
        }
        public override async Task<int> HandleCommand(UpdateCompanyCommand request, CancellationToken cancellationToken)
        {
            Company company = null;
            if (request.Model == null || request.Model.Id == 0)
            {
                throw new BusinessException("Company.NotExisted");
            }
            else
            {
                company = await companyQueries.GetByIdAsync(request.Model.Id);
                if (company == null)
                {
                    throw new BusinessException("Company.NotExisted");
                }
            }

            var rs = -1;
            using (var conn = DALHelper.GetConnection())
            {
                conn.Open();
                using (var trans = conn.BeginTransaction())
                {
                    try
                    {
                        request.Model.CreatedBy = company.CreatedBy;
                        request.Model.CreatedDate = company.CreatedDate;

                        request.Model.ModifiedDate = DateTime.Now;
                        request.Model.ModifiedBy = request.LoginSession.Id;

                        var filePath = await storageFile.getCompanyFilePathAsync(request.Model);

                        if ((request.Model.ImageData?.Length ?? 0) > Constant.MaxImageLength)
                            throw new BusinessException("Image.OutOfLength");

                        if (request.Model.IsChangedImage && (request.Model.ImageData?.Length ?? 0) > 0)
                        {
                            string oldLogoPath = company?.LogoPath ?? "";
                            string type = CommonHelper.GetImageType(System.Text.Encoding.ASCII.GetBytes(request.Model.ImageData));
                            if (!CommonHelper.IsImageType(type))
                            {
                                throw new BusinessException("Image.WrongType");
                            }
                            string base64Data = request.Model.ImageData.Substring(request.Model.ImageData.IndexOf(",") + 1);
                            string fileName = Guid.NewGuid().ToString().Replace("-", "");
                            request.Model.LogoPath = await storageFile.SaveCompanyLogo(filePath, type, base64Data);

                            if (string.IsNullOrWhiteSpace(oldLogoPath))
                            {
                                var imageFolderPath = settingQueries.GetValueAsync(SettingKeys.Path_Company);

                                var scheduleAction = (new RemoveFileAction() { FilePath = $"{imageFolderPath}/{oldLogoPath}" })
                                    .GetScheduleAction(request.LoginSession?.Id ?? 0);
                                await scheduleActionRepository.AddAsync(scheduleAction);
                            }
                        }

                        if (await companyRepository.UpdateAsync(request.Model) > 0)
                            rs = 0;
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        if (rs == 0) { trans.Commit(); }
                        else { try { trans.Rollback(); } catch { } }
                    }
                }
            }
            return rs;
        }
    }
}
