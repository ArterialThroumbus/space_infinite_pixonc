using Assets.Scripts.Models.Interfaces;
using Zenject;
using System.Collections.Generic;
using System.Linq;
using System;
using UniRx;

namespace Assets.Scripts.Models
{
    public class SimpleSpecialView : ISpecialView, IInitializable
    {
        [Inject]
        private IShipModel _ship;
        [Inject]
        private ISpace _space;
        [Inject]
        private ISpecialViewChecker _specialViewChecker;
        [Inject]
        private Configuration _configuration;
        [Inject]
        private IScaling _scaling;
        [Inject]
        private SpaceInfo _spaceInfo;
        [Inject]
        private ICameraFollowingModel _cameraFollowing;

        public ReactiveProperty<bool> IsEnabled { get; private set; }
        private IDictionary<int, List<IPlanet>> _planetByRankDifferent;

        public SimpleSpecialView()
        {
            IsEnabled = new ReactiveProperty<bool>(false);
            _planetByRankDifferent = new SortedList<int, List<IPlanet>>();
        }

        public void Initialize()
        {
            _specialViewChecker.Checking += SpecialChanging;
            _cameraFollowing.CurrentPosition.Subscribe(CameraPosChanged);
            _scaling.Changed += ScalingChanged;
        }

        private void SpecialChanging(bool isEnabled)
        {
            IsEnabled.Value = isEnabled;
            ShowPlanets();
        }

        private void CameraPosChanged(Coordinate cameraPos)
        {
            ShowPlanets();
        }

        private void ScalingChanged(int scale)
        {
            ShowPlanets();
        }

        public void FilterVisible(IEnumerable<IPlanet> planets)
        {
            foreach (var planet in planets)
            {
                CachedPlanet(planet);

                if (!IsEnabled.Value)
                {
                    planet.IsVisible.Value = true;
                    continue;
                }
            }

            ShowPlanets();
        }

        private void CachedPlanet(IPlanet planet)
        {
            var diff = Math.Abs(_ship.Rank - planet.Rank);

            if (!_planetByRankDifferent.ContainsKey(diff))
                _planetByRankDifferent.Add(diff, new List<IPlanet>());

            if (!_planetByRankDifferent[diff].Contains(planet))
                _planetByRankDifferent[diff].Add(planet);
        }

        private void ShowPlanets()
        {
            if (IsEnabled.Value)
            {
                var maxShowing = _configuration.CountPlanetInSpecialView;

                foreach (var diffToPlanets in _planetByRankDifferent)
                {
                    foreach(var planet in diffToPlanets.Value)
                    {
                        if (IsInView(planet.Position))
                        {
                            maxShowing -= 1;
                            planet.IsVisible.Value = true;

                            if (maxShowing == 0)
                                break;
                        }
                    }

                    if (maxShowing == 0)
                        break;
                }
            }
            else
            {
                for (var i = _spaceInfo.CurrentMinX; i <= _spaceInfo.CurrentMaxX; ++i)
                {
                    for (var j = _spaceInfo.CurrentMinY; j <= _spaceInfo.CurrentMaxX; ++j)
                    {
                        var uid = new Coordinate(i, j).GetHashCode();

                        if (_space.Planets.ContainsKey(uid))
                        {
                            _space.Planets[uid].IsVisible.Value = true;
                        }
                    }
                }
            }
        }

        private bool IsInView(Coordinate coordinate)
        {
            var halfScale = _spaceInfo.CurrentScale / 2;
            var centerPos = _cameraFollowing.CurrentPosition.Value;
            return centerPos.X - halfScale < coordinate.X && centerPos.X + halfScale > coordinate.X &&
                   centerPos.Y - halfScale < coordinate.Y && centerPos.Y + halfScale > coordinate.Y; 
        }

        public void FilterUnvisible(IEnumerable<IPlanet> planets)
        {
            foreach(var planet in planets)
            {
                planet.IsVisible.Value = false;
            }
        }
    }
}
