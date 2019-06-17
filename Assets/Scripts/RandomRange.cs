using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Scripts
{
    [Serializable]
    public class RandomRange<T>
    {
        public T MinValue;
        public T MaxValue;
    }

    public static class RandomRange
    {
        public static float GetRandomRange(RandomRangeFloat range)
        {
            return Random.Range(range.MinValue, range.MaxValue);
        }
        
        public static int GetRandomRange(RandomRangeInt range)
        {
            return Random.Range(range.MinValue, range.MaxValue + 1);
        }

        public static Vector3 GetRandomRange(RandomRangeVector3 range)
        {
            var x = Random.Range(range.MinValue.x, range.MaxValue.x);
            var y = Random.Range(range.MinValue.y, range.MaxValue.y);
            var z = Random.Range(range.MinValue.z, range.MaxValue.z);
            return new Vector3(x, y, z);
        }
    }

    [Serializable]
    public class RandomRangeVector3 : RandomRange<Vector3>
    {
    }

    [Serializable]
    public class RandomRangeFloat : RandomRange<float>
    {
    }
    
    [Serializable]
    public class RandomRangeInt : RandomRange<int>
    {
    }
}