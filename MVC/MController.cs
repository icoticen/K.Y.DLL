using K.Y.DLL.Model;
using K.Y.DLL.Tool;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web.Mvc;

namespace K.Y.DLL.MVC
{
    public abstract class MController<E> : MController
        where E : System.Data.Entity.DbContext, new()
    {

        #region Index 暂不可用
        private ActionResult _Index(String _PageBelong)
        {
            ViewBag._PageBelong = String.IsNullOrWhiteSpace(_PageBelong) ? "Admin" : _PageBelong;
            return View();
        }
        private ActionResult _Index_Top(Dictionary<String, String> lLink)
        {
            ViewBag.lLink = lLink;
            return View();
        }
        private ActionResult _Index_Left(List<M_TreeNode> iList)
        {
            //var iList = new List<M_TreeNode>();
            //iList.AddRange(new List<M_TreeNode>
            //{
            //    new M_TreeNode(){NodeID=1,NodeLevel=2,NodeName="落地页",NodeValue="",ParentNodeID=0},
            //    new M_TreeNode(){NodeID=0,NodeLevel=3,NodeName="成功案例",NodeValue="Admin_WEB_BData_SuccessfulCase_List",ParentNodeID=1},
            //    new M_TreeNode(){NodeID=0,NodeLevel=3,NodeName="老师列表",NodeValue="Admin_WEB_Emp_Teacher_List",ParentNodeID=1},
            //});
            //iList.AddRange(new List<M_TreeNode>            
            //{
            //    new M_TreeNode(){NodeID=1,NodeLevel=2,NodeName="试卷管理",NodeValue="",ParentNodeID=0},
            //    new M_TreeNode(){NodeID=0,NodeLevel=3,NodeName="试卷列表",NodeValue="Admin_WEB_Examination_TPO_List",ParentNodeID=1},
            //});
            //iList.AddRange(new List<M_TreeNode>            
            //{
            //    new M_TreeNode(){NodeID=4,NodeLevel=2,NodeName="试卷题目管理",NodeValue="",ParentNodeID=0},
            //    new M_TreeNode(){NodeID=0,NodeLevel=3,NodeName="试卷题目相关数据列表",NodeValue="Admin_WEB_Examination_Contain_TPO_List",ParentNodeID=4},
            //});
            //iList.AddRange(new List<M_TreeNode>            
            //{
            //    new M_TreeNode(){NodeID=5,NodeLevel=2,NodeName="阅读管理",NodeValue="",ParentNodeID=0},
            //    new M_TreeNode(){NodeID=0,NodeLevel=3,NodeName="阅读文章列表",NodeValue="Admin_WEB_Examination_Read_Passage_TPO_List",ParentNodeID=5},
            //    new M_TreeNode(){NodeID=1,NodeLevel=3,NodeName="阅读题目列表",NodeValue="Admin_WEB_Examination_Read_Question_TPO_List",ParentNodeID=5},

            //});
            //iList.AddRange(new List<M_TreeNode>            
            //{
            //    new M_TreeNode(){NodeID=6,NodeLevel=2,NodeName="听力管理",NodeValue="",ParentNodeID=0},
            //    new M_TreeNode(){NodeID=0,NodeLevel=3,NodeName="听力文章列表",NodeValue="Admin_WEB_Examination_Listen_Conversation_TPO_List",ParentNodeID=6},
            //    new M_TreeNode(){NodeID=1,NodeLevel=3,NodeName="听力题目列表",NodeValue="Admin_WEB_Examination_Listen_Question_TPO_List",ParentNodeID=6},

            //});
            //iList.AddRange(new List<M_TreeNode>            
            //{
            //    new M_TreeNode(){NodeID=7,NodeLevel=2,NodeName="口语管理",NodeValue="",ParentNodeID=0},
            //    new M_TreeNode(){NodeID=0,NodeLevel=3,NodeName="口语题目列表",NodeValue="Admin_WEB_Examination_Speak_Question_TPO_List",ParentNodeID=7},


            //});
            //iList.AddRange(new List<M_TreeNode>            
            //{
            //    new M_TreeNode(){NodeID=8,NodeLevel=2,NodeName="写作管理",NodeValue="",ParentNodeID=0},
            //    new M_TreeNode(){NodeID=0,NodeLevel=3,NodeName="写作文章列表",NodeValue="Admin_WEB_Examination_Write_Task_TPO_List",ParentNodeID=8},
            //    new M_TreeNode(){NodeID=1,NodeLevel=3,NodeName="写作题目列表",NodeValue="Admin_WEB_Examination_Write_Question_TPO_List",ParentNodeID=8},
            //});
            //iList.AddRange(new List<M_TreeNode>            
            //{
            //    new M_TreeNode(){NodeID=9,NodeLevel=2,NodeName="用户考试记录",NodeValue="",ParentNodeID=0},
            //    new M_TreeNode(){NodeID=0,NodeLevel=3,NodeName="用户考试记录列表",NodeValue="Admin_WEB_User_Examination_TPO_List",ParentNodeID=9},

            //});
            ViewBag.iList = iList;
            return View();
        }
        private ActionResult _Index_Right()
        {

            //FileStream fs = new FileStream(Server.MapPath("~" + K.Y.DLL.Tool.WebConfig.CSD_Admin_GoodImage_BasePath + "G_B_Default.gif"), FileMode.Open, FileAccess.Read); //将图片以文件流的形式进行保存
            //BinaryReader br = new BinaryReader(fs);
            //byte[] TEX = br.ReadBytes((int)fs.Length);  //将流读入到字节数组中
            ////MemoryStream ms = new MemoryStream(TEX);
            ////Bitmap bmpt = new Bitmap(ms);
            ////String Path = Server.MapPath("~/AppData/AR/User/PostedImg/P_" + DateTime.Now.ToString("yyMMddHHmmss") + ".gif");
            ////bmpt.Save(Path);
            //Response.BinaryWrite(TEX);
            return Content("");
        }


        #endregion

        #region _T_
        protected ActionResult _T_Admin_List<T>(T Model, DateTime? dt1, DateTime? dt2, Func<IQueryable<T>, IOrderedQueryable<T>> F)
            where T : class,new()
        {
            return _T_Admin_List<E, T>(Model, dt1, dt2, F);
        }
        protected ActionResult _T_Ajax_Admin_List<T, Q>(T Model, Int32? PageIndex, Int32? PageSize, Func<IQueryable<T>, IOrderedQueryable<T>> F, Func<List<T>, IEnumerable<Q>> F2)
            where T : class,new()
        {
            return _T_Ajax_Admin_List<E, T, Q>(Model, PageIndex, PageSize, F, F2);
        }


        //protected ActionResult _T_Admin_Insert<T>(T Model)
        //    where T : class,new()
        //{
        //    //var ViewName = this.RouteData.Values["Action"].ToString();
        //    if (Request.RequestType == "POST")
        //    {
        //        var R = K.Y.DLL.Entity._API<E>.Insert<T>(Model);
        //        _T_Admin_Response_K_LayerClose(R);
        //        return Content("");
        //    }
        //    return View();
        //}
        protected ActionResult _T_Admin_Insert<T>(T Model, Func<E, M_Result> F = null, Action<E, T> CallBack = null)
            where T : class,new()
        {
            return _T_Admin_Insert<E, T>(Model, F, CallBack);
        }

        protected ActionResult _T_Admin_Update<T>(Int32 ID, Action<T> A, Action<E, T> CallBack = null)
            where T : class,new()
        {
            return _T_Admin_Update<E, T>(ID, A, CallBack);
        }
        protected ActionResult _T_Admin_Update<T>(Expression<Func<T, Boolean>> F, Action<T> A, Action<E, T> CallBack = null)
            where T : class,new()
        {
            return _T_Admin_Update<E, T>(F, A, CallBack);
        }

        protected ActionResult _T_Admin_Delete<T>(Int32 ID, Action<E, T> CallBack = null)
            where T : class,new()
        {
            return _T_Admin_Delete<E, T>(ID, CallBack);
        }
        protected ActionResult _T_Admin_Delete<T>(Expression<Func<T, Boolean>> F, Action<E, T> CallBack = null)
            where T : class,new()
        {
            return _T_Admin_Delete<E, T>(F, CallBack);
        }

        protected ActionResult _T_Admin_View<T>(Int32 ID, Action<E, T> CallBack = null)
            where T : class,new()
        {
            return _T_Admin_View<E, T>(ID, CallBack);
        }
        protected ActionResult _T_Admin_View<T>(Expression<Func<T, Boolean>> F, Action<E, T> CallBack = null)
            where T : class,new()
        {
            return _T_Admin_View<E, T>(F, CallBack);
        }


        protected ActionResult _T_API_List<T, Q>(Int32? PageIndex, Int32? PageSize, Func<IQueryable<T>, IOrderedQueryable<T>> F, Func<List<T>, IEnumerable<Q>> F2)
            where T : class,new()
        {
            return _T_API_List<E, T, Q>(PageIndex, PageSize, F, F2);
        }
        #endregion
    }
    public abstract class MController : System.Web.Mvc.Controller
    {
        #region Identity
        private Int32 __AdminID = 0;
        protected Int32 _AdminID
        {
            get
            {
                if (__AdminID > 0)
                    return __AdminID;
                var M = M_Identify.Get("_Admin");
                if (M == null) __AdminID = 0;
                else __AdminID = M.ID;
                return __AdminID;
            }
            set
            {
                if (value <= 0)
                {
                    __AdminID = 0;
                    M_Identify.Clear("_Admin");
                }
                else
                {
                    M_Identify.Set("_Admin", new M_Identify { ID = value, Identify = 1, IdentifyCode = "_Admin" });
                    __AdminID = value;
                }
            }
        }
        private Int32 __UserID = 0;
        protected Int32 _UserID
        {
            get
            {
                if (__UserID > 0)
                    return __UserID;
                var M = M_Identify.Get("_UserID");
                if (M == null) __UserID = 0;
                else __UserID = M.ID;
                return __UserID;
            }
            set
            {
                if (value <= 0)
                {
                    __UserID = 0;
                    M_Identify.Clear("_UserID");
                }
                else
                {
                    M_Identify.Set("_UserID", new M_Identify { ID = value, Identify = 1, IdentifyCode = "_UserID" });
                    __UserID = value;
                }
            }
        }
        #endregion

