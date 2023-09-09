using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoatRentalApp {
    class ReservationManager {

        private Dictionary<string, List<Reservation>> _reservations = new Dictionary<string, List<Reservation>>();

        public bool MakeReservation(Inventory inventory, ClientInformation clientInfo, string boatName, DateTime start, DateTime end) {

            Boat boat = inventory.GetAvailableBoats().Find(b => b.Name == boatName);

            if (boat != null) {

                //Generates a unique identifier based on the client information
                string clientId = $"{clientInfo.LastName}_{clientInfo.FirstName}_{clientInfo.DateOfBirth:ddMMyyyy}";

                //Check if the boat is already reserved for any overlapping dates
                if (IsBoatAlreadyReserved(clientId, start, end)) {

                    Console.WriteLine("This boat is already reserved for some or all of the selected dates");
                    return false;
                }

                // Create a new list of reservations for the boat if it doesn't exist
                if (!_reservations.ContainsKey(clientId)) {

                    _reservations[clientId] = new List<Reservation>();
                }

                _reservations[clientId].Add(new Reservation(clientId, start, end));
                return true;
            }

            return false;
        }

        private bool IsBoatAlreadyReserved(string clientId, DateTime start, DateTime end) {

            if (_reservations.ContainsKey(clientId)) {

                foreach (var reservation in _reservations[clientId]) {

                    //Check if the reservation overlaps with the selected dates
                    if (!(start > reservation.EndDate || end < reservation.StartDate)) {

                        return true; //Overlapping reservation found
                    }
                }
            }

            return false; //No overlapping reservation found
        }
    }
}
