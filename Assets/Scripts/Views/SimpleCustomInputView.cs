using Assets.Scripts.Views.Interfaces;
using UnityEngine;
using UniRx;

namespace Assets.Scripts.Views
{
    public class SimpleCustomInputView : MonoBehaviour, ICustomInputView
    {
        private Subject<Unit> _rightFire;
        private Subject<Unit> _upFire;
        private Subject<Unit> _leftFire;
        private Subject<Unit> _downFire;
        private Subject<Unit> _scaleUpFire;
        private Subject<Unit> _scaleDownFire;
        private Subject<Unit> _exitFire;

        private void Awake()
        {
            _rightFire = new Subject<Unit>();
            _upFire = new Subject<Unit>();
            _leftFire = new Subject<Unit>();
            _downFire = new Subject<Unit>();
            _scaleUpFire = new Subject<Unit>();
            _scaleDownFire = new Subject<Unit>();
            _exitFire = new Subject<Unit>();
        }

        public IObservable<Unit> DownFire()
        {
            return _downFire.AsObservable();
        }

        public IObservable<Unit> ExitFire()
        {
            return _exitFire.AsObservable();
        }

        public IObservable<Unit> LeftFire()
        {
            return _leftFire.AsObservable();
        }

        public IObservable<Unit> RightFire()
        {
            return _rightFire.AsObservable();
        }

        public IObservable<Unit> ScaleDownFire()
        {
            return _scaleDownFire.AsObservable();
        }

        public IObservable<Unit> ScaleUpFire()
        {
            return _scaleUpFire.AsObservable();
        }

        public IObservable<Unit> UpFire()
        {
            return _upFire.AsObservable();
        }

        public void ScaleUp()
        {
            _scaleUpFire.OnNext(Unit.Default);
        }

        public void ScaleDown()
        {
            _scaleDownFire.OnNext(Unit.Default);
        }

        public void Exit()
        {
            _exitFire.OnNext(Unit.Default);
        }

        public void Right()
        {
            _rightFire.OnNext(Unit.Default);
        }

        public void Up()
        {
            _upFire.OnNext(Unit.Default);
        }

        public void Left()
        {
            _leftFire.OnNext(Unit.Default);
        }

        public void Down()
        {
            _downFire.OnNext(Unit.Default);
        }
    }
}
