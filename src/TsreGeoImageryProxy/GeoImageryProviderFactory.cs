using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace GeoImageryProxy
{
    public class GeoImageryProviderFactory
    {
        public static List<GeoImageryProvider> CreateProviders(String rootFolder)
        {
            GeoImageryProvider.RootFolder = rootFolder;
            
            List<GeoImageryProvider> list = new List<GeoImageryProvider>();
            list.Add(
                new GeoImageryProviderBuilder()
                .SetName("BING")
                .SetDisplayName("Microsoft Bing Maps")
                //imagery_dir=grouped
                .AddUrl("http://r0.ortho.tiles.virtualearth.net/tiles/a{QuadKey}.jpeg?g=136")
                .AddUrl("http://r1.ortho.tiles.virtualearth.net/tiles/a{QuadKey}.jpeg?g=136")
                .AddUrl("http://r2.ortho.tiles.virtualearth.net/tiles/a{QuadKey}.jpeg?g=136")
                .AddUrl("http://r3.ortho.tiles.virtualearth.net/tiles/a{QuadKey}.jpeg?g=136")
                .Build()
                );

            list.Add(
                new GeoImageryProviderBuilder()
                .SetName("GOOGLE")
                .SetDisplayName("Google Maps")
                .SetGridType("webmercator")
                .SetMaxThread(16)
                //imagery_dir=grouped
                .AddUrl("http://mt0.google.com/vt/lyrs=s&x={TileX}&y={TileY}&z={ZoomLevel}")
                .AddUrl("http://mt1.google.com/vt/lyrs=s&x={TileX}&y={TileY}&z={ZoomLevel}")
                .AddUrl("http://mt2.google.com/vt/lyrs=s&x={TileX}&y={TileY}&z={ZoomLevel}")
                .AddUrl("http://mt3.google.com/vt/lyrs=s&x={TileX}&y={TileY}&z={ZoomLevel}")
                .Build()
                );


            list.Add(
                new GeoImageryProviderBuilder()
                .SetName("IGN")
                .SetDisplayName("Geoportail IGN")
                .AddUrl("https://data.geopf.fr/wmts?layer=ORTHOIMAGERY.ORTHOPHOTOS&style=normal&tilematrixset=PM&Service=WMTS&Request=GetTile&Version=1.0.0&Format=image%2Fjpeg&TileMatrix={ZoomLevel}&TileCol={TileX}&TileRow={TileY}")
                .Build()
                );

            list.Add(
               new GeoImageryProviderBuilder()
               .SetName("IGN_1950")
               .SetDisplayName("Geoportail IGN (1950-1965)")
               .AddUrl("https://data.geopf.fr/wmts?layer=ORTHOIMAGERY.ORTHOPHOTOS.1950-1965&style=BDORTHOHISTORIQUE&tilematrixset=PM&Service=WMTS&Request=GetTile&Version=1.0.0&Format=image%2Fpng&TileMatrix={ZoomLevel}&TileCol={TileX}&TileRow={TileY}")
               .Build()
               );

            /*
            list.Add(
               new GeoImageryProviderBuilder()
               .SetName("IGN_1820")
               .SetDisplayName("Geoportail IGN (1820-1866) Etat-Major")
               .AddUrl("https://data.geopf.fr/wmts?layer=GEOGRAPHICALGRIDSYSTEMS.ETATMAJOR40&style=BDORTHOHISTORIQUE&tilematrixset=PM&Service=WMTS&Request=GetTile&Version=1.0.0&Format=image%2Fpng&TileMatrix={ZoomLevel}&TileCol={TileX}&TileRow={TileY}")
               .Build()
               );
            */
            return list;
        }
    }
}
