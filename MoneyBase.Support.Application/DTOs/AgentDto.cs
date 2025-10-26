using MoneyBase.Support.Domain.Entities;
using MoneyBase.Support.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyBase.Support.Application.DTOs
{
    public class AgentDto : BaseEntity<Guid>
    {
        public string Name { get; set; }
        public SeniorityEnum Seniority { get; set; }
        public int ShiftStartHour { get; set; }
        public int ShiftEndHour { get; set; }
        public bool IsOverflow { get; set; }
        public IEnumerable<Guid> AssignedChatIds { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedOn { get; set; }
    }
}
