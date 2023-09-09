using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoatRentalApp {
      class Inventory {

        private Dictionary<string, Boat> _inventory = new Dictionary<string, Boat>();
        private Dictionary<string, List<Reservation>> _reservations = new Dictionary<string, List<Reservation>>();
        private Reservation _reservationManager = new Reservation("", DateTime.Now, DateTime.Now);

        public void AddBoat(Boat boat) {

            _inventory[boat.Name] = boat;
        }

        public List<Boat> GetAvailableBoats() {

            List<Boat> availableBoats = new List<Boat>();

            foreach (var kvp in _inventory) {
                string boatName = kvp.Key;
                Boat boat = kvp.Value;

                // Check if the boat is available (not reserved)
                if (_reservationManager.IsBoatAvailable(_reservations, boatName, DateTime.Now, DateTime.Now)) {
                    availableBoats.Add(boat);
                }
            }

            return availableBoats;
        }

        public Boat GetBoatByName(string name) {

            //If the name of the boat is founded in the inventory then that boat wil be returned..
            if (_inventory.TryGetValue(name, out Boat boat)) {

                return boat;
            }
            else {
                return null; //No boat found by the given name..
            }
        }

        public bool AddReservation(string clientInfo, DateTime start, DateTime end) {

            if (!_inventory.ContainsKey(clientInfo)) {

                Console.WriteLine("This boat isn't found in the inventory.");
                return false; //Reservation failed..
            }
             
            //Call the ReserveBoat method from the Reservation class
            Reservation reservation = new Reservation(clientInfo, start, end);
            bool isReserved = reservation.IsBoatAvailable(_reservations, clientInfo, start, end);

            return isReserved; //Reservation succeeded..
        }
    }
}
