using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Training.Models
{
    public class University
    {
        [Key]
        public int Id { get; set; }
        public String Name { get; set; }
        public bool IsAvailable { get; set; }
    }
}
