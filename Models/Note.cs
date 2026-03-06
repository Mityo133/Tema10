using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Themes.Models
{
    public class Note : BaseEntity
    {

        [Required]
        [MaxLength(150)]
        public string Title { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        [MaxLength(1000)]
        public string Description { get; set; }

        public int NoteListId { get; set; }
        public virtual NoteList? NoteList { get; set; }
    }
}
