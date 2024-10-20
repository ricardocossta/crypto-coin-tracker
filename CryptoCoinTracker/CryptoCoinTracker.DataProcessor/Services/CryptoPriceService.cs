using CryptoCoinTracker.DataProcessor.Entities;
using CryptoCoinTracker.DataProcessor.Interface;
using CryptoCoinTracker.Models;
using Newtonsoft.Json;

namespace CryptoCoinTracker.DataProcessor.Services;

public class CryptoPriceService : ICryptoPriceService
{
    private readonly ICryptoPriceRepository _cryptoPriceRepository;

    public CryptoPriceService(ICryptoPriceRepository cryptoPriceRepository)
    {
        _cryptoPriceRepository = cryptoPriceRepository;
    }

    public async Task ProcessMessageAsync(string message)
    {
        try
        {
            var cryptoData = JsonConvert.DeserializeObject<Crypto>(message) ?? throw new Exception("Failed to deserialize message.");

            if (cryptoData.Bitcoin != null)
            {
                await UpsertCryptoPrice("bitcoin", cryptoData.Bitcoin);
            }

            if (cryptoData.Solana != null)
            {
                await UpsertCryptoPrice("solana", cryptoData.Solana);
            }

            if (cryptoData.Ethereum != null)
            {
                await UpsertCryptoPrice("ethereum", cryptoData.Ethereum);
            }

            if (cryptoData.BinanceCoin != null)
            {
                await UpsertCryptoPrice("binancecoin", cryptoData.BinanceCoin);
            }

            if (cryptoData.DogeCoin != null)
            {
                await UpsertCryptoPrice("dogecoin", cryptoData.DogeCoin);
            }
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred while processing the message.", ex);
        }
    }

    private async Task UpsertCryptoPrice(string coinName, CryptoData cryptoData)
    {
        var cryptoPrice = new CryptoPrice
        {
            Coin = coinName,
            Usd = cryptoData.Usd,
            UsdMarketCap = cryptoData.UsdMarketCap
        };

        await _cryptoPriceRepository.UpsertAsync(cryptoPrice);
    }
}
