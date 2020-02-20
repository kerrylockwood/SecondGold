using MenuRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01_Cafe
{
    public class ProgramUI
    {
        private readonly MenuReposit _menuRepo = new MenuReposit();

        public void Run()
        {
            _menuRepo.Seed();
            runMenu();
        }
        //==============================
        private void runMenu()
        {
            bool running = true;
            while (running)
            {
                //string answer;
                //int selection;
                string question = "Welcome to the Komodo Cafe\n\n" +
                    "   1. Add Meal\n" +
                    "   2. Add Ingredients to Meal\n" +
                    "   3. View All Meals\n" +
                    "   4. View All Meals with Ingredients\n" +
                    "   5. View a Single Meal with Ingredients\n" +
                    "   6. Delete a Meal And All Its Ingredients\n" +
                    "   7. Delete an Ingredient from a Meal\n" +
                    "   8. Exit\n\n" +
                    "Please enter your selection: ";

                int selection = GetIntAnswer(question);
              
                switch (selection)
                {
                    case 1:
                        AddMealUI();
                        break;
                    case 2:
                        AddIngrdToMealUI();
                        break;
                    case 3:
                        ViewAllMealsUI();
                        break;
                    case 4:
                        ViewAllMealsWithIngUI();
                        break;
                    case 5:
                        ViewSnglMealsWithIngUI();
                        break;
                    case 6:
                        DltMealUI();
                        break;
                    case 7:
                        DltIngredFromMealUI();
                        break;
                    case 8:
                        running = false;
                        break;
                }
            }
        }

        //===================================
        private void AddMealUI()
        {
            Menu menuInfo = new Menu();

            // Meal ID

            bool running = true;
            while (running)
            {
                int selection = GetIntAnswer("Please enter a Meal ID number: ");
               
                Menu existMenu = _menuRepo.RtnMealById(selection);

                if (existMenu.MealId == selection)
                {
                    Console.WriteLine($"meal id {existMenu.MealId} for {existMenu.MealName} already exists\n" +
                        $" press any key to continue");
                    Console.ReadKey();
                }
                else
                {
                    menuInfo.MealId = selection;
                    running = false;
                }
            }

            // Meal Name
            running = true;
            while (running)
            {
                string answer;
                Console.Clear();
                Console.Write($"Meal ID: {menuInfo.MealId}\n" +
                    $"Please enter a Meal Name: ");
                answer = Console.ReadLine();
                if (String.IsNullOrEmpty(answer))
                {
                    Console.WriteLine($"Meal Name cannot be blank.\n" +
                        $" Press any key to continue");
                    Console.ReadKey();
                }
                else
                {
                    menuInfo.MealName = answer;
                    running = false;
                }
            }

            // Meal Desc
            running = true;
            while (running)
            {
                string answer;
                Console.Clear();
                Console.Write($"Meal ID: {menuInfo.MealId}\n" +
                    $"Meal Name: {menuInfo.MealName}\n" +
                    $"Please enter a Meal Description: ");
                answer = Console.ReadLine();
                if (String.IsNullOrEmpty(answer))
                {
                    Console.WriteLine($"Meal Description cannot be blank.\n" +
                        $" Press any key to continue");
                    Console.ReadKey();
                }
                else
                {
                    menuInfo.MealDesc = answer;
                    running = false;
                }
            }

            // Mean Price
            running = true;
            while (running)
            {
                string answer;
                decimal selection;
                Console.Clear();
                Console.Write($"Meal ID: {menuInfo.MealId}\n" +
                    $"Meal Name: {menuInfo.MealName}\n" +
                    $"Mean Description: {menuInfo.MealDesc}\n" +
                    $"Please enter a Price for the Meal: ");

                answer = Console.ReadLine();
                if (String.IsNullOrEmpty(answer) || !decimal.TryParse(answer, out selection))
                {
                    Console.WriteLine($"Meal numbers must be a number.\n" +
                        $" Press any key to continue");
                    Console.ReadKey();
                }
                else
                {
                    menuInfo.MealPrice = selection;
                    running = false;
                }
            }

            Console.WriteLine("");
            Console.WriteLine(_menuRepo.AddMeal(menuInfo));
            Console.WriteLine(" Press any key to continue");
            Console.ReadKey();
        }

        //===================================
        private void AddIngrdToMealUI()
        {
            Menu menuInfo = new Menu();
            int selection = 0;
            bool running;

            // Meal ID
            running = true;
            while (running)
            {
                selection = GetIntAnswer("What Meal # do you want to add Ingredients to? ");
           
                menuInfo = _menuRepo.RtnMealById(selection);

                if (menuInfo.MealId == 0)
                {
                    Console.WriteLine($"meal id {selection} does not exist\n" +
                        $" press any key to continue");
                    Console.ReadKey();
                }
                else
                {
                    running = false;
                }
            }

            // Get ingredients
            bool moreIngreds = true;
            while (moreIngreds)
            {
                running = true;
                while (running)
                {
                    string answer;
                    Console.Clear();
                    Console.Write($"Meal ID: {menuInfo.MealId}\n" +
                        $"Meal Name: {menuInfo.MealName}\n\n" +
                        $"Please enter a new Ingredient: ");
                    answer = Console.ReadLine();
                    if (String.IsNullOrEmpty(answer))
                    {
                        Console.WriteLine($"Ingredient cannot be blank.\n" +
                            $" Press any key to continue");
                        Console.ReadKey();
                    }
                    else
                    {
                        Console.WriteLine("\n" + _menuRepo.AddIngredToMeal(selection, answer));
                        running = false;
                    }
                }
                // Add another ingredient?
                Console.WriteLine("\nDo you want to add another Ingredient? " +
                    "(Y to add another / any other key to return to the menu)? ");
                if (Console.ReadLine().ToLower() != "y")
                {
                    moreIngreds = false;
                }
            }
        }

        //===================================
        private void ViewAllMealsUI()
        {
            //Menu menuInfo = new Menu();
            List<Menu> menuList = new List<Menu>();

            menuList = _menuRepo.RtnAllMeals();

            Console.Clear();

            foreach (Menu menuInfo in menuList)
            {
                Console.WriteLine($"Meal# {menuInfo.MealId}:     {menuInfo.MealName} ${menuInfo.MealPrice}");
                Console.WriteLine($"Description: {menuInfo.MealDesc}");
                Console.WriteLine("--------------------------------------------");
            }
            Console.WriteLine("Press any key to continue");
            Console.ReadKey();
        }

        //===================================
        private void ViewAllMealsWithIngUI()
        {
            //Menu menuInfo = new Menu();
            List<Menu> menuList = new List<Menu>();

            menuList = _menuRepo.RtnAllMeals();

            Console.Clear();

            foreach (Menu menuInfo in menuList)
            {
                Console.WriteLine($"Meal# {menuInfo.MealId}:     {menuInfo.MealName} ${menuInfo.MealPrice}");
                Console.WriteLine($"Description: {menuInfo.MealDesc}");
                List<Ingred> ingredList = new List<Ingred>();
                ingredList = menuInfo.MealIngrds;
                if (ingredList != null && (ingredList.Any()))
                {
                    foreach (Ingred ingred in ingredList)
                    {
                        Console.WriteLine($"     {ingred.Ingr}");
                    }
                }
                Console.WriteLine("--------------------------------------------");
            }
            Console.WriteLine("Press any key to continue");
            Console.ReadKey();
        }
        //===================================
        private void ViewSnglMealsWithIngUI()
        {
            //Menu menuInfo = new Menu();
            Menu menuInfo = new Menu();
            int selection = -9;

            selection = GetIntAnswer("Please enter a Meal ID number: ");
           
            menuInfo = _menuRepo.RtnMealById(selection);

            if (menuInfo.MealId <= 0)
            {
                Console.WriteLine($"Meal # {selection} not found.");
            }
            else
            {
                Console.WriteLine($"\nMeal# {menuInfo.MealId}:     {menuInfo.MealName} ${menuInfo.MealPrice}");
                Console.WriteLine($"Description: {menuInfo.MealDesc}");
                List<Ingred> ingredList = new List<Ingred>();
                ingredList = menuInfo.MealIngrds;
                if (ingredList != null && (ingredList.Any()))
                {
                    foreach (Ingred ingred in ingredList)
                    {
                        Console.WriteLine($"     {ingred.Ingr}");
                    }
                }
            }
            Console.WriteLine("--------------------------------------------");
            Console.WriteLine("Press any key to continue");
            Console.ReadKey();
        }
        //===================================
        private void DltMealUI()
        {
            //Menu menuInfo = new Menu();
            Menu menuInfo = new Menu();
            int selection = -9;

            selection = GetIntAnswer("Please enter the Meal # you want to DELETE: ");
           
            menuInfo = _menuRepo.RtnMealById(selection);

            if (menuInfo.MealId <= 0)
            {
                Console.WriteLine($"Meal # {selection} not found.");
            }
            else
            {
                Console.WriteLine($"\nMeal# {menuInfo.MealId}:     {menuInfo.MealName} ${menuInfo.MealPrice}");
                Console.WriteLine($"Description: {menuInfo.MealDesc}");
                List<Ingred> ingredList = new List<Ingred>();
                ingredList = menuInfo.MealIngrds;
                if (ingredList != null && (ingredList.Any()))
                {
                    foreach (Ingred ingred in ingredList)
                    {
                        Console.WriteLine($"     {ingred.Ingr}");
                    }
                }
                Console.WriteLine("\nAre you sure you want to DELETE this Meal? " +
                        "(Y to add another / any other key to return to the menu)? ");
                if (Console.ReadLine().ToLower() == "y")
                {
                    Console.WriteLine("\n" + _menuRepo.DltMealById(menuInfo.MealId));
                }
                else
                {
                    Console.WriteLine("\nMeal NOT deleted");
                }
            }
            Console.WriteLine("\nPress any key to continue");
            Console.ReadKey();
        }
        //===================================
        private void DltIngredFromMealUI()
        {
            Menu menuInfo = new Menu();
            int selection = -9;

            selection = GetIntAnswer("Please enter a Meal ID number: ");

            menuInfo = _menuRepo.RtnMealById(selection);

            if (menuInfo.MealId <= 0)
            {
                Console.WriteLine($"Meal # {selection} not found.");
            }
            else
            {
                bool running = true;
                while (running)
                {
                    Console.Clear();
                    Console.WriteLine($"\nMeal# {menuInfo.MealId}:     {menuInfo.MealName} ${menuInfo.MealPrice}");
                    Console.WriteLine($"Description: {menuInfo.MealDesc}");
                    List<Ingred> ingredList = new List<Ingred>();
                    ingredList = menuInfo.MealIngrds;
                    if (ingredList != null && (ingredList.Any()))
                    {
                        foreach (Ingred ingred in ingredList)
                        {
                            Console.WriteLine($"     {ingred.Ingr}");
                        }
                        // get ingredient to delete
                        string answer = null;
                        Console.WriteLine("Enter the Ingredient you want to delete" +
                            " or press just Enter to exit without deleting anything:");
                        answer = Console.ReadLine();
                        if (String.IsNullOrEmpty(answer))
                        {
                            break;
                        }
                        bool deleteAttempted = false;
                        // if ingredient is in list, delete it
                        foreach (Ingred ingred in ingredList)
                        {
                            if (ingred.Ingr == answer)
                            {
                                // delete it
                                Console.WriteLine("\n" +
                                    _menuRepo.DltIngredInMeal(menuInfo.MealId, ingred));
                                deleteAttempted = true;
                                break;
                            }
                        }
                        if (deleteAttempted)
                        {
                            Console.WriteLine($"\n\n" +
                                $"Do you want to Delete another Ingredient " +
                                $"(Y to add another / any other key to return to the menu)?");
                            if (Console.ReadLine().ToLower() != "y")
                            {
                                running = false;
                            }
                        }
                        else
                        {
                            Console.WriteLine($"\nIngredient '{answer}' not found in " +
                                $"Ingredient list\n");
                            Console.WriteLine("Press any key to continue");
                            Console.ReadKey();
                        }
                    }
                    else
                    {
                        Console.WriteLine("\nNo Ingredients to delete\n");
                        break;
                    }
                }
            }
            Console.WriteLine("--------------------------------------------");
            Console.WriteLine("Press any key to continue");
            Console.ReadKey();
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
    }
}
