using System;
using System.IO;
using PathFinderLib;
using PathFinderLib.City;
using PathFinderLib.City.Institutions;
using PathFinderLib.City.Institutions.Factories;
using PathFinderLib.GraphEngine.Algorithms;

namespace PathFinderCLI
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            string inputFilePath;
            
            if (args.Length < 2)
            {
                Console.WriteLine("Пожалуйста, укажите путь к входному файлу.");
                inputFilePath = Console.ReadLine();
            }
            else
            {
                inputFilePath = args[1];
            }
            
            CityInputInfo cityInputInfo = ReadCityInputInfoFromFile(inputFilePath);
            if (cityInputInfo == null)
            {
                return;
            }
            
            City city = BuildCity(cityInputInfo);
            Console.WriteLine("Городские дороги: ");
            foreach (Road road in city.Roads)
            {
                Console.WriteLine(road.ToString());
            }

            while (true)
            {
                if (!TryAskStartPoint(city, out var startPoint)) return;
                if (!TryAskEndPoint(out var institutionType)) return;

                PathInfo pathInfo = Finder.FindPathTo(city, startPoint, institutionType);

                if (pathInfo.IsEmptyPath())
                {
                    Console.WriteLine("Не удалось найти путь.");
                    return;
                }

                Console.WriteLine($"Путь найден: {pathInfo}. Общая продолжительность {pathInfo.TotalLength}");
                Console.WriteLine("Нажмите любую клавишу для продолжения...");
                Console.ReadKey();
            }
        }

        /// <summary>
        /// Получает содержимое файла по указанному пути, и читает из него сведения о городе.
        /// Если не удалось прочитать, то возвращает null.
        /// </summary>
        /// <param name="inputFilePath">Путь ко входному файлу, откуда нужно прочитать информацию о городе.</param>
        /// <returns>Данные о городе, либо null, если не удалось прочитать.</returns>
        private static CityInputInfo ReadCityInputInfoFromFile(string inputFilePath)
        {
            if (!File.Exists(inputFilePath))
            {
                Console.WriteLine("Не удается найти входной файл по указанному пути.");
                return null;
            }

            string inputFileContent;

            try
            {
                inputFileContent = File.ReadAllText(inputFilePath);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                return null;
            }

            CityInputInfo cityInputInfo = CityInputInfo.Deserialize(inputFileContent);
            if (cityInputInfo == null)
            {
                Console.WriteLine("Не удалось обработать файл.");
                return null;
            }
            return cityInputInfo;
        }

        /// <summary>
        /// Выводит список доступных учреждений, а также спрашивает у пользователя тип нужный ему
        /// тип учреждения. Если пользователь указал корректный тип, то возвращает true, а в
        /// <paramref name="institutionType"/> записывает этот самый тип.
        /// Если тип был указан некорректно, то возвращает false.
        /// </summary>
        /// <param name="institutionType">Возвращаемый тип учреждения, который выбрал пользователь.</param>
        /// <returns>true, если тип учреждения корректный. false, если нет.</returns>
        private static bool TryAskEndPoint(out InstitutionType institutionType)
        {
            Console.WriteLine("Доступные виды учреждений: ");
            string[] availableInstitutions = Enum.GetNames(typeof(InstitutionType));
            for (int i = 0; i < availableInstitutions.Length; i++)
            {
                Console.WriteLine($"{i}. {availableInstitutions[i]}");
            }
            
            Console.Write("Введите номер типа учреждения, к которому будет искаться путь: ");
            string answer = Console.ReadLine();
            
            if (!Enum.TryParse(answer, out institutionType))
            {
                Console.WriteLine("Введен некорректный номер.");
                return false;
            }

            return true;
        }

        /// <summary>
        /// Выводит список построенных учреждений в городе, а затем спрашивает у пользователя какое учреждение
        /// ему нужно в качестве стартовой точки. Если пользователь указал учреждение корректно, то возвращает true, а
        /// в <paramref name="startInstitution"/> устанавливается объект этого учреждения. Если нет, то возвращает
        /// false.
        /// </summary>
        /// <param name="city">Объект «города», в котором находятся нужные учреждения.</param>
        /// <param name="startInstitution">Возвращаемый объект учреждения, который выбрал пользователь.</param>
        /// <returns>true, если объект выбран корректно. false, если нет.</returns>
        private static bool TryAskStartPoint(City city, out Institution startInstitution)
        {
            startInstitution = default(Institution);
            
            Console.WriteLine("Учреждения в городе: ");
            for (var i = 0; i < city.Institutions.Count; i++)
            {
                Institution institution = city.Institutions[i];
                Console.WriteLine($"{i}. {institution}");
            }
            
            Console.Write("Введите индекс учреждения, с которого начать путь (или -1 для завершения): ");
            string answer = Console.ReadLine();

            int institutionIndex = 0;
            if (!int.TryParse(answer, out institutionIndex) || 
                institutionIndex < 0 || institutionIndex >= city.Institutions.Count)
            {
                if (institutionIndex != -1)
                {
                    Console.WriteLine("Введен неверный индекс.");
                }
                return false;
            }

            startInstitution = city.Institutions[institutionIndex];
            return true;
        }

        /// <summary>
        /// Осуществляет «постройку» города, исходя из полученной в <paramref name="cityInputInfo"/> информации.
        /// </summary>
        /// <param name="cityInputInfo">Информация о городе, который нужно «построить».</param>
        /// <returns>Объект «построенного» города.</returns>
        private static City BuildCity(CityInputInfo cityInputInfo)
        {
            City city = new City(cityInputInfo.TopLeftCorner, cityInputInfo.BottomRightCorner, new RandomBuilder());
            foreach (var point2DPair in cityInputInfo.Roads)
            {
                city.AddRoad(new Road(point2DPair.Item1, point2DPair.Item2));
            }

            return city;
        }
    }
}