using System;

namespace Assets.Scripts.Models.Interfaces
{
    public interface IScaling
    {
        Action<int> Changed { get; set; }
    }
}
