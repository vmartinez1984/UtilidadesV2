using Microsoft.EntityFrameworkCore;

namespace utilidadesv2.Repositorio
{
    public class RepositorioDeNombresYApellidos
    {
        private readonly AppDbContext _dbContext;

        public RepositorioDeNombresYApellidos(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<string> ObtenerApellidoAleatorioAsync()
        {
            int total;
            int numeroRandom;
            int inicio;
            Random random = new Random();
            ApellidoNombre apellidoNombre;

            total = _dbContext.ApellidoNombre.Where(x => x.Tipo == "Apellido").Count();
            inicio = (await _dbContext.ApellidoNombre.Where(x => x.Tipo == "Apellido").FirstOrDefaultAsync()).Id;
            do
            {
                numeroRandom = random.Next(inicio, total + inicio);
                apellidoNombre = await _dbContext.ApellidoNombre.Where(x => x.Id == numeroRandom).FirstOrDefaultAsync();
            } while (apellidoNombre == null);

            return apellidoNombre.Dato;
        }

        public async Task<string> ObtenerNombre(int genero)
        {
            int total;
            int numeroRandom;
            int inicio;
            Random random = new Random();
            ApellidoNombre apellidoNombre;
            string tipo;

            if (genero == 0)
                tipo = "Nombre mujer";
            else
                tipo = "Nombre";

            total = _dbContext.ApellidoNombre.Where(x => x.Tipo == tipo).Count();
            inicio = (await _dbContext.ApellidoNombre.Where(x => x.Tipo == tipo).FirstOrDefaultAsync()).Id;
            do
            {
                numeroRandom = random.Next(inicio, total + inicio);
                apellidoNombre = await _dbContext.ApellidoNombre.Where(x => x.Id == numeroRandom).FirstOrDefaultAsync();
            } while (apellidoNombre == null);

            return apellidoNombre.Dato;
        }

        public async Task AgregarNombresYApellidos()
        {
            List<string> lines;
            int j;

            j = 0;

            Console.WriteLine("Insertar nombre y apellidos");
            _dbContext.Database.ExecuteSqlRaw("TRUNCATE TABLE ApellidoNombre");
            await _dbContext.SaveChangesAsync();
            lines = Apellido.ObtenerTodos();
            for (int i = 0; i < lines.Count(); i++)
            {

                ApellidoNombre entity;

                Console.WriteLine($"{i}  ->" + lines[i]);
                entity = new ApellidoNombre
                {
                    Dato = lines[i],
                    Tipo = "Apellido"
                };
                _dbContext.ApellidoNombre.Add(entity);
                if (j == 10000)
                {
                    await _dbContext.SaveChangesAsync();
                    j = 0;
                }
                j++;
            }
            await _dbContext.SaveChangesAsync();

            Console.WriteLine("Insertando nombres");
            lines = Nombre.ObtenerTodos();
            j = 0;

            for (int i = 0; i < lines.Count(); i++)
            {

                ApellidoNombre entity;

                Console.WriteLine($"{i} ->" + lines[i]);
                entity = new ApellidoNombre
                {
                    Dato = lines[i],
                    Tipo = "Nombre"
                };
                _dbContext.ApellidoNombre.Add(entity);
                if (j == 10000)
                {
                    await _dbContext.SaveChangesAsync();
                    j = 0;
                }
                j++;
            }
            await _dbContext.SaveChangesAsync();

            Console.WriteLine("Insertando nombres de mujer");
            lines = Nombre.ObtenerTodas();
            j = 0;

            for (int i = 0; i < lines.Count(); i++)
            {

                ApellidoNombre entity;

                Console.WriteLine($"{i} ->" + lines[i]);
                entity = new ApellidoNombre
                {
                    Dato = lines[i],
                    Tipo = "Nombre mujer"
                };
                _dbContext.ApellidoNombre.Add(entity);
                if (j == 10000)
                {
                    await _dbContext.SaveChangesAsync();
                    j = 0;
                }
                j++;
            }
            await _dbContext.SaveChangesAsync();
            Console.WriteLine("Terminado");
        }
    }
}