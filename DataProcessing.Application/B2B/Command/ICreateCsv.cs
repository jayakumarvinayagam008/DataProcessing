using DataProcessing.CommonModels;
using DataProcessing.Persistence;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DataProcessing.Application.B2B.Command
{
    public interface ICreateCsv
    {
        void Create(List<BusinessToBusinessModel> businessToBusinesses, string filePath, DownloadRequest downloadRequest);
    }

    public class CreateCsv : ICreateCsv
    {
        private string[] columns = new string[] {
                                        "Add1",
                                        "Add2",
                                        "City",
                                        "Area",
                                        "Pincode",
                                        "State",
                                        "PhoneNew",
                                        "MobileNew",
                                        "Phone1",
                                        "Phone2",
                                        "Mobile1",
                                        "Mobile2",
                                        "Fax",
                                        "Email",
                                        "Email1",
                                        "Web",
                                        "ContactPerson",
                                        "Contactperson1",
                                        "Designation",
                                        "Designation1",
                                        "EstYear",
                                        "LandMark",
                                        "NoOfEmp",
                                        "Country",
                                        "CompanyName",
                                        "CategoryName"};

        private readonly IDownloadRequestRepository _downloadRequestRepository;

        public CreateCsv(IDownloadRequestRepository downloadRequestRepository)
        {
            _downloadRequestRepository = downloadRequestRepository;
        }

        public void Create(List<BusinessToBusinessModel> businessToBusinesses, string filePath, DownloadRequest downloadRequest)
        {
            StringBuilder sb = new StringBuilder();
            //Title of the table
            sb.AppendLine("Add1,Add2,City,Area,Pincode,State,PhoneNew,MobileNew,Phone1,Phone2,Mobile1,Mobile2,Fax,Email,Email1,Web," +
                "ContactPerson,Contactperson1,Designation,Designation1,EstYear,LandMark,NoOfEmp,Country,CompanyName");

            downloadRequest.StatusCode = (int)FileCreateStatus.InProgress;
            _downloadRequestRepository.UpdateAsync(downloadRequest);

            foreach (var item in businessToBusinesses)
            {
                sb.AppendLine($"{item.Add1},{item.Add2},{item.City},{item.Area},{item.Pincode},{item.State}" +
                    $",{item.PhoneNew},{item.MobileNew},{item.Phone1},{item.Phone2},{item.Mobile1},{item.Mobile2},{item.Fax},{item.Email}" +
                    $",{item.Email1},{item.Web},{item.ContactPerson},{item.Contactperson1},{item.Designation},{item.Designation1}" +
                    $",{item.EstYear},{item.LandMark},{item.NoOfEmp},{item.Country},{item.CompanyName}");
            }
            // This text is added only once to the file.
            if (!File.Exists(filePath))
            {
                // Create a file to write to.
                File.WriteAllText(filePath, sb.ToString());
            }
            downloadRequest.StatusCode = (int)FileCreateStatus.Completed;
            _downloadRequestRepository.UpdateAsync(downloadRequest);
        }
    }
}