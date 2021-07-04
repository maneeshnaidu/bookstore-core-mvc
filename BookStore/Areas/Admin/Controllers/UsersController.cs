using BookStore.Areas.Admin.Models;
using BookStore.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("users")]
    //[Authorize(Roles = "Admin")]
    public class UsersController : Controller
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IRolesRepository _rolesRepository;

        public UsersController(IAccountRepository accountRepository, IRolesRepository rolesRepository)
        {
            _accountRepository = accountRepository;
            _rolesRepository = rolesRepository;
        }

        [Route("")]
        public IActionResult Index()
        {
            var users = _accountRepository.GetAllApplicationUsers();
            return View(users);
        }

        [Route("roles")]
        public IActionResult GetAllRoles()
        {
            var roles = _accountRepository.GetAllRoles();
            return View(roles);
        }

        [Route("role-create")]
        public IActionResult AddNewRole()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddNewRole(RolesModel model)
        {
            if (ModelState.IsValid)
            {
                await _rolesRepository.AddRoleAsync(model.Name);
            }
            

            return RedirectToAction("GetAllRoles");
        }
    }
}
