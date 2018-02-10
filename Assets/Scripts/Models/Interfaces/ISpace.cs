using UniRx;


namespace Assets.Scripts.Models.Interfaces
{
    public interface ISpace
    {
        ReactiveCollection<IStaticObject> Spaces { get; }
        ReactiveCollection<IPlanet> Planets { get; }
    }
}
