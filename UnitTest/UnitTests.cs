using System;
using System.Collections.Generic;
using MenuRepository;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest
{
    [TestClass]
    public class UnitTests
    {
        private readonly MenuReposit _menuRepo = new MenuReposit();

        [TestInitialize]
        public void Seed()
        {
            List<Ingred> _newIngred = new List<Ingred>();
            _newIngred.Add(new Ingred("1-a-Ingred"));
            _newIngred.Add(new Ingred("1-b-Ingred"));
            _newIngred.Add(new Ingred("1-c-Ingred"));

            Menu newMenu = new Menu(1, "Name1", "Desc1", 10.00m, _newIngred);
            //test add 1
            List<Menu> menus = new List<Menu>();

            string rtnStr = _menuRepo.AddMeal(newMenu);
            //Console.WriteLine(rtnStr);

            //test add second
            newMenu = new Menu(2, "Name2", "Desc2", 20.00m);

            rtnStr = _menuRepo.AddMeal(newMenu);
            //Console.WriteLine(rtnStr);

            //test add 3rd
            newMenu = new Menu(3, "Name3", "Desc3", 30.00m);

            rtnStr = _menuRepo.AddMeal(newMenu);
            //Console.WriteLine(rtnStr);
        }

        //==================================================
        [TestMethod]
        public void testAddMeal()
        {
            Menu newMenu = new Menu(4,"Name4", "Desc4", 40.00m);
            //test add 1
            List<Menu> menus = new List<Menu>();
            menus = _menuRepo.RtnAllMeals();
            int b4Count = menus.Count;

            string rtnStr = _menuRepo.AddMeal(newMenu);
            //Console.WriteLine(rtnStr);

            int AftCount = menus.Count;

            Assert.IsTrue(AftCount == b4Count + 1);
            Assert.IsTrue(AftCount == 4);

            //test add same
            newMenu = new Menu(1, "Name1", "Desc1", 10.00m);
            //List<Menu> menus = new List<Menu>();
            menus = _menuRepo.RtnAllMeals();
            b4Count = menus.Count;

            rtnStr = _menuRepo.AddMeal(newMenu);
            //Console.WriteLine(rtnStr);

            AftCount = menus.Count;

            Assert.IsTrue(AftCount == b4Count);

            //test add second
            newMenu = new Menu(5, "Name5", "Desc5", 50.00m);
            //List<Menu> menus = new List<Menu>();
            menus = _menuRepo.RtnAllMeals();
            b4Count = menus.Count;

            rtnStr = _menuRepo.AddMeal(newMenu);
            //Console.WriteLine(rtnStr);

            AftCount = menus.Count;

            Assert.IsTrue(AftCount == b4Count + 1);
        }
        //=============================================
        [TestMethod]
        public void TestAddIngredientToMeal()
        {
            Menu menuInfo = new Menu();
            List<Ingred> ingList = new List<Ingred>();
            int mealNum = 1;
            int beforeIngCnt;

            menuInfo = _menuRepo.RtnMealById(mealNum);
            ingList = menuInfo.MealIngrds;
            if (ingList == null)
            {
                beforeIngCnt = 0;
            }
            else
            {
                beforeIngCnt = ingList.Count;
            }

            _menuRepo.AddIngredToMeal(mealNum, "test add");

            menuInfo = _menuRepo.RtnMealById(mealNum);
            ingList = menuInfo.MealIngrds;
            int afterIngCnt = ingList.Count;

            Assert.AreEqual(afterIngCnt, beforeIngCnt + 1);

            mealNum = 3;
            menuInfo = _menuRepo.RtnMealById(mealNum);
            ingList = menuInfo.MealIngrds;
            if (ingList == null)
            {
                beforeIngCnt = 0;
            }
            else
            {
                beforeIngCnt = ingList.Count;
            }

            _menuRepo.AddIngredToMeal(mealNum, "test add");

            menuInfo = _menuRepo.RtnMealById(mealNum);
            ingList = menuInfo.MealIngrds;
            afterIngCnt = ingList.Count;

            Assert.AreEqual(afterIngCnt, beforeIngCnt + 1);
        }
        //=============================================
        [TestMethod]
        public void testRtnAllMeals()
        {
            List<Menu> menus = new List<Menu>();
            menus = _menuRepo.RtnAllMeals();
            Assert.AreEqual(3, menus.Count);
        }
        //=============================================
        [TestMethod]
        public void testRtnMealById()
        {
            Menu menuInfo = new Menu();

            menuInfo = _menuRepo.RtnMealById(1);

            Assert.AreEqual(1, menuInfo.MealId);
        }
        //=============================================
        [TestMethod]
        public void testDelMealById()
        {
            List<Menu> menuList = new List<Menu>();
            menuList = _menuRepo.RtnAllMeals();
            int beforeCnt = menuList.Count;

            string RtnStr = _menuRepo.DltMealById(1);

            menuList = _menuRepo.RtnAllMeals();
            int afterCnt = menuList.Count;

            Assert.AreEqual(afterCnt, beforeCnt - 1);
        }
        //=============================================
        [TestMethod]
        public void testDltMealIngred()
        {
            Menu menuInfo = new Menu();
            List<Ingred> ingredList = new List<Ingred>();
            Ingred delIngrd = new Ingred();
            int mealNum = 1;

            menuInfo = _menuRepo.RtnMealById(mealNum);

            ingredList = menuInfo.MealIngrds;
            int beforeCnt = ingredList.Count;

            foreach (Ingred ingrd in ingredList)
            {
                if (ingrd.Ingr == "1-c-Ingred")
                {
                    delIngrd = ingrd;
                    break;
                }
            }

            string rtnStr = _menuRepo.DltIngredInMeal(menuInfo.MealId, delIngrd);

            menuInfo = _menuRepo.RtnMealById(mealNum);

            ingredList = menuInfo.MealIngrds;
            int afterCnt = ingredList.Count;

            Assert.AreEqual(afterCnt, beforeCnt - 1);
        }
    }
}
