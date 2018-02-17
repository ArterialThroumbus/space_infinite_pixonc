using UniRx;

namespace Assets.Scripts.Models.Interfaces
{
    public interface IInputSubscriber
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
