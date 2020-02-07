using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Senai.CodeTur.Dominio.Entidades;
using Senai.CodeTur.Dominio.Interfaces.Repositorios;
using Senai.CodeTur.Servico.ViewModels.Pacote;

namespace Senai.CodeTur.Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PacoteController : ControllerBase
    {

        private IPacoteRepositorio _pacoteRepositorio { get; set; }

        public PacoteController(IPacoteRepositorio pacoteRepositorio)
        {
            _pacoteRepositorio = pacoteRepositorio;
        }

        [HttpPost]
        [Authorize]
        public IActionResult Post(CadastrarPacoteViewModel pacote)
        {
            try
            {
                PacoteDominio pacoteNovo = new PacoteDominio()
                {
                    Titulo = pacote.Titulo,
                    Descricao = pacote.Descricao,
                    Imagem = pacote.Imagem,
                    DataInicio = pacote.DataInicio,
                    DataFim = pacote.DataFim,
                    Ativo = pacote.Ativo,
                    Pais = pacote.Pais
                };

                var pacoteRetorno = _pacoteRepositorio.Cadastrar(pacoteNovo);

                return Ok(pacoteRetorno);
            }
            catch (Exception ex)
            {
                return BadRequest(new { sucesso = false, mensagem = ex.Message });
            }
        }

        [HttpGet]
        [Route("listar")]
        [Authorize]
        public IActionResult Listar()
        {
            try
            {
                return Ok(_pacoteRepositorio.Listar());
            }
            catch (Exception ex)
            {
                return BadRequest(new { sucesso = false, mensagem = ex.Message });
            }
        }

        [HttpGet]
        [Route("listarativos")]
        public IActionResult ListarAtivos()
        {
            try
            {
                return Ok(_pacoteRepositorio.Listar(true));
            }
            catch (Exception ex)
            {
                return BadRequest(new { sucesso = false, mensagem = ex.Message });
            }
        }

        [HttpGet("{id}")]
        [Authorize]
        public IActionResult BuscarPorId(int id)
        {
            try
            {
                var pacote = _pacoteRepositorio.BuscarPorId(id);

                if (pacote == null)
                    return NotFound(new { mensagem = "Pacote não encontrado" });

                return Ok(pacote);
            }
            catch (Exception ex)
            {
                return BadRequest(new { sucesso = false, mensagem = ex.Message });
            }
        }
    }
}