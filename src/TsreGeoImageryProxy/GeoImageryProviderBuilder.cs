using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoImageryProxy
{
    public class GeoImageryProviderBuilder
    {
        private readonly GeoImageryProvider provider = new GeoImageryProvider();

        public GeoImageryProviderBuilder SetName(String name)
        {
            provider.Name = name;
            return this;
        }

        public GeoImageryProviderBuilder SetDisplayName(String displayName)
        {
            provider.DisplayName = displayName;
            return this;
        }

        public GeoImageryProviderBuilder SetGridType(String gridType)
        {
            provider.GridType = gridType;
            return this;
        }

        public GeoImageryProviderBuilder SetMaxThread(int maxThread)
        {
            provider.MaxThread = maxThread;
            return this;
        }

        public GeoImageryProviderBuilder AddUrl(String url)
        {
            provider.Urls.Add(url);
            return this;
        }
        public GeoImageryProvider Build()
        {
            return provider;
        }
    }
}
