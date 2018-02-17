using System;
using Zenject;
using Assets.Scripts.Models.Interfaces;
using System.Collections.Generic;

namespace Assets.Scripts.Models
{
    public class SimpleMap : IMap, IInitializable
    {
        [Inject]
        private Configuration _configuration;
        [Inject]
        private int seed;

        private Random _random;

        public void Initialize()
        {
            _random = new Random(seed);
        }

        public IEnumerable<IStaticObject> Generate(int width, int height)
        {
            var result = new IStaticObject[width * height];
            var planetRation = _configuration.RationOfPlanets;

            for (var i = 0; i < width; ++i)
            {
                for (var j = 0; j < height; ++j)
                {
                    if (_random.NextDouble() < planetRation)
                        result[i * height + j] = new SimplePlanet {
                            Position = new Coordinate(i, j),
                            Rank = _random.Next(_configuration.MinRank, _configuration.MaxRank + 1),
                            PlanetType = _random.Next(0, _configuration.PlanetsType)
                        };
                }
            }

            return result;
        }
    }
}
