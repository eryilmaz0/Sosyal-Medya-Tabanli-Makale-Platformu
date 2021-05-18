using Project.Core.Business.BusinessResultObjects;
using Project.Core.Business.BusinessResultObjects.Enums;

namespace Project.Core.Utilities.Business
{
    public class ResultConverter
    {
        
        //VERİLEN IRESULTU, VERİLEN T TİPİNDE GENERİC BİR DATARESULTA ÇEVİR
        public static IDataResult<T> ResultToDataResult<T>(IResult result)
        {

            if (result.ResultType == ResultType.UnAuthorized)
            {
                return ConvertToUnauthorizedDataResult<T>(result);
            }

            else if (result.ResultType == ResultType.Error)
            {
                return  convertToErrorDataResult<T>(result);
            }


            return null;
        }




        private static UnauthorizedDataResult<T> ConvertToUnauthorizedDataResult<T>(IResult result)
        {
            return new UnauthorizedDataResult<T>(result.Message);
        }



        private static ErrorDataResult<T> convertToErrorDataResult<T>(IResult result)
        {
            return new ErrorDataResult<T>(result.Message);
        }

    }
}