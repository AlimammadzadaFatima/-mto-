using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ImtahanaHazrlg.Models
{
    public class Team:BaseEntity
    {
        public string FullName { get; set; }
        public string Profession { get; set; }
        public string Image { get; set; }
        public List<TeamSocialMedia> SocialMedias { get; set; }
        [NotMapped]
        public IFormFile FormFile  { get; set; }
    }
}
