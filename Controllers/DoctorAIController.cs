using Microsoft.AspNetCore.Mvc;
using DiagnosticoAI.Models;
using RestSharp;
using DiagnosticoAI.Recursos.ChatGPT;
using Newtonsoft.Json;

namespace DiagnosticoAI.Controllers
{
    public class DoctorAIController : Controller
    {
        public static string _EndPoint = "https://api.openai.com/";
        public static string _URI = "v1/chat/completions";
        public static string _APIKey = "Your_APIKEY_HERE";

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(Paciente paciente)
        {
            string pSolicitud = "Hola, funcionas?";
            var strRespuesta = "";

            //Consumir api:
            var oCliente = new RestClient(_EndPoint);
            var oSolicitud = new RestRequest(_URI, Method.Post);

            oSolicitud.AddHeader("Content-Type","application/json");
            oSolicitud.AddHeader("Authorization", "Bearer "+_APIKey);

            //Creamos el cuerpo de la solicitud

            var oCuerpo = new Request()
            {
                model = "gpt-3.5-turbo-0613",
                messages = new List<Message>()
                {
                    new Message()
                    {
                        role = "user",
                        content = pSolicitud
                    }
                }
            };

            var jsonString = JsonConvert.SerializeObject(oCuerpo);

            oSolicitud.AddJsonBody(jsonString);

            //Generar la respuestas
            var oRespuesta = oCliente.Post<Response>(oSolicitud);

            strRespuesta = oRespuesta.choices[0].message.content;

            ViewBag.Respuesta = strRespuesta;

            return View("Conversar");
        }

        public ActionResult Conversar(string pSolicitud, string submitButton)
        {
            var strRespuesta = "";

            if (submitButton == "Generar") {
                //Consumir api:
                var oCliente = new RestClient(_EndPoint);
                var oSolicitud = new RestRequest(_URI, Method.Post);

                oSolicitud.AddHeader("Content-Type", "application/json");
                oSolicitud.AddHeader("Authorization", "Bearer " + _APIKey);

                //Creamos el cuerpo de la solicitud

                var oCuerpo = new Request()
                {
                    //-0613
                    model = "gpt-3.5-turbo",
                    messages = new List<Message>()
                {
                    new Message()
                    {
                        role = "user",
                        content = pSolicitud
                    }
                }
                };

                var jsonString = JsonConvert.SerializeObject(oCuerpo);

                oSolicitud.AddJsonBody(jsonString);

                var oRespuesta = oCliente.Post<Response>(oSolicitud);

                strRespuesta = oRespuesta.choices[0].message.content;

                ViewBag.Respuesta = strRespuesta;
            }
            else if (submitButton == "Clear")
            {
                return View("Index", "DoctorAI");
            }

            

            return View();
        }
    }
}
