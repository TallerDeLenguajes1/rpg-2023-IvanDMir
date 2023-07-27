namespace Fabrica {
    using Personajes;
    using System.Text.Json;
    using tipopersonaje;
    using System.Net;
    using Api;
    public class FabricaDePersonajes {
    static DateTime start = new DateTime(1723, 1, 1);
    static int range = (DateTime.Today - start).Days;
        public personaje NuevoPersonaje(string? Nombre, TipoPersonaje type, string? apodo ) {
            Random random = new Random();
            var Nuevo = new personaje();
            Nuevo.Nombre = Nombre;
            Nuevo.Apodo = apodo;
            Nuevo.Tipo = type;

            if (Nuevo.Tipo ==  TipoPersonaje.Peleador){
                Nuevo.Fuerza=random.Next(7,11);
                Nuevo.Velocidad = random.Next(3,6);
                Nuevo.Destreza = random.Next(1,4);
                Nuevo.Nivel = random.Next(1,5);
                Nuevo.Armadura = random.Next(3,5);
                Nuevo.Salud = random.Next(50,80);
                Nuevo.FecNac = start.AddDays(random.Next(range));
                Nuevo.Edad =  Nuevo.FecNac.Year - start.Year;
            }

                if (Nuevo.Tipo == TipoPersonaje.Asesino){
                Nuevo.Fuerza=random.Next(7,11);
                Nuevo.Velocidad = random.Next(7,11);
                Nuevo.Destreza = random.Next(2,4);
                Nuevo.Nivel = random.Next(1,5);
                Nuevo.Armadura = random.Next(1,3);
                Nuevo.Salud = random.Next(30,55);
                Nuevo.FecNac = start.AddDays(random.Next(range));
                Nuevo.Edad =  Nuevo.FecNac.Year - start.Year;
            }
            if (Nuevo.Tipo == TipoPersonaje.tirador){
                Nuevo.Fuerza=random.Next(9,11);
                Nuevo.Velocidad = random.Next(5,11);
                Nuevo.Destreza = random.Next(4,6);
                Nuevo.Nivel = random.Next(1,5);
                Nuevo.Armadura = random.Next(1,2);
                Nuevo.Salud = random.Next(30,45);
                Nuevo.FecNac = start.AddDays(random.Next(range));
                Nuevo.Edad = start.Year - Nuevo.FecNac.Year;
            }
            if (Nuevo.Tipo == TipoPersonaje.tanque){
                Nuevo.Fuerza=random.Next(5,8);
                Nuevo.Velocidad = random.Next(2,4);
                Nuevo.Destreza = random.Next(1,3);
                Nuevo.Nivel = random.Next(1,5);
                Nuevo.Armadura = random.Next(8,11);
                Nuevo.Salud = random.Next(80,101);
                Nuevo.FecNac = start.AddDays(random.Next(range));
                Nuevo.Edad = start.Year - Nuevo.FecNac.Year;
            }

            return Nuevo;
        }

    }
    public class PersonajesJSON {
        public void guardarPersonajes(List<personaje> Lista, string fileName){
            string json = JsonSerializer.Serialize(Lista);
            File.WriteAllText(fileName,json);
        }

        public List<personaje>? leerPersonajes(string fileName)
        {
            if (File.Exists(fileName))
            {

                String jsonString = File.ReadAllText(fileName);
                List<personaje>? personajesDeserializados = JsonSerializer.Deserialize<List<personaje>>(jsonString);
                return personajesDeserializados;

            }
            else
            {
                System.Console.WriteLine("El archivo de nombre: " + fileName + " no existe");
                return null;
            }
        }
        public bool existe(string? fileName)
        {
            if (File.Exists(fileName))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
public void mostrarPersonajes(List<personaje> lista)
        {
            foreach (var personaje in lista)
            {
                System.Console.WriteLine("Jugador de nombre: " + personaje.Nombre + ", tambien conocido como: " + personaje.Apodo +", "+ personaje.Edad + "Años");
                System.Console.WriteLine("VEL.: " + personaje.Velocidad + " Destreza: " + personaje.Destreza + " Fuerza: " + personaje.Fuerza);
                System.Console.WriteLine("Nivel: " + personaje.Nivel + " Armadura: " + personaje.Armadura + " Salud: " + personaje.Salud);

            }
        }
}
    public static class usoAPI {
        public static double? TraerProbabilidad() {
            var url = $"https://api.genderize.io/?name=luc" ;
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.ContentType = "*/*";
            request.Accept = "*/*";
            double probabilidad = 0;
        
        try{
            using(HttpWebResponse Respuesta = (HttpWebResponse)request.GetResponse()){
                using(StreamReader Leedor = new StreamReader (Respuesta.GetResponseStream())){
                    var texto = Leedor.ReadToEnd();
                    Root Item = JsonSerializer.Deserialize<Root>(texto);
                    probabilidad = Item.count;
                }
            }
        }catch (WebException Ex){
            Console.WriteLine("Problemas a la hora de recibir probabilidad");
        }
    return probabilidad;
    }
}
}
