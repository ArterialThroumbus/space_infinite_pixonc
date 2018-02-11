using Assets.Scripts.Models.Interfaces;
using UniRx;

namespace Assets.Scripts.Models
{
    public class SimpleSpace : ISpace
    {
        public ReactiveDictionary<int, IPlanet> Planets { get; private set; }

        public SimpleSpace()
        {
            Planets = new ReactiveDictionary<int, IPlanet>();
        }
    }
}
