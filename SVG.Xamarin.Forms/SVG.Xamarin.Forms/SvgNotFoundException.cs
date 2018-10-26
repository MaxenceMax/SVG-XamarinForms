using System;
namespace SVG.Xamarin.Forms
{
    public class SvgNotFoundException : Exception
    {
        public SvgNotFoundException(string file):base(String.Format("Invalid SVG File Name : {0}",file))
        {
        }
    }
}
