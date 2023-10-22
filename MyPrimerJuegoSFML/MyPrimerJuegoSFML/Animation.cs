using SFML.Graphics;
using SFML.System;

namespace MyPrimerJuegoSFML
{
    internal class Animation
    {
        //Una animacion esta formada por fotogramas, asi que armamos una 'lista' de estos.

        //Una lista es un array dynamico en el cual cada elemento (en este caso cada frame)
        //guarda una refencia a la posicion en memoria del siguiente elemento.
        //esto las hace extremadamente flexibles, pero muy costosas de usar.

        List<Sprite> _frames;

        //tiempo entre cada frame.
        float _frameTime;

        //tiempo total de nuestra animacion.
        float _animationTime;

        //Acumulador de tiempo.
        float _animationtimer = 0;

        //Fotograma actual de nuesra animacion.
        int _currentFrame;

        public Animation(Texture spriteSheet, int frameHeight, int frameWidth, int offsetX, int offsetY, int animRows, int animColumns, float animationTime)
        {
            _frames = new List<Sprite>();

            //Nuestro spritesheet sera siempre una imagen bidimensional de la cual queremos recortar cuadrados(fotogramas)
            //por esto, para recorrer el array bidimensional, deberemos usar un for anidado.
            for (int i = offsetX; i < animRows; i++)
            {
                for (int j = offsetY; j < animColumns; j++)
                {
                    IntRect frameRect = new IntRect();
                    //Haciendo que la posicion en Y del recorte dependa del valor de i
                    //hara que avanze en la dimension Y con respecto a la execucion del for.
                    frameRect.Top = i * frameHeight; // Y

                    //Haciendo que la posicion en X del recorte dependa del valor de j
                    //hara que avanze en la dimension X con respecto a la execucion del for.
                    frameRect.Left = j * frameWidth; // X

                    //Seteamos las dimensiones de nuestro recorte en base a los datos que nos dan.
                    frameRect.Width = frameWidth; //W
                    frameRect.Height = frameHeight;  //H

                    //Finalmente, teniendo la textura que pedimos como parametro y el recorte de esa
                    //textura correspondiente al fotograma actual, creamos el fotograma.
                    Sprite newSprite = new Sprite(spriteSheet, frameRect);

                    //Para ahorrarnos problemas en el futuro cuando querramos flipear las ciertas
                    //animaciones, seteamos el origen de coordenadas a la mitad del ancho y la mitad del alto.

                    //Esto se hace asi por que el origen ya funciona en base a la posicion de la sprite, por lo que
                    //no es necesario sumarla para llegar al centro.
                    newSprite.Origin = new Vector2f(newSprite.TextureRect.Width / 2,newSprite.TextureRect.Height / 2);
                    
                    //Despues de todo ese trabajo, agregamos el frame a la lista y volvemos a empezar hasta cumplir el For.
                    _frames.Add(newSprite);
                }
            }
            //Una vez tenemos todos nuestros frames en la lista, calculamos cuanto tiene que durar cada fotograma
            //en base al tiempod de animacion.
            _frameTime = animationTime / _frames.Count();
        }

        //Para que la animacion se corra debe avanzar en el tiempo
        //el fotograma que se esta dibujando.
        public Sprite updateAnimation(Time deltaTime)
        {
            //Acumulamos tiempo en un float
            _animationtimer += deltaTime.AsSeconds();

            //Cuando el acumulador cumple el tiempo que deberia tomar un fotograma
            //no lo reseteamos si no que le restamos un el tiempo de cada fotograma.
            if (_animationtimer > _frameTime)
            {
                //Restamos
                _animationtimer -= _frameTime;
                //Avanzamos
                _currentFrame++;

                //Limitamos el valor de _currentFrame usando el operador,
                //ya que esto reseteara el valor cuando llegue al total de fotogramas.

                //Prueba:
                //(0%10) = 0, (1%10) = 1, (2%10) = 2, (3%10) = 3, (4%10) = 4, (5%10) = 5
                //(6%10) = 6, (7%10) = 7, (8%10) = 8, (9%10) = 9, (10%10) = 0, (1%10) = 1
                _currentFrame = _currentFrame % _frames.Count;
            }

            //Retorno el frame actual de la animacion.
            return _frames[_currentFrame];
        }

    }
}
