using _03_Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _03_Badges
{
    public class BadgeUI
    {
        protected readonly BadgeRepository _badgeRepo = new BadgeRepository();

        //================================
        public void Run()
        {
            _badgeRepo.seed();
            RunMenuUI();
        }

        //================================
        private void RunMenuUI()
        {
            bool running = true;
            while (running)
            {
                string question = $"{"",-20}{"Welcome to Komodo Badges"}\n\n" +
                    "   1. Add a badge\n" +
                    "   2. Update a badge\n" +
                    "   3. List all badges view\n" +
                    "   9. Exit\n\n" +
                    "Please enter your selection: ";

                int selection = GetIntAnswer(question);

                switch (selection)
                {
                    case 1:
                        AddBadgeUI();
                        break;
                    case 2:
                        UpdateBadgeUI();
                        break;
                    case 3:
                        DspAllBadgesUI();
                        break;
                    case 9:
                        running = false;
                        break;
                }
            }
        }

        //================================
        public void AddBadgeUI()
        {
            int badgeToAdd = 0;
            HashSet<string> hashDoorsToAdd = new HashSet<string>();
            string error;

            bool running = true;
            while (running)
            {
                Console.Clear();
                string question = $"{"",-20}{"Welcome to Komodo Badges"}\n\n" +
                    $"What is the number on the badge:  ";
                int selection = GetIntAnswer(question);

                // if Badge already exists, error
                if (_badgeRepo.ChkBadgeExists(selection))
                {
                    Console.WriteLine($"Badge # {selection} already exists - Cannot add");
                    KeyToContinue();
                }
                else
                {
                    badgeToAdd = selection;
                    running = false;
                }
            }

            bool moreDoors = true;
            bool firstLoop = true;
            while (moreDoors)
            {
                string question;
                string answer;
                running = true;
                while (running)
                {
                    Console.Clear();
                    question = null;
                    firstLoop = true;
                    foreach (string door in hashDoorsToAdd)
                    {
                        if (firstLoop)
                        {
                            question = $"Doors already added: {door}";
                            firstLoop = false;
                        }
                        else
                        {
                            question = $"{question}, {door}";
                        }
                    }
                    question = $"{"",-20}{"Welcome to Komodo Badges"}\n\n" +
                        $"Badge # {badgeToAdd}\n" +
                        $"{question}\n\n" +
                        $"List a door that it needs access to:  ";
                    Console.Write(question);

                    answer = Console.ReadLine();
                    if (String.IsNullOrEmpty(answer))
                    {
                        Console.WriteLine($"A Door is Required.");
                        KeyToContinue();
                    }
                    else
                    {
                        hashDoorsToAdd.Add(answer);
                        running = false;
                    }
                }
                question = null;
                firstLoop = true;
                foreach (string door in hashDoorsToAdd)
                {
                    if (firstLoop)
                    {
                        question = $"Doors already added: {door}";
                        firstLoop = false;
                    }
                    else
                    {
                        question = $"{question}, {door}";
                    }
                }
                question = $"{"",-20}{"Welcome to Komodo Badges"}\n\n" +
                    $"Badge # {badgeToAdd}\n" +
                    $"{question}\n\n" +
                    $"Any other doors(y/n)?  ";
                answer = GetYNAnswer(question);
                if (answer == "n")
                {
                    moreDoors = false;
                }
            }

            error = _badgeRepo.AddBadge(badgeToAdd, hashDoorsToAdd.ToList());

            Console.WriteLine($"\n{error}");
            KeyToContinue();
        }

        //================================
        private void UpdateBadgeUI()
        {
            int badgeToUpdate = 0;

            // Get Badge #
            bool running = true;
            while (running)
            {
                Console.Clear();
                string question = $"{"",-20}{"Welcome to Komodo Badges"}\n\n" +
                    $"What is the badge number to update?  ";
                int selection = GetIntAnswer(question);

                if (_badgeRepo.ChkBadgeExists(selection))
                {
                    badgeToUpdate = selection;
                    running = false;
                }
                else
                {
                    Console.WriteLine($"Badge # {selection} does NOT exist");
                    KeyToContinue();
                }
            }


            // Add or Delete?
            running = true;
            while (running)
            {
                // Populate Door list
                List<string> doorList = _badgeRepo.GetDoorList(badgeToUpdate);
                bool firstLoop = true;
                string strDoorList = null;
                if (doorList.Count == 0)
                {
                    strDoorList = "---no doors---";
                }
                else
                {
                    foreach (string door in doorList)
                    {
                        if (firstLoop)
                        {
                            strDoorList = door;
                            firstLoop = false;
                        }
                        else
                        {
                            strDoorList = $"{strDoorList}, {door}";
                        }
                    }
                }
                Console.Clear();
                string question = $"{"",-20}{"Welcome to Komodo Badges"}\n\n" +
                    $"{badgeToUpdate} has access to doors {strDoorList}\n\n" +
                    $"What would you like to do?\n\n" +
                    $"     1. Remove a door\n" +
                    $"     2. Add a door\n" +
                    $"     9. Exit Update Option\n\n" +
                    $"Enter your selection:  ";
                int selection = GetIntAnswer(question);

                switch (selection)
                {
                    case 1:
                        if (doorList.Count == 0)
                        {
                            Console.WriteLine($"There are no Doors on Badge # {badgeToUpdate}");
                            KeyToContinue();
                        }
                        else
                        {
                            RemoveDoorFromBadgeUI(badgeToUpdate);
                        }
                        break;
                    case 2:
                        AddDoorToBadgeUI(badgeToUpdate);
                        break;
                    case 9:
                        running = false;
                        break;
                    default:
                        Console.WriteLine("Please enter a valid selection");
                        KeyToContinue();
                        break;
                }
            }  //Running loop
        }

        //================================
        private void RemoveDoorFromBadgeUI(int badgeToUpdate)
        {
            bool running = true;
            while (running)
            {
                // Populate Door list
                bool firstLoop = true;
                List<string> doorList = _badgeRepo.GetDoorList(badgeToUpdate);
                string strDoorList = null;
                if (doorList.Count == 0)
                {
                    strDoorList = "---no doors---";
                }
                else
                {
                    foreach (string door in doorList)
                    {
                        if (firstLoop)
                        {
                            strDoorList = door;
                            firstLoop = false;
                        }
                        else
                        {
                            strDoorList = $"{strDoorList}, {door}";
                        }
                    }
                }

                Console.Clear();
                Console.Write($"{"",-20}{"Welcome to Komodo Badges"}\n\n" +
                    $"{badgeToUpdate} has access to doors {strDoorList}\n\n" +
                    $"Which door would you like to remove?  ");
                string doorToDelete = Console.ReadLine();

                if (String.IsNullOrEmpty(doorToDelete))
                {
                    Console.WriteLine($"A Door is Required.");
                    KeyToContinue();
                }
                else
                {
                    bool doorFound = false;
                    doorList = _badgeRepo.GetDoorList(badgeToUpdate);
                    foreach (string door in doorList)
                    {
                        if (door == doorToDelete)
                        {
                            doorFound = true;
                            break;
                        }
                    }

                    if (doorFound)
                    {
                        Console.WriteLine($"\n{_badgeRepo.RemoveDoorFromBadge(badgeToUpdate, doorToDelete)}");
                        KeyToContinue();
                        running = false;
                    }
                    else
                    {
                        Console.WriteLine($"Door {doorToDelete} NOT found on Badge # {badgeToUpdate}");
                        KeyToContinue();
                    }
                }
            }
        }

        //================================
        private void AddDoorToBadgeUI(int badgeToUpdate)
        {
            bool running = true;
            while (running)
            {
                // Populate Door list
                bool firstLoop = true;
                List<string> doorList = _badgeRepo.GetDoorList(badgeToUpdate);
                string strDoorList = "---no doors---";
                if (doorList.Count != 0)
                {
                    foreach (string door in doorList)
                    {
                        if (firstLoop)
                        {
                            strDoorList = door;
                            firstLoop = false;
                        }
                        else
                        {
                            strDoorList = $"{strDoorList}, {door}";
                        }
                    }
                }

                Console.Clear();
                Console.Write($"{"",-20}{"Welcome to Komodo Badges"}\n\n" +
                    $"{badgeToUpdate} has access to doors {strDoorList}\n\n" +
                    $"Please enter a door to add:  ");
                string doorToAdd = Console.ReadLine();

                if (String.IsNullOrEmpty(doorToAdd))
                {
                    Console.WriteLine($"A Door is Required.");
                    KeyToContinue();
                }
                else
                {
                    bool doorFound = false;
                    doorList = _badgeRepo.GetDoorList(badgeToUpdate);
                    foreach (string door in doorList)
                    {
                        if (door == doorToAdd)
                        {
                            doorFound = true;
                            break;
                        }
                    }

                    if (doorFound)
                    {
                        Console.WriteLine($"Door {doorToAdd} already exists on Badge # {badgeToUpdate}");
                        KeyToContinue();
                    }
                    else
                    {
                        BoolText RtnResult = _badgeRepo.AddDoorToBadge(badgeToUpdate, doorToAdd);
                        Console.WriteLine(RtnResult.RtnText);
                        Console.WriteLine();
                        KeyToContinue();
                        running = false;
                    }
                }
            }
        }

        //================================
        private void DspAllBadgesUI()
        {

            Dictionary<int, List<string>> badgeDict = _badgeRepo.RtnAllBadges();

            List<KeyValuePair<int, List<string>>> badgeList = badgeDict.ToList();

            Console.Clear();
            Console.WriteLine($"{"",-20}{"Welcome to Komodo Badges"}\n\n" +
                $"Key\n" +
                $"{"Badge #",-12}{"Door Access"}");

            foreach (var badgeInfo in badgeList)
            {
                string prtLine = null;
                bool firstLoop = true;

                List<string> doors = badgeInfo.Value;
                doors.Sort();
                if (doors.Count == 0)
                {
                    prtLine = prtLine + "---no doors---";
                }
                else
                {
                    foreach (string door in doors)
                    {
                        if (firstLoop)
                        {
                            // first time don't prefix swith comma
                            prtLine = prtLine + door;
                            firstLoop = false;
                        }
                        else
                        {
                            // prefix with comma
                            prtLine = $"{prtLine}, {door}";
                        }
                    }
                }

                prtLine = $"{badgeInfo.Key,-12}{prtLine}";
                Console.WriteLine(prtLine);
            }
            KeyToContinue();
        }

        //===================================
        private int GetIntAnswer(string question)
        {
            int selection = -9;

            bool running = true;
            while (running)
            {
                string answer;
                Console.Clear();
                Console.Write(question);

                answer = Console.ReadLine();
                if (String.IsNullOrEmpty(answer) || !int.TryParse(answer, out selection))
                {
                    Console.WriteLine($"Your answer must be a whole number.\n" +
                        $" Press any key to continue");
                    Console.ReadKey();
                }
                else
                {
                    running = false;
                }
            }
            return selection;
        }

        //===================================
        private void KeyToContinue()
        {
            Console.WriteLine($"\n Press any key to continue");
            Console.ReadKey();
        }

        //===================================
        private string GetYNAnswer(string question)
        {
            string answer = null;
            bool running = true;
            while (running)
            {
                Console.Clear();
                Console.Write(question);

                answer = Console.ReadLine();
                answer = answer.ToLower();
                if (String.IsNullOrEmpty(answer))
                {
                    Console.WriteLine($"Your answer is Required.");
                    KeyToContinue();
                }
                else if (answer != "y" && answer != "n")
                {
                    Console.WriteLine($"Your answer must be 'Y' or 'N'.");
                    KeyToContinue();
                }
                else
                {
                    running = false;
                }
            }
            return answer;
        }
    }
}
