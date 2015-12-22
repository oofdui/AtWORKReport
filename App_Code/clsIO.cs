using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;

/// <summary>
/// Summary description for clsIO
/// </summary>
public class clsIO
{
	public clsIO()
	{
		//
		// TODO: Add constructor logic here
		//
	}
	
	public int ImageWidth(string shortPath)
    {
        #region Remark
        /*############################ Example ############################
        หาความกว้างของรูปภาพ โดยระบุพาร์ธรูป
        clsIO.ImageWidth("/Images/animate_loading.gif").ToString()
        #################################################################*/
        #endregion

        int imgWidth = 0;

        if (FileTypeChecker(shortPath) == "IMG")
        {
            System.Drawing.Image myImage = System.Drawing.Image.FromFile(System.Web.HttpContext.Current.Server.MapPath(shortPath));

            if (myImage != null)
            {
                imgWidth = myImage.Width;
                myImage.Dispose();
            }
        }

        return imgWidth;
    }

    public int ImageWidth(FileUpload fuName)
    {
        #region Remark
        /*############################ Example ############################
        ใช้หาขนาดความกว้างของรูป จาก FileUpload Control
        clsIO.ImageWidth(FileUploadControl).ToString()
        
        TIP : สามารถเรียกใช้ได้ครั้งละ 1 Function คือ ImageWidth หรือ ImageHeight เท่านั้น เพราะค่าใน FileUpload จะหายไป
        #################################################################*/
        #endregion

        int imgWidth = 0;

        if (fuName.HasFile)
        {
            if (FileTypeChecker(fuName.FileName) == "IMG")
            {
                Stream myStream = fuName.PostedFile.InputStream;
                System.Drawing.Image myImage = System.Drawing.Image.FromStream(myStream);

                if (myImage != null)
                {
                    imgWidth = myImage.Width;
                    myImage.Dispose();
                    myStream.Dispose();
                }
            }
        }

        return imgWidth;
    }

    public int ImageHeight(string shortPath)
    {
        #region Remark
        /*############################ Example ############################
        ใช้หาขนาดความสูงของรูป จาก Path
        clsIO.ImageHeight("Images/animate_loading.gif").ToString()
        #################################################################*/
        #endregion

        int imgHeight = 0;

        if (FileTypeChecker(shortPath) == "IMG")
        {
            System.Drawing.Image myImage = System.Drawing.Image.FromFile(System.Web.HttpContext.Current.Server.MapPath(shortPath));

            if (myImage != null)
            {
                imgHeight = myImage.Height;
                myImage.Dispose();
            }
        }

        return imgHeight;
    }

    public int ImageHeight(FileUpload fuName)
    {
        #region Remark
        /*############################ Example ############################
        ใช้หาขนาดความสูงของรูป จาก FileUpload Control
        clsIO.ImageHeight(FileUploadControl).ToString()
        #################################################################*/
        #endregion

        int imgHeight = 0;

        if (fuName.HasFile)
        {
            if (FileTypeChecker(fuName.FileName) == "IMG")
            {
                Stream myStream = fuName.PostedFile.InputStream;
                System.Drawing.Image myImage = System.Drawing.Image.FromStream(myStream);

                if (myImage != null)
                {
                    imgHeight = myImage.Height;
                    myImage.Dispose();
                    myStream.Dispose();
                }
            }
        }

        return imgHeight;
    }
	
	public void ImageSize(string shortPath,out int outWidth,out int outHeight)
    {
        #region Remark
        /*############################ Example ############################
        หาความกว้าง ยาวของรูป จาก Path ที่กำหนด
        clsIO.ImageHeight("Images/animate_loading.gif",out width,out height)
        #################################################################*/
        #endregion

        int imgWidth = 0;
        int imgHeight = 0;

        if (FileTypeChecker(shortPath) == "IMG")
        {
            System.Drawing.Image myImage = System.Drawing.Image.FromFile(System.Web.HttpContext.Current.Server.MapPath(shortPath));

            if (myImage != null)
            {
                imgWidth=myImage.Width;
                imgHeight = myImage.Height;
                myImage.Dispose();
            }
        }

        outWidth=imgWidth;
        outHeight = imgHeight;
    }

    public void ImageSize(FileUpload fuName,out int outWidth,out int outHeight)
    {
        #region Remark
        /*############################ Example ############################
        หาความกว้าง ยาวของรูป จาก FileUpload Control ที่กำหนด
        clsIO.ImageHeight(FileUploadControl,out width,out height)
        #################################################################*/
        #endregion

        int imgWidth = 0;
        int imgHeight = 0;

        if (fuName.HasFile)
        {
            if (FileTypeChecker(fuName.FileName) == "IMG")
            {
                Stream myStream = fuName.PostedFile.InputStream;
                System.Drawing.Image myImage = System.Drawing.Image.FromStream(myStream);

                if (myImage != null)
                {
                    imgWidth=myImage.Width;
                    imgHeight = myImage.Height;
                    myImage.Dispose();
                    myStream.Dispose();
                }
            }
        }

        outWidth = imgWidth;
        outHeight = imgHeight;
    }

    public bool ImageResize(int intWidth, int intHeight, string pathInput, string pathOutput, string strWatermark, int fontSize)
    {
        #region Remark
        /*############################ Example ############################
        ย่อภาพ และ ใส่ Watermark
        clsIO.ImageResize(500,0,"/Images/Img.jpg","/Images/","GooDesign",10);
        #################################################################*/
        #endregion

        if (FileTypeChecker(Path.GetFileName(pathInput)) != "IMG")
        {
            return false;
        }
        int fontSizeDefault = 10;
        FileStream fs = new FileStream(System.Web.HttpContext.Current.Server.MapPath(pathInput), FileMode.Open);
        System.Drawing.Image myImage = System.Drawing.Image.FromStream(fs);
        fs.Close();
        int newWidth;
        int newHeight;
        StringFormat strFormat = new StringFormat();
        strFormat.Alignment = StringAlignment.Far;
        strFormat.LineAlignment = StringAlignment.Far;

        if (intWidth == 0 && intHeight == 0)
        {   //ไม่กำหนดขนาดรูป
            newWidth = myImage.Width;
            newHeight = myImage.Height;
            if (!string.IsNullOrEmpty(strWatermark))
            {
                if (Path.GetExtension(pathInput).ToLower() == ".gif")
                {
                    return true;
                }
                Bitmap thumbnailBitmap = new Bitmap(newWidth, newHeight);
                Graphics thumbnailGraphic = Graphics.FromImage(myImage);
                Rectangle imageRectangle = new Rectangle(0, 0, newWidth, newHeight);

                thumbnailGraphic.DrawString(strWatermark, new Font("Tahoma", (fontSize > 0 ? fontSize : fontSizeDefault)), new SolidBrush(Color.FromArgb(212, 212, 212)), imageRectangle, strFormat);

                if (!string.IsNullOrEmpty(pathOutput))
                {
                    thumbnailBitmap.Save(System.Web.HttpContext.Current.Server.MapPath(pathOutput), myImage.RawFormat);
                }
                else
                {
                    thumbnailBitmap.Save(System.Web.HttpContext.Current.Server.MapPath(pathInput), myImage.RawFormat);
                }

                thumbnailGraphic.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                thumbnailGraphic.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                thumbnailGraphic.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;

                thumbnailGraphic.Dispose();
                thumbnailBitmap.Dispose();
                myImage.Dispose();
                return true;
            }
            else
            {
                return true;
            }
        }
        else if (intWidth == 0 && intHeight > 0)
        {
            // nW = nH * (W/H)
            if (myImage.Height > intHeight)
            {
                newWidth = (int)((double)intHeight * ((double)myImage.Width / (double)myImage.Height));
                newHeight = intHeight;
            }
            else
            {
                if (Path.GetExtension(pathInput).ToLower() == ".gif")
                {
                    return true;
                }
                newWidth = myImage.Width;
                newHeight = myImage.Height;
            }
            Bitmap thumbnailBitmap = new Bitmap(newWidth, newHeight);
            Graphics thumbnailGraphic = Graphics.FromImage(thumbnailBitmap);
            thumbnailGraphic.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            thumbnailGraphic.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            thumbnailGraphic.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            Rectangle imageRectangle = new Rectangle(0, 0, newWidth, newHeight);
            thumbnailGraphic.DrawImage(myImage, imageRectangle);

            //####### Watermark ########
            if (!string.IsNullOrEmpty(strWatermark))
            {
                thumbnailGraphic.DrawString(strWatermark, new Font("Tahoma", (fontSize > 0 ? fontSize : fontSizeDefault)), new SolidBrush(Color.FromArgb(212, 212, 212)), imageRectangle, strFormat);
            }

            ImageFormat objRawFormat = myImage.RawFormat;

            //####### Input / Output Path ########
            if (!string.IsNullOrEmpty(pathOutput))
            {
                thumbnailBitmap.Save(System.Web.HttpContext.Current.Server.MapPath(pathOutput), objRawFormat);
            }
            else
            {
                thumbnailBitmap.Save(System.Web.HttpContext.Current.Server.MapPath(pathInput), objRawFormat);
            }

            thumbnailGraphic.Dispose();
            thumbnailBitmap.Dispose();
            myImage.Dispose();
            return true;
        }
        else if (intWidth > 0 && intHeight == 0)
        {
            // nH = nW * (H/W)
            if (myImage.Width > intWidth)
            {
                newWidth = intWidth;
                newHeight = (int)((double)intWidth * ((double)myImage.Height / (double)myImage.Width));
            }
            else
            {
                if (Path.GetExtension(pathInput).ToLower() == ".gif")
                {
                    return true;
                }
                newWidth = myImage.Width;
                newHeight = myImage.Height;
            }
            Bitmap thumbnailBitmap = new Bitmap(newWidth, newHeight);
            Graphics thumbnailGraphic = Graphics.FromImage(thumbnailBitmap);
            thumbnailGraphic.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            thumbnailGraphic.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            thumbnailGraphic.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            Rectangle imageRectangle = new Rectangle(0, 0, newWidth, newHeight);
            thumbnailGraphic.DrawImage(myImage, imageRectangle);

            //####### Watermark ########
            if (!string.IsNullOrEmpty(strWatermark))
            {
                thumbnailGraphic.DrawString(strWatermark, new Font("Tahoma", (fontSize > 0 ? fontSize : fontSizeDefault)), new SolidBrush(Color.FromArgb(212, 212, 212)), imageRectangle, strFormat);
            }

            ImageFormat objRawFormat = myImage.RawFormat;

            //####### Input / Output Path ########
            if (!string.IsNullOrEmpty(pathOutput))
            {
                thumbnailBitmap.Save(System.Web.HttpContext.Current.Server.MapPath(pathOutput), objRawFormat);
            }
            else
            {
                thumbnailBitmap.Save(System.Web.HttpContext.Current.Server.MapPath(pathInput), objRawFormat);
            }

            thumbnailGraphic.Dispose();
            thumbnailBitmap.Dispose();
            myImage.Dispose();
            return true;
        }
        else if (intWidth > 0 && intHeight > 0)
        {
            //
            if (myImage.Width > intWidth)
            {
                newWidth = intWidth;
                newHeight = (int)((double)intWidth * ((double)myImage.Height / (double)myImage.Width));
                if (newHeight > intHeight)
                {
                    newWidth = (int)((double)intHeight * ((double)myImage.Width / (double)myImage.Height));
                    newHeight = intHeight;
                }
            }
            else if (myImage.Height > intHeight)
            {
                newWidth = (int)((double)intHeight * ((double)myImage.Width / (double)myImage.Height));
                newHeight = intHeight;
                if (newWidth > intWidth)
                {
                    newWidth = intWidth;
                    newHeight = (int)((double)intWidth * ((double)myImage.Height / (double)myImage.Width));
                }
            }
            else
            {
                if (Path.GetExtension(pathInput).ToLower() == ".gif")
                {
                    return true;
                }
                newWidth = myImage.Width;
                newHeight = myImage.Height;
            }
            Bitmap thumbnailBitmap = new Bitmap(newWidth, newHeight);
            Graphics thumbnailGraphic = Graphics.FromImage(thumbnailBitmap);
            thumbnailGraphic.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            thumbnailGraphic.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            thumbnailGraphic.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            Rectangle imageRectangle = new Rectangle(0, 0, newWidth, newHeight);
            thumbnailGraphic.DrawImage(myImage, imageRectangle);

            //####### Watermark ########
            if (!string.IsNullOrEmpty(strWatermark))
            {
                thumbnailGraphic.DrawString(strWatermark, new Font("Tahoma", (fontSize > 0 ? fontSize : fontSizeDefault)), new SolidBrush(Color.FromArgb(212, 212, 212)), imageRectangle, strFormat);
            }

            ImageFormat objRawFormat = myImage.RawFormat;

            //####### Input / Output Path ########
            if (!string.IsNullOrEmpty(pathOutput))
            {
                thumbnailBitmap.Save(System.Web.HttpContext.Current.Server.MapPath(pathOutput), objRawFormat);
            }
            else
            {
                thumbnailBitmap.Save(System.Web.HttpContext.Current.Server.MapPath(pathInput), objRawFormat);
            }

            thumbnailGraphic.Dispose();
            thumbnailBitmap.Dispose();
            myImage.Dispose();
            return true;
        }
        else
        {
            return false;
        }
    }

