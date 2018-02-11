using System;

namespace Assets.Scripts.Models.Interfaces
{
    public interface ISpecialViewChecker
    {
        Action<bool> Checking { get; set; }
        void Check();
    }
}
