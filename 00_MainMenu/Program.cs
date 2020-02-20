using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _00_MainMenu
{
    public class Program
    {
        static void Main(string[] args)
        {

            bool running = true;
            while (running)
            {
                int selection;

                Console.Clear();
                Console.WriteLine("      Komodo Main Menu");
                Console.WriteLine("  1. Menu");
                Console.WriteLine("  2. Claims");
                Console.WriteLine("  3. Badges");
                Console.WriteLine("  9. Exit");
                Console.WriteLine("\nPlease select an option: ");

                string answer = Console.ReadLine();
                if (String.IsNullOrEmpty(answer) || !int.TryParse(answer, out selection))
                {
                    Console.WriteLine($"Your answer must be a whole number.\n" +
                        $" Press any key to continue");
                    Console.ReadKey();
                }
                else
                {
                    switch (selection)
                    {
                        case 1:
                            // Needed to add the namespaces to the References under 00_MainMenu
                            _01_Cafe.ProgramUI _menuUI = new _01_Cafe.ProgramUI();
                            _menuUI.Run();
                            break;
                        case 2:
                            _02_Claims.ClaimsUI _claimUI = new _02_Claims.ClaimsUI();
                            _claimUI.Run();
                            break;
                        case 3:
                            _03_Badges.BadgeUI _badgeUI = new _03_Badges.BadgeUI();
                            _badgeUI.Run();
                            break;
                        case 9:
                            running = false;
                            break;
                        default:
                            Console.WriteLine($"Option {selection} is not valid.\n" +
                                $" Press any key to continue");
                            Console.ReadKey();
                            break;
                    }
                }
            }
        }
    }
}
