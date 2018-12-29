using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using DataProcessing.CommonModels;
using DataProcessing.Persistence;

namespace DataProcessing.Application.CustomerDate.Command
{
    public class CreateCustomerDataCsv : ICreateCustomerDataCsv
    {
        private string[] columns = new string[] {
                                        "Numbers",
                                        "Operator",
                                        "Circle",
                                        "ClientName",
                                        "Business Vertical",
                                        "Db Quality",
                                        "Date Of Use",
                                        "City",
                                        "State",
                                        "Country"
                                        };
        private readonly IDownloadRequestRepository _downloadRequestRepository;
        public CreateCustomerDataCsv(IDownloadRequestRepository downloadRequestRepository)
        {
            _downloadRequestRepository = downloadRequestRepository;
        }
        public void Create(List<CustomerDataExportModel> customerData, string filePath, DownloadRequest downloadRequest)
        {
            StringBuilder sb = new StringBuilder();
            //Title of the table
            sb.AppendLine("Numbers,Operator,Circle,ClientName,Business Vertical,Db Quality,Date Of Use,City,State,Country");

            downloadRequest.StatusCode = (int)FileCreateStatus.InProgress;
            _downloadRequestRepository.UpdateAsync(downloadRequest);

            foreach (var item in customerData)
            {
                sb.AppendLine($"{item.Numbers},{item.Operator},{item.Circle},{item.ClientName},{item.ClientBusinessVertical},{item.Dbquality}" +
                    $",{item.DateOfUse},{item.ClientCity},{item.State},{item.Country}");
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
