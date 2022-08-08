using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TTTN_ASP
{
    public class XMessage1
    {
        public string TypeMsg { get; set; }
        public string Msg { get; set; }
        public XMessage1() { }
        public XMessage1(string typeMsg, string msg)
        {
            this.TypeMsg = typeMsg;
            this.Msg = msg;
        }
    }
}