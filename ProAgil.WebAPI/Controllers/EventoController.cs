using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProAgil.Domain;
using ProAgil.Repository;
using ProAgil.WebAPI.DTOs;
using System;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProAgil.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventoController : ControllerBase
    {
        private readonly IProAgilRepository _repo;
        private readonly IMapper _mapper;

        public EventoController(IProAgilRepository repo, IMapper mapper)
        {
            this._repo = repo;
            this._mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                Evento[] eventos = await _repo.GetAllEventosAsync(true);
                var results = _mapper.Map<EventoDTO[]>(eventos);
                return Ok(results);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("{eventoId}")]
        public async Task<IActionResult> Get(int eventoId)
        {
            try
            {
                Evento evento = await _repo.GetEventoByIdAsync(eventoId, true);
                var results = _mapper.Map<EventoDTO>(evento);
                return Ok(results);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("getByTema{tema}")]
        public async Task<IActionResult> Get(string tema)
        {
            try
            {
                Evento[] evento = await _repo.GetEventosByTemaAsync(tema, true);
                var results = _mapper.Map<EventoDTO[]>(evento);
                return Ok(results);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(EventoDTO model)
        {
            try
            {
                Evento evento = _mapper.Map<Evento>(model);
                _repo.Add(evento);
                if (await _repo.SaveChangesAsync())
                {
                    return Created($"api/Evento/{evento.Id}", _mapper.Map<EventoDTO>(evento));
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
            return BadRequest();
        }

        [HttpPut("{EventoId}")]
        public async Task<IActionResult> Put(int EventoId, EventoDTO model)
        {
            try
            {
                var evento = await _repo.GetEventoByIdAsync(EventoId);
                if (evento == null) return NotFound();
                _mapper.Map(model, evento);
                _repo.Update(evento);
                if (await _repo.SaveChangesAsync())
                {
                    return Created($"api/Evento/{evento.Id}", _mapper.Map<EventoDTO>(evento));
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
            return BadRequest();
        }

        [HttpDelete("{EventoId}")]
        public async Task<IActionResult> Delete(int EventoId)
        {
            try
            {
                var evento = await _repo.GetEventoByIdAsync(EventoId);
                if (evento == null)
                    return NotFound();
                _repo.Delete(evento);
                if (await _repo.SaveChangesAsync())
                {
                    return Ok();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
            return BadRequest();
        }
 
        [HttpPost("upload")]
        public async Task<IActionResult> Upload()
        {
            try
            {
                string destination = Path.Combine(Directory.GetCurrentDirectory(), Path.Combine("Resources", "Images"));
                var file = Request.Form.Files[0];
                if(file.Length > 0)
                {
                    string fileName = file.FileName;
                    destination = Path.Combine(destination, fileName.Replace("\"", " ").Trim());
                    using (FileStream stream = new FileStream(destination, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                        
                    }

                }
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
   }
}