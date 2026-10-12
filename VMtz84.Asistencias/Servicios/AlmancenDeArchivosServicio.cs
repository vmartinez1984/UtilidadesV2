using Microsoft.AspNetCore.Http;

namespace VMtz84.Asistencias.Servicios
{
    public class AlmancenDeArchivosServicio
    {
        private string webRootPat;
        private string contenedor;
        public void ColocarCarpeta(string ruta, string contenedor)
        {
            this.webRootPat = ruta;
            this.contenedor = contenedor;
        }

        public async Task<string> GuardarLocal(string nombreDelArchivo, IFormFile formFile)
        {
            string folder = Path.Combine(webRootPat, contenedor);
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }
            string ruta = Path.Combine(folder, nombreDelArchivo);
            //using (var memoryStream = new MemoryStream())
            //{
            //    await formFile.CopyToAsync(memoryStream);
            //    var contenido = memoryStream.ToArray();
            //    await File.WriteAllBytesAsync(ruta, contenido);
            //}
            await using (var stream = new FileStream(
                ruta,
                FileMode.Create,
                FileAccess.Write,
                FileShare.None,
                64 * 1024 * 1024,
                useAsync: true))
            {
                await formFile.CopyToAsync(stream);
            }
            return $"{contenedor}/{nombreDelArchivo}";
        }

        public async Task<byte[]> Obtener(string ruta)
        {
            byte[] bytes;

            ruta = Path.Combine(webRootPat, ruta);
            bytes = await File.ReadAllBytesAsync(ruta);

            return bytes;
        }

        public async Task<string> Guardar(string nombreDelArchivo, IFormFile formFile)
        {
            using var client = new HttpClient();
            var baseUrl = "https://almacen-de-objetos.vmartinez84.xyz";
            var url = $"{baseUrl}/api/Archivos/{contenedor}";
            using var request = new HttpRequestMessage(HttpMethod.Post, url);
            request.Headers.Add("accept", "*/*");
            using var content = new MultipartFormDataContent();
            // Agregar el archivo recibido como argumento
            var streamContent = new StreamContent(formFile.OpenReadStream());
            streamContent.Headers.ContentType =
                new System.Net.Http.Headers.MediaTypeHeaderValue(formFile.ContentType ?? "application/octet-stream");
            content.Add(streamContent, "FormFile", formFile.FileName);
            // Agregar el nombre del archivo
            content.Add(new StringContent(nombreDelArchivo), "NombreDelArchivo");
            request.Content = content;
            using var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var respuesta = await response.Content.ReadAsStringAsync();
            Console.WriteLine(respuesta);

            return $"{baseUrl}/{contenedor}/{nombreDelArchivo}";
        }
    }
}
