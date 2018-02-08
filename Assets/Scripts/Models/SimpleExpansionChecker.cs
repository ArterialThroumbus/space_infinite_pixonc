using Assets.Scripts.Models.Interfaces;
using System;
using Zenject;

namespace Assets.Scripts.Models
{
    public class SimpleExpansionChecker : IExpansionChecker
    {
        [Inject]
        private IMovableObject _ship;
        [Inject]
        private ISpace _space;

        public event Action<SpaceChanging> Changing = (changing) => { };


        public void Check()
        {
            
        }
    }
}
