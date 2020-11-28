using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BachelorMid.Models
{
    public class ValuesModel
    {
        public DateTime DateTime { get; set; }
        [Key]
        public int MicrocontrollerID { get; set; }
        public float Temperature { get; set; }
        public float Humidity { get; set; }
        public float Dust { get; set; }
        public bool DoorOpen { get; set; }
        public float Power { get; set; }
    }
}
