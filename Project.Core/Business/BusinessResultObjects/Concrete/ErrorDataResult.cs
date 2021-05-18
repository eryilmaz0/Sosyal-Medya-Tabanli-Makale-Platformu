using Project.Core.Business.BusinessResultObjects.Enums;

namespace Project.Core.Business.BusinessResultObjects
{
    public class ErrorDataResult<T> : DataResult<T>
    {


        public ErrorDataResult(T data, string message):base(data,ResultType.Error,message)
        {
            
        }


        public ErrorDataResult(T data):base(data,ResultType.Error)
        {
            
        }


        public ErrorDataResult(string message):base(default,ResultType.Error,message)
        {
            
        }


        public ErrorDataResult():base(default,ResultType.Error)
        {
            
        }
    }
}