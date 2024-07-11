using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class AppUser:IdentityUser<int>
    {
      
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Gender { get; set; }
        public int ConfirmCode { get; set; }
  
    }
}
