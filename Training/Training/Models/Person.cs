using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Training.Models
{
    public class Person
    {
        [Key]
        public String NIK { get; set; }
        public String FirstName { get; set; }
        public String Phone { get; set; }
        public DateTime BirthDate { get; set; }
        public virtual Account Account { get; set; }
    }
}
