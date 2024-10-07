namespace AdventureWorks.Http.Responses.HumanResources.v1
{
    public class DepartmentResponseModel
    {
        public short DepartmentId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string GroupName { get; set; } = string.Empty;
        public DateTime ModifiedDate { get; set; }
    }
}
