using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace TatooWeb.Utils
{
    public class ImageUtils
    {
        private List<string> acceptedImageType = new List<string>()
        {
            "image/jpeg",
            "image/pjpeg",
            "image/png",
        };

        private Image originalImg;

        /// <summary>
        /// Save the posted image to the specify directory.
        /// </summary>
        /// <param name="image">the image file to save</param>
        /// <param name="saveLocation">the path of the location to save the image</param>
        /// <param name="fileName">the file name (if left empty, the file name will be generated)</param>
        /// <returns>saved file name</returns>
        /// <exception cref="NotSupportedException"></exception>
        /// <exception cref="DirectoryNotFoundException"></exception>
        public void SaveImage(HttpPostedFileBase image, string saveLocation, string fileName)
        {
            var fullPath = Path.Combine(saveLocation, fileName);

            if(File.Exists(fullPath)) throw new IOException(string.Format("The file {0} already exist.", fileName));

            if (CheckIfSupportedType(image))
            {
                image.SaveAs(fullPath);
                return;
            }
            throw new NotSupportedException("Only support for: jpg, jpeg and png image");
        }

        /// <summary>
        /// Save the posted image to the specify directory and create a thumbnail for the image. 
        /// </summary>
        /// <param name="image">the image file to save</param>
        /// <param name="saveLocation">the path of the location to save the image</param>
        /// <param name="fileName">the file name (if left empty, the file name will be generated)</param>
        /// <param name="thumbWidth">width of the thumbnail (if it is bigger than the image width, it will be the image width)</param>
        /// <param name="thumbHeight">height of the thubnail (if bigger than the image height, it will be the image height)</param>
        /// <returns>saved file name</returns>
        /// <exception cref="NotSupportedException"></exception>
        /// <exception cref="DirectoryNotFoundException"></exception>
        public void SaveImageWithThumbnail(HttpPostedFileBase image, string saveLocation, string fileName, int thumbWidth, int thumbHeight)
        {
            ResizeAndCropImage(image, saveLocation, string.Format("thumb-{0}", fileName), thumbWidth, thumbHeight); 
            SaveImage(image, saveLocation, fileName);             
        }

        /// <summary>
        /// Resize the image with the specify width and height while maintain the aspect ratio. If the specify with and height does not
        /// have the same ratio as the original image if will be crop to fixed the size.
        /// </summary>
        /// <param name="image">the image to be resized</param>
        /// <param name="saveLocation">the path of the location to save the image</param>
        /// <param name="fileName">the image file name</param>
        /// <param name="width">the width of the image to resize</param>
        /// <param name="height">the height of the image to resize</param>
        /// <exception cref="IOException"></exception>
        /// <exception cref="NotSupportedException"></exception>
        public void ResizeAndCropImage(HttpPostedFileBase image, string saveLocation, string fileName, int width, int height){
            var fullPath = Path.Combine(saveLocation, fileName);

            if(File.Exists(fullPath)) throw new IOException(string.Format("The file {0} already exist.", fileName));

            var isSupported = CheckIfSupportedType(image);

            if (!isSupported) throw new NotSupportedException("Only support for: jpg, jpeg and png image");
            try
            {
                originalImg = Image.FromStream(image.InputStream, true, true);
                //var pixelFormat = originalImg.PixelFormat;
                var originalWidth = originalImg.Width;
                var originalHeight = originalImg.Height;

                if (width == originalWidth && height == originalHeight)
                {
                    image.SaveAs(fullPath);
                    originalImg.Dispose();
                    return;
                }

                var ratioX = (float)width / originalWidth;
                var ratioY = (float)height / originalHeight;
                var ratio = Math.Max(ratioX, ratioY);

                var newWidth = (int)(originalWidth * ratio);
                newWidth = newWidth < width ? width : newWidth;
                var newHeight = (int)(originalHeight * ratio);
                newHeight = newHeight < height ? height : newHeight;

                using(var newImage = new Bitmap(newWidth, newHeight, PixelFormat.Format32bppArgb))
                {
                    using (var graphic = Graphics.FromImage(newImage))
                    {
                        graphic.CompositingQuality = CompositingQuality.HighQuality;
                        graphic.InterpolationMode = InterpolationMode.HighQualityBicubic;
                        graphic.SmoothingMode = SmoothingMode.HighQuality;
                        graphic.DrawImage(originalImg, 0, 0, newWidth, newHeight);
                    }
                    ImageCodecInfo imageCodecInfo = GetImageCodecInfo(image.ContentType);
                    Encoder encoder = Encoder.Quality;
                    EncoderParameters encoderParams = new EncoderParameters(1);
                    long quality = 100;
                    EncoderParameter encoderParam = new EncoderParameter(encoder, quality);
                    encoderParams.Param[0] = encoderParam;
                    if(newWidth > width || newHeight > height)
                    {
                        Rectangle cropArea = new Rectangle(0, 0, width, height);
                        var cropedImage = newImage.Clone(cropArea, PixelFormat.Format32bppArgb);
                        
                        cropedImage.Save(fullPath, imageCodecInfo, encoderParams);
                        cropedImage.Dispose();    
                    } 
                    else
                    {
                        newImage.Save(fullPath, imageCodecInfo, encoderParams);
                    }                  
                 
                    DisposeImage();
                }                    
            }
            catch(Exception ex)
            {
                DisposeImage();
                throw ex;
            }            
        }

        public void DeleteImage(string fileName, string fileLocation)
        {
            var path = Path.Combine(fileLocation, fileName);
            if (File.Exists(path)) File.Delete(path);
        }

        private bool CheckIfSupportedType(HttpPostedFileBase image)
        {
            var accepted = false;
            foreach (var type in acceptedImageType)
            {
                accepted = type == image.ContentType;
                if (accepted) break;
            }
            return accepted;
        }

        private ImageCodecInfo GetImageCodecInfo(string mimeType)
        {
            int j;
            ImageCodecInfo[] encoders;
            encoders = ImageCodecInfo.GetImageEncoders();
            for (j = 0; j < encoders.Length; ++j)
            {
                if (encoders[j].MimeType == mimeType)
                    return encoders[j];
            }
            return null;
        }

        private void DisposeImage(){
            if(originalImg != null) originalImg.Dispose();
        }
    }
}