using UniRx;
using System;

namespace Assets.Scripts.Models.Interfaces
{
    public interface ISpecialView : IVisibleFilter
    {
        ReactiveProperty<bool> IsEnabled { get; }
    }
}
