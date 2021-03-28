using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProAgil.Domain;
using ProAgil.Repository;
using System;
using System.IO;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace ProAgil.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PalestranteController : ControllerBase
    {
        private readonly IProAgilRepository _repo;

        public PalestranteController(IProAgilRepository repo)
        {
            this._repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                return Ok(await _repo.GetAllPalestrantesAsync(true));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
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
                    string fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName;
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

        [HttpGet("{palestranteId}")]
        public async Task<IActionResult> Get(int palestranteId)
        {
            try
            {
                return Ok(await _repo.GetPalestranteByIdAsync(palestranteId, true));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("getByName{tema}")]
        public async Task<IActionResult> Get(string nome)
        {
            try
            {
                return Ok(await _repo.GetPalestrantesByNameAsync(nome, true));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(Palestrante model)
        {
            try
            {
                _repo.Add(model);
                if (await _repo.SaveChangesAsync())
                {
                    return Created($"api/Palestrante/{model.Id}", model);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
            return BadRequest();
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromQuery] int PalestranteId, [FromBody] Palestrante model)
        {
            try
            {
                var palestrante = await _repo.GetPalestranteByIdAsync(PalestranteId);
                if (palestrante == null)
                    return NotFound();
                _repo.Update(model);
                if (await _repo.SaveChangesAsync())
                {
                    return Created($"api/Palestrante/{model.Id}", model);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
            return BadRequest();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int PalestranteId)
        {
            try
            {
                var palestrante = await _repo.GetPalestranteByIdAsync(PalestranteId);
                if (palestrante == null)
                    return NotFound();
                _repo.Delete(palestrante);
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
    }
}