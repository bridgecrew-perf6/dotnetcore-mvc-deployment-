using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace dacon_exam.Models
{
    [Table("INFO",Schema="dbo")]
    
    public class Info
    {   
        [Key]
        public int Info_id {get; set;}

        public string line_number {get; set;}
        public string location {get; set;}
        public string from {get; set;}
        public string to {get; set;}
        public string drawing_number {get; set;}
        public string service {get; set;}
        public string material {get; set;}
        public DateTime inservice_date {get; set;}
    
        public double pipe_size {get; set;}
        public double original_thickness {get; set;}
        public int stress {get; set;}
        public int joint_efficiency {get; set;}
        public int ca {get; set;}
        public int design_life {get; set;}
        public int design_pressure {get; set;}
        public int design_temperature {get; set;}
        public int operating_pressure {get; set;}

        public double operating_temperature {get; set;}
    }
}