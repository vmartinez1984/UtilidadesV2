using Firebase.Auth;
using Firebase.Auth.Providers;
using Firebase.Storage;
using Microsoft.AspNetCore.Http;

namespace Peliculas.Bl
{
    public class AlmacenDeArchivos
    {
        public AlmacenDeArchivos()
        {
            
        }

        string authDomain = "archivos-54624.firebaseapp.com";
        string apikey = "AIzaSyAEnLgwQe4B-zwq-WYx0IMYHIYhcHyCkzw";
        string email = "vmartinez@gmail.com";
        string password = "123456";
        string token;
        string rutaDelStorage = "archivos-54624.appspot.com";
        string contenedor = "Peliculas";

        public async Task Borrar(string nombre)
        {
            await ObtenerToken();
            await new FirebaseStorage(
                    rutaDelStorage,
                    new FirebaseStorageOptions
                    {
                        AuthTokenAsyncFactory = () => Task.FromResult(token),
                        ThrowOnCancel = true
                    }
                )
                .Child(contenedor)
                .Child(nombre)
                .DeleteAsync();
        }

        public async Task<string> EditarArchivo(string nombre, IFormFile formFile)
        {
            string url;

            await Borrar(nombre);
            url = await Guardar(nombre, formFile);

            return url;
        }

        public async Task<string> Guardar(string nombre, IFormFile formFile)
        {            
            await ObtenerToken();
            var downloadURL = await new FirebaseStorage(
                    rutaDelStorage,
                    new FirebaseStorageOptions
                    {
                        AuthTokenAsyncFactory = () => Task.FromResult(token),
                        ThrowOnCancel = true
                    }
                )
                .Child(contenedor)
                .Child(nombre)
                .PutAsync(formFile.OpenReadStream());

            return downloadURL;
        }

        public async Task<byte[]> ObtenerBytes(string ruta)
        {
            byte[] bytes = null;
            HttpClient client;

            client = new HttpClient();
            bytes = await client.GetByteArrayAsync(ruta);

            return bytes;
        }

        private async Task ObtenerToken()
        {
            var client = new FirebaseAuthClient(new FirebaseAuthConfig()
            {
                ApiKey = apikey,
                AuthDomain = authDomain,
                Providers = new FirebaseAuthProvider[]
                {
                    new EmailProvider()
                }
            });

            var credenciales = await client.SignInWithEmailAndPasswordAsync(email, password);
            token = await credenciales.User.GetIdTokenAsync();
        }
    }
}
