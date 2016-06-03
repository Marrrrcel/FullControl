using System;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using nSpotify;
using System.Threading;

namespace SpotiBoti
{
    public static class RichTextBoxExtensions
    {
        public static void AppendText(this RichTextBox box, string text, Color? col = null)
        {
            Color? color = Color.Black;
            if (col != null)
            {
                color = col;
            }
            box.SelectionStart = box.TextLength;
            box.SelectionLength = 0;

            box.SelectionColor = (Color)color;
            box.AppendText(text);
            box.SelectionColor = box.ForeColor;
        }

        public static void AppendLine(this RichTextBox control, string text)
        {
            control.AppendText(text + "\r\n");
        }
    }
}