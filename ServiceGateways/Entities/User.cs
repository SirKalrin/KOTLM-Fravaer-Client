using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace ServiceGateways.Entities
{
    public enum Role
    {
        Medarbejder, Afdelingsleder, Administrator
    }
    public class User : AbstractEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        [RegularExpression("^(?=.*[A-Z])(?=.*[a-z])(?=.*[0-9])(?=.*[!#¤%&/()=?@£$€{}*^|<>§]).{6,24}$")]
        public string Password { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public List<Absence> Absences { get; set; }
        public Department Department { get; set; }
        public Role Role { get; set; }

        public User()
        {
            Absences = new List<Absence>();
            Department = new Department();
        }
    }
}
