using Assets.Scripts.Models.Interfaces;
using System.Collections.Generic;
using Zenject;

namespace Assets.Scripts.Models
{
    public class SimpleExpansionStrategy : IExpansionStrategy, IInitializable
    {
        [Inject]
        private ICameraFollowingModel _followingModel;
        [Inject]
        private SpaceInfo _spaceInfo;
        [Inject]
        private Configuration _configuration;
        [Inject]
        private IExpansionChecker _expansionChecker;
        [Inject]
        private IMap _mapCreater;
        [Inject]
        private ISpace _space;
        [Inject]
        private IVisibleFilter _visibleFilter;

        private MoveDirection _nowDirection;
        private List<IPlanet> _visiblePlanet;
        private List<IPlanet> _hiddedPlanet;

        public void Initialize()
        {
            _visiblePlanet = new List<IPlanet>();
            _hiddedPlanet = new List<IPlanet>();
            _expansionChecker.Changing += ExpansionNeed;
            InitMap();   
        }

        private void InitMap()
        {
            _spaceInfo.CurrentMinX = _spaceInfo.CurrentScale * -1;
            _spaceInfo.CurrentMaxX = _spaceInfo.CurrentScale - 1;
            _spaceInfo.CurrentMaxY = _spaceInfo.CurrentScale - 1;
            _spaceInfo.CurrentMinY = _spaceInfo.CurrentScale * -1;

            _spaceInfo.MinX = _spaceInfo.CurrentMinX;
            _spaceInfo.MaxX = _spaceInfo.CurrentMaxX;
            _spaceInfo.MinY = _spaceInfo.CurrentMinY;
            _spaceInfo.MaxY = _spaceInfo.CurrentMaxY;

            var spObjects = _mapCreater.Generate(_spaceInfo.CurrentScale * 2, _spaceInfo.CurrentScale * 2);
            var startCoordinate = new Coordinate(_spaceInfo.CurrentMinX, _spaceInfo.CurrentMinY);
            foreach (var spObj in spObjects)
            {
                if (spObj is IPlanet)
                    SetPlanet(spObj as IPlanet, startCoordinate, true); 
            }
            FilterChanges();
        }

        private void ExpansionNeed(SpaceChanging changing, MoveDirection direction)
        {
            _nowDirection = direction;
            Apply();
        }

