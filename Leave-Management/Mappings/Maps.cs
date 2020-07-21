using AutoMapper;
using Leave_Management.Data;
using Leave_Management.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Leave_Management.Mappings
{
    public class Maps : Profile
    {
        public Maps()                                                   //For the mappings to work, the field names must be exactly the same
        {
            CreateMap<LeaveType, DetailsLeaveTypeVM>().ReverseMap();
            CreateMap<LeaveType, CreateLeaveTypeVM>().ReverseMap();
            CreateMap<LeaveAllocation, DetailLeaveAllocationVM>().ReverseMap();
            CreateMap<LeaveAllocation, CreateLeaveAllocationVM>().ReverseMap();
            CreateMap<LeaveHistory, DetailLeaveHistoryVM>().ReverseMap();
            CreateMap<LeaveHistory, CreateLeaveHistoryVM>().ReverseMap();
            CreateMap<Employee, EmployeeVM>().ReverseMap();
        }
    }
}
