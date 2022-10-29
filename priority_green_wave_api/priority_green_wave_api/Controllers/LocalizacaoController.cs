using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using priority_green_wave_api.DTOs;
using priority_green_wave_api.Model;
using priority_green_wave_api.Repository;

namespace priority_green_wave_api.Controllers
{
    [ApiController]
    [Route("api/[controller]/")]
    public class LocalizacaoController : BaseController
    {
        private readonly ILogger<LocalizacaoController> _logger;
        private readonly LocalizacaoRepository _localizacaoRepository;
        private readonly SemaforoRepository _semaforoRepository;
        private readonly CatadioptricoRepository _catadioptricoRepository;
        private readonly LocalizacaoCatadioptricoRepository _localizacaoCatadioptricoRepository;
        private readonly LocalizacaoSemaforoRepository _localizacaoSemaforoRepository;
        public LocalizacaoController(ILogger<LocalizacaoController> logger, LocalizacaoRepository localizacaoRepository, LocalizacaoCatadioptricoRepository localizacaoCatadioptricoRepository, LocalizacaoSemaforoRepository localizacaoSemaforoRepository, SemaforoRepository semaforoRepository, CatadioptricoRepository catadioptricoRepository)
        {
            _logger = logger;
            _localizacaoRepository = localizacaoRepository;
            _localizacaoCatadioptricoRepository = localizacaoCatadioptricoRepository;
            _localizacaoSemaforoRepository = localizacaoSemaforoRepository;
            _semaforoRepository = semaforoRepository;
            _catadioptricoRepository = catadioptricoRepository;
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("ReadLocalizacao")]
        public IActionResult ReadLocalizacao([FromQuery] int idlocalizacao)
        {
            try
            {
                var retornoLocalizacao = _localizacaoRepository.Read(idlocalizacao);
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
        public IActionResult CreateLocalizacao([FromBody] LocalizacaoRequestDTO localizacao)
        {
            try
            {
                Localizacao localizacaoNovo = new Localizacao();
                localizacaoNovo.Logradouro = localizacao.Logradouro;
                localizacaoNovo.Numero = localizacao.Numero;
                localizacaoNovo.Bairro = localizacao.Bairro;
                localizacaoNovo.Regional = localizacao.Regional;
                localizacaoNovo.Area = localizacao.Area;
                localizacaoNovo.Cidade = localizacao.Cidade;
                localizacaoNovo.Estado = localizacao.Estado;
                localizacaoNovo.DistanciaSemaforo = localizacao.DistanciaSemaforo;
                localizacaoNovo.Latitude = localizacao.Latitude;
                localizacaoNovo.Longitude = localizacao.Longitude;

                int idLocalizacao = _localizacaoRepository.Create(localizacaoNovo);
                return Ok(localizacaoNovo);
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
        public IActionResult DeleteLocalizacao([FromQuery] int IdLocalizacao)
        {
            try
            {
                var localizacaoRequestDelete = _localizacaoRepository.Read(IdLocalizacao);

                if (localizacaoRequestDelete.Id != -1)
                {
                    var localizacaoCatadioptrico = _localizacaoCatadioptricoRepository.ReadListaLocalizacao(IdLocalizacao);

                    if(localizacaoCatadioptrico.Count() > 0)
                    {
                        foreach (LocalizacaoCatadioptrico localizacao in localizacaoCatadioptrico)
                        {
                            var catadioptrico = _catadioptricoRepository.Read(localizacao.IdCatadioptrico);
                            _catadioptricoRepository.Delete(catadioptrico);
                            _localizacaoCatadioptricoRepository.Delete(localizacao);
                        }
                    }

                    var localizacaoSemaforo = _localizacaoSemaforoRepository.ReadListaLocalizacao(IdLocalizacao);

                    if (localizacaoSemaforo.Count() > 0)
                    {
                        foreach (LocalizacaoSemaforo localizacao in localizacaoSemaforo)
                        {
                            var semaforo = _semaforoRepository.Read(localizacao.IdSemaforo);
                            _semaforoRepository.Delete(semaforo);
                            _localizacaoSemaforoRepository.Delete(localizacao);
                        }
                    }

                    _localizacaoRepository.Delete(localizacaoRequestDelete);
                    return Ok(localizacaoRequestDelete);    
                }
                else
                {
                    return BadRequest("Usuário não encontrado!");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Um erro ocorreu!", ex, IdLocalizacao);
                return this.StatusCode(StatusCodes.Status500InternalServerError, new ErrorReturnDTO()
                {
                    Error = "Um erro ocorreu!",
                    StatusCode = StatusCodes.Status500InternalServerError
                });
            }
        }
    }
}
