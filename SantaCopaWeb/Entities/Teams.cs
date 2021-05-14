using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SantaCopaWeb.Entities
{
    public class Teams
    {
        [Key]
        public int TeamId { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
    }
}
