using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
namespace FormBrowser
{
    public partial class NetworkTracer : Form
    {
        public NetworkTracer()
        {
            InitializeComponent();
        }

        [DllImport("wininet.dll", SetLastError = true)]
        private static extern bool InternetSetOption(IntPtr dwl, int dw, IntPtr dwB, int dwb);
        public struct SIPI
        {
            public int dwAT;
            public IntPtr pro;
            public IntPtr prB;
        }

        private void UseProxy(string proxy)
        {
            const int PO = 38;
            const int POI = 3;
            SIPI ISI = default(SIPI);

            ISI.dwAT = POI;
            ISI.pro = Marshal.StringToHGlobalAnsi(proxy);
            ISI.prB = Marshal.StringToHGlobalAnsi("local");

            IntPtr INS = Marshal.AllocCoTaskMem(Marshal.SizeOf(ISI));

            Marshal.StructureToPtr(ISI, INS, true);

            bool ir = InternetSetOption(IntPtr.Zero, PO, INS, Marshal.SizeOf(ISI));
        }
        private void btnGO_Click(object sender, EventArgs e)
        {
            webBrowser1.Navigate(txtURL.Text);
        }

        private void btnProxy_Click(object sender, EventArgs e)
        {
            UseProxy(txtProxy.Text);
            proxyLabel.Text = txtProxy.Text;
        }
    }
}
