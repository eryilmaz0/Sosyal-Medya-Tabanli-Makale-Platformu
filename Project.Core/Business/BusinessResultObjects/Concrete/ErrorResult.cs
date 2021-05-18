using Project.Core.Business.BusinessResultObjects.Enums;

namespace Project.Core.Business.BusinessResultObjects
{
    public class ErrorResult : Result
    {


        public ErrorResult():base(ResultType.Error)
        {
            
        }


        public ErrorResult(string message):base(ResultType.Error,message)
        {
            
        }
    }
}