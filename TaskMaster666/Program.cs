using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System;
using TaskMaster666;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Microsoft.VisualBasic;
using System.Runtime.CompilerServices;
using System.ComponentModel;
using System.Diagnostics;

// Create List with Task-objects, fill it be reading from file
string path = @"./tasks.txt"; // The location of the text file where the tasks are saved
List<TaskMaster666.Task> tasks = ReadFromFile(path);

Console.ForegroundColor = ConsoleColor.Blue; Console.Write("Welcome to TaskMaster"); Console.ResetColor();
Console.ForegroundColor = ConsoleColor.Red; Console.Write("666"); Console.ResetColor(); 
Console.ForegroundColor = ConsoleColor.Blue; Console.WriteLine(", your own to-do-list!"); Console.ResetColor();
// Starting the program with welcome text and starting the function UserInterface()
UserInterface();

// Function declarations below
void UserInterface()
{
    Console.ForegroundColor = ConsoleColor.Blue;
    Console.WriteLine(CountTasks());
    Console.ForegroundColor = ConsoleColor.White;
    Console.WriteLine("----------------------------------------------");
    Console.WriteLine("Here are your options:");
    Console.ForegroundColor = ConsoleColor.Magenta; Console.Write("(1) "); Console.ResetColor(); Console.WriteLine("Show Task List");
    Console.ForegroundColor = ConsoleColor.Magenta; Console.Write("(2) "); Console.ResetColor(); Console.WriteLine("Add New Task");
    Console.ForegroundColor = ConsoleColor.Magenta; Console.Write("(3) "); Console.ResetColor(); Console.WriteLine("Edit Task");
    Console.ForegroundColor = ConsoleColor.Magenta; Console.Write("(4) "); Console.ResetColor(); Console.WriteLine("Save and Quit");
    Console.Write("Pick an option and press enter: ");
    string choice1 = Console.ReadLine();

    switch (choice1)
    {
        case "1":
            Console.Clear();
            ShowTaskList();
            UserInterface();
            break;
        case "2":
            // Code
            Console.Clear();
            AddTask();
            break;
        case "3":
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Blue; Console.WriteLine("Edit Task"); Console.ResetColor();
            Console.WriteLine("---------------------------------------------");
            Console.WriteLine("Here are your options:");
            Console.ForegroundColor = ConsoleColor.Magenta; Console.Write("(1) "); Console.ResetColor(); Console.WriteLine("Update task info (title/project/due date)");
            Console.ForegroundColor = ConsoleColor.Magenta; Console.Write("(2) "); Console.ResetColor(); Console.WriteLine("Mark task as done");
            Console.ForegroundColor = ConsoleColor.Magenta; Console.Write("(3) "); Console.ResetColor(); Console.WriteLine("Remove task");
            Console.ForegroundColor = ConsoleColor.Magenta; Console.Write("(4) "); Console.ResetColor(); Console.WriteLine("Go back to main menu");
            Console.Write("Pick an option and press enter: ");
            string choice2 = Console.ReadLine();
            switch (choice2)
            {
                case "1":
                    Console.Clear();
                    Console.WriteLine("Here are you tasks:");
                    ShowTaskList();
                    UpdateTask();
                    break;
                case "2":
                    ShowTaskList();
                    MarkAsDone();
                    break;
                case "3":
                    ShowTaskList();
                    RemoveTask();
                    break;
                case "4":
                    Console.Clear();
                    UserInterface();
                    break;
            }
            break;
        case "4":
            SaveAndQuit();
            break;
        default:
            Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine("\nYou chose an incorrect option, please try again!\n"); Console.ResetColor();
            UserInterface();
            break;
    }
}

string CountTasks()
{
    int sumOfTasks = tasks.Count();
    int uncompletedTasks = 0;

    foreach (TaskMaster666.Task task in tasks)
    {
        if (task.Completed == false)
        {
            uncompletedTasks++;
        }
    }
    int completedTasks = sumOfTasks - uncompletedTasks;
    if (completedTasks == 1 && uncompletedTasks != 1)
    {
        return $"You have {uncompletedTasks} tasks to do, and {completedTasks} task is done....";
    }
    else if (uncompletedTasks == 1 && completedTasks != 1)
    {
        return $"You have {uncompletedTasks} task to do, and {completedTasks} tasks are done...";
    }
    else
    {
        return $"You have {uncompletedTasks} tasks to do, and {completedTasks} tasks are done...";
    }
}

