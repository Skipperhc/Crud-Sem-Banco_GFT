using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using crud_sem_banco.Models;
using Microsoft.AspNetCore.Mvc;


namespace crud_sem_banco.Controllers {
    public class PessoaController : Controller {
        private readonly List<Pessoa> _listaPessoas;
        public PessoaController(List<Pessoa> listaPessoas) {
            _listaPessoas = listaPessoas;
        }


        public IActionResult Index() {
            return View(_listaPessoas);
        }

        public IActionResult Cadastrar() {
            return View();
        }
        
        [HttpPost]
        public IActionResult Cadastrar([FromForm] Pessoa pessoa) {
            if (!ModelState.IsValid) {
                return View(pessoa);
            }
            pessoa = GerarIdDaPessoa(pessoa);
            _listaPessoas.Add(pessoa);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Editar(int id) {
            var pessoa = _listaPessoas.FirstOrDefault(p => p.Id == id);
            return View(pessoa);
        }

        [HttpPost]
        public IActionResult Editar([FromForm] Pessoa pessoa) {
            if (!ModelState.IsValid) {
                return View(pessoa);
            }

            var pessoalista = _listaPessoas.FirstOrDefault(p => p.Id == pessoa.Id);

            pessoalista.Nome = pessoa.Nome;
            pessoalista.Telefone = pessoa.Telefone;
            pessoalista.Email = pessoa.Email;

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult Excluir(int id) {
            var pessoa = _listaPessoas.FirstOrDefault(p => p.Id == id);
            _listaPessoas.Remove(pessoa);
            return RedirectToAction(nameof(Index));
        }

        private Pessoa GerarIdDaPessoa(Pessoa pessoa) {
            var id = 0;

            if (_listaPessoas.Count != 0) {
                id = _listaPessoas.LastOrDefault().Id + 1;
            }
            pessoa.Id = id;

            return pessoa;
        }

    }
}