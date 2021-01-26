using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Training.Models
{
    public class Education
    {
        [Key]
        public int Id { get; set; }
        public String Degree { get; set; }
        public String GPA { get; set; }
        public String Univiersity_Id { get; set; } 

        [ForeignKey("University_Id")]
        public virtual University Universities { get; set; }

    }
}
