using UniRx;

namespace Assets.Scripts.Models
{
    public class SimpleShip : BaseShip {

        public SimpleShip()
        {
            this.Direction = new ReactiveProperty<MoveDirection>(MoveDirection.None);
            this.Position = new ReactiveProperty<Coordinate>();
            this.Speed = new ReactiveProperty<float>(0);

            this.Direction.Subscribe(_ => CalculateCoordinates());
        }

        private void CalculateCoordinates()
        {
            var newCoordinates = new Coordinate();

            if ((Direction.Value & MoveDirection.Right) == MoveDirection.Right)
                newCoordinates.X = 1;
            else if ((Direction.Value & MoveDirection.Left) == MoveDirection.Left)
                newCoordinates.X = -1;

            if ((Direction.Value & MoveDirection.Up) == MoveDirection.Up)
                newCoordinates.Y = 1;
            else if ((Direction.Value & MoveDirection.Down) == MoveDirection.Down)
                newCoordinates.Y = -1;

            Position.Value += newCoordinates;
        }
    }
}
