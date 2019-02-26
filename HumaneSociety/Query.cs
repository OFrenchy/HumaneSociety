using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumaneSociety
{
    public static class Query
    {
        internal static Employee RunEmployeeQueries(Employee employee, string action) 
        {
            HumaneSocietyDataContext db = new HumaneSocietyDataContext();
            switch (action)
            {
                case "create":
                    db.Employees.InsertOnSubmit(employee);
                    db.SubmitChanges();
                    return null;
                    //break;
                case "read":
                    if (employee.EmployeeNumber != null)
                    {
                        employee = db.Employees.Where(e => e.EmployeeNumber == employee.EmployeeNumber).First();
                    }
                    if (employee.EmployeeId != 0)
                    {
                        employee = db.Employees.Where(e => e.EmployeeId == employee.EmployeeId).First();
                    }
                    UserInterface.DisplayEmployeeInfo(employee);
                    Console.WriteLine("\nPress enter to continue:");
                    Console.ReadLine();
                    return employee;
                    //break;
                case "update":
                    Employee thisEmployee = db.Employees.Where(e => e.EmployeeId == employee.EmployeeId).Single();
                    thisEmployee.LastName = employee.LastName;
                    thisEmployee.FirstName = employee.FirstName;
                    thisEmployee.EmployeeId = employee.EmployeeId;
                    thisEmployee.UserName = employee.UserName;
                    thisEmployee.Password = employee.Password;
                    thisEmployee.Email = employee.Email;
                    db.SubmitChanges();
                    Console.Clear();
                    Console.WriteLine("Updated:");
                    UserInterface.DisplayEmployeeInfo(employee);
                    Console.WriteLine("\nPress enter to continue:");
                    Console.ReadLine();
                    return null;
                    //break;
                case "display": // display 
                    Console.Clear();
                    UserInterface.DisplayEmployees(db.Employees.OrderBy(e => e.LastName).ThenBy(e => e.FirstName));// DisplayEmployees
                    return null;
                    //break;
                case "delete":
                    employee = db.Employees.Where(e => e.LastName == employee.LastName && e.EmployeeNumber == employee.EmployeeNumber).First();
                    db.Employees.DeleteOnSubmit(employee);
                    db.SubmitChanges();
                    return null;
                    //break;
                default:
                    return null;
            }
        }

        internal static  Animal GetAnimalByID(int ID)
        {
            HumaneSocietyDataContext db = new HumaneSocietyDataContext();
            return db.Animals.Where(a => a.AnimalId == ID).First();
        }
        internal static List<AnimalShot> GetShots(Animal animal)
        {
            HumaneSocietyDataContext db = new HumaneSocietyDataContext();
            List<AnimalShot> animalshots = db.AnimalShots.Where(a => a.AnimalId == animal.AnimalId).ToList() ;
            return animalshots;
        }

        internal static List<USState> GetStates()
        {
            HumaneSocietyDataContext db = new HumaneSocietyDataContext();

            List<USState> allStates = db.USStates.ToList();

            return allStates;
        }
        internal static Room GetRoom(int AnimalId)
        {
            // TODO - fill this in
            HumaneSocietyDataContext db = new HumaneSocietyDataContext();
            Room room;
            try
            {
                room = db.Rooms.Where(r => r.AnimalId == AnimalId).Single();
            }
            catch (Exception)
            {
                // TODO - Do we need a try/catch?
                throw;
            }
            return room;
        }
        internal static int GetCategoryId()
        {
            HumaneSocietyDataContext db = new HumaneSocietyDataContext();
            Console.Clear();
            //foreach (var category in db.Categories) Console.WriteLine(category.CategoryId + " " + category.Name);
            List<string> categories = new List<string>();
            foreach (var category in db.Categories) categories.Add($"{category.CategoryId}  {category.Name}");
            UserInterface.DisplayUserOptions(categories);
            UserInterface.DisplayUserOptions("Enter the number of the category you want: ");
            return UserInterface.GetIntegerData();
        }
        internal static int GetDietPlanId()
        {
            //TODO
            HumaneSocietyDataContext db = new HumaneSocietyDataContext();
            Console.Clear();

            //foreach (var category in db.Categories) Console.WriteLine(category.CategoryId + " " + category.Name);
            List<string> dietPlans = new List<string>();
            foreach (var dietPlan in db.DietPlans) dietPlans.Add($"{dietPlan.DietPlanId} {dietPlan.Name} {dietPlan.FoodType}, {dietPlan.FoodAmountInCups} cup(s)");

            UserInterface.DisplayUserOptions(dietPlans);
            UserInterface.DisplayUserOptions("Enter the number of the diet plan you want: ");
            return UserInterface.GetIntegerData();

            
        }
        internal static void RemoveAnimal(Animal animal)
        {
            //TODO
        }
        internal static void Adopt(Animal animal, Client client)
        {
            //TODO
        }
        internal static bool Vacancy()
        {
            HumaneSocietyDataContext db = new HumaneSocietyDataContext();
            var room = db.Rooms.Where(r => r.AnimalId == null).First();
            if (room == null) return false; return true;
        }

        internal static void AddAnimal(Animal animal)
        {
            HumaneSocietyDataContext db = new HumaneSocietyDataContext();
            var room = db.Rooms.Where(r => r.AnimalId == null).First();
            db.Animals.InsertOnSubmit(animal);
            db.SubmitChanges();
            room.AnimalId = animal.AnimalId;
            db.SubmitChanges();
        }
        internal static List<Animal> SearchForAnimalByMulitpleTraits()
        {
            //TODO
            HumaneSocietyDataContext db = new HumaneSocietyDataContext();

            // TODO - add filtering?
            //List<Animal> animalsfound = new List<Animal>();
            Dictionary<int, string> searchCriteria;
            searchCriteria = UserInterface.GetAnimalCriteria();


            List<Animal> animalsfound = db.Animals.ToList();

            return animalsfound;
        }
        internal static List<Adoption> GetPendingAdoptions()
        {
            HumaneSocietyDataContext db = new HumaneSocietyDataContext();
            // TODO - status pending??  
            List<Adoption> pendingAdoptions = db.Adoptions.Where(a => a.ApprovalStatus == "pending").ToList();
            return pendingAdoptions;
        }

        internal static void UpdateAdoption(bool approve, Adoption adoption)
        {
            HumaneSocietyDataContext db = new HumaneSocietyDataContext();
            //ToDO
            adoption.ApprovalStatus = approve ? "approved" : "denied";
            var dbAdoption = db.Adoptions.Where(a => a.AdoptionId == adoption.AdoptionId);

            db.SubmitChanges();
        }
        internal static void UpdateShot(string blahblah, Animal animal)
        {
            //TODO 
        }
        internal static void EnterAnimalUpdate(Animal animal, Dictionary<int, string> updates)
        {
            //TODO
        }

        internal static Client GetClient(string userName, string password)
        {
            //TODO
            HumaneSocietyDataContext db = new HumaneSocietyDataContext();

            Client client = db.Clients.Where(c => c.UserName == userName && c.Password == password).Single();

            return client;
        }

        internal static List<Client> GetClients()
        {
            HumaneSocietyDataContext  db = new HumaneSocietyDataContext();

            List<Client> allClients = db.Clients.ToList();

            return allClients;
        }

        internal static void AddNewClient(string firstName, string lastName, string username, string password, string email, string streetAddress, int zipCode, int stateId)
        {
            HumaneSocietyDataContext  db = new HumaneSocietyDataContext();

            Client newClient = new Client();

            newClient.FirstName = firstName;
            newClient.LastName = lastName;
            newClient.UserName = username;
            newClient.Password = password;
            newClient.Email = email;

            Address addressFromDb = db.Addresses.Where(a => a.AddressLine1 == streetAddress && a.Zipcode == zipCode && a.USStateId == stateId).FirstOrDefault();

            // if the address isn't found in the Db, create and insert it
            if (addressFromDb == null)
            {
                Address newAddress = new Address();
                newAddress.AddressLine1 = streetAddress;
                newAddress.AddressLine2 = null;
                newAddress.Zipcode = zipCode;
                newAddress.USStateId = stateId;

                db.Addresses.InsertOnSubmit(newAddress);
                db.SubmitChanges();

                addressFromDb = newAddress;
            }

            // attach AddressId to clientFromDb.AddressId
            newClient.AddressId = addressFromDb.AddressId;

            db.Clients.InsertOnSubmit(newClient);

            db.SubmitChanges();
        }

        internal static void UpdateClient(Client clientWithUpdates)
        {
            HumaneSocietyDataContext  db = new HumaneSocietyDataContext();

            // find corresponding Client from Db
            Client clientFromDb = db.Clients.Where(c => c.ClientId == clientWithUpdates.ClientId).Single();

            // update clientFromDb information with the values on clientWithUpdates (aside from address)
            clientFromDb.FirstName = clientWithUpdates.FirstName;
            clientFromDb.LastName = clientWithUpdates.LastName;
            clientFromDb.UserName = clientWithUpdates.UserName;
            clientFromDb.Password = clientWithUpdates.Password;
            clientFromDb.Email = clientWithUpdates.Email;

            // get address object from clientWithUpdates
            Address clientAddress = clientWithUpdates.Address;

            // look for existing Address in Db (null will be returned if the address isn't already in the Db
            Address updatedAddress = db.Addresses.Where(a => a.AddressLine1 == clientAddress.AddressLine1 && a.USStateId == clientAddress.USStateId && a.Zipcode == clientAddress.Zipcode).FirstOrDefault();

            // if the address isn't found in the Db, create and insert it
            if(updatedAddress == null)
            {
                Address newAddress = new Address();
                newAddress.AddressLine1 = clientAddress.AddressLine1;
                newAddress.AddressLine2 = null;
                newAddress.Zipcode = clientAddress.Zipcode;
                newAddress.USStateId = clientAddress.USStateId;

                db.Addresses.InsertOnSubmit(newAddress);
                db.SubmitChanges();

                updatedAddress = newAddress;
            }

            // attach AddressId to clientFromDb.AddressId
            clientFromDb.AddressId = updatedAddress.AddressId;
            
            // submit changes
            db.SubmitChanges();
        }

        internal static Employee RetrieveEmployeeUser(string email, int employeeNumber)
        {
            HumaneSocietyDataContext  db = new HumaneSocietyDataContext();
            Employee employeeFromDb = db.Employees.Where(e => e.Email == email && e.EmployeeNumber == employeeNumber).FirstOrDefault();

            if (employeeFromDb == null)
            {
                throw new NullReferenceException();
            }
            else
            {
                return employeeFromDb;
            }
        }

        internal static Employee EmployeeLogin(string userName, string password)
        {
            HumaneSocietyDataContext  db = new HumaneSocietyDataContext();
            Employee employeeFromDb = db.Employees.Where(e => e.UserName == userName && e.Password == password).FirstOrDefault();
            if (employeeFromDb == null)
            {
                throw new NullReferenceException();
            }
            else
            {
                return employeeFromDb;
            }
        }

        internal static bool CheckEmployeeUserNameExist(string userName)
        {
            HumaneSocietyDataContext  db = new HumaneSocietyDataContext();
            Employee employeeWithUserName = db.Employees.Where(e => e.UserName == userName).FirstOrDefault();
            return employeeWithUserName != null;
        }

        internal static void AddUsernameAndPassword(Employee employee)
        {
            HumaneSocietyDataContext  db = new HumaneSocietyDataContext();
            Employee employeeFromDb = db.Employees.Where(e => e.EmployeeId == employee.EmployeeId).FirstOrDefault();
            employeeFromDb.UserName = employee.UserName;
            employeeFromDb.Password = employee.Password;
            db.SubmitChanges();
        }
    }
}