using Project.Core.Utilities.Paging;

namespace Project.Core.Utilities.Response
{
    
    public class PagedDataResponse<T> : Response 
    {

        //VERİLEN T TİPİNDE BİR PAGEDLİST TUTUYOR
        public PagedList<T> Data { get;}


        //PAGEDLİST HAKKINDAKİ BİLGİLER
        //CTORDA SETLENEBİLİR
        public int CurrentPage { get;  }
        public int TotalPages { get;  }
        public int PageSize { get;  }
        public int TotalCount { get;  }
        public bool HasPrevious { get; }
        public bool HasNext { get;  }



        //VERİLEN T TİPİNİ TUTAN PAGEDLİST İSTİYOR
        public PagedDataResponse(PagedList<T> data)
        {
            Data = data;
            CurrentPage = data.CurrentPage;
            TotalPages = data.TotalPages;
            PageSize = data.PageSize;
            TotalCount = data.TotalCount;
            HasPrevious = data.HasPrevious;
            HasNext = data.HasNext;
        }

    }
}