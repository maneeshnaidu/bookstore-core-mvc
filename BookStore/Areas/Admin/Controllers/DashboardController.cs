using BookStore.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin")]
    [Authorize(Roles ="Admin")]
    public class DashboardController : Controller
    {
        private readonly IAccountRepository _accountRepository;

        public DashboardController(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        // GET: DashboardController
        [Route("")]
        public ActionResult Index()
        {
            var users = _accountRepository.GetAllApplicationUsers();
            return View(users);
        }

        // GET: DashboardController/Details/5
        [Route("details/{id}")]
        public ActionResult Details(int id)
        {
            return View(id);
        }

        //// GET: DashboardController/Create
        //[Route]
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: DashboardController/Create
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create(IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: DashboardController/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        //// POST: DashboardController/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: DashboardController/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        //// POST: DashboardController/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Delete(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}
