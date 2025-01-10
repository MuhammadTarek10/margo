using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Domain.Entities;

public class User : IdentityUser<Guid>
{
    [MaxLength(100)]
    public required string FirstName { get; set; }
    public string? LastName { get; set; }

    public List<Order> Orders { get; set; } = [];

}

