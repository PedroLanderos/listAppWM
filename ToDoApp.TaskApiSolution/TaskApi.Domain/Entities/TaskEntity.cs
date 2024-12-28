using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskApi.Domain.Entities
{
    public class TaskEntity
    {
        [Key]
        public int TaskId { get; set; }
        public int ListId { get; set; }
        public bool Finished { get; set; } = false;
        public string? TaskName { get; set; }
        public string? TaskDescription { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedDate { get; set; } = DateTime.UtcNow;
    }
}
