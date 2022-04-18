namespace Domain.Models
{
    public record Student(int Id, string Name, string Email, string PhoneNumber) : IBaseEntity;
}