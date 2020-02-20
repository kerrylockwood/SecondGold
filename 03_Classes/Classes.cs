using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _03_Classes
{
    public class BoolText
    {
        public bool IsTrue { get; set; }
        public string RtnText { get; set; }

        public BoolText()
        {
        }

        public BoolText(bool isTrue, string rtnText)
        {
            IsTrue = isTrue;
            RtnText = rtnText;
        }
    }
}
