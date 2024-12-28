using System;
using System.ComponentModel.DataAnnotations;

namespace ListApi.Domain.Entities
{
    public class ListEntity
    {
        [Key]
        public int ListId { get; set; } 
        public int UserId { get; set; } 
        public string? ListName { get; set; } 
        public DateTime CreatedDate { get; set; } 
        public DateTime UpdatedDate { get; set; }
    }
}
