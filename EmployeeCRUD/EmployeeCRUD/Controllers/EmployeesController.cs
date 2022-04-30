using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EmployeeCRUD.Data;
using EmployeeCRUD.Models;



namespace EmployeeCRUD.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EmployeesController(ApplicationDbContext context)
        {
            _context = context;
        }
        // GET: EmployeesController
        public ActionResult Index()
        {
            IEnumerable<Employee>colEmployees = _context.Employees.Where(s=>s.Estate==true);// Código refactorizado para retornar 
            return View(colEmployees);
        }

        // GET: EmployeesController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: EmployeesController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EmployeesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Employee employee)
        {
            
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Employees.Add(employee);
                    _context.SaveChanges();
                    TempData["ResultOk"] = "Record added successfuly";
                 
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();//empobj
            }
        }

        // GET: EmployeesController/Edit/5
        public ActionResult Edit(int id)
        {
            if ( id == null || id == 0)
            {
                return NotFound();
            }
            var employeefromdb = _context.Employees.Find(id);
            if (employeefromdb == null)
            {
                return NotFound();
            }
            return View(employeefromdb);
        }

        // POST: EmployeesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Employee employee)
        {
            

            try
            {
                if (ModelState.IsValid)
                {
                    _context.Employees.Update(employee);
                    _context.SaveChanges();
                    TempData["ResultOk"] = "Data uptaded sucessfully";
                   
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();//empobj
            }
        }

        // GET: EmployeesController/Delete/5
        public ActionResult Delete(int id)
        {
            if (id == null || id <= 0 )
            {
                return NotFound();
            }
            var employeefromdb = _context.Employees.Find(id);
            if (employeefromdb == null)
            {
                return NotFound();
            }
            return View(employeefromdb);
        }

        // POST: EmployeesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int? id )
        {

            try
            {
                var employeefromdb = _context.Employees.Find(id);

               


                if (employeefromdb == null)
                {
                    return NotFound();

                }
                // _context.Employees.Remove(employeefromdb); // Refactorizado para inactivar el usuario. No eliminarlo 
                employeefromdb.Estate = false;
                _context.Employees.Update(employeefromdb);

                _context.SaveChanges();
                TempData["ResultOk"] = "Data Deleted Successfully";
                return RedirectToAction("Index");
            }
            catch
            {
                return View(); //empobj
            }
        }
    }
}
