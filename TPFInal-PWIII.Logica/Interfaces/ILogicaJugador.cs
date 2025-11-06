using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPFInal_PWIII.Logica.Interfaces
{
    public interface ILogicaJugador
    {
        Task RegistrarJugador(string walletAddress, string nombreUsuario, string apellido, string correo);
        Task<bool> ExisteJugador(string walletAddress);
    }
}
