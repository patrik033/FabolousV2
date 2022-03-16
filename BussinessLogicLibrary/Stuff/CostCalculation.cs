using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLogicLibrary
{
    public class CostCalculation
    {
        public TimeSpan ParkedTime(DateTime startTime)
        {
            TimeSpan totalHours = (DateTime.Now - startTime);
            return totalHours;
        }

        public string ParkedTimeToScreen(DateTime startTime)
        {
            TimeSpan totalTime = (DateTime.Now - startTime);
            int days = totalTime.Days;
            int hours = totalTime.Hours;
            int minutes = totalTime.Minutes;
            string totalCalculatedTime = $"Dagar: {days}   Timmar: {hours}   Minuter: {minutes}";
            return totalCalculatedTime;
        }

        public decimal CalculateCost(DateTime startTime, int costPerHour)
        {
            TimeSpan totalHours = (DateTime.Now - startTime);
            decimal total = (decimal)Math.Round(totalHours.TotalHours * costPerHour);
            total = Math.Round(total - (costPerHour / 6));

            if (total > 0)
                return total;
            else
                return 0;
        }
    }
}
