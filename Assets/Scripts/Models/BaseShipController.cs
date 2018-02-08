using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Zenject;

namespace Assets.Scripts.Models
{
    public abstract class BaseShipController : IDisposable
    {
        [Inject]
        protected BaseShip _ship;

        public abstract void Dispose();
    }
}
