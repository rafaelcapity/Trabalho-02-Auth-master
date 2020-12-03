using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Trabalho_02_Auth.Data;

namespace Trabalho_02_Auth.Controllers
{
    public class LoginController : Controller
    {

        private Datacontext db = new Datacontext();

        private static string AesIV256BD = @"%j?TmFP6$BbMnY$@";//16 caracteres
        private static string AesKey256BD = @"rxmBUJy]&,;3jKwDTzf(cui$<nc2EQr)";//32 caracteres

        public ActionResult Index(string erro)
        {
            if (erro != null)
            {
                TempData["error"] = erro;
            }
            return View();
        }

        #region  Index GET
        [HttpPost]
        public ActionResult Verificar(Models.User user)
        {
            AesCryptoServiceProvider aes = new AesCryptoServiceProvider();
            aes.BlockSize = 128;
            aes.KeySize = 256;
            aes.IV = Encoding.UTF8.GetBytes(AesIV256BD);
            aes.Key = Encoding.UTF8.GetBytes(AesKey256BD);
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;

            byte[] src = Encoding.Unicode.GetBytes(user.Email);

            using (ICryptoTransform encrypt = aes.CreateEncryptor())
            {
                byte[] dest = encrypt.TransformFinalBlock(src, 0, src.Length);

                //Converte byte array para string de base 64
                user.Email = Convert.ToBase64String(dest);
            }

            Models.User Consulta = db.Users.FirstOrDefault
                (u => u.Email == user.Email);

            string erro = "Usuario ou Senha Inválido";

            if (Consulta == null)
            {
                return RedirectToAction(nameof(Index), new { @erro = erro });
            }

            if (BCrypt.Net.BCrypt.Verify(user.Senha, Consulta.Senha))
            {
                Session["Nome"] = Consulta.Nome;
                Session["Nivel"] = Consulta.NivelUsuario;
                return RedirectToAction("Index", "Usuario");
            }

            return RedirectToAction(nameof(Index), new { @erro = erro });
        }
        #endregion

    }
}