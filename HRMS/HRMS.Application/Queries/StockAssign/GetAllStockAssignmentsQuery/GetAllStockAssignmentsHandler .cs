using HRMS.Application.Common.Interfaces;
using HRMS.Application.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace HRMS.Application.Queries.StockAssign.GetAllStockAssignmentsQuery
{
    public  class GetAllStockAssignmentsHandler : IRequestHandler<GetAllStockAssignmentsQuery, List<StockAssignmentDTO>>
    {
        private readonly IApplicationDbContext _context;
        public GetAllStockAssignmentsHandler(IApplicationDbContext context)
        {
            _context = context;
            
        }
        public async Task<List<StockAssignmentDTO>> Handle(
            GetAllStockAssignmentsQuery request,
            CancellationToken ct)
        {



            // var assignments = await _context.StockAssignments
            //.AsNoTracking()
            //.Include(a => a.Employee)
            //.Include(a => a.Stock)
            //.OrderByDescending(a => a.AssigmentDate)
            //.ToListAsync(ct); // Execute query first

            // Now project in memory using LINQ to Objects
            //return assignments.Select(a => new StockAssignmentDTO
            //{
            //    Id = a.Id,
            //    EmployeeName = a.Employee != null ? a.Employee.Name : "Unknown",
            //    StockName = a.Stock != null ? a.Stock.Name : "Deleted",
            //    StockType = a.Stock?.Type,
            //    AssignmentDate = a.AssigmentDate,
            //    AssignedQuantity = a.AsssignedQuantity
            //}).ToList();
            //Fetching full entities (StockAssignment + related Employee and Stock)
            //and that mean two or more enitity so we used AsNoTracking
            var assignments = await _context.StockAssignments
            .AsNoTracking()
            .Include(a => a.Employee)
            .Include(a => a.Stock)
            .OrderByDescending(a => a.AssigmentDate)
            .Select(a => new StockAssignmentDTO
            {
               Id = a.Id,
               EmployeeName = a.Employee != null ? a.Employee.Name : "Unknown",
               StockName = a.Stock != null ? a.Stock.Name : "Deleted",
               StockType = a.Stock.Type,
               AssignmentDate = a.AssigmentDate,
               AssignedQuantity = a.AsssignedQuantity
            }) .ToListAsync(ct); // Projected DTO fetched directly from database

            return assignments;


        }
    }
}
