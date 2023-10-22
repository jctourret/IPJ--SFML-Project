using SFML.Graphics;
using SFML.System;

namespace MyPrimerJuegoSFML
{
    internal class Animation
    {
        List<Sprite> _frames;

        float _frameTime;

        float _animationTime;

        float _animationtimer = 0;

        int _currentFrame;

        public Animation(Texture spriteSheet, int frameHeight, int frameWidth, int offsetX, int offsetY, int animRows, int animColumns, float animationTime)
        {
            _frames = new List<Sprite>();

            for (int i = offsetX; i < animRows; i++)
            {
                for (int j = offsetY; j < animColumns; j++)
                {
                    IntRect frameRect = new IntRect();
                    frameRect.Top = i * frameHeight; // Y
                    frameRect.Left = j * frameWidth; // X
                    frameRect.Width = frameWidth; //W
                    frameRect.Height = frameHeight;  //H

                    Sprite newSprite = new Sprite(spriteSheet, frameRect);

                    newSprite.Origin = new Vector2f(newSprite.TextureRect.Width / 2,newSprite.TextureRect.Height / 2);
                    
                    _frames.Add(newSprite);
                }
            }

            _frameTime = animationTime / _frames.Count();
        }


        public Sprite updateAnimation(Time deltaTime)
        {
            _animationtimer += deltaTime.AsSeconds();

            if (_animationtimer > _frameTime)
            {
                _animationtimer -= _frameTime;
                _currentFrame++;
                _currentFrame = _currentFrame % _frames.Count;
            }

            
                return _frames[_currentFrame];
        }

    }
}
