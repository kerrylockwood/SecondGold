using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _03_Classes
{
    public class BadgeRepository
    {
        public Dictionary<int, List<string>> _badgeDoors = new Dictionary<int, List<string>>();

        //==========================================
        public void seed()
        {
            //string door = null;
            List<string> doorList = new List<string>();

            // Badge 1
            doorList.Add("A1");
            doorList.Add("A2");
            doorList.Add("A3");
            doorList.Add("A0");
            _badgeDoors.Add(1, new List<string>(doorList));

            // Badge 2
            doorList.Clear();
            doorList.TrimExcess();
            doorList.Add("B1");
            doorList.Add("B2");
            doorList.Add("B3");
            doorList.Add("B4");
            _badgeDoors.Add(2, new List<string>(doorList));

            // Badge 3
            doorList.Clear();
            doorList.TrimExcess();
            doorList.Add("C1");
            doorList.Add("C4");
            _badgeDoors.Add(3, new List<string>(doorList));
        }

        //==========================================
        public Dictionary<int, List<string>> RtnAllBadges()
        {
            return _badgeDoors;
        }

        //==========================================
        public string AddBadge(int badgeNum, List<string> doors)
        {
            try
            {
                _badgeDoors.Add(badgeNum, doors);
            }
            catch (ArgumentException)
            {
                return $"WARNING:  Badge # {badgeNum} NOT added";
            }
            return $"Badge # {badgeNum} added";
        }

        //==========================================
        public BoolText AddDoorToBadge(int badgeNum, string doorToAdd)
        {
            List<string> doorList = _badgeDoors[badgeNum];

            BoolText rtnExistError = new BoolText();
            string rtnDesc = null;
            bool errorFound = false;

            foreach (string door in doorList)
            {
                if (door == doorToAdd)
                {
                    errorFound = true;
                    rtnDesc = $"Door {doorToAdd} already exists on Badge #{badgeNum}";
                }
            }

            if (!errorFound)
            {
                doorList.Add(doorToAdd);
                doorList.Sort();
                errorFound = false;
                rtnDesc = $"Door {doorToAdd} Added to Badge #{badgeNum}";
            }

            rtnExistError = new BoolText(errorFound, rtnDesc);
            return rtnExistError;
        }

        //==========================================
        public string RemoveDoorFromBadge(int badgeNum, string door)
        {
            List<string> doors = new List<string>();
            try
            {
                doors = _badgeDoors[badgeNum];
            }
            catch (KeyNotFoundException)
            {
                return $"Door {door} NOT found on Badge # {badgeNum}";
            }
            doors.Remove(door);
            _badgeDoors[badgeNum] = doors;

            return $"Door {door} removed from Badge # {badgeNum}";
        }

        //==========================================
        public List<string> GetDoorList(int badgeNum)
        {
            List<string> doorList = _badgeDoors[badgeNum];
            doorList.Sort();

            return doorList;
        }

        //==========================================
        public bool ChkBadgeExists(int badgeNum)
        {
            try
            {
                List<string> doors = _badgeDoors[badgeNum];
            }
            catch (KeyNotFoundException)
            {
                return false;
            }
            return true;
        }
    }
}
