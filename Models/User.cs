using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Themes.Models
{
    public class User : IdentityUser
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }

        [Phone]
        public string PhoneNumber { get; set; }

        public ICollection<NoteList> NotesListss { get; set; } = new List<NoteList>();
    }
}
