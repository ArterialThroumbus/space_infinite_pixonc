using Assets.Scripts.Signals;

namespace Assets.Scripts.Models.Interfaces
{
    public interface ISpecialView : IVisibleFilter
    {
        SpecialViewChanged Changed { get; }
    }
}
