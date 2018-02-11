namespace Assets.Scripts.Models.Interfaces
{
    public interface IPlanet : IStaticObject, IRank
    {
        int PlanetType { get; set; }
    }
}
