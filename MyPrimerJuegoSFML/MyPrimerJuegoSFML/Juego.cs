using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace MyPrimerJuegoSFML
{
    internal class Juego
    {
        Player chespirito;

        Clock deltaClock;
        Time deltaTime;

        RenderWindow window;
        VideoMode videoMode;
        
        //No puedo usar variable que puedan ir a negativo.
        //Unsigned: mismo dato pero sin negativos.
        uint screenWidth;
        uint screenHeight;

        public void Run()
        {
            // Start the game loop
            while (window.IsOpen)
            {
                window.DispatchEvents();
                Update();
                Draw();
            }
        }

        public Juego()
        {
            screenWidth = 800;
            screenHeight = 600;


            deltaClock = new Clock();

            deltaTime = new Time();
            // Create the main window
            videoMode = new VideoMode(screenWidth, screenHeight);
            window = new RenderWindow(videoMode, "Mi Primer Juegito");

            window.SetFramerateLimit(60);
            window.SetVerticalSyncEnabled(true);

            chespirito = new Player(10,10,100,100);
        }

        void Update()// Se encarga de la logica de nuestro juego.
                     // Es lo que se ejecuta frame a frame.
        {
            deltaTime = deltaClock.Restart();
            chespirito.Update(deltaTime);
        }

        void Draw() // Es la que encarga de dibujar toda
                    // nuestra geometria en pantalla
        {
            // Clear screen
            window.Clear();// Clear tiene que ser la primera linea.

            chespirito.Draw(window);

            // Update the window
            window.Display();// Display tiene que ser la ultima.
        }

    }
}