namespace Project.Core.Business.BusinessResultObjects
{
    public interface IDataResult<T>: IResult
    {
         T Data { get; set; }
    }
}