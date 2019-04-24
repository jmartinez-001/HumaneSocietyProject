using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumaneSociety
{
    public static class UserInterface
    {
        public static void DisplayUserOptions(List<string> options)
        {
            foreach(string option in options)
            {
                Console.WriteLine(option);
            }
        }
        public static void DisplayUserOptions(string options)
        {
            Console.WriteLine(options);
        }
        public static string GetUserInput()
        {
            string input = Console.ReadLine();
            switch (input.ToLower())
            {
                case "reset":
                    PointOfEntry.Run();
                    Environment.Exit(1);
                    break;
                case "exit":
                    Environment.Exit(1);
                    break;
                default:
                    break;
            }

            return input;
        }
        public static string GetStringData(string parameter, string target)
        {
            string data;
            DisplayUserOptions($"What is {target} {parameter}?");
            data = GetUserInput();
            return data;
        }

        internal static bool? GetBitData(List<string> options)
        {
            DisplayUserOptions(options);
            string input = GetUserInput();
            if (input.ToLower() == "yes" || input.ToLower() == "y")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool? GetBitData()
        {
            string input = GetUserInput();
            if (input.ToLower() == "yes" || input.ToLower() == "y")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        internal static bool? GetBitData(string target, string parameter)
        {
            DisplayUserOptions($"Is {target} {parameter}?");
            string input = GetUserInput();
            if (input.ToLower() == "yes" || input.ToLower() == "y")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        internal static void DisplayAnimals(List<Animal> animals)
        {
            foreach(Animal animal in animals)
            {
                Console.WriteLine(animal.AnimalId + " " + animal.Name + " " + animal.Category.Name);
            }
        }

        internal static int GetIntegerData()
        {
            try
            {
                int data = int.Parse(GetUserInput());
                return data;
            }
            catch
            {
                Console.Clear();
                DisplayUserOptions("Incorrect input please enter an integer number.");
                return GetIntegerData();
            }
        }

        public static int GetIntegerData(string parameter, string target)
        {
            try
            {
                int data = int.Parse(GetStringData(parameter, target));
                return data;
            }
            catch
            {
                Console.Clear();
                DisplayUserOptions("Incorrect input please enter an integer number.");
                return GetIntegerData(parameter, target);
            }
        }

        internal static void DisplayClientInfo(Client client)
        {
            List<string> info = new List<string>() { client.FirstName, client.LastName, client.Email, client.Address.USState.Name };
            DisplayUserOptions(info);
            Console.ReadLine();
        }

        public static void DisplayAnimalInfo(Animal animal)
        {
            Room animalRoom = Query.GetRoom(animal.AnimalId);
            List<string> info = new List<string>() {"ID: " + animal.AnimalId, animal.Name, animal.Age + "years old", "Demeanour: " + animal.Demeanor, "Kid friendly: " + BoolToYesNo(animal.KidFriendly), "pet friendly: " + BoolToYesNo(animal.PetFriendly), $"Location: " + animalRoom.RoomId, "Weight: " + animal.Weight.ToString(),  "Food amoumnt in cups:" + animal.DietPlan.FoodAmountInCups};
            DisplayUserOptions(info);
            Console.ReadLine();

        }

        private static string BoolToYesNo(bool? input)
        {
            if (input == true)
            {
                return "yes";
            }
            else
            {
                return "no";
            }
        }

        public static bool GetBitData(string option)
        {
            DisplayUserOptions(option);
            string input = GetUserInput();
            if (input.ToLower() == "yes" || input.ToLower() == "y")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static Dictionary<string, string> GetAnimalSearchCriteria()
        {
            Dictionary<string, string> searchParameters = new Dictionary<string, string>();
            bool isSearching = true;
            while (isSearching)
            {
                Console.Clear();
                List<string> options = new List<string>() { "Select Search Criteia: (Enter number and choose finished when finished)", "1. Category", "2. Name", "3. Age", "4. Demeanor", "5. Kid friendly", "6. Pet friendly", "7. Weight", "8. ID", "9. Finished" };
                DisplayUserOptions(options);
                string input = GetUserInput();
                if (input.ToLower() == "9" || input.ToLower() == "finished")
                {
                    isSearching = false;
                    continue;
                }
                else
                {
                    searchParameters = EnterSearchCriteria(searchParameters, input);
                }
            }
            return searchParameters;
        }
        public static Dictionary<string, string> EnterSearchCriteria(Dictionary<string, string> searchParameters, string input)
        {
            Console.Clear();
            switch (input)
            {
                case "1":
                    searchParameters.Add("Category", GetStringData("category", "the animal's"));
                    break;
                case "2":
                    searchParameters.Add("Name", GetStringData("name", "the animal's"));
                    break;
                case "3":
                    searchParameters.Add("Age", GetIntegerData("age", "the animal's").ToString());
                    break;
                case "4":
                    searchParameters.Add("Demeanor", GetStringData("demeanor", "the animal's"));
                    break;
                case "5":
                    searchParameters.Add("KidFriendly", GetBitData("the animal", "kid friendly").ToString());
                    break;
                case "6":
                    searchParameters.Add("PetFriendly", GetBitData("the animal", "pet friendly").ToString());
                    break;
                case "7":
                    searchParameters.Add("Weight", GetIntegerData("weight", "the animal's").ToString());
                    break;
                case "8":
                    searchParameters.Add("ID", GetIntegerData("ID", "the animal's").ToString());
                    break;
                default:
                    DisplayUserOptions("Input not recognized please try agian");
                    break;
            }
            return searchParameters;
        }
    }
}
