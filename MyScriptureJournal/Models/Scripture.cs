using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace MyScriptureJournal.Models
{
    public class Scripture
    {
        public int Id { get; set; }


        [Display(Name = "Standard Work")]
        [StringLength(60, MinimumLength = 3)]
        [Required]
        public string StandardWork { get; set; } = string.Empty;

        [StringLength(60, MinimumLength = 3)]

        public string Book { get; set; } = string.Empty;

        [Required]
        public int Chapter { get; set; }

        [RegularExpression(@"^\d+(-\d+)?$", ErrorMessage = "Verse must be a number or a range (e.g., '16' or '16-18').")]
        [Required]
        public string Verse { get; set; } = string.Empty;

        [Display(Name = "Verse Text")]
        [Required]
        public string VerseText { get; set; } = string.Empty;


        [StringLength(1000, ErrorMessage = "Notes cannot exceed 1000 characters.")]
        public string? Notes { get; set; }


        [Display(Name = "Date")]
        [DataType(DataType.Date)]
        public DateTime SaveDate { get; set; } = DateTime.Now;
    }
}
