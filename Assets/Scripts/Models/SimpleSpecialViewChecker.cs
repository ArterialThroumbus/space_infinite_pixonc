using Assets.Scripts.Models.Interfaces;
using System;
using Zenject;

namespace Assets.Scripts.Models
{
    public class SimpleSpecialViewChecker : ISpecialViewChecker, IInitializable
    {
        [Inject]
        private IScaling _scaling;
        [Inject]
        private SpaceInfo _spaceInfo;
        [Inject]
        private Configuration _configuration;

        private Action<bool> _checking = (a) => { };

        public Action<bool> Checking {
            get { return _checking; }
            set { _checking += value; }
        }

        public void Initialize()
        {
            _scaling.Changed += (scale) => Check();
            Check();
        }

        public void Check()
        {
            var isSpecial = _configuration.SpecialViewScale <= _spaceInfo.CurrentScale;
            _checking(isSpecial);
        }
    }
}
