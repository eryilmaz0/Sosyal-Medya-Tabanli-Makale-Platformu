using Project.Core.Business.BusinessResultObjects.Enums;

namespace Project.Core.Business.BusinessResultObjects
{
    public class SuccessResult : Result
    {


        public SuccessResult():base(ResultType.Success)
        {
            
        }


        public SuccessResult(string message):base(ResultType.Success,message)
        {
            
        }
    }
}