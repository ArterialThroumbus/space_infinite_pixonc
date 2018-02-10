using System;
using System.Linq;
using UniRx;
using UnityEngine;

namespace Assets.Scripts.Models
{
    public class SimpleShipController : BaseShipController
    {
        private CompositeDisposable _currentControls;
        private readonly string _horizontalAxis = "Horizontal";
        private readonly string _verticalAxis = "Vertical";

        public override void Initialize()
        {
            _currentControls = new CompositeDisposable();
            IDisposable control;
            control = Observable.EveryUpdate().Where(_ => Input.anyKeyDown)
                .Subscribe(_ => CheckDirection());
            _currentControls.Add(control);
            control = Observable.EveryUpdate().Where(_ => !Input.anyKeyDown)
                .Subscribe(_ => _ship.Direction.Value = MoveDirection.None);
            _currentControls.Add(control);
        }

        private void CheckDirection()
        {
            var newDirection = MoveDirection.None;

            var horizontal = Input.GetAxis(_horizontalAxis);
            //move right
            if (horizontal > 0)
                newDirection |= MoveDirection.Right;
            else if (horizontal < 0)
                newDirection |= MoveDirection.Left;

            var vertical = Input.GetAxis(_verticalAxis);
            //move up
            if (vertical > 0)
                newDirection |= MoveDirection.Up;
            else if (vertical < 0)
                newDirection |= MoveDirection.Down;

            _ship.Direction.Value = newDirection;

        }

        public override void Dispose()
        {
            _currentControls.Dispose();
        }
    }
}
