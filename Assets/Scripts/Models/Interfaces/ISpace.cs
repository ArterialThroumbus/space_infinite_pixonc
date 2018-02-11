using UniRx;


namespace Assets.Scripts.Models.Interfaces
{
    public interface ISpace
    {
        ReactiveDictionary<int, IPlanet> Planets { get; }
    }
}
