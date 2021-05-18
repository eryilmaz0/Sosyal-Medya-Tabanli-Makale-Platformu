using System.Net.NetworkInformation;

namespace Project.Business.Messages
{
    public static class Message
    {

    public static string DefaultError = "Bir Hata Oluştu.";
    public static string DefaultSuccess = "İşlem Tamamlandı.";

    public static string PagingParamsNotValid = "Sayfalama Parametreleri Doğrulanamadı.";

    public static string UndefinedFileTypeEnum = "Belge Tipi Geçersiz.";


    public static string UserNotFound = "Kullanıcı Bulunamadı.";
    public static string ReceiverNotFound = "Hedef Kullanıcı Bulunamadı.";
    public static string UserUpdated = "Kullanıcı Düzenlendi.";
    public static string ProfilePictureUpdated = "Profil Resmi Güncellendi.";
    public static string UserIsNotInRole = "Kullanıcı Bu Role Sahip Değil.";


    public static string ArticleNotFound = "Makale Bulunamadı.";
    public static string ArticleLimitCanNotBeZero = "Makale Limiti 0 Olamaz.";
    public static string ArticleCreated = "Makale Oluşturuldu.";
    public static string ArticleUpdated = "Makale Düzenlendi.";
    public static string CantDeleteArticle = "Size Ait Olmayan Bir Makaleyi Silemezsiniz.";
    public static string ArticleDeleted = "Makale Silindi.";
    public static string CantEditArticle = "Size Ait Olmayan Bir Makaleyi Düzenleyemezsiniz.";
    public static string ArticleCategoryNotFound = "Makale Kategorisi Bulunamadı.";
    public static string CantProcessOnArticle = "Size Ait Olmayan Makale Üzerinde İşlem Yapamazsınız.";


    public static string ArticleCommentNotFound = "Makale Yorumu Bulunamadı.";
    public static string ArticleCommentCreated = "Makale Yorumu Eklendi.";
    public static string CantEditArticleComment = "Size Ait Olmayan Bir Yorumu Düzenleyemezsiniz.";
    public static string ArticleCommentUpdated = "Yorum Düzenlendi.";
    public static string CantDeleteArticleComment = "Size Ait Olmayan Bir Yorumu Silemezsiniz.";
    public static string ArticleCommentDeleted = "Yorum Silindi.";
    public static string CantProcessOnArticleComment = "Size Ait Olmayan Yorum Üzerinde İşlem Yapamazsınız.";



    public static string ThereIsWaitingAppeal = "Zaten Beklemede Olan Bir Başvurunuz Bulunmaktadır.";
    public static string AppealCreated = "Başvuruz Oluşturuldu.";
    public static string AppealNotFound = "Başvuru Bulunamadı.";
    public static string ThereIsConfirmedAppeal = "Yazar Yetkisine Sahipsiniz. Başvuru Yapamazsınız.";
    public static string CantProcessOnAppeal = "Size Ait Olmayan Başvuru Üzerinde İşlem Yapamazsınız.";




    public static string ArticleInFavoritesAlready = "Makale Zaten Favorilerinizde Bulunuyor.";
    public static string FavoriteCreated = "Makale Favori Olarak Eklendi.";
    public static string FavoriteNotFound = "Favori Makale Bulunamadı.";
    public static string FavoriteRemoved = "Favori Makale Silindi.";
    public static string CantDeleteFavorite = "Size Ait Olmayan Bir Favoriyi Silemezsiniz.";
    public static string CantProcessOnFavorite = "Size Ait Olmayan Bir Favori Üzerinde İşlem Yapamazsınız.";


    public static string TopicNotFound = "Soru-Cevap Konusu Bulunamadı.";
    public static string TopicCreated = "Soru-Cevap Konusu Oluşturuldu.";
    public static string TopicUpdated = "Soru-Cevap Konusu Güncellendi.";
    public static string TopicDeleted = "Soru-Cevap Konusu Silindi.";
    public static string CantDeleteTopic = "Size Ait Olmayan Bir Soru-Cevap Konusunu Silemezsiniz.";
    public static string CantProcessOnTopic = "Size Ait Olmayan Soru-Cevap Konusu Üzerinde İşlem Yapamazsınız.";



    public static string TopicCommentNotFound = "Yorum Bulunamadı.";
    public static string TopicCommentCreated = "Yorum Oluşturuldu.";
    public static string TopicCommentUpdated = "Yorum Güncellendi.";
    public static string TopicCommentRemoved = "Yorum Silindi.";
    public static string CantUpdateTopicComment = "Size Ait Olmayan Bir Yorumu Güncelleyemezsiniz.";
    public static string CantDeleteTopicComment = "Size Ait Olmayan Bir Yorumu Silemezsiniz.";



    public static string ProfilePictureCantBeEmpty = "Profil Resmi Boş Olamaz.";
    public static string ArticleCoverPhotoCantBeEmpty = "Makale Resmi Boş Olamaz.";



    public static string ChatNotFound = "Konuşma Bulunamadı.";
    public static string ChatAndCommentCreated = "Konuşma Oluşturuldu, Mesaj Gönderildi.";
    public static string ChatCommentCreated = "Mesaj Gönderildi.";
   
  }
}
