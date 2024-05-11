using System.ComponentModel.DataAnnotations;

namespace Entities.Common;

public abstract class BaseEntity<TKey>
{
    [Key]
    public TKey Id { get; set; }
}