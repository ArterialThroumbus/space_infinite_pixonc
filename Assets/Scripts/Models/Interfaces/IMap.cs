using System.Collections.Generic;

namespace Assets.Scripts.Models.Interfaces
{
    public interface IMap
    {
        IEnumerable<IStaticObject> Generate(int width, int height);
    }
}
