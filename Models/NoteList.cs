using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Themes.Models
{
    public class NoteList
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        public int UserId { get; set; }
        public  virtual User? User { get; set; }
        public ICollection<Note> Notes { get; set; } = new List<Note>();
    }
}
