using UniRx;

namespace Assets.Scripts.Models
{
    public class SimpleShip : BaseShip {
        private ReactiveProperty<MoveDirection> _moveDirection;
        private ReactiveProperty<float> _speed = new ReactiveProperty<float>(0);

        public override ReactiveProperty<float> Speed {
            get { return _speed; }
            set { }
        }

        public override ReactiveProperty<MoveDirection> Direction
        {
            get
            {
                return _moveDirection;
            }

            set
            {
                _moveDirection = value;
                CalculateCoordinates();
            }
        }

        private void CalculateCoordinates()
        {
            var newCoordinates = new Coordinate();

            if ((_moveDirection.Value & MoveDirection.Right) == MoveDirection.Right)
                newCoordinates.X = 1;
            else if ((_moveDirection.Value & MoveDirection.Up) == MoveDirection.Up)
                newCoordinates.Y = 1;
            else if ((_moveDirection.Value & MoveDirection.Left) == MoveDirection.Left)
                newCoordinates.X = -1;
            else if ((_moveDirection.Value & MoveDirection.Down) == MoveDirection.Down)
                newCoordinates.Y = -1;

            Position.Value += newCoordinates;
        }
    }
}
