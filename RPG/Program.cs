using Fabrica;
using Personajes;
using System.Text.Json;
using tipopersonaje;
using System.Collections.Generic;

List<personaje>? ListaDePersonajes = new List<personaje>();
string[] nombres = { "Jhin", "Ksante", "Karma","Irelia","Sejuani","Tryndamere","Yasuo","Aphelios","Renekton","Pikachu"};
string[] apodo = { "Lautaro", "Juancho", "Iván","nacho Grande","Guty","Milico","Julieta","Tutu","Anita","Pachi"};
Random random = new Random();
int ronda = 0;
string? NombreArchivo = "personajes.json";
var jsonPersonajes = new PersonajesJSON();

if(jsonPersonajes.existe(NombreArchivo)){
    ListaDePersonajes = jsonPersonajes.leerPersonajes(NombreArchivo);
}
else {
    personaje JugadorNuevo = new personaje();
    var FabricaDeP = new FabricaDePersonajes();
    for(int i=0; i<10;i++){
        if (i == 0 || i == 5 || i == 7 ){
            JugadorNuevo = FabricaDeP.NuevoPersonaje(nombres[i],TipoPersonaje.tirador,apodo[i]);
        }
        if (i == 1 || i == 4 || i == 8){
             JugadorNuevo = FabricaDeP.NuevoPersonaje(nombres[i],TipoPersonaje.tanque,apodo[i]);
        }
        if (i == 2 || i == 6 ){
             JugadorNuevo = FabricaDeP.NuevoPersonaje(nombres[i],TipoPersonaje.Asesino,apodo[i]);
        }
        if (i == 3 || i == 9){
             JugadorNuevo = FabricaDeP.NuevoPersonaje(nombres[i],TipoPersonaje.Peleador,apodo[i]);
        }
        ListaDePersonajes.Add(JugadorNuevo);
    }
    jsonPersonajes.guardarPersonajes(ListaDePersonajes, NombreArchivo);
   
}

Console.WriteLine(" Has elegido al personaje "+ ListaDePersonajes[5].Nombre + " Tambien conocido como "+ ListaDePersonajes[5].Apodo);

/*  Comienza el juego*/
Console.WriteLine("Elija su personaje");
 for (int i = 0; i < ListaDePersonajes.Count; i++)
{
    Console.WriteLine( i + "-" + ListaDePersonajes[i].Apodo);
}
int elegido ;
string? Eleccion = Console.ReadLine();
bool control = int.TryParse(Eleccion, out elegido);

if(control) {
    var Protagonista = ListaDePersonajes[elegido];
    ListaDePersonajes.Remove(Protagonista);
    int saludOriginal = Protagonista.Salud;
    do {
        if(ronda == 9){
            break;
        }
        Console.WriteLine("Ha elegido a " + Protagonista.Apodo);
        var enemigo = new personaje();
        int IndiceEnemigo = random.Next(0,9-ronda);
        enemigo = ListaDePersonajes[IndiceEnemigo];
        Console.WriteLine("Peleas contra " + enemigo.Apodo);
        Console.WriteLine("\n -----------------------------Inicio De combate------------------------ \n");
        do {
            Console.Write("Tu HP es "+ Protagonista.Salud + "\n");
            Console.Write("la HP del enemigo es "+ enemigo.Salud +"\n");
            int ataque =0;
            int defensa =0;
            int Efectividad =0;
            int Balance = 350 ;
            int danio =0;
            int Turno = random.Next(1,3);
            int DanioCritico = 0;
            string? critico = "0";

            if (Turno == 1) {
                ataque = Protagonista.Destreza * Protagonista.Fuerza * Protagonista.Nivel ;
                Efectividad = random.Next(20,101);
                defensa = enemigo.Armadura * enemigo.Velocidad;
                danio = ((ataque*Efectividad)-defensa)/Balance;
                Console.WriteLine("Ingrese un Numero para realizar critico");
                critico = Console.ReadLine();
                control = int.TryParse(Eleccion, out DanioCritico);
                if(DanioCritico == random.Next(0,11)){
                        enemigo.Salud = enemigo.Salud - (danio*2);
                        Console.WriteLine("Has hecho "+ danio + " De daño Critico\n");
                }else {
                    enemigo.Salud = enemigo.Salud - (danio);
                    Console.WriteLine("Has hecho "+ danio + " De daño \n");
                }
            } else {
                Console.WriteLine("Ingrese un Numero para recibir critico");
                ataque = enemigo.Destreza * enemigo.Fuerza * enemigo.Nivel;
                Efectividad = random.Next(20,101);
                defensa = Protagonista.Armadura * Protagonista.Velocidad;
                danio = ((ataque*Efectividad)-defensa)/Balance;
                critico = Console.ReadLine();
                control = int.TryParse(Eleccion, out DanioCritico);
                if(DanioCritico == random.Next(0,11)){
                        Protagonista.Salud = Protagonista.Salud - (danio*2);
                        Console.WriteLine("Has recibido "+ danio + " De daño Critico\n");
                }else {
                     Protagonista.Salud = Protagonista.Salud - danio;
                Console.WriteLine("Has recibido "+ danio + " De daño \n");
                }
            }
        }while (Protagonista.Salud > 0 && enemigo.Salud > 0 );
        if (Protagonista.Salud <= 0){
            Console.WriteLine("Has perdido contra " + enemigo.Apodo +", Vuelve a intentarlo la proxima vez");
            break;
        }else {
            Console.WriteLine("Has Ganado a " + enemigo.Apodo +", has recuperado tu salud y has subido de nivel");
            ronda ++;
            Protagonista.Nivel++;
            Protagonista.Salud = saludOriginal;
            Protagonista.Armadura = Protagonista.Armadura + random.Next(1,3);
            Protagonista.Destreza = Protagonista.Destreza + random.Next(1,3);
            Protagonista.Fuerza = Protagonista.Fuerza + random.Next(1,3);
            saludOriginal = Protagonista.Salud;
            Protagonista.Salud = Protagonista.Salud + random.Next(5,13);
            Protagonista.Velocidad = Protagonista.Velocidad + random.Next(1,3);
            ListaDePersonajes.Remove(enemigo);
            if(Protagonista.Salud > 130){
                string Logro1 = "Warmog";
                Console.WriteLine("Has superado los 130 de vida, has ganado el Logro"+ Logro1);
            }
            if(Protagonista.Armadura > 20){
                string Logro2 = "Armadura Petrea";
                 Console.WriteLine("Has superado los 20 de armadura, has ganado el Logro"+ Logro2);
            }
            if(Protagonista.Fuerza > 20){
                string Logro3 = "Drakhtar";
                Console.WriteLine("Has superado los 20 de Fuerza, has ganado el Logro"+ Logro3);
            }
            if(Protagonista.Destreza > 20){
                string Logro4 = "Daga de Stattik";
                Console.WriteLine("Has superado los 20 de destreza, has ganado el Logro"+ Logro4);
            }
            if(Protagonista.Velocidad > 20){
                string Logro5 = "Botas de Jonia";
                Console.WriteLine("Has superado los 20 de Velocidad, has ganado el Logro"+ Logro5);
            }
        }
    }while (ronda != 9);
    if (ronda == 9){
        Console.WriteLine ("Has Finalizado el juego, ahora solo falta Encontrar los Logros");
    }
}