using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Training.Models
{
    public class Profiling
    {
        [Key]
        public string NIK { get; set; }
        public int Education_Id { get; set; }

        [ForeignKey("Education_Id")]
        public Education Educations { get; set; }
        public virtual Account Account { get; set; }
    }
}
