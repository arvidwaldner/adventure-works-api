namespace AdventureWorks.Http.Responses.Production.v1
{
    public class LocationResponseModel
    {
        public short LocationId { get; set; }
        public string Name { get; set; } = null!;
        public decimal CostRate { get; set; }
        public decimal Availability { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
