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

        static string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "lhms_850002581.txt");
        static string backupPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "lhms_850002581_backup.txt");
        // Main function
        static void Main(string[] args)
        {
            string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);//Get the path to the user's Documents folder
            filePath = Path.Combine(folderPath, "HotelManagement.txt");// Combine the folder path with the file name

            string ans; // Variable to store user response

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
                catch (FormatException ex)
                {
                    Console.WriteLine("Invalid input. Please enter a valid number.");
                    Console.WriteLine(ex.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An error occurred: " + ex.Message);
                }

                Console.Write("\nWould You Like To Continue? (yes/no): ");//
                ans = Console.ReadLine()?.Trim().ToLower();

            } while (ans == "yes");
        }

        static void AddRooms()
        {
            try
            {
                Console.Write("Please Enter the Total Number of Rooms in the Hotel: ");// Prompt user for total number of rooms
                int totalRooms = Convert.ToInt32(Console.ReadLine());// Get the total number of rooms from user

                for (int i = 0; i < totalRooms; i++)
                {
                    Console.Write("Please enter the Room Number: ");// Prompt user for room number
                    int roomNo = Convert.ToInt32(Console.ReadLine());// Get the room number from user

                    Room room = new Room { RoomNo = roomNo, IsAllocated = false };// Create a new room object
                    listOfRooms.Add(room);// Add the room to the list of rooms
                }

                Console.WriteLine("Rooms Added Successfully!");
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid input. Please enter numeric values.");//for invalid input
            }
        }
        static void DisplayRooms()
        {
            if (listOfRooms.Count == 0)
            {
                Console.WriteLine("No rooms found.");//for empty list
                return;
            }

            Console.WriteLine("\nList of Rooms:");//for displaying the list of rooms
            foreach (var room in listOfRooms)//for each room in the list
            {
                Console.WriteLine($"Room No: {room.RoomNo}, Allocated: {room.IsAllocated}");//for each room, display its number and allocation status
            }
        }
        static void AllocateRoom()
        {
            try
            {
                Console.Write("Enter Room No to Allocate: ");// Prompt user for room number
                int roomNo = Convert.ToInt32(Console.ReadLine());// Get the room number from user

                Room room = listOfRooms.Find(r => r.RoomNo == roomNo && !r.IsAllocated);// Find the room that is not allocated
                if (room == null)
                    throw new InvalidOperationException("Room not available for allocation.");//for room not available

                Console.Write("Enter Customer Number: ");// Prompt user for customer numberq
                int custNo = Convert.ToInt32(Console.ReadLine());// Get the customer number from user
                Console.Write("Enter Customer Name: ");// Prompt user for customer name
                string name = Console.ReadLine();// Get the customer name from user

                Customer customer = new Customer { CustomerNo = custNo, CustomerName = name };// Create a new customer object
                room.IsAllocated = true;// Mark the room as allocated

                listOfRoomAllocations.Add(new RoomsAllocation
                {
                    AllocatedRoomNo = roomNo,
                    AllocatedCustomer = customer
                });// Add the room allocation to the list

                Console.WriteLine("Room Allocated Successfully!");//for successful allocation
            }
            catch (FormatException)
            {
                Console.WriteLine("Please enter numeric values.");//for invalid input
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine(ex.Message);//for room not available
            }
        }
        static void DeallocateRoom()
        {
            try
            {
                Console.Write("Enter Room No to De-Allocate: ");// Prompt user for room number
                int roomNo = Convert.ToInt32(Console.ReadLine());// Get the room number from user

                var allocation = listOfRoomAllocations.Find(r => r.AllocatedRoomNo == roomNo);// Find the room allocation
                if (allocation == null)
                    throw new InvalidOperationException("Room not currently allocated.");//for room not allocated

                listOfRoomAllocations.Remove(allocation);// Remove the allocation from the list

                Room room = listOfRooms.Find(r => r.RoomNo == roomNo);// Find the room
                if (room != null)
                    room.IsAllocated = false;// Mark the room as not allocated

                Console.WriteLine("Room De-Allocated Successfully!");//for successful deallocation
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine(ex.Message);//for room not allocated
            }
            catch (FormatException)
            {
                Console.WriteLine("Please enter numeric values.");//for invalid input
            }
        }
        static void DisplayRoomAllocations()
        {
            if (listOfRoomAllocations.Count == 0)
            {
                Console.WriteLine("No allocations found.");//for empty list
                return;
            }

            Console.WriteLine("\nRoom Allocations:");//for displaying the list of allocations
            foreach (var alloc in listOfRoomAllocations)
            {
                Console.WriteLine($"Room No: {alloc.AllocatedRoomNo} -> Customer: {alloc.AllocatedCustomer.CustomerNo}, {alloc.AllocatedCustomer.CustomerName}");
                //for each allocation, display the room number and customer details
            }
        }
        static void SaveToFile()
        {
            string filename = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "lhms_850002581.txt");// Get the path to the file
            StreamWriter sw = null;// Initialize StreamWriter

            try
            {
                sw = new StreamWriter(filename, true);
                sw.WriteLine($"--- Room Allocations @ {DateTime.Now} ---");//for file header
                foreach (var alloc in listOfRoomAllocations)
                {
                    sw.WriteLine($"Room No: {alloc.AllocatedRoomNo}, Customer: {alloc.AllocatedCustomer.CustomerNo}, {alloc.AllocatedCustomer.CustomerName}");
                    //for each allocation, write the room number and customer details to the file
                }

                Console.WriteLine("Allocations saved to file successfully!");//for successful save
            }
            catch (UnauthorizedAccessException)
            {
                Console.WriteLine("Access denied. File is read-only.");//for file access denied
            }
            finally
            {
                if (sw != null)
                {
                    sw.Close();// Close the StreamWriter
                }
            }
        }
        static void LoadFromFile()
        {
            string filename = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "lhms_850002581.txt");// Get the path to the file
            StreamReader sr = null;// Initialize StreamReader

            try
            {
                if (!File.Exists(filename))
                    throw new FileNotFoundException("File not found.");//for file not found

                sr = new StreamReader(filename);
                Console.WriteLine("\nFile Content:\n" + sr.ReadToEnd());// Read the content of the file
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine(ex.Message);//for file not found
            }
            finally
            {
                if (sr != null)
                {
                    sr.Close();// Close the StreamReader
                }
            }
        }
        static void BackupFile()
        {
            string mainFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "lhms_850002581.txt");// Get the path to the main file
            string backupFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "lhms_850002581_backup.txt");// Get the path to the backup file

            try
            {
                if (!File.Exists(mainFile))
                    throw new FileNotFoundException("Main file not found.");//for file not found

                string content = File.ReadAllText(mainFile);// Read the content of the main file
                File.AppendAllText(backupFile, content);// Append the content to the backup file
                File.WriteAllText(mainFile, string.Empty);// Clear the main file

                Console.WriteLine("Backup created and main file cleared.");//for successful backup
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");//for any error
            }
        }

    }

}