        private static String __SYS_LocalHostAuthority = "http://" + System.Web.HttpContext.Current.Request.Url.Authority;
        public static String _SYS_LocalHostAuthority
        {
            get
            {
                var S = System.Web.Configuration.WebConfigurationManager.AppSettings["_SYS_LocalHostAuthority"];
                return String.IsNullOrEmpty(S) ? __SYS_LocalHostAuthority : S;
            }
        }



        #region Tool
        protected void _T_Admin_Response_K_LayerClose(M_Result M)
        {
            Response.Write(T_Web.Tmpl_Javascript("parent.K_LayerClose(" + M.Ex_ToJson() + ")"));
        }
        protected void _T_Admin_Response_Top_Redirect(String URL)
        {
            Response.Write(T_Web.Tmpl_Javascript("window.top.location='" + URL + "'"));
        } 
        #endregion

        #region File

        protected ActionResult _T_Ajax_FilePost(String FileElementName, String Dirpath, String PreStr, String FileType = "image", String FileExtension = ".jpg.bmp.png.jpeg.gif")
        {
            //String FileExtension = ".jpg.bmp.png.jpeg.gif";
            switch (FileType .ToLower())
            {
                //其他定制吧  ->_->
                case "image": FileExtension = ".jpg.bmp.png.jpeg.gif"; break;
                case "video": FileExtension = ".mp4.flv.swf.avi.3gp.f4v"; break;
                case "zip": FileExtension = ".zip.rar.7z."; break;
                case "application": FileExtension = ".apk."; break;
                case "document": FileExtension = ".xls.xlsx.doc.docx.ppt.pptx.wps.txt"; break;


                case "datafile": FileExtension = ".jpg.bmp.png.jpeg.gif .xls.xlsx.doc.docx.ppt.pptx.wps.txt .pdf .zip.rar.7z .apk .mp4.flv.swf.avi.3gp.f4v"; break;
            }
            var URL = "";
            var pFile = Request.Files[FileElementName];
            URL = T_File.File_Save(pFile, Dirpath, PreStr, FileExtension);
            if (URL == "0") return Content(new { result = 0, msg = "文件不能为空~" }.Ex_ToJson());
            else if (URL == "-1") return Content(new { result = -1, msg = "文件上传失败~请选择正确的文件或稍后再试~" }.Ex_ToJson());

            return Content(new { result = 1, data = _SYS_LocalHostAuthority + URL, msg = "上传成功~" }.Ex_ToJson());
        }
        protected ActionResult _T_Ajax_FilePost(String FileElementName, String Dirpath, String PreStr, Boolean IntoDirDay, String FileType)
        {
            String FileExtension = ".jpg.bmp.png.jpeg.gif";
            switch ((FileType ?? "image").ToLower())
            {
                //其他定制吧  ->_->
                case "image": FileExtension = ".jpg.bmp.png.jpeg.gif"; break;
                case "video": FileExtension = ".mp4.flv.swf.avi.3gp.f4v"; break;
                case "zip": FileExtension = ".zip.rar.7z."; break;
                case "application": FileExtension = ".apk."; break;
                case "document": FileExtension = ".xls.xlsx.doc.docx.ppt.pptx.wps.txt"; break;


                case "datafile": FileExtension = ".jpg.bmp.png.jpeg.gif .xls.xlsx.doc.docx.ppt.pptx.wps.txt .pdf"; break;
            }
            var URL = "";
            var pFile = Request.Files[FileElementName];
            URL = T_File.File_Save(pFile, Dirpath, PreStr, IntoDirDay, FileExtension);
            if (URL == "0") return Content(new { result = 0, msg = "文件不能为空~" }.Ex_ToJson());
            else if (URL == "-1") return Content(new { result = -1, msg = "文件上传失败~请选择正确的文件或稍后再试~" }.Ex_ToJson());

            return Content(new { result = 1, data = _SYS_LocalHostAuthority + URL, msg = "上传成功~" }.Ex_ToJson());
        }
        //文件替换
        protected ActionResult _T_Ajax_FilePost(String FileElementName, String AbsolutePath, String FileType)
        {
            String FileExtension = ".jpg.bmp.png.jpeg.gif";
            switch ((FileType ?? "image").ToLower())
            {
                //其他定制吧  ->_->
                case "image": FileExtension = ".jpg.bmp.png.jpeg.gif"; break;
                case "video": FileExtension = ".mp4.flv.swf.avi.3gp.f4v"; break;
                case "zip": FileExtension = ".zip.rar.7z."; break;
                case "application": FileExtension = ".apk."; break;
                case "document": FileExtension = ".xls.xlsx.doc.docx.ppt.pptx.wps.txt"; break;

                case "datafile": FileExtension = ".jpg.bmp.png.jpeg.gif .xls.xlsx.doc.docx.ppt.pptx.wps.txt .pdf"; break;
            }
            var URL = "";
            var pFile = Request.Files[FileElementName];
            URL = T_File.File_Replace(pFile, AbsolutePath, FileExtension);
            if (URL == "0") return Content(new { result = 0, msg = "文件不能为空~" }.Ex_ToJson());
            else if (URL == "-1") return Content(new { result = -1, msg = "文件上传失败~请选择正确的文件或稍后再试~" }.Ex_ToJson());

            return Content(new { result = 1, data = _SYS_LocalHostAuthority + URL, msg = "上传成功~" }.Ex_ToJson());
        }

