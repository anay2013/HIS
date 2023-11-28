using HiQPdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace MediSoftTech_HIS
{
    public class PDFUtility
    {
        public string ConvertPdfToImageTags(string pdfFile)
        {
            string ImageTagsList = "";
            if(!File.Exists(pdfFile))
            {
                ImageTagsList = "File Not Exists";
                return ImageTagsList;
            }
            // create the PDF rasterizer
            PdfRasterizer pdfRasterizer = new PdfRasterizer();
            pdfRasterizer.SerialNumber = "PXVUbG1Z-W3FUX09c-T0QMCBMN-HQwdDh0M-HQ4MEwwP-EwQEBAQ=";

            // set the output images color space
            pdfRasterizer.ColorSpace = RasterImageColorSpace.Rgb;
            // set the rasterization resolution in DPI
            pdfRasterizer.DPI = 150;
            int fromPdfPageNumber =1;
            int toPdfPageNumber = 0;// textBoxToPage.Text.Length > 0 ? int.Parse(textBoxToPage.Text) : 0;
            PdfPageRasterImage[] pageImages = pdfRasterizer.RasterizeToImageObjects(pdfFile, fromPdfPageNumber, toPdfPageNumber);
            try
            {
                if (pageImages.Length > 0)
                {
                    for (int i = 0; i < pageImages.Length; i++)
                    {
                        System.Drawing.Image imageObject = pageImages[i].ImageObject;
                        using (MemoryStream m = new MemoryStream())
                        {
                            imageObject.Save(m, imageObject.RawFormat);
                            Regex regex = new Regex(@"^[\w/\:.-]+;base64,");
                            string base64String = Convert.ToBase64String(m.ToArray());
                            base64String=regex.Replace(base64String, string.Empty);
                            string srcstr ="data:image/png;base64,"+ base64String + "";
                            ImageTagsList = ImageTagsList + "<img src='"+ srcstr + "' style='width:700px;'/>";
                        }
                    }
                }
            }
            catch (Exception ex) {
                ImageTagsList = "Some Error in rendering Pdf Attachment";
            }
            finally
            {
                // dispose the page images
                for (int i = 0; i < pageImages.Length; i++)
                    pageImages[i].Dispose();
            }
            return ImageTagsList;
        }
    }
}