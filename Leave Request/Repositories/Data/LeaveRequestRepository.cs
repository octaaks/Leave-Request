using Leave_Request.Models;
using Leave_Request.ViewModels;
using Microsoft.EntityFrameworkCore;
using NETCore.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Leave_Request.Repositories.Data
{
    public class LeaveRequestRepository : GeneralRepository<MyContext, LeaveRequest, int>
    {
        private readonly MyContext myContext;
        public LeaveRequestRepository(MyContext myContext) : base(myContext)
        {
            this.myContext = myContext;
        }

        public IEnumerable<LeaveRequestVM> GetEmployeeLeaveRequestVMs(int employeeId)
        {
            var getLR = (from lr in myContext.LeaveRequests
                         join mf in myContext.ManagerFills on lr.Id equals mf.LeaveRequestId
                         join em in myContext.Employees on lr.EmployeeId equals em.Id
                         join tp in myContext.LeaveTypes on lr.LeaveTypeId equals tp.Id
                         join st in myContext.Statuses on mf.StatusId equals st.Id

                         select new LeaveRequestVM
                         {
                             Id = lr.Id,
                             RequestDate = lr.RequestDate,
                             StartDate = lr.StartDate,
                             EndDate = lr.EndDate,
                             LeaveTypeId = lr.LeaveTypeId,
                             LeaveTypeName = tp.Name,
                             LeaveDuration = lr.LeaveDuration,
                             EmployeeId = lr.EmployeeId,
                             EmployeeName = em.Name,

                             ApprovedDate = mf.DateApproved,
                             StatusId = mf.StatusId,
                             StatusName = st.Name,
                             Notes = mf.Note,
                             ManagerFillId = mf.Id

                         }).Where(lr => lr.EmployeeId == employeeId).OrderBy(lr => lr.RequestDate).ToList();
            return getLR;
        }
        public IEnumerable<LeaveRequestVM> GetLeaveRequestVMs()
        {
            var getLR = (from lr in myContext.LeaveRequests
                         join mf in myContext.ManagerFills on lr.Id equals mf.LeaveRequestId
                         join em in myContext.Employees on lr.EmployeeId equals em.Id
                         join tp in myContext.LeaveTypes on lr.LeaveTypeId equals tp.Id
                         join st in myContext.Statuses on mf.StatusId equals st.Id

                         select new LeaveRequestVM
                         {
                             Id = lr.Id,
                             RequestDate = lr.RequestDate,
                             StartDate = lr.StartDate,
                             EndDate = lr.EndDate,
                             LeaveTypeId = lr.LeaveTypeId,
                             LeaveTypeName = tp.Name,
                             LeaveDuration = lr.LeaveDuration,
                             EmployeeId = lr.EmployeeId,
                             EmployeeName = em.Name,

                             ApprovedDate = mf.DateApproved,
                             StatusId = mf.StatusId,
                             StatusName = st.Name,
                             Notes = mf.Note,
                             ManagerFillId = mf.Id
                         }).ToList();
            return getLR;
        }
        public LeaveRequestVM GetById(int id)
        {
            var getLR = (from lr in myContext.LeaveRequests
                         join mf in myContext.ManagerFills on lr.Id equals mf.LeaveRequestId
                         join em in myContext.Employees on lr.EmployeeId equals em.Id
                         join tp in myContext.LeaveTypes on lr.LeaveTypeId equals tp.Id
                         join st in myContext.Statuses on mf.StatusId equals st.Id

                         select new LeaveRequestVM
                         {
                             Id = lr.Id,
                             RequestDate = lr.RequestDate,
                             StartDate = lr.StartDate,
                             EndDate = lr.EndDate,
                             LeaveTypeId = lr.LeaveTypeId,
                             LeaveTypeName = tp.Name,
                             LeaveDuration = lr.LeaveDuration,
                             EmployeeId = lr.EmployeeId,
                             EmployeeName = em.Name,

                             ApprovedDate = mf.DateApproved,
                             StatusId = mf.StatusId,
                             StatusName = st.Name,
                             Notes = mf.Note,
                             ManagerFillId = mf.Id
                         }).Where(lr => lr.Id == id).First();
            return getLR;
        }

        public int AddLeaveRequest(LeaveRequest leaveRequestVM)
        {
            try
            {
                LeaveRequest leaveRequest = new LeaveRequest(
                    leaveRequestVM.RequestDate,
                    leaveRequestVM.StartDate,
                    leaveRequestVM.EndDate,
                    leaveRequestVM.LeaveTypeId,
                    leaveRequestVM.LeaveDuration,
                    leaveRequestVM.EmployeeId
                    );
                myContext.LeaveRequests.Add(leaveRequestVM);
                myContext.SaveChanges();

                var insert = myContext.SaveChanges();
                return insert;
            }
            catch
            {
                throw new DbUpdateException();
            }
        }
    }
}
