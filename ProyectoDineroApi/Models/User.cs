using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ProyectoDineroApi.Models
{
    [Table("USER")]
    public class User
    {
        [Key]
        [Column("ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        [Column("USERNAME")]
        public String Username { get; set; }
        [Column("PASSWORD")]
        public String Password { get; set; }
        [Column("EMAIL")]
        public String Email { get; set; }
        [Column("NAME")]
        public String Name { get; set; }
        [Column("SURNAME")]
        public String Surname { get; set; }
        [Column("ROLE_ID")]
        public int RoleId { get; set; }
        [Column("PASS_SALT")]
        public string PasswordSalt { get; set; }
        [Column("ACTIVATED")]
        public bool Activated { get; set; }
    }
}
