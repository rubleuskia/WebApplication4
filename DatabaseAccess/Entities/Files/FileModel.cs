using System.ComponentModel.DataAnnotations;
using DatabaseAccess.Entities.Common;

namespace DatabaseAccess.Entities.Files
{
    public class FileModel : BaseEntity
    {
        [Required]
        [MaxLength(255)]
        public string Name { get; set; }

        [Required]
        [MaxLength(4000)]
        public string Path { get; set; }
    }
}