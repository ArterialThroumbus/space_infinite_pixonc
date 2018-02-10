using Assets.Scripts.Models;
using Assets.Scripts.Views.Interfaces;
using UnityEngine;

namespace Assets.Scripts.Views
{
    public class SimpleCoordinateConverter : ICoordinateConverter
    {
        private readonly float XRation = 2.0f;
        private readonly float YRation = 2.0f;

        public Vector2 Convert(Coordinate coordinate)
        {
            return new Vector2(coordinate.X * XRation, coordinate.Y * YRation);
        }
    }
}
