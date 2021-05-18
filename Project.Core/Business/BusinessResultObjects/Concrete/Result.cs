using Project.Core.Business.BusinessResultObjects.Enums;

namespace Project.Core.Business.BusinessResultObjects
{
    public class Result : IResult
    {
        public ResultType ResultType { get; set; }
        public string Message { get; set; }



        public Result(ResultType result)
        {
            this.ResultType = result;
        }


        public Result(ResultType result, string message):this(result)
        {
            this.Message = message;
        }


    }
}