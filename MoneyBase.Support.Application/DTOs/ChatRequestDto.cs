using MoneyBase.Support.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MoneyBase.Support.Application.DTOs
{
    public class ChatRequestDto
    {
        [Required(ErrorMessage = "ChatId is required")]
        [MaxLength(50, ErrorMessage = "ChatId max length 50")]
        public Guid ChatId { get; set; } = Guid.NewGuid(); // default to new GUID

        [Required(ErrorMessage = "UserId required")]
        [MaxLength(20, ErrorMessage = "User Id Max Length 20")]
        public string UserId { get; set; }

        [JsonIgnore]
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow; // default to now
    }
}
