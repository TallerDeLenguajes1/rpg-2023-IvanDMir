using Fabrica;
using Personajes;
using System.Text.Json;
using tipopersonaje;
using System.Collections.Generic;

List<personaje> ListaDePersonajes = new List<personaje>();
string[] nombres = { "Jhin", "Ksante", "Karma","Irelia","Sejuani","Teemo","Yasuo"};
string[] apodo = { "m0rfito", "wewinagain", "CristianLana12","PeronistKiller","OPTYasuo","Piratajack","Gabimatagato"};

string? fileName = "personajes.json";
var jsonPersonajes = new PersonajesJSON();

if(jsonPersonajes.existe(fileName)){
    List<personaje>? PersonajeDeserializados = jsonPersonajes.leerPersonajes(fileName);
    if (PersonajeDeserializados != null){
        jsonPersonajes.mostrarPersonajes(PersonajeDeserializados);
    }
}
else {
    personaje JugadorNuevo = new personaje();
    var FabricaDeP = new FabricaDePersonajes();
    for(int i=0; i<7;i++){
        if (i == 0 || i == 5){
            JugadorNuevo = FabricaDeP.NuevoPersonaje(nombres[i],TipoPersonaje.tirador,apodo[i]);
        }
        if (i == 1 || i == 4){
             JugadorNuevo = FabricaDeP.NuevoPersonaje(nombres[i],TipoPersonaje.tanque,apodo[i]);
        }
        if (i == 2 || i == 6 ){
             JugadorNuevo = FabricaDeP.NuevoPersonaje(nombres[i],TipoPersonaje.Asesino,apodo[i]);
        }
        if (i == 3){
             JugadorNuevo = FabricaDeP.NuevoPersonaje(nombres[i],TipoPersonaje.Peleador,apodo[i]);
        }
        ListaDePersonajes.Add(JugadorNuevo);
    }
    jsonPersonajes.guardarPersonajes(ListaDePersonajes, fileName);
    jsonPersonajes.mostrarPersonajes(ListaDePersonajes);
}