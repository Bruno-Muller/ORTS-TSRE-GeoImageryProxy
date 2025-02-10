using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace GeoImageryProxy
{
    public class GeoImageryProvider
    {
        public static readonly WebClient Client = new WebClient();
        public static String RootFolder;
        private static readonly Random _random = new Random();

        public String Name { get; set; }
        public String DisplayName { get; set; }

        public String GridType { get; set; }

        public int MaxThread { get; set; }

        public readonly List<String> Urls = new List<String>();

        private String GetUrl()
        {
            int i = _random.Next(0, Urls.Count);
            return Urls[i];
        }

        private String EncodeUrl(GeoImageryCoord gic)
        {
            String url = GetUrl();
            url = url.Replace("{Latitude}", gic.Latitude.ToString());
            url = url.Replace("{Longitude}", gic.Longitude.ToString());
            url = url.Replace("{ZoomLevel}", gic.ZoomLevel.ToString());
            url = url.Replace("{TileX}", gic.TileX.ToString());
            url = url.Replace("{TileY}", gic.TileY.ToString());
            url = url.Replace("{QuadKey}", gic.QuadKey.ToString());

            return url;
        }

        public Task<Image> GetTileImageAsync(GeoImageryCoord coord)
        {
            return Task.FromResult(GetTileImage(coord));
        }

        public Image GetTileImage(GeoImageryCoord coord)
        {
            String path = Path.Combine(RootFolder, Name, $"Z{coord.ZoomLevel}", $"{coord.QuadKey}.jpg");

            if (!File.Exists(path))
            {
                string url = EncodeUrl(coord);
                Console.WriteLine($"Download from {url} to cache {path}");
                return DownloadTileImage(path, url);
            }
            else
            {
                Console.WriteLine($"Use tile from cache {path}");
                return Image.FromFile(path);
            }  
        }

        public Image DownloadTileImage(String path, String url)
        {            
            String directory = Path.GetDirectoryName(path);
            if (!Directory.Exists(directory)) {
                Directory.CreateDirectory(directory);
            }

            if (Properties.Settings.Default.ForceUserAgent)
            {
                GeoImageryProvider.Client.Headers[HttpRequestHeader.UserAgent] = Properties.Settings.Default.UserAgent;
            }

            using (Stream stream = Client.OpenRead(url))
            {
                Bitmap bitmap = new Bitmap(stream);
                //if (bitmap == null) throw new Exception();
                bitmap.Save(path, ImageFormat.Jpeg);

                //stream.Flush();
                //stream.Close();
                //_client.Dispose();

                return bitmap;
            }
        }

        public override string ToString()
        {
            return DisplayName;
        }

    }
}
