using Assets.Scripts.Models.Interfaces;
using Assets.Scripts.Views.Interfaces;
using System.Collections.Generic;
using Zenject;
using UniRx;
using Assets.Scripts.Signals;

namespace Assets.Scripts.Presenters
{
    public class SpecialViewPresenter : IInitializable
    {
        [Inject]
        private SpecialViewChanged _specialViewSignal;
        [Inject]
        private IList<ISpecialViewComponent> _specialViews;

        public SpecialViewPresenter(IList<ISpecialViewComponent> specialViews)
        {
            _specialViews = specialViews;
        }

        public void Initialize()
        {
            _specialViewSignal += SpecialViewChanged;
        }

        private void SpecialViewChanged(bool isEnable, int scale)
        {
            foreach (var spView in _specialViews)
                spView.SpecialView(isEnable, scale);
        }
    }
}
