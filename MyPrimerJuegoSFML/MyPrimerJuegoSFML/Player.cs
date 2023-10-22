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
    internal class Player : GameObjectBase
    {
        public RectangleShape _body;
        float _radius;
        Color innerColor;
        Color outerColor;


        PlayerStates _currentState = PlayerStates.idle;
        Texture _spriteSheet;
        Sprite _currentSprite;

        List<Animation> animations;

        float speed = 100;

        public Player(float posX, float posY, float sizeX, float sizeY)
        {
            _radius = 30;
            _body = new RectangleShape(new Vector2f(sizeX,sizeY));
            
            //Si bien el calculo para conseguir el centro de nuestra forma es new Vector2f(posX+(_body.Size.X/2), posY+(_body.Size.Y/2))

            //Origin necesita estar calculado de una forma que debe ignorar toda transformacion, un cambio de posicion es una transformacion,
            //por lo que tenemos que usar solo _body.Size.Y/2
            _body.Origin = new Vector2f(_body.Size.X/2, _body.Size.Y/2);
            _body.Position = new Vector2f(posX, posY);

            _spriteSheet = new Texture("YellowMan.png");

            _currentSprite = new Sprite();

            animations = new List<Animation>();

            animations.Add(new Animation(_spriteSheet,50,50,1,4,2,5,1)); // Idle

        }

        public void Update(Time deltaTime)
        {
            _currentSprite = animations[(int)_currentState].updateAnimation(deltaTime);

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
            float dirMag = MathF.Sqrt(direction.X*direction.X+direction.Y*direction.Y);
            
            if (dirMag > 0)
            {
                Vector2f dirNormalized = direction / dirMag;

                _body.Position += dirNormalized * speed * deltaTime.AsSeconds() ;
            }
            _currentSprite.Position = _body.Position;
        }

        public void Draw(RenderWindow playerWindow)
        {
            playerWindow.Draw(_body);

            playerWindow.Draw(_currentSprite);
        }
    }
}
