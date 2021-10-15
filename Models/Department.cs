

//1:M

//using Microsoft.EntityFrameworkCore;
//using System;
//using System.Linq;
//using System.ComponentModel.DataAnnotations.Schema;
//using System.ComponentModel.DataAnnotations;
//using System.Collections.Generic;

//namespace EFCoreDemo
//{
//    public class Program
//    {
//        private static EFCoreDemoDbContext _context = new EFCoreDemoDbContext();
//        static void Main()
//        {
//            _context.Database.EnsureCreated(); // normally used for testing, jei nera klentels db, jis pdarys
//            //GetEmployees();
//            //AddEmployee(); 
//            //AddEmployeeWithIdentityInsert(19, "Lops");
//            //UpdateEmployee();
//            //DeleteEmployee();
//            //GetEmployees();
//            //GetShoppingItems();
//            //AddShoppingItems();
//            //GetShoppingItems();
//            //UpdateShoppingItems();
//            //GetShoppingItems();
//            //DeleteShoppingItems();
//            //GetShoppingItems();

//            //GetDepartmentsWithEmployees();
//            Console.WriteLine("");


//            //AddDepartmentsWithEmployees();
//            //GetDepartmentFromEmployee();
//            AddEmployee1();


//            Console.WriteLine("");
//            //GetDepartmentsWithEmployees();

//            //with Linq
//            //GetEmployeeByNonIdCollumns();
//        }

//        static void GetEmployees()
//        {
//            var el = _context.Employees.ToListAsync();
//            Console.WriteLine(el.Result.Count); //result duodamas kad gautume rezultata is to list async
//            foreach (var employee in el.Result)
//                Console.WriteLine(employee);
//        }
//        static void AddEmployee()
//        {
//            var employee = new Employee("Zoro");
//            _context.Employees.Add(employee);
//            _context.SaveChanges();
//        }
//        static void AddEmployeeWithIdentityInsert(int id, string name)
//        {
//            using (var tx = _context.Database.BeginTransaction())
//            {
//                var employee = new Employee(id, name);  //Dteached
//                _context.Employees.Add(employee); // attaching yje entity, added
//                _context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[Employees] ON");
//                _context.SaveChanges(); // generate insert for added, update for modifiend, delete for deleted
//                _context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[Employees] OFF");
//                tx.Commit();
//            }

//        }
//        static void UpdateEmployee()
//        {
//            var emp = _context.Employees.Find(1);
//            if (emp != null)
//                emp.Name = "UpdatedName";
//            _context.SaveChanges();
//        }

//        static void DeleteEmployee()
//        {
//            var emp = _context.Employees.Find(1);
//            if (emp != null)
//            {
//                _context.Employees.Remove(emp);
//                _context.SaveChanges();
//            }
//        }
//        static void GetEmployeeByNonIdCollumns()
//        {
//            var el = _context.Employees
//                .Where(empl => empl.Name == "Palmyra")
//                .FirstOrDefault();
//            Console.WriteLine(el);

//        }
//        static void AddEmployee1()
//        {
//            var emp = new Employee("Krakis");
//            _context.Employees.Add(emp);
//            _context.SaveChanges();
//        }




//        static void GetShoppingItems()
//        {
//            var si = _context.ShoppingItems.ToListAsync();
//            Console.WriteLine(si.Result.Count); //result duodamas kad gautume rezultata is to list async
//            foreach (var item in si.Result)
//                Console.WriteLine(item);
//        }
//        static void AddShoppingItems()
//        {
//            var item = new ShoppingItem("Kebab", 3.99);
//            _context.ShoppingItems.Add(item);
//            _context.SaveChanges();
//        }

//        static void UpdateShoppingItems()
//        {
//            var item = _context.ShoppingItems.Find(1);
//            if (item != null)
//                item.Article = "Kefir";
//            _context.SaveChanges();
//        }

//        static void DeleteShoppingItems()
//        {
//            var item = _context.ShoppingItems.Find(1);
//            if (item != null)
//            {
//                _context.ShoppingItems.Remove(item);
//                _context.SaveChanges();
//            }
//        }

