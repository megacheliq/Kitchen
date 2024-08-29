using Kitchen.Service.DataAccess.Models;
using Kitchen.Service.Domain.Kitchen.Abstract;
using Kitchen.Service.Domain.Kitchen.Models;

namespace Kitchen.Service.Domain.Kitchen.Services
{
    public class KitchenValidationService : IKitchenValidationService
    {
        public List<PlacedModule> FindOptimalPositions(KitchenResponse kitchen, List<PlacedModule> modules)
        {
            // Создаем список для размещенных модулей
            var placedModules = new List<PlacedModule>();

            // Разделяем модули на те, которые имеют специфические требования и те, которые не имеют
            var modulesWithSpecificRequirements = modules
                .Where(m => m.Module.RequiresWater || m.Module.IsCorner)
                .ToList();
            var otherModules = modules.Except(modulesWithSpecificRequirements).ToList();

            // Сначала размещаем модули с особыми требованиями
            PlaceOptimalModules(kitchen, modulesWithSpecificRequirements, placedModules);

            // Затем размещаем остальные модули
            PlaceOptimalModules(kitchen, otherModules, placedModules);

            return placedModules;
        }

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

        private List<Coordinate> GetPotentialWallPositions(KitchenResponse kitchen, PlacedModule module)
        {
            var positions = new List<Coordinate>();

            // Определяем позиции вдоль левой и правой стен
            for (double y = 0; y <= kitchen.Height - module.Module.Height; y += 0.1)
            {
                positions.Add(new Coordinate { X = 0, Y = y }); // Левый край
                positions.Add(new Coordinate { X = kitchen.Width - module.Module.Width, Y = y }); // Правый край
            }

            // Определяем позиции вдоль верхней и нижней стен
            for (double x = 0; x <= kitchen.Width - module.Module.Width; x += 0.1)
            {
                positions.Add(new Coordinate { X = x, Y = 0 }); // Верхний край
                positions.Add(new Coordinate { X = x, Y = kitchen.Height - module.Module.Height }); // Нижний край
            }

            // Сортируем позиции так, чтобы сначала пробовать ближе к углам
            positions = positions.OrderBy(p => p.X).ThenBy(p => p.Y).ToList();

            return positions;
        }

        private void PlaceOptimalModules(KitchenResponse kitchen, List<PlacedModule> modules, List<PlacedModule> placedModules)
        {
            foreach (var module in modules)
            {
                bool placed = false;

                // Получаем список возможных позиций вдоль стен
                var potentialPositions = GetPotentialWallPositions(kitchen, module);

                // Проходим по всем возможным позициям
                foreach (var position in potentialPositions)
                {
                    // Сначала проверяем ориентацию модуля с длинной стороной вдоль стены
                    var preferredOrientations = GetPreferredOrientations(kitchen, position, module);
                    foreach (var orientation in preferredOrientations)
                    {
                        var workModule = new PlacedModule
                        {
                            Module = module.Module.Clone(),
                            Orientation = orientation,
                            Coordinate = position
                        };

                        if (workModule.Orientation == Orientation.Vertical)                       
                            (workModule.Module.Width, workModule.Module.Height) = (workModule.Module.Height, workModule.Module.Width);               

                        // Проверка, что модуль находится в пределах кухни
                        if (!ModuleFitsInKitchen(kitchen, workModule))
                            continue;

                        // Проверка, что модуль не пересекается с другими модулями
                        if (placedModules.Any(existingModule => ModulesOverlap(existingModule, workModule)))
                            continue;

                        // Проверка условий, если модуль требует воду
                        if (module.Module.RequiresWater && !IsNearWaterPipe(kitchen, workModule, 0.5))
                            continue;

                        // Проверка условий, если модуль угловой
                        if (module.Module.IsCorner && !IsInCorner(kitchen, workModule, 0.5))
                            continue;

                        // Если все условия выполнены, размещаем модуль
                        placedModules.Add(new PlacedModule
                        {
                            Module = workModule.Module,
                            Coordinate = new Coordinate
                            {
                                X = position.X,
                                Y = position.Y
                            },
                            Orientation = orientation
                        });
                        placed = true;
                        break;
                    }

                    if (placed)
                        break;
                }

                if (!placed)
                {
                    throw new Exception($"Не удалось найти подходящее место для модуля {module.Module.Name}");
                }
            }
        }

        // Метод для определения предпочтительных ориентаций модуля
        private List<Orientation> GetPreferredOrientations(KitchenResponse kitchen, Coordinate position, PlacedModule module)
        {
            var preferredOrientations = new List<Orientation>();

            // Проверяем, на какой стене находится модуль, и выбираем предпочтительную ориентацию
            if (position.X == 0 || position.X + module.Module.Width == kitchen.Width)
            {
                // Модуль вдоль левой или правой стены — предпочтительнее вертикальная ориентация
                preferredOrientations.Add(Orientation.Vertical);
                preferredOrientations.Add(Orientation.Horizontal);
            }
            else if (position.Y == 0 || position.Y + module.Module.Height == kitchen.Height)
            {
                // Модуль вдоль верхней или нижней стены — предпочтительнее горизонтальная ориентация
                preferredOrientations.Add(Orientation.Horizontal);
                preferredOrientations.Add(Orientation.Vertical);
            }
            else
            {
                // Если модуль не вдоль стен, пробуем сначала все возможные ориентации
                preferredOrientations.Add(Orientation.Horizontal);
                preferredOrientations.Add(Orientation.Vertical);
            }

            return preferredOrientations;
        }
    }
}