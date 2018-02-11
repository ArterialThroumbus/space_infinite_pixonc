using Zenject;
using UniRx;
using Assets.Scripts.Models.Interfaces;
using Assets.Scripts.Views.Interfaces;
using System;

namespace Assets.Scripts.Presenters
{
    public class SpacePresenter : IInitializable
    {
        [Inject]
        private ISpace _space;
        [Inject]
        private ISpaceView _spaceView;
        [Inject]
        private ISpecialView _specialView;

        public void Initialize()
        {
            _space.Planets.ObserveAdd().Subscribe(observer => _spaceView.AddPlanet(observer.Value));

            foreach (var planet in _space.Planets.Values)
                _spaceView.AddPlanet(planet);

            _specialView.IsEnabled.Subscribe(isEnabled => _spaceView.SpecialView(isEnabled));
        }
    }
}
