using Assets.Scripts.Models.Interfaces;
using System;
using System.Linq;
using System.Collections.Generic;
using UniRx;

namespace Assets.Scripts.Models
{
    public class SimpleInputManager : IInputManager
    {
        private IList<IInputSubscriber> _inputs;
        
        public SimpleInputManager(IList<IInputSubscriber> inputs)
        {
            _inputs = inputs;
        }

        public void AddInput(IInputSubscriber input)
        {
            _inputs.Add(input);
        }

        public IObservable<Unit> DownFire()
        {
            return Observable.Merge(_inputs.Select(input => input.DownFire()));
        }

        public IObservable<Unit> ExitFire()
        {
            return Observable.Concat(_inputs.Select(input => input.ExitFire()));
        }

        public IObservable<Unit> LeftFire()
        {
            return Observable.Concat(_inputs.Select(input => input.LeftFire()));
        }

        public IObservable<Unit> RightFire()
        {
            return Observable.Concat(_inputs.Select(input => input.RightFire()));
        }

        public IObservable<Unit> ScaleDownFire()
        {
            return Observable.Concat(_inputs.Select(input => input.ScaleDownFire()));
        }

        public IObservable<Unit> ScaleUpFire()
        {
            return Observable.Concat(_inputs.Select(input => input.ScaleUpFire()));
        }

        public IObservable<Unit> UpFire()
        {
            return Observable.Concat(_inputs.Select(input => input.UpFire()));
        }
    }
}
