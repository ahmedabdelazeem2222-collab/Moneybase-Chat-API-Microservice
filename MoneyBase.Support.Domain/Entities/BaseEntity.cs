using System;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MoneyBase.Support.Domain.Entities
{
    public class BaseEntity<TId>
    {
        [MaxLength(50)]
        public TId Id { get; set; } = default!;
    }
}
