using System.ComponentModel.DataAnnotations;

namespace StageAPI.Application.DTOs.Activity
{
    public class UpdateActivityDTO
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string? Title { get; set; }
        [Required]
        public DateTime? Date { get; set; }
        [Required]
        public string? Description { get; set; }
        [Required]
        public string? Category { get; set; }
        [Required]
        public string? City { get; set; }
        [Required]
        public string? Venue { get; set; }
    }
}