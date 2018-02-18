using Assets.Scripts.Models.Interfaces;
using Assets.Scripts.Views.Interfaces;
using System.Collections.Generic;
using Zenject;
using UniRx;
using System.Linq;
using Assets.Scripts.Views.UnityViews;

namespace Assets.Scripts.Views
{
    public class VSpaceView : ISpaceView, ISpecialViewComponent
    {
        [Inject]
        private ICoordinateConverter _coordinateConverter;
        [Inject]
        private VPlanet.Pool _planetPools;

        private IDictionary<int, VPlanet> _currentPlanets = new Dictionary<int, VPlanet>();

        public void AddPlanet(IPlanet planet)
        {
            planet.IsVisible.Subscribe(isVisible => {
                if (isVisible)
                    InitPlanet(planet);
                else
                    HidePlanet(planet);
            });
        }

        private void InitPlanet(IPlanet planet)
        {
            _currentPlanets[planet.Position.GetHashCode()] = _planetPools.Spawn(
                _coordinateConverter.Convert(planet.Position),
                planet.IsVisible.Value,
                planet.PlanetType
                );
        }

        public void HidePlanet(IPlanet planet)
        {
            var uid = planet.Position.GetHashCode();
            if (_currentPlanets.ContainsKey(uid))
            {
                var vPlanet = _currentPlanets[uid];
                _currentPlanets.Remove(uid);
                _planetPools.Despawn(vPlanet);
            }
        }

        public void SpecialView(bool isEnable, int scale)
        {
            foreach (var planet in _currentPlanets.Values.Select(pl => pl.GetComponent<SpecialViewShowing>()))
            {
                planet.SpecialViewUpdate(isEnable, scale);
            }
        }

        public void ShowPlanet(IPlanet planet)
        {
            
        }

        public void HideAll()
        {
            var uids = _currentPlanets.Keys.ToArray();

            foreach (var uid in uids)
            {
                var vPlanet = _currentPlanets[uid];
                _currentPlanets.Remove(uid);
                _planetPools.Despawn(vPlanet);
            }
        }
    }
}
