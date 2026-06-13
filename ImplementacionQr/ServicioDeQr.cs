using System.Drawing;
using ZXing;
using ZXing.Windows.Compatibility;

namespace ImplementacionQr
{
    public class ServicioDeQr
    {
        public byte[] GenerateQrCode(string mensaje, int width = 400, int height = 400, int margin = 10)
        {
            byte[] bytes;
            // Generar el código de barras
            BarcodeWriter barcodeWriter = new BarcodeWriter();
            barcodeWriter.Format = BarcodeFormat.QR_CODE;
            barcodeWriter.Options.Width = width; // ancho de la imagen
            barcodeWriter.Options.Height = height; // alto de la imagen
            barcodeWriter.Options.Margin = margin;
            using (Bitmap bitmap = barcodeWriter.Write(mensaje))
            {
                // Convertir la imagen a un arreglo de bytes

                using (MemoryStream ms = new MemoryStream())
                {
                    bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                    // Guardar en el MemoryStream
                    bytes = ms.ToArray(); // Convertir a byte array
                }
            }

            return bytes;
        }
    }
}