using System.Collections.Generic;
using UnityEngine;

namespace Tools
{

    public static class WeightedRandom
    {
        public static T GetRandom<T>(List<WeightedValue<T>> values)
        {
            var totalWeight = 0f;

            foreach (var v in values) totalWeight += v.Weight;

            var random = Random.Range(0, totalWeight);

            foreach (var v in values)
            {
                if (random < v.Weight) return v.Value;
                random -= v.Weight;
            }

            return values[0].Value;
        }
    }
}

public class WeightedValue<T> { public T Value; public float Weight; }
