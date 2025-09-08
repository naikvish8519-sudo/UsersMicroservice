using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Core.DTO
{
 public class UserInfo
    {

        public Guid UserId { get; set; }
        public string? Email { get; set; }
        public string? Username { get; set; }
    }
}
