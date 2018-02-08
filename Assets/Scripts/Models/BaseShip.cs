using Assets.Scripts.Models.Interfaces;
using UniRx;

namespace Assets.Scripts.Models
{
    public abstract class BaseShip : IMovableObject
    {
        public ReactiveProperty<Coordinate> Position { get; set; }

        public virtual ReactiveProperty<MoveDirection> Direction { get; set; }

        public virtual ReactiveProperty<float> Speed { get; set; }

        public int Rank { get; set; }
    }
}
