using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using System.Security.Cryptography;
using System.Text;

namespace Utilidades.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class IdentificadoresController : ControllerBase
    {

        /// <summary>
        /// Guid 32 caracteres, idMongoDb 24 caracteres, idFirebase 20 caracteres
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(new
            {
                Guid = Guid.NewGuid(),
                _id = ObjectId.GenerateNewId(),
                idMongoDb = ObjectId.GenerateNewId().ToString(),
                idFireBase = GenerateFirebaseId()
            });
        }

        private string GenerateFirebaseId()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            const int length = 20;
            StringBuilder result = new StringBuilder(length);

            using (RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider())
            {
                byte[] data = new byte[length];
                crypto.GetBytes(data);

                foreach (byte b in data)
                {
                    result.Append(chars[b % chars.Length]);
                }
            }

            return result.ToString();
        }
    }
}