using Leave_Request.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Leave_Request.Repositories.Data
{
    public class ReligionRepository : GeneralRepository<MyContext, Religion, int>
    {
        private readonly MyContext myContext;
        public ReligionRepository(MyContext myContext) : base(myContext)
        {
            this.myContext = myContext;
        }
    }
}