        public void Apply()
        {
            var currentPosition = _followingModel.CurrentPosition.Value;
            var hidePlace = _configuration.HiddenSpace;

            var scale = _spaceInfo.CurrentScale / 2f / hidePlace;
            var expansionDelta = hidePlace * scale;
            var narDelta = expansionDelta * 3;

            IEnumerable<IStaticObject> changedObjects;
            var coordinate = new Coordinate();

            if ((_nowDirection & MoveDirection.Right) == MoveDirection.Right)
            {
                var changeNowDelta = _spaceInfo.CurrentMaxX - currentPosition.X;
                
                if (changeNowDelta < expansionDelta)
                {
                    _spaceInfo.CurrentMaxX += hidePlace;
                    if (_spaceInfo.CurrentMaxX > _spaceInfo.MaxX)
                    {
                        _spaceInfo.MaxX = _spaceInfo.CurrentMaxX;
                        changedObjects = _mapCreater.Generate(hidePlace, _spaceInfo.CurrentMaxY - _spaceInfo.CurrentMinY);

                        var startCoordinate = new Coordinate(_spaceInfo.CurrentMaxX - hidePlace, _spaceInfo.CurrentMinY);

                        foreach (var spObj in changedObjects)
                        {
                            if (spObj is IPlanet)
                                SetPlanet(spObj as IPlanet, startCoordinate, true);
                        }
                    }
                    else
                    {
                        for (var i = _spaceInfo.CurrentMaxX - hidePlace; i <= _spaceInfo.CurrentMaxX; ++i)
                        {
                            for (var j = _spaceInfo.CurrentMinY; j <= _spaceInfo.CurrentMaxY; ++j)
                            {
                                coordinate.X = i;
                                coordinate.Y = j;
                                SetPlanet(coordinate, true);
                            }
                        }
                    }
                }
                else if (changeNowDelta > narDelta)
                {
                    for (var i = _spaceInfo.CurrentMaxX - hidePlace; i < _spaceInfo.CurrentMaxX; ++i)
                    {
                        for (var j = _spaceInfo.CurrentMinY; j <= _spaceInfo.CurrentMaxY; ++j)
                        {
                            coordinate.X = i;
                            coordinate.Y = j;
                            SetPlanet(coordinate, false);
                        }
                    }

                    _spaceInfo.CurrentMaxX -= hidePlace;
                }
            }

            if ((_nowDirection & MoveDirection.Up) == MoveDirection.Up)
            {
                var changeNowDelta = _spaceInfo.CurrentMaxY - currentPosition.Y;

                if (changeNowDelta < expansionDelta)
                {
                    _spaceInfo.CurrentMaxY += hidePlace;
                    if (_spaceInfo.CurrentMaxY > _spaceInfo.MaxY)
                    {
                        _spaceInfo.MaxY = _spaceInfo.CurrentMaxY;
                        changedObjects = _mapCreater.Generate(_spaceInfo.CurrentMaxX - _spaceInfo.CurrentMinX, hidePlace);

                        var startCoordinate = new Coordinate(_spaceInfo.CurrentMinX, _spaceInfo.CurrentMaxY - hidePlace);

                        foreach (var spObj in changedObjects)
                        {
                            if (spObj is IPlanet)
                                SetPlanet(spObj as IPlanet, startCoordinate, true);
                        }
                    }
                    else
                    {
                        for (var i = _spaceInfo.CurrentMinX; i <= _spaceInfo.CurrentMaxX; ++i)
                        {
                            for (var j = _spaceInfo.CurrentMaxY - hidePlace; j <= _spaceInfo.CurrentMaxY; ++j)
                            {
                                coordinate.X = i;
                                coordinate.Y = j;
                                SetPlanet(coordinate, true);
                            }
                        }
                    }
                }
                else if (changeNowDelta > narDelta)
                {
                    for (var i = _spaceInfo.CurrentMinX; i <= _spaceInfo.CurrentMaxX; ++i)
                    {
                        for (var j = _spaceInfo.CurrentMaxY - hidePlace; j < _spaceInfo.CurrentMaxY; ++j)
                        {
                            coordinate.X = i;
                            coordinate.Y = j;
                            SetPlanet(coordinate, false);
                        }
                    }

                    _spaceInfo.CurrentMaxY -= hidePlace;
                }
            }

            if ((_nowDirection & MoveDirection.Left) == MoveDirection.Left)
            {
                var changeNowDelta = currentPosition.X - _spaceInfo.CurrentMinX;

                if (changeNowDelta < expansionDelta)
                {
                    _spaceInfo.CurrentMinX -= hidePlace;
                    if (_spaceInfo.CurrentMinX < _spaceInfo.MinX)
                    {
                        _spaceInfo.MinX = _spaceInfo.CurrentMinX;
                        changedObjects = _mapCreater.Generate(hidePlace, _spaceInfo.CurrentMaxY - _spaceInfo.CurrentMinY);

                        var startCoordinate = new Coordinate(_spaceInfo.CurrentMinX, _spaceInfo.CurrentMinY);

                        foreach (var spObj in changedObjects)
                        {
                            if (spObj is IPlanet)
                                SetPlanet(spObj as IPlanet, startCoordinate, true);
                        }
                    }
                    else
                    {
                        for (var i = _spaceInfo.CurrentMinX; i < _spaceInfo.CurrentMinX + hidePlace; ++i)
                        {
                            for (var j = _spaceInfo.CurrentMinY; j <= _spaceInfo.CurrentMaxY; ++j)
                            {
                                coordinate.X = i;
                                coordinate.Y = j;
                                SetPlanet(coordinate, true);
                            }
                        }
                    }
                }
                else if (changeNowDelta > narDelta)
                {
                    for (var i = _spaceInfo.CurrentMinX; i < _spaceInfo.CurrentMinX + hidePlace; ++i)
                    {
                        for (var j = _spaceInfo.CurrentMinY; j <= _spaceInfo.CurrentMaxX; ++j)
                        {
                            coordinate.X = i;
                            coordinate.Y = j;
                            SetPlanet(coordinate, false);
                        }
                    }

                    _spaceInfo.CurrentMinX += hidePlace;
                }
            }

            if ((_nowDirection & MoveDirection.Down) == MoveDirection.Down)
            {
                var changeNowDelta = currentPosition.Y - _spaceInfo.CurrentMinY;

                if (changeNowDelta < expansionDelta)
                {
                    _spaceInfo.CurrentMinY -= hidePlace;
                    if (_spaceInfo.CurrentMinY < _spaceInfo.MinY)
                    {
                        changedObjects = _mapCreater.Generate(_spaceInfo.CurrentMaxX - _spaceInfo.CurrentMinX, hidePlace);

                        var startCoordinate = new Coordinate(_spaceInfo.CurrentMinX, _spaceInfo.CurrentMinY);

                        foreach (var spObj in changedObjects)
                        {
                            if (spObj is IPlanet)
                                SetPlanet(spObj as IPlanet, startCoordinate, true);
                        }
                    }
                    else
                    {
                        for (var i = _spaceInfo.CurrentMinX; i <= _spaceInfo.CurrentMaxX; ++i)
                        {
                            for (var j = _spaceInfo.CurrentMinY; j <= _spaceInfo.CurrentMinY + hidePlace; ++j)
                            {
                                coordinate.X = i;
                                coordinate.Y = j;
                                SetPlanet(coordinate, true);
                            }
                        }
                    }
                }
                else if (changeNowDelta > narDelta)
                {
                    for (var i = _spaceInfo.CurrentMinX; i <= _spaceInfo.CurrentMaxX; ++i)
                    {
                        for (var j = _spaceInfo.CurrentMinY; j <= _spaceInfo.CurrentMinY + hidePlace; ++j)
                        {
                            coordinate.X = i;
                            coordinate.Y = j;
                            SetPlanet(coordinate, false);
                        }
                    }

                    _spaceInfo.CurrentMinY += hidePlace;
                }
            }

            FilterChanges();
            _nowDirection = MoveDirection.None;
        }

        private void SetPlanet(Coordinate pos, bool visible)
        {
            IPlanet planet;
            var hash = pos.GetHashCode();
            if (_space.Planets.TryGetValue(hash, out planet))
            {
                if (visible)
                    _visiblePlanet.Add(planet);
                else
                    _hiddedPlanet.Add(planet);
            }
        }

        private void SetPlanet(IPlanet planet, Coordinate startCoordinate, bool visible)
        {
            var position = planet.Position;
            position.X = startCoordinate.X + position.X;
            position.Y = startCoordinate.Y + position.Y;
            planet.Position = position;
            _space.Planets[position.GetHashCode()] = planet;

            if (visible)
                _visiblePlanet.Add(planet);
            else
                _hiddedPlanet.Add(planet);
        }

        private void FilterChanges()
        {
            _visibleFilter.FilterUnvisible(_hiddedPlanet);
            _visibleFilter.FilterVisible(_visiblePlanet);

            _visiblePlanet.Clear();
            _hiddedPlanet.Clear();
        }
    }
}
