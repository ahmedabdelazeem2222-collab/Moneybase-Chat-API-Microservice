using MoneyBase.Support.Domain.Enums;
using MoneyBase.Support.Domain.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace MoneyBase.Support.Domain.Entities
{
    public class Agent : BaseEntity<Guid>, ICreatable, IEdittable, IDeletable
    {
        /// <summary>
        /// I am using Data Annotations here for demo purposes, but Fluent API is preferred for scalable apps
        /// </summary>
        /// 
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        public SeniorityEnum Seniority { get; set; }
        public int ShiftStartHour { get; set; }
        public int ShiftEndHour { get; set; }
        public bool IsOverflow { get; set; } = false;
        public HashSet<Guid> AssignedChatIds { get; init; } = new HashSet<Guid>();
        public int CreatedBy { get; set; } = 0; // default system user
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public int? UpdatedBy { get; set; } = null;
        public DateTime? UpdatedDate { get; set; } = null;
        public bool IsDeleted { get; set; } = false;
        public DateTime? DeletedOn { get; set; } = null;
    }
}
