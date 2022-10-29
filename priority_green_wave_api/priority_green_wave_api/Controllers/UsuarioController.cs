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
    public class UsuarioController : BaseController
    {
        private readonly ILogger<UsuarioController> _logger;
        private readonly UsuarioRepository _usuarioRepository;
        private readonly VeiculoUsuarioRepository _veiculoUsuarioRepository;
        private readonly VeiculoRepository _veiculoRepository;

        public UsuarioController(ILogger<UsuarioController> logger, UsuarioRepository usuarioRequestRepository, VeiculoUsuarioRepository veiculoUsuarioRepository, VeiculoRepository veiculoRepository)
        {
            _logger = logger;
            _usuarioRepository = usuarioRequestRepository;
            _veiculoUsuarioRepository = veiculoUsuarioRepository;
            _veiculoRepository = veiculoRepository;
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("ReadUsuario")]
        public IActionResult ReadUsuario([FromQuery] int IdUsuario)
        {
            try
            {  
                var usuarioRequestRetorno = _usuarioRepository.Read(IdUsuario);
                if (usuarioRequestRetorno.Id != -1)
                {
                    return Ok(usuarioRequestRetorno);
                }
                else
                {
                return BadRequest(new ErrorReturnDTO()
                {
                    Error = "Usuário Inválido!",
                    StatusCode = StatusCodes.Status400BadRequest});
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
        [Route("CreateUsuario")]
        public IActionResult CreateUsuario([FromBody] UsuarioRequestDTO usuarioRequest)
        {
            try
            {
                var errors = new List<string>();
                if (string.IsNullOrEmpty(usuarioRequest.Nome) || string.IsNullOrWhiteSpace(usuarioRequest.Nome))
                {
                    errors.Add("Nome Inválido!");
                }
                else if (string.IsNullOrEmpty(usuarioRequest.Email) || string.IsNullOrWhiteSpace(usuarioRequest.Email)
                    || !Regex.IsMatch(usuarioRequest.Email, @"^([\w\.\-\+\d]+)@([\w\-]+)((\.(\w){2,3})+)$", RegexOptions.IgnoreCase))
                {
                    errors.Add("Email inválido!");
                }
                else if (string.IsNullOrEmpty(usuarioRequest.Senha) || string.IsNullOrWhiteSpace(usuarioRequest.Senha)
                    && !Regex.IsMatch(usuarioRequest.Senha, "[a-zA-Z0-9]+", RegexOptions.IgnoreCase) && usuarioRequest.Senha.Length < 8)
                {
                    errors.Add("Senha inválida!");
                }
                else if (_usuarioRepository.CheckEmail(usuarioRequest.Email))
                {
                    errors.Add("Email já está sendo utilizado!");
                }
                if (errors.Count > 0)
                {
                    return BadRequest(new ErrorReturnDTO()
                    {
                        Errors = errors,
                        StatusCode = StatusCodes.Status400BadRequest
                    });
                }
                Usuario usuario = new Usuario(nome: usuarioRequest.Nome,
                                              cpf: usuarioRequest.Cpf,
                                              dataNascimento: usuarioRequest.DataNascimento,
                                              email: usuarioRequest.Email,
                                              telefone: usuarioRequest.Telefone,
                                              senha: MD5Utils.GetHashMD5(usuarioRequest.Senha),
                                              motoristaEmergencia: usuarioRequest.MotoristaEmergencia);

                int usuarioRequestId = _usuarioRepository.Create(usuario);
                return Ok(new { msg = "usuarioRequest criado com sucesso! usuario ID: "+  usuarioRequestId.ToString()});
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
        [Route("LoginUsuario")]
        public IActionResult LoginUsuario([FromBody] LoginRequestDTO request)
        {
            try
            {
                if (request == null || String.IsNullOrEmpty(request.login) || String.IsNullOrEmpty(request.password)
                    || string.IsNullOrWhiteSpace(request.login) || string.IsNullOrWhiteSpace(request.password))
                {
                    return BadRequest(new ErrorReturnDTO()
                    {
                        Error = "Invalid login and/or password!",
                        StatusCode = StatusCodes.Status400BadRequest
                    });
                }
                request.password = MD5Utils.GetHashMD5(request.password);
                Usuario user = _usuarioRepository.CheckLogin(request);

                if (user.Id != -1)
                {
                    var token = TokenService.CreateToken(user.Id);
                    return Ok(new LoginResponseDTO()
                    {
                        Id = user.Id,
                        Nome = user.Nome,
                        Email = user.Email,
                        Token = token
                    });
                }
                else
                {
                    return BadRequest(new ErrorReturnDTO()
                    {
                        Error = "Invalid login and/or password!",
                        StatusCode = StatusCodes.Status400BadRequest
                    });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Um erro ocorreu!", ex, request);
                return this.StatusCode(StatusCodes.Status500InternalServerError, new ErrorReturnDTO()
                {
                    Error = "Um erro ocorreu!",
                    StatusCode = StatusCodes.Status500InternalServerError
                });
            }
        }

        [HttpPut]
        [AllowAnonymous]
        [Route("UpdateUsuario")]
        public IActionResult UpdateUsuario([FromBody] Usuario usuarioRequest)
        {
            try
            {
                usuarioRequest.Senha = MD5Utils.GetHashMD5(usuarioRequest.Senha);
                var usuarioRequestDadosAntigos = _usuarioRepository.Read(usuarioRequest.Id);
                
                if(usuarioRequestDadosAntigos.Id != -1)
                {
                    Usuario usuarioRequestDadosNovos = new Usuario();
                    usuarioRequestDadosNovos.Id = usuarioRequestDadosAntigos.Id;

                    usuarioRequestDadosNovos.Nome = usuarioRequest.Nome != usuarioRequestDadosAntigos.Nome ? usuarioRequest.Nome : usuarioRequestDadosAntigos.Nome;
                    usuarioRequestDadosNovos.Cpf = usuarioRequest.Cpf != usuarioRequestDadosAntigos.Cpf ? usuarioRequest.Cpf : usuarioRequestDadosAntigos.Cpf;
                    usuarioRequestDadosNovos.DataNascimento = usuarioRequest.DataNascimento != usuarioRequestDadosAntigos.DataNascimento ? usuarioRequest.DataNascimento : usuarioRequestDadosAntigos.DataNascimento;
                    usuarioRequestDadosNovos.Telefone = usuarioRequest.Telefone != usuarioRequestDadosAntigos.Telefone ? usuarioRequest.Telefone : usuarioRequestDadosAntigos.Telefone;
                    usuarioRequestDadosNovos.Senha = usuarioRequest.Senha != usuarioRequestDadosAntigos.Senha ? usuarioRequest.Senha : usuarioRequestDadosAntigos.Senha;
                    usuarioRequestDadosNovos.MotoristaEmergencia = usuarioRequest.MotoristaEmergencia != usuarioRequestDadosAntigos.MotoristaEmergencia ? usuarioRequest.MotoristaEmergencia : usuarioRequestDadosAntigos.MotoristaEmergencia;
                    usuarioRequestDadosNovos.Email = (usuarioRequest.Email != usuarioRequestDadosAntigos.Email) && (!_usuarioRepository.CheckEmail(usuarioRequest.Email)) ? usuarioRequest.Email : usuarioRequestDadosAntigos.Email;

                    _usuarioRepository.Update(usuarioRequestDadosNovos);
                    return Ok(usuarioRequestDadosNovos);
                }
                else
                {
                    return BadRequest();
                } 
            }
            catch (Exception ex)
            {
                _logger.LogError("Um erro ocorreu!", ex, usuarioRequest);
                return this.StatusCode(StatusCodes.Status500InternalServerError, new ErrorReturnDTO()
                {
                    Error = "Ocorreu um erro no login!",
                    StatusCode = StatusCodes.Status500InternalServerError
                });
            }
        }
        
        [HttpDelete]
        [AllowAnonymous]
        [Route("DeleteUsuario")]
        public IActionResult DeleteUsuario([FromQuery] int IdUsuario)
        {
            try
            {
                var usuarioRequestDelete = _usuarioRepository.Read(IdUsuario);
                
                if (usuarioRequestDelete.Id != -1)
                {
                    var veiculosUsuario = _veiculoUsuarioRepository.ReadIdUsuario(IdUsuario);

                    
                    foreach (VeiculoUsuario veiculoUsuario in veiculosUsuario)
                    {
                        var veiculo = _veiculoRepository.Read(veiculoUsuario.IdVeiculo);
                        _veiculoRepository.Delete(veiculo);
                        _veiculoUsuarioRepository.Delete(veiculoUsuario);
                    }
                    _usuarioRepository.Delete(usuarioRequestDelete);
                    return Ok(usuarioRequestDelete);   
                }
                else
                {
                    return BadRequest("Usuário não encontrado!");
                }               
            }
            catch (Exception ex)
            {
                _logger.LogError("Um erro ocorreu!", ex, IdUsuario);
                return this.StatusCode(StatusCodes.Status500InternalServerError, new ErrorReturnDTO()
                {
                    Error = "Um erro ocorreu!",
                    StatusCode = StatusCodes.Status500InternalServerError
                });
            }
        }
    }
}
