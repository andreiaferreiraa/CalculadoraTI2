using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CalculadoraCompleta.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        [HttpGet] //facultativo, porque por defeito é sempre
        public ActionResult Index()
        {
            //inicialização dos primeiros valores da calculadora
            Session["primeiroOperador"] = true;
            Session["iniciaOperando"] = true;

            ViewBag.Display = 0;
            return View();

        }
        //POST:Home
        //string bt permite ler o as variaveis introduzidas pelo utilizador
        [HttpPost]
        public ActionResult Index(string bt, string display )
        {
        

            //avaliar o valor atribuido à variavel 'bt'
            switch (bt)
            {
                case "1":
                case "2":
                case "3":
                case "4":
                case "5":
                case "6":
                case "7":
                case "8":
                case "9":
                case "0":
                    if ((bool)Session["iniciaOperando"] || display.Equals("0"))
                        display = bt;
                    else
                        display = display + bt;
                    Session["iniciaOperando"] = false;
                    break;

                case "+/-":
                    display = Convert.ToDouble(display) * (-1) + "";
                    break;

                case ",":
                    if (!display.Contains(","))
                        display += ",";
                    break;

                case "+":
                case "-":
                case "x":
                case ":":
                case "=":
                    //se é a primeira vez que carrego num operador
                    if (!(bool)Session["primeiroOperador"])
                    {
                        //guardar o valor do 1º operando
                        Session["primeiroOperando"] = display;
                        //limpar o display
                        Session["iniciaOperando"] = true;
                        if (bt.Equals("="))
                        {
                            //marcar o operador como primeiro operador
                            Session["primeiroOperador"] = true;
                        }
                        else
                        {
                            //guardar o valor do operador
                            Session["operadorAnterior"] = bt;
                            Session["primeiroOperador"] = false;
                        }
                        //marcar o display para o reinicio
                        Session["iniciaOperando"] = true;

                        break;
                    }
                    else
                    {
                        //recuperar os valores dos operandos
                        double operando1 = Convert.ToDouble((string)Session["primeiroOperando"]);
                        double operando2 = Convert.ToDouble(display);
                        switch (Session["operadorAnterior"])
                        {

                            case "+":
                                display = operando1 + operando2 + "";
                                break;
                            case "-":
                                display = operando1 - operando2 + "";
                                break;
                            case "x":
                                display = operando1 * operando2 + "";
                                break;
                            case ":":
                                display = operando1 / operando2 + "";
                                break;
                        }
                        //guardar os dados dos display para utilização futura
                        Session["primeiroOperando"] = display;
                        Session["operadorAnterior"] = bt;
                        Session["iniciaOperando"] = true;

                    }//else

                    break;

            }

            
            ViewBag.Display = display;

            return View();
        }
    }
}