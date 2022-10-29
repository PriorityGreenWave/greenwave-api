using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using priority_green_wave_api.DTOs;
using priority_green_wave_api.Model;
using priority_green_wave_api.Repository;
using priority_green_wave_api.Services;
using priority_green_wave_api.Utils;
using System.Net.Http.Headers;
using System.Text.RegularExpressions;
namespace priority_green_wave_api.Controllers
{
    [ApiController]
    [Route("api/[controller]/")]
    public class CatadioptricoController : BaseController
    {
        private readonly ILogger<CatadioptricoController> _logger;
        private readonly CatadioptricoRepository _catadioptricoRepository;
        private readonly LocalizacaoCatadioptricoRepository _localizacaoCatadioptricoRepository;
        private readonly LocalizacaoRepository _localizacaoRepository;
        public CatadioptricoController(ILogger<CatadioptricoController> logger, CatadioptricoRepository catadioptricoRepository, LocalizacaoCatadioptricoRepository localizacaoCatadioptricoRepository, LocalizacaoRepository localizacaoRepository)
        {
            _logger = logger;
            _catadioptricoRepository = catadioptricoRepository;
            _localizacaoCatadioptricoRepository = localizacaoCatadioptricoRepository;
            _localizacaoRepository = localizacaoRepository;
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("ReadCatadioptrico")]
        public IActionResult ReadCatadioptrico([FromQuery] int idcatadioptrico)
        {
            try
            {
                var retornoCatadioptrico = _catadioptricoRepository.Read(idcatadioptrico);
                if(retornoCatadioptrico.Id != -1)
                {
                    return Ok(retornoCatadioptrico);
                }
                else
                {
                    return BadRequest(new ErrorReturnDTO()
                    {
                        Error = "Invalid catadioptrico ID!",
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
        [Route("CreateCatadioptrico")]
        public IActionResult CreateCatadioptrico([FromBody] CatadioptricoRequestDTO catadioptricoRequest)
        {
            try
            {
                
                Catadioptrico novoCatadioptrico = new Catadioptrico();
                LocalizacaoCatadioptrico novalocalizacaoCatadioptrico = new LocalizacaoCatadioptrico();
              
                var localizacao = _localizacaoRepository.Read(catadioptricoRequest.IdLocalizacao);
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
                    novoCatadioptrico.Nome = catadioptricoRequest.Nome;
                    
                    novalocalizacaoCatadioptrico.IdCatadioptrico = _catadioptricoRepository.Create(novoCatadioptrico);
                    novalocalizacaoCatadioptrico.IdLocalizacao = localizacao.Id;

                    _localizacaoCatadioptricoRepository.Create(novalocalizacaoCatadioptrico);
                    return Ok(novalocalizacaoCatadioptrico);
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
        [Route("UpdateCatadioptrico")]
        public IActionResult UpdateCatadioptrico([FromBody] CatadioptricoUpdateDTO catadioptricoUpdate)
        {
            try
            {
                var localizacao = _localizacaoRepository.Read(catadioptricoUpdate.IdLocalizacao);
                var catadioptricoAntigo = _catadioptricoRepository.Read(catadioptricoUpdate.Id);
                
                var erros = new List<string>();


                if (localizacao.Id == -1)
                {
                    erros.Add("Localização não cadastrada na base");
                }
                if(catadioptricoAntigo.Id == -1){
                    erros.Add("Catadioptrico não cadastrado na base");
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
                    var catadioptricoLocalizacaoAntigo = _localizacaoCatadioptricoRepository.ReadLocalizacaoCatadioptrico(catadioptricoUpdate.Id);
                    Catadioptrico catadioptricoNovo = new Catadioptrico();
                    LocalizacaoCatadioptrico localizacaoCatadioptricoNovo = new LocalizacaoCatadioptrico();

                    localizacaoCatadioptricoNovo.Id = catadioptricoLocalizacaoAntigo.Id;
                    localizacaoCatadioptricoNovo.IdCatadioptrico = catadioptricoLocalizacaoAntigo.IdCatadioptrico;
                    localizacaoCatadioptricoNovo.IdLocalizacao = catadioptricoLocalizacaoAntigo.IdLocalizacao;

                    if (catadioptricoLocalizacaoAntigo.IdLocalizacao != localizacao.Id)
                    {

                        localizacaoCatadioptricoNovo.IdLocalizacao = localizacao.Id;
                        _localizacaoCatadioptricoRepository.Update(localizacaoCatadioptricoNovo);
                    }
                        
                    catadioptricoNovo.Id = catadioptricoAntigo.Id;
                    catadioptricoNovo.Nome = catadioptricoUpdate.Nome;
                              
                    _catadioptricoRepository.Update(catadioptricoNovo);
                    return Ok(localizacaoCatadioptricoNovo);                
                }
                
                
            }
            catch (Exception ex)
            {
                _logger.LogError("Um erro ocorreu!", ex, catadioptricoUpdate);
                return this.StatusCode(StatusCodes.Status500InternalServerError, new ErrorReturnDTO()
                {
                    Error = "Um erro ocorreu!",
                    StatusCode = StatusCodes.Status500InternalServerError
                });
            }
        }

        [HttpDelete]
        [AllowAnonymous]
        [Route("DeleteCatadioptrico")]
        public IActionResult DeleteCatadioptrico([FromQuery] int idcatadioptrico)
        {
            try
            {
                var catadioptrico_delete = _catadioptricoRepository.Read(idcatadioptrico);
                if (catadioptrico_delete.Id != -1)
                {
                    var localizacaoCatadioptrico = _localizacaoCatadioptricoRepository.ReadCatadioptrico(idcatadioptrico);
                    _localizacaoCatadioptricoRepository.Delete(localizacaoCatadioptrico);        
                    _catadioptricoRepository.Delete(catadioptrico_delete);
                    return Ok(catadioptrico_delete);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Um erro ocorreu!", ex, idcatadioptrico);
                return this.StatusCode(StatusCodes.Status500InternalServerError, new ErrorReturnDTO()
                {
                    Error = "Um erro ocorreu!",
                    StatusCode = StatusCodes.Status500InternalServerError
                });
            }
        }
    }
}
