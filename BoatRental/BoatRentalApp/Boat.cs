using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoatRentalApp {
    class Boat {

        internal string Name { get; }
        internal BoatType Type { get; }
        internal int Capacity { get; }
        internal decimal PricePerDay { get; }
        internal bool IsAvailable { get; set; }

        public Boat(string name, BoatType type, int Capacity, decimal PricePerDay) {

            this.Name = name;
            this.Type = type;
            this.Capacity = Capacity;
            this.PricePerDay = PricePerDay;
        }
    }
}
