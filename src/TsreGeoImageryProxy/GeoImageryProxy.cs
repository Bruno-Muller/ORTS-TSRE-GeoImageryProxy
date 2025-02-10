using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace GeoImageryProxy
{
    public class GeoImageryProxy
    {
        private int _requestCount = 0;

        public GeoImageryProvider Provider {get; set;}

        public Task Start()
        {
            return Task.Run(ProxyTaskAsync);
        }


        private async Task ProxyTaskAsync()
        {
            //Console.WriteLine("ProxyTask.Begin");
            try
            {
                String url = $"http://localhost:{Properties.Settings.Default.PortNumber}/";

                // Create a Http server and start listening for incoming connections
                using (HttpListener listener = new HttpListener())
                {
                    listener.Prefixes.Add(url);
                    listener.Start();
                    Console.WriteLine($"Listening for connections on {url}");

                    bool runServer = true;

                    // While a user hasn't visited the `shutdown` url, keep on handling requests
                    while (runServer)
                    {
                        // Will wait here until we hear from a connection
                        HttpListenerContext ctx = await listener.GetContextAsync();
                        _ = ProxyRequestHandlerAsync(ctx);
                    }

                    //listener.Close(); // Close the listener
                }
                
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            //Console.WriteLine("ProxyTask.End");
        }

        

        private async Task ProxyRequestHandlerAsync(HttpListenerContext ctx)
        {
            //Console.WriteLine("ProxyRequestHandler.Begin");

            _requestCount++;

            // Peel out the requests and response objects
            HttpListenerRequest req = ctx.Request;

            // Print out some info about the request
            Console.WriteLine(RequestToString(req)); // TODO/

            try
            {
                double longitude = double.Parse(req.QueryString["lon"], System.Globalization.CultureInfo.InvariantCulture);
                double latitude = double.Parse(req.QueryString["lat"], System.Globalization.CultureInfo.InvariantCulture);
                int zoomLevel = int.Parse(req.QueryString["zoom"]);
                int size = int.Parse(req.QueryString["size"]);

                GeoImageryCoord coord = GeoImageryCoord.FromWgs84(latitude, longitude, zoomLevel);
                GeoImageryBitmapBuilder bitmapBuilder = new GeoImageryBitmapBuilder(Provider);

                using (Image image = await bitmapBuilder.ToBitmapAsync(coord, size))
                {
                    using (HttpListenerResponse resp = ctx.Response)
                    {
                        resp.ContentType = "image/jpeg";
                        resp.ContentEncoding = Encoding.UTF8;
                        using (MemoryStream ms = new MemoryStream())
                        {
                            image.Save(ms, ImageFormat.Jpeg);
                            resp.ContentLength64 = ms.Length;
                        }
                        image.Save(resp.OutputStream, ImageFormat.Jpeg);
                        //resp.Close();
                    }
                }               
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e);

#if DEBUG
                byte[] data = Encoding.UTF8.GetBytes(e.ToString());
#else
                byte[] data = Encoding.UTF8.GetBytes(e.Message);
#endif

                using (HttpListenerResponse resp = ctx.Response)
                {
                    resp.ContentType = "text/plain; charset=utf-8";
                    resp.StatusCode = 500;
                    resp.ContentEncoding = Encoding.UTF8;
                    resp.ContentLength64 = data.Length;
                    resp.OutputStream.Write(data, 0, data.Length); // TODO await async
                    //resp.Close();
                }
            }

            //Console.WriteLine("ProxyRequestHandler.End");
        }

        private String RequestToString(HttpListenerRequest req)
        {
            return $"Request:{_requestCount} Url:{req.Url} UserAgent:{req.UserAgent}";
            /*StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Request #: {_requestCount}");
            sb.AppendLine($"Url: {req.Url}");
            sb.AppendLine($"HttpMethod: {req.HttpMethod}");
            sb.AppendLine($"UserHostName: {req.UserHostName}");
            sb.AppendLine($"UserAgent: {req.UserAgent}");*/

            /*if (req.AcceptTypes != null)
            {
                foreach (String type in req.AcceptTypes) sb.AppendLine($"AcceptType: {type}");
            }
            else
            {
                sb.AppendLine($"AcceptType: None");
            }*/

            //return sb.ToString();
        }
    }
}
