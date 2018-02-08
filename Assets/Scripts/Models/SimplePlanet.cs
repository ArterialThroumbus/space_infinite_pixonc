using Assets.Scripts.Models.Interfaces;

namespace Assets.Scripts.Models
{
    public class SimplePlanet : IPlanet
    {
        public Coordinate Position { get; set; }

        public int Rank { get; set; }
    }
}
