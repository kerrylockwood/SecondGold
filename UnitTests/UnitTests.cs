using System;
using System.Collections.Generic;
using System.Linq;
using _03_Classes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    [TestClass]
    public class UnitTests
    {
        protected readonly BadgeRepository _badgeRepo = new BadgeRepository();

        //===================================
        [TestInitialize]
        public void seed()
        {
            _badgeRepo.seed();
        }

        //===================================
        [TestMethod]
        public void TestRtnAllBadges()
        {
            Dictionary<int, System.Collections.Generic.List<string>> badgeDict = _badgeRepo.RtnAllBadges();

            List<KeyValuePair<int, List<string>>> badgeList = badgeDict.ToList();

            Assert.AreEqual(3, badgeList.Count);
        }

        //===================================
        [TestMethod]
        public void TestAddBadge()
        {
            int badgeToAdd = 5;
            HashSet<string> hashDoorsToAdd = new HashSet<string>();
            string error;

            // before
            Dictionary<int, System.Collections.Generic.List<string>> badgeDict = _badgeRepo.RtnAllBadges();

            List<KeyValuePair<int, List<string>>> badgeList = badgeDict.ToList();
            int beforeCnt = badgeList.Count;

            // add badge
            hashDoorsToAdd.Add("xxx");

            error = _badgeRepo.AddBadge(badgeToAdd, hashDoorsToAdd.ToList());

            // after
            badgeDict = _badgeRepo.RtnAllBadges();

            badgeList = badgeDict.ToList();
            int afterCnt = badgeList.Count;

            Assert.AreEqual(beforeCnt + 1, afterCnt);
        }

        //===================================
        [TestMethod]
        public void TestAddDoorToBadge()
        {
            // door already on Badge
            int badgeToUpdate = 3;
            string doorToAdd = "C1";
            List<string> doorList = _badgeRepo.GetDoorList(badgeToUpdate);
            int beforeCnt = doorList.Count();

            BoolText result = _badgeRepo.AddDoorToBadge(badgeToUpdate, doorToAdd);

            doorList = _badgeRepo.GetDoorList(badgeToUpdate);
            int afterCnt = doorList.Count();

            Assert.AreEqual(beforeCnt, afterCnt);

            // adding door
            badgeToUpdate = 3;
            doorToAdd = "C2";
            doorList = _badgeRepo.GetDoorList(badgeToUpdate);
            beforeCnt = doorList.Count();

            result = _badgeRepo.AddDoorToBadge(badgeToUpdate, doorToAdd);

            doorList = _badgeRepo.GetDoorList(badgeToUpdate);
            afterCnt = doorList.Count();

            Assert.AreEqual(beforeCnt + 1, afterCnt);
        }

        //===================================
        [TestMethod]
        public void TestRemoveDoorFromBadge()
        {
            int badgeToUpdate = 3;
            string doorToDelete = "C1";
            List<string> doorList = _badgeRepo.GetDoorList(badgeToUpdate);
            int beforeCnt = doorList.Count();

            string result = _badgeRepo.RemoveDoorFromBadge(badgeToUpdate, doorToDelete);

            doorList = _badgeRepo.GetDoorList(badgeToUpdate);
            int afterCnt = doorList.Count();

            Assert.AreEqual(beforeCnt -1, afterCnt);
        }

        //===================================
        [TestMethod]
        public void TestGetDoorList()
        {
            int badgeNum = 3;

            List<string> doorList = _badgeRepo.GetDoorList(badgeNum);
            int Cnt = doorList.Count();

            Assert.AreEqual(2, Cnt);
        }
        //===================================
        [TestMethod]
        public void TestChkBadgeExists()
        {
            bool badgeExists = _badgeRepo.ChkBadgeExists(1);
            Assert.IsTrue(badgeExists);

            badgeExists = _badgeRepo.ChkBadgeExists(6);
            Assert.IsFalse(badgeExists);
        }
    }
}
