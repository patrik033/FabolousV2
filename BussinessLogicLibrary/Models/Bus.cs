﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLogicLibrary.Models
{
    public class Bus : Vehicle
    {
        public override int Id { get; set; }
        public int Size { get; set; } = 16;
        //public string Type { get; set; } = "Car";

        [Required(ErrorMessage = "Please provide a registration number")]
        [RegularExpression(@"[\d\w\s-]+", ErrorMessage = "Please use only letter, numbers, dash or space")]
        [StringLength(maximumLength: 8)]
        public override string Registration { get; set; }
        public override DateTime StartTime { get; set; } = DateTime.Now;
        public override int Parkingspot { get; set; }
    }
}
