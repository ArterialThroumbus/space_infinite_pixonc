using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniRx;
using UnityEngine;

namespace Assets.Scripts.Models
{
    public class SimpleShipController : BaseShipController
    {
        private CompositeDisposable _currentControls;

        public SimpleShipController()
        {
            _currentControls = new CompositeDisposable();
            IDisposable control;
            control = Observable.EveryUpdate().Where(_ => Input.anyKeyDown)
                .Subscribe(_ => CheckDirection());
            _currentControls.Add(control);
            control = Observable.EveryUpdate().Where(_ => !Input.anyKeyDown)
                .Subscribe(_ => _ship.Direction.Value = MoveDirection.None);
        }

        private void CheckDirection()
        {
            var newDirection = MoveDirection.None;
            
            //move right
            if (Input.GetKeyDown(KeyCode.D))
                newDirection |= MoveDirection.Right;
            else if (Input.GetKeyDown(KeyCode.A))
                newDirection |= MoveDirection.Left;

            //move up
            if (Input.GetKeyDown(KeyCode.W))
                newDirection |= MoveDirection.Up;
            else if (Input.GetKeyDown(KeyCode.S))
                newDirection |= MoveDirection.Down;

            _ship.Direction.Value = newDirection;

        }

        public override void Dispose()
        {
            _currentControls.Dispose();
        }
    }
}
