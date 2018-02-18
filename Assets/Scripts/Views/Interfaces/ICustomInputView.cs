﻿using UniRx;

namespace Assets.Scripts.Views.Interfaces
{
    public interface ICustomInputView
    {
        IObservable<Unit> RightFire();
        IObservable<Unit> UpFire();
        IObservable<Unit> LeftFire();
        IObservable<Unit> DownFire();
        IObservable<Unit> ScaleUpFire();
        IObservable<Unit> ScaleDownFire();
        IObservable<Unit> ExitFire();
    }
}
