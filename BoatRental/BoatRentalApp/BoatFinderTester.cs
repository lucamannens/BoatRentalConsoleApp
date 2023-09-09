using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using BoatRentalApp.Transactions;

namespace BoatRentalApp {
    public class BoatFinderTester {

        public static void boatapptest(string[] args) {

            Inventory inventory = new Inventory();
            Reservation reservation = new Reservation("", DateTime.Now, DateTime.Now);
            ReservationManager _reservationManager = new ReservationManager();

            inventory.AddBoat(new Boat("Sailboat 1", BoatType.sailboat, 4, 80)); // Testprice per day: 80
            inventory.AddBoat(new Boat("Motorboat 1", BoatType.motorboat, 6, 150)); // Testprice per day: 150
            inventory.AddBoat(new Boat("Catamaran 1", BoatType.catamaran, 8, 250)); // Testprice per day: 250

            while (true) {

                Console.WriteLine("Welcome to the boat rental program!");
                Console.WriteLine("1. Show available boats");
                Console.WriteLine("2. Make reservation");
                Console.WriteLine("3. Close");
                Console.Write("Make a choice: ");

                string choice = Console.ReadLine().Trim();

                switch (choice) {
                    case "1":

                        List<Boat> availableBoats = inventory.GetAvailableBoats();
                        Console.WriteLine("Available boats:");

                        foreach (var boat in availableBoats) {
                            Console.WriteLine($"Name: {boat.Name}, Type: {boat.Type}, Capacity: {boat.Capacity}, Price per day: {boat.PricePerDay:C}");
                        }
                        break;

                    case "2":

                        Console.Write("Enter the name of the boat: ");
                        string boatName = Console.ReadLine();
                        if (string.IsNullOrWhiteSpace(boatName)) {
                            Console.WriteLine("Invalid boat name!");
                            break;
                        }

                        //Here the preferred currency will be saved in in a string to pass on..
                        string selectedCurrency = CurrencyConverter.ChooseCurrency();

                        //Create and populate an client information info
                        ClientInformation clientInfo = new ClientInformation();
                        Console.Write("Enter first name: ");
                        clientInfo.FirstName = Console.ReadLine();
                        Console.Write("Enter last name: ");
                        clientInfo.LastName = Console.ReadLine();
                        Console.Write("Enter date of birth (dd/mm/yyyy): ");
                        if (DateTime.TryParseExact(Console.ReadLine(), "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dateOfBirth)) {
                            clientInfo.DateOfBirth = dateOfBirth;
                        }
                        else {
                            Console.WriteLine("Invalid date of birth!");
                            break;
                        }

                        //Creating reservation if inputs are valid
                        Console.Write("Enter start date (dd/mm/yyyy): ");
                        if (DateTime.TryParse(Console.ReadLine(), out DateTime startDate)) {

                            Console.Write("Enter end date (dd/mm/yyyy): ");
                            if (DateTime.TryParse(Console.ReadLine(), out DateTime endDate)) {

                                bool isReserved = _reservationManager.MakeReservation(inventory, clientInfo, boatName, startDate, endDate);
                                if (isReserved) {

                                    // Calculate the reservation price in USD
                                    decimal reservationPrice =
                                        (decimal)(endDate - startDate).TotalDays * inventory.GetAvailableBoats().Find(b => b.Name == boatName).PricePerDay;

                                    // Convert and format the reservation price in the selected currency
                                    string formattedPrice = CurrencyConverter.ConvertAndFormatCurrency(reservationPrice, "USD", selectedCurrency, inventory);


                                    Console.WriteLine($"Reservation for '{boatName}' is made from {startDate:d} to {endDate:d}.");
                                    Console.WriteLine($"Total price: {formattedPrice:C}");
                                    Console.WriteLine($"We wish you a great experience {clientInfo.FirstName} {clientInfo.LastName}");
                                }
                                else {
                                    Console.WriteLine("Reservation failed.");
                                }
                            }
                            else {
                                Console.WriteLine("Invalid end date.");
                            }
                        }
                        else {
                            Console.WriteLine("Invalid start date.");
                        }
                        break;

                    case "3":
                        return;

                    default:
                        Console.WriteLine("Invalid choice.");
                        break;
                }
            }
        }
    }
}
