using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;

namespace K.Y.DLL.Tool
{
    public class T_Image
    {
        /// <summary>
        /// 等比缩放
        /// </summary>
        /// <param name="OriginalFilePath">源文件地址</param>
        /// <param name="ThumbnailFilePath">目标文件地址-不指定则在同源目录下</param>
        /// <param name="Size">缩放至 正方形</param>
        /// <param name="Mode">缩放方式</param>
        /// <param name="IsOverWrite">是否重写</param>
        /// <returns></returns>
        public static String Zoom(String OriginalFilePath, String ThumbnailFilePath = default(String), Int32 Size = 64, ZoomMode Mode = ZoomMode.FitLongSideIn, Boolean IsOverWrite = true)
        {
            var FileInfo = new FileInfo(OriginalFilePath);
            if (!FileInfo.Exists) return "";
            if (String.IsNullOrWhiteSpace(ThumbnailFilePath)) ThumbnailFilePath = FileInfo.DirectoryName + "\\" + FileInfo.Name.Ex_ToList('.').FirstOrDefault() + "_" + Size + "_" + Size + "_" + ((Int32)Mode) + FileInfo.Extension;

            if (File.Exists(ThumbnailFilePath))
            {
                if (!IsOverWrite)
                    return ThumbnailFilePath;
                else
                    File.Delete(ThumbnailFilePath);
            }

            System.Drawing.Image SourceImage = System.Drawing.Image.FromFile(OriginalFilePath);

            if (SourceImage.Width <= Size && SourceImage.Height <= Size)
            {
                File.Copy(OriginalFilePath, ThumbnailFilePath);
                return ThumbnailFilePath;
            }


            var Multiple = 1.0f;
            switch (Mode)//更加缩放方式决定缩放比例
            {
                case ZoomMode.FitLongSideIn: Multiple = (Single)Math.Max(SourceImage.Width, SourceImage.Height) / Size; break;
                case ZoomMode.FitShortSideOut: Multiple = (Single)Math.Min(SourceImage.Width, SourceImage.Height) / Size; break;
            }

            var ThumbWidth = (Int32)(SourceImage.Width / Multiple);//缩小咯~
            var ThumbHeight = (Int32)(SourceImage.Height / Multiple);//缩小咯~

            Bitmap bmp = new Bitmap(ThumbWidth, ThumbHeight);
            //从Bitmap创建一个System.Drawing.Graphics对象，用来绘制高质量的缩小图。

            System.Drawing.Graphics gr = System.Drawing.Graphics.FromImage(bmp);

            //设置 System.Drawing.Graphics对象的SmoothingMode属性为HighQuality

            gr.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

            //下面这个也设成高质量

            gr.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;

            //下面这个设成High

            gr.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;

            //把原始图像绘制成上面所设置宽高的缩小图

            System.Drawing.Rectangle rectDestination = new System.Drawing.Rectangle(0, 0, ThumbWidth, ThumbHeight);
            gr.DrawImage(SourceImage, rectDestination, 0, 0, SourceImage.Width, SourceImage.Height, GraphicsUnit.Pixel);

            //保存图像，大功告成！

            bmp.Save(ThumbnailFilePath);

            //最后别忘了释放资源
            bmp.Dispose();
            SourceImage.Dispose();
            return ThumbnailFilePath;
        }
        public enum ZoomMode
        {
            /// <summary>
            /// 短边不超过Size 缩放后图片长边大于Size 短边等于Size '日'效果
            /// </summary>
            FitShortSideOut = 1,
            /// <summary>
            /// 长边不超过Size 缩放后图片长边等于Size 短边小于Size '回'效果
            /// </summary>
            FitLongSideIn = 2,
        }
        /// <summary>
        /// 图片裁剪
        /// </summary>
        /// <param name="OriginalFilePath">源文件地址</param>
        /// <param name="ThumbnailFilePath">目标文件地址-不指定则在同源目录下</param>
        /// <param name="ToHeight">所需高度</param>
        /// <param name="ToWidth">所需宽度</param>
        /// <param name="Position">裁剪位置</param>
        /// <param name="IsOverWrite">是否覆盖重写</param>
        /// <returns></returns>
        public static String Cut(String OriginalFilePath, String ThumbnailFilePath = default(String), Int32 ToHeight = 64, Int32 ToWidth = 64, CutPosition Position = CutPosition.Middle, CutDirection Direction = CutDirection.Auto, Boolean IsOverWrite = true)
        {

            var FileInfo = new FileInfo(OriginalFilePath);
            if (!FileInfo.Exists) return "";
            if (String.IsNullOrWhiteSpace(ThumbnailFilePath)) ThumbnailFilePath = FileInfo.DirectoryName + "\\" + FileInfo.Name.Ex_ToList('.').FirstOrDefault() + "_" + ToWidth + "_" + ToHeight + "_" + Position + "_" + Direction + FileInfo.Extension;

            if (File.Exists(ThumbnailFilePath))
            {
                if (!IsOverWrite)
                    return ThumbnailFilePath;
                else
                    File.Delete(ThumbnailFilePath);
            }

            System.Drawing.Image SourceImage = System.Drawing.Image.FromFile(OriginalFilePath);

            if (SourceImage.Width < ToWidth || SourceImage.Height < ToHeight)
            {
                File.Copy(OriginalFilePath, ThumbnailFilePath);
                return ThumbnailFilePath;
            }

            var Multiple = 1.0f;// (Single)Math.Min(SourceImage.Width, SourceImage.Height) / Math.Max(ToHeight, ToWidth);//需要缩小的倍数          
            var ThumbWidth = SourceImage.Width;// (Int32)(SourceImage.Width / Multiple);//缩小咯~
            var ThumbHeight = SourceImage.Height;// (Int32)(SourceImage.Height / Multiple);//缩小咯~

            var StartX = 0;
            var StartY = 0;
            switch (Direction)
            {
                case CutDirection.Auto:
                    {
                        Multiple = (Single)Math.Min((Single)SourceImage.Height / ToHeight, (Single)SourceImage.Width / ToWidth);//需要缩小的倍数          
                        ThumbWidth = (Int32)(SourceImage.Width / Multiple);//缩小咯~
                        ThumbHeight = (Int32)(SourceImage.Height / Multiple);//缩小咯~
                        if (ThumbHeight > ThumbWidth)//高度大于宽度 竖图 计算Y即可
                        {
                            switch (Position)
                            {
                                case CutPosition.Header: StartY = 0; break;//上部
                                case CutPosition.Middle: StartY = (Int32)((Single)(ThumbHeight - ToHeight) / 2 * Multiple); break;
                                case CutPosition.Footer: StartY = (Int32)((Single)(ThumbHeight - ToHeight) * Multiple); break;
                            }
                        }
                        else//高度小于宽度 横图 计算X即可
                            switch (Position)
                            {
                                case CutPosition.Header: StartX = 0; break;//上部
                                case CutPosition.Middle: StartX = (Int32)((Single)(ThumbWidth - ToWidth) / 2 * Multiple); break;
                                case CutPosition.Footer: StartX = (Int32)((Single)(ThumbWidth - ToWidth) * Multiple); break;
                            }
                    }; break;
                case CutDirection.X:
                    {
                        Multiple = (Single)SourceImage.Height / ToHeight;//需要缩小的倍数          
                        ThumbWidth = (Int32)(SourceImage.Width / Multiple);//缩小咯~
                        ThumbHeight = ToHeight;//缩小咯~
                        ToWidth = Math.Min(ToWidth, ThumbWidth);
                        if (ThumbWidth > ToWidth)
                            switch (Position)
                            {
                                case CutPosition.Header: StartX = 0; break;//上部
                                case CutPosition.Middle: StartX = (Int32)((Single)(ThumbWidth - ToWidth) / 2 * Multiple); break;
                                case CutPosition.Footer: StartX = (Int32)((Single)(ThumbWidth - ToWidth) * Multiple); break;
                            }
                    }; break;
                case CutDirection.Y:
                    {
                        Multiple = (Single)SourceImage.Width / ToWidth;//需要缩小的倍数          
                        ThumbWidth = ToWidth;//缩小咯~
                        ThumbHeight = (Int32)(SourceImage.Height / Multiple);//缩小咯~
                        ToHeight = Math.Min(ToHeight, ThumbHeight);
                        if (ThumbHeight > ToHeight)
                            switch (Position)
                            {
                                case CutPosition.Header: StartY = 0; break;//上部
                                case CutPosition.Middle: StartY = (Int32)((Single)(ThumbHeight - ToHeight) / 2 * Multiple); break;
                                case CutPosition.Footer: StartY = (Int32)((Single)(ThumbHeight - ToHeight) * Multiple); break;
                            }
                    }; break;
            }
            if (ThumbHeight > ThumbWidth)//高度大于宽度 竖图 计算Y即可
                switch (Position)
                {
                    case CutPosition.Header: StartY = 0; break;//上部
                    case CutPosition.Middle: StartY = (Int32)((Single)(ThumbHeight - ToHeight) / 2 * Multiple); break;
                    case CutPosition.Footer: StartY = (Int32)((Single)(ThumbHeight - ToHeight) * Multiple); break;
                }
            else//高度小于宽度 横图 计算X即可
                switch (Position)
                {
                    case CutPosition.Header: StartX = 0; break;//上部
                    case CutPosition.Middle: StartX = (Int32)((Single)(ThumbWidth - ToWidth) / 2 * Multiple); break;
                    case CutPosition.Footer: StartX = (Int32)((Single)(ThumbWidth - ToWidth) * Multiple); break;
                }

            Bitmap bmp = new Bitmap(ToWidth, ToHeight);
            //从Bitmap创建一个System.Drawing.Graphics对象，用来绘制高质量的缩小图。

            System.Drawing.Graphics gr = System.Drawing.Graphics.FromImage(bmp);

            //设置 System.Drawing.Graphics对象的SmoothingMode属性为HighQuality

            gr.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

            //下面这个也设成高质量

            gr.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;

            //下面这个设成High

            gr.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;

            //把原始图像绘制成上面所设置宽高的缩小图

            System.Drawing.Rectangle rectDestination = new System.Drawing.Rectangle(0, 0, ThumbWidth, ThumbHeight);
            gr.DrawImage(SourceImage, rectDestination, StartX, StartY, SourceImage.Width, SourceImage.Height, GraphicsUnit.Pixel);

            //保存图像，大功告成！

            bmp.Save(ThumbnailFilePath);

            //最后别忘了释放资源
            bmp.Dispose();
            SourceImage.Dispose();
            return ThumbnailFilePath;
        }
        public enum CutPosition
        {
            Header = 1,
            Middle = 2,
            Footer = 3,
        }
        public enum CutDirection
        {
            Auto = 1,
            X = 2,
            Y = 3,
        }
    }
}