void ShowTaskList()
{
    Console.WriteLine("----------------------------------------------------------------------------------------");
    Console.WriteLine("TITLE".PadRight(35) + "PROJECT".PadRight(20) + "DUE DATE".PadRight(20) + "STATUS");
    DateOnly todaysDate = DateOnly.FromDateTime(DateTime.Now);
    string completed = "";

    // Sorting the list by first project, then by due date
    List<TaskMaster666.Task> sortedTasks = tasks.OrderBy(x => x.Project).ThenBy(x => x.DueDate).ToList(); //https://stackoverflow.com/questions/298725/multiple-order-by-in-linq

    foreach (TaskMaster666.Task task in sortedTasks)
    {
        // Comparing the due date to todays date, and changing colors 
        var dateOnly = DateOnly.FromDateTime(task.DueDate);
        int comparison = dateOnly.CompareTo(todaysDate);// https://stackoverflow.com/questions/683037/how-to-compare-only-date-without-time-in-datetime-types
        if(task.Completed)
        {
            completed = "Completed";
        }
        else
        {
            completed = "Not Completed";
        }

        if (comparison < 0 && !task.Completed)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            completed = "Past Due Date";
        }
        else if (task.Completed == true)
        {
            Console.ForegroundColor = ConsoleColor.Green;
        }
        Console.WriteLine(task.Title.PadRight(35) + task.Project.PadRight(20) + dateOnly.ToString().PadRight(20) + completed);
        Console.ResetColor();
    }
    Console.WriteLine("----------------------------------------------------------------------------------------");
}

void AddTask()
{
    Console.ForegroundColor = ConsoleColor.Blue; Console.WriteLine("ADD NEW TASK"); Console.ResetColor();
    Console.WriteLine("------------");
    Console.Write("What is the task title? ");
    string title = Console.ReadLine();
    Console.Write("\nWhat project does the task belong to? ");
    string project = Console.ReadLine();
    bool isValidDate = false;
    DateTime dueDate;
    Console.Write($"\nWhat is the due date of the task (yyyy-MM-dd)?: ");
    string date = Console.ReadLine();
    isValidDate = DateTime.TryParse(date, out dueDate);
    if (DateTime.TryParse(date, out DateTime validDate))
    {
        isValidDate = true;
    }
    else
    {
        isValidDate = false;
        Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine("Incorrect date, please please input date in format (yyyy-MM-dd)!"); Console.ResetColor();
        AddTask();
    }

    if (title != "" && project != "" && isValidDate)
    {
        tasks.Add(new TaskMaster666.Task(title, dueDate, project)); // adds anonymous object
        Console.ForegroundColor = ConsoleColor.Green; Console.WriteLine("\nTask successfully added!\n"); Console.ResetColor();
        UserInterface();
    }
    else { AddTask(); }
}

List<TaskMaster666.Task> ReadFromFile(string path)
{
    List<TaskMaster666.Task> tasks = new List<TaskMaster666.Task>();
    List<string> temp = File.ReadAllLines(path).ToList();
    foreach (string line in temp)
    {
        string[] items = line.Split(','); // divides each line into array-elements at the commas
        string[] date = items[1].Split("-"); // splits the date-element into its separate elements, year/month/day

        // yyyy-MM-dd
        int year = int.Parse(date[0]);
        int month = int.Parse(date[1]);
        int day = int.Parse(date[2]);

        DateTime dateDue = new DateTime(year, month, day);
        bool isBool = bool.TryParse(items[2], out bool myBool);

        if (isBool)
        {
        tasks.Add(new TaskMaster666.Task(
                items[0], //title
                dateDue,
                myBool,
                items[3]
            )
        
        );
        }
        else
        {
            Console.WriteLine("Invalid Boolean.");
        }
    }
    return tasks;
    // items will have 4 different things in string format, separated by comma
    // In the order: title, dueDate, completed, project which is also the order of the constructor
    //             items[0]  items[1]  items[2]  items[3]

}

void SaveAndQuit()
{
    string path = @"./tasks.txt";
    
    TaskMaster666.Task[] tasksArray = tasks.ToArray(); // Creates an array of tasks (instead of a list)
    string[] tasksString = tasksArray.Select(task => $"{task.Title},{task.DueDate:yyyy-MM-dd},{task.Completed},{task.Project}").ToArray(); //  https://www.techiehook.com/articles/convert-arrays-to-strings-in-csharp
    // this LINQ function takes a task object and creates a string array with title, duedate, compled and project separated by commas.

    File.WriteAllLines(path, tasksString); // the string array is written to file in the path-location
    
    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine("Files are now saved.");
    Console.WriteLine("Thank you for using TaskMaster666!");
    Console.ResetColor();
}

