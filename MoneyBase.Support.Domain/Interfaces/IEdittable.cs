using System;

namespace MoneyBase.Support.Domain.Interfaces
{
    public interface IEdittable
    {
       public int? UpdatedBy { get; set; }
       public DateTime? UpdatedDate { get; set; }
    }
}
