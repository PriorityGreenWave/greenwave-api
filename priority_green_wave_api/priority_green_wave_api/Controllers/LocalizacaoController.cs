using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using priority_green_wave_api.DTOs;
using priority_green_wave_api.Model;
using priority_green_wave_api.Repository;

namespace priority_green_wave_api.Controllers
{
    public class LocalizacaoController : BaseController
    {
        private readonly ILogger<LocalizacaoController> _logger;
        private readonly LocalizacaoRepository _localizacaoRepository;
        public LocalizacaoController(ILogger<LocalizacaoController> logger, LocalizacaoRepository localizacaoRepository)
        {
            _logger = logger;
            _localizacaoRepository = localizacaoRepository;
        }

        [HttpGet]
        [Route("ReadLocalizacao")]
        public IActionResult ReadLocalizacao([FromBody] Localizacao localizacao)
        {
            try
            {
                var retornoLocalizacao = _localizacaoRepository.Read(localizacao.Id);
                if (retornoLocalizacao.Id != -1)
                {
                    return Ok(retornoLocalizacao);
                }
                else
                {
                    return BadRequest(new ErrorReturnDTO()
                    {
                        Error = "Invalid localizacao ID!",
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
        [Route("CreateLocalizacao")]
        public IActionResult CreateLocalizacao([FromBody] Localizacao localizacao)
        {
            try
            {
                _localizacaoRepository.Create(localizacao);
                return Ok(new { msg = "localizacao created successfully!" });
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
        [Route("UpdateLocalizacao")]
        public IActionResult UpdateLocalizacao([FromBody] Localizacao localizacao)
        {
            try
            {
                var localizacaoAntigo = _localizacaoRepository.Read(localizacao.Id);
                if (localizacaoAntigo.Id != -1)
                {
                    Localizacao localizacaoNovo = new Localizacao();

                    localizacaoNovo.Id = localizacaoAntigo.Id;

                    localizacaoNovo.Logradouro = localizacao.Logradouro != localizacaoAntigo.Logradouro ? localizacao.Logradouro : localizacaoAntigo.Logradouro;
                    localizacaoNovo.Numero = localizacao.Numero != localizacaoAntigo.Numero ? localizacao.Numero : localizacaoAntigo.Numero;
                    localizacaoNovo.Bairro = localizacao.Bairro != localizacaoAntigo.Bairro ? localizacao.Bairro : localizacaoAntigo.Bairro;
                    localizacaoNovo.Regional = localizacao.Regional != localizacaoAntigo.Regional ? localizacao.Regional : localizacaoAntigo.Regional;
                    localizacaoNovo.Cidade = localizacao.Cidade != localizacaoAntigo.Cidade ? localizacao.Cidade : localizacaoAntigo.Cidade;
                    localizacaoNovo.Estado = localizacao.Estado != localizacaoAntigo.Estado ? localizacao.Estado : localizacaoAntigo.Estado;
                    localizacaoNovo.DistanciaSemaforo = localizacao.DistanciaSemaforo != localizacaoAntigo.DistanciaSemaforo ? localizacao.DistanciaSemaforo : localizacaoAntigo.DistanciaSemaforo;
                    localizacaoNovo.Latitude = localizacao.Latitude != localizacaoAntigo.Latitude ? localizacao.Latitude : localizacaoAntigo.Latitude;
                    localizacaoNovo.Longitude = localizacao.Longitude != localizacaoAntigo.Longitude ? localizacao.Longitude : localizacaoAntigo.Longitude;

                    _localizacaoRepository.Update(localizacaoNovo);
                    return Ok(localizacaoNovo);
                }
                else
                {
                    return BadRequest();
                }

            }
            catch (Exception ex)
            {
                _logger.LogError("Um erro ocorreu!", ex, localizacao);
                return this.StatusCode(StatusCodes.Status500InternalServerError, new ErrorReturnDTO()
                {
                    Error = "Um erro ocorreu!",
                    StatusCode = StatusCodes.Status500InternalServerError
                });
            }
        }

        [HttpDelete]
        [AllowAnonymous]
        [Route("DeleteLocalizacao")]
        public IActionResult DeleteLocalizacao([FromBody] Localizacao localizacao)
        {
            try
            {
                var localizacao_delete = _localizacaoRepository.Read(localizacao.Id);
                if (localizacao_delete.Id != -1)
                {
                    _localizacaoRepository.Delete(localizacao_delete);
                    return Ok(localizacao_delete);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Um erro ocorreu!", ex, localizacao);
                return this.StatusCode(StatusCodes.Status500InternalServerError, new ErrorReturnDTO()
                {
                    Error = "Um erro ocorreu!",
                    StatusCode = StatusCodes.Status500InternalServerError
                });
            }
        }
    }
}
