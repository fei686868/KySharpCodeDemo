using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using demo.Model;
using Microsoft.AspNetCore.Mvc;
using demo.Models;
using KYSharpCore.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace demo.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRepository<Users, string> _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        public HomeController(IRepository<Users, string> userRepository, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Index()
        {
            //查询出用户
            var user = await _userRepository.Query(m => m.Id == "b29bf6f3e8144cb59952ab4900fd042a").FirstOrDefaultAsync();
            if (user != null)
            {
                user.Name = "段誉";
                //执行提交
                await _userRepository.UpdateAsync(user);
            }

            return Content("Hello World");
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
