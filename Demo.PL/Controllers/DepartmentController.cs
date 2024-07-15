
using AutoMapper;
using Demo.BLL.Interfaces;
using Demo.BLL.Reposatories;
using Demo.DAL.Models;
using Demo.PL.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;

namespace Demo.PL.Controllers
{
    [Authorize]
    public class DepartmentController : Controller
    {
        private IDepartmentReposatory _departmentReposatory;
        public IMapper _mapper;

        public DepartmentController(IDepartmentReposatory departmentReposatory, IMapper mapper)
        {
            _departmentReposatory = departmentReposatory;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {
            var departments = await Task.Run(() => _departmentReposatory.GetAll());
            var result = _mapper.Map<IEnumerable<DepartmentViewModel>>(departments);
            return View(result);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(DepartmentViewModel model)
        {
            if (ModelState.IsValid)
            {
                var department = _mapper.Map<Department>(model);
                var count = await Task.Run(() => _departmentReposatory.Add(department));
                if (count > 0)
                {
                    TempData["Message"] = "The Department has been created :)";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    TempData["Message"] = "The Department hasn't created :(";
                }
            }
            return View(model);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id is null)
                return BadRequest();

            var department = await Task.Run(() => _departmentReposatory.Get(id.Value));
            var departmentViewModel = _mapper.Map<DepartmentViewModel>(department);

            if (department == null)
                return NotFound();

            return View(departmentViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null)
                return BadRequest();

            var department = await Task.Run(() => _departmentReposatory.Get(id.Value));
            if (department == null)
                return NotFound();

            var departmentViewModel = _mapper.Map<DepartmentViewModel>(department);

            return View(departmentViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(DepartmentViewModel model)
        {
            var department = _mapper.Map<Department>(model);
            var count = await Task.Run(() => _departmentReposatory.Update(department));
            if (count > 0)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null)
                return BadRequest();

            var department = await Task.Run(() => _departmentReposatory.Get(id.Value));
            if (department == null)
                return NotFound();

            var departmentViewModel = _mapper.Map<DepartmentViewModel>(department);

            return View(departmentViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(DepartmentViewModel model)
        {
            var department = _mapper.Map<Department>(model);
            var count = await Task.Run(() => _departmentReposatory.Delete(department));
            if (count > 0)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

    }
}