TaskMaster666.Task FindTask()
{
    Console.WriteLine("-----------------------------------------------------------------\nWhat task do you wish to edit? (type exit to return to main menu)\n-----------------------------------------------------------------");
    Console.Write("Task title: ");
    string title = Console.ReadLine().ToLower();
    if (title == "exit")
    {
        Console.Clear();
        UserInterface();
        return null; // In order to break out of the function if exit is types, null is returned
    }
    Console.Write("Task belongs to project: ");
    string project = Console.ReadLine().ToLower();
    if (title == "exit")
    {
        Console.Clear();
        UserInterface();
        return null;
    }
    if (!string.IsNullOrEmpty(title) && !string.IsNullOrEmpty(project))
    {
        var TaskToUpdate = tasks.FirstOrDefault(Task => (Task.Title.ToLower().Contains(title)) && Task.Project.ToLower().Contains(project)); //https://stackoverflow.com/questions/2281083/using-more-than-one-condition-in-linqs-where-method
        if (TaskToUpdate != null)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Task '{title}' belonging to project '{project}' found\n");
            Console.ResetColor();
            return TaskToUpdate;
        }
    }
    Console.WriteLine("Task not found, please try again!");
    return FindTask();
}

void UpdateTask()
{
    var TaskToUpdate = FindTask();
    string title = TaskToUpdate.Title;
    string project = TaskToUpdate.Project;
    DateTime dueDate = TaskToUpdate.DueDate;
    Console.WriteLine("Here are your options for changing the project:");
    Console.ForegroundColor = ConsoleColor.Magenta; Console.Write("(1) "); Console.ResetColor(); Console.WriteLine("Update task title");
    Console.ForegroundColor = ConsoleColor.Magenta; Console.Write("(2) "); Console.ResetColor(); Console.WriteLine("Update task project");
    Console.ForegroundColor = ConsoleColor.Magenta; Console.Write("(3) "); Console.ResetColor(); Console.WriteLine("Update task due date");
    Console.ForegroundColor = ConsoleColor.Magenta; Console.Write("(4) "); Console.ResetColor(); Console.WriteLine("Return to main menu");
    Console.Write("Pick an option and press enter: ");
    string choice2 = Console.ReadLine();
    switch (choice2)
    {
        case "1":
            Console.Write($"Current title is {title}. What do you want to change the title to instead? ");
            TaskToUpdate.Title = Console.ReadLine();
            Console.ForegroundColor = ConsoleColor.Green; Console.WriteLine("Title Successfully changed!");
            UserInterface();
            break;
        
        case "2":
            Console.Write($"Current project is {project}. What do you want to change the project to instead? ");
            TaskToUpdate.Project = Console.ReadLine();
            break;
        
        case "3":
            Console.Write($"Current date is {dueDate}. What do you want to change the date to instead (yyyy-MM-dd)? ");
            string date = Console.ReadLine();
            bool isValidDate = DateTime.TryParse(date, out DateTime dueDateUpdate);
            if (isValidDate)
            {
                
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine("Incorrect date, please please input date in format (yyyy-MM-dd)!"); Console.ResetColor();
                UpdateTask();
            }

            if (title != "" && project != "" && isValidDate)
            {
                tasks.Remove(TaskToUpdate);
                tasks.Add(new TaskMaster666.Task(title, dueDateUpdate, project)); // adds anonymous object
                Console.ForegroundColor = ConsoleColor.Green; Console.WriteLine("\nTask successfully added!\n"); Console.ResetColor();
                UserInterface();
            }
            else 
            { AddTask(); }
            break;
        case "4":
            UserInterface();
            return;
        default:
            Console.WriteLine("Wrong input, please try again!");
            UpdateTask();
            break;
    }
    Console.WriteLine("Task not found, please try again!");
    UpdateTask();
}

void MarkAsDone()
{
    var TaskToUpdate = FindTask();
    string title = TaskToUpdate.Title;
    string project = TaskToUpdate.Project;
    Console.Write("Are you sure you want to mark this project as done (Y/N) ? ");
    string answer = Console.ReadLine();
    if (answer == "y" || answer == "Y")
    {
        TaskToUpdate.Completed = true;
        Console.ForegroundColor = ConsoleColor.Green; Console.WriteLine("Task is marked as completed, well done!");
        UserInterface();
    }

}

void RemoveTask()
{
    var TaskToUpdate = FindTask();
    string title = TaskToUpdate.Title;
    if (TaskToUpdate != null)
    {
        Console.Write($"Are you sure you want to remove the task: {title} (Y/N) ? ");
        string answer = Console.ReadLine();
        if (answer == "y" || answer == "Y")
        {
            tasks.Remove(TaskToUpdate);
            Console.ForegroundColor = ConsoleColor.Green; Console.WriteLine("\nTask successfully removed!");
            UserInterface();
        }
        else
        {
            UserInterface();
        }

    }
}