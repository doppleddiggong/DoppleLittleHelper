using System;
using System.Linq;

namespace DoppleLittleHelper
{
    public class FileHelper
    {
        public static string CombinePaths(params string[] paths)
        {
            if (paths == null || paths.Length == 0)
                throw new ArgumentNullException(nameof(paths), "Paths cannot be null or empty.");
            return paths.Aggregate(System.IO.Path.Combine);
        }
    }
}