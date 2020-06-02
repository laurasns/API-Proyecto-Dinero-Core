using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ProyectoDineroApi.Models
{
    [Table("RESOURCE")]
    public class Resource
    {
        [Key]
        [Column("ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        [Column("TYPE")]
        public String Type { get; set; }
        [Column("NAME")]
        public String Name { get; set; }
        [Column("URL")]
        public String Url { get; set; }
        [Column("DESCRIPTION")]
        public String Description { get; set; }
        [Column("IMAGE")]
        public String Image { get; set; }
        [Column("AUTHOR")]
        public String Author { get; set; }
    }
}
