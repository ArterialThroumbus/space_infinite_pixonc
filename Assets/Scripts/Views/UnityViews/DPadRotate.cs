using System;
using UnityEngine;
using UniRx;

namespace Assets.Scripts.Views.UnityViews
{
    public class DPadRotate : MonoBehaviour
    {
        private const float RotateAngle = 25;
        private const float RecoveryWait = 200; //in ms
        private Transform _cacheTransform;

        protected void Awake()
        {
            _cacheTransform = transform;
        }

        public void Right()
        {
            _cacheTransform.localRotation = Quaternion.Euler(0, RotateAngle, 0);
            Recovery();
        }

        public void Up()
        {
            _cacheTransform.localRotation = Quaternion.Euler(RotateAngle, 0, 0);
            Recovery();
        }

        public void Left()
        {
            _cacheTransform.localRotation = Quaternion.Euler(0, -RotateAngle, 0);
            Recovery();
        }

        public void Down()
        {
            _cacheTransform.localRotation = Quaternion.Euler(-RotateAngle, 0, 0);
            Recovery();
        }

        private void Recovery()
        {
            Observable.Timer(TimeSpan.FromMilliseconds(RecoveryWait))
                .Subscribe(_ => _cacheTransform.localRotation = Quaternion.identity);
        }
    }
}
