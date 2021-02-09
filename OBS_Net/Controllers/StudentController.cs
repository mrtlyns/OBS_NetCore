using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OBS_Net.BL.StudentManager;
using OBS_Net.Entities.Tables;

namespace OBS_Net.Controllers
{
    public class StudentController : Controller
    {
        private readonly IStudentManager _student;
        public StudentController(IStudentManager student)
        {
            _student = student;
        }

        public IActionResult Index()
        {
            var result = _student.Get();
            return View(result);
        }
        public IActionResult Create()
        {
            Student model = _student.GetCreateModel();
            return View(model);
        }
        [HttpPost]
        public IActionResult Create(Student model)
        {
            if (base.TryValidateModel(model))
            {
                _student.Create(model);
                return RedirectToAction("Index");
            }
            return View(model);
        }
    }
}