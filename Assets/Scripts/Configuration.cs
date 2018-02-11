using UnityEngine;

namespace Assets.Scripts
{
    [CreateAssetMenu(fileName = "Configuration", menuName = "Configration/Default", order = 1)]
    public class Configuration : ScriptableObject
    {
        public string objectName = "Configuration";
        public int MinScale = 5;
        public int MaxScale = 10000;
        public int SpecialViewScale = 10;
        public int CountPlanetInSpecialView = 20;

        public int MinRank = 0;
        public int MaxRank = 10000;
        public float RationOfPlanets = 0.3f;
        public int HiddenSpace = 5;
        public int PlanetsType = 9;
    }
}
