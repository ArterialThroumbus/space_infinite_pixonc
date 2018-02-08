using System;

namespace Assets.Scripts.Models.Interfaces
{
    public interface IExpansionChecker
    {
        event Action<SpaceChanging> Changing;
        void Check();
    }
}
