using Project.Core.Business.BusinessResultObjects.Enums;

namespace Project.Core.Business.BusinessResultObjects
{
    public interface IResult
    {
         ResultType ResultType { get; set; }
         string Message { get; set; }
    }
}