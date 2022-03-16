using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace dacon_exam.Models
{
    
    [Table("CML",Schema="dbo")]
    public class Cml
    {
      
        [Key]
        public int cml_id {get; set;}
        public int cml_number {get; set;}
        
        [ForeignKey("info_fk")]
        public int info_id {get; set;}
        public virtual Info info_fk {get; set;}

        [NotMapped]
        public string line_number {get; set;}
        public string cml_description {get; set;}

        public double actual_outside_diameter {get; set;}

        public double design_thickness {get; set;}

        public double structural_thickness {get; set;}

        public double required_thickness {get; set;}



        
    }
}