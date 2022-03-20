using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseAccessLibrary.Repository.IRepository
{
    public interface IUnitOfWork 
    {
        //TODO uppdatera för att lägga till nya typer
        ICarRepository Car { get; }
        IMotorcycleRepository Motorcycle { get; }
        IBicycleRepository Bicycle { get; }
        IBusRepository Bus { get; }
        void Save();
    }
}
