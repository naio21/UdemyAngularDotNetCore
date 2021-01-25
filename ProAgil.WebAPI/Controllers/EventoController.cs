using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProAgil.WebAPI.Model;

namespace ProAgil.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EventoController : ControllerBase
    {
        private readonly ILogger<EventoController> _logger;
        public EventoController(ILogger<EventoController> logger)
        {
            _logger = logger;
        }

        private Evento[] Eventos()
        {
            return new Evento[]
            {
                new Evento()
                {
                    EventoId = 1,
                    Tema = "Angular e .NET Core",
                    Lote = "1º Lote",
                    QtdPessoas = 250,
                    DataEvento = DateTime.Now.AddDays(2).ToString("dd/MM/yyyy")
                },
                new Evento()
                {
                    EventoId = 2,
                    Tema = "Angular e Suas Novidades",
                    Lote = "2º Lote",
                    QtdPessoas = 350,
                    DataEvento = DateTime.Now.AddDays(3).ToString("dd/MM/yyyy")
                }
            };
         }

        [HttpGet("{id}")]
        public ActionResult<Evento> GetAction(int id)
        {
            return Eventos().FirstOrDefault(x => x.EventoId == id);
        }


    }
}