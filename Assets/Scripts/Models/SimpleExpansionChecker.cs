using Assets.Scripts.Models.Interfaces;
using System;
using Zenject;

namespace Assets.Scripts.Models
{
    public class SimpleExpansionChecker : IExpansionChecker
    {
        [Inject]
        private SpaceInfo _spaceInfo;
        [Inject]
        private Configuration _configuration;
        [Inject]
        private ICameraFollowingModel _followingModel;

        public event Action<SpaceChanging, MoveDirection> Changing = (a, b) => { };

        public void Check()
        {
            var currentPos = _followingModel.CurrentPosition.Value;
            var scale = (_spaceInfo.CurrentScale / 2f) / _configuration.HiddenSpace;
            var expansionDelta = _configuration.HiddenSpace * scale;
            var narDelta = expansionDelta * 3;

            var rightDelta = _spaceInfo.CurrentMaxX - currentPos.X;
            var upDelta = _spaceInfo.CurrentMaxY - currentPos.Y;
            var leftDelta = currentPos.X - _spaceInfo.CurrentMinX;
            var downDelta = currentPos.Y - _spaceInfo.CurrentMinY;

            var direction = MoveDirection.None;

            if (rightDelta < expansionDelta || rightDelta > narDelta)
                direction |= MoveDirection.Right;

            if (upDelta < expansionDelta || upDelta > narDelta)
                direction |= MoveDirection.Up;

            if (leftDelta < expansionDelta || leftDelta >= narDelta)
                direction |= MoveDirection.Left;

            if (downDelta < expansionDelta || downDelta > narDelta)
                direction |= MoveDirection.Down;
            
            if (direction != MoveDirection.None)
                Changing(SpaceChanging.None, direction);
        }
    }
}
