using Assets.Scripts.Models;
using UnityEngine;

namespace Assets.Scripts.Views.Interfaces
{
    public interface ICoordinateConverter
    {
        Vector2 Convert(Coordinate coordinate);
    }
}
