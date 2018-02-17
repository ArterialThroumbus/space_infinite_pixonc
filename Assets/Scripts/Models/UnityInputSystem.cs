using Assets.Scripts.Models.Interfaces;
using UniRx;
using UnityEngine;

namespace Assets.Scripts.Models
{
    public class UnityInputSystem : IInputSubscriber
    {
        private readonly string _horizontalAxis = "Horizontal";
        private readonly string _verticalAxis = "Vertical";
        private readonly string _scaling = "Scaling";
        private readonly string _exit = "Exit";

        private IObservable<Unit> _rightStream;
        private IObservable<Unit> _upStream;
        private IObservable<Unit> _leftStream;
        private IObservable<Unit> _downStream;
        private IObservable<Unit> _scaleUpStream;
        private IObservable<Unit> _scaleDownStream;
        private IObservable<Unit> _exitStream;

        public UnityInputSystem()
        {
            _rightStream = Observable.EveryUpdate()
                           .Where(_ => Input.GetAxis(_horizontalAxis) > 0 && Input.anyKeyDown).Select(_ => Unit.Default);
            _upStream = Observable.EveryUpdate()
                           .Where(_ => Input.GetAxis(_verticalAxis) > 0 && Input.anyKeyDown).Select(_ => Unit.Default);
            _leftStream = Observable.EveryUpdate()
                           .Where(_ => Input.GetAxis(_horizontalAxis) < 0 && Input.anyKeyDown).Select(_ => Unit.Default);
            _downStream = Observable.EveryUpdate()
                           .Where(_ => Input.GetAxis(_verticalAxis) < 0 && Input.anyKeyDown).Select(_ => Unit.Default);
            _scaleUpStream = Observable.EveryUpdate()
                           .Where(_ => Input.GetAxis(_scaling) > 0 && Input.anyKeyDown).Select(_ => Unit.Default);
            _scaleDownStream = Observable.EveryUpdate()
                           .Where(_ => Input.GetAxis(_scaling) < 0 && Input.anyKeyDown).Select(_ => Unit.Default);
            _exitStream = Observable.EveryUpdate()
                           .Where(_ => Input.GetAxis(_exit) != 0 && Input.anyKeyDown).Select(_ => Unit.Default);
        }

        public IObservable<Unit> DownFire()
        {
            return _downStream;
        }

        public IObservable<Unit> ExitFire()
        {
            return _exitStream;
        }

        public IObservable<Unit> LeftFire()
        {
            return _leftStream;
        }

        public IObservable<Unit> RightFire()
        {
            return _rightStream;
        }

        public IObservable<Unit> ScaleDownFire()
        {
            return _scaleDownStream;
        }

        public IObservable<Unit> ScaleUpFire()
        {
            return _scaleUpStream;
        }

        public IObservable<Unit> UpFire()
        {
            return _upStream;
        }
    }
}
