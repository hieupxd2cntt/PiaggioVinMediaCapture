using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using VINMediaCaptureEntities.Entities;

namespace VINMediaCaptureEntities.ViewModel
{
    public class UserViewModel
    {
        public VINMediaCaptureEntities.Entities.Users Search { get; set; }
        public List<VINMediaCaptureEntities.Entities.Users> Users { get; set; }
        public UserViewModel()
        {
            Users = new List<VINMediaCaptureEntities.Entities.Users>();
            Search = new VINMediaCaptureEntities.Entities.Users();
        }
    }
}
