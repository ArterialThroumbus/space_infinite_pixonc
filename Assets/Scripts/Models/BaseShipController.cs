using System;
using Zenject;

namespace Assets.Scripts.Models
{
    public abstract class BaseShipController : IDisposable, IInitializable
    {
        [Inject]
        protected IShipModel _ship;

        public abstract void Dispose();

        public abstract void Initialize();
    }
}
