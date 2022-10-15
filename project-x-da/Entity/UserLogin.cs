using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace project_x_da.Entity
{
    [Table("user_login")]
    public class UserLogin
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("user_id")]
        public int UserId { get; set; }

        [Column("login_date")]
        public DateTime LoginDate { get; set; }

        [Column("ip")]
        public string Ip { get; set; }
    }
}