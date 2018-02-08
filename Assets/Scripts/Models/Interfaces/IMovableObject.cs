using UniRx;

namespace Assets.Scripts.Models.Interfaces
{
    public interface IMovableObject : IRank
    {
        ReactiveProperty<Coordinate> Position { get; }
        ReactiveProperty<MoveDirection> Direction { get; }
        //Calculated in seconds to move on next coordinate
        ReactiveProperty<float> Speed { get; }
    }
}
