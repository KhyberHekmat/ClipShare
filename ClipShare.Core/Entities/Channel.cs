

using System;
using System.Collections;
using System.Collections.Generic;

namespace ClipShare.Core.Entities
{
    public class Channel:BaseEntity
    {
        public string Name { get; set; }
        public string About { get; set; }
        public DateTime CreatedAt { get; set; }
        public int AppUserId { get; set; }

        //Navigation properties
        public AppUser AppUser { get; set; }
        public ICollection<Video> Videos { get; set; }
        public ICollection<Subscribe> Subscribers { get; set; }
    }
}
