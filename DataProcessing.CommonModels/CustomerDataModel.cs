using System;
using System.Collections.Generic;
using System.Text;

namespace DataProcessing.CommonModels
{
    public class CustomerDataModel
    {
        public string Numbers { get; set; }
        public string Operator { get; set; }
        public string Circle { get; set; }
        public string ClientName { get; set; }
        public string ClientBusinessVertical { get; set; }
        public string Dbquality { get; set; }
        public DateTime? DateOfUse { get; set; }
        public string ClientCity { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public int? RequestId { get; set; }
    }
}
