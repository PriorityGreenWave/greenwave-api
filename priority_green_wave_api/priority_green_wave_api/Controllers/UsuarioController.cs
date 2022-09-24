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
        public UsuarioController(ILogger<UsuarioController> logger, UsuarioRepository usuarioRepository)
        {
            _logger = logger;
            _usuarioRepository = usuarioRepository;
        }

        [HttpGet]
        [Route("ReadUsuario")]
        public IActionResult ReadUsuario([FromHeader] string authorization)
        {
            try
            {
                if (AuthenticationHeaderValue.TryParse(authorization, out var headerValue))
                {
                    var scheme = headerValue.Scheme;
                    var parameter = headerValue.Parameter;
                    var token = TokenService.DecodeToken(parameter);
                    var usuario = _usuarioRepository.Read(int.Parse(token));
                    if (usuario.Id != 0)
                    {
                        return Ok(usuario);
                    }
                    else
                    {
                        return BadRequest(new ErrorReturnDTO()
                        {
                            Error = "Invalid usuario ID!",
                            StatusCode = StatusCodes.Status400BadRequest
                        });
                    }
                }
                return BadRequest(new ErrorReturnDTO()
                {
                    Error = "Invalid usuario ID!",
                    StatusCode = StatusCodes.Status400BadRequest
                });
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
        [Route("CreateUsuario")]
        public IActionResult CreateUsuario([FromBody] Usuario usuario)
        {
            try
            {
                var errors = new List<string>();
                if (string.IsNullOrEmpty(usuario.Nome) || string.IsNullOrWhiteSpace(usuario.Nome))
                {
                    errors.Add("Invalid Name!");
                }
                else if (string.IsNullOrEmpty(usuario.Email) || string.IsNullOrWhiteSpace(usuario.Email)
                    || !Regex.IsMatch(usuario.Email, @"^([\w\.\-\+\d]+)@([\w\-]+)((\.(\w){2,3})+)$", RegexOptions.IgnoreCase))
                {
                    errors.Add("Invalid Email!");
                }
                else if (string.IsNullOrEmpty(usuario.Senha) || string.IsNullOrWhiteSpace(usuario.Senha)
                    && !Regex.IsMatch(usuario.Senha, "[a-zA-Z0-9]+", RegexOptions.IgnoreCase) && usuario.Senha.Length < 8)
                {
                    errors.Add("Invalid Password!");
                }
                else if (_usuarioRepository.CheckEmail(usuario.Email))
                {
                    errors.Add("Email is already being used!");
                }
                if (errors.Count > 0)
                {
                    return BadRequest(new ErrorReturnDTO()
                    {
                        Errors = errors,
                        StatusCode = StatusCodes.Status400BadRequest
                    });
                }
                usuario.Senha = MD5Utils.GetHashMD5(usuario.Senha);
                _usuarioRepository.Create(usuario);
                return Ok(new { msg = "usuario created successfully!" });
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
                _logger.LogError("An error ocurred!", ex, request);
                return this.StatusCode(StatusCodes.Status500InternalServerError, new ErrorReturnDTO()
                {
                    Error = "An login error ocurred!",
                    StatusCode = StatusCodes.Status500InternalServerError
                });
            }
        }

        [HttpPut]
        [Route("UpdateUsuario")]
        public IActionResult UpdateUsuario([FromBody] Usuario usuario)
        {
            try
            {
                usuario.Senha = MD5Utils.GetHashMD5(usuario.Senha);
                var usuarioDadosAntigos = _usuarioRepository.Read(usuario.Id);

                Usuario usuarioDadosNovos = new Usuario();
                usuarioDadosNovos.Id = usuarioDadosAntigos.Id;

                usuarioDadosNovos.Nome = usuario.Nome != usuarioDadosAntigos.Nome ? usuario.Nome : usuarioDadosAntigos.Nome;
                usuarioDadosNovos.Cpf = usuario.Cpf != usuarioDadosAntigos.Cpf ? usuario.Cpf : usuarioDadosAntigos.Cpf;
                usuarioDadosNovos.DataNascimento = usuario.DataNascimento != usuarioDadosAntigos.DataNascimento ? usuario.DataNascimento : usuarioDadosAntigos.DataNascimento;
                usuarioDadosNovos.Telefone = usuario.Telefone != usuarioDadosAntigos.Telefone ? usuario.Telefone : usuarioDadosAntigos.Telefone;
                usuarioDadosNovos.Senha = usuario.Senha != usuarioDadosAntigos.Senha ? usuario.Senha : usuarioDadosAntigos.Senha;
                usuarioDadosNovos.MotoristaEmergencia = usuario.MotoristaEmergencia != usuarioDadosAntigos.MotoristaEmergencia ? usuario.MotoristaEmergencia : usuarioDadosAntigos.MotoristaEmergencia;
                usuarioDadosNovos.Email = (usuario.Email != usuarioDadosAntigos.Email) && (!_usuarioRepository.CheckEmail(usuario.Email)) ? usuario.Email : usuarioDadosAntigos.Email;

                _usuarioRepository.Update(usuarioDadosNovos);
                return Ok(usuarioDadosNovos);
            }
            catch (Exception ex)
            {
                _logger.LogError("An error ocurred!", ex, usuario);
                return this.StatusCode(StatusCodes.Status500InternalServerError, new ErrorReturnDTO()
                {
                    Error = "An login error ocurred!",
                    StatusCode = StatusCodes.Status500InternalServerError
                });
            }
        }
    }
}
