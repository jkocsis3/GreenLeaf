
using System;
using System.IO;
using System.Linq;

namespace GreenLeaf.Core.Utilities
{
    public static class ImageHandler
    {     
        /// <summary>
        /// Returns the path to the default image
        /// </summary>
        /// <param name="plantId">The is od the Plant</param>
        /// <param name="weekId">The current week</param>
        /// <returns>the <see cref="Path"/> of the image</returns>
        public static string GetDefaultImagePath(int plantId, int weekId)
        {
            string localStorage = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData));
            DirectoryInfo directory = new DirectoryInfo(localStorage + $"\\{plantId}\\{weekId}\\");
            try
            {
                FileInfo info = directory.GetFiles().Where(x => !x.Name.Contains("_small")).OrderByDescending(x => x.LastWriteTime).FirstOrDefault();
                return $"{info?.DirectoryName}\\{info?.Name}";
            }
            catch (DirectoryNotFoundException)
            {
                return string.Empty;
            }
            catch (FileNotFoundException)
            {
                return string.Empty;
            }

        }

        /// <summary>
        /// Gets a image rendered for smaller screens
        /// </summary>
        /// <param name="plantId">The is od the Plant</param>
        /// <param name="weekId">The current week</param>
        /// <returns>the <see cref="Path"/> of the image</returns>
        public static string GetSmallImagePath(int plantId, int weekId)
        {
            string localStorage = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData));
            DirectoryInfo directory = new DirectoryInfo(localStorage + $"\\{plantId}\\{weekId}\\");
            try
            {
                FileInfo info = directory.GetFiles().Where(x => x.Name.Contains("_small")).OrderByDescending(x => x.LastWriteTime).FirstOrDefault();
                return $"{info?.DirectoryName}\\{info?.Name}";
            }
            catch (DirectoryNotFoundException)
            {
                return string.Empty;
            }
            catch (FileNotFoundException)
            {
                return string.Empty;
            }

        }

        /// <summary>
        /// Gets a image rendered for smaller screens
        /// </summary>
        /// <param name="plantId">The is od the Plant</param>
        /// <returns>the <see cref="Path"/> of the image</returns>
        public static string GetMostRecentDefaultImage(int plantId)
        {
            string localStorage = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData));
            DirectoryInfo directory = new DirectoryInfo(localStorage + $"\\{plantId}\\");
            try
            {
                FileInfo info = directory.GetFiles("*", SearchOption.AllDirectories).Where(x => !x.Name.Contains("_small")).OrderByDescending(x => x.LastWriteTime).FirstOrDefault();
                return $"{info?.DirectoryName}\\{info?.Name}";
            }
            catch (DirectoryNotFoundException)
            {
                return string.Empty;
            }
            catch (FileNotFoundException)
            {
                return string.Empty;
            }
        }

        public static bool GetMostRecentSmallImage(int plantId, out string imagePath)
        {
            string localStorage = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData));
            DirectoryInfo directory = new DirectoryInfo(localStorage + $"\\{plantId}\\");
            try
            {
                FileInfo info = directory.GetFiles("*", SearchOption.AllDirectories).Where(x => x.Name.Contains("_small")).OrderByDescending(x => x.LastWriteTime).FirstOrDefault();
                imagePath = $"{info?.DirectoryName}\\{info?.Name}";
                return true;
            }
            catch (DirectoryNotFoundException)
            {
                imagePath = string.Empty;
            }
            catch (FileNotFoundException)
            {
                imagePath = string.Empty;
            }
            return false;
        }
    }
}
