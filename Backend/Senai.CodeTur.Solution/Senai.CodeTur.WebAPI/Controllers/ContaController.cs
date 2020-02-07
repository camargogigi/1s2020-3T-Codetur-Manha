using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Senai.CodeTur.Dominio.Interfaces.Repositorios;
using Senai.CodeTur.Infra.Data.Repositorios;
using Senai.CodeTur.Servico.ViewModels.Conta;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Senai.CodeTur.Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContaController : ControllerBase
    {
        private IUsuarioRepositorio _usuarioRepositorio;

        public ContaController(IUsuarioRepositorio usuarioRepositorio)
        {
            _usuarioRepositorio = usuarioRepositorio;
        }


        [HttpPost]
        public IActionResult Login(LoginViewModel usuario)
        {
            try
            {
                var usuarioRetorno = _usuarioRepositorio.EfetuarLogin(usuario.Email, usuario.Senha);

                if (usuarioRetorno == null)
                {
                    return NotFound(new { mensagem = "Email ou senha inválido" });
                }

                var claims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Email, usuarioRetorno.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, usuarioRetorno.Id.ToString())
                };

                var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("codetur-chave-autenticacao"));

                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(
                    issuer: "CodeTur.WebApi",
                    audience: "CodeTur.WebApi",
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(30),
                    signingCredentials: creds
                );

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token)
                });

            }
            catch (Exception ex)
            {
                return BadRequest(new { sucesso = false, mensagem = ex.Message });
            }
        }
    }
}