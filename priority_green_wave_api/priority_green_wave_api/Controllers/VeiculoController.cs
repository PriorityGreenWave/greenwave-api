using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using priority_green_wave_api.DTOs;
using priority_green_wave_api.Model;
using priority_green_wave_api.Repository;

namespace priority_green_wave_api.Controllers
{
    public class VeiculoController : BaseController
    {
        private readonly ILogger<VeiculoController> _logger;
        private readonly VeiculoRepository _veiculoRepository;
        public VeiculoController(ILogger<VeiculoController> logger, VeiculoRepository veiculoRepository)
        {
            _logger = logger;
            _veiculoRepository = veiculoRepository;
        }

        [HttpGet]
        [Route("ReadVeiculo")]
        public IActionResult ReadVeiculo([FromBody] Veiculo veiculo)
        {
            try
            {
                var retornoVeiculo = _veiculoRepository.Read(veiculo.Id);
                if (retornoVeiculo.Id != -1)
                {
                    return Ok(retornoVeiculo);
                }
                else
                {
                    return BadRequest(new ErrorReturnDTO()
                    {
                        Error = "Invalid veiculo ID!",
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
        [Route("CreateVeiculo")]
        public IActionResult CreateVeiculo([FromBody] VeiculoDTO veiculo)
        {
            try
            {
                _veiculoRepository.Create(new Veiculo(veiculo));
                return Ok(new { msg = "veiculo created successfully!" });
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
        [Route("UpdateVeiculo")]
        public IActionResult UpdateVeiculo([FromBody] VeiculoDTO veiculo)
        {
            try
            {
                var veiculoAntigo = _veiculoRepository.Read(veiculo.Id);
                if (veiculoAntigo.Id != -1)
                {
                    Veiculo veiculoNovo = new Veiculo();

                    veiculoNovo.Id = veiculoAntigo.Id;
                    veiculoNovo.Placa = veiculo.Placa != veiculoAntigo.Placa ? veiculo.Placa : veiculoAntigo.Placa;
                    veiculoNovo.Rfid = veiculo.Rfid != veiculoAntigo.Rfid ? veiculo.Rfid : veiculoAntigo.Rfid;
                    veiculoNovo.Fabricante = veiculo.Fabricante != veiculoAntigo.Fabricante ? veiculo.Fabricante : veiculoAntigo.Fabricante;
                    veiculoNovo.Modelo = veiculo.Modelo != veiculoAntigo.Modelo ? veiculo.Modelo : veiculoAntigo.Modelo;
                    veiculoNovo.Ano = veiculo.Ano != veiculoAntigo.Ano ? veiculo.Ano : veiculoAntigo.Ano;
                    veiculoNovo.TipoVeiculo = veiculo.TipoVeiculo != veiculoAntigo.TipoVeiculo ? veiculo.TipoVeiculo : veiculoAntigo.TipoVeiculo;
                    veiculoNovo.VeiculoEmergencia = veiculo.VeiculoEmergencia != veiculoAntigo.VeiculoEmergencia ? veiculo.VeiculoEmergencia : veiculoAntigo.VeiculoEmergencia;
                    veiculoNovo.EstadoEmergencia = veiculo.EstadoEmergencia != veiculoAntigo.EstadoEmergencia ? veiculo.EstadoEmergencia : veiculoAntigo.EstadoEmergencia;

                    _veiculoRepository.Update(veiculoNovo);
                    return Ok(veiculoNovo);
                }
                else
                {
                    return BadRequest();
                }

            }
            catch (Exception ex)
            {
                _logger.LogError("An error ocurred!", ex, veiculo);
                return this.StatusCode(StatusCodes.Status500InternalServerError, new ErrorReturnDTO()
                {
                    Error = "An login error ocurred!",
                    StatusCode = StatusCodes.Status500InternalServerError
                });
            }
        }

        [HttpDelete]
        [AllowAnonymous]
        [Route("DeleteVeiculo")]
        public IActionResult DeleteVeiculo([FromBody] VeiculoDTO veiculo)
        {
            try
            {
                var veiculo_delete = _veiculoRepository.Read(veiculo.Id);
                if (veiculo_delete.Id != -1)
                {
                    _veiculoRepository.Delete(veiculo_delete);
                    return Ok(veiculo_delete);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("An error ocurred!", ex, veiculo);
                return this.StatusCode(StatusCodes.Status500InternalServerError, new ErrorReturnDTO()
                {
                    Error = "An login error ocurred!",
                    StatusCode = StatusCodes.Status500InternalServerError
                });
            }
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("EstadoEmergencia")]
        public IActionResult EstadoEmergencia([FromBody] string Rfid)
        {
            try
            {
                var veiculo = _veiculoRepository.ReadRfid(Rfid);
                if (veiculo.Id != -1)
                {
                    return Ok(veiculo.EstadoEmergencia);
                }
                else
                {
                    return Ok(false);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("An error ocurred!", ex);
                return this.StatusCode(StatusCodes.Status500InternalServerError, new ErrorReturnDTO()
                {
                    Error = "An login error ocurred!",
                    StatusCode = StatusCodes.Status500InternalServerError
                });
            }
        }
    }
}
