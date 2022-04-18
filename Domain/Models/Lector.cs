namespace Domain.Models
{
    public record Lector(int Id, string Name, string Email) : IBaseEntity;
}