








////M:M

using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

class EmployeeRepository
{

    private static EFCoreDemoDbContext _context;

    public EmployeeRepository(EFCoreDemoDbContext context)
    {
        _context = context;
    }

    //static void AddEmployee()
    //{
    //    var employee = new Employee("Zoro");
    //    _context.Employees.Add(employee);
    //    _context.SaveChanges();
    //}

    //static void AddEmployeeWithIDInsert(int id)
    //{
    //    using (var tx = _context.Database.BeginTransaction())
    //    {
    //        var employee = new Employee(id, "Palmyra"); // Detached
    //        _context.Employees.Add(employee); // ... attaching the entity, Added
    //        _context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[Employees] ON");
    //        // Generate insert for "Added" state entities,
    //        // ... Update for "Modified" (none here),
    //        // ... Delete for "Deleted" (none here)
    //        _context.SaveChanges();
    //        _context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[Employees] OFF");
    //        tx.Commit();
    //    }
    //}
    public void GetEmployees()
    {
        var el = _context.Employees.ToListAsync();
        Console.WriteLine(el.Result.Count);
        foreach (var employee in el.Result)
            Console.WriteLine(employee);
    }
    static void UpdateEmployee()
    {
        var emp = _context.Employees.Find(1);
        if (emp != null)
        {
            emp.Name = "UpdatedName";
            _context.SaveChanges();
        }
    }

    static void DeleteEmployee()
    {
        var emp = _context.Employees.Find(1);
        if (emp != null)
        {
            _context.Employees.Remove(emp);
            _context.SaveChanges();
        }
    }

    static void GetEmployeesByNonIdColumn()
    {
        var el = _context.Employees
            .Where(empl => empl.Name == "Petras")
            .FirstOrDefault();
        Console.WriteLine(el);
    }

    
    static void AddEmployee()
    {
        var emp = new Employee("Krakis");
        _context.Employees.Add(emp);
        _context.SaveChanges();
    }
}
