using Assets.Scripts.Models.Interfaces;
using System;
using Zenject;

namespace Assets.Scripts.Models
{
    public abstract class BaseShipController : IDisposable, IInitializable
    {
        [Inject]
        protected IShipModel _ship;
        [Inject(Id = "input_manager")]
        protected IInputSubscriber _input;

        public abstract void Dispose();

        public abstract void Initialize();
    }
}
