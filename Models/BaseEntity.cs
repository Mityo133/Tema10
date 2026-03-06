using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Themes.Models
{
    public abstract class BaseEntity
    {
        [Key]
        public int Id { get; set; }
    }
}