using System.Collections.Generic;

namespace CognitiveXR.Cpop
{
    [System.Serializable]
    public struct CpopData
    {
        public float Timestamp;
        public string Type;
        public Coordinates Position;
        public List<Coordinates> Shape;

        public override string ToString()
        {
            return $"{Timestamp}: {Type}/{Position}";
        }
    }

    [System.Serializable]
    public struct Coordinates
    {
        public float X;
        public float Y;
        public float Z;
        
        public override string ToString()
        {
            return $"({X},{Y},{Z})";
        }
    }
}