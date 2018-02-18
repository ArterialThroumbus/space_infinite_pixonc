using Zenject;
using UniRx;
using Assets.Scripts.Models.Interfaces;
using Assets.Scripts.Views.Interfaces;

namespace Assets.Scripts.Presenters
{
    public class CustomInputPresenter : IInitializable
    {
        [Inject]
        private IInputCaller _inputCaller;
        [Inject]
        private ICustomInputView _customInputView;

        public void Initialize()
        {
            _customInputView.RightFire().Subscribe(_ => {
                _inputCaller.Right();
            });
            _customInputView.UpFire().Subscribe(_ => {
                _inputCaller.Up();
            });
            _customInputView.LeftFire().Subscribe(_ => {
                _inputCaller.Left();
            });
            _customInputView.DownFire().Subscribe(_ => {
                _inputCaller.Down();
            });
            _customInputView.ScaleUpFire().Subscribe(_ => {
                _inputCaller.ScaleUp();
            });
            _customInputView.ScaleDownFire().Subscribe(_ => {
                _inputCaller.ScaleDown();
            });
            _customInputView.ExitFire().Subscribe(_ => {
                _inputCaller.Exit();
            });
        }
    }
}
