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
    [Authorize(Roles = "Admin")]
    public class UsersController : Controller
    {
        private readonly IAccountRepository _accountRepository;

        public UsersController(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
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
    }
}
