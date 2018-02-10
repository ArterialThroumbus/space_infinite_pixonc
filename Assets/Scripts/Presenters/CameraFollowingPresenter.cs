using Assets.Scripts.Models.Interfaces;
using Assets.Scripts.Views.Interfaces;
using Zenject;
using UniRx;

namespace Assets.Scripts.Presenters
{
    public class CameraFollowingPresenter : IInitializable
    {
        [Inject]
        private ICameraFollowingModel _ship;
        [Inject]
        private ICoordinateConverter _coordinateConverter;
        [Inject]
        private ICameraFollowing _view;

        public void Initialize()
        {
            _ship.CurrentPosition.Subscribe(newPos => _view.MoveTo(_coordinateConverter.Convert(newPos)));
        }
    }
}
