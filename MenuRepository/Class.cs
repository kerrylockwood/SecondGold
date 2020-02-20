using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MenuRepository
{
    public class Menu
    {
        public int MealId { get; set; }
        public string MealName { get; set; }
        public string MealDesc { get; set; }
        public decimal MealPrice { get; set; }
        public List<Ingred> MealIngrds { get; set; }

        public Menu()
        {
        }

        public Menu(int mealId, string mealName, string mealDesc, decimal mealPrice)
        {
            MealId = mealId;
            MealName = mealName;
            MealDesc = mealDesc;
            MealPrice = mealPrice;
        }

        public Menu(int mealId, string mealName, string mealDesc, decimal mealPrice, List<Ingred> mealIngreds)
        {
            MealId = mealId;
            MealName = mealName;
            MealDesc = mealDesc;
            MealPrice = mealPrice;
            MealIngrds = mealIngreds;
        }
    }

    public class Ingred
    {
        public string Ingr { get; set; }

        public Ingred()
        {
        }

        public Ingred(string ingr)
        {
            Ingr = ingr;
        }
    }
}
