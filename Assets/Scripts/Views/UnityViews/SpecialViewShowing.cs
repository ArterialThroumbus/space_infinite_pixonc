using UnityEngine;

namespace Assets.Scripts.Views.UnityViews
{
    public class SpecialViewShowing : MonoBehaviour
    {
        private const float Factor = 20f;
        private const float BorderForFactoring = 80f;
        private Vector2 _initScale;

        protected void Awake()
        {
            _initScale = transform.localScale;
        }

        public void SpecialViewUpdate(bool isEnable, int scale)
        {
            if (isEnable && scale > BorderForFactoring)
            {
                var viewScale = scale / Factor;
                transform.localScale = new Vector2(viewScale, viewScale);
            }
            else
                transform.localScale = _initScale;
        }
    }
}
