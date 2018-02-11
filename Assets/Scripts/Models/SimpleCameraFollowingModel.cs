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
        [Inject]
        private IExpansionChecker _expansionChecker;
        [Inject] IScaling _scaling;

        public ReactiveProperty<Coordinate> CurrentPosition { get; private set; }
        private bool _initalized;

        public void Initialize()
        {
            CurrentPosition = new ReactiveProperty<Coordinate>();
            _movableObject.Position.Subscribe(CalculateNewCameraPosition);
            _initalized = true;
        }

        private void CalculateNewCameraPosition(Coordinate newMovablePos)
        {
            CurrentPosition.Value = newMovablePos;
            if (_initalized)
                _expansionChecker.Check();
        }
    }
}
