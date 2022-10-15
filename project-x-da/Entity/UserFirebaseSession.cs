using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project_x_da.Entity
{
    [Table("user_firebase_session")]
    public class UserFirebaseSession
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("user_id")]
        public int UserId { get; set; }
        [Column("ip")]
        public string Ip { get; set; }

        [Column("created_date")]
        public DateTime CreatedDate { get; set; }
        [Column("created_by")]
        public int CreatedBy { get; set; }
        [Column("expired_date")]
        public DateTime ExpiredDate { get; set; }
        [Column("description")]
        public string Description { get; set; }
        [Column("firebase_token")]
        public string FirebaseToken { get; set; }
        [Column("is_disabled")]
        public bool IsDisabled { get; set; }
    }
}
