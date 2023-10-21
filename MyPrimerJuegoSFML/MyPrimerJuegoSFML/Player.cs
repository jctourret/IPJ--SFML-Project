using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace MyPrimerJuegoSFML
{
    enum playerStates
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

        Texture _texture;
        Sprite _sprite;

        List<Animation> animations;

        float speed = 100;

        public Player(float posX, float posY, float sizeX, float sizeY)
        {
            _radius = 30;
            _body = new RectangleShape(new Vector2f(sizeX,sizeY));
            _body.Origin = new Vector2f(posX+(sizeX/2), posY+(sizeY/2));
            _body.Position = new Vector2f(posX, posY);

            _texture = new Texture("Novice.png");

            IntRect spriteRect = new IntRect(11,11,36,72);

            _sprite = new Sprite(_texture,spriteRect);
            _sprite.Origin = new Vector2f(_sprite.Position.X+(_sprite.TextureRect.Width/2),
                _sprite.Position.Y + (_sprite.TextureRect.Height / 2));


        }


        public Player(float radius, Vector2f pos)
        {
        }

        public void Update(Time deltaTime)
        {
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
            _sprite.Position = _body.Position;
        }

        public void Draw(RenderWindow playerWindow)
        {
            playerWindow.Draw(_body);
            playerWindow.Draw(_sprite);
        }
    }
}
