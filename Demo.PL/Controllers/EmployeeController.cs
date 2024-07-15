using AutoMapper;
using Demo.BLL.Interfaces;
using Demo.BLL.Reposatories;
using Demo.DAL.Models;
using Demo.PL.Helper;
using Demo.PL.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Threading.Tasks;

namespace Demo.PL.Controllers
{
    [Authorize]
    public class EmployeeController : Controller
    {
        private IEmployeeReposatory _employeeReposatory;
        private IDepartmentReposatory _departmentReposatory;
        private IMapper _mapper;

        public EmployeeController(IEmployeeReposatory employeeReposatory, IMapper mapper)
        {
            _employeeReposatory = employeeReposatory;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {
            var employees = await _employeeReposatory.GetAll();
            var result = _mapper.Map<IEnumerable<EmployeeViewModel>>(employees);
            return View(result);
        }

        [HttpGet]
        public IActionResult Create()
        {
            //ViewData["Departments"] = _departmentReposatory.GetAll();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(EmployeeViewModel model)
        {
            if (ModelState.IsValid)
            {
                model.ImageName = DocumentSetting.UploadFile(model.Image, "image");
                var employee = _mapper.Map<Employee>(model);
                var count = await _employeeReposatory.Add(employee);
                if (count > 0)
                {
                    TempData["Message"] = "The Employee has been created :)";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    TempData["Message"] = "The Employee hasn't created :(";
                }
            }
            return View(model);
        }

        public async Task<ActionResult> Details(int? id)
        {
            if (id is null)
                return BadRequest();

            var employee = await _employeeReposatory.Get(id.Value);
            var employeeViewModel = _mapper.Map<EmployeeViewModel>(employee);
            if (employee == null)
                return NotFound();

            return View(employeeViewModel);
        }

        [HttpGet]
        public async Task<ActionResult> Edit(int? id)
        {
            //ViewData["Departments"] = await _departmentReposatory.GetAll();
            if (id is null)
                return BadRequest();

            var employee = await _employeeReposatory.Get(id.Value);
            if (employee == null)
                return NotFound();

            var employeeViewModel = _mapper.Map<EmployeeViewModel>(employee);
            return View(employeeViewModel);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(EmployeeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewData["Departments"] = await _departmentReposatory.GetAll();
                return View(model);
            }

            var employee = _mapper.Map<Employee>(model);

            if (model.ImageName is not null)
            {
                DocumentSetting.DeleteFile(model.ImageName, "Image");
                model.ImageName = DocumentSetting.UploadFile(model.Image, "image");
            }
            var count = await _employeeReposatory.Update(employee);
            if (count > 0)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        [HttpGet]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id is null)
                return BadRequest();

            var employee = await _employeeReposatory.Get(id.Value);
            if (employee == null)
                return NotFound();

            var employeeViewModel = _mapper.Map<EmployeeViewModel>(employee);
            return View(employeeViewModel);
        }

        [HttpPost]
        public async Task<ActionResult> Delete(EmployeeViewModel model)
        {
            var employee = _mapper.Map<Employee>(model);

            var count = await _employeeReposatory.Delete(employee);
            if (count > 0)
            {
                DocumentSetting.DeleteFile(model.ImageName, "image");
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

    }
}
