using CodeFirst_EF.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System.Diagnostics;

namespace CodeFirst_EF.Controllers
{
    public class HomeController : Controller
    {
        private readonly StudentDBContext studentDB;

        //private readonly ILogger<HomeController> _logger;

        //public HomeController(ILogger<HomeController> logger)
        //{
        //    _logger = logger;
        //}




        public HomeController(StudentDBContext studentDB)
        {
            this.studentDB = studentDB;
        }



        public IActionResult Index()
        {
            var stdData = studentDB.Students.ToList();
            return View(stdData);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Student std)
        {
            if (ModelState.IsValid)
            {
                studentDB.Students.Add(std);
                studentDB.SaveChanges();
             return   RedirectToAction("Index","Home");
            }

            return View(std);
        }

        public async Task<IActionResult> Details(int ID)
        {
            var stdData = await studentDB.Students.FirstOrDefaultAsync(x=>x.ID == ID);
            return View(stdData);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? ID)
        {
            if(ID == null || studentDB.Students ==  null)
            {
                return NotFound();
            }

            var stdData = await studentDB.Students.FindAsync(ID);
            if (stdData==null)
            {
                return NotFound();
            }

            return View(stdData);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int? ID ,Student std)
        {

            if (ID!=std.ID)
            {
                return NotFound();

            }


            if (ModelState.IsValid)
            {
                studentDB.Update(std);
                await studentDB.SaveChangesAsync();
                return RedirectToAction("Index", "Home");
            }
            return View(std);
        }


        public async Task<IActionResult> Delete(int? ID)
        {
            if (ID == null || studentDB.Students == null)
            {
                return NotFound();
            }
        var stdData = await studentDB.Students.FirstOrDefaultAsync(x => x.ID == ID);
            if (stdData == null)
            {
                return NotFound();

            }
            return View(stdData);
        }

        [HttpPost,ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int? ID)
        {
            var stdData = await studentDB.Students.FindAsync(ID);
            if (stdData != null)
            {
                studentDB.Students.Remove(stdData);
            }
            await studentDB.SaveChangesAsync();
            return RedirectToAction("Index", "Home");

        }




        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}