using CodeBase.Infrastructure.Services;

namespace CodeBase.Infrastructure.Factory
{
    public interface IGameFactory : IService
    {
        public Car CreateCar();
        public TestSlider CreateSlider();
    }
}