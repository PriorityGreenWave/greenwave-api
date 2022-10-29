using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using priority_green_wave_api.DTOs;
using priority_green_wave_api.Model;
using priority_green_wave_api.Repository;

namespace priority_green_wave_api.Controllers
{
    [ApiController]
    [Route("api/[controller]/")]
    public class SemaforoController : BaseController
    {
        private readonly ILogger<SemaforoController> _logger;
        private readonly SemaforoRepository _semaforoRepository;
        private readonly LocalizacaoRepository _localizacaoRepository;
        private readonly LocalizacaoSemaforoRepository _localizacaoSemaforoRepository;
        public SemaforoController(ILogger<SemaforoController> logger, SemaforoRepository semaforoRepository, LocalizacaoRepository localizacaoRepository, LocalizacaoSemaforoRepository localizacaoSemaforoRepository)
        {
            _logger = logger;
            _semaforoRepository = semaforoRepository ?? throw new ArgumentNullException(nameof(semaforoRepository));
            _localizacaoRepository = localizacaoRepository;
            _localizacaoSemaforoRepository = localizacaoSemaforoRepository;
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("ReadSemaforo")]
        public IActionResult ReadSemaforo([FromQuery] int idsemaforo)
        {
            try
            {
                var retornoSemaforo = _semaforoRepository.Read(idsemaforo);
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
        public IActionResult CreateSemaforo([FromBody] SemaforoRequestDTO semaforoRequest)
        {
            try
            {
                Semaforo novoSemaforo = new Semaforo();
                LocalizacaoSemaforo novalocalizacaoSemaforo = new LocalizacaoSemaforo();

                var localizacao = _localizacaoRepository.Read(semaforoRequest.IdLocalizacao);
                var erros = new List<string>();
                if (localizacao.Id == -1)
                {
                    erros.Add("Localização não cadastrada na base");
                }
                if (erros.Count > 0)
                {
                    return BadRequest(new ErrorReturnDTO()
                    {
                        Errors = erros,
                        StatusCode = StatusCodes.Status400BadRequest
                    });
                }
                else
                {
                    novoSemaforo.Nome = semaforoRequest.Nome;

                    novalocalizacaoSemaforo.IdSemaforo = _semaforoRepository.Create(novoSemaforo);
                    novalocalizacaoSemaforo.IdLocalizacao = localizacao.Id;

                    _localizacaoSemaforoRepository.Create(novalocalizacaoSemaforo);
                    return Ok(novalocalizacaoSemaforo);
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

        [HttpPut]
        [AllowAnonymous]
        [Route("UpdateSemaforo")]
        public IActionResult UpdateSemaforo([FromBody] SemaforoUpdateDTO semaforoUpdate)
        {
            try
            {
                var localizacao = _localizacaoRepository.Read(semaforoUpdate.IdLocalizacao);
                var semaforoAntigo = _semaforoRepository.Read(semaforoUpdate.Id);

                var erros = new List<string>();


                if (localizacao.Id == -1)
                {
                    erros.Add("Localização não cadastrada na base");
                }
                if (semaforoAntigo.Id == -1)
                {
                    erros.Add("Semaforo não cadastrado na base");
                }
                if (erros.Count > 0)
                {
                    return BadRequest(new ErrorReturnDTO()
                    {
                        Errors = erros,
                        StatusCode = StatusCodes.Status400BadRequest
                    });
                }
                else
                {
                    var semaforoLocalizacaoAntigo = _localizacaoSemaforoRepository.ReadLocalizacaoSemaforo(semaforoUpdate.Id);
                    
                    Semaforo semaforoNovo = new Semaforo();
                    LocalizacaoSemaforo localizacaoSemaforoNovo = new LocalizacaoSemaforo();

                    localizacaoSemaforoNovo.Id = semaforoLocalizacaoAntigo.Id;
                    localizacaoSemaforoNovo.IdSemaforo = semaforoLocalizacaoAntigo.IdSemaforo;
                    localizacaoSemaforoNovo.IdLocalizacao = semaforoLocalizacaoAntigo.IdLocalizacao;

                    if (semaforoLocalizacaoAntigo.IdLocalizacao != localizacao.Id)
                    {

                        localizacaoSemaforoNovo.IdLocalizacao = localizacao.Id;
                        _localizacaoSemaforoRepository.Update(localizacaoSemaforoNovo);
                    }

                    semaforoNovo.Id = semaforoAntigo.Id;
                    semaforoNovo.Nome = semaforoUpdate.Nome;

                    _semaforoRepository.Update(semaforoNovo);
                    return Ok(localizacaoSemaforoNovo);
                }

            }
            catch (Exception ex)
            {
                _logger.LogError("Um erro ocorreu!", ex, semaforoUpdate);
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
        public IActionResult DeleteSemaforo([FromQuery] int idsemaforo)
        {
            try
            {
                var semaforo_delete = _semaforoRepository.Read(idsemaforo);
                if (semaforo_delete.Id != -1)
                {
                    var localizacaoSemaforo = _localizacaoSemaforoRepository.ReadSemaforo(idsemaforo);
                    _localizacaoSemaforoRepository.Delete(localizacaoSemaforo);      
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
                _logger.LogError("Um erro ocorreu!", ex, idsemaforo);
                return this.StatusCode(StatusCodes.Status500InternalServerError, new ErrorReturnDTO()
                {
                    Error = "Um erro ocorreu!",
                    StatusCode = StatusCodes.Status500InternalServerError
                });
            }
        }
    }
}
