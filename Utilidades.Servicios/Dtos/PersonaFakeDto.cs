namespace Utilidades.Servicios.Dtos
{
    public class PersonaFakeDto
    {
        public string idFirebase { get; set; }
        public string idMongoDb { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MobilePhone { get; set; }
        public string EmailAddress { get; set; }
        public string BirthDate { get; set; }
        public string PrimerApellido { get; set; }
        public string EstadoCivilDatosCliente { get; set; }
        public string Genero { get; set; }
        public string Rfc { get; set; }
        public string Curp { get; set; }
        public string LugarDeNacimiento { get; set; }
        public string SegundoApellido { get; set; }
        public string Calle { get; set; }
        public string Colonia { get; set; }
        public string CodigoPostal { get; set; }
        public string Estado { get; set; }
        public string Municipio { get; set; }
        public string NumeroInterior { get; set; }
        public string NumeroExterior { get; set; }
        public string GeneroInicial { get; set; }
        public string GeneroNombre { get; set; }

        public string GeneroMasculinoFemenino { get { return this.GeneroNombre == "Hombre" ? "Masculino" : "Femenino"; } }
        public string GeneroMasculinoFemeninoInicial { get { return this.GeneroNombre == "Hombre" ? "M" : "F"; } }

        public Guid Guid { get; internal set; }
    }
}