//        static void GetDepartmentsWithEmployees()
//        {
//            var deps = _context.Departments.Include("Employees");
//            foreach (var dep in deps)
//            {
//                Console.WriteLine(dep.Id);
//                Console.WriteLine(dep.Name);
//                Console.WriteLine(dep.Employees != null ? dep.Employees.Count : 0);
//                if (dep.Employees != null)
//                {
//                    foreach (var employee in dep.Employees)
//                    {
//                        Console.WriteLine(employee);
//                    }
//                    Console.WriteLine("");
//                }
//            }

//        }
//        static void AddDepartmentsWithEmployees()
//        {
//            var dName = "HR";
//            var nEmp = new Employee("Zigmas");
//            var mDep = _context.Departments
//                .Include("Employees")
//                .Where(d => d.Name == dName)
//                .First();
//            mDep.Employees.Add(nEmp);
//            _context.SaveChanges();
//        }

//        static void GetDepartmentFromEmployee()
//        {
//            var e = _context.Employees.Include("Department").Where(e => e.Id == 878).First();
//            Console.WriteLine(e);

//            //var empl = _context.Employees.Find(878);
//            //var dep = _context.Departments.Find(empl.DepartmentId);
//            //Console.WriteLine(dep.Name);

//        }


//    }



//    class EFCoreDemoDbContext : DbContext // konfiguracine klase, keliama i aplanka contect. pakaitalas configuracijai askiram faile
//    {
//        public DbSet<Employee> Employees { get; set; } //paduoda entitiy Freamwork, akd su duombaze dirbs info is sitos kemployee kalses
//        public DbSet<ShoppingItem> ShoppingItems { get; set; }
//        public DbSet<Department> Departments { get; set; }


//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
//            //optionsBuilder.UseSqlServer("data source=(localdb)\\MSSQLLocalDB; database=TCL; integrated security=True");
//            optionsBuilder.UseSqlServer("data source=.; database=EF; integrated security=True"); //paduoda prisijungimus, DB tipa
//            //optionsBuilder.LogTo(Console.WriteLine); //informacija pateikiama log
//            base.OnConfiguring(optionsBuilder); //uzbaigia pakietimus
//        }
//        protected override void OnModelCreating(ModelBuilder modelBuilder)
//        {
//            modelBuilder.Entity<Employee>().Property(p => p.Name).
//                HasColumnName("Vardenis").
//                HasColumnType("varchar(55)");

//            modelBuilder.Entity<Department>().HasData(
//                new Department() { Id = 1, Name = "IT" },
//                new Department() { Id = 2, Name = "Marketing" },
//                new Department() { Id = 3, Name = "HR" }
//                );


//            modelBuilder.Entity<Employee>().HasData( //seedinimas
//                new Employee(878, "Joanis") { DepartmentId = 1 },
//                new Employee(3565, "Kybis") { DepartmentId = 2 },
//                new Employee(65, "Darelis") { DepartmentId = 3 }
//                );
//            modelBuilder.Entity<Employee>().Property(p => p.Name).HasColumnName("Vardenis");
//            modelBuilder.Entity<Department>().Property(p => p.Name).HasColumnName("DepName");
//        }
//    }

//    [Table("Darbuotojai")]
//    class Employee
//    {
//        [Column("xxx_id")]
//        public int Id { get; set; }

//        [Required]
//        [Column("Vardukas", TypeName = "varchar(200)")]
//        public string Name { get; set; }
//        public int? DepartmentId { get; set; }
//        public Department Department { get; set; }


//        public Employee(int id, string name)
//        {
//            Id = id;
//            Name = name;
//        }

//        public Employee(string name)
//        {
//            Name = name;
//        }

//        public override string ToString()
//        {
//            return $"{{ Id: {Id} : Name: { Name } }}";
//        }
//    }
//    class ShoppingItem
//    {
//        public int Id { get; set; }
//        public string Article { get; set; }
//        public double Price { get; set; }

//        public ShoppingItem(int id, string article, double price)
//        {
//            Id = id;
//            Article = article;
//            Price = price;
//        }

//        public ShoppingItem(string article, double price)
//        {
//            Article = article;
//            Price = price;
//        }

//        public override string ToString()
//        {
//            return $"{{ Id: {Id} : Article: { Article } : Price: { Price } }}";
//        }
//    }
//    class Department
//    {
//        public int Id { get; set; }
//        public string Name { get; set; }
//        public List<Employee> Employees { get; set; }
//}


//}








////M:M

using System.Collections.Generic;

class Department
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<Employee> Employees { get; set; }
}