    public string FileTypeChecker(string filename)
    {
        #region Remark
        /*############################ Example ############################
        ตรวจสอบประเภทไฟล์จากชื่อที่ส่งเข้ามา
        clsIO.FileTypeChecker("Test.jpeg");
        #################################################################*/
        #endregion

        string strExtension = Path.GetExtension(filename).ToLower();

        if (strExtension == ".jpg" ||
            strExtension == ".jpeg" ||
            strExtension == ".png" ||
            strExtension == ".gif" ||
            strExtension == ".bmp")
        {
            return "IMG";
        }
        else if (strExtension == ".doc" ||
            strExtension == ".docx" ||
            strExtension == ".xls" ||
            strExtension == ".xlsx" ||
            strExtension == ".ppt" ||
            strExtension == ".pptx" ||
            strExtension == ".pdf")
        {
            return "DOC";
        }
        else
        {
            return "UNKNOWN";
        }
    }

    public bool FolderExist(string pathShort, bool delete)
    {
        #region Remark
        /*############################ Example ############################
        เช็คว่าโฟล์เดอร์ที่ส่งชื่อเข้ามามีอยู่จริงไหม และ มีคำสั่งให้ลบโฟล์เดอร์กรณีมีไฟล์แล้ว
        clsIO.FolderExist("/Upload/Photo",true);
        #################################################################*/
        #endregion

        DirectoryInfo di = new DirectoryInfo(System.Web.HttpContext.Current.Server.MapPath(pathShort));
        if (di.Exists == false)
        {
            return false;
        }
        else
        {
            if (delete)
            {
                di.Delete();
            }
            return true;
        }
    }

