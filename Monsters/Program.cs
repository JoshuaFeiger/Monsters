using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Monsters
{
    // **************************************************
    //
    // Title: Monsters
    // Description: Demonstration of classes and objects
    // Author: Joshua Caleb Feiger
    // Dated Created: 11/25/2019
    // Last Modified: 12/4/2019
    //
    // **************************************************    
    class Program
    {
        static void Main(string[] args)
        {
            //
            // initialize monster list
            //

            //List<Monster> monsters = InitializeMonsterList();
            List<Monster> monsters = ReadFromDataFile();

            //
            // call methods
            //
            DisplayMenuScreen(monsters);

        }

        static List<Monster> InitializeMonsterList()
        {
            //List<int> myInts = new List<int>();
            //myInts.Add(3);
            //myInts.Add(22);

            //List<int> yourInts = new List<int>()
            //{
            //    3,
            //    22
            //};

            //Monster myMonster = new Monster();
            //myMonster.Name = "John";
            //myMonster.Age = 44;
            //myMonster.Attitude = Monster.EmotionalState.angry;

            //Monster yourMonster = new Monster()
            //{
            //    Name = "John",
            //    Age = 44,
            //    Attitude = Monster.EmotionalState.angry
            //};

            //
            // create a list of monsters
            //
            List<Monster> monsters = new List<Monster>()
            {

                new Monster()
                {
                    Name = "Sid",
                    Age = 145,
                    Attitude = Monster.EmotionalState.happy,
                    Town = "Jankenpon",
                    Tribe = Monster.Tribes.snowballGang,
                    Active = true,
                    Birthdate = DateTime.Parse($"05/28/{DateTime.Now.Year - 145}")
                    //In case you're wondering, no, it shouldn't disturb you that this monster's birthdate
                    //shifts around based on what year it is.
                },

                new Monster()
                {
                    Name = "Lucy",
                    Age = 125,
                    Attitude = Monster.EmotionalState.bored,
                    Town = "Isogashiinara",
                    Tribe = Monster.Tribes.gamers,
                    Active = true,
                    Birthdate = DateTime.Parse($"03/17/{DateTime.Now.Year - 125}")
                },

                new Monster()
                {
                    Name = "Ying",
                    Age = 15,
                    Attitude = Monster.EmotionalState.happy,
                    Town = "West Town",
                    Tribe = Monster.Tribes.goodChildren,
                    Active = true,
                    Birthdate = DateTime.Parse($"05/01/{DateTime.Now.Year - 15}")
                },

                new Monster()
                {
                    Name = "Yang",
                    Age = -15,
                    Attitude = Monster.EmotionalState.sad,
                    Town = "East Town",
                    Tribe = Monster.Tribes.badChildren,
                    Active = false,
                    Birthdate = DateTime.Parse($"11/01/{DateTime.Now.Year + 15}")
                }

            };

            Console.WriteLine(monsters[0]);

            return monsters;
        }

        static void DisplayMenuScreen(List<Monster> monsters)
        {
            bool quitApplication = false;
            string menuChoice;

            do
            {
                DisplayScreenHeader("Main Menu");

                //
                // get user menu choice
                //
                Console.WriteLine("Please enter a menu choice!");
                Console.WriteLine("(1) List All Monsters");
                Console.WriteLine("(2) View Monster Detail");
                Console.WriteLine("(3) Add Monster");
                Console.WriteLine("(4) Delete Monster");
                Console.WriteLine("(5) Update Monster");
                Console.WriteLine("(6) Write to Data File");
                Console.WriteLine("(7) Filter by attitude");
                Console.WriteLine("(8) Quit");
                menuChoice = GetUserInput().ToLower();

                //
                // process user menu choice
                //
                switch (menuChoice)
                {
                    case "1":
                        DisplayAllMonsters(monsters);
                        break;

                    case "2":
                        DisplayViewMonsterDetail(monsters);
                        break;

                    case "3":
                        DisplayAddMonster(monsters);
                        break;

                    case "4":
                        DisplayDeleteMonster(monsters);
                        break;

                    case "5":
                        DisplayUpdateMonster(monsters);
                        break;

                    case "6":
                        DisplayWriteToDataFile(monsters);
                        break;

                    case "7":
                        DisplayFilterByAttitude(monsters);
                        break;

                    case "8":
                        quitApplication = true;
                        break;

                    default:
                        Console.WriteLine();
                        Console.WriteLine("Please enter a number that corresponds to a menu choice.");
                        DisplayContinuePrompt();
                        break;
                }


            } while (!quitApplication);
        }
    

        static void DisplayFilterByAttitude(List<Monster> Monsters)
        {
            DisplayScreenHeader("Filter by Attitude");
            List<Monster> FilteredMonsters = new List<Monster>();
            Monster.EmotionalState SelectedAttitude = Monster.EmotionalState.happy;

            foreach (Monster monster in Monsters)
            {
                if (monster.Attitude == SelectedAttitude)
                {
                    Console.WriteLine("\t" + monster.Name);
                    FilteredMonsters.Add(monster);
                }
            }
            DisplayContinuePrompt();
        }

        static void DisplayWriteToDataFile(List<Monster> Monsters)
        {
            DisplayScreenHeader("Write to data file");

            //warn user
            DisplayContinuePrompt();

            WriteToDataFile(Monsters);
            //process I/O exeptions

            Console.WriteLine();
            Console.WriteLine("List written to Data file.");

            DisplayContinuePrompt();
        }

        static List<Monster> ReadFromDataFile()
        {
            List<Monster> Monsters = new List<Monster>();

            //Read all lines in the file
            string[] MonsterStrings = File.ReadAllLines("Data\\Data.txt");

            foreach (string MonsterString in MonsterStrings)
            {
                //get individual properties
                string[] MonsterProperties = MonsterString.Split('|');

                //create monster
                Monster NewMonster = new Monster();

                NewMonster.Name = MonsterProperties[0];
                int.TryParse(MonsterProperties[1], out int Age);
                NewMonster.Age = Age;
                Enum.TryParse(MonsterProperties[2], out Monster.EmotionalState Attitude);
                NewMonster.Attitude = Attitude;
                NewMonster.Town = MonsterProperties[3];
                Enum.TryParse(MonsterProperties[4], out Monster.Tribes Tribe);
                NewMonster.Tribe = Tribe;
                bool.TryParse(MonsterProperties[5], out bool Active);
                NewMonster.Active = Active;
                DateTime.TryParse(MonsterProperties[6], out DateTime BirthDate);
                NewMonster.Birthdate = BirthDate;

                Monsters.Add(NewMonster);
            }

            return Monsters;
        }

        static void WriteToDataFile(List<Monster> Monsters)
        {
            string[] MonsterStrings = new string[Monsters.Count];
            for (int Index = 0; Index < Monsters.Count; Index++)
            {
                string MonsterString =
                    Monsters[Index].Name + "|" +
                    Monsters[Index].Age + "|" +
                    Monsters[Index].Attitude + "|" +
                    Monsters[Index].Town + "|" +
                    Monsters[Index].Tribe + "|" +
                    Monsters[Index].Active + "|" +
                    Monsters[Index].Birthdate;
                MonsterStrings[Index] = MonsterString;
            }
            File.WriteAllLines("Data\\Data.txt", MonsterStrings);
        }

        static void DisplayUpdateMonster(List<Monster> Monsters)
        {
            //todo: validate
            Monster MonsterToUpdate = null;
            while (MonsterToUpdate == null)
            {
                DisplayScreenHeader("Update Monster");

                Console.WriteLine("List of Monsters");
                foreach (Monster ObjMonster in Monsters)
                {
                    Console.WriteLine("\t" + ObjMonster.Name);
                }

                Console.WriteLine("Enter the name of the monster you wish to update.");
                string Name = GetUserInput();

                MonsterToUpdate = Monsters.FirstOrDefault(m => m.Name == Name);

                DisplayScreenHeader("Update Monster");
                if (MonsterToUpdate == null)
                {
                    Console.WriteLine($"\"{Name}\" is not the name of one of the monsters.");
                    Console.WriteLine("Please make sure you properly spell and capitalize the name, it offends them if you don't do so.");
                }
                else
                {
                    bool QuitMenu = false;
                    while (QuitMenu == false)
                    {
                        DisplayScreenHeader("Update Monster");
                        MonsterInfo(MonsterToUpdate);

                        Console.WriteLine();
                        Console.WriteLine("CHOOSE THE FEATURE YOU WANT TO EDIT.");
                        Console.WriteLine();
                        Console.WriteLine("1. Edit Monster Name");
                        Console.WriteLine("2. Edit Monster Age");
                        Console.WriteLine("3. Edit Monster Attitude");
                        Console.WriteLine("4. Edit Monster Hometown");
                        Console.WriteLine("5. Edit Monster Tribe");
                        Console.WriteLine("6. Edit Monster Birth Date");
                        Console.WriteLine("7. Edit Monster Activity Status");
                        Console.WriteLine("8. Exit");
                        Console.WriteLine();
                        string UserResponse = GetUserInput();

                        switch (UserResponse)
                        {
                            case "1":
                                bool UserResponseValid = false;

                                while (!UserResponseValid)
                                {
                                    DisplayScreenHeader("Edit Monster Name");
                                    Console.WriteLine("Enter a new name for the monster.");
                                    UserResponse = GetUserInput();
                                    if (UserResponse == "")
                                    {
                                        DisplayScreenHeader("Edit Monster Name");
                                        Console.WriteLine("You must enter a name for your monster. Do not leave your monster's name blank.");
                                        DisplayContinuePrompt();
                                    }
                                    else
                                    {
                                        MonsterToUpdate.Name = UserResponse;
                                        UserResponseValid = true;
                                    }
                                }
                                break;
                            case "2":
                                UserResponseValid = false;
                                while (!UserResponseValid)
                                {
                                    DisplayScreenHeader("Edit Monster Age");
                                    Console.WriteLine("Enter a new age for the monster.");
                                    if (int.TryParse(GetUserInput(), out int age))
                                    {
                                        MonsterToUpdate.Age = age;
                                        UserResponseValid = true;
                                    }
                                    else
                                    {
                                        DisplayScreenHeader("Edit Monster Age");
                                        Console.WriteLine("Input invalid. Please enter a number, such as 25.");
                                        DisplayContinuePrompt();
                                    }
                                }
                                break;
                            case "3":
                                UserResponseValid = false;
                                while (!UserResponseValid)
                                {
                                    DisplayScreenHeader("Edit Monster Attitude");
                                    Console.WriteLine("Enter a new attitude for the monster.");
                                    Console.WriteLine();
                                    Console.WriteLine("Emotion listing:");
                                    Console.WriteLine("\tHappy,");
                                    Console.WriteLine("\tSad,");
                                    Console.WriteLine("\tAngry,");
                                    Console.WriteLine("\tBored,");
                                    Console.WriteLine("\tNone");
                                    Console.WriteLine();
                                    if (Enum.TryParse(GetUserInput().ToLower(), out Monster.EmotionalState attitude))
                                    {
                                        MonsterToUpdate.Attitude = attitude;
                                        UserResponseValid = true;
                                    }
                                    else
                                    {
                                        DisplayScreenHeader("Edit Monster Attitude");
                                        Console.WriteLine("Input invalid. Please enter one of the listed emotions.");
                                        DisplayContinuePrompt();
                                    }
                                }
                                break;
                            case "4":
                                UserResponseValid = false;
                                while (!UserResponseValid)
                                {
                                    DisplayScreenHeader("Edit Monster Hometown");
                                    Console.WriteLine("Enter a new hometown for your monster.");
                                    UserResponse = GetUserInput();
                                    if (UserResponse == "")
                                    {
                                        DisplayScreenHeader("Edit Monster Hometown");
                                        Console.WriteLine("You must enter a hometown for your monster. Do not leave this blank.");
                                        DisplayContinuePrompt();
                                    }
                                    else
                                    {
                                        MonsterToUpdate.Town = UserResponse;
                                        UserResponseValid = true;
                                    }
                                }
                                break;
                            case "5":
                                UserResponseValid = false;
                                while (!UserResponseValid)
                                {
                                    DisplayScreenHeader("Edit Monster Tribe");
                                    Console.WriteLine("Enter a new tribe for your monster.");
                                    Console.WriteLine();
                                    Console.WriteLine("Tribe listing:");
                                    Console.WriteLine("\tGood Children,");
                                    Console.WriteLine("\tBad Children,");
                                    Console.WriteLine("\tSnowball Gang,");
                                    Console.WriteLine("\tGamers");
                                    Console.WriteLine("Enter \"none\" if your monster does not belong to one of these.");
                                    Console.WriteLine();
                                    Monster.Tribes Tribe = MonsterToUpdate.TribeNameToTribe(GetUserInput());
                                    if (Tribe == Monster.Tribes.glitchedMonsters)
                                    {
                                        DisplayScreenHeader("Edit Monster Tribe");
                                        Console.WriteLine("Input invalid. Please enter the name of one of the listed tribes, or \"none\" if your monster does not belong to a tribe.");
                                        DisplayContinuePrompt();
                                    }
                                    else
                                    {
                                        MonsterToUpdate.Tribe = Tribe;
                                        UserResponseValid = true;
                                    }
                                }
                                break;
                            case "6":
                                UserResponseValid = false;
                                while (!UserResponseValid)
                                {
                                    DisplayScreenHeader("Edit Monster Birth Date");
                                    Console.WriteLine("Enter a birth date for your monster. MM/DD/YYYY");
                                    if (DateTime.TryParse(GetUserInput(), out DateTime BirthDate))
                                    {
                                        MonsterToUpdate.Birthdate = BirthDate;
                                        UserResponseValid = true;
                                    }
                                    else
                                    {
                                        DisplayScreenHeader("Edit Monster Birth Date");
                                        Console.WriteLine("Input invalid. Please enter a date in the form of MM/DD/YYYY.");
                                        DisplayContinuePrompt();
                                    }
                                }
                                break;
                            case "7":
                                UserResponseValid = false;
                                while (!UserResponseValid)
                                {
                                    DisplayScreenHeader("Edit Monster Activity Status");
                                    Console.WriteLine("Is your monster currently active? (yes/no)");
                                    UserResponse = GetUserInput().ToLower();
                                    UserResponseValid = true;
                                    if (UserResponse == "yes")
                                    {
                                        MonsterToUpdate.Active = true;
                                    }
                                    else if (UserResponse == "no")
                                    {
                                        MonsterToUpdate.Active = false;
                                    }
                                    else
                                    {
                                        UserResponseValid = false;
                                        DisplayScreenHeader("Edit Monster Activity Status");
                                        Console.WriteLine("Input invalid. Simply type in \"yes\" or \"no\".");
                                        DisplayContinuePrompt();
                                    }
                                }
                                break;
                            case "8":
                                QuitMenu = true;
                                break;
                            default:
                                DisplayScreenHeader("Update Monster");
                                Console.WriteLine("Please enter the name of one of the menu options.");
                                DisplayContinuePrompt();
                                break;
                        }
                    }
                }
                DisplayContinuePrompt();
            }
        }

            static void DisplayDeleteMonster(List<Monster> monsters)
        {
            DisplayScreenHeader("Delete Monster");

            //
            // display all monster names
            //
            Console.WriteLine("\tMonster Names");
            Console.WriteLine("\t-------------");
            foreach (Monster monster in monsters)
            {
                Console.WriteLine("\t" + monster.Name);
            }
            Console.WriteLine("\t-------------");

            //
            // get user monster choice
            //
            Console.WriteLine();
            Console.WriteLine("\tEnter the name of the monster you wish to delete.");
            string monsterName = GetUserInput();

            //
            // get monster object
            //
            Monster selectedMonster = null;
            foreach (Monster monster in monsters)
            {
                if (monster.Name == monsterName)
                {
                    selectedMonster = monster;
                    break;
                }
            }

            //
            // delete monster
            //
            if (selectedMonster != null)
            {
                monsters.Remove(selectedMonster);
                Console.WriteLine();
                Console.WriteLine($"\t{selectedMonster.Name} deleted");
            }
            else
            {
                DisplayScreenHeader("Delete Monster");
                Console.WriteLine("Input was invalid. Please enter the name of one of the listed monsters.");
                Console.WriteLine("Make sure you use the correct capitals-- the monsters get upset if you don't.");
            }

            DisplayContinuePrompt();
        }

        static void DisplayViewMonsterDetail(List<Monster> monsters)
        {
            bool CanProceed = false;
            Monster selectedMonster = null;
            while (!CanProceed)
            {
                DisplayScreenHeader("Monster Detail");

                //
                // display all monster names
                //
                Console.WriteLine("\tMonster Names");
                Console.WriteLine("\t-------------");
                foreach (Monster monster in monsters)
                {
                    Console.WriteLine("\t" + monster.Name);
                }
                Console.WriteLine("\t-------------");

                //
                // get user monster choice
                //
                Console.WriteLine();
                Console.WriteLine("Please enter the name of the monster you wish to view.");
                string monsterName = GetUserInput();

                //
                // get monster object
                //
                foreach (Monster monster in monsters)
                {
                    if (monster.Name == monsterName)
                    {
                        selectedMonster = monster;
                        break;
                    }
                }
                if (!(selectedMonster == null))
                {
                    CanProceed = true;
                }
                else
                {
                    DisplayScreenHeader("Monster Detail");
                    Console.WriteLine("Input was invalid. Please enter the name of one of the listed monsters.");
                    Console.WriteLine("Make sure you use the correct capitals-- the monsters get upset if you don't.");
                    DisplayContinuePrompt();
                }
            }

            //
            // display monster detail
            //
            Console.WriteLine();
            Console.WriteLine("\t*********************");
            MonsterInfo(selectedMonster);
            Console.WriteLine("\t*********************");

            DisplayContinuePrompt();
        }

        static void DisplayAddMonster(List<Monster> monsters)
        {
            //todo: validate

            Monster newMonster = new Monster();

            //
            // add monster object property values
            //

            //establish some variables to be used in the validation process as necessary.
            bool UserResponseValid = false;
            string UserResponse;

            //Get the monster's name.
            while (!UserResponseValid)
            {
                DisplayScreenHeader("Add Monster");
                Console.WriteLine("\tPlease enter a name for your monster.");
                UserResponse = GetUserInput();
                if (UserResponse == "")
                {
                    DisplayScreenHeader("Add Monster");
                    Console.WriteLine("You must enter a name for your monster. Do not leave your monster's name blank.");
                    DisplayContinuePrompt();
                }
                else
                {
                    newMonster.Name = UserResponse;
                    UserResponseValid = true;
                }
            }

            //Get the monster's age.
            UserResponseValid = false;
            while (!UserResponseValid)
            {
                DisplayScreenHeader("Add Monster");
                Console.WriteLine("\tPlease enter the age of your monster.");
                if (int.TryParse(GetUserInput(), out int age))
                {
                    newMonster.Age = age;
                    UserResponseValid = true;
                }
                else
                {
                    DisplayScreenHeader("Add Monster");
                    Console.WriteLine("Input invalid. Please enter a number, such as 25.");
                    DisplayContinuePrompt();
                }
            }

            //Get the monster's attitude.
            UserResponseValid = false;
            while (!UserResponseValid)
            {
                DisplayScreenHeader("Add Monster");
                Console.WriteLine("\tPlease enter the attitude of your monster.");
                Console.WriteLine();
                Console.WriteLine("Emotion listing:");
                Console.WriteLine("\tHappy,");
                Console.WriteLine("\tSad,");
                Console.WriteLine("\tAngry,");
                Console.WriteLine("\tBored,");
                Console.WriteLine("\tNone");
                Console.WriteLine();
                if (Enum.TryParse(GetUserInput().ToLower(), out Monster.EmotionalState attitude))
                {
                    newMonster.Attitude = attitude;
                    UserResponseValid = true;
                }
                else
                {
                    DisplayScreenHeader("Add Monster");
                    Console.WriteLine("Input invalid. Please enter one of the listed emotions.");
                    DisplayContinuePrompt();
                }
            }

            //Get the monster's hometown.
            UserResponseValid = false;
            while (!UserResponseValid)
            {
                DisplayScreenHeader("Add Monster");
                Console.WriteLine("\tPlease enter the name of your monster's hometown.");
                UserResponse = GetUserInput();
                if (UserResponse == "")
                {
                    DisplayScreenHeader("Add Monster");
                    Console.WriteLine("You must enter a hometown for your monster. Do not leave this blank.");
                    DisplayContinuePrompt();
                }
                else
                {
                    newMonster.Town = UserResponse;
                    UserResponseValid = true;
                }
            }

            //Get monster tribe.
            UserResponseValid = false;
            while (!UserResponseValid)
            {
                DisplayScreenHeader("Add Monster");
                Console.WriteLine("\tPlease enter the name of your monster's tribe.");
                Console.WriteLine();
                Console.WriteLine("Tribe listing:");
                Console.WriteLine("\tGood Children,");
                Console.WriteLine("\tBad Children,");
                Console.WriteLine("\tSnowball Gang,");
                Console.WriteLine("\tGamers");
                Console.WriteLine("Enter \"none\" if your monster does not belong to one of these.");
                Console.WriteLine();
                Monster.Tribes Tribe = newMonster.TribeNameToTribe(GetUserInput());
                if (Tribe == Monster.Tribes.glitchedMonsters)
                {
                    DisplayScreenHeader("Add Monster");
                    Console.WriteLine("Input invalid. Please enter the name of one of the listed tribes, or \"none\" if your monster does not belong to a tribe.");
                    DisplayContinuePrompt();
                }
                else
                {
                    newMonster.Tribe = Tribe;
                    UserResponseValid = true;
                }
            }

            //Get monster birth date.
            UserResponseValid = false;
            while (!UserResponseValid)
            {
                DisplayScreenHeader("Add Monster");
                Console.WriteLine("\tPlease enter the birth date of your monster. MM/DD/YYYY");
                if (DateTime.TryParse(GetUserInput(), out DateTime BirthDate))
                {
                    newMonster.Birthdate = BirthDate;
                    UserResponseValid = true;
                }
                else
                {
                    DisplayScreenHeader("Add Monster");
                    Console.WriteLine("Input invalid. Please enter a date in the form of MM/DD/YYYY.");
                    DisplayContinuePrompt();
                }
            }

            //Get monster activity status.
            UserResponseValid = false;
            while (!UserResponseValid)
            {
                DisplayScreenHeader("Add Monster");
                Console.WriteLine("\tIs your monster currently active? (yes/no)");
                UserResponse = GetUserInput().ToLower();
                UserResponseValid = true;
                if (UserResponse == "yes")
                {
                    newMonster.Active = true;
                }
                else if (UserResponse == "no")
                {
                    newMonster.Active = false;
                }
                else
                {
                    UserResponseValid = false;
                    DisplayScreenHeader("Add Monster");
                    Console.WriteLine("Input invalid. Simply type in \"yes\" or \"no\".");
                    DisplayContinuePrompt();
                }
            }

            //
            // echo new monster properties
            //
            DisplayScreenHeader("Add Monster");
            Console.WriteLine("\tNew Monster's Properties");
            MonsterInfo(newMonster);
            DisplayContinuePrompt();

            monsters.Add(newMonster);
        }

        static void DisplayAllMonsters(List<Monster> monsters)
        {
            DisplayScreenHeader("All Monsters");

            Console.WriteLine("\t***************************");
            foreach (Monster monster in monsters)
            {
                MonsterInfo(monster);
                Console.WriteLine();
                Console.WriteLine("\t***************************");
            }

            DisplayContinuePrompt();
        }

        static void MonsterInfo(Monster monster)
        {
            Console.WriteLine($"\tName: {monster.Name}");
            Console.WriteLine($"\tAge: {monster.Age}");
            Console.WriteLine($"\tAttitude: {monster.Attitude}");
            Console.WriteLine($"\tTown: {monster.Town}");
            Console.WriteLine($"\tTribe: " + monster.TribeName());
            Console.WriteLine($"\tBirth Date: {monster.Birthdate.ToLongDateString()}");
            if (monster.Active)
            {
                Console.WriteLine("\tIs currently active.");
            }
            else
            {
                Console.WriteLine("\tIs currently inactive.");
            }
            Console.WriteLine();
            Console.WriteLine("\t" + monster.Greeting());
        }

        #region HELPER METHODS

        /// <summary>
        /// display continue prompt
        /// </summary>
        static void DisplayContinuePrompt()
        {
            Console.WriteLine();
            Console.WriteLine("Press any key to continue.");
            Console.ReadKey();
        }

        /// <summary>
        /// Get input from the user.
        /// </summary>
        /// <returns></returns>
        static string GetUserInput()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("User Input: ");
            string UserInput = Console.ReadLine();
            Console.ForegroundColor = ConsoleColor.White;
            return UserInput;
        }

        /// <summary>
        /// display screen header
        /// </summary>
        static void DisplayScreenHeader(string headerText)
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("\t\t" + headerText);
            Console.WriteLine();
        }
        #endregion
    }
}
