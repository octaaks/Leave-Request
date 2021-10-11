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

                         select new LeaveRequestVM
                         {
                             RequestDate = lr.RequestDate,
                             StartDate = lr.StartDate,
                             EndDate = lr.EndDate,
                             LeaveTypeId = lr.LeaveTypeId,
                             LeaveDuration = lr.LeaveDuration,
                             EmployeeId = lr.EmployeeId,

                             ApprovedDate = mf.DateApproved,
                             StatusId = mf.StatusId

                         }).Where(lr => lr.EmployeeId == employeeId).ToList();
            return getLR;
        }
        public IEnumerable<LeaveRequestVM> GetLeaveRequestVMs()
        {
            var getLR = (from lr in myContext.LeaveRequests
                         join mf in myContext.ManagerFills on lr.Id equals mf.LeaveRequestId

                         select new LeaveRequestVM
                            {
                                RequestDate = lr.RequestDate,
                                StartDate = lr.StartDate,
                                EndDate = lr.EndDate,
                                LeaveTypeId = lr.LeaveTypeId,
                                LeaveDuration = lr.LeaveDuration,
                                EmployeeId = lr.EmployeeId,
                             
                                ApprovedDate = mf.DateApproved,
                                StatusId = mf.StatusId

                                //ApprovedDate = myContext.ManagerFills.Where(e => e.LeaveRequestId == lr.Id).FirstOrDefault().DateApproved,
                                //StatusId = myContext.ManagerFills.Where(e => e.LeaveRequestId == lr.Id).FirstOrDefault().StatusId

                         }).ToList();
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

        //register new leave request
        public int Insert(LeaveRequestVM leaveRequestVM)
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
                myContext.LeaveRequests.Add(leaveRequest);
                myContext.SaveChanges();

                ManagerFill managerFill = new ManagerFill("test", leaveRequestVM.ApprovedDate, leaveRequest.Id, leaveRequestVM.StatusId);

                myContext.ManagerFills.Add(managerFill);

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
