namespace VMtz84.Pizzas.Entities
{
    public class ProductoEntity
    {
        public string Nombre { get; set; }        
        public string Ingredientes { get; set; }        
        public decimal Precio { get; internal set; }
        public int Id { get; internal set; }
        public string Descripcion { get; internal set; }
        public string Ruta { get; internal set; }
        public string Menu { get; internal set; }
    }

    public class PizzaEntity
    {
        public int PizzaId { get; set; }
        public int Pizza2Id { get; set; }
        public string Nombre1 { get; set; }
        public string Nombre2 { get; set; }
        public string Tamanio { get; set; }
        public string Masa { get; set; }
        public decimal Precio { get; set; }
    }
}
