using Leave_Request.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Leave_Request.Repositories.Data
{
    public class LeaveTypesRepository : GeneralRepository<MyContext, LeaveType, int>
    {
        private readonly MyContext myContext;
        public LeaveTypesRepository(MyContext myContext) : base(myContext)
        {
            this.myContext = myContext;
        }
    }
}
