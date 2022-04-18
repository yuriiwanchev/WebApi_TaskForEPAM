using System;

namespace Domain.Models
{
    public record Lection(int Id, string Name, int LectorId, DateTime Date) : IBaseEntity;
}