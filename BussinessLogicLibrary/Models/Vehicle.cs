using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLogicLibrary.Models
{
    public abstract class Vehicle
    {
        public virtual int Id { get; set; }
        public virtual string Registration { get; set; }
        public virtual DateTime StartTime { get; set; } = DateTime.Now;
        public virtual int Parkingspot { get; set; }
    }
}
