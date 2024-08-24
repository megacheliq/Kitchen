using Kitchen.Service.DataAccess.Models;
using Kitchen.Service.Domain.Kitchen.Abstract;
using Kitchen.Service.Domain.Kitchen.Models;

namespace Kitchen.Service.Domain.Kitchen.Services
{
    public class KitchenValidationService : IKitchenValidationService
    {
        public bool ModulesOverlap(PlacedModule existingModule, PlacedModule newModule)
        {
            // Определение крайних координат существующего модуля
            var existingEndX = existingModule.Coordinate.X + existingModule.Module.Width;
            var existingEndY = existingModule.Coordinate.Y + existingModule.Module.Height;

            // Определение крайних координат нового модуля
            var newEndX = newModule.Coordinate.X + newModule.Module.Width;
            var newEndY = newModule.Coordinate.Y + newModule.Module.Height;

            // Проверка пересекаются ли модули по осям X и Y
            return newModule.Coordinate.X < existingEndX && newEndX > existingModule.Coordinate.X &&
                   newModule.Coordinate.Y < existingEndY && newEndY > existingModule.Coordinate.Y;
        }

        public bool ModuleFitsInKitchen(KitchenResponse kitchen, PlacedModule module)
        {
            // Проверка, что координаты модуля находятся в пределах кухни
            return module.Coordinate.X >= 0 &&
                   module.Coordinate.Y >= 0 &&
                   module.Coordinate.X + module.Module.Width <= kitchen.Width &&
                   module.Coordinate.Y + module.Module.Height <= kitchen.Height;
        }

        public bool IsNearWaterPipe(KitchenResponse kitchen, PlacedModule module, double radius)
        {
            // Координаты углов модуля
            double moduleStartX = module.Coordinate.X;
            double moduleStartY = module.Coordinate.Y;
            double moduleEndX = moduleStartX + module.Module.Width;
            double moduleEndY = moduleStartY + module.Module.Height;

            // Координаты трубы
            double pipeX = kitchen.WaterPipe.X;
            double pipeY = kitchen.WaterPipe.Y;

            // Проверка, находится ли труба в пределах радиуса от модуля
            return IsPointNearModule(pipeX, pipeY, moduleStartX, moduleStartY, moduleEndX, moduleEndY, radius);
        }

        public bool IsInCorner(KitchenResponse kitchen, PlacedModule module, double radius)
        {
            // Координаты углов модуля
            double moduleStartX = module.Coordinate.X;
            double moduleStartY = module.Coordinate.Y;
            double moduleEndX = moduleStartX + module.Module.Width;
            double moduleEndY = moduleStartY + module.Module.Height;

            // Проверка расстояния до каждого из углов кухни
            return IsPointNearModule(0, 0, moduleStartX, moduleStartY, moduleEndX, moduleEndY, radius) ||
                   IsPointNearModule(kitchen.Width, 0, moduleStartX, moduleStartY, moduleEndX, moduleEndY, radius) ||
                   IsPointNearModule(0, kitchen.Height, moduleStartX, moduleStartY, moduleEndX, moduleEndY, radius) ||
                   IsPointNearModule(kitchen.Width, kitchen.Height, moduleStartX, moduleStartY, moduleEndX, moduleEndY, radius);
        }

        // Универсальный метод для проверки расстояния от точки до модуля
        private bool IsPointNearModule(double pointX, double pointY, double moduleStartX, double moduleStartY, double moduleEndX, double moduleEndY, double radius)
        {
            // Проверка, находится ли точка внутри модуля
            if (pointX >= moduleStartX && pointX <= moduleEndX && pointY >= moduleStartY && pointY <= moduleEndY)
            {
                return true; // Точка внутри модуля, возвращаем true
            }

            // Вычисление минимального расстояния от точки до сторон модуля
            double nearestX = Math.Max(moduleStartX, Math.Min(pointX, moduleEndX));
            double nearestY = Math.Max(moduleStartY, Math.Min(pointY, moduleEndY));

            // Вычисление Евклидова расстояния от точки до ближайшей точки модуля
            double distance = Math.Sqrt(Math.Pow(nearestX - pointX, 2) + Math.Pow(nearestY - pointY, 2));

            // Проверка, находится ли расстояние в пределах заданного радиуса
            return distance <= radius;
        }
    }
}