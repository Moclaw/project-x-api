using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project_x_da.PostModels
{
    public class LoginPostModel
    {
        [Required]
        [StringLength(200)]
        public string? Email { get; set; }

        [Required]
        [StringLength(50)]
        public string? Password { get; set; }

        public bool IsRemember { get; set; }
    }
}
