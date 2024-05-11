using Entities.Common;

namespace Entities.Models;

public class Person : BaseEntityInt
{
    public string? Name { get; set; }
    public string? LastName { get; set; }
    public string? Address { get; set; }
    public int Color { get; set; }
}

