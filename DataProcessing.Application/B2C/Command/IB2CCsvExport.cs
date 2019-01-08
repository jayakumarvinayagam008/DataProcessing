using DataProcessing.CommonModels;
using DataProcessing.Persistence;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DataProcessing.Application.B2C.Command
{
    public interface IB2CCsvExport
    {
        void Create(List<B2CSearchResult> businessToCustomers, 
            string filePath, DownloadRequest downloadRequest);
    }
    public class B2CCsvExport : IB2CCsvExport
    {
        private readonly IDownloadRequestRepository _downloadRequestRepository;

        public B2CCsvExport(IDownloadRequestRepository downloadRequestRepository)
        {
            _downloadRequestRepository = downloadRequestRepository;
        }
        public void Create(List<B2CSearchResult> businessToCustomers, string filePath, DownloadRequest downloadRequest)
        {
            StringBuilder sb = new StringBuilder();
            //Title of the table
            sb.AppendLine($"Name,Dob,Qualification,Experience,Employer,KeySkills,Location,Roles,Industry,Address,Address2" +
                $",Email,PhoneNew,MobileNew,Mobile2,AnnualSalary,Pincode,Area,City,State,Country,Network,Gender,Caste");

            downloadRequest.StatusCode = (int)FileCreateStatus.InProgress;
            _downloadRequestRepository.UpdateAsync(downloadRequest);

            foreach (var item in businessToCustomers)
            {
                sb.AppendLine($"{item.Name},{item.Dob},{item.Qualification},{item.Experience},{item.Employer}" +
                    $",{item.KeySkills},{item.Location},{item.Roles},{item.Industry},{item.Address},{item.Address2}" +                   
                    $",{item.Email},{item.PhoneNew},{item.MobileNew},{item.Mobile2},{item.AnnualSalary},{item.Pincode},{item.Area}" +
                    $",{item.City},{item.State},{item.Country},{item.Network},{item.Gender},{item.Caste}");
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
