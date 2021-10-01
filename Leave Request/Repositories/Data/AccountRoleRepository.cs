using Leave_Request.Models;
using NETCore.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Leave_Request.Repositories.Data
{
    public class AccountRoleRepository : GeneralRepository<MyContext, AccountRole, int>
    {
        private readonly MyContext myContext;
        public AccountRoleRepository(MyContext myContext) : base(myContext)
        {
            this.myContext = myContext;
        }
    }
}
