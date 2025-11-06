using Nethereum.Web3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using TPFInal_PWIII.Logica.Core;
using TPFInal_PWIII.Logica.Interfaces;
using Nethereum.Contracts;
using Microsoft.Extensions.Configuration;

namespace TPFInal_PWIII.Logica
{
    public class HistoriaLogica: ILogicaHistorial
    {
        private readonly IWeb3 _web3;
        private readonly Contract _contrato;

        public HistoriaLogica(IWeb3 web3, IConfiguration config)
        {
            _web3 = web3;
            string contractAddress = config["BlockchainSettings:ContractAddress"];
            string abi = config["BlockchainSettings:ContractAbi"];
            _contrato = _web3.Eth.GetContract(abi, contractAddress);
        }

        public async Task<List<DecisionDTO>> ObtenerHistorialBlockchain(BigInteger partidaId)
        {
            try
            {
                var funcionHistorial = _contrato.GetFunction("obtenerHistorialPartida");
                var historial = await funcionHistorial.CallDeserializingToObjectAsync<List<DecisionDTO>>(partidaId);
                return historial;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al leer de la Blockchain: {ex.Message}");
                return new List<DecisionDTO>();
            }
        }

    }
}
