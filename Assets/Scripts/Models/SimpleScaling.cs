﻿using Assets.Scripts.Models.Interfaces;
using System;
using Zenject;
using UniRx;

namespace Assets.Scripts.Models
{
    public class SimpleScaling : IScaling, IInitializable
    {
        [Inject]
        private SpaceInfo _spaceInfo;
        [Inject]
        private Configuration _configuration;
        [Inject]
        private IExpansionChecker _expansionChecker;
        [Inject(Id = "input_manager")]
        private IInputSubscriber _input;

        private readonly int DeltaChange = 5;
        private Action<int> _changed = (scale) => { };

        public Action<int> Changed
        {
            get
            {
                return _changed;
            }

            set
            {
                _changed += value;
            }
        }

        public void Initialize()
        {
            _spaceInfo.CurrentScale = _configuration.MinScale;

            _input.ScaleUpFire().Subscribe(_ => {
                _spaceInfo.CurrentScale = Math.Min(_spaceInfo.CurrentScale + DeltaChange, _configuration.MaxScale);
                _expansionChecker.Check();
                if (_changed != null)
                    _changed(_spaceInfo.CurrentScale);
            });

            _input.ScaleDownFire().Subscribe(_ =>
            {
                _spaceInfo.CurrentScale = Math.Max(_spaceInfo.CurrentScale - DeltaChange, _configuration.MinScale);
                _expansionChecker.Check();
                if (_changed != null)
                    _changed(_spaceInfo.CurrentScale);
            });
        }
    }
}
