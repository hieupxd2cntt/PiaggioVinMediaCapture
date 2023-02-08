using VINMediaCaptureEntities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VINMediaCaptureEntities.Model
{
    public class UserLoginModel
    {
        public User User { get; set; }
        public Branch Branch { get; set; }
        public UserLoginModel()
        {
            User = new User();
            Branch = new Branch();
        }
    }
}
