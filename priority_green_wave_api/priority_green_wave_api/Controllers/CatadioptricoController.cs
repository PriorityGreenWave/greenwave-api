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
    public class CatadioptricoController : ControllerBase
    {
        private readonly ILogger<CatadioptricoController> _logger;
        private readonly CatadioptricoRepository _catadioptricoRepository;
        public CatadioptricoController(ILogger<CatadioptricoController> logger, CatadioptricoRepository catadioptricoRepository)
        {
            _logger = logger;
            _catadioptricoRepository = catadioptricoRepository;
        }

        [HttpGet]
        [Route("ReadCatadioptrico")]
        public IActionResult ReadCatadioptrico([FromBody] Catadioptrico catadioptrico)
        {
            try
            {
                var retornoCatadioptrico = _catadioptricoRepository.Read(catadioptrico.Id);
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
                _logger.LogError("An error ocurred!", ex);
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
        public IActionResult CreateCatadioptrico([FromBody] CatadioptricoDTO catadioptrico)
        {
            try
            {
                _catadioptricoRepository.Create(new Catadioptrico(catadioptrico));
                return Ok(new { msg = "catadioptrico created successfully!" });
            }
            catch (Exception ex)
            {
                _logger.LogError("An error ocurred!", ex);
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
        public IActionResult UpdateCatadioptrico([FromBody] CatadioptricoDTO catadioptrico)
        {
            try
            {
                var catadioptricoAntigo = _catadioptricoRepository.Read(catadioptrico.Id);
                if(catadioptricoAntigo.Id != -1)
                {
                    Catadioptrico catadioptricoNovo = new Catadioptrico();

                    catadioptricoNovo.Id = catadioptricoAntigo.Id;
                    catadioptricoNovo.Localizacao.Id = catadioptrico.Id != catadioptricoAntigo.Localizacao.Id ? catadioptrico.Id : catadioptricoAntigo.Localizacao.Id;
                    _catadioptricoRepository.Update(catadioptricoNovo);
                    return Ok(catadioptricoNovo);
                }
                else
                {
                    return BadRequest();
                }
                
            }
            catch (Exception ex)
            {
                _logger.LogError("An error ocurred!", ex, catadioptrico);
                return this.StatusCode(StatusCodes.Status500InternalServerError, new ErrorReturnDTO()
                {
                    Error = "An login error ocurred!",
                    StatusCode = StatusCodes.Status500InternalServerError
                });
            }
        }

        [HttpDelete]
        [AllowAnonymous]
        [Route("DeleteCatadioptrico")]
        public IActionResult DeleteCatadioptrico([FromBody] CatadioptricoDTO catadioptrico)
        {
            try
            {
                var catadioptrico_delete = _catadioptricoRepository.Read(catadioptrico.Id);
                if (catadioptrico_delete.Id != -1)
                {
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
                _logger.LogError("An error ocurred!", ex, catadioptrico);
                return this.StatusCode(StatusCodes.Status500InternalServerError, new ErrorReturnDTO()
                {
                    Error = "An login error ocurred!",
                    StatusCode = StatusCodes.Status500InternalServerError
                });
            }
        }
    }
}
