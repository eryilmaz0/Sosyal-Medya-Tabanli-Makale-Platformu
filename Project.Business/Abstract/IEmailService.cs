using Project.Core.Business.BusinessResultObjects;

namespace Project.Business.Abstract
{
    public interface IEmailService
    {
        IResult SendConfirmAccountEmail(string email, string link);
        IResult SendPasswordResetEmail(string email, string link);
    }
}