using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ProyectoDineroApi.Models
{
    [Table("PRODUCT")]
    public class Product
    {
        [Key]
        [Column("ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        [Column("NAME")]
        public String Name { get; set; }
        [Column("CODE")]
        public String Code { get; set; }
        [Column("TYPE")]
        public String Type { get; set; }
    }
}
