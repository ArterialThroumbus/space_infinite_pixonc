using UniRx;

namespace Assets.Scripts.Models.Interfaces
{
    public interface IStaticObject : IRank
    {
        Coordinate Position { get; set; }
        ReactiveProperty<bool> IsVisible { get; }
    }
}
