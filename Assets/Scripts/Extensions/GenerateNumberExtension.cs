using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Extensions
{
    public static class GenerateNumberExtension
    {
        public static IEnumerable<int> GenerateUniqueValues(int count, int min, int max, int interval)
        {
            HashSet<int> values = new HashSet<int>();
            List<int> possibleValues = Enumerable.Range(min / 1000, (max - min) / 1000 + 1)
                .Select(x => x * 1000)
                .OrderBy(_ => Random.value)
                .ToList();

            foreach (int value in possibleValues)
            {
                if (values.Count >= count) break;
                if (!values.Any(v => Mathf.Abs(v - value) < interval))
                    values.Add(value);
            }
            return values;
        }

        public static string FormatNumber(this int num)
        {
            StringBuilder sb = new StringBuilder();
            if (num >= 1_000_000)
                sb.Append((num / 1_000_000f).ToString("F1")).Append("m");
            else if (num >= 1_000)
                sb.Append((num / 1_000f).ToString("F1")).Append("k");
            else
                sb.Append(num);
        
            return sb.ToString();
        }
    }
}