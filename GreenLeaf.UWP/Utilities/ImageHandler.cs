using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using Windows.Graphics.Imaging;
using Windows.Storage.Streams;

namespace GreenLeaf.UWP.Utilities
{
    public static class ImageHandler
    {
        public static async Task<bool> SaveImage(int plantId, int weekId)
        {
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                return false;
            }
            MediaFile photo = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions()
            {
                Directory = $"{plantId}\\{weekId}\\",
                AllowCropping = false,
            });

            if (photo == null) return false;

            byte[] resizedImage = await ResizeImageWindows(photo, 100, 100);
            string newPath = photo.Path.Insert(photo.Path.Length - 4, "_small");
            File.WriteAllBytes(newPath, resizedImage);

            return photo != null;
        }

        public static async Task<string> SaveProgressImage(int plantId, int weekId)
        {
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                return null;
            }
            MediaFile photo = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions()
            {
                Directory = $"{plantId}\\{weekId}\\",
                AllowCropping = false,
            });

            if (photo == null) return null;

            byte[] resizedImage = await ResizeImageWindows(photo, 100, 100);
            string newPath = photo.Path.Insert(photo.Path.Length - 4, "_small");
            File.WriteAllBytes(newPath, resizedImage);
            return newPath;
        }

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

        public static string GetMostRecentSmallImage(int plantId)
        {
            string localStorage = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData));
            DirectoryInfo directory = new DirectoryInfo(localStorage + $"\\{plantId}\\");
            try
            {
                FileInfo info = directory.GetFiles("*", SearchOption.AllDirectories).Where(x => x.Name.Contains("_small")).OrderByDescending(x => x.LastWriteTime).FirstOrDefault();
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


        public static async Task<byte[]> ResizeImageWindows(MediaFile file, float width, float height)
        {
            byte[] resizedData;
            Stream stream = file.GetStream();
            stream.Position = 0;
            byte[] buffer = new byte[stream.Length];
            for (int totalBytesCopied = 0; totalBytesCopied < stream.Length;)
            {
                totalBytesCopied += stream.Read(buffer, totalBytesCopied,
                    Convert.ToInt32(stream.Length) - totalBytesCopied);
            }
            using (var streamIn = new MemoryStream(buffer))
            {
                using (var imageStream = streamIn.AsRandomAccessStream())
                {
                    var decoder = await BitmapDecoder.CreateAsync(imageStream);
                    var resizedStream = new InMemoryRandomAccessStream();
                    var encoder = await BitmapEncoder.CreateForTranscodingAsync(resizedStream, decoder);
                    encoder.BitmapTransform.InterpolationMode = BitmapInterpolationMode.Linear;
                    encoder.BitmapTransform.ScaledHeight = (uint)height;
                    encoder.BitmapTransform.ScaledWidth = (uint)width;
                    await encoder.FlushAsync();
                    resizedStream.Seek(0);
                    resizedData = new byte[resizedStream.Size];
                    await resizedStream.ReadAsync(resizedData.AsBuffer(), (uint)resizedStream.Size, InputStreamOptions.None);
                }
            }

            return resizedData;
        }

    }
}
