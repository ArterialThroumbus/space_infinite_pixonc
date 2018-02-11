using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Models.Interfaces
{
    public interface IVisibleFilter
    {
        void FilterVisible(IEnumerable<IPlanet> planets);
        void FilterUnvisible(IEnumerable<IPlanet> planets);
    }
}
