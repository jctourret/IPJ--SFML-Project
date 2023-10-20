using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace MyPrimerJuegoSFML
{
    internal class Player
    {
        public CircleShape _body;
        float _radius;
        Color innerColor;
        Color outerColor;

        public Player(float posX, float posY)
        {
            _radius = 30;

            _body = new CircleShape(_radius);
            _body.Position = new Vector2f(posX, posY);
        }

        public Player(float radius, Vector2f pos)
        {
            _body = new CircleShape(radius);
            _body.Position = pos;
        }

        public void Update()
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
            if (dirMag <= 0)
            {
                Vector2f dirNormalized = direction / dirMag;

                _body.Position += dirNormalized;

            }
        }

        public void Draw(RenderWindow playerWindow)
        {
            playerWindow.Draw(_body);
        }
    }
}
