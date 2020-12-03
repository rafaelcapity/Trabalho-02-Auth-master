using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Trabalho_02_Auth.Data;
using Trabalho_02_Auth.Models;

namespace Trabalho_02_Auth.Controllers
{
    public class ProdutoController : Controller
    {
        private Datacontext db = new Datacontext();

        #region Index - GET
        public ActionResult Index()
        {
            var produtos = db.Produtos.Include(p => p.Fornecedor);
            return View(produtos.ToList());
        }
        #endregion


        #region Details - GET
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Produto produto = db.Produtos.Find(id);

           
            if (produto == null)
            {
                return HttpNotFound();
            }
            return View(produto);
        }
        #endregion


        #region Create - GET
        public ActionResult Create()
        {
            ViewBag.IdFornecedor = new SelectList(db.Fornecedors, "Id", "Nome");
            return View();
        }
        #endregion


        #region Create - POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Nome,Descricao,CorProduto,StatusProduto,IdFornecedor")] Produto produto)
        {
            if (ModelState.IsValid)
            {
                db.Produtos.Add(produto);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdFornecedor = new SelectList(db.Fornecedors, "Id", "Nome", produto.IdFornecedor);
            return View(produto);
        }
        #endregion


        #region Edit - GET
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Produto produto = db.Produtos.Find(id);
            if (produto == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdFornecedor = new SelectList(db.Fornecedors, "Id", "Nome", produto.IdFornecedor);
            return View(produto);
        }
        #endregion


        #region Edit - POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nome,Descricao,CorProduto,StatusProduto,IdFornecedor")] Produto produto)
        {
            if (ModelState.IsValid)
            {
                db.Entry(produto).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdFornecedor = new SelectList(db.Fornecedors, "Id", "Nome", produto.IdFornecedor);
            return View(produto);
        }
        #endregion


        #region Delete - GET
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Produto produto = db.Produtos.Find(id);
            if (produto == null)
            {
                return HttpNotFound();
            }
            return View(produto);
        }
        #endregion


        #region Delete - POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Produto produto = db.Produtos.Find(id);
            db.Produtos.Remove(produto);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        #endregion


        #region Disconnect
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        #endregion
    }
}
