using ImtahanaHazrlg.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImtahanaHazrlg.ViewModels
{
    public class HomeViewModel
    {
        public List<Team> Teams { get; set; }
        public List <TeamSocialMedia> SocialMedias { get; set; }
    }
}
