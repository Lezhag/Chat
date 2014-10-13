using System;
using System.Drawing;
using System.Windows.Forms;
using PostOffice;

namespace Server
{

    public static class RichTextBoxExtensions
    {
        // this extension method is courtesy of Nathan Baulch (http://stackoverflow.com/questions/1926264/color-different-parts-of-a-richtextbox-string)
        public static void AppendText(this RichTextBox box, string text, Color color)
        {
            box.SelectionStart = box.TextLength;
            box.SelectionLength = 0;

            box.SelectionColor = color;
            box.AppendText(text);
            box.SelectionColor = box.ForeColor;
        }

        public static void AppendText(this RichTextBox box, Parcel p)
        {
            box.Font = new Font("Calibri", 12);
            box.AppendText("(" + p.TimeStamp.ToShortTimeString() + ") ", Color.Black);
            box.AppendText(p.UserName, p.Colour);
            box.AppendText(" : " + p.Msg, Color.Black);
            box.AppendText(Environment.NewLine);
        }
    }
}
