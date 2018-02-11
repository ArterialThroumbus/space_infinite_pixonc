using Assets.Scripts.Models.Interfaces;
using UniRx;

namespace Assets.Scripts.Models
{
    public class SimplePlanet : IPlanet
    {
        private ReactiveProperty<bool> _isVisible = new ReactiveProperty<bool>(false);

        public Coordinate Position { get; set; }

        public int Rank { get; set; }

        public ReactiveProperty<bool> IsVisible
        {
            get
            {
                return _isVisible;
            }
        }

        public int PlanetType { get; set; }
    }
}