        protected void _Log_FileUpload_URLMsg(String Func)
        {
            String UrlMsg = "";
            UrlMsg += "URL: " + this.Request.Url.AbsoluteUri + "\r\n";
            UrlMsg += "Action: " + this.RouteData.Values["Action"] + "\r\n";
            UrlMsg += "Files:{\r\n";
            if (this.Request.Files.Count > 0)
                foreach (var K in this.Request.Files.AllKeys)
                {
                    var F = this.Request.Files[K];
                    UrlMsg += "\t\t------------------------[" + K + "]------------------------\r\n";
                    UrlMsg += "\t\tFileName:" + F.FileName + "\r\n";
                    UrlMsg += "\t\tContentLength:" + F.ContentLength + "\r\n";
                    UrlMsg += "\t\tContentType:" + F.ContentType + "\r\n";
                }
            UrlMsg += "}\r\n";
            UrlMsg += "####################################################################################################";
            //AR_API.LogRun(UrlMsg);          
            T_Log.LogMsg(Func, UrlMsg);
        } 
        #endregion


        #region _T_  --  This Is The Base
        protected ActionResult _T_Admin_List<E, T>(T Model, DateTime? dt1, DateTime? dt2, Func<IQueryable<T>, IOrderedQueryable<T>> F)
            where E : System.Data.Entity.DbContext, new()
            where T : class,new()
        {
            //var ViewName = this.RouteData.Values["Action"].ToString();
            var Page = new M_Pagination(1, 10);
            var iList = K.Y.DLL.Entity._API<E>.Search<T>(Page, list => F(list));

            ViewBag.Page = Page;
            ViewBag.dtmin = dt1.HasValue ? dt1.Ex_ToString("yyyy-MM-dd") : "";
            ViewBag.dtmax = dt2.HasValue ? dt2.Ex_ToString("yyyy-MM-dd") : "";
            return View(Model);
        }
        protected ActionResult _T_Ajax_Admin_List<E, T, Q>(T Model, Int32? PageIndex, Int32? PageSize, Func<IQueryable<T>, IOrderedQueryable<T>> F, Func<List<T>, IEnumerable<Q>> F2)
            where E : System.Data.Entity.DbContext, new()
            where T : class,new()
        {
            //var ViewName = this.RouteData.Values["Action"].ToString();
            if (PageIndex > 0)
            {
                M_Pagination Page = new M_Pagination() { PageIndex = PageIndex.Value, PageSize = PageSize ?? 10 };
                var iList = K.Y.DLL.Entity._API<E>.Search<T>(Page, list => F(list));
                if (iList == null) return Content(new { result = -1, page = Page, model = Model }.Ex_ToJson());
                if (iList.Count == 0) return Content(new { result = 2, page = Page, model = Model }.Ex_ToJson());
                return Content(new
                {
                    result = 1,
                    page = Page,
                    model = Model,
                    data = F2(iList),
                }.Ex_ToJson());
            }
            else return Content(new { result = 0 }.Ex_ToJson());
        }


