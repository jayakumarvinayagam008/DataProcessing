namespace DataProcessing.CommonModels
{
    public class CustomerDataSearchSummary
    {
        public string SearchId { get; set; }

        public decimal Numbers { get; set; }
        public decimal Operator { get; set; }
        public decimal Circle { get; set; }
        public decimal ClientName { get; set; }
        public decimal ClientBusinessVertical { get; set; }
        public decimal Dbquality { get; set; }
        public decimal DateOfUse { get; set; }

        public decimal Country { get; set; }
        public decimal State { get; set; }
        public decimal ClientCity { get; set; }
        public int SearchCount { get; set; }
        public long Total { get; set; }
    }
}

/*
 * searchSummaryBoard.Add1 = (response.Select(x => !string.IsNullOrWhiteSpace(x.Add1)).Count() / (decimal)searchTotoal) * 100;

     */