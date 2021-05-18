using System;
using System.Collections.Generic;
using System.Linq;

namespace Project.Core.Utilities.Paging
{
    public class PagedList<T> : List<T>
    {

        //SAYFA DURUMU HAKKINDAKİ BİLGİLER
        public int CurrentPage { get; }
        public int TotalPages { get; }
        public int PageSize { get; }
        public int TotalCount { get; }

        //CURRENTPAGE 1'DEN BÜYÜKSE BİR ÖNCEKİ SAYFA MEVCUTTUR
        public bool HasPrevious => CurrentPage > 1;

        //CURRENTPAGE TOTALPAGE'DEN KÜÇÜKSE, SON SAYFADA DEĞİLDİR
        public bool HasNext => CurrentPage < TotalPages;




        public PagedList(List<T> items, int count, int pageNumber, int pageSize)
        {
            TotalCount = count;
            PageSize = pageSize;
            CurrentPage = pageNumber;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            base.AddRange(items);
        }



        //VERİLEN DATAYI, VERİLEN PARAMETRELERE GÖRE SAYFALA VE DÖN
        public static PagedList<T> ToPagedList(List<T> source, int pageNumber, int pageSize)
        {
            var items = source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            return new PagedList<T>(items, source.Count(), pageNumber, pageSize);
        }
    }
}