using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace project_x_da.Entity
{
    [Table("user_remember_token")]
    public class UserRememberToken
    {
        [Key]
        [JsonIgnore]
        [Column("id")]
        public int Id { get; set; }

        [Column("user_id")]
        public int UserId { get; set; }

        [Column("token")]
        public string Token { get; set; }

        [Column("expired_date")]
        public DateTime Expires { get; set; }

        [Column("created_date")]
        public DateTime CreatedDate { get; set; }

        [Column("created_ip")]
        public string CreatedByIp { get; set; }

        [Column("revoked_date")]
        public DateTime RevokedDate { get; set; }

        [Column("revoked_ip")]
        public string RevokedByIp { get; set; }
    }
}