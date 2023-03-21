namespace ConsoleGame.Objects
{
    // this interface is intended for graphical objects
    public interface IObject
    {
        // this method is intended to be called in a game loop to render the object on the screen
        void Render();
    }
}
