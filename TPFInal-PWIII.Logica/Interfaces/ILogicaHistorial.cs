using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using TPFInal_PWIII.Logica.Core;

namespace TPFInal_PWIII.Logica.Interfaces
{
    public interface ILogicaHistorial
    {
        Task<List<DecisionDTO>> ObtenerHistorialBlockchain(BigInteger historiaId);
    }
}
