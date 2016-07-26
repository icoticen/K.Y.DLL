using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace K.Y.DLL.Model
{
    public class M_Pagination
    {
        public Int32 PageIndex { get; set; }
        public Int32 PageCount { get; set; }
        public Int32 RowCount { get; set; }
        public Int32 PageSize { get; set; }
        public M_Pagination()
        {
            this.PageIndex = 1;
            this.PageSize = 10;
        }
        public M_Pagination(Int32 _PageIndex)
        {
            this.PageIndex = _PageIndex;
            this.PageSize = 10;
        }
        public M_Pagination(Int32 _PageIndex, Int32 _PageSize)
        {
            this.PageIndex = _PageIndex;
            this.PageSize = _PageSize;
        }
        public void GetPagination<T>(IQueryable<T> iList, ref List<T> Result)
        {
            if (PageSize > 0)//分页数小于1 则取出全部
            {
                Result = iList.Skip(PageSize * (PageIndex - 1))
                               .Take(PageSize).ToList();
                RowCount = iList.Count();
                PageIndex = PageIndex;
                PageCount = (RowCount + PageSize - 1) / PageSize;
                PageCount = PageCount > 0 ? PageCount : 1;
            }
            else
            {
                Result = iList.ToList();
                RowCount = iList.Count();
                PageIndex = 1;
                PageCount = 1;
            }
        }
        public List<T> GetPagination<T>(IOrderedQueryable<T> iList)
        {
            if (PageSize > 0)//分页数小于1 则取出全部
            {
                var Result = iList.Skip(PageSize * (PageIndex - 1))
                                  .Take(PageSize).ToList();
                RowCount = iList.Count();
                PageIndex = PageIndex;
                PageCount = (RowCount + PageSize - 1) / PageSize;
                PageCount = PageCount > 0 ? PageCount : 1;
                return Result;
            }
            else
            {
                var Result = iList.ToList();
                RowCount = iList.Count();
                PageIndex = 1;
                PageCount = 1;
                return Result;
            }
        }
        public static M_Pagination GetPagination<T>(IOrderedQueryable<T> iList, ref List<T> Result, Int32 PageIndex, Int32 PageSize)
        {
            var page = new M_Pagination();
            if (PageSize > 0)//分页数小于1 则取出全部
            {
                Result = iList.Skip(PageSize * (PageIndex - 1))
                               .Take(PageSize).ToList();
                page.RowCount = iList.Count();
                page.PageIndex = PageIndex;
                page.PageCount = (page.RowCount + PageSize - 1) / PageSize;
                page.PageCount = page.PageCount > 0 ? page.PageCount : 1;
            }
            else
            {
                Result = iList.ToList();
                page.RowCount = iList.Count();
                page.PageIndex = 1;
                page.PageCount = 1;
            }
            return page;
        }
        public static void GetPagination<T>(IOrderedQueryable<T> iList, ref List<T> Result, ref M_Pagination Page)
        {
            if (Page.PageSize > 0)//分页数小于1 则取出全部
            {
                Result = iList.Skip(Page.PageSize * (Page.PageIndex - 1))
                               .Take(Page.PageSize).ToList();
                Page.RowCount = iList.Count();
                Page.PageIndex = Page.PageIndex;
                Page.PageCount = (Page.RowCount + Page.PageSize - 1) / Page.PageSize;
                Page.PageCount = Page.PageCount > 0 ? Page.PageCount : 1;
            }
            else
            {
                Result = iList.ToList();
                Page.RowCount = iList.Count();
                Page.PageIndex = 1;
                Page.PageCount = 1;
            }
        }
    }
}
