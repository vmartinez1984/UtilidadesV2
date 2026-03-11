namespace VMtz84.Pizzas.Dtos
{
    public class ProductoDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Ingredientes { get; set; }
        public string Ruta { get; set; }
        public string Menu { get; internal set; }
        public int Precio { get; internal set; }
    }

    public class MasaDto
    {
        public int Id { get; set; }

        public string Nombre { get; set; }

        public string Descripcion { get; set; }
    }

    public class TamanioDto
    {
        public int Id { get; set; }

        public string Descripcion { get; set; }

        public decimal Precio { get; set; }
    }
}
