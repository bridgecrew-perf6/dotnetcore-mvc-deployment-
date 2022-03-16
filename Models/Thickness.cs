using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace dacon_exam.Models
{
    
    [Table("THICKNESS",Schema="dbo")]
    public class Thickness
    {
        [Key]
        public int thickness_id {get; set;}

        [ForeignKey("tp_number_fk")]
        public int tp_number {get; set;}
        public virtual TestPoint tp_number_fk {get; set;}
        public DateTime inspection_date {get; set;}
        
        public double actual_thickness {get; set;}

        public int cml_id {get; set;}
        public string line_number {get; set;}

      



    }
}