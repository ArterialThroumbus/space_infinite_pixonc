using Assets.Scripts.Models.Interfaces;
using Zenject;
using System.Collections.Generic;
using System.Linq;
using System;
using UniRx;
using Assets.Scripts.Signals;

namespace Assets.Scripts.Models
{
    public class SimpleSpecialView : ISpecialView, IInitializable
    {
        [Inject]
        private SpecialViewChanged _specialSignal;
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

        public SpecialViewChanged Changed { get { return _specialSignal; } }
        private IDictionary<int, List<IPlanet>> _planetByRankDifferent;
        private List<IPlanet> _showingPlanets;
        private bool _isEnabled;

        public SimpleSpecialView()
        {
            _planetByRankDifferent = new SortedList<int, List<IPlanet>>();
            _showingPlanets = new List<IPlanet>();
        }

        public void Initialize()
        {
            _specialViewChecker.Checking += SpecialChanging;
            _cameraFollowing.CurrentPosition.Subscribe(CameraPosChanged);
            _scaling.Changed += ScalingChanged;
        }

        private void SpecialChanging(bool isEnabled)
        {
            _isEnabled = isEnabled;
            _specialSignal.Fire(_isEnabled, _spaceInfo.CurrentScale);
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

                if (!_isEnabled)
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
            _showingPlanets.ForEach(planet => planet.IsVisible.Value = false);
            _showingPlanets.Clear();

            if (_isEnabled)
            {
                foreach (var diffToPlanets in _planetByRankDifferent)
                {
                    foreach(var planet in diffToPlanets.Value)
                    {
                        if (IsInView(planet.Position))
                        {
                            _showingPlanets.Add(planet);
                            planet.IsVisible.Value = true;

                            if (_showingPlanets.Count == _configuration.CountPlanetInSpecialView)
                                break;
                        }
                    }

                    if (_showingPlanets.Count == _configuration.CountPlanetInSpecialView)
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
                            _showingPlanets.Add(_space.Planets[uid]);
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
