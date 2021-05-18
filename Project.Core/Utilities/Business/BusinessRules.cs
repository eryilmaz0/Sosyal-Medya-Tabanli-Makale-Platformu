using System.ComponentModel;
using Project.Core.Business.BusinessResultObjects;
using Project.Core.Business.BusinessResultObjects.Enums;

namespace Project.Core.Utilities.Business
{
    public class BusinessRules
    {

        public static IResult Run(params IResult[] logics)
        {
            foreach (IResult logic in logics)
            {

                if (logic.ResultType != ResultType.Success)
                {
                    return logic;
                }

            }

            return null;
        }

    }
}