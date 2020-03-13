using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CA2API
{
    public class Dog
    {
           [Key]
        public String ID { get; set; }
        public String Name { get; set; }
        public String Breed { get; set; }
        public double Age { get; set; }
        public String Information { get; set; }
        public String ImageURL { get; set; }
        public bool IsAdopted { get; set; }

           
    }
}
