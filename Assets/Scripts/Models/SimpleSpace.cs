using Assets.Scripts.Models.Interfaces;
using UniRx;


namespace Assets.Scripts.Models
{
    public class SimpleSpace : ISpace
    {
        public ReactiveCollection<IStaticObject> Spaces { get; set; }
        public ReactiveCollection<IPlanet> Planets { get; set; }
        public int CurrentMinX { get; set; }
        public int CurrentMinY { get; set; }
        public int CurrentMaxX { get; set; }
        public int CurrentMaxY { get; set; }
    }
}
