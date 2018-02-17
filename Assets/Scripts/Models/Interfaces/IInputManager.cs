namespace Assets.Scripts.Models.Interfaces
{
    public interface IInputManager : IInputSubscriber
    {
        void AddInput(IInputSubscriber input);
    }
}
