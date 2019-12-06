using System;
using System.Linq;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Utilities
{
    public static class CodeGenerator
    {
        private static Random random = new Random();
        public static string Generate(int length)
        {
            const string pool = "abcdefghijklmnopqrstuvwxyz0123456789";
            var chars = Enumerable.Range(0, length)
                .Select(x => pool[random.Next(0, pool.Length)]);
            return new string(chars.ToArray()).ToUpper();
        }
    }
}