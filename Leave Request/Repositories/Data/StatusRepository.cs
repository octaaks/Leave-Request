using Leave_Request.Models;
using NETCore.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Leave_Request.Repositories.Data
{
    public class StatusRepository : GeneralRepository<MyContext, Status, int>
    {
        private readonly MyContext myContext;
        public StatusRepository(MyContext myContext) : base(myContext)
        {
            this.myContext = myContext;
        }
    }
}
