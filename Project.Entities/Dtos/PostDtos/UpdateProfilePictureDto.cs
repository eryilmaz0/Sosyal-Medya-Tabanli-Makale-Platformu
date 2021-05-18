using Project.Core.Entities;

namespace Project.Entities.Dtos
{
    public class UpdateProfilePictureDto : DtoBase
    {
        public string Picture { get; set; } 

        //SONRASINDA GÜNCELLENMEK İSTENEN ALANLAR OLURSA, BURAYA EKLENEBİLİR.
    }
}