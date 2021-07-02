using BookStore.Models;
using BookStore.Repository;
using BookStore.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Controllers
{
    //Global token replacement route method
    //[Route("[controller]/[action]")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;
        private readonly IMessageRepository _messageRepository;
        private readonly IUserService _userService;
        private readonly IEmailService _emailService;
        private readonly NewBookAlertConfig _newBookAlertConfiguration;
        private readonly NewBookAlertConfig _thirdPartyBookConfiguration;

        public HomeController(ILogger<HomeController> logger, IConfiguration configuration, IOptionsSnapshot<NewBookAlertConfig> newBookAlertConfiguration,
            IMessageRepository messageRepository, IUserService userService, IEmailService emailService)
        {
            _logger = logger;
            _configuration = configuration;
            _messageRepository = messageRepository;
            _userService = userService;
            _emailService = emailService;

            //Read configuration using IOptions
            //_newBookAlertConfiguration = newBookAlertConfiguration.Value;

            //Get configuration using named options
            _newBookAlertConfiguration = newBookAlertConfiguration.Get("InternalBook");
            _thirdPartyBookConfiguration = newBookAlertConfiguration.Get("ThirdPartyBook");
        }

        [Route("~/")]
        //Token replacement route method
        //[Route("[controller]/[action]")]
        public IActionResult Index()
        {
            //UserEmailOptions options = new UserEmailOptions
            //{
            //    ToEmails = new List<string>() { "test@gmail.com" },
            //    Placeholders = new List<KeyValuePair<string, string>>() 
            //    { 
            //        new KeyValuePair<string, string>("{{Username}}", "Nemo") 
            //    }

            //};

            //await _emailService.SendTestEmail(options);

            //Read data from appsettings.json file
            //var result = _configuration["AppName"];
            //var key1 = _configuration["infoObj:key1"];
            //var key2 = _configuration["infoObj:key2"];
            //var key3 = _configuration["infoObj:key3Obj:key3Name"];

            //Read data from appsettings.json using GetValue method
            //var result = _configuration.GetValue<bool>("NewBookAlert:DisplayNewBookAlert");
            //var result1 = _configuration.GetValue<string>("NewBookAlert:AlertMessage");

            //Read data from appsettings.json using GetSection method
            //var newBookAlert = _configuration.GetSection("NewBookAlert");
            //var result = newBookAlert.GetValue<bool>("DisplayNewBookAlert");
            //var alertMessage = newBookAlert.GetValue<string>("AlertMessage");

            //Using GetSection and GetValue in one line
            //var result = _configuration.GetSection("NewBookAlert").GetValue<bool>("DisplayNewBookAlert");

            //Binding configuration to objects
            //var newBookAlert = new NewBookAlertConfig();
            //_configuration.Bind("NewBookAlert", newBookAlert);


            //UserService method example
            //var userId = _userService.GetUserId();
            //var isLoggedIn = _userService.IsAuthenticated();

            var alert1 = _newBookAlertConfiguration.AlertMessage; 
            var alert2 = _thirdPartyBookConfiguration.AlertMessage; 

            var message = _messageRepository.GetName();

            return View();
        }

        [HttpGet]
        //Example of routing constraints
        //[Route("privacy/{name:alpha:minlength(5)}", Name = "privacy")]
        [Route("privacy")]
        public IActionResult Privacy(string name)
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        [Route("contactus")]
        public IActionResult ContactUs()
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
