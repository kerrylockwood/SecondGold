using _01_Cafe;
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
                Console.WriteLine("  3. ?????");
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
                            ProgramUI _ui = new _01_Cafe.ProgramUI();
                            _ui.Run();
                            break;
                        case 2:
                            //ProgramUI _ui = new _02_Claims.ProgramUI();
                            //_ui.Run();
                            break;
                        case 3:
                            //ProgramUI _ui = new ProgramUI();
                            //_ui.Run();
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
