using MoneyBase.Support.Domain.Enums;
using MoneyBase.Support.Domain.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace MoneyBase.Support.Domain.Entities
{
    public class ChatSession : BaseEntity<Guid>, ICreatable, IEdittable, IDeletable
    {
        /// <summary>
        /// I am using Data Annotations here for demo purposes, but Fluent API is preferred for scalable apps
        /// </summary>
        
        [MaxLength(20)] [Required]
        public string UserId { get; set; }

        [Required]
        public ChatStatusEnum ChatStatus { get; set; }
        public DateTime LastPollAtUtc { get; set; } = DateTime.UtcNow;
        public DateTime AssignedAt { get; set; }
        public Guid? AgentId { get; set; }
        public virtual Agent Agent { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedOn { get; set; }
    }
}
