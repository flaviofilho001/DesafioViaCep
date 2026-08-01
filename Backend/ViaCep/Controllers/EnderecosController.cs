using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using ViaCep.Models;
using ViaCep.Services;
using ViaCep.Services.Exportacao;
using ViaCep.ViewModels;

namespace ViaCep.Controllers
{
    [Authorize]
    public class EnderecosController : Controller
    {
        private readonly IEnderecoService _enderecoService;
        private readonly IViaCepService _viaCepService;
        private readonly IExportadorContext _exportadorContext;

        public EnderecosController(
            IEnderecoService enderecoService,
            IViaCepService viaCepService,
            IExportadorContext exportadorContext)
        {
            _enderecoService = enderecoService;
            _viaCepService = viaCepService;
            _exportadorContext = exportadorContext;
        }

        private int ObterUsuarioIdLogado()
        {
            var claim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return int.TryParse(claim, out var id) ? id : 0;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var usuarioId = ObterUsuarioIdLogado();
            var enderecos = await _enderecoService.ListarPorUsuarioAsync(usuarioId);

            var viewModels = enderecos.Select(e => new EnderecoViewModel
            {
                Id = e.Id,
                Cep = e.Cep,
                Logradouro = e.Logradouro,
                Complemento = e.Complemento,
                Bairro = e.Bairro,
                Cidade = e.Cidade,
                Uf = e.Uf,
                Numero = e.Numero
            }).ToList();

            return View(viewModels);
        }

        [HttpGet]
        public IActionResult Criar()
        {
            return View(new EnderecoFormViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Criar(EnderecoFormViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var usuarioId = ObterUsuarioIdLogado();
            var endereco = new Endereco
            {
                Cep = model.Cep,
                Logradouro = model.Logradouro,
                Complemento = model.Complemento,
                Bairro = model.Bairro,
                Cidade = model.Cidade,
                Uf = model.Uf,
                Numero = model.Numero,
                UsuarioId = usuarioId
            };

            await _enderecoService.AdicionarAsync(endereco);
            TempData["Sucesso"] = "Endereço cadastrado com sucesso!";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Editar(int id)
        {
            var usuarioId = ObterUsuarioIdLogado();
            var endereco = await _enderecoService.ObterPorIdAsync(id, usuarioId);
            if (endereco is null)
                return NotFound();

            var model = new EnderecoFormViewModel
            {
                Id = endereco.Id,
                Cep = endereco.Cep,
                Logradouro = endereco.Logradouro,
                Complemento = endereco.Complemento,
                Bairro = endereco.Bairro,
                Cidade = endereco.Cidade,
                Uf = endereco.Uf,
                Numero = endereco.Numero
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(int id, EnderecoFormViewModel model)
        {
            if (id != model.Id)
                return BadRequest();

            if (!ModelState.IsValid)
                return View(model);

            var usuarioId = ObterUsuarioIdLogado();
            var endereco = new Endereco
            {
                Id = model.Id,
                Cep = model.Cep,
                Logradouro = model.Logradouro,
                Complemento = model.Complemento,
                Bairro = model.Bairro,
                Cidade = model.Cidade,
                Uf = model.Uf,
                Numero = model.Numero,
                UsuarioId = usuarioId
            };

            await _enderecoService.AtualizarAsync(endereco);
            TempData["Sucesso"] = "Endereço atualizado com sucesso!";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Excluir(int id)
        {
            var usuarioId = ObterUsuarioIdLogado();
            await _enderecoService.ExcluirAsync(id, usuarioId);
            TempData["Sucesso"] = "Endereço excluído com sucesso!";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet("api/viacep/{cep}")]
        [HttpGet]
        public async Task<IActionResult> BuscarCep(string cep)
        {
            var resultado = await _viaCepService.BuscarPorCepAsync(cep);
            if (resultado is null)
                return NotFound(new { erro = "CEP não localizado." });

            return Ok(resultado);
        }

        [HttpGet]
        public async Task<IActionResult> ExportarCsv(string formato = "csv")
        {
            var usuarioId = ObterUsuarioIdLogado();
            var enderecos = await _enderecoService.ListarPorUsuarioAsync(usuarioId);

            var fileBytes = _exportadorContext.Exportar(formato, enderecos, out string contentType, out string nomeArquivo);
            return File(fileBytes, contentType, nomeArquivo);
        }
    }
}
