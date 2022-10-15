using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace project_x_da.Entity
{
    [Table("user_forgot_password")]
    public class UserForgotPassword
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("user_id")]
        public int UserId { get; set; }

        [Column("token")]
        public string Token { get; set; }

        [Column("created_date")]
        public DateTime CreatedDate { get; set; }
    }
}