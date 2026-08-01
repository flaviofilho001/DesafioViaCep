using System.Text;
using ViaCep.Models;

namespace ViaCep.Services.Exportacao
{
    public class CsvExportadorStrategy : IExportadorStrategy, IExportadorEnderecos
    {
        public string Formato => "csv";
        public string ContentType => "text/csv; charset=utf-8";

        public string ObterNomeArquivo(string prefixo = "enderecos")
        {
            return $"{prefixo}_{DateTime.Now:yyyyMMdd_HHmmss}.csv";
        }

        public byte[] Exportar(List<Endereco> enderecos)
        {
            var sb = new StringBuilder();

            // Cabeçalho CSV
            sb.AppendLine("Id;CEP;Logradouro;Numero;Complemento;Bairro;Cidade;UF");

            foreach (var item in enderecos)
            {
                var complementoSanitizado = (item.Complemento ?? "").Replace(";", " ");
                var logradouroSanitizado = (item.Logradouro ?? "").Replace(";", " ");
                var bairroSanitizado = (item.Bairro ?? "").Replace(";", " ");
                var cidadeSanitizada = (item.Cidade ?? "").Replace(";", " ");

                sb.AppendLine($"{item.Id};\"{item.Cep}\";\"{logradouroSanitizado}\";\"{item.Numero}\";\"{complementoSanitizado}\";\"{bairroSanitizado}\";\"{cidadeSanitizada}\";\"{item.Uf}\"");
            }

            // UTF-8 BOM para garantir acentuação correta no Excel
            var encoding = new UTF8Encoding(true);
            return encoding.GetBytes(sb.ToString());
        }
    }
}
