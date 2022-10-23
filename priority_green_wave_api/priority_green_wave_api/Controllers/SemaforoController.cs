using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using priority_green_wave_api.DTOs;
using priority_green_wave_api.Model;
using priority_green_wave_api.Repository;

namespace priority_green_wave_api.Controllers
{
    public class SemaforoController : BaseController
    {
        private readonly ILogger<SemaforoController> _logger;
        private readonly SemaforoRepository _semaforoRepository;
        public SemaforoController(ILogger<SemaforoController> logger, SemaforoRepository semaforoRepository)
        {
            _logger = logger;
            _semaforoRepository = semaforoRepository ?? throw new ArgumentNullException(nameof(semaforoRepository));
        }

        [HttpGet]
        [Route("ReadSemaforo")]
        public IActionResult ReadSemaforo([FromBody] Semaforo semaforo)
        {
            try
            {
                var retornoSemaforo = _semaforoRepository.Read(semaforo.Id);
                if (retornoSemaforo.Id != -1)
                {
                    return Ok(retornoSemaforo);
                }
                else
                {
                    return BadRequest(new ErrorReturnDTO()
                    {
                        Error = "Invalid semaforo ID!",
                        StatusCode = StatusCodes.Status400BadRequest
                    });
                }

            }
            catch (Exception ex)
            {
                _logger.LogError("Um erro ocorreu!", ex);
                return this.StatusCode(StatusCodes.Status500InternalServerError, new ErrorReturnDTO()
                {
                    Error = "Error!",
                    StatusCode = StatusCodes.Status500InternalServerError
                });
            }
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("CreateSemaforo")]
        public IActionResult CreateSemaforo([FromBody] Semaforo semaforo)
        {
            try
            {
                _semaforoRepository.Create(semaforo);
                return Ok(new { msg = "semaforo created successfully!" });
            }
            catch (Exception ex)
            {
                _logger.LogError("Um erro ocorreu!", ex);
                return this.StatusCode(StatusCodes.Status500InternalServerError, new ErrorReturnDTO()
                {
                    Error = "Error!",
                    StatusCode = StatusCodes.Status500InternalServerError
                });
            }
        }

        [HttpPut]
        [AllowAnonymous]
        [Route("UpdateSemaforo")]
        public IActionResult UpdateSemaforo([FromBody] Semaforo semaforo)
        {
            try
            {
                var semaforoAntigo = _semaforoRepository.Read(semaforo.Id);
                if (semaforoAntigo.Id != -1)
                {
                    Semaforo semaforoNovo = new Semaforo();

                    semaforoNovo.Id = semaforoAntigo.Id;
                    semaforoNovo.IdLocalizacao = semaforo.IdLocalizacao != semaforoAntigo.IdLocalizacao ? semaforo.IdLocalizacao : semaforoAntigo.IdLocalizacao;
                    semaforoNovo.Nome = semaforo.Nome != semaforoAntigo.Nome ? semaforo.Nome : semaforoAntigo.Nome;

                    _semaforoRepository.Update(semaforoNovo);
                    return Ok(semaforoNovo);
                }
                else
                {
                    return BadRequest();
                }

            }
            catch (Exception ex)
            {
                _logger.LogError("Um erro ocorreu!", ex, semaforo);
                return this.StatusCode(StatusCodes.Status500InternalServerError, new ErrorReturnDTO()
                {
                    Error = "Um erro ocorreu!",
                    StatusCode = StatusCodes.Status500InternalServerError
                });
            }
        }

        [HttpDelete]
        [AllowAnonymous]
        [Route("DeleteSemaforo")]
        public IActionResult DeleteSemaforo([FromBody] Semaforo semaforo)
        {
            try
            {
                var semaforo_delete = _semaforoRepository.Read(semaforo.Id);
                if (semaforo_delete.Id != -1)
                {
                    _semaforoRepository.Delete(semaforo_delete);
                    return Ok(semaforo_delete);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Um erro ocorreu!", ex, semaforo);
                return this.StatusCode(StatusCodes.Status500InternalServerError, new ErrorReturnDTO()
                {
                    Error = "Um erro ocorreu!",
                    StatusCode = StatusCodes.Status500InternalServerError
                });
            }
        }
    }
}
