using UniRx;


namespace Assets.Scripts.Models.Interfaces
{
    public interface ISpace
    {
        ReactiveCollection<IStaticObject> Spaces { get; }
        ReactiveCollection<IPlanet> Planets { get; }

        int CurrentMinX { get; set; }
        int CurrentMinY { get; set; }
        int CurrentMaxX { get; set; }
        int CurrentMaxY { get; set; }
        int CurrentScale { get; set; }
    }
}
