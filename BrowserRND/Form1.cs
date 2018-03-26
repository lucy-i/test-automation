using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BrowserRND
{
    public partial class BowserForm : Form
    {
        public BowserForm()
        {

            InitializeComponent();
            this.urltxt.Text = "http://localhost:8000";
            Assembly _assembly = Assembly.GetExecutingAssembly();
            StreamReader _textStreamReader = new StreamReader(_assembly.GetManifestResourceStream("BrowserRND.engine.min.js"));
            txtScript.Text = _textStreamReader.ReadToEnd();
            // "Element.prototype.matches||(Element.prototype.matches=Element.prototype.msMatchesSelector),Element.prototype.closest||(Element.prototype.closest=function(e){for(var t=this;t;){if(t.matches(e))return t;t=t.parentElement}});var engineHelper=function(e){return{addStep:function(t){try{e.external.PushRecordSteps(JSON.stringify(t))}catch(e){console.error(e)}},addSteps:function(t){try{e.external.PushAllRecordSteps(JSON.stringify(t))}catch(e){console.error(e)}},play:function(t){try{e.external.Play()}catch(e){return console.error(e),[]}},log:function(t){console.log(t);try{e.external.log(t)}catch(e){console.error(e)}},showSelector:function(t){console.log(t);try{e.external.ElementSelector(t)}catch(e){console.error(e)}}}}(window),engine=function(e,t){var n=[],o=function(e){var o=s(e.target);n.push({action:\"click\",selector:o}),t.addSteps(n)},r=function(e){t.log(\"_keyPressRecord\"+e.target.tagName);var o=s(e.target);t.log(\"_keyPressRecord target \"+o);for(var r=-1,l=0;l<n.length;++l)if(\"input\"==n[l].action&&n[l].selector==o){r=l;break}t.log(\"_keyPressRecord :: \"+r),-1==r?n.push({action:\"input\",selector:o,value:e.target.value}):n[r].value=e.target.value,t.log(\"_keyPressRecord\"),t.addSteps(n)},l=function(){e.addEventListener(\"keydown\",r),e.addEventListener(\"click\",o)},a=function(){e.removeEventListener(\"click\",o),e.removeEventListener(\"keydown\",r),t.addSteps(c()),t.log(\"Recorded\",c())},c=function(){return n},i=function(){t.log(\"call play action\"),t.play()},s=function(e){if(\"HTML\"==e.tagName|\"Body\")return e.tagName;var t=[];t.push(e.tagName),t=t.concat(u(e.attributes));var n=d(t);return\"string\"==typeof n?n:s(e.parentElement)+\" \"+n[0]},u=function(e){for(var t=[],n=0;n<e.length;n++){const o=e[n];\"id\"!=o.name?\"class\"!=o.name?o.value?t.push(\"[\"+o.name+\"='\"+o.value+\"']\"):t.push(\"[\"+o.name+\"]\"):t.push(\".\"+o.value.split(\" \").join(\".\")):t.push(\"#\"+o.value)}return t},d=function(t){for(let n=0;n<t.length;n++){const o=t[n];if(1==e.querySelectorAll(o).length)return o}var n=t.join(\"\");if(1==e.querySelectorAll(n).length)return n;{const t=[];for(let o=0;o<e.querySelectorAll(n).length;o++){e.querySelectorAll(n).length[o];t.push(n)}return t}},p=function(){let t=e.createElement(\"div\"),n=e.createElement(\"style\");t.innerHTML=\"<div id='test-auto-too'>\n        <label>[CTRL + ALT + R] = Record</label>\n        <br/>\n        <label> [CTRL + ALT + S] = Stop </label>\n        <br/>\n        <label> [CTRL + ALT + P] = Play </label>\n    </div>\",n.innerHTML=\"#test-auto-too {display: block;position: fixed;opacity: 0.5;bottom: 20px;right: 20px;padding: 20px;} #test-auto-too:hover {opacity: 1;border: 1px solid blue;}\",e.body&&(e.body.appendChild(t),e.body.appendChild(n))};return e.addEventListener(\"mouseover\",function(e){var n=s(e.target);t.showSelector(n)}),window.addEventListener(\"load\",function(t){\"complete\"===e.readyState&&p()}),e.addEventListener(\"keydown\",function(e){var n=window.event?event:e;if(t.log(\"Keydown :: \"+n.keyCode),n.ctrlKey&&n.altKey)switch(n.keyCode){case 82:l();break;case 83:a();break;case 80:i()}}),p(),{getHits:c,play:function(o){t.log(o),(n=JSON.parse(o)).forEach(function(t){switch(t.action){case\"click\":e.querySelector(t.selector).click();break;case\"input\":e.querySelector(t.selector).value=t.value}})},record:l,stop:a}}(window.document,engineHelper);function engine_play(e){engineHelper.log(e),engine.play(e)}";
            browser.ObjectForScripting = new ScriptManager(this);
        }

        private void btnGo_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(this.urltxt.Text)) return;
                this.browser.Url = new Uri(this.urltxt.Text);
            }
            catch (Exception ex)
            {
                this.browser.Url = new Uri(string.Format("https://{0}", this.urltxt.Text));

            }
        }

        private void browser_Navigating(object sender, WebBrowserNavigatingEventArgs e)
        {
            this.urltxt.Text = e.Url.AbsoluteUri;
        }

        private void browser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            var doc = this.browser.Document;
            var dd = doc.GetElementsByTagName("button");
            // dd[0].Style="color:#000;";
            HtmlElement elem = browser.Document.CreateElement("script");
            elem.InnerText += txtScript.Text;
            //"Element.prototype.matches||(Element.prototype.matches=Element.prototype.msMatchesSelector),Element.prototype.closest||(Element.prototype.closest=function(e){for(var t=this;t;){if(t.matches(e))return t;t=t.parentElement}});var dd=function(e){var t=[],n=function(e){var n=i(e.target);t.push({action:\"click\",selector:n}),console.log(\"recordActions\",t)},o=function(e){var n=i(e.target),o=t.findIndex(function(e){\"input\"==e.action&&e.selector});-1==o?t.push({action:\"input\",selector:n,value:e.target.value}):t[o].value=e.target.value},r=function(){e.addEventListener(\"click\",n),e.addEventListener(\"keypress\",o)},l=function(){e.removeEventListener(\"click\",n),e.removeEventListener(\"keypress\",o),console.log(\"Recorded\",a())},a=function(){return t},c=function(){t.forEach(function(t){switch(t.action){case\"click\":e.querySelector(t.selector).click();break;case\"input\":e.querySelector(t.selector).value=t.value}})},i=function(e){if(\"HTML\"==e.tagName|\"Body\")return e.tagName;var t=[];t.push(e.tagName),t=t.concat(s(e.attributes));var n=u(t);return\"string\"==typeof n?n:i(e.parentElement)+\" \"+n[0]},s=function(e){var t=[];for(let n=0;n<e.length;n++){const o=e[n];\"id\"!=o.name?\"class\"!=o.name?o.value?t.push(\"[\"+o.name+\"='\"+o.value+\"']\"):t.push(\"[\"+o.name+\"]\"):t.push(\".\"+o.value.split(\" \").join(\".\")):t.push(\"#\"+o.value)}return t},u=function(t){for(let n=0;n<t.length;n++){const o=t[n];if(1==e.querySelectorAll(o).length)return o}var n=t.join(\"\");if(1==e.querySelectorAll(n).length)return n;{const t=[];for(let o=0;o<e.querySelectorAll(n).length;o++){e.querySelectorAll(n).length[o];t.push(n)}return t}},d=function(){let t=e.createElement(\"div\"),n=e.createElement(\"style\");t.innerHTML=\"<div id='test-auto-too'>\n        <label>[CTRL + ALT + R] = Record</label>\n        <br/>\n        <label> [CTRL + ALT + S] = Stop </label>\n        <br/>\n        <label> [CTRL + ALT + P] = Play </label>\n    </div>\",n.innerHTML=\"#test-auto-too {display: block;position: fixed;opacity: 0.5;bottom: 20px;right: 20px;padding: 20px;} #test-auto-too:hover {opacity: 1;border: 1px solid blue;}\",e.body.appendChild(t),e.body.appendChild(n)};return e.addEventListener(\"mouseover\",function(e){i(e.target)}),window.addEventListener(\"load\",function(t){\"complete\"===e.readyState&&d()}),e.addEventListener(\"keydown\",function(e){var t=window.event?event:e;if(console.log(\"Keydown\",t.keyCode),t.ctrlKey&&t.altKey)switch(t.keyCode){case 82:r();break;case 83:l();break;case 80:c()}}),d(),{getHits:a,play:c,record:r,stop:l}}(window.document);";
            //"function addXMLRequestCallback(e){var t,s;XMLHttpRequest.callbacks?XMLHttpRequest.callbacks.push(e):(XMLHttpRequest.callbacks=[e],t=XMLHttpRequest.prototype.send,XMLHttpRequest.prototype.send=function(e){for(s=0;s<XMLHttpRequest.callbacks.length;s++)XMLHttpRequest.callbacks[s](this);t.apply(this,arguments)})}addXMLRequestCallback(function(e){console.log(e.responseText),alert(e.responseText)}),addXMLRequestCallback(function(e){console.dir(e),alert(e.responseText)}),addXMLRequestCallback(function(e){console.dir(e),alert(e.responseText)});";


            doc.Body.AppendChild(elem);
            doc.MouseOver += HtmlElementEventHandler_One;
            doc.MouseDown += HtmlElementEventHandler_One;
        }
        void HtmlElementEventHandler_One(object sender, HtmlElementEventArgs e)
        {
            //e.ToElement.Style = "border:2px solid black;";
        }

        private void browser_FileDownload(object sender, EventArgs e)
        {

        }


        [ComVisible(true)]
        public class ScriptManager
        {
            BowserForm _form;
            List<BrowserAction> _steps = new List<BrowserAction>();
            string oneStep = string.Empty;
            public ScriptManager(BowserForm form)
            {
                _form = form;
            }
            public void ShowMessage(object obj)
            {
                MessageBox.Show(obj.ToString());
            }


            public void PushRecordSteps(object step)
            {
                try
                {
                    var tempStatus = JsonConvert.DeserializeObject<BrowserAction>(oneStep);
                    _steps.Add(tempStatus);
                }
                catch (Exception ex)
                {

                    throw;
                }
            }
            public void PushAllRecordSteps(object step)
            {
                oneStep = step.ToString();
                _steps = JsonConvert.DeserializeObject<List<BrowserAction>>(oneStep);
                //_steps.AddRange();
            }

            public object Play()
            {
                oneStep = JsonConvert.SerializeObject(_steps);
                Object[] objArray = new Object[1];
                objArray[0] = (Object)oneStep;

                //object[] codeString = { string.Format("engine.play('{0}');", oneStep) };
                //_form.browser.Document.InvokeScript("eval", codeString);
                //_form.browser.Document.InvokeScript("engine.play", objArray);
                _form.browser.Document.InvokeScript("engine_play", objArray);
                return objArray;
            }
            public void Log(object ss)
            {
                _form.textConsole.Text += Environment.NewLine + ss.ToString();
                while (_form.textConsole.Lines.Length > 100)
                {
                    _form.textConsole.SelectionStart = 0;
                    _form.textConsole.SelectionLength = _form.textConsole.Text.IndexOf("\n", 0) + 1;
                    _form.textConsole.SelectedText = "";
                }
                _form.textConsole.Select(_form.textConsole.Text.Length - 1, 0);
                _form.textConsole.ScrollToCaret();
                Console.WriteLine(ss.ToString());
            }
            public void ElementSelector(object ss)
            {
                _form.statusIdentityLabel.Text = ss.ToString();
                Console.WriteLine(ss.ToString());
            }
            
        }

        public class BrowserAction
        {
            public string action { get; set; }
            public string selector { get; set; }
            public string value { get; set; }
        }
    }

}
