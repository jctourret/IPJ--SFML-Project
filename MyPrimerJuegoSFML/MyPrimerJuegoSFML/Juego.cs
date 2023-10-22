using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace MyPrimerJuegoSFML
{
    internal class Juego
    {
        //Declaramos a nuestro jugador.
        Player chespirito;

        //Clock maneja los tiempos de nuestro juego
        
        //Eventualmente estaria bueno hacer nuestro
        //propio reloj para entender como funciona el calculo.
        Clock deltaClock;
        Time deltaTime;

        //Declaramos la ventana asi como los settings de video.
        RenderWindow window;
        VideoMode videoMode;
        
        //Declaramos variables que guardaran las dimensiones de nuestra ventana. 
        //No puedo usar variable que puedan ir a negativo
        //para la resolucion de nuestro juego.
        //Unsigned: mismo dato pero sin negativos y mayor capacidad.
        uint screenWidth;
        uint screenHeight;

        public void Run()
        {
            // Comenzamos el loop dependiendo de que la ventana este abierta.
            while (window.IsOpen)
            {
                //El event dispatcher hace que todo funcione en SFML
                //Si no me creen, comentenlo y fijense :P
                window.DispatchEvents();

                //Idealmente, aca deberiamos tener un switch que cambie
                //entre nuestras diferentes 'escenas' y que estas
                //corran su propio run, que dentro deberia tener su propio
                //Update() y Draw(), pero por ahora podemos trabajar asi.

                //Corremos el update de nuestro juego
                Update();
                //Dibujamos nuestro juego.
                Draw();
            }
        }

        public Juego()
        {
            //Seteamos las dimensiones
            screenWidth = 800;
            screenHeight = 600;

            //Creamos el tiempo y el reloj.
            deltaClock = new Clock();

            deltaTime = new Time();
            // Creamos los settings y despues con ellos la ventana.
            videoMode = new VideoMode(screenWidth, screenHeight);
            window = new RenderWindow(videoMode, "Mi Primer Juegito");

            //Closed es uno de los tantos eventos que contiene nuestra ventana.

            //Los eventos pueden por ahora pensarlos como ciertas partes de nuestro
            //codigo gritando "HEY, PASO ESTO!". Esto es util por que podemos hacer 
            //que nuestro codigo reaccione a este grito.

            //En este caso, agremamos Window_Close a la lista de funciones que van a escuchar
            //cuando el boton X de nuestra ventana grite "HEY! ME APRETARON!" para cerrar la ventana.
            window.Closed += Window_Closed;

            //Seteamos el limite de frames, por que pinto (⌐■ʖ■)
            window.SetFramerateLimit(60);

            //Vsync(sincronizacion vertical) es un metodo de renderizado
            //en el cual se sincroniza el la tasa de refresco de nuestra pantalla 
            //con los frames por segundo de nuestro juego para evitar anomalias visuales

            //Lo que les mencione en clase sobre el front y back buffer en realidad
            //es otra tecnica llamada "Double Buffering". Perdon, se me cruzaron 
            //los cables en el cerebro. Σ(-∧-；)

            //Ya fue, tambien pongamos Vsync (⌐■ʖ■)
            window.SetVerticalSyncEnabled(true);
            
            //Creamos al player.
            chespirito = new Player(100,100,100,100);
        }

        //Funcion que escucha al evento Closed.
        private void Window_Closed(object? sender, EventArgs e)
        {
            window.Close();
        }

        void Update()// Se encarga de la logica de nuestro juego.
                     // Es lo que se ejecuta frame a frame.
        {
            //Reseteando el reloj podemos conseguire el tiempo entre frames.
            deltaTime = deltaClock.Restart();
            //Updateamos al player
            chespirito.Update(deltaTime);
        }

        void Draw() // Es la que encarga de dibujar toda
                    // nuestra geometria en pantalla
        {
            // Clear screen
            window.Clear();// Clear tiene que ser la primera linea.

            //Dibujamos a nuestro player corriendo su funcion.
            chespirito.Draw(window);

            // Update the window
            window.Display();// Display tiene que ser la ultima.
        }

    }
}