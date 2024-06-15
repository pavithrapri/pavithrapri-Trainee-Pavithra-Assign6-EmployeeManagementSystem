namespace EmployeeManagementSystems_A6.Models
{
    public class BaseEntity
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

    }
}
