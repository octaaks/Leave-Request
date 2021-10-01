using Leave_Request.Models;
using NETCore.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Leave_Request.Repositories.Data
{
    public class ManagerFillRepository : GeneralRepository<MyContext, ManagerFill, int>
    {
        private readonly MyContext myContext;
        public ManagerFillRepository(MyContext myContext) : base(myContext)
        {
            this.myContext = myContext;
        }
    }
}
