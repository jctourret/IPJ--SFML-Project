using SFML.Graphics;
using SFML.Window;

namespace MyPrimerJuegoSFML
{
    internal class Juego
    {
        Player chespirito;

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

            // Create the main window

            videoMode = new VideoMode(screenWidth, screenHeight);
            window = new RenderWindow(videoMode, "Mi Primer Juegito");

            window.SetFramerateLimit(60);
            window.SetVerticalSyncEnabled(true);

            chespirito = new Player(screenWidth/2,screenHeight/2);
        }

        void Update()// Se encarga de la logica de nuestro juego.
                     // Es lo que se ejecuta frame a frame.
        {
            chespirito.Update();
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