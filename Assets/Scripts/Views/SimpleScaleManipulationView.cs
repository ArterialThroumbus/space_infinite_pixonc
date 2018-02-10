using Assets.Scripts.Views.Interfaces;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Views
{
    public class SimpleScaleManipulationView : IScaleManipulationView, IInitializable
    {
        private Camera _camera;

        public void Scale(int scale)
        {
            _camera.orthographicSize = scale;
        }

        public void Initialize()
        {
            _camera = Camera.main;
        }
    }
}
