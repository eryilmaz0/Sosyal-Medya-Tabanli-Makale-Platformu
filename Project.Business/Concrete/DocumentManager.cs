using System;
using System.IO;
using EnumsNET;
using Microsoft.AspNetCore.Http;
using Project.Business.Abstract;
using Project.Business.Messages;
using Project.Core.Business.BusinessResultObjects;
using Project.Core.Business.BusinessResultObjects.Enums;
using Project.Core.Utilities.Business;
using Project.Entities.Enums;

namespace Project.Business.Concrete
{
    public class DocumentManager : IDocumentService
    {


        public IResult UplaodFile(IFormFile file, FileType fileType)
        {
             var filePath = "C:\\Users\\Batuhan\\Desktop\\Couldnt-Find-d82edb0ac8e637f5dc815070df575de332729e36\\apps\\react\\src\\assets";
             var errorResult = BusinessRules.Run(IsFileTypeEnumInRange(fileType));

            if (errorResult != null)
            {
                return errorResult;
            }

            
            try
            {
                string uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                string path = Path.Combine(filePath,"Resources", fileType.ToString(), uniqueFileName);
                file.CopyTo(new FileStream(path, FileMode.Create));

                return new SuccessResult(uniqueFileName);
            }
            catch 
            {
                return new ErrorResult("Dosya Yüklenirken Bir Hata Oluştu.");
            }

        }




        private IResult IsFileTypeEnumInRange(FileType fileType)
        {

            if (!Enum.IsDefined(typeof(FileType), fileType))
            {
                return new ErrorResult();
            }

            return new SuccessResult();
        }
    }
}
