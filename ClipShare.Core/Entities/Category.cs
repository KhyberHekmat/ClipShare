

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ClipShare.Core.Entities
{
    public class Category : BaseEntity
    {
        [Required]
        public string Name { get; set; }

        //Navigations
        public ICollection<Video> Videos { get; set; }
    }
}
