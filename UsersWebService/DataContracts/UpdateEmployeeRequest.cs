namespace UsersWebService.DataContracts
{
    public class UpdateEmployeeRequest
    {
        public long Id { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? Phone { get; set; }
        public long? CompanyId { get; set; }
        public long? PassportId { get; set; }
        public long? DepartmentId { get; set; }
    }
}
