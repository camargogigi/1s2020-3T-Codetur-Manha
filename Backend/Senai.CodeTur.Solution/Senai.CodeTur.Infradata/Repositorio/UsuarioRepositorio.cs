using Senai.CodeTur.Dominio.Entidades;
using Senai.CodeTur.Dominio.Interfaces.Repositorios;
using Senai.CodeTur.Infra.Data.Contextos;
using System.Linq;

namespace Senai.CodeTur.Infra.Data.Repositorios
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {

        private CodeTurContext _context;

        public UsuarioRepositorio(CodeTurContext context)
        {
            _context = context;
        }

        public UsuarioDominio EfetuarLogin(string email, string senha)
        {
            try
            {
                return _context.Usuarios.FirstOrDefault(x => x.Email == email && x.Senha == senha);
            }
            catch (System.Exception ex)
            {
                throw new System.Exception(ex.Message);
            }
        }
    }
}
