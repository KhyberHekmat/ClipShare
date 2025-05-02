

using System.ComponentModel.DataAnnotations;

namespace ClipShare.Core.Entities
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }
    }
}
