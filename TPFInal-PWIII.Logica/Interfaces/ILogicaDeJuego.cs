using TPFInal_PWIII.Data.Entidades;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TPFInal_PWIII.Logica.Interfaces
{
    /// <summary>
    /// Maneja la lógica de la partida
    /// </summary>
    public interface ILogicaDeJuego
    {
        /// <summary>
        /// Obtiene el capítulo actual que se está votando
        /// </summary>
        Task<Capitulo> ObtenerSiguienteCapitulo(int idOpcionElegida);  // FK a otro Capitulo

        /// <summary>
        /// Almacena el voto de un jugador (off-chain) en la base de datos
        /// para la ronda actual.
        /// </summary>
        Task Votar(int partidaId, string walletJugador, string opcionElegida); 

        /// <summary>
        /// Procesa la ronda: cuenta votos, aplica desempate, sella en la blockchain
        /// y avanza al siguiente capítulo.
        /// </summary>
        Task<Capitulo> FinalizarRonda(int partidaId);

        /// <summary>
        /// inicia la historia.
        /// </summary>
        Task<Capitulo> IniciarNuevaAventura();

        /// <summary>
        /// Un nuevo jugador se une a la partida en curso.
        /// </summary>
        Task UnirseAPartida(string idJugador, int partidaId);
    }
}