using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using priority_green_wave_api.DTOs;
using priority_green_wave_api.Model;
using priority_green_wave_api.Repository;

namespace priority_green_wave_api.Controllers
{
    [ApiController]
    [Route("api/[controller]/")]
    public class VeiculoController : BaseController
    {
        private readonly ILogger<VeiculoController> _logger;
        private readonly VeiculoRepository _veiculoRepository;
        private readonly UsuarioRepository _usuarioRepository;
        private readonly VeiculoUsuarioRepository _veiculoUsuarioRepository;
        public VeiculoController(ILogger<VeiculoController> logger, VeiculoRepository veiculoRepository, UsuarioRepository usuarioRepository, VeiculoUsuarioRepository veiculoUsuarioRepository)
        {
            _logger = logger;
            _veiculoRepository = veiculoRepository;
            _usuarioRepository = usuarioRepository;
            _veiculoUsuarioRepository = veiculoUsuarioRepository;
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("ReadVeiculo")]
        public IActionResult ReadVeiculo([FromQuery] int IdVeiculo)
        {
            try
            {
                var retornoVeiculo = _veiculoRepository.Read(IdVeiculo);
                var retornoVeiculoUsuario = _veiculoUsuarioRepository.ReadIdVeiculo(IdVeiculo);
                if (retornoVeiculo.Id != -1)
                {
                    return Ok(retornoVeiculoUsuario);
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
        [Route("CreateVeiculo")]
        public IActionResult CreateVeiculo([FromBody] VeiculoRequestDTO veiculoRequest)
        {
            try
            {
                Veiculo veiculo = new Veiculo();
                VeiculoUsuario veiculoUsuario = new VeiculoUsuario();
                var usuario = _usuarioRepository.Read(veiculoRequest.IdUsuario);
                var erros = new List<string>();

                if (_veiculoRepository.CheckPlaca(veiculoRequest.Placa)){
                    erros.Add("A placa informada já foi cadastrada no sistema.");
                }
                if (_veiculoRepository.CheckRfid(veiculoRequest.Rfid))
                {
                    erros.Add("O Rfid informado já foi cadastrada no sistema.");
                }
                if (usuario.Id == -1)
                {
                    erros.Add("Usuário não cadastrado na base");
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
                    veiculo.Placa = veiculoRequest.Placa;
                    veiculo.Rfid = veiculoRequest.Rfid;
                    veiculo.Fabricante = veiculoRequest.Fabricante;
                    veiculo.Modelo = veiculoRequest.Modelo;
                    veiculo.Ano = veiculoRequest.Ano;
                    veiculo.TipoVeiculo = veiculoRequest.TipoVeiculo;
                    veiculo.VeiculoEmergencia = veiculoRequest.VeiculoEmergencia;
                    veiculo.EstadoEmergencia = veiculoRequest.EstadoEmergencia;
                    int veiculoId = _veiculoRepository.Create(veiculo);

                    veiculoUsuario.IdVeiculo = veiculoId;
                    veiculoUsuario.IdUsuario = usuario.Id;
                    _veiculoUsuarioRepository.Create(veiculoUsuario);
                    return Ok(veiculoUsuario);
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
        [Route("UpdateVeiculo")]
        public IActionResult UpdateVeiculo([FromBody] Veiculo veiculo)
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
                _logger.LogError("Um erro ocorreu!", ex, veiculo);
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
        public IActionResult DeleteVeiculo([FromQuery] int IdVeiculo)
        {
            try
            {
                var veiculoDelete = _veiculoRepository.Read(IdVeiculo);
                var veiculoUsuario = _veiculoUsuarioRepository.ReadIdVeiculo(IdVeiculo);
                if (veiculoDelete.Id != -1)
                {
                    _veiculoRepository.Delete(veiculoDelete);
                    _veiculoUsuarioRepository.Delete(veiculoUsuario);
                    return Ok(veiculoDelete);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Um erro ocorreu!", ex, IdVeiculo);
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
        public IActionResult EstadoEmergencia([FromQuery] string Rfid)
        {
            try
            {
                var veiculo = _veiculoRepository.ReadRfid(Rfid);
                if (veiculo.Id != -1)
                {
                    bool retorno = false;
                    if (veiculo.VeiculoEmergencia)
                    {
                        if (veiculo.EstadoEmergencia)
                        {
                            retorno = true;
                        }
                    }
                    return Ok(retorno);
                }
                else
                {
                    return Ok(false);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Um erro ocorreu!", ex);
                return this.StatusCode(StatusCodes.Status500InternalServerError, new ErrorReturnDTO()
                {
                    Error = "An login error ocurred!",
                    StatusCode = StatusCodes.Status500InternalServerError
                });
            }
        }
    }
}
