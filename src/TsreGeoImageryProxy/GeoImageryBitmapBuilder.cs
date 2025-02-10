using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoImageryProxy
{
    public class GeoImageryBitmapBuilder
    {

        private GeoImageryProvider _provider;

        public GeoImageryBitmapBuilder(GeoImageryProvider provider)
        {
            _provider = provider;
        }


        public async Task<Image> ToBitmapAsync(GeoImageryCoord c, int size)
        {
            //Console.WriteLine("ToBitmap.Begin");

            Bitmap bitmap = new Bitmap(size, size);

            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                // Coordinate of the top left corner of the image
                double imageTopLeftX = c.PixelX - size / 2;
                double imageTopLeftY = c.PixelY - size / 2;

                // Coordinate of the top left corner of the tile (of the top left corner of the image)
                double tileTopLeftX = Math.Floor(imageTopLeftX / 256) * 256;
                double tileTopLeftY = Math.Floor(imageTopLeftY / 256) * 256;

                // Pixel Offset between top left corner of the image, and top left corner of the tile
                double offsetWorldX = imageTopLeftX - tileTopLeftX;
                double offsetWorldY = imageTopLeftY - tileTopLeftY;

                IList<Task> getImageTasks = new List<Task>();

                Dictionary<Task, double> ImageX = new Dictionary<Task, double>();
                Dictionary<Task, double> ImageY = new Dictionary<Task, double>();

                // We iterate on all tiles covering the image
                for (double x = 0; x - offsetWorldX < size; x += 256)
                    for (double y = 0; y - offsetWorldY < size; y += 256)
                    {
                        GeoImageryCoord coord = GeoImageryCoord.FromPixel(imageTopLeftX + x, imageTopLeftY + y, c.ZoomLevel);
                        Task task = _provider.GetTileImageAsync(coord);
                        ImageX[task] = x;
                        ImageY[task] = y;
                        getImageTasks.Add(task);

                        //using (Image image = _provider.GetTileImage(coord))
                        //{
                        //    graphics.DrawImage(image, (float)(x - offsetWorldX), (float)(y - offsetWorldY));
                        //}
                    }

                await Task.WhenAll(getImageTasks);

                foreach (Task<Image> t in getImageTasks)
                {
                    using (Image image = t.Result)
                    {
                        graphics.DrawImage(image, (float)(ImageX[t] - offsetWorldX), (float)(ImageY[t] - offsetWorldY));
                    }
                }

            }
            //Console.WriteLine("ToBitmap.End");
            return bitmap;
        }
    }
}
