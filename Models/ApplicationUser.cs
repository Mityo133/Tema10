using Microsoft.AspNetCore.Identity;

namespace Themes.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public virtual ICollection<NoteList>? NoteLists { get; set; } = new List<NoteList>();
    }
}