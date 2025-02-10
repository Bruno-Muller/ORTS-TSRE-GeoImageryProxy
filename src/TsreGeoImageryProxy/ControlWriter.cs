using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GeoImageryProxy
{
    public class ControlWriter : TextWriter
    {
        // The control where we will write text.
        private readonly Control _control;
        public ControlWriter(Control control)
        {
            _control = control;
        }

        public override void Write(char value)
        {
            if (value == '\r') return;
            if (_control.InvokeRequired)
            {
                _control.BeginInvoke(new Action<char>(Write), value);
                return;
            }
            _control.Text += value;
        }

        public override void Write(string value)
        {
            if (_control.InvokeRequired)
            {
                _control.BeginInvoke(new Action<string>(Write), value);
                return;
            }
            _control.Text += value;
        }

        public override void WriteLine(string value)
        {
            if (_control.InvokeRequired)
            {
                _control.BeginInvoke(new Action<string>(WriteLine), value);
                return;
            }
            _control.Text += value;
        }

        public override Encoding Encoding
        {
            get { return Encoding.ASCII/*Unicode*/; }
        }
    }
}
