using Fabrica;
using Personajes;
using tipopersonaje;
using System.IO;
using Api;

List<personaje>? ListaDePersonajes = new List<personaje>();
string[] nombres = { "Jhin", "Ksante", "Karma","Irelia","Sejuani","Tryndamere","Yasuo","Aphelios","Renekton","Pikachu"};
string[] apodo = { "Lautaro", "Juancho", "Iván","nacho Grande","Guty","Milico","Valentina","Tutu","Anita","Pachi"};
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


/*  Comienza el juego*/
Console.WriteLine("Elija su personaje");
 for (int i = 0; i < ListaDePersonajes.Count; i++)
{
    Console.WriteLine( i + "-" + ListaDePersonajes[i].Nombre);
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
        Console.WriteLine("Eres " + Protagonista.Apodo);
        var enemigo = new personaje();
        int IndiceEnemigo = random.Next(0,9-ronda);
        enemigo = ListaDePersonajes[IndiceEnemigo];
        Console.WriteLine("Peleas contra " + enemigo.Apodo);
        //Escalamiento de los enemigos segun la ronda
        enemigo.Armadura = enemigo.Armadura + ronda;
        enemigo.Destreza = enemigo.Destreza + ronda;
        enemigo.Velocidad = enemigo.Armadura + ronda;
        enemigo.Fuerza = enemigo.Destreza + ronda;
        enemigo.Nivel = enemigo.Nivel + ronda;
        enemigo.Salud = enemigo.Salud + ronda*3;

        Console.WriteLine("\n -----------------------------Inicio De combate------------------------ \n");
        do {
            Console.Write("Tu HP es "+ Protagonista.Salud + "\n");
            Console.Write("la HP del enemigo es "+ enemigo.Salud +"\n");
            int ataque =0;
            int defensa =0;
            int Efectividad =0;
            int Balance = 250 ;
            int danio =0;
            int Turno = random.Next(1,3); 
            int DanioCritico = 0;
            string? critico = "0"; // La accion de critico funciona de tal manera que despues se lo iguala con un numero random entre 1 y 10, si coincide con el ingresado por el usario, el daño se duplica.
            critico = Console.ReadLine();
            if (Turno == 1) {
                ataque = Protagonista.Destreza * Protagonista.Fuerza * Protagonista.Nivel ;
                Efectividad = random.Next(20,101);
                defensa = enemigo.Armadura * enemigo.Velocidad;
                danio = ((ataque*Efectividad)-defensa)/Balance;
                control = int.TryParse(Eleccion, out DanioCritico);
                if(danio ==0){
                    Console.WriteLine("El enemigo ha esquivado tu ataque");
                }
                if(DanioCritico == random.Next(0,11) && danio != 0){
                        enemigo.Salud = enemigo.Salud - (danio*2);
                        Console.WriteLine("Has hecho "+ danio + " De daño Critico\n");
                }else {
                    enemigo.Salud = enemigo.Salud - (danio);
                    Console.WriteLine("Has hecho "+ danio + " De daño \n");
                }
            } else {
                ataque = enemigo.Destreza * enemigo.Fuerza * enemigo.Nivel;
                Efectividad = random.Next(20,101);
                defensa = Protagonista.Armadura * Protagonista.Velocidad;
                danio = ((ataque*Efectividad)-defensa)/Balance;
                control = int.TryParse(Eleccion, out DanioCritico);
                  if(danio ==0){
                    Console.WriteLine("has esquivado el ataque enemigo");
                }
                if(DanioCritico == random.Next(0,11) && danio !=0){
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
    
            var Probabilidad = usoAPI.TraerProbabilidad();
            double? objetoDouble = Probabilidad % random.Next(0,51);
            int objeto = (int)objetoDouble; 
            Console.WriteLine("Su Objeto es " + objeto);
            

            Console.WriteLine("Has Ganado a " + enemigo.Apodo +", has recuperado tu salud y has subido de nivel");
             if (objeto == 0){
                Console.WriteLine("Has encontrado Un Filo infinito,¡¡tu Fuerza Aumenta en 5 !!");
                Protagonista.Fuerza += 5;
            }
            if (objeto == 10){
                Console.WriteLine("Has encontrado Un Cefiro,¡¡tu Destreza Aumenta en 4 !!");
                Protagonista.Destreza += 4;
            }
            if (objeto == 20){
                Console.WriteLine("Has encontrado Un Corazon de hierro,¡¡tu salud Aumenta en 15 !!");
                Protagonista.Salud += 15;
            }
             if (objeto == 30){
                Console.WriteLine("Has encontrado un presagio de Randuin,¡¡tu armadura Aumenta en 5 !!");
                Protagonista.Armadura += 5;
            }
            if (objeto == 40){
                Console.WriteLine("Has encontrado una Daga de Stattik,¡¡ tu velocidad a aumentado en 7!!");
                Protagonista.Velocidad+=7;
            }
            if (objeto == 50){
                Console.WriteLine("Has encontrado La maldicion de Nilah,¡¡ Has subido 2 niveles!!");
                Protagonista.Nivel+=2;
            }
            ronda ++;
            Protagonista.Nivel++;
            Protagonista.Salud = saludOriginal;
            Protagonista.Armadura = Protagonista.Armadura + random.Next(1,3);
            Protagonista.Destreza = Protagonista.Destreza + random.Next(1,3);
            Protagonista.Fuerza = Protagonista.Fuerza + random.Next(1,3);
            Protagonista.Salud = Protagonista.Salud + random.Next(5,25);  
            saludOriginal = Protagonista.Salud;
            Protagonista.Velocidad = Protagonista.Velocidad + random.Next(1,3);
            ListaDePersonajes.Remove(enemigo);
            /* Logros desbloqueables */
           
        }
    }while (ronda != 9);
    if (ronda == 9){
        Console.WriteLine ("Has Finalizado el juego, ahora solo falta Encontrar los Logros ");
    }
    /* Logros desbloqueables */
            if(Protagonista.Salud > 200){
                string Logro1 = "Warmog";
                Console.WriteLine("Has superado los 130 de vida, has ganado el Logro "+ Logro1);
            }
            if(Protagonista.Armadura > 20){
                string Logro2 = "Armadura Petrea";
                 Console.WriteLine("Has superado los 20 de armadura, has ganado el Logro "+ Logro2);
            }
            if(Protagonista.Fuerza > 20){
                string Logro3 = " Hoja Crepuscular de Drakhttar";
                Console.WriteLine("Has superado los 20 de Fuerza, has ganado el Logro "+ Logro3);
            }
            if(Protagonista.Destreza > 20){
                string Logro4 = "Filo De la noche";
                Console.WriteLine("Has superado los 20 de destreza, has ganado el Logro "+ Logro4);
            }
            if(Protagonista.Velocidad > 20){
                string Logro5 = "Botas de Jonia";
                Console.WriteLine("Has superado los 20 de Velocidad, has ganado el Logro "+ Logro5);
            }
    
}
// Para que cada vez que se termine el juego, los personajes se eliminen asi cuando se ejecuta el juego, se crean nuevos personajes.
File.Delete("personajes.json");