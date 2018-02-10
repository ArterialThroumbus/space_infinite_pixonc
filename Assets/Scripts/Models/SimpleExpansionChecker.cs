using Assets.Scripts.Models.Interfaces;
using System;
using Zenject;

namespace Assets.Scripts.Models
{
    public class SimpleExpansionChecker : IExpansionChecker
    {
        private const int DeltaForChanging = 10; 

        [Inject]
        private IMovableObject _ship;
        [Inject]
        private SpaceInfo _spaceInfo;

        public event Action<SpaceChanging, MoveDirection> Changing = (a, b) => { };

        public void Check()
        {
            var shipCoord = _ship.Position;

            var rightDelta = _spaceInfo.CurrentMaxX - shipCoord.Value.X;
            var upDelta = _spaceInfo.CurrentMaxY - shipCoord.Value.Y;
            var leftDelta = shipCoord.Value.X - _spaceInfo.CurrentMinX;
            var downDelta = shipCoord.Value.Y - _spaceInfo.CurrentMinY;
        }
    }
}
