using AriSystem.UI.Storages.Interfaces;
using Common;
using Common.Exceptions;
using Common.Helpers;
using DAL;
using MDM.UI.Addresses.Interfaces;
using MDM.UI.Companies.Interfaces;
using MDM.UI.Companies.Models;
using MDM.UI.Companies.ViewModels;
using MDM.UI.Contacts.Interfaces;
using MDM.UI.ScheduleActions.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;
using Web.Controllers;

namespace Administrator.Commands.CompanyCommands
{
    public class InsertCompanyCommand : BaseCommand<int>
    {
        public CompanyViewModel Model { set; get; }
        public InsertCompanyCommand(CompanyViewModel company)
        {
            Model = company;
        }
    }

    public class InsertCompanyCommandHandler : BaseCommandHandler<InsertCompanyCommand, int>
    {
        private readonly ICompanyRepository companyRepository = null;
        private readonly ICompanyQueries companyQueries = null;

        private readonly IAddressRepository addressRepository = null;
        private readonly IAddressQueries addressQueries = null;

        private readonly IContactRepository contactRepository = null;
        private readonly IContactQueries contactQueries = null;

        private readonly IStorageFileProvider storageFile;
        private readonly IScheduleActionRepository scheduleActionRepository;
        public InsertCompanyCommandHandler(ICompanyRepository companyRepository,
                                            ICompanyQueries companyQueries,
                                            IAddressRepository addressRepository,
                                            IAddressQueries addressQueries,
                                            IContactRepository contactRepository,
                                            IContactQueries contactQueries,
                                            IStorageFileProvider storageFile,
                                            IScheduleActionRepository scheduleActionRepository)
        {
            this.companyRepository = companyRepository;
            this.companyQueries = companyQueries;

            this.addressRepository = addressRepository;
            this.addressQueries = addressQueries;

            this.contactRepository = contactRepository;
            this.contactQueries = contactQueries;

            this.storageFile = storageFile;
            this.scheduleActionRepository = scheduleActionRepository;
        }
        public override async Task<int> HandleCommand(InsertCompanyCommand request, CancellationToken cancellationToken)
        {
            var id = 0;
            using (var conn = DALHelper.GetConnection())
            {
                conn.Open();
                using (var trans = conn.BeginTransaction())
                {
                    try
                    {
                        request.Model.Address.CreatedDate = DateTime.Now;
                        request.Model.Address.CreatedBy = request.LoginSession?.Id ?? 0;
                        request.Model.Address.ModifiedDate = DateTime.Now;
                        request.Model.Address.ModifiedBy = request.LoginSession?.Id ?? 0;
                        var addressId = await addressRepository.AddAsync(request.Model.Address);


                        request.Model.Contact.CreatedDate = DateTime.Now;
                        request.Model.Contact.CreatedBy = request.LoginSession?.Id ?? 0;
                        request.Model.Contact.ModifiedDate = DateTime.Now;
                        request.Model.Contact.ModifiedBy = request.LoginSession?.Id ?? 0;
                        var contactId = await contactRepository.AddAsync(request.Model.Contact);

                        request.Model.AddressId = addressId;
                        request.Model.ContactId = contactId;
                        request.Model.CreatedDate = DateTime.Now;
                        request.Model.CreatedBy = request.LoginSession?.Id ?? 0;
                        request.Model.ModifiedDate = DateTime.Now;
                        request.Model.ModifiedBy = request.LoginSession?.Id ?? 0;
                        id = await companyRepository.AddAsync(request.Model);

                        Company company = await companyQueries.GetByIdAsync(id);

                        var filePath = await storageFile.getCompanyFilePathAsync(company);

                        if ((request.Model.ImageData?.Length ?? 0) > Constant.MaxImageLength)
                            throw new BusinessException("Image.OutOfLength");

                        if (request.Model.IsChangedImage && (request.Model.ImageData?.Length ?? 0) > 0)
                        {
                            string type = CommonHelper.GetImageType(System.Text.Encoding.ASCII.GetBytes(request.Model.ImageData));
                            if (!CommonHelper.IsImageType(type))
                            {
                                throw new BusinessException("Image.WrongType");
                            }
                            string base64Data = request.Model.ImageData.Substring(request.Model.ImageData.IndexOf(",") + 1);
                            string fileName = Guid.NewGuid().ToString().Replace("-", "");
                            company.LogoPath = await storageFile.SaveCompanyLogo(filePath, type, base64Data);

                            await companyRepository.UpdateAsync(company);
                        }
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        if (id > 0) { trans.Commit(); }
                        else { try { trans.Rollback(); } catch { } }
                    }
                }
            }

            return id;
        }
    }
}
