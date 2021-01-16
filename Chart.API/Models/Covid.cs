using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chart.API.Models
{
    public class Covid
    {
        public int Id { get; set; }
        public CityEnum City { get; set; }
        public int Count { get; set; }
        public DateTime CovidDate { get; set; }
    }
}
