using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoatRentalApp {
     class Reservation {

        public string BoatName { get; }
        public DateTime StartDate { get; }
        public DateTime EndDate { get; }

        public Reservation(string boatName, DateTime start, DateTime end) { 
            
            this.BoatName = boatName;
            this.StartDate = start;
            this.EndDate = end;
        }

        public bool IsBoatAvailable(Dictionary<string, List<Reservation>> boatReservations, string boatName, DateTime start, DateTime end) {

            //Check if the boat is already in use in the given period
            if (boatReservations.ContainsKey(boatName)) {

                foreach (var reservation in boatReservations[boatName]) {

                    if (start >= reservation.StartDate && start <= reservation.EndDate ||
                        end >= reservation.StartDate && end >= reservation.EndDate) {

                        return false; //The boat is already reserved for a part of the given period
                    }
                }
            }

            return true; //The boat is available
        }
    }
}
