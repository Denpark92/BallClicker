using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Scripts
{
    [Serializable]
    public class Range<T>
    {
        public T MinValue;
        public T MaxValue;
    }

    public static class RandomRange
    {
        public static float GetRandomRange(RangeFloat range)
        {
            return Random.Range(range.MinValue, range.MaxValue);
        }
        
        public static int GetRandomRange(RangeInt range)
        {
            return Random.Range(range.MinValue, range.MaxValue + 1);
        }

        public static Vector3 GetRandomRange(RangeVector3 range)
        {
            var x = Random.Range(range.MinValue.x, range.MaxValue.x);
            var y = Random.Range(range.MinValue.y, range.MaxValue.y);
            var z = Random.Range(range.MinValue.z, range.MaxValue.z);
            return new Vector3(x, y, z);
        }
    }

    [Serializable]
    public class RangeVector3 : Range<Vector3>
    {
    }

    [Serializable]
    public class RangeFloat : Range<float>
    {
    }
    
    [Serializable]
    public class RangeInt : Range<int>
    {
    }
    
    [Serializable]
    public class RangeColor : Range<Color>
    {
    }
}