using System;
using Microsoft.AspNetCore.Http;
using Project.Core.Business.BusinessResultObjects;
using Project.Entities.Enums;

namespace Project.Business.Abstract
{
    public interface IDocumentService
    {
        IResult UplaodFile(IFormFile file, FileType fileType);
    }
}