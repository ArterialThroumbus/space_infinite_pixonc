using Assets.Scripts.Models.Interfaces;
using Assets.Scripts.Views.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UniRx;
using Zenject;

namespace Assets.Scripts.Views
{
    public class VSimpleCameraFollowing : ICameraFollowing, IInitializable
    {
        private const float FollowingTime = 2.0f;
        private Transform _cameraTransform;
        private IDisposable _currentMoving;
        
        public void Initialize()
        {
            _cameraTransform = Camera.main.transform;
            Camera.main.aspect = 1f;
        }

        public void MoveTo(Vector2 newPos)
        {
            if (_currentMoving != null)
                _currentMoving.Dispose();

            var startTime = Time.time;
            _currentMoving = Observable.EveryUpdate().TakeWhile(_ => Time.time - startTime <= FollowingTime).Subscribe(_ => {
                if (_cameraTransform == null)
                    return;

                var partToComplete = (Time.time - startTime) / FollowingTime;
                var nextPos = Vector3.Lerp(_cameraTransform.position, newPos, partToComplete);
                nextPos.z = _cameraTransform.position.z;
                _cameraTransform.position = nextPos;
            });
        }
    }
}
