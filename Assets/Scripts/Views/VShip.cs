using UnityEngine;
using Assets.Scripts.Views.Interfaces;
using Assets.Scripts.Views.UnityViews;

namespace Assets.Scripts.Views
{
    public class VShip : MonoBehaviour, IShip, ISpecialViewComponent
    {
        [SerializeField]
        private SpecialViewShowing _specialViewComponent;

        public void MoveTo(Vector2 newPos)
        {
            RotateTo(newPos - (Vector2)transform.position);
            transform.localPosition = newPos;
        }

        public void SpecialView(bool isEnable, int scale)
        {
            _specialViewComponent.SpecialViewUpdate(isEnable, scale);
        }

        private void RotateTo(Vector2 toPos)
        {
            float angle = Mathf.Atan2(toPos.y, toPos.x) * Mathf.Rad2Deg;
            Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = q;
        }
    }
}
