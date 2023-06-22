namespace Fabrica {
    using Personajes;
    using System.Text.Json;

    public class FabricaDePersonajes {

        public personaje NuevoPersonaje(string? Nombre, Type Type, string? apodo ) {
            Random random = new Random();
            var Nuevo = new personaje();
            Nuevo.Nombre = Nombre;
            Nuevo.Apodo = apodo;
            Nuevo.Tipo = Type;

            if ( Nuevo.Tipo == Type.Asesino){

            }
        }

    }
}