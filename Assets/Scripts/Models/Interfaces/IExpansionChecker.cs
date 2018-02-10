using System;

namespace Assets.Scripts.Models.Interfaces
{
    public interface IExpansionChecker
    {
        event Action<SpaceChanging, MoveDirection> Changing;
        void Check();
    }
}
