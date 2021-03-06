﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MarktSys_ASP_NET_CORE.Data;
using MarktSys_ASP_NET_CORE.Models;
using Microsoft.AspNetCore.Mvc;

namespace MarktSys_ASP_NET_CORE.Controllers
{
    public class EstoqueController : Controller{

        public readonly ApplicationDbContext database;

        public EstoqueController(ApplicationDbContext database) {
            this.database = database;
        }

        [HttpPost]
        public IActionResult Salvar(Estoque estoque){

            if (ModelState.IsValid) {

                database.Estoques.Add(estoque);
                database.SaveChanges();
            } else {
                return View("../administrativo/novoestoque");
            }

            return RedirectToAction("Estoque", "Administrativo");
        }

        [HttpPost]
        public IActionResult Editar(Estoque estoque) {

            if (ModelState.IsValid) {

                Estoque estoqueBanco = database.Estoques.First(e => e.Id == estoque.Id);

                estoqueBanco.Produto = database.Produtos.First(e => e.Id == estoque.ProdutoId);
                estoqueBanco.Saldo = estoque.Saldo;

                database.SaveChanges();
            } else {
                return View("../administrativo/novoestoque");
            }

            return RedirectToAction("Estoque", "Administrativo");
        }
    }
}