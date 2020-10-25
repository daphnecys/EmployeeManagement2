using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement2.Models
{
    public interface IEmployeeRepository
    {
        Employee GetEmployee(int Id);

        // Part 27: List View
        IEnumerable<Employee> GetAllEmployees();

        // Part 41: Model Binding Complex Type
        Employee Add(Employee employee);
    }
}
