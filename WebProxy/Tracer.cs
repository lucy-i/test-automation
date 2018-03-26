using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WebProxy
{
    public partial class RequestListener : Form
    {

        HttpListener listener;
        bool isRunning = false;
        public RequestListener()
        {
            InitializeComponent();
            this.button1.Enabled = true;
            this.btnStop.Enabled = false;
        }

        void cc()
        {

            if (!HttpListener.IsSupported)
            {
                Console.WriteLine("Windows XP SP2 or Server 2003 is required to use the HttpListener class.");
                return;
            }

        }

        public async Task ListenHttp()
        {
            // Create a listener.
            listener = new HttpListener();

            // URI prefixes are required,
            // for example "http://contoso.com:8080/index/".
            // Add the prefixes.
            try
            {
                listener.Prefixes.Add(this.monitorTxt.Text);
                listener.Start();
                while (isRunning)
                {
                    // Note: The GetContext method blocks while waiting for a request. 
                    HttpListenerContext context = await listener.GetContextAsync();
                    HttpListenerRequest request = context.Request;
                    this.responseTxt.Text = request.RawUrl;

                    // Obtain a response object.
                    HttpListenerResponse response = context.Response;
                    // Construct a response.
                    string responseString = "<HTML><BODY> Hello world!</BODY></HTML>";
                    byte[] buffer = new byte[response.OutputStream.Length];//System.Text.Encoding.UTF8.GetBytes(responseString);
                                                                           // Get a response stream and write the response to it.
                    response.ContentLength64 = buffer.Length;
                    System.IO.Stream output = response.OutputStream;
                    output.Write(buffer, 0, buffer.Length);
                    // You must close the output stream.
                    string s_unicode2 = System.Text.Encoding.UTF8.GetString(buffer);

                    this.responseTxt.Text = s_unicode2;
                    output.Close();
                }

            }
            catch (Exception ex)
            {
                this.responseTxt.Text = ex.Message;
                listener.Stop();
            }
            finally
            {

            }

            return;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.button1.Enabled = false;
            this.btnStop.Enabled = true;
            isRunning = true;
            ListenHttp();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            this.button1.Enabled = true;
            this.btnStop.Enabled = false;
            isRunning = false;
            if (listener != null&& listener.IsListening)
                listener.Stop();
        }
    }
}
