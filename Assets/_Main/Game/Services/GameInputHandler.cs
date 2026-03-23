namespace Game
{
    public class GameInputHandler
    {
        public Inputs Inputs { get; private set; }
        
        public GameInputHandler(Inputs inputs)
        {
            Inputs = inputs;
        }
    }
}