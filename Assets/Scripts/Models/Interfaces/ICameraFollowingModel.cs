using UniRx;

namespace Assets.Scripts.Models.Interfaces
{
    public interface ICameraFollowingModel
    {
        ReactiveProperty<Coordinate> CurrentPosition { get; }
    }
}
