using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace dacon_exam.Models
{
    
    [Table("TEST_POINT",Schema="dbo")]
    public class TestPoint
    {
      
        [Key]
        public int tp_id {get; set;}

        public int tp_number {get; set;}

        [ForeignKey("cml_fk")]
        public int cml_id {get; set;}
        public virtual Cml cml_fk {get; set;}
        public int tp_description {get; set;}
        public string note {get; set;}
        public string line_number {get; set;}

       

    }
}