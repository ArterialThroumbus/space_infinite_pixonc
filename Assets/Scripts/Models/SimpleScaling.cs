using Assets.Scripts.Models.Interfaces;
using System;
using UnityEngine;
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

        private readonly int DeltaChange = 5;
        private readonly string Scaling = "Scaling";
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

            Observable.EveryUpdate().Where(_ => Input.anyKeyDown).Subscribe(_ => {
                var scaling = Input.GetAxis(Scaling);

                if (scaling > 0)
                {
                    _spaceInfo.CurrentScale = Math.Min(_spaceInfo.CurrentScale + DeltaChange, _configuration.MaxScale);
                    if (_changed != null)
                        _changed(_spaceInfo.CurrentScale);
                }
                else if (scaling < 0)
                {
                    _spaceInfo.CurrentScale = Math.Max(_spaceInfo.CurrentScale - DeltaChange, _configuration.MinScale);
                    if (_changed != null)
                        _changed(_spaceInfo.CurrentScale);
                }
            });
        }
    }
}
