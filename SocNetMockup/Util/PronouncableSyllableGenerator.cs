using System;
using System.Collections.Generic;

namespace SocNetMockup.Util
{
    public static class PronouncableSyllableGenerator
    {
        public static readonly char[] Consonants = "bdgptkmnlr".ToCharArray();
        public static readonly char[] Vowels = "aouie".ToCharArray();

        public static readonly Random Rng = new();
        public static string Pick<T>(T[] arr) => arr[Rng.Next(0, arr.Length)].ToString();

        public static string Generate(int syllableCount)
        {
            var strings = new List<string>();

            for (int i = 0; i < syllableCount; i++) {
                string onset = Pick(Consonants);
                string nucleus = Pick(Vowels);
                string coda = Pick(Consonants);

                strings.Add(onset);
                strings.Add(nucleus);
                strings.Add(coda);
            }

            return string.Join("", strings);
        }
    }
}
