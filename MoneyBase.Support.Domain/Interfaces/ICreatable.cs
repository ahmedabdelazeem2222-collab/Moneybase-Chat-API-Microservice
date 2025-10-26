using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyBase.Support.Domain.Interfaces
{
    public interface ICreatable
    {
        int CreatedBy { get; set; }
        DateTime CreatedDate { get; set; }
    }
}
