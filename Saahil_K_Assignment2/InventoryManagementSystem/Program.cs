using System;
using System.Collections.Generic;

namespace InventoryManagementSystem
{
    // Item class definition
    public class Item
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }

        public Item(int id, string name, double price, int quantity)
        {
            ID = id;
            Name = name;
            Price = price;
            Quantity = quantity;
        }

        // Display item information
        public void DisplayItem()
        {
            Console.WriteLine($"ID: {ID}, Name: {Name}, Price: {Price}, Quantity: {Quantity}");
        }
    }


    public class Inventory
    {

        private List<Item> items = new List<Item>();


        public void AddItem(Item item)
        {
            items.Add(item);
            
        }


        public void DisplayAllItems()
        {
            foreach (var item in items)
            {
                item.DisplayItem();
            }
        }


        public Item FindItemByID(int id)
        {
            return items.Find(item => item.ID == id);
        }


        public void UpdateItem(int id, string name, double price, int quantity)
        {
            Item item = FindItemByID(id);
            if (item != null)
            {
                item.Name = name;
                item.Price = price;
                item.Quantity = quantity;
                Console.WriteLine("Item updated successfully.");
            }
        }


        public void DeleteItem(int id)
        {
            Item item = FindItemByID(id);
            if (item != null)
            {
                items.Remove(item);
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Inventory inventory = new Inventory();

            do
            {
                Console.WriteLine("1. Add Item");
                Console.WriteLine("2. Display All Items");
                Console.WriteLine("3. Find Item by ID");
                Console.WriteLine("4. Update Item");
                Console.WriteLine("5. Delete Item");
                Console.Write("Choose an option: ");
                int choice = int.ToInt32(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        // Add Item
                        Console.Write("Enter ID: ");
                        int id = int.Parse(Console.ReadLine());
                        Console.Write("Enter Name: ");
                        string name = Console.ReadLine();
                        Console.Write("Enter Price: ");
                        double price = double.Parse(Console.ReadLine());
                        Console.Write("Enter Quantity: ");
                        int quantity = int.Parse(Console.ReadLine());

                        Item newItem = new Item(id, name, price, quantity);
                        inventory.AddItem(newItem);
                        break;

                    case 2:
                        // Display All Items
                        inventory.DisplayAllItems();
                        break;

                    case 3:
                        // Find Item by ID
                        Console.Write("Enter ID: ");
                        int findId = int.Parse(Console.ReadLine());
                        Item foundItem = inventory.FindItemByID(findId);
                        if (foundItem != null)
                        {
                            foundItem.DisplayItem();
                        }
                        else
                        {
                            Console.WriteLine("Item not found.");
                        }
                        break;

                    case 4:
                        // Update Item
                        Console.Write("Enter ID: ");
                        int updateId = int.Parse(Console.ReadLine());
                        Console.Write("Enter New Name: ");
                        string newName = Console.ReadLine();
                        Console.Write("Enter New Price: ");
                        double newPrice = double.Parse(Console.ReadLine());
                        Console.Write("Enter New Quantity: ");
                        int newQuantity = int.Parse(Console.ReadLine());
                        inventory.UpdateItem(updateId, newName, newPrice, newQuantity);
                        break;

                    case 5:
                        // Delete Item
                        Console.Write("Enter ID: ");
                        int deleteId = int.Parse(Console.ReadLine());
                        inventory.DeleteItem(deleteId);
                        break;

                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }
    }
}
