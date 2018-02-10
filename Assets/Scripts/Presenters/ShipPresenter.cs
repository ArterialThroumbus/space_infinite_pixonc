using Assets.Scripts.Models;
using Assets.Scripts.Views.Interfaces;
using Zenject;
using UniRx;

namespace Assets.Scripts.Presenters
{
    public class ShipPresenter : IInitializable
    {
        [Inject]
        private IShipModel _model;

        [Inject]
        private IShip _view;

        [Inject]
        private ICoordinateConverter _coordinateConverter;

        public void Initialize()
        {
            _model.Position.Subscribe(pos => _view.MoveTo(_coordinateConverter.Convert(pos)));
        }
    }
}
