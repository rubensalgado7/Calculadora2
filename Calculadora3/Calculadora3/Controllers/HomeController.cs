using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Calculadora3.Models;

namespace Calculadora.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// apresentação inicial da view no browser 
        /// </summary>
        /// <returns>View</returns>
        public IActionResult Index()
        {
            //inicializar o valor do visor
            ViewBag.Visor = "0";
            return View();
        }


        /// <summary>
        /// apresentação da 'vista' da calculadora , qd a interação é efetuada em modo 'HTTP POST'
        /// </summary>
        /// <param name="visor">representção do operando a utilizar na operação,bem como a resposta após a execução</param>
        /// <param name="bt">valor do botão que foi premido</param>
        /// <param name="operando">valor do primeiro a usar na operação</param>
        /// <param name="operador">simbolo da operação a ser executada</param>
        /// <param name="limpaVisor">identifica se o visor deve ser limpo,ou nao</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Index(string visor,string bt,string operando,string operador,bool limpaVisor)
        {
           

            switch (bt)
            {
                case"1":
                case "2":
                case "3":
                case "4":
                case "5":
                case "6":
                case "7":
                case "8":
                case "9":
                case "0":
                    if (visor == "0" || limpaVisor==true) visor = bt;
                    else visor += bt;
                    limpaVisor = false;
                    break;
                case "+/-":
                    visor = Convert.ToDouble(visor) * (-1)+"";
                    //como o visor é uma string , a inversao poderia ser feita por manipulação de strings
                   // visor.StartsWith().ToString().Length
                    break;
                case ",":
                    if (!visor.Contains(bt)) visor += bt;
                    break;
                case "+":
                case "-":
                case ":":
                case "x":
                case "=":

                    //como guardar os dados do visor para ter "memoria"?
                    // é a primeira vez que escolho o operador?
                    if (operador != null)
                    {


                        //carreguei + do que uma vez num 'operador'
                        double primeiroOperando = Convert.ToDouble(operando);
                        double segundoOperando = Convert.ToDouble(visor);
                        switch (operador)
                        {
                            case "+":
                                visor = primeiroOperando + segundoOperando + "";
                                break;
                            case "-":
                                visor = primeiroOperando - segundoOperando + "";
                                break;
                            case ":":
                                visor = primeiroOperando / segundoOperando + "";
                                break;
                            case "x":
                                visor = primeiroOperando * segundoOperando + "";
                                break;

                        }
                        if (!bt.Equals("="))
                        {
                            //garantir o efeito "memoria"
                            operando = visor;
                            operador = bt;
                            //como dar ordem para o visor reiniciar
                        }
                        else {
                            operando = null;
                            operador = null;
                        }
                        limpaVisor = true;
                    }
                break;

                case "C":
                    visor = "0";
                    operador = null;
                    operando = null;
                    limpaVisor = true;
                    break;

            }

            //enviar dados do Visor para a view
            ViewBag.Visor = visor;
            ViewBag.Operador = operador;
            ViewBag.Operando = operando;
            ViewBag.LimpaVisor = limpaVisor+"";

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
