using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ProyectoDineroApi.Models
{
    [Table("CALCULATOR")]
    public class Calculator
    {
        [Key]
        [Column("ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        [Column("USERID")]
        public int UserId { get; set; }
        [Column("RESULT")]
        public int Result { get; set; }
    }
}
