using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ProyectoDineroApi.Models
{
    [Table("ROLE")]
    public class Role
    {
        [Key]
        [Column("ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        [Column("NAME")]
        public String Name { get; set; }
        public ICollection<User> Users { get; set; }
    }
}
