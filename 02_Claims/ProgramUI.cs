using _02_Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _02_Claims
{
    class ProgramUI
    {
        private readonly ClaimReposit _claimRepo = new ClaimReposit();

        public void Run()
        {
            _claimRepo.SeedQue();
            RunMenu();
        }

        //===================================
        private void RunMenu()
        {
            bool running = true;
            while (running)
            {
                //string answer;
                //int selection;
                string question = "Welcome to Komodo Claims\n\n" +
                    "   1. See all claims\n" +
                    "   2. Take care of next claim\n" +
                    "   3. Enter a new claim\n" +
                    "   9. Exit\n\n" +
                    "Please enter your selection: ";

                int selection = GetIntAnswer(question);

                switch (selection)
                {
                    case 1:
                        ViewAllClaims();
                        break;
                    case 2:
                        ProcessNextClaim();
                        break;
                    case 3:
                        AddNewClaim();
                        break;
                    case 9:
                        running = false;
                        break;
                }
            }
        }

        //===================================
        private void ViewAllClaims()
        {
            List<CustClaim> claimList = new List<CustClaim>();

            claimList = _claimRepo.RtnAllClaims();

            Console.Clear();

            Console.WriteLine($"{"",-40}{"Active Claims"}\n");
            Console.WriteLine($"{"ClaimID",-9} {"Type",-7} {"Description",-30} {"Amount",-12} {"DateOfAccident",-20} {"DateOfClaim",-17} {"IsValid",-10}");
            Console.WriteLine($"{"-------",-9} {"----",-7} {"-----------",-30} {"------",-12} {"--------------",-20} {"-----------",-17} {"-------",-10}");

            foreach (CustClaim claimInfo in claimList)
            {
                //string str = claimInfo.IsValid.ToString();
                Console.WriteLine($"{claimInfo.ClaimId,-9} {claimInfo.ClaimType,-7} {claimInfo.Description,-30} {claimInfo.ClaimAmount,-12} {claimInfo.DateOfIncident.ToString("M/dd/yy"),-20} {claimInfo.DateOfClaim.ToString("M/dd/yy"),-17} {claimInfo.IsValid,-10}");
            }
            KeyToContinue();
        }

        //===================================
        private void ProcessNextClaim()
        {
            CustClaim claimInfo = new CustClaim();
            claimInfo = _claimRepo.RtnPeekNextClaim();

            string answer = null;
            bool running = true;
            while (running)
            {
                Console.Clear();

                Console.WriteLine("Here are the details for the next claim to be handled:\n");
                Console.WriteLine($"  Claim ID.........:  {claimInfo.ClaimId}");
                Console.WriteLine($"  Claim Type.......:  {claimInfo.ClaimType}");
                Console.WriteLine($"  Claim Description:  {claimInfo.Description}");
                Console.WriteLine($"  Claim Amount.....:  {claimInfo.ClaimAmount}");
                Console.WriteLine($"  Date of Incident.:  {claimInfo.DateOfIncident.ToString("M/dd/yy")} ");
                Console.WriteLine($"  Date of Claim....:  {claimInfo.DateOfClaim.ToString("M/dd/yy")}");
                Console.WriteLine($"  Claim is Valid...:  {claimInfo.IsValid}");
                Console.Write("\nDo you want to deal with this claim now(y/n)?  ");

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
                    if (answer == "y")
                    {
                        claimInfo = _claimRepo.RtnDeQNextClaim();
                        Console.WriteLine($"  Claim ID {claimInfo.ClaimId} has been processed.\n");
                        KeyToContinue();
                    }
                    running = false;
                }
            }
        }

        //===================================
        private void AddNewClaim()
        {
            CustClaim newClaim = new CustClaim();
            int selection = 0;
            string answer = null;
            string question = null;

            // Claim ID
            bool running = true;
            while (running)
            {
                question = null;
                selection = 0;
                List<CustClaim> claimList = new List<CustClaim>();

                Console.Clear();
                question = "Add New Claim\n\n" +
                    "Enter the claim id: ";
                selection = GetIntAnswer(question);

                claimList = _claimRepo.RtnAllClaims();

                running = false;
                foreach (CustClaim claimInfo in claimList)
                {
                    if (selection == claimInfo.ClaimId)
                    {
                        Console.WriteLine($"Claim ID {selection} already exists.  Please choose another ID");
                        KeyToContinue();
                        running = true;
                        break;
                    }
                }
            }
            newClaim.ClaimId = selection;

            // Claim Type
            running = true;
            while (running)
            {
                selection = 0;
                question = null;

                Console.Clear();
                question = $"Add New Claim\n\n" +
                    $"Claim ID:  {newClaim.ClaimId}\n\n" +
                    $"Select a the claim type from the list\n";

                int typeCnt = Enum.GetNames(typeof(TypeOfClaim)).Length;

                for (int cnt = 1; cnt <= typeCnt; cnt++)
                {
                    question = $"{question}   {cnt}. {(TypeOfClaim)cnt}\n";
                }
                selection = GetIntAnswer(question);

                if (selection < 1 || selection > typeCnt)
                {
                    Console.WriteLine("Invalid number entered.");
                    KeyToContinue();
                }
                else
                {
                    newClaim.ClaimType = (TypeOfClaim)selection;
                    running = false;
                }
            }

            // Claim Description
            running = true;
            while (running)
            {
                question = null;
                answer = null;

                Console.Clear();
                question = $"Add New Claim\n\n" +
                    $"Claim ID:  {newClaim.ClaimId}\n" +
                    $"Claim Type:  {newClaim.ClaimType}\n\n" +
                    $"Enter a claim description: ";

                Console.Clear();
                Console.Write(question);

                answer = Console.ReadLine();
                if (String.IsNullOrEmpty(answer))
                {
                    Console.WriteLine($"A description is Required.");
                    KeyToContinue();
                }
                else
                {
                    newClaim.Description = answer;
                    running = false;
                }
            }

            // Amount of Damage
            running = true;
            while (running)
            {
                decimal selectAmt = -9;
                question = null;
                answer = null;

                Console.Clear();
                question = $"Add New Claim\n\n" +
                    $"Claim ID:  {newClaim.ClaimId}\n" +
                    $"Claim Type:  {newClaim.ClaimType}\n" +
                    $"Claim Description:  {newClaim.Description}\n\n" +
                    $"Amount of Damage: ";

                Console.Clear();
                Console.Write(question);

                answer = Console.ReadLine();
                if (String.IsNullOrEmpty(answer) || !decimal.TryParse(answer, out selectAmt))
                {
                    Console.WriteLine($"Your answer must be a number.\n" +
                        $" Press any key to continue");
                    Console.ReadKey();
                }
                else
                {
                    newClaim.ClaimAmount = selectAmt;
                    running = false;
                }

            }

            // Date Of Accident
            running = true;
            while (running)
            {
                DateTime selectDate;
                question = null;
                answer = null;

                Console.Clear();
                question = $"Add New Claim\n\n" +
                    $"Claim ID:  {newClaim.ClaimId}\n" +
                    $"Claim Type:  {newClaim.ClaimType}\n" +
                    $"Claim Description:  {newClaim.Description}\n" +
                    $"Claim Amount:  {newClaim.ClaimAmount}\n\n" +
                    $"Date Of Accident: ";

                Console.Clear();
                Console.Write(question);

                answer = Console.ReadLine();
                if (String.IsNullOrEmpty(answer) || !DateTime.TryParse(answer, out selectDate))
                {
                    Console.WriteLine($"Your answer must be a date.\n" +
                        $" Press any key to continue");
                    Console.ReadKey();
                }
                else
                {
                    newClaim.DateOfIncident = selectDate;
                    running = false;
                }
            }

            // Date of Claim
            running = true;
            while (running)
            {
                DateTime selectDate;
                question = null;
                answer = null;

                Console.Clear();
                question = $"Add New Claim\n\n" +
                    $"Claim ID:  {newClaim.ClaimId}\n" +
                    $"Claim Type:  {newClaim.ClaimType}\n" +
                    $"Claim Description:  {newClaim.Description}\n" +
                    $"Claim Amount:  {newClaim.ClaimAmount}\n" +
                    $"Date of Incident:  {newClaim.DateOfIncident.ToString("M/dd/yy")}\n\n" +
                    $"Date of Claim: ";

                Console.Clear();
                Console.Write(question);

                answer = Console.ReadLine();
                if (String.IsNullOrEmpty(answer) || !DateTime.TryParse(answer, out selectDate))
                {
                    Console.WriteLine($"Your answer must be a date.\n" +
                        $" Press any key to continue");
                    Console.ReadKey();
                }
                else if (selectDate < newClaim.DateOfIncident)
                {
                    Console.WriteLine($"The Incident Date CANNOT be before the Claim Date.\n" +
                        $" Press any key to continue");
                    Console.ReadKey();
                }
                else
                {
                    newClaim.DateOfClaim = selectDate;
                    running = false;
                }
            }

            // This claim is valid (or not)
            answer = null;

            Console.Clear();
            question = $"Add New Claim\n\n" +
                $"Claim ID:  {newClaim.ClaimId}\n" +
                $"Claim Type:  {newClaim.ClaimType}\n" +
                $"Claim Description:  {newClaim.Description}\n" +
                $"Claim Amount:  {newClaim.ClaimAmount}\n" +
                $"Date of Incident:  {newClaim.DateOfIncident.ToString("M/dd/yy")}\n" +
                $"Date of Claim:  {newClaim.DateOfClaim.ToString("M/dd/yy")}\n\n";
            if (newClaim.IsValid)
            {
                question = question + $"This claim is valid.\n\n";
            }
            else
            {
                question = question + $"This claim is NOT valid.\n\n";
            }
            question = question + $"Do you want to add this claim? ";

            answer = GetYNAnswer(question);

            if (answer == "y")
            {
                CustClaim addedClaim = new CustClaim();
                addedClaim = _claimRepo.AddClaim(newClaim);
                if (addedClaim.ClaimId == newClaim.ClaimId)
                {
                    Console.WriteLine($"Claim ID {addedClaim.ClaimId} added");
                }
                else
                {
                    Console.WriteLine($"WARNING:  Claim ID {addedClaim.ClaimId} NOT added");
                }
            }
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

        //===================================
        private void KeyToContinue()
        {
            Console.WriteLine($"\n Press any key to continue");
            Console.ReadKey();
        }
    }
}
