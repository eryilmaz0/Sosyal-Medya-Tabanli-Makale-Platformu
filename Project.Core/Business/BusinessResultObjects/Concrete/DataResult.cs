using Project.Core.Business.BusinessResultObjects.Enums;

namespace Project.Core.Business.BusinessResultObjects
{
    public class DataResult<T> : Result, IDataResult<T>
    {
        public T Data { get; set; }



        public DataResult(T data, ResultType result, string message):base(result,message)
        {
            this.Data = data;
        }



        public DataResult(T data, ResultType result):base(result)
        {
            this.Data = data;
        }
    }
}