using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using PathFinderLib.City;

namespace PathFinderCLI
{
    internal class CityInputInfo
    {
        [JsonProperty]
        public Point2D TopLeftCorner { get; private set; }
        
        [JsonProperty]
        public Point2D BottomRightCorner { get; private set; }

        [JsonProperty]
        public List<Tuple<Point2D, Point2D>> Roads { get; private set; }

        public CityInputInfo(Point2D topLeftCorner, Point2D bottomRightCorner, List<Tuple<Point2D, Point2D>> roads)
        {
            TopLeftCorner = topLeftCorner;
            BottomRightCorner = bottomRightCorner;
            Roads = roads;
        }

        public static CityInputInfo Deserialize(string json)
        {
            return JsonConvert.DeserializeObject<CityInputInfo>(json);
        }
    }
}