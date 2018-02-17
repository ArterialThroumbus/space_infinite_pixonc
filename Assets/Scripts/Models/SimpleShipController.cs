using UniRx;

namespace Assets.Scripts.Models
{
    public class SimpleShipController : BaseShipController
    {
        public override void Initialize()
        {
            _input.RightFire().Subscribe(_ => _ship.Direction.SetValueAndForceNotify(MoveDirection.Right));
            _input.UpFire().Subscribe(_ => _ship.Direction.SetValueAndForceNotify(MoveDirection.Up));
            _input.LeftFire().Subscribe(_ => _ship.Direction.SetValueAndForceNotify(MoveDirection.Left));
            _input.DownFire().Subscribe(_ => _ship.Direction.SetValueAndForceNotify(MoveDirection.Down));
        }

        public override void Dispose()
        {
        }
    }
}
