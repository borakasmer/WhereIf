using DalWhereIf.Models.DB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WhereIfTest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WhereIfController : ControllerBase
    {
        NorthwindContext _contex;
        public WhereIfController(NorthwindContext contex)
        {
            _contex = contex;
        }

        [HttpPost(Name = "WhereIf")]
        public async Task<List<Employee>> TestWhereIf([FromBody] WhereIfFilter filter)
        {
            var linq = _contex.Employees
                .WhereIf(filter.City !=null, e => e.City == filter.City)
                .WhereIf(filter.Country != null, e => e.Country == filter.Country)
                .WhereIf(filter.TitleOfCourtesy != null, e => e.TitleOfCourtesy == filter.TitleOfCourtesy)
                .Select(e => new Employee()
                {
                    Address = e.Address,
                    EmployeeId = e.EmployeeId,
                    LastName = e.LastName,
                    FirstName = e.FirstName,
                    BirthDate = e.BirthDate,
                    City = e.City,
                    Region = e.Region,
                    PostalCode = e.PostalCode,
                    Country = e.Country,
                }).ToQueryString();

            return await _contex.Employees
                .WhereIf(filter.City != null, e => e.City == filter.City)
                .WhereIf(filter.Country != null, e => e.Country == filter.Country)
                .WhereIf(filter.TitleOfCourtesy != null, e => e.TitleOfCourtesy == filter.TitleOfCourtesy)
                .Select(e => new Employee()
                {
                    Address = e.Address,
                    EmployeeId = e.EmployeeId,
                    LastName = e.LastName,
                    FirstName = e.FirstName,
                    BirthDate = e.BirthDate,
                    City = e.City,
                    Region = e.Region,
                    PostalCode = e.PostalCode,
                    Country = e.Country,
                }).ToListAsync();

        }
    }
}