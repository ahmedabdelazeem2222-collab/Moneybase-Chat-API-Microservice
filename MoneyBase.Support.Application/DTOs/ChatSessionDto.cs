using MoneyBase.Support.Domain.Entities;
using MoneyBase.Support.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyBase.Support.Application.DTOs
{
    public class ChatSessionDto : BaseEntity<Guid>
    {
        public string UserId { get; set; }
        public ChatStatusEnum ChatStatus { get; set; }
        public DateTime LastPollAtUtc { get; set; } = DateTime.UtcNow;
        public Guid AgentId { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public int? UpdatedBy { get; set; } = null;
        public DateTime? UpdatedDate { get; set; } = null;
        public bool IsDeleted { get; set; } = false;
        public DateTime? DeletedOn { get; set; } = null;
    }
}
