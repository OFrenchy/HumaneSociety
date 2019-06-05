using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumaneSociety
{
    class Admin : User
    {
        private static int adoptionFee = 75;
        public static int AdoptionFee
        {
            get => adoptionFee;
            // TODO - for future improvement, figure out how to only allow admin to change adoption fee
            // set => adoptionFee == value;
        }
        public override void LogIn()
        {
            UserInterface.DisplayUserOptions("What is your password?");
            string password = UserInterface.GetUserInput();
            if (password.ToLower() != "poiuyt")
            {
                UserInterface.DisplayUserOptions("Incorrect password please try again or type exit");
            }
            else
            {
                RunUserMenus();
            }
        }

        protected override void RunUserMenus()
        {
            Console.Clear();
            List<string> options = new List<string>() { "Admin log in successful.", "What would you like to do?", "1. Create new employee", "2. Delete employee", "3. Read employee info ", "4. Update employee info" };  //, "(type 1, 2, 3, 4,  create, read, update, or delete)
            UserInterface.DisplayUserOptions(options);
            string input = UserInterface.GetUserInput();
            RunInput(input);
        }

        // =============== Added delegate code below ==================================================
        private delegate void EmployeeFunction();

        protected void RunInput(string input)
        {
            EmployeeFunction employeeFunction;
            if (input == "1" || input.ToLower() == "create")
            {
                employeeFunction = AddEmployee;
            }
            else if (input == "2" || input.ToLower() == "delete")
            {
                employeeFunction = RemoveEmployee;
            }
            else if (input == "3" || input.ToLower() == "read")
            {
                employeeFunction = ReadEmployee;
            }
            else if (input == "4" || input.ToLower() == "update")
            {
                employeeFunction = UpdateEmployee;
            }
            else
            {
                employeeFunction = InvalidInput;
            }
            // Run delegate:
            employeeFunction();
            RunUserMenus();
        }
        // =============== Added delegate code above ==================================================

        private void UpdateEmployee()
        {
            Query.RunEmployeeQueries(null, "display");
            Employee employee = new Employee();
            //employee.id = UserInterface.GetStringData("ID", "the employee's");
            employee.EmployeeId = int.Parse(UserInterface.GetStringData("employee ID to update", "the employee's"));
            employee = Query.RunEmployeeQueries(employee, "read");
            Console.WriteLine("Updating user:\n");

            string temp = UserInterface.GetStringData("last name, or enter to accept", "the employee's");
            if (temp != "") employee.LastName = temp;
            temp = UserInterface.GetStringData("first name, or enter to accept", "the employee's");
            if (temp != "") employee.FirstName = temp;
            temp = UserInterface.GetStringData("employee number, or enter to accept", "the employee's");
            if (temp != "") employee.EmployeeNumber = int.Parse(temp);
            temp = UserInterface.GetStringData("email, or enter to accept", "the employee's");
            if (temp != "") employee.Email = temp;
            temp = UserInterface.GetStringData("username, or enter to accept", "the employee's");
            if (temp != "") employee.UserName = temp;
            temp = UserInterface.GetStringData("password, or enter to accept", "the employee's");
            if (temp != "") employee.Password = temp;

            try
            {
                Query.RunEmployeeQueries(employee, "update");
                UserInterface.DisplayUserOptions("Employee update successful.");
            }
            catch
            {
                Console.Clear();
                UserInterface.DisplayUserOptions("Employee update unsuccessful please try again or type exit;");
                return;
            }
        }

        private void ReadEmployee()
        {
            try
            {
                Employee employee = new Employee();
                employee.EmployeeNumber = int.Parse(UserInterface.GetStringData("employee number", "the employee's"));
                Query.RunEmployeeQueries(employee, "read");
            }
            catch
            {
                Console.Clear();
                UserInterface.DisplayUserOptions("Employee not found please try again or type exit;");
                return;
            }
        }
        //private  void ReadEmployeeByID(int employeeID)
        //{
        //    try
        //    {
        //        Employee employee = new Employee();
        //        employee.EmployeeId = int.Parse(UserInterface.GetStringData("employee ID", "the employee's"));
        //        Query.RunEmployeeQueries(employee, "read");
        //    }
        //    catch
        //    {
        //        Console.Clear();
        //        UserInterface.DisplayUserOptions("Employee not found please try again or type exit;");
        //        return;
        //    }
        //}

        private void RemoveEmployee()
        {
            Employee employee = new Employee();
            employee.LastName = UserInterface.GetStringData("last name", "the employee's");
            employee.EmployeeNumber = int.Parse(UserInterface.GetStringData("employee number", "the employee's"));
            try
            {
                Console.Clear();
                Query.RunEmployeeQueries(employee, "delete");
                UserInterface.DisplayUserOptions("Employee successfully removed");
            }
            catch
            {
                Console.Clear();
                UserInterface.DisplayUserOptions("Employee removal unsuccessful please try again or type exit");
                RemoveEmployee();
            }
        }

        private void AddEmployee()
        {
            Employee employee = new Employee();
            employee.FirstName = UserInterface.GetStringData("first name", "the employee's");
            employee.LastName = UserInterface.GetStringData("last name", "the employee's");
            employee.EmployeeNumber = int.Parse(UserInterface.GetStringData("employee number", "the employee's"));
            employee.Email = UserInterface.GetStringData("email", "the employee's");
            try
            {
                Query.RunEmployeeQueries(employee, "create");
                UserInterface.DisplayUserOptions("Employee addition successful.");
            }
            catch
            {
                Console.Clear();
                UserInterface.DisplayUserOptions("Employee addition unsuccessful please try again or type exit;");
                return;
            }
        }
        private void InvalidInput()
        {
            UserInterface.DisplayUserOptions("Input not recognized please try again or type exit");
            return;
        }
    }
}
