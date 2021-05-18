using Project.Core.Business.BusinessResultObjects.Enums;

namespace Project.Core.Business.BusinessResultObjects
{
    public class UnauthorizedDataResult<T> : DataResult<T>
    {
        public UnauthorizedDataResult(T data, string message) : base(data, ResultType.UnAuthorized, message)
        {

        }


        public UnauthorizedDataResult(T data) : base(data, ResultType.UnAuthorized)
        {

        }


        public UnauthorizedDataResult(string message) : base(default, ResultType.UnAuthorized, message)
        {

        }


        public UnauthorizedDataResult() : base(default, ResultType.UnAuthorized)
        {

        }
    }
}