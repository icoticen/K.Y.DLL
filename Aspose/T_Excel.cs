using Aspose.Cells;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace K.Y.DLL.Aspose
{
    public class Excel
    {
        /// <summary>
        /// 通过Excel模板流文件导出xls
        /// </summary>
        /// <param name="response"></param>
        /// <param name="dt">Table数据源</param>
        /// <param name="dataTableName">表名</param>
        /// <param name="templetePath">模板路径</param>
        /// <param name="filename">下载的文件名</param>
        /// <param name="litem"></param>
        public static void Export(HttpResponse response, DataTable dt, string dataTableName, string templetePath, string filename, Dictionary<string, string> dic)
        {
            try
            {
                //  DataTable dt = K_Util.ListToDatatable(llist);
                filename = (filename.EndsWith(".xls") || filename.EndsWith(".xlsx")) ? filename : (filename + "xls");
                dt.TableName = dataTableName == "" ? "A" : dataTableName;
                //if (dt.Rows.Count == 0)
                //    return;
                WorkbookDesigner designer = new WorkbookDesigner();
                designer.Open(templetePath);
                designer.SetDataSource(dt);
                foreach (var p in dic)
                {
                    designer.SetDataSource(p.Key, p.Value);
                }
                //designer.SetDataSource("TableName", Depart.get(SessionManager.getSessionUserInstance().DepartID).DepartName + "区（县）2014年乡公路建设（大修）工程计划表");
                designer.Process();
                designer.Save(string.Format(filename), SaveType.OpenInExcel, FileFormatType.Excel2003, response);
                response.Flush();
                response.Close();
                designer = null;
                response.End();
            }
            catch
            {
                response.Write("<script>alert(\"Excel模板'" + templetePath.Substring(templetePath.LastIndexOf('\\') + 1) + "'正在被使用或已丢失~~~~~~\\n请联系管理员o(╯□╰)o\");</script>");
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="dataTableName"></param>
        /// <param name="templetePath"></param>
        /// <param name="filename"></param>
        /// <param name="dic"></param>
        public static void Export(DataTable dt, string dataTableName, string templetePath, string filename, Dictionary<string, string> dic)
        {
            var response = HttpContext.Current.Response;
            try
            {
                //  DataTable dt = K_Util.ListToDatatable(llist);
                //filename = (filename.EndsWith(".xls") || filename.EndsWith(".xlsx")) ? filename : (filename + "xls");
                dt.TableName = dataTableName == "" ? "A" : dataTableName;
                //if (dt.Rows.Count == 0)
                //    return;
                WorkbookDesigner designer = new WorkbookDesigner();
                designer.Open(templetePath);
                designer.SetDataSource(dt);
                foreach (var p in dic)
                {
                    designer.SetDataSource(p.Key, p.Value);
                }
                //designer.SetDataSource("TableName", Depart.get(SessionManager.getSessionUserInstance().DepartID).DepartName + "区（县）2014年乡公路建设（大修）工程计划表");
                designer.Process();
                designer.Save(string.Format(filename), SaveType.OpenInExcel, FileFormatType.Excel2003, response);
                response.Flush();
                response.Close();
                designer = null;
                response.End();
            }
            catch
            {
                response.Write("<script>alert(\"Excel模板'" + templetePath.Substring(templetePath.LastIndexOf('\\') + 1) + "'正在被使用或已丢失~~~~~~\\n请联系管理员o(╯□╰)o\");</script>");
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="templetePath"></param>
        /// <param name="filename"></param>
        /// <param name="dic"></param>
        public static void Export(DataTable dt, string templetePath, string filename, Dictionary<string, string> dic)
        {
            var response = HttpContext.Current.Response;
            try
            {
                //  DataTable dt = K_Util.ListToDatatable(llist);
                //filename = (filename.EndsWith(".xls") || filename.EndsWith(".xlsx")) ? filename : (filename + "xls");
                dt.TableName = "T";
                //if (dt.Rows.Count == 0)
                //    return;
                WorkbookDesigner designer = new WorkbookDesigner();
                designer.Open(templetePath);
                designer.SetDataSource(dt);
                foreach (var p in dic)
                {
                    designer.SetDataSource(p.Key, p.Value);
                }
                //designer.SetDataSource("TableName", Depart.get(SessionManager.getSessionUserInstance().DepartID).DepartName + "区（县）2014年乡公路建设（大修）工程计划表");
                designer.Process();
                designer.Save(string.Format(filename), SaveType.OpenInExcel, FileFormatType.Excel2003, response);
                response.Flush();
                response.Close();
                designer = null;
                response.End();
            }
            catch
            {
                response.Write("<script>alert(\"Excel模板'" + templetePath.Substring(templetePath.LastIndexOf('\\') + 1) + "'正在被使用或已丢失~~~~~~\\n请联系管理员o(╯□╰)o\");</script>");
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="templetePath"></param>
        /// <param name="filename"></param>
        public static void Export(DataTable dt, string templetePath, string filename)
        {
            var response = HttpContext.Current.Response;
            try
            {
                //  DataTable dt = K_Util.ListToDatatable(llist);
                filename = (filename.EndsWith(".xls") || filename.EndsWith(".xlsx")) ? filename : (filename + ".xlsx");
                dt.TableName = "T";
                //if (dt.Rows.Count == 0)
                //    return;
                WorkbookDesigner designer = new WorkbookDesigner();
                designer.Open(templetePath);
                designer.SetDataSource(dt);
                //designer.SetDataSource("TableName", Depart.get(SessionManager.getSessionUserInstance().DepartID).DepartName + "区（县）2014年乡公路建设（大修）工程计划表");
                designer.Process();
                designer.Save(string.Format(filename), SaveType.OpenInExcel, FileFormatType.Excel2007Xlsx, response);
                response.Flush();
                response.Close();
                designer = null;
                response.End();
            }
            catch
            {
                response.Write("<script>alert(\"Excel模板'" + templetePath.Substring(templetePath.LastIndexOf('\\') + 1) + "'正在被使用或已丢失~~~~~~\\n请联系管理员o(╯□╰)o\");</script>");
            }
        }
        public static AsposeXls Import(String DataFilePath, String TempletePath)
        {
            var x = new AsposeXls();
            x.LoadXlsData(DataFilePath, TempletePath);
            return x;
        }
    }
    public class AsposeXlsCell
    {
        #region Attr
        public string Name { get; set; }
        public string Value { get; set; }
        public int RowIndex { get; set; }
        public int ColIndex { get; set; }
        public AsposeXlsCell()
        {
            Name = "";
            Value = "";
            RowIndex = -1;
            ColIndex = -1;
        }
        #endregion
    }
    public class AsposeXlsItem
    {
        #region Attr
        public AsposeXlsCell this[String Name]
        {
            get { return _LxCells.FirstOrDefault(p => p.Name == Name) ?? new AsposeXlsCell(); }
        }
        public AsposeXlsCell this[Int32 Index]
        {
            get { return _LxCells[Index]; }
        }
        public int HeadRowIndex { get; set; }
        public int HeadColIndex { get; set; }
        public int FootRowIndex { get; set; }
        public int FootColIndex { get; set; }
        public int RowsCount { get; set; }
        public int ColsCount { get; set; }
        public List<AsposeXlsCell> _LxCells;
        public AsposeXlsItem()
        {
            _LxCells = new List<AsposeXlsCell>();
            HeadRowIndex = -1;
            HeadColIndex = -1;
            FootRowIndex = -1;
            FootColIndex = -1;
            RowsCount = -1;
            ColsCount = -1;
        }
        #endregion
        public static AsposeXlsItem Item_LoadTemplete(List<AsposeXlsCell> _LxCells)
        {
            AsposeXlsItem _xItem = new AsposeXlsItem();
            _xItem.HeadRowIndex = _LxCells.Min(p => p.RowIndex);
            _xItem.HeadColIndex = _LxCells.Min(p => p.ColIndex);
            _xItem.FootRowIndex = _LxCells.Max(p => p.RowIndex);
            _xItem.FootColIndex = _LxCells.Max(p => p.ColIndex);
            _xItem._LxCells = _LxCells.Where(p => !p.Name.ToLower().Contains("null")).ToList();
            _xItem.RowsCount = _xItem.FootRowIndex - _xItem.HeadRowIndex + 1;
            _xItem.ColsCount = _xItem.FootColIndex - _xItem.HeadColIndex + 1;
            return _xItem;
        }
        public static DataRow DS_GetRow(AsposeXlsItem _xItem, System.Data.DataTable dt)
        {
            //创建一个DataRow实例    
            DataRow row = dt.NewRow();
            //给row 赋值    
            _xItem._LxCells.ForEach(p => row[p.Name] = p.Value);

            return row;
        }
    }
    public class AsposeXlsSheet
    {
        #region Attr
        public string SheetName { get; set; }
        public int SheetIndex { get; set; }
        /// <summary>
        /// 模板~~~导入的最小单元
        /// </summary>
        public AsposeXlsItem _xItem_Templete;
        /// <summary>
        /// 全局数据
        /// </summary>
        public List<AsposeXlsCell> _lxCells_SheetValue;
        /// <summary>
        /// 单表包含数据
        /// </summary>
        public List<AsposeXlsItem> _LxItems_SheetData;
        public String ErrorMsg { get; set; }
        public AsposeXlsSheet()
        {
            _xItem_Templete = new AsposeXlsItem();
            _lxCells_SheetValue = new List<AsposeXlsCell>();
            _LxItems_SheetData = new List<AsposeXlsItem>();
            ErrorMsg = "";
            SheetIndex = 0;
            SheetName = "Sheet" + (SheetIndex + 1);
        }
        #endregion
        /// <summary>
        /// 导入模板
        /// </summary>
        /// <param name="sheet"></param>
        /// <returns></returns>
        public static AsposeXlsSheet Sheet_LoadTemplete(Worksheet sheet)
        {
            AsposeXlsSheet _xSheet = new AsposeXlsSheet();
            _xSheet.SheetName = sheet.Name;
            _xSheet.SheetIndex = sheet.Index;
            try
            {
                var _LxCells = new List<AsposeXlsCell>();
                Cells cells = sheet.Cells;
                System.Data.DataTable dt = cells.ExportDataTableAsString(0, 0, cells.MaxDataRow + 1, cells.MaxDataColumn + 1, true);
                //遍历所有表单元格，获取对象属性
                #region 获取到所有cell
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        string cellvalue = dt.Rows[i][j].ToString().Trim().Replace(" ", "");

                        AsposeXlsCell _xcell = new AsposeXlsCell();
                        _xcell.RowIndex = i;
                        _xcell.ColIndex = j;
                        //全局模板
                        if (cellvalue.StartsWith("&=$"))
                        {
                            _xcell.Name = _xcell.Name.Substring(3);
                            if (!string.IsNullOrEmpty(_xcell.Name))
                                _xSheet._lxCells_SheetValue.Add(_xcell);
                        }//单元模板
                        else if (cellvalue.StartsWith("&="))
                        {
                            _xcell.Name = cellvalue.Split('.').Count() == 2 ? cellvalue.Split('.')[1] : "";
                            if (!string.IsNullOrEmpty(_xcell.Name))
                                _LxCells.Add(_xcell);
                        }
                    }
                }
                #endregion
                if (_LxCells.Count > 0)
                    _xSheet._xItem_Templete = AsposeXlsItem.Item_LoadTemplete(_LxCells);
                else _xSheet.ErrorMsg += "error sheettemplete:系统模板无效，请联系管理员；\n";
            }
            catch
            {
                _xSheet.ErrorMsg += "error sheettemplete:系统模板出错，请联系管理员；\n";
            }
            return _xSheet;
        }
        /// <summary>
        /// 加载导入数据
        /// </summary>
        /// <param name="sheet"></param>
        /// <param name="Templete"></param>
        /// <returns></returns>
        public static AsposeXlsSheet Sheet_LoadData(Worksheet sheet, AsposeXlsItem Templete)
        {
            AsposeXlsSheet _xSheet = new AsposeXlsSheet();
            _xSheet._xItem_Templete = Templete;
            _xSheet.SheetIndex = sheet.Index;
            _xSheet.SheetName = sheet.Name;
            try
            {
                Cells cells = sheet.Cells;
                System.Data.DataTable dt = cells.ExportDataTableAsString(0, 0, cells.MaxDataRow + 1, cells.MaxDataColumn + 1, true);
                if (_xSheet._lxCells_SheetValue.Count > 0)
                    _xSheet._lxCells_SheetValue.ForEach(p =>
                    {
                        try
                        {
                            p.Value = dt.Rows[p.RowIndex][p.ColIndex].ToString();
                        }
                        catch
                        {
                            _xSheet.ErrorMsg += "error sheetvalue:未找到" + (p.RowIndex + 2) + "行" + (p.RowIndex + 1) + "列\n";
                        }
                    });
                for (int i = Templete.HeadRowIndex; i < dt.Rows.Count; i += Templete.RowsCount)
                {
                    //损坏的序列跳过
                    try
                    {
                        //DataRow dr = new DataRow();
                        AsposeXlsItem _xItem = new AsposeXlsItem();
                        foreach (var _mCell in Templete._LxCells)
                        {
                            AsposeXlsCell _xCell = new AsposeXlsCell();
                            _xCell.ColIndex = _mCell.ColIndex;
                            _xCell.RowIndex = _mCell.RowIndex - Templete.HeadRowIndex + i;
                            _xCell.Name = _mCell.Name;
                            _xCell.Value = dt.Rows[_xCell.RowIndex][_xCell.ColIndex].ToString();
                            _xItem._LxCells.Add(_xCell);
                        }

                        _xSheet._LxItems_SheetData.Add(_xItem);
                    }
                    catch
                    {
                        _xSheet.ErrorMsg += "error sheetitem:第" + ((Int32)(i / Templete.RowsCount) + 1) + "条数据被忽略;\n";
                    }
                }

            }
            catch
            {
                _xSheet.ErrorMsg += "error sheet:未知原因，请报告管理员;\n";
            }
            return _xSheet;
        }
        public static System.Data.DataTable DS_GetTable(AsposeXlsSheet _xSheet)
        {
            System.Data.DataTable table = new System.Data.DataTable();
            _xSheet._xItem_Templete._LxCells.ForEach(p => table.Columns.Add(p.Name));
            _xSheet._LxItems_SheetData.ForEach(p => table.Rows.Add(AsposeXlsItem.DS_GetRow(p, table)));
            return table;
        }
    }
    public class AsposeXlsData
    {
        #region Attr
        public String ErrorMsg { get; set; }
        public List<AsposeXlsSheet> _LxSheets;

        public AsposeXlsData()
        {
            _LxSheets = new List<AsposeXlsSheet>();
            ErrorMsg = "";
        }
        #endregion

        public static AsposeXlsData Data_LoadTemplete(String TempletePath)
        {
            AsposeXlsData _XData = new AsposeXlsData();
            if (!new FileInfo(TempletePath).Exists)
            {
                _XData.ErrorMsg += "error datatemplete:系统模板丢失，请联系管理员；\n";
                return _XData;
            }
            Workbook book = new Workbook();
            book.Open(TempletePath);
            foreach (Worksheet ws in book.Worksheets)
            {
                _XData._LxSheets.Add(AsposeXlsSheet.Sheet_LoadTemplete(ws));
            }
            return _XData;
        }
        public static AsposeXlsData Data_LoadData(String FilePath, String TempletePath)
        {
            AsposeXlsData _xData = new AsposeXlsData();
            if (!new FileInfo(FilePath).Exists || !new FileInfo(TempletePath).Exists)
            {
                _xData.ErrorMsg += "error datafilemiss:服务器出错，文件丢失，请重新上传，或请联系管理员；\n";
                return _xData;
            }
            //获取Templete 
            _xData = AsposeXlsData.Data_LoadTemplete(TempletePath);
            Workbook book = new Workbook();
            book.Open(FilePath);

            foreach (Worksheet ws in book.Worksheets)
            {
                if (ws.Cells.Count < 2) continue;
                Int32 i = ws.Index;
                if (_xData._LxSheets[i]._xItem_Templete._LxCells.Count <= 0) continue;
                try
                {

                    _xData._LxSheets[i] = AsposeXlsSheet.Sheet_LoadData(ws, _xData._LxSheets[i]._xItem_Templete);
                    if (_xData._LxSheets[i]._LxItems_SheetData.Count <= 0)
                        _xData.ErrorMsg += "error datasheet:sheet" + (i + 1) + ":" + ws.Name + "无有效数据,请确保表格格式和填写正确；\n";
                }
                catch { }
            }
            return _xData;
        }
        public static DataSet DS_GetSet(AsposeXlsData _xData)
        {
            DataSet ds = new DataSet();
            _xData._LxSheets.ForEach(p => ds.Tables.Add(AsposeXlsSheet.DS_GetTable(p)));
            return ds;
        }
    }
    public class AsposeXls
    {
        #region Attr
        private int _SheetCount;
        public String _TempletePath { get; set; }
        public String _DataFilePath { get; set; }
        public AsposeXlsData XData { get { return _xData; } }
        public String ErrorMsg { get; set; }
        private DataSet _xDataSet;
        private AsposeXlsData _xData;

        //SheetCount 只读
        public int SheetCount
        {
            get
            {
                return _SheetCount;
            }
        }
        public AsposeXls()
        {
            _SheetCount = 0;
            _TempletePath = "";
            _DataFilePath = "";
            _xDataSet = new DataSet();
            _xData = new AsposeXlsData();
        }
        #endregion

        public void LoadXlsData()
        {
            if (!new FileInfo(_DataFilePath).Exists || !new FileInfo(_TempletePath).Exists)
                return;
            _xData = AsposeXlsData.Data_LoadData(_DataFilePath, _TempletePath);
            _xDataSet = AsposeXlsData.DS_GetSet(_xData);
            _SheetCount = _xData._LxSheets.Count;
        }
        public void LoadXlsData(String DataFilePath, String TempletePath)
        {
            _DataFilePath = DataFilePath;
            _TempletePath = TempletePath;
            LoadXlsData();
        }
        public DataSet GetDataSet()
        {
            if (SheetCount > 0)
            {
                return _xDataSet;
            }
            else
                return new DataSet();
        }

        public System.Data.DataTable GetDataTable()
        {
            return _xDataSet.Tables[0];
        }
        public System.Data.DataTable GetDataTable(String SheetName)
        {
            return _xDataSet.Tables.Contains(SheetName) ? _xDataSet.Tables[SheetName] : new System.Data.DataTable();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="SheetIndex">所处Sheet排序 从0开始</param>
        /// <returns></returns>
        public System.Data.DataTable GetDataTable(int SheetIndex)
        {
            return _xDataSet.Tables.Count > SheetIndex ? _xDataSet.Tables[SheetIndex] : new System.Data.DataTable();
        }

    }
}

