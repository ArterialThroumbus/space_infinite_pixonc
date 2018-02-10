using Assets.Scripts.Models;
using Assets.Scripts.Models.Interfaces;
using Assets.Scripts.Views.Interfaces;
using Zenject;

namespace Assets.Scripts.Presenters
{
    public class ScalingPresenter : IInitializable
    {
        [Inject]
        private IScaling _scaling;
        [Inject]
        private IScaleManipulationView _scalingView;
        [Inject]
        private SpaceInfo _spaceInfo;

        public void Initialize()
        {
            _scaling.Changed += _scalingView.Scale;
            _scalingView.Scale(_spaceInfo.CurrentScale);
        }
    }
}
