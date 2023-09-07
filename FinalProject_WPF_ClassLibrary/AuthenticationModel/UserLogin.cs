using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace FinalProject_WPF_ClassLibrary.AuthenticationModel
{
    public class UserLogin
    {
        [Required, MaxLength(20)]
        public string LoginProp { get; set; }

        [Required, DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
