﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumaneSociety
{
    public static class Query
    {        
        static HumaneSocietyDataContext db;

        static Query()
        {
            db = new HumaneSocietyDataContext();
        }

        internal static List<USState> GetStates()
        {
            List<USState> allStates = db.USStates.ToList();       

            return allStates;
        }
            
        internal static Client GetClient(string userName, string password)
        {
            Client client = db.Clients.Where(c => c.UserName == userName && c.Password == password).Single();

            return client;
        }

        internal static List<Client> GetClients()
        {
            List<Client> allClients = db.Clients.ToList();

            return allClients;
        }

        internal static void AddNewClient(string firstName, string lastName, string username, string password, string email, string streetAddress, int zipCode, int stateId)
        {
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
                newAddress.City = null;
                newAddress.USStateId = stateId;
                newAddress.Zipcode = zipCode;                

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
                newAddress.City = null;
                newAddress.USStateId = clientAddress.USStateId;
                newAddress.Zipcode = clientAddress.Zipcode;                

                db.Addresses.InsertOnSubmit(newAddress);
                db.SubmitChanges();

                updatedAddress = newAddress;
            }

            // attach AddressId to clientFromDb.AddressId
            clientFromDb.AddressId = updatedAddress.AddressId;
            
            // submit changes
            db.SubmitChanges();
        }
        
        internal static void AddUsernameAndPassword(Employee employee)
        {
            Employee employeeFromDb = db.Employees.Where(e => e.EmployeeId == employee.EmployeeId).FirstOrDefault();

            employeeFromDb.UserName = employee.UserName;
            employeeFromDb.Password = employee.Password;

            db.SubmitChanges();
        }

        internal static Employee RetrieveEmployeeUser(string email, int employeeNumber)
        {
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
            Employee employeeFromDb = db.Employees.Where(e => e.UserName == userName && e.Password == password).FirstOrDefault();

            return employeeFromDb;
        }

        internal static bool CheckEmployeeUserNameExist(string userName)
        {
            Employee employeeWithUserName = db.Employees.Where(e => e.UserName == userName).FirstOrDefault();

            return employeeWithUserName == null;
        }


        //// TODO Items: ////
        
        // TODO: Allow any of the CRUD operations to occur here
        internal static void RunEmployeeQueries(Employee employee, string crudOperation)
        {
            switch (crudOperation)
            {
                case "delete":
                    RemoveEmployee(employee);
                    break;
                case "update":
                    UpdateEmployee(employee);
                    break;
                case "read":
                    GetEmployeeByID(employee.EmployeeId);
                    break;
                case "create":
                    AddEmployee(employee);
                    break;
                default:

                    break;
            }
                                    

        }

        internal static void AddEmployee(Employee employee)
        {
            db.Employees.InsertOnSubmit(employee);
            db.SubmitChanges();
           
        }

        internal static Employee GetEmployeeByID(int id)
        {
            
            Employee employee = db.Employees.Where(e => e.EmployeeId == id).Single();
            return employee;
        }

        internal static void UpdateEmployee(Employee employeeToUpdate) 
        {
            Employee employeeFromDb = db.Employees.Where(e => e.EmployeeId == employeeToUpdate.EmployeeId).Single();

            employeeFromDb.FirstName = employeeToUpdate.FirstName;
            employeeFromDb.LastName = employeeToUpdate.LastName;
            employeeFromDb.UserName = employeeToUpdate.UserName;
            employeeFromDb.Password = employeeToUpdate.Password;
            employeeFromDb.Email = employeeToUpdate.Email;

            db.SubmitChanges();
        }

        internal static void RemoveEmployee(Employee employee)
        {
            db.Employees.DeleteOnSubmit(employee);
            db.SubmitChanges();
        }

        // TODO: Animal CRUD Operations
        internal static void AddAnimal(Animal animal)
        {
            db.Animals.InsertOnSubmit(animal);
            db.SubmitChanges();
        }

        internal static Animal GetAnimalByID(int id)
        {
            Animal animal = db.Animals.Where(a => a.AnimalId == id).Single();
            return animal;
        }


        internal static void UpdateAnimal(Animal animal, Dictionary<int, string> updates)
        {
            Animal animalFromDb = db.Animals.Where(a => a.AnimalId == animal.AnimalId).Single();

            var update = from u in updates
                         select u;
            foreach (var u in update)
            {
                switch (u.Key)
                {
                    case 1:
                        animalFromDb.CategoryId = db.Categories.Where(c => c.Name == u.Value).Select(p => p.CategoryId).Single();
                        break;
                    case 2:
                        animalFromDb.Name = u.Value;
                        break;
                    case 3:
                        animalFromDb.Age = int.Parse(u.Value);
                        break;
                    case 4:
                        animalFromDb.Demeanor = u.Value;
                        break;
                    case 5:
                        animalFromDb.KidFriendly = bool.Parse(u.Value);
                        break;
                    case 6:
                        animalFromDb.PetFriendly = bool.Parse(u.Value);
                        break;
                    case 7:
                        animalFromDb.Weight = int.Parse(u.Value);
                        break;
                    case 8:
                        animalFromDb.AnimalId = int.Parse(u.Value);
                        break;
                    default:

                        break;
                }
            }

            db.SubmitChanges();

        }


        internal static void RemoveAnimal(Animal animal)
        {
            db.Animals.DeleteOnSubmit(animal);
            db.SubmitChanges();
        }

        // TODO: Animal Multi-Trait Search
        internal static IQueryable<Animal> SearchForAnimalByMultipleTraits(Dictionary<int, string> updates) // parameter(s)?
        {
            IQueryable<Animal> animalsToSearch = db.Animals;
            animalsToSearch = animalsToSearch.Where(a => updates.Values.Equals(db.Categories.Where(c => c.Name == updates.Values.ElementAt(0)).Select(p => p.CategoryId).Single()) && updates.Values.Equals(a.Name) && updates.Values.Equals(a.Weight.ToString()) && updates.Values.Equals(a.Age.ToString()) && updates.Values.Equals(a.Demeanor) && updates.Values.Equals(a.KidFriendly.ToString()) && updates.Values.Equals(a.PetFriendly.ToString()) && updates.Values.Equals(a.Weight.ToString()) && updates.Values.Equals(a.AnimalId.ToString())).AsQueryable();
            return animalsToSearch;


        }

        // TODO: Misc Animal Things
        internal static int GetCategoryId(string categoryName)
        {
            var CategoryId = db.Categories.Where(c => c.Name == categoryName).Select(p => p.CategoryId).Single();
            return CategoryId;
        }
        
        internal static Room GetRoom(int animalId)
        {
            Room gotRoom = db.Rooms.Where(r => r.AnimalId == animalId).Single();            
            return gotRoom;
        }
        
        internal static int GetDietPlanId(string dietPlanName)
        {
            var DietPlanId = db.DietPlans.Where(d => d.Name == dietPlanName).Select(i => i.DietPlanId).Single();
            return DietPlanId;
        }

        // TODO: Adoption CRUD Operations
        internal static void Adopt(Animal animal, Client client)
        {
            Adoption adoption = new Adoption();
            adoption.ClientId = client.ClientId;
            adoption.AnimalId = animal.AnimalId;
            adoption.PaymentCollected = true;
            db.Adoptions.InsertOnSubmit(adoption);
            db.SubmitChanges();
        }

        internal static IQueryable<Adoption> GetPendingAdoptions()
        {
         
            var pendingAdoptions = db.Adoptions.Where(a => a.ApprovalStatus == null).AsQueryable();
            
            return pendingAdoptions;


        }

        internal static void UpdateAdoption(bool isAdopted, Adoption adoption)
        {
            Adoption adoptionFileToUpdate = db.Adoptions.Where(a => a.AnimalId == adoption.AnimalId).Single();
            if (isAdopted == true)
            {
                adoptionFileToUpdate.ApprovalStatus = "Approved";
            }
            else
            {
                RemoveAdoption(adoption.AnimalId, adoption.ClientId);
            }
        }

        internal static void RemoveAdoption(int animalId, int clientId)
        {
            Adoption adoptionFileToRemove = db.Adoptions.Where(a => a.AnimalId == animalId && a.ClientId == clientId).Single();
            db.Adoptions.DeleteOnSubmit(adoptionFileToRemove);
            db.SubmitChanges();
        }

        // TODO: Shots Stuff
        internal static IQueryable<AnimalShot> GetShots(Animal animal)
        {
            var animalShots = db.AnimalShots.Where(a => a.AnimalId == animal.AnimalId);

            return animalShots.AsQueryable();
        }

        internal static void UpdateShot(string shotName, Animal animal)
        {
            var checkForShots = db.AnimalShots.Where(a => a.AnimalId == animal.AnimalId && a.ShotId == db.Shots.Where(s => s.Name == shotName).Select(p => p.ShotId).Single());
            if (checkForShots == null)
            {
                AnimalShot newRecord = new AnimalShot();
                newRecord.AnimalId = animal.AnimalId;
                newRecord.ShotId = db.Shots.Where(s => s.Name == shotName).Select(p => p.ShotId).Single();
                newRecord.DateReceived = DateTime.Now;
            }
            else
            {
                AnimalShot record = new AnimalShot();
                var aRecordToUpdate = db.AnimalShots.Where(s => s.AnimalId == animal.AnimalId && s.ShotId == db.Shots.Where(t => t.Name == shotName).Select(p => p.ShotId).FirstOrDefault());
                record = aRecordToUpdate.FirstOrDefault();               
                record.DateReceived = DateTime.Now;
                
                
            }

            db.SubmitChanges();
        }
    }
}