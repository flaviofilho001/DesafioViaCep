using System.Net.Http.Json;
using System.Text.RegularExpressions;

namespace ViaCep.Services
{
    public class ViaCepService : IViaCepService
    {
        private readonly HttpClient _httpClient;

        public ViaCepService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ViaCepResponse?> BuscarPorCepAsync(string cep)
        {
            if (string.IsNullOrWhiteSpace(cep))
                return null;

            // Limpa o CEP mantendo apenas números
            var cepLimpo = Regex.Replace(cep, @"[^\d]", "");
            if (cepLimpo.Length != 8)
                return null;

            try
            {
                var response = await _httpClient.GetFromJsonAsync<ViaCepResponse>($"https://viacep.com.br/ws/{cepLimpo}/json/");
                
                if (response is not null && response.Erro)
                    return null;

                return response;
            }
            catch
            {
                return null;
            }
        }
    }
}
