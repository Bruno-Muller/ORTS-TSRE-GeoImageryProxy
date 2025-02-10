using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GeoImageryProxy
{   
    public partial class FormGeoImageryProxy : Form
    {
        public FormGeoImageryProxy()
        {
            InitializeComponent();
        }

        private GeoImageryProxy _proxy = new GeoImageryProxy();

        private void Form1_Load(object sender, EventArgs e)
        {
            if (!Directory.Exists(Properties.Settings.Default.ImageryFolder))
            {
                Directory.CreateDirectory(Properties.Settings.Default.ImageryFolder);
            }


#if !DEBUG
            StreamWriter sw = new StreamWriter(Path.Combine(Properties.Settings.Default.ImageryFolder, "log.txt"), false, Encoding.Unicode);
            sw.AutoFlush = true;
            Console.SetOut(sw);
            Console.SetError(sw);
#endif
            //Console.WriteLine("Form1_Load.Begin");

            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"BETA RELEASE");
            sb.AppendLine();
            sb.AppendLine("Make sure you have the correct setting for \"imageMapsUrls\" in TSRE \"settings.txt\" file:");
            sb.AppendLine($"imageMapsUrl = http://localhost:{Properties.Settings.Default.PortNumber}/?lon={{lon}}&lat={{lat}}&zoom={{zoom}}&res={{res}}");
            sb.AppendLine();
            sb.AppendLine($"Raw images from providers will be stored at \"{Properties.Settings.Default.ImageryFolder}\"");
            sb.AppendLine($"Port numbers is \"{Properties.Settings.Default.PortNumber}\"");
            sb.AppendLine();
            sb.AppendLine("You can change these settings in the configuration file \"GeoImageryProxy.exe.config\":");
            sb.AppendLine("1. Close the application");
            sb.AppendLine("2. Edit the configuration file.");
            sb.AppendLine("3. Start the application");
            sb.AppendLine();
            this.richTextBoxLog.Text += sb.ToString();

            this.comboBoxProvider.DataSource = GeoImageryProviderFactory.CreateProviders(Properties.Settings.Default.ImageryFolder);
            this.comboBoxProvider.SelectedIndex = 1;
            _proxy.Start();
            //proxyTask.GetAwaiter().GetResult();

            this.richTextBoxLog.Text += $"Test me: http://localhost:{Properties.Settings.Default.PortNumber}/?lon=7.4812&lat=47.9551&zoom=18&size=640";

            //Console.WriteLine("Form1_Load.End");
        }

        private void OnComboBoxProviderChanged(object sender, EventArgs e)
        {
            Console.WriteLine($"Provider Changed {this.comboBoxProvider.SelectedItem}");
            _proxy.Provider = (GeoImageryProvider)this.comboBoxProvider.SelectedItem;
        }

        private void OnLinkClicked(object sender, LinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(e.LinkText);
        }

        private void OnToolStripStatusLabelClicked(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start($"file://{Properties.Settings.Default.ImageryFolder}");
        }
    }
}
