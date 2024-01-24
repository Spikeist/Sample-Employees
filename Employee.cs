public class Employee
{
    protected string name;
    protected int employeeID;
    protected string title;
    protected string startDate;

    public Employee(string Name, int EmpID, string Title, string Start)
    {
        name = Name;
        employeeID = EmpID;
        title = Title;
        startDate = Start;
    }

    public string getName()
    {
        return name;
    }

    public int getEmployeeID()
    {
        return employeeID;
    }

    public string getTitle()
    {
        return title;
    }

    public string getStartDate()
    {
        return startDate;
    }

    public void setName(string Name)
    {
        name = Name;
    }

    public void setEmployeeID(int EmpID)
    {
        employeeID = EmpID;
    }

    public void setTitle(string Title)
    {
        title = Title;
    }

    public void setStartDate(string Start)
    {
        startDate = Start;
    }

    public string printDetails()
    {
        return name + ", " + title + "\nEmployee ID: " + employeeID + "\nStart Date: " + startDate + "\n";
    }  
}