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
            var getLR = (from mf in myContext.ManagerFills
                         join lr in myContext.LeaveRequests on
                         mf.LeaveRequestId equals lr.Id

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
            var getLR = (from mf in myContext.ManagerFills
                         join lr in myContext.LeaveRequests on
                         mf.LeaveRequestId equals lr.Id
                         
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

                                }).ToList();
            return getLR;
        }

        //register new leave request
        public int Insert(LeaveRequestVM leaveRequestVM)
        {
            try
            {
                //var leaveRequest = new LeaveRequest()
                //{
                //    RequestDate = leaveRequestVM.RequestDate,
                //    StartDate = leaveRequestVM.StartDate,
                //    EndDate = leaveRequestVM.EndDate,
                //    LeaveTypeId = leaveRequestVM.LeaveTypeId,
                //    LeaveDuration = leaveRequestVM.LeaveDuration,
                //    EmployeeId = leaveRequestVM.EmployeeId
                //};
                LeaveRequest leaveRequest = new LeaveRequest(
                    leaveRequestVM.RequestDate,
                    leaveRequestVM.StartDate,
                    leaveRequestVM.EndDate,
                    leaveRequestVM.LeaveTypeId,
                    leaveRequestVM.LeaveDuration,
                    leaveRequestVM.EmployeeId
                    );

                //leaveRequest.LeaveTypeId = leaveRequestVM.LeaveTypeId;

                //leaveRequest.RequestDate = leaveRequestVM.RequestDate;
                //leaveRequest.StartDate = leaveRequestVM.StartDate;
                //leaveRequest.EndDate = leaveRequestVM.EndDate;
                ////
                //leaveRequest.LeaveDuration = leaveRequestVM.LeaveDuration;
                //leaveRequest.EmployeeId = leaveRequest.EmployeeId;

                myContext.LeaveRequests.Add(leaveRequest);
                myContext.SaveChanges();

                ManagerFill managerFill = new ManagerFill("test", leaveRequestVM.ApprovedDate, leaveRequest.Id, leaveRequestVM.StatusId);
                //managerFill.Note = "Test";
                //managerFill.DateApproved = leaveRequestVM.ApprovedDate;
                //managerFill.LeaveRequestId = leaveRequest.Id;
                //managerFill.StatusId = leaveRequestVM.StatusId;

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
