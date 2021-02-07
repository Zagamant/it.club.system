using System.ComponentModel.DataAnnotations;

namespace System.DAL.Entities
{
    public class Contact : BaseEntity
    {
        [Required] public string Name { get; set; }

        [Required] public string ContactType { get; set; }

        [Required] public string ContactAsIs { get; set; }
    }
}