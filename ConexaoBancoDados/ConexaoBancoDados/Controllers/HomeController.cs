using ConexaoBancoDados.Context;
using ConexaoBancoDados.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ConexaoBancoDados.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var context = new ContextOracle();
            string query = "select * from TB_TESTE";
            var tabTeste = context.execultaConsultas(System.Data.CommandType.Text, query);
            foreach (DataRow rs in tabTeste.Rows)
            {
                int id = (DBNull.Value != rs["ID"]) ? Convert.ToInt32(rs["ID"]) : 0;
            }


            return View();
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
