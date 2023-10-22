using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPrimerJuegoSFML
{
    //GameObjectBase va a ser la base abstracta de todos los objetos de nuestro juego y nos permitira 
    //darles funcionalidad y atributos compartidos.

    //Por ahora sabemos que todos los objetos de nuestro juego van a necesitar una sprite
    //para aparecer en pantalla.
    abstract class GameObjectBase
    {
        protected Texture texture;
        protected Sprite sprite;
    }
}