        //protected ActionResult _T_Admin_Insert<T>(T Model)
        //    where T : class,new()
        //{
        //    //var ViewName = this.RouteData.Values["Action"].ToString();
        //    if (Request.RequestType == "POST")
        //    {
        //        var R = K.Y.DLL.Entity._API<E>.Insert<T>(Model);
        //        _T_Admin_Response_K_LayerClose(R);
        //        return Content("");
        //    }
        //    return View();
        //}
        protected ActionResult _T_Admin_Insert<E, T>(T Model, Func<E, M_Result> F = null, Action<E, T> CallBack = null)
            where E : System.Data.Entity.DbContext, new()
            where T : class,new()
        {
            //var ViewName = this.RouteData.Values["Action"].ToString();
            if (Request.RequestType == "POST")
            {
                var R = K.Y.DLL.Entity._API<E>.Insert<T>(Model, F, CallBack);
                _T_Admin_Response_K_LayerClose(R);
                return Content("");
            }
            return View();
        }

        protected ActionResult _T_Admin_Update<E, T>(Int32 ID, Action<T> A, Action<E, T> CallBack = null)
            where E : System.Data.Entity.DbContext, new()
            where T : class,new()
        {
            //var ViewName = this.RouteData.Values["Action"].ToString();
            var M = K.Y.DLL.Entity._API<E>.Model<T>(ID);
            if (M == null)
            {
                _T_Admin_Response_K_LayerClose(new M_Result { result = 0, msg = "错误的请求" });
                return Content("");
            }
            if (Request.RequestType == "POST")
            {
                var R = K.Y.DLL.Entity._API<E>.Update<T>(ID, model => A(model), CallBack);
                _T_Admin_Response_K_LayerClose(R);
                return Content("");
            }
            return View(M);
        }
        protected ActionResult _T_Admin_Update<E, T>(Expression<Func<T, Boolean>> F, Action<T> A, Action<E, T> CallBack = null)
            where E : System.Data.Entity.DbContext, new()
            where T : class,new()
        {
            //var ViewName = this.RouteData.Values["Action"].ToString();
            var M = K.Y.DLL.Entity._API<E>.Model<T>(F);
            if (M == null)
            {
                _T_Admin_Response_K_LayerClose(new M_Result { result = 0, msg = "错误的请求" });
                return Content("");
            }
            if (Request.RequestType == "POST")
            {
                var R = K.Y.DLL.Entity._API<E>.Update<T>(F, model => A(model), CallBack);
                _T_Admin_Response_K_LayerClose(R);
                return Content("");
            }
            return View(M);
        }

