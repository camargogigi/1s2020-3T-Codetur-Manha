using Senai.CodeTur.Dominio.Entidades;
using Senai.CodeTur.Dominio.Interfaces.Repositorios;
using Senai.CodeTur.Infra.Data.Contextos;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Senai.CodeTur.Infra.Data.Repositorios
{
    public class PacoteRepositorio : IPacoteRepositorio, IDisposable
    {

        private CodeTurContext _context;

        public PacoteRepositorio(CodeTurContext context)
        {
            _context = context;
        }

        public PacoteDominio BuscarPorId(int id)
        {
            try
            {
                return _context.Pacotes.Find(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public PacoteDominio Cadastrar(PacoteDominio pacote)
        {
            try
            {
                _context.Pacotes.Add(pacote);
                _context.SaveChanges();

                return pacote;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<PacoteDominio> Listar(bool? todos = null)
        {
            try
            {
                if (todos == null)
                {
                    return _context.Pacotes.ToList();
                }
                else if (todos == true)
                {
                    return _context.Pacotes.Where(x => x.Ativo == true).ToList();
                }
                else
                {
                    return _context.Pacotes.Where(x => x.Ativo == false).ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
