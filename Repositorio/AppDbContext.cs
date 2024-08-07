using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace utilidadesv2.Repositorio
{
    public class AppDbContext : DbContext
    {
        private readonly string _connectionString;
        private readonly string _db;


        public DbSet<ApellidoNombre> ApellidoNombre { get; set; }

        /// <summary>
        /// Para hacer operaciones con entityframework seleccione el orden de la cadena a ejecutar en ultimo lugar
        /// </summary>
        public AppDbContext()
        {
            _db = "SqlServerNombresYApellidos";
            _connectionString = "Data Source=192.168.1.86;Initial Catalog=Utilidades; Persist Security Info=True;User ID=sa;Password=Macross#2012; TrustServerCertificate=True;";//SqlServer
            _db = "MySqlNombresYApellidos";
            _connectionString = "Server=vmartinez84.xyz; Port=3306; Database=vmartinez_codigos_postales; Uid=vmartinez_CodigosPostales; Pwd=Macross#2012;";//MySql
            _connectionString = "Server=localhost; Port=3306; Database=Utilidades; Uid=root; Pwd=FesAragon#2024;";//MySql
        }

        public AppDbContext(IConfiguration configuration)
        {
            _db = configuration.GetConnectionString("DbNombresyApellidos");
            _connectionString = configuration.GetConnectionString(_db);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                switch (_db)
                {
                    case "MySqlNombresYApellidos":
                        optionsBuilder.UseMySql(_connectionString, ServerVersion.AutoDetect(_connectionString));
                        break;
                    case "SqlServerNombresYApellidos":
                        optionsBuilder.UseSqlServer(_connectionString);
                        break;
                    default:
                        break;
                }
            }
        }
    }

    public class ApellidoNombre
    {
        [NotMapped]
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; }
        public int Id { get; set; }

        public string Dato { get; set; }

        public string Tipo { get; set; }
    }
}