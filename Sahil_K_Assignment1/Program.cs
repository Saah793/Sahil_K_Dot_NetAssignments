using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleTaskListApp
{
    internal class SimpleTaskListApp
    {

        static List<string> TaskListItems = new List<string>();

        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the Task list");

            
            while (true)
            {

                Console.WriteLine("Here are the options you can select:");
                Console.WriteLine("0 - Exit");
                Console.WriteLine("1 - View Task list");
                Console.WriteLine("2 - Add to Task list");
                Console.WriteLine("3 - Remove from Task list");
                Console.WriteLine("4 - Update Task list");

                // Reading user input
                string userInput = Console.ReadLine();
                int optionId;
                bool optionParseResult = int.TryParse(userInput, out optionId);
                if (!optionParseResult)
                {
                    // input should be an integer
                    Console.WriteLine("Input was not an integer!");
                    continue;
                }

                // Call appropriate method of user input
                switch (optionId)
                {
                    case 0:
                        Exit();
                        break;
                    case 1:
                        ViewTask();
                        break;
                    case 2:
                        AddTask();
                        break;
                    case 3:
                        RemoveTask();
                        break;
                    case 4:
                        UpdateTask();
                        break;
                    default:
                        Console.WriteLine("That option is not valid!");
                        break;
                }
            }
        }

        // Method to add a task to the Task list
        static void AddTask()
        {
            Console.WriteLine("Enter your Task list item:");
            string newTaskListItem = Console.ReadLine();
            TaskListItems.Add(newTaskListItem);
            Console.WriteLine("Your task is Successfully added");
        }

        // Method to remove a task
        static void RemoveTask()
        {   //display all tasks
            Console.WriteLine("Your Task list index are:");
            for (int i = 0; i < TaskListItems.Count; i++)
            {
                string item = TaskListItems[i];
                Console.WriteLine("\t" + i + " - " + item);
            }
            Console.WriteLine("Enter the index of the Task list");

            string userInputForRemoveIndex = Console.ReadLine();
            int removeIndex;
            bool removeIndexParseResult = int.TryParse(userInputForRemoveIndex, out removeIndex);
            if (!removeIndexParseResult)
            {
                // Inform the user if input is not an integer
                Console.WriteLine("Input was not an integer");
                return;
            }

            if (removeIndex < 0 || removeIndex >= TaskListItems.Count)
            {
                // Inform the user if input is out of range
                Console.WriteLine("Input must be non-negative and less than the size of the collection");
                return;
            }

            // Removing the task from the list
            TaskListItems.RemoveAt(removeIndex);
            Console.WriteLine("Your task is Successfully removed");

        }

        // Method to update a task in the Task list
        static void UpdateTask()
        {   //first we will list all the task index so that user can choose
            Console.WriteLine("Your Task list index are:");
            for (int i = 0; i < TaskListItems.Count; i++)
            {
                string item = TaskListItems[i];
                Console.WriteLine("\t" + i + " - " + item);
            }

            Console.WriteLine("Enter the index of the Task list item");
            string userInputForUpdateIndex = Console.ReadLine();
            int updateIndex;
            bool updateIndexParseResult = int.TryParse(userInputForUpdateIndex, out updateIndex);
            if (!updateIndexParseResult)
            {
                // Inform the user if input is not an integer
                Console.WriteLine("Input was not an integer");
                return;
            }

            if (updateIndex < 0 || updateIndex >= TaskListItems.Count)
            {
                // Inform the user if input is out of range
                Console.WriteLine("Input must be non-negative and less than the size of the collection!");
                return;
            }

            Console.WriteLine("Please enter the new description for the Task list item:");
            string newDescription = Console.ReadLine();

            TaskListItems[updateIndex] = newDescription;
            Console.WriteLine("Your task is Successfully updated!!");
        }

        // Method to view all tasks in the Task list
        static void ViewTask()
        {
            Console.WriteLine("Your Task list:");
            for (int i = 0; i < TaskListItems.Count; i++)
            {
                string item = TaskListItems[i];
                Console.WriteLine("\t" + i + " - " + item);
            }
        }

        // Method to exit the program
        static void Exit()
        {
            Environment.Exit(0);
        }



    }
}