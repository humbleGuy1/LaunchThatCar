using CodeBase.Infrastructure.AssetManagment;
using UnityEngine;

namespace CodeBase.Infrastructure.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssetProvider _assets;

        public GameFactory(IAssetProvider assets)
        {
            _assets = assets;
        }

        public Car CreateCar()
        {
            Car car = CreateObjectOfType<Car>(AssetPath.CarPath);
            return car;
        }

        public TestSlider CreateSlider()
        {
            TestSlider slider = CreateObjectOfType<TestSlider>(AssetPath.FuelSliderPath);
            return slider;
        }

        private TComponent CreateObjectOfType<TComponent>(string parth) where TComponent : Component
        {
            var gameObject = _assets.Instantiate(parth);
            var type = gameObject.GetComponent<TComponent>();

            return type;
        }
    }
}

