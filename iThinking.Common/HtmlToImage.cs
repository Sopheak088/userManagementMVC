using DocumentFormat.OpenXml.Wordprocessing;
using iThinking.Common.Exceptions;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace iThinking.Common
{
    public class HtmlToImage
    {
        private Bitmap m_Bitmap;
        private string m_HtmlContent;
        private string m_FileName = string.Empty;

        public HtmlToImage(string htmlContent)
        {
            // Without file
            m_HtmlContent = htmlContent;
        }

        public HtmlToImage(string htmlContent, string fileName)
        {
            // With file
            m_HtmlContent = htmlContent;
            m_FileName = fileName;
        }

        public Bitmap Generate()
        {
            // Thread
            var m_thread = new Thread(_Generate);
            m_thread.SetApartmentState(ApartmentState.STA);
            m_thread.Start();
            m_thread.Join();
            return m_Bitmap;
        }

        private void _Generate()
        {
            var browser = new WebBrowser { ScrollBarsEnabled = false };
            browser.DocumentText = m_HtmlContent;
            browser.DocumentCompleted += WebBrowser_DocumentCompleted;

            while (browser.ReadyState != WebBrowserReadyState.Complete)
            {
                Application.DoEvents();
            }

            browser.Dispose();
        }

        private void WebBrowser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            // Capture
            var browser = (WebBrowser)sender;
            browser.ClientSize = new Size(browser.Document.Body.ScrollRectangle.Width, browser.Document.Body.ScrollRectangle.Bottom);
            browser.ScrollBarsEnabled = false;
            m_Bitmap = new Bitmap(browser.Document.Body.ScrollRectangle.Width, browser.Document.Body.ScrollRectangle.Bottom);
            browser.BringToFront();
            browser.DrawToBitmap(m_Bitmap, browser.Bounds);

            // Save as file?
            if (m_FileName.Length > 0)
            {
                // Save
                m_Bitmap.SaveJPG100(m_FileName);
            }
        }
    }
}