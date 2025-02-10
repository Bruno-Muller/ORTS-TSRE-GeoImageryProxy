using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoImageryProxy
{
    public class GeoImageryCoord
    {

        public static void Wgs84ToPix(double latitude, double longitude, double zoomLevel, out double x, out double y)
        {
            double rat_x = longitude / 180.0;
            double rat_y = Math.Log(Math.Tan((90.0 + latitude) * Math.PI / 360.0)) / Math.PI;
            x = Math.Round((rat_x + 1) * Math.Pow(2, zoomLevel + 7));
            y = Math.Round((1 - rat_y) * Math.Pow(2, zoomLevel + 7));
        }

        public static void PixToWgs84(double x, double y, double zoomLevel, out double latitude, out double longitude)
        {
            double rat_x = (x / Math.Pow(2, zoomLevel + 7) - 1);
            double rat_y = (1 - y / Math.Pow(2, zoomLevel + 7));
            longitude = rat_x * 180;
            latitude = 360 / Math.PI * Math.Atan(Math.Exp(Math.PI * rat_y)) - 90;
        }

        public static void PixToGtile(double x, double y, out int tileX, out int tileY)
        {
            tileX = (int)Math.Floor(x / 256.0);
            tileY = (int)Math.Floor(y / 256.0);
        }

        public static void GtileToPix(int tileX, int tileY, out double x, out double y)
        {
            x = tileX * 256.0;
            y = tileY * 256.0;
        }

        public static String GtileToQuadkey(int tileX, int tileY, int zoomLevel)
        {
            // Translates Google coding of tiles to Bing Quadkey coding.
            StringBuilder quadKey = new StringBuilder();
            double temp_x = tileX;
            double temp_y = tileY;

            for (int step = 1; step <= zoomLevel; step++)
            {
                double size = Math.Pow(2, zoomLevel - step);
                double a = Math.Floor(temp_x / size);
                double b = Math.Floor(temp_y / size);

                temp_x = temp_x - a * size;
                temp_y = temp_y - b * size;

                quadKey.Append(a + 2 * b);
            }

            return quadKey.ToString();
        }

        private double _latitude, _longitude;
        private double _pixelX, _pixelY;
        private int _tileX, _tileY;
        
        public double Latitude { get { return _latitude; } }
        public double Longitude { get { return _longitude; } }

        public double PixelX { get { return _pixelX; } }
        public double PixelY { get { return _pixelY; } }

        public int TileX { get { return _tileX; } }
        public int TileY { get { return _tileY; } }


        public int ZoomLevel { get; private set; }

        public String QuadKey { get; private set; }

        private GeoImageryCoord()
        {

        }

        public static GeoImageryCoord FromPixel(double x, double y, int zoomLevel)
        {
            GeoImageryCoord it = new GeoImageryCoord();

            it._pixelX = x;
            it._pixelY = y;
            it.ZoomLevel = zoomLevel;

            PixToWgs84(x, y, zoomLevel, out it._latitude, out it._longitude);
            PixToGtile(x, y, out it._tileX, out it._tileY);
            it.QuadKey = GtileToQuadkey(it._tileX, it._tileY, it.ZoomLevel);

            return it;
        }

        public static GeoImageryCoord FromWgs84(double latitude, double longitude, int zoomLevel)
        {
            GeoImageryCoord it = new GeoImageryCoord();

            it._latitude = latitude;
            it._longitude = longitude;
            it.ZoomLevel = zoomLevel;

            Wgs84ToPix(it._latitude, it._longitude, it.ZoomLevel, out it._pixelX, out it._pixelY);
            PixToGtile(it._pixelX, it._pixelY, out it._tileX, out it._tileY);
            it.QuadKey = GtileToQuadkey(it._tileX, it._tileY, it.ZoomLevel);

            return it;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Latitude: ");
            sb.Append(Latitude);
            sb.Append(" Longitude: ");
            sb.Append(Longitude);
            return sb.ToString();
        }
    }
}
