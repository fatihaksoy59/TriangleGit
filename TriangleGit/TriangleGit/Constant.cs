using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TriangleGit.Tables;

namespace TriangleGit
{
    public class Constant
    {
        public static MobileServiceClient MobileService = new MobileServiceClient("https://outside.azurewebsites.net");

        public IMobileServiceTable<Users> usersTable = MobileService.GetTable<Users>();

        public IMobileServiceTable<Events> eventsTable = MobileService.GetTable<Events>();

        public IMobileServiceTable<Going> goingTable = MobileService.GetTable<Going>();
    }
}
