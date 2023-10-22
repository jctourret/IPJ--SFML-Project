namespace MyPrimerJuegoSFML
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Para mantener nuestro codigo limpio, vamos solo crear nuestro juego y correrlo.
            Juego jueguito = new Juego();
            jueguito.Run();
        }
    }
}