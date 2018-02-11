using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Views
{
    public class VPlanet : MonoBehaviour
    {
        [SerializeField]
        private List<Sprite> _planetsTypeBySprite;

        [SerializeField]
        private SpriteRenderer _sprite;

        [Inject]
        public void Construct()
        {
            Reset();
        }

        public int PlanetType
        {
            set {
                if (value >= _planetsTypeBySprite.Count || value < 0)
                    return;
                _sprite.sprite = _planetsTypeBySprite[value];
            }
        }

        void Reset()
        {
            gameObject.SetActive(false);
            transform.position = Vector2.zero;
            _sprite.sprite = _planetsTypeBySprite[0];
        }

        public class Pool : MonoMemoryPool<Vector2, bool, int, VPlanet>
        {
            protected override void Reinitialize(Vector2 position, bool visible, int planetType, VPlanet planet)
            {
                planet.Reset();
                planet.transform.position = position;
                planet.gameObject.SetActive(visible);
                planet.PlanetType = planetType;
            }
        }
    }
}