        protected ActionResult _T_Admin_Delete<E, T>(Int32 ID, Action<E, T> CallBack = null)
            where E : System.Data.Entity.DbContext, new()
            where T : class,new()
        {
            //var ViewName = this.RouteData.Values["Action"].ToString();
            var R = K.Y.DLL.Entity._API<E>.Delete<T>(ID, CallBack);
            _T_Admin_Response_K_LayerClose(R);
            return Content("");
        }
        protected ActionResult _T_Admin_Delete<E, T>(Expression<Func<T, Boolean>> F, Action<E, T> CallBack = null)
            where E : System.Data.Entity.DbContext, new()
            where T : class,new()
        {
            //var ViewName = this.RouteData.Values["Action"].ToString();
            var R = K.Y.DLL.Entity._API<E>.Delete<T>(F, CallBack);
            _T_Admin_Response_K_LayerClose(R);
            return Content("");
        }

        protected ActionResult _T_Admin_View<E, T>(Int32 ID, Action<E, T> CallBack = null)
            where E : System.Data.Entity.DbContext, new()
            where T : class,new()
        {
            //var ViewName = this.RouteData.Values["Action"].ToString();
            var M = K.Y.DLL.Entity._API<E>.Model<T>(ID, CallBack);
            if (M == null)
            {
                _T_Admin_Response_K_LayerClose(new M_Result { result = 0, msg = "错误的请求" });
                return Content("");
            }
            return View(M);
        }
        protected ActionResult _T_Admin_View<E, T>(Expression<Func<T, Boolean>> F, Action<E, T> CallBack = null)
            where E : System.Data.Entity.DbContext, new()
            where T : class,new()
        {
            //var ViewName = this.RouteData.Values["Action"].ToString();
            var M = K.Y.DLL.Entity._API<E>.Model<T>(F, CallBack);
            if (M == null)
            {
                _T_Admin_Response_K_LayerClose(new M_Result { result = 0, msg = "错误的请求" });
                return Content("");
            }
            return View(M);
        }


        protected ActionResult _T_API_List<E, T, Q>(Int32? PageIndex, Int32? PageSize, Func<IQueryable<T>, IOrderedQueryable<T>> F, Func<List<T>, IEnumerable<Q>> F2)
            where E : System.Data.Entity.DbContext, new()
            where T : class,new()
        {
            var ViewName = this.RouteData.Values["Action"].ToString();
            if (PageIndex > 0)
            {
                M_Pagination Page = new M_Pagination() { PageIndex = PageIndex.Value, PageSize = PageSize ?? 10 };
                var iList = K.Y.DLL.Entity._API<E>.Search<T>(Page, list => F(list));
                if (iList == null) return Content(new M_Result { result = -1, msg = "" }.Ex_ToJson());
                if (iList.Count == 0) return Content(new M_Result { result = 2, msg = "操作成功 但数据为空" }.Ex_ToJson());
                return Content(new M_Result
                {
                    result = 1,
                    msg = "操作成功",
                    data = F2(iList),
                }.Ex_ToJson());
            }
            else return Content(new M_Result { result = 0, msg = "参数错误 PageIndex必须大于0" }.Ex_ToJson());
        }
        #endregion
    }
}
