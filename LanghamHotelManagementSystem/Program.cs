/* 
 * Project Name:LANGHAM Hotel Management System
 * Author Name:Manpreet Kaur
 * Date:10/04/2025
 * Application Purpose:To manage hotel room allocation, customer records,file storage,and backups with exception handling
 * 
 */

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace LanghamHotelManagementSystem
{
    // Custom Class - Room
       // Custom Class - Room
    public class Room
    {
        public int RoomNo { get; set; }  // Room Number            
        public bool IsAllocated { get; set; } = false; // Room Allocation Status 
    }

    // Custom Class - Customer
    public class Customer
    {
        public int CustomerNo { get; set; } // Customer number
        public string CustomerName { get; set; } // Customer name

    }

    // Custom Class - RoomAllocation
    public class RoomsAllocation
    {
        public int AllocatedRoomNo { get; set; } // Allocated room number
        public Customer AllocatedCustomer { get; set; } // Customer details


    }

    // Custom Main Class - Program
    class Program
    {
        // Variables declaration and initialization
         public static List<Room> listOfRooms = new List<Room>();
        public static List<RoomsAllocation> listOfRoomAllocations = new List<RoomsAllocation>();

        public static string filePath;
        // Main function
        static void Main(string[] args)
        {
            string folderPath =
            Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            filePath = Path.Combine(folderPath, "HotelManagement.txt");
            char ans;
            do
            {
                Console.Clear();
                Console.WriteLine("***********************************************************************************");
                Console.WriteLine("                 LANGHAM HOTEL MANAGEMENT SYSTEM                  ");
                Console.WriteLine("                            MENU                                 ");
                Console.WriteLine("***********************************************************************************");
                Console.WriteLine("1. Add Rooms");
                Console.WriteLine("2. Display Rooms");
                Console.WriteLine("3. Allocate Rooms");
                Console.WriteLine("4. De-Allocate Rooms");
                Console.WriteLine("5. Display Room Allocation Details");
                Console.WriteLine("6. Billing");
                Console.WriteLine("7. Save the Room Allocations To a File");
                Console.WriteLine("8. Show the Room Allocations From a File");
                Console.WriteLine("9. Exit");
                Console.WriteLine("0. Backup Allocations File");
                Console.WriteLine("***********************************************************************************");

                try

                {
                    Console.Write("Enter Your Choice Number Here:");
                    int choice = Convert.ToInt32(Console.ReadLine());


                    switch (choice)//Switch case to handle user choice
                    {

                        case 1:
                            AddRooms();// Add rooms to the list
                            break;
                        case 2:
                            DisplayRooms();// Display the list of rooms
                            break;
                        case 3:
                            AllocateRoom();// Allocate a room to a customer
                            break;
                        case 4:
                            DeallocateRoom();// Deallocate a room from a customer
                            break;
                        case 5:
                            DisplayRoomAllocations();// Display room allocation details
                            break;
                        case 6:
                            Console.WriteLine("Billing Feature is Under Construction and will be added soon…!!!");
                            // Billing feature is not implemented yet
                            break;

                        case 7:
                            SaveToFile();// Save room allocation details to a file
                            break;
                        case 8:
                            LoadFromFile();// Show room allocation details from a file
                            break;
                        case 9:
                            // Exit Application
                            Console.WriteLine("Exiting the Application. Thank you for using the Hotel Management System.");
                            // Exit the loop
                            break;
                        case 0:
                            BackupFile();// Backup the room allocation file
                            break;
                        default:
                            Console.WriteLine("Invalid choice. Please enter a number between 1 and 9.");
                            break;
                            break;
                    }
                }

                Console.Write("\nWould You Like To Continue(Y/N):");
                ans = Convert.ToChar(Console.ReadLine());
            } while (ans == 'y' || ans == 'Y');
        }
    }
}