    public bool FileExist(string pathShort, bool delete)
    {
        #region Remark
        /*############################ Example ############################
        เช็คว่าไฟล์ที่ส่งชื่อเข้ามามีอยู่จริงไหม และ มีคำสั่งให้ลบไฟล์กรณีมีไฟล์แล้ว
        clsIO.FileExist("/Upload/Photo/Test.jpg",true);
        #################################################################*/
        #endregion

        FileInfo objInfo = new FileInfo(System.Web.HttpContext.Current.Server.MapPath(pathShort));
        if (objInfo.Exists == false)
        {
            return false;
        }
        else
        {
            if (delete == true)
            {
                objInfo.Delete();
            }
            return true;
        }
    }
	
	public bool UploadPhoto(FileUpload fuPhoto, string pathShort, string photoName, int maxSizeKB,int intWidth,int intHeight,string strWatermark,int fontSize,out string errorMessage,out string fileName)
    {
        #region Remark
        /*############################ Example ############################
        Upload ไฟล์ พร้อมลดขนาดรูป
        
        string outError;
        string outFilename;
        if (!clsIO.UploadPhoto(fuPhoto, "/Upload/PhotoBook/", "Book_" + "id", 512, 150, 0, "", 0, out outError, out outFilename))
        {
            lblSQL.Text = outError;
        }
        else
        {
            //ทำอะไรต่อ ถ้าอัพผ่าน
        }
        #################################################################*/
        #endregion

        bool rtnValue = true;
        errorMessage = "";
        fileName = "";

        if (fuPhoto.HasFile == true)
        {
            if (FileTypeChecker(fuPhoto.FileName) != "IMG")
            {
                errorMessage = "โปรดเลือกเฉพาะไฟล์รูปภาพ";
                fuPhoto.Focus();
                return false;
            }
            if (maxSizeKB > 0)
            {
                if (fuPhoto.PostedFile.ContentLength > maxSizeKB * 1000)
                {
                    errorMessage = "ขนาดไฟล์ใหญ่เกิน " + maxSizeKB + " KB";
                    fuPhoto.Focus();
                    return false;
                }
            }

            fileName = photoName + System.IO.Path.GetExtension(fuPhoto.FileName).ToLower();

            FileExist(pathShort + fileName, true);
            try
            {
                fuPhoto.SaveAs(System.Web.HttpContext.Current.Server.MapPath(pathShort + fileName));
            }
            catch (Exception ex)
            {
                errorMessage = "เกิดข้อผิดพลาด ขณะอัพโหลดไฟล์ไว้ที่ " + System.Web.HttpContext.Current.Server.MapPath(pathShort + fileName);
                fuPhoto.Focus();
                return false;
            }

            if (!ImageResize(intWidth, intHeight, pathShort + fileName, "", strWatermark, fontSize))
            {
                errorMessage = "เกิดข้อผิดพลาด ขณะย่อขนาดภาพ";
                fuPhoto.Focus();
                return false;
            }
        }
        else
        {
            errorMessage = "ไม่พบไฟล์ที่ต้องการอัพโหลด";
            fuPhoto.Focus();
            return false;
        }

        return rtnValue;
    }
}
