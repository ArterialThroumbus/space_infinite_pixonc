using UnityEngine;
using Assets.Scripts.Models;
using UniRx;
using Assets.Scripts.Views.Interfaces;
using Zenject;

namespace Assets.Scripts.Views
{
    public class VShip : MonoBehaviour, IShip
    {
        public void MoveTo(Vector2 newPos)
        {
            RotateTo(newPos - (Vector2)transform.position);
            transform.localPosition = newPos;
        }

        private void RotateTo(Vector2 toPos)
        {
            float angle = Mathf.Atan2(toPos.y, toPos.x) * Mathf.Rad2Deg;
            Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = q;
        }
    }
}
