using Project.Core.Business.BusinessResultObjects.Enums;

namespace Project.Core.Business.BusinessResultObjects
{
    public class SuccessDataResult<T> : DataResult<T>
    {

      
        public SuccessDataResult(T data, string message):base(data,ResultType.Success,message)
        {
            
        }


        
        public SuccessDataResult(T data):base(data,ResultType.Success)
        {
            
        }


       
        public SuccessDataResult(string message):base(default,ResultType.Success,message)
        {
            
        }


        
        public SuccessDataResult():base(default,ResultType.Success)
        {
            
        }

    }
}