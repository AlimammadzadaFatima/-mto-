using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImtahanaHazrlg.Models
{
    public class TeamSocialMedia:BaseEntity
    {
        public string SocialMediaUrl { get; set; }
        public string Icon { get; set; }
        public int TeamId { get; set; }
        public Team Team { get; set; }
        public string Name { get; set; }
    }
}
