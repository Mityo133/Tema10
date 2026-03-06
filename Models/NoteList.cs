using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Themes.Models
{
    public class NoteList : BaseEntity
    {
        [Required]
        [MaxLength(100)]
        public required string Name { get; set; }

        public required string UserId { get; set; }
        public virtual ApplicationUser? User { get; set; }

        public ICollection<Note> Notes { get; set; } = new List<Note>();
    }
}
