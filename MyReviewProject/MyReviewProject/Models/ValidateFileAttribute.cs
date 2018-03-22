using System;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Drawing.Imaging;
using System.Web;

namespace MyReviewProject.Models
{
    public class ValidateFileAttribute : RequiredAttribute
    {
        public override bool IsValid(object value)
        {
            try
            {
                var file = value as HttpPostedFileBase;
                if (file == null)
                {
                    return true;
                }

                if (file.ContentLength > 5 * 1024 * 1024)
                {
                    return false;
                }

                try
                {
                    using (var img = Image.FromStream(file.InputStream))
                    {
                        if (img.RawFormat.Equals(ImageFormat.Png) || img.RawFormat.Equals(ImageFormat.Jpeg) || img.RawFormat.Equals(ImageFormat.MemoryBmp))
                            return true;
                    }
                }
                catch { }
                return false;
            }
            catch
            {
                return false;
            }
        }
    }
}