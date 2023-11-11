using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using ZXing;

namespace MediSoftTech_HIS
{
    public static class UtilityClass
    {
        public static string decoding(string toEncode)
        {
            string base64Decoded;
            byte[] data = System.Convert.FromBase64String(toEncode);
            base64Decoded = System.Text.ASCIIEncoding.ASCII.GetString(data);
            return base64Decoded;
        }
    }
    public class ZxIngQRCode
    {
        public static string GenerateMyQCCode(string QCText)
        {
            string base64String = string.Empty;
            var QCwriter = new BarcodeWriter();
            QCwriter.Format = BarcodeFormat.QR_CODE;
            var result = QCwriter.Write(QCText);
            var barcodeBitmap = new Bitmap(result, new Size(100,100));
            using (MemoryStream ms = new MemoryStream())
            {
                barcodeBitmap.Save(ms, ImageFormat.Jpeg);
                Regex regex = new Regex(@"^[\w/\:.-]+;base64,");
                base64String = Convert.ToBase64String(ms.ToArray());
                base64String = regex.Replace(base64String, string.Empty);
            }
            return base64String;
        }
    }

}