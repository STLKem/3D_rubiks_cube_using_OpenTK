namespace rubix_cube_opentk
{

    class Program
    {
        static void Main(string[] args)
        {
            //just to start the "cube"
            using (Game game = new Game(500, 500))
            {
                game.Run();
            }
        }
    }

}