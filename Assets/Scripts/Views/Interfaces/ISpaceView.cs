using Assets.Scripts.Models.Interfaces;

namespace Assets.Scripts.Views.Interfaces
{
    public interface ISpaceView
    {
        void AddPlanet(IPlanet planet);
        void HidePlanet(IPlanet planet);
        void ShowPlanet(IPlanet planet);
        void HideAll();
        void SpecialView(bool isEnable);
    }
}
