using System;
using System.ComponentModel;
using System.IO;
using System.Linq.Expressions;

List<Employee> employeeList = new();

string[] text = File.ReadAllLines("employees.txt");
foreach(string t in text)
{
    employeeList.Add(convertStringToEmployee(t));
}

int choice = 0;
while(choice != 6)
{
    displayOptions();
    switch(getChoice())
    {
        case 1:
            Console.WriteLine("\n********************\nCreate New Employee\n********************\n");
            employee();
            break;
        case 2:
            Console.WriteLine("\n*******************\nView All Employees\n*******************\n");
            displayEmployees();
            break;
        case 3:
            Console.WriteLine("\n*********************\nSearch Employees by:\n1. Employee ID\n2. Name\n*********************\n");
            switch(getSearchChoice())
            {
                case 1:
                    searchByID();
                    break;
                case 2:
                    searchByName();
                    break;
                default:
                    Console.WriteLine("\nError.");
                    break;
            }
            break;
        case 4:
            Console.WriteLine("\n*******************\nDelete Employee\n*******************\n");
            delete();
            break;
        case 5:
            Console.WriteLine("\n****************\nUpdate Employee\n****************\n");
            update();
            break;
        case 6:
            Console.WriteLine("\nEmployees Saved. Goodbye!\n");
            break;
        default:
            Console.WriteLine("\nError. Goodbye!\n");
            break;
    }
}

using (StreamWriter file = new StreamWriter("employees.txt", false)) {
    foreach (Employee eL in employeeList) {
        file.WriteLine(convertEmployeeToString(eL));
    }
}


int getChoice()
{
    bool isValid = false;
    while(!isValid) {
        Console.Write("Enter your choice: ");
        var input = Console.ReadLine() ?? "";
        int.TryParse(input, out choice);
        if(choice >= 1 && choice <= 6) {
            isValid = true;
            return choice;
        }
    }
    return choice;
}

void displayOptions()
{
    Console.WriteLine("\n***********************\n1. Create New Employee\n2. View All Employees\n3. Search Employees\n4. Delete Employee\n5. Update Employee\n6. Quit\n***********************\n");
}

void employee()
{
    Console.Write("What is the employee's name? ");
    string createName = Console.ReadLine() ?? "";
    var random = new Random();
    int createID = random.Next(1000, 10000);
    Console.Write("What is the employee's title? ");
    string createTitle = Console.ReadLine() ?? "";
    string createStartDate = "";
    bool isValid = false;
    DateTime dateTime;
    while(!isValid)
    {
        Console.Write("What is the employee's start date? ");
        createStartDate = Console.ReadLine() ?? "";
        isValid = DateTime.TryParse(createStartDate, out dateTime);
    }
    Employee employee = new Employee(createName, createID, createTitle, createStartDate);    
    employeeList.Add(employee);
    Console.WriteLine("\nNew Employee Added - Employee ID: " + employeeList[employeeList.Count - 1].getEmployeeID());
}

void displayEmployees()
{
    for(int i = 0; i < employeeList.Count; i++)
    {
        Console.WriteLine(employeeList[i].printDetails());
    }
}

int getSearchChoice()
{
    bool isValid = false;
    while(!isValid) {
        Console.Write("Enter your choice: ");
        var input = Console.ReadLine() ?? "";
        int.TryParse(input, out choice);
        if(choice == 1 || choice == 2) {
            isValid = true;
            return choice;
        }
    }
    return choice;
}

void searchByID()
{
    int employeeID;
    Console.Write("\nEnter the Employee ID: ");
    var input = Console.ReadLine() ?? "";
    int.TryParse(input, out employeeID);
    Console.WriteLine("\n*********\nResults:\n*********\n");
    for(int i = 0; i < employeeList.Count; i++)
    {
        if(employeeID == employeeList[i].getEmployeeID())
        {
            Console.WriteLine(employeeList[i].printDetails());
            return;
        }
    }
    Console.WriteLine("No employees found.");
}

void searchByName()
{
    bool isValid = false;
    Console.Write("\nEnter a Name: ");
    string name = Console.ReadLine() ?? "";
    Console.WriteLine("\n*********\nResults:\n*********\n");   
    for(int i = 0; i < employeeList.Count; i++)
    {
        if(employeeList[i].getName().ToLower().Contains(name.ToLower()))
        {
            Console.WriteLine(employeeList[i].printDetails());
            isValid = true;
        }
    }
    if(!isValid)
    {
        Console.WriteLine("No employees found.");
    }
}

void delete()
{
    int employeeID;
    Console.Write("Enter employee ID to delete: ");
    var input = Console.ReadLine() ?? "";
    int.TryParse(input, out employeeID);
    for(int i = 0; i < employeeList.Count; i++)
    {
        if(employeeID == employeeList[i].getEmployeeID())
        {
            employeeList.Remove(employeeList[i]);
            Console.WriteLine("\nEmployee ID " + employeeID + " has been deleted.");
            return;
        }
    }
    Console.Write("\nEmployee ID " + input + " not found.\n");
}

void update()
{
    int employeeID;
    Console.Write("Enter employee ID to update: ");
    var input = Console.ReadLine() ?? "";
    int.TryParse(input, out employeeID);
    int idx = -1;
    for(int i = 0; i < employeeList.Count; i++)
    {
        if(employeeID == employeeList[i].getEmployeeID())
        {
            idx = i;
        }
    }
    if(idx == -1)
    {
        Console.Write("\nEmployee ID " + input + " not found.\n");
        return;
    }
    Console.WriteLine("\n******************************\n1. Update Employee Name\n2. Update Employee Title\n3. Update Employee Start Date\n4. Go Back\n******************************\n");
    switch(getUpdateChoice())
    {
        case 1:
            updateName(idx);
            break;
        case 2:
            updateTitle(idx);
            break;
        case 3:
            updateStartDate(idx);
            break;
        case 4:
            break;
        default:
            Console.WriteLine("\nError. Goodbye!\n");
            break;
    }
}

int getUpdateChoice()
{
    bool isValid = false;
    while(!isValid) {
        Console.Write("Enter your choice: ");
        var input = Console.ReadLine() ?? "";
        int.TryParse(input, out choice);
        if(choice >= 1 && choice <= 4)
        {
            isValid = true;
            return choice;
        }
    }
    return choice;
}

void updateName(int idx)
{
    Console.Write("\nEnter new name: ");
    string name = Console.ReadLine() ?? "";
    employeeList[idx].setName(name);
}

void updateTitle(int idx)
{
    Console.Write("\nEnter new title: ");
    string title = Console.ReadLine() ?? "";
    employeeList[idx].setTitle(title);
}

void updateStartDate(int idx)
{
    string startDate = "";
    bool isValid = false;
    DateTime dateTime;
    while(!isValid)
    {
        Console.Write("\nEnter new start date: ");
        startDate = Console.ReadLine() ?? "";
        isValid = DateTime.TryParse(startDate, out dateTime);
    }
    employeeList[idx].setStartDate(startDate);
}

string convertEmployeeToString(Employee employee)
{
    string result = employee.getName() + ", " + employee.getEmployeeID() + ", " + employee.getTitle() + ", " + employee.getStartDate();
    return result;
}

Employee convertStringToEmployee(string input)
{
    string[] result = input.Split(", ");
    Employee employee = new Employee(result[0], int.Parse(result[1]), result[2], result[3]);
    return employee;
}