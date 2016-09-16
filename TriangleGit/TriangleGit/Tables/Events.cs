using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TriangleGit.Tables
{
    public class Events
    {
        public string id { get; set; }
        public string userID { get; set; }
        public double lat { get; set; }
        public double lng { get; set; }
        public DateTime Time { get; set; }
        public string placeName { get; set; }
        public string eventName { get; set; }
        public bool deleted { get; set; }

    }
}
