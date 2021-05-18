using Project.Core.Business.BusinessResultObjects.Enums;

namespace Project.Core.Business.BusinessResultObjects
{
    public class UnauthorizedResult :Result, IResult
    {
        public UnauthorizedResult():base(ResultType.UnAuthorized)
        {
            
        }


        public UnauthorizedResult(string message):base(ResultType.UnAuthorized,message)
        {
            
        }
    }
}