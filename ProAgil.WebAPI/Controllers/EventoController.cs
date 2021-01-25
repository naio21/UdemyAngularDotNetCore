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
        private readonly DataContext _context;

        public EventoController(ILogger<EventoController> logger, DataContext context)
        {
            this._logger = logger;
            this._context = context;
        }

        private List<Evento> Eventos()
        {
            return _context.Eventos.ToList();
        }

        [HttpGet]
        public ActionResult<IEnumerable<Evento>> Get()
        {
            return Eventos();
        }

        [HttpGet("{id}")]
        public ActionResult<Evento> GetAction(int id)
        {
            return Eventos().FirstOrDefault(x => x.EventoId == id);
        }
    }
}