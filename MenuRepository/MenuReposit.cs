using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MenuRepository
{
    public class MenuReposit
    {
        List<Menu> _meals = new List<Menu>();
        List<Ingred> _ingreds = new List<Ingred>();

        //=========================================
        public void Seed()
        {
            List<Menu> menus = new List<Menu>();

            List<Ingred> _newIngred = new List<Ingred>();
            _newIngred.Add(new Ingred("1-a-Ingred"));
            _newIngred.Add(new Ingred("1-b-Ingred"));
            _newIngred.Add(new Ingred("1-c-Ingred"));

            Menu newMenu = new Menu(1, "Name1", "Desc1", 10.00m, _newIngred);

            //test add 1
            string rtnStr = AddMeal(newMenu);
            //Console.WriteLine(rtnStr);

            //test add second
            _newIngred = new List<Ingred>();
            _newIngred.Add(new Ingred("2-a-Ingred"));
            _newIngred.Add(new Ingred("2-b-Ingred"));
            _newIngred.Add(new Ingred("2-c-Ingred"));

            newMenu = new Menu(2, "Name2", "Desc2", 20.00m, _newIngred);

            rtnStr = AddMeal(newMenu);
            //Console.WriteLine(rtnStr);

            //test add 3rd
            newMenu = new Menu(3, "Name3", "Desc3", 30.00m);

            rtnStr = AddMeal(newMenu);
            //Console.WriteLine(rtnStr);
        }

        //=========================================
        public string AddMeal(Menu menuInfo)
        {
            Menu existMenu = RtnMealById(menuInfo.MealId);

            if (existMenu.MealId == menuInfo.MealId)
            {
                return $"WARNING: Meal not added: Meal ID {menuInfo.MealId} already exists";
            }

            _meals.Add(new Menu(menuInfo.MealId, menuInfo.MealName, menuInfo.MealDesc, menuInfo.MealPrice, menuInfo.MealIngrds));

            return $"Meal {menuInfo.MealName} added";
        }

        //=========================================
        public string AddIngredToMeal(int id, string newIngred)
        {
            // Get meal data
            Menu existMenu = RtnMealById(id);
            if (existMenu == null)
            {
                return $"Menu # {id} Not found";
            }
            List<Ingred> oldIngredList = existMenu.MealIngrds;
            List<Ingred> newIngredList = new List<Ingred>();

            if (oldIngredList != null)
            {
                foreach (Ingred ingr in oldIngredList)
                {
                    newIngredList.Add(ingr);
                }
            }

            newIngredList.Add(new Ingred(newIngred));

            existMenu.MealIngrds = newIngredList;

            return $"{newIngred} added to Meal # {id}";
        }

        //=========================================
        public List<Menu> RtnAllMeals()
        {
            return _meals;
        }

        //=========================================
        public Menu RtnMealById(int id)
        {
            foreach (Menu menuInfo in _meals)
            {
                if (id == menuInfo.MealId)
                {
                    return menuInfo;
                }
            }
            return new Menu();
        }

        //=========================================
        public string DltMealById(int id)
        {
            Menu menuInfo = RtnMealById(id);
            Menu deletedMeal = menuInfo;

            if (menuInfo == null)
            {
                return $"Meal # {id} not found";
            }
            _meals.Remove(menuInfo);
            return $"Sucessfully deleted:\n" +
                $"Meal # {deletedMeal.MealId}\n" +
                $"Meal Name: {deletedMeal.MealName}\n" +
                $"Meal Description: {deletedMeal.MealDesc}\n" +
                $"Meal Price: {deletedMeal.MealPrice}";
        }

        //=========================================
        public string DltIngredInMeal(int id, Ingred ingr)
        {
            Menu menuInfo = RtnMealById(id);
            List<Ingred> IngrList = new List<Ingred>();

            if (menuInfo == null)
            {
                return $"Meal # {id} not found";
            }

            IngrList = menuInfo.MealIngrds;
            bool deleted = IngrList.Remove(ingr);
            menuInfo.MealIngrds = IngrList;

            if (deleted)
            {
                return $"'{ingr.Ingr}' has been DELETED";
            }
            return $"'{ingr.Ingr}' NOT found in Ingredient List";

        }
    }
}
