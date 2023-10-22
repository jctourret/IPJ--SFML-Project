using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace MyPrimerJuegoSFML
{
    enum PlayerStates
    {
        idle,
        moveUp,
        moveRight,
        moveDown,
        moveLeft
    }
    //Nuestro player hereda de GameObjectBase
    internal class Player : GameObjectBase
    {
        //SFML tiene un set de formas basicas como rectangulos y circulos.
        public RectangleShape _body;
        Color innerColor;
        Color outerColor;

        //Estado actual del jugador, esto va a ser lo que domine que animacion de nuestra lista se ejecuta.
        PlayerStates _currentState = PlayerStates.idle;

        //En esta 'lista' vamos a guardar todas nuestras animaciones.
        
        //Una lista es un array dynamico en el cual cada elemento (en este caso cada frame)
        //guarda una refencia a la posicion en memoria del siguiente elemento.
        //esto las hace extremadamente flexibles, pero muy costosas de usar.
        List<Animation> animations;

        float speed = 100;

        public Player(float posX, float posY, float sizeX, float sizeY)
        {
            //Creamos nuestro cuerpo
            _body = new RectangleShape(new Vector2f(sizeX,sizeY));
            
            //Si bien el calculo para conseguir el centro de nuestra forma es new Vector2f(posX+(_body.Size.X/2), posY+(_body.Size.Y/2))

            //Origin necesita estar calculado de una forma que debe ignorar toda transformacion, un cambio de posicion es una transformacion,
            //por lo que tenemos que usar solo _body.Size.Y/2
            _body.Origin = new Vector2f(_body.Size.X/2, _body.Size.Y/2);
            //Nos ubicamos en el mundo
            _body.Position = new Vector2f(posX, posY);

            //Creamos nuestra sprite sheet en base a una direccion en nuestra computadora.
            //Para acceder de forma sencilla a esta la sprite sheet debe estar en nuestra carpeta de proyecto
            //en la carpeta bin(Binaries) donde esta nuestro exe.

            //Idealmente no deberia estar el archivo ahi tirado, si no que es una carpeta llamada 'res' (resources)
            //pero eso lo vamos a hacer en clase.
            texture = new Texture("YellowMan.png");

            //Creamos nuestra sprite vacia.
            sprite = new Sprite();

            //creamos nuestra lista de animaciones.
            animations = new List<Animation>();

            //Agregamos nuestras animacion de idle, usando su constructor y la funcion Add() de las listas.
            animations.Add(new Animation(texture,50,50,1,4,2,5,1)); // Idle

        }

        public void Update(Time deltaTime)
        {
            //Updateamos nuestra animacion
            sprite = animations[(int)_currentState].updateAnimation(deltaTime);

            //Recibimos input y seteamos la dirrecion de nuestro jugador.
            Vector2f direction = new Vector2f();
            if (Keyboard.IsKeyPressed(Keyboard.Key.Up))
            {
                direction.Y = -1;
            }
            if (Keyboard.IsKeyPressed(Keyboard.Key.Down))
            {
                direction.Y = 1;
            }
            if (Keyboard.IsKeyPressed(Keyboard.Key.Left))
            {
                direction.X = -1;
            }
            if (Keyboard.IsKeyPressed(Keyboard.Key.Right))
            {
                direction.X = 1;
            }
            //Problema: Mi player es mas rapido en diagonal.
            //Solucion: Aplicar matematicas de vectores.

            //Lo primero que hacemos es sacar la magnitud de un vector, es decir, su largo total.
            float dirMag = MathF.Sqrt(direction.X*direction.X+direction.Y*direction.Y);
            
            //Pensando un vector como una flecha, la magnitud seria su largo y la direccion es hacia donde apunta.
            //La magnitud es un numero y la direccion es una coordenada.

            // vector = (direccion.x, direccion.y) * magnitude;

            // Para que el calculo refleje correctamente un vector, debemos normalizar su direccion,
            // para que la magnitude misma de este sea siempre 1.
            if (dirMag > 0) // Si nos estamos moviendo....
            {
                //normalizar mi direccion en base a la magnitud de la direccion.
                Vector2f dirNormalized = direction / dirMag;

                //Ahora que tenemos nuestra direccion normalizada,
                //podemos multiplicarla por las magnitudes 'speed' y 'deltaTime'

                //deltaTime es necesario para que nuestro juego sea independiente
                //de los fotogramas por segundo.
                
                _body.Position += dirNormalized * speed * deltaTime.AsSeconds() ;
            }
            //Hacemos que nuestra sprite siga a nuestro cuerpo.
            sprite.Position = _body.Position;
        }

        public void Draw(RenderWindow playerWindow)
        {
            //Dibujamos nuestro Player
            playerWindow.Draw(_body);
            playerWindow.Draw(sprite);
        }
    }
}
