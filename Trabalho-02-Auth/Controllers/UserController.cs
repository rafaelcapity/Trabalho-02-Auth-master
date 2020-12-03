using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Trabalho_02_Auth.Data;
using Trabalho_02_Auth.Models;

namespace Trabalho_02_Auth.Controllers
{
    public class UserController : Controller
    {
        private Datacontext db = new Datacontext();

        private static string AesIV256BD = @"%j?TmFP6$BbMnY$@";//16 caracteres
        private static string AesKey256BD = @"rxmBUJy]&,;3jKwDTzf(cui$<nc2EQr)";//32 caracteres

        #region Index - GET
        public ActionResult Index()
        {
            List<Models.User> usuarios = db.Users.ToList();

            //AesCryptoServiceProvider
            AesCryptoServiceProvider aes = new AesCryptoServiceProvider();
            aes.BlockSize = 128;
            aes.KeySize = 256;
            aes.IV = Encoding.UTF8.GetBytes(AesIV256BD);
            aes.Key = Encoding.UTF8.GetBytes(AesKey256BD);
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;

            for (int i = 0; i < usuarios.Count; i++)
            {
                byte[] src = Convert.FromBase64String(usuarios[i].Email);
                using (ICryptoTransform decrypt = aes.CreateDecryptor())
                {
                    byte[] dest = decrypt.TransformFinalBlock(src, 0, src.Length);
                    usuarios[i].Email = Encoding.Unicode.GetString(dest);
                }
            }

            return View(usuarios.ToList());

        }
        #endregion


        #region Create - GET
        [HttpGet]
        public ActionResult Create() 
        {
            return View();
        }
        #endregion


        #region Create - POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Models.User user) 
        {

            if (ModelState.IsValid)
            {
                //Hash da Senha
                user.Senha = BCrypt.Net.BCrypt.HashPassword(user.Senha);
                user.ConfirmarSenha = user.Senha;

                //AesCryptoServiceProvider
                AesCryptoServiceProvider aes = new AesCryptoServiceProvider();
                aes.BlockSize = 128;
                aes.KeySize = 256;
                aes.IV = Encoding.UTF8.GetBytes(AesIV256BD);
                aes.Key = Encoding.UTF8.GetBytes(AesKey256BD);
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;

                byte[] src = Encoding.Unicode.GetBytes(user.Email);

                //Encriptação
                using (ICryptoTransform encrypt = aes.CreateEncryptor())
                {
                    byte[] dest = encrypt.TransformFinalBlock(src, 0, src.Length);

                    //Converte byte array para string de base 64
                    user.Email = Convert.ToBase64String(dest);
                }
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            return View(user);

        }
        #endregion


        #region Delaits - GET
        [HttpGet]
        public ActionResult Details (int? Id) 
        {

            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(Id);
            if (user == null)
            {
                return HttpNotFound();
            }
            AesCryptoServiceProvider aes = new AesCryptoServiceProvider();
            aes.BlockSize = 128;
            aes.KeySize = 256;
            aes.IV = Encoding.UTF8.GetBytes(AesIV256BD);
            aes.Key = Encoding.UTF8.GetBytes(AesKey256BD);
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;

            byte[] src = Convert.FromBase64String(user.Email);

            using (ICryptoTransform decrypt = aes.CreateDecryptor())
            {
                byte[] dest = decrypt.TransformFinalBlock(src, 0, src.Length);
                user.Email = Encoding.Unicode.GetString(dest);
            }
            return View(user);
        }
        #endregion


        #region Edit - GET
        public ActionResult Edti(int? Id)
        {
            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(Id);
            if (user == null)
            {
                return HttpNotFound();
            }
            AesCryptoServiceProvider aes = new AesCryptoServiceProvider();
            aes.BlockSize = 128;
            aes.KeySize = 256;
            aes.IV = Encoding.UTF8.GetBytes(AesIV256BD);
            aes.Key = Encoding.UTF8.GetBytes(AesKey256BD);
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;

            byte[] src = System.Convert.FromBase64String(user.Email);

            using (ICryptoTransform encrypt = aes.CreateEncryptor())
            {
                byte[] dest = encrypt.TransformFinalBlock(src, 0, src.Length);

                //Converte byte array para string de base 64
                user.Email = Encoding.Unicode.GetString(dest);

            }
            return View(user);
        }
        #endregion


        #region Edit - POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Models.User User)
        {
            Models.User user = db.Users.Find(User.Id);
            user.Senha = user.Senha;
            user.ConfirmarSenha = user.Senha;
            db.Entry(User).State = EntityState.Detached;

            AesCryptoServiceProvider aes = new AesCryptoServiceProvider();
            aes.BlockSize = 128;
            aes.KeySize = 256;
            aes.IV = Encoding.UTF8.GetBytes(AesIV256BD);
            aes.Key = Encoding.UTF8.GetBytes(AesKey256BD);
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;

            byte[] src = Encoding.Unicode.GetBytes(user.Email);

            using (ICryptoTransform decrypt = aes.CreateEncryptor())
            {
                byte[] dest = decrypt.TransformFinalBlock(src, 0, src.Length);
                user.Email = Convert.ToBase64String(dest);
            }
            db.Entry(User).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        #endregion


        #region Delete - POST

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]

        public ActionResult Delete(int id)
        {
            Models.User user = db.Users.Find(id);
            db.Users.Remove(user);
            db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        #endregion


        #region Logout
        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("index", "Login");
        }
        #endregion

    }
}