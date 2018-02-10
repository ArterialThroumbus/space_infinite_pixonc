using Assets.Scripts.Models.Interfaces;
using System;
using System.Collections.Generic;
using UniRx;
using Zenject;

namespace Assets.Scripts.Models
{
    public class SimpleCameraFollowingModel : ICameraFollowingModel, IInitializable
    {
        [Inject]
        private SpaceInfo _spaceInfo;
        [Inject]
        private IMovableObject _movableObject;

        public ReactiveProperty<Coordinate> CurrentPosition { get; private set; }

        public void Initialize()
        {
            CurrentPosition = new ReactiveProperty<Coordinate>();
            _movableObject.Position.Subscribe(CalculateNewCameraPosition);
        }

        private void CalculateNewCameraPosition(Coordinate newMovablePos)
        {
            var delta = _spaceInfo.CurrentScale / 4; // move camera if movable object on half from center
            var deltaByX = Math.Abs(newMovablePos.X - CurrentPosition.Value.X);
            var deltaByY = Math.Abs(newMovablePos.Y - CurrentPosition.Value.Y);

            if (deltaByX >= delta || deltaByY >= delta)
                CurrentPosition.Value = newMovablePos;
        }
    }
}
