using BussinessLogicLibrary.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLogicLibrary
{
    public class Motorcycle : Vehicle
    {
        public override int Id { get; set; }
        public  int Size { get; } = 2;
        //public string Type { get; set; } = "MC";

        [Required(ErrorMessage = "Please provide a registration number")]
        [RegularExpression(@"[\d\w\s-]+", ErrorMessage = "Please use only letter, numbers, dash or space")]
        [StringLength(maximumLength: 8)]
        public override string Registration { get; set; }
        public override DateTime StartTime { get; set; } = DateTime.Now;
        public override int Parkingspot { get; set; }
    }
}
