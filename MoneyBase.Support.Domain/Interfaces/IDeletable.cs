using System;

namespace MoneyBase.Support.Domain.Interfaces
{
    public interface IDeletable
    {
        bool IsDeleted { get; set; }
        DateTime? DeletedOn { get; set; }
    }
}
