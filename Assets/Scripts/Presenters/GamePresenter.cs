using Assets.Scripts.Models.Interfaces;
using Zenject;
using UniRx;

namespace Assets.Scripts.Presenters
{
    public class GamePresenter : IInitializable
    {
        [Inject(Id = "input_manager")]
        private IInputSubscriber _input;

        public void Initialize()
        {
            _input.ExitFire().Subscribe(_ => { UnityEngine.Application.Quit(); });
        }
    }
}
