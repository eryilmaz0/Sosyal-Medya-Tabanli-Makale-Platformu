# Sosyal Medya Tabanlı Makale Platformu

## Proje Açıklaması

 Projenin amacı, günümüzde yaygın olan makale paylaşım sitelerini geliştirmek, ve sosyal medyalarda yer alan aktif kullanıcı etkileşimi ile destekleyerek aktif bir platform oluşturmaktır.

 Proje üniversite bitirme projesi olarak iki kişilik bir ekip geliştirmektedir. Ben backend tarafından, arkadaşım ise frontend tarafından sorumludur. 

 Proje .Net Core 3.1 sürümü üzerinde geliştirilmiş, ve geliştirilmesi devam etmektedir. Önyüz tarafında React kullanılmıştır. Projede JWT tabanlı kullanıcı doğrulama mekanizması kullanışmış, kullanıcı işlemleri için
 Identity kütüphanesi kullanılmıştır.

 Proje oldukça geniş çaplıdır ve bir çok kütüphane kullanılmıştır. 


 ## Proje Yapıları

 Ana başlıklar halinde bahsedecek olursak, Projenin çekirdek yapısı makale paylaşım platformu olarak tasarlanmıştır. Projeyi diğer makale paylaşım platformlarından ayıran başlıklar şu şekilde sıralanabilir: 

 ### Soru-Cevap Sayfası

 Kullanıcı etkileşimini arttırmak için geliştirilen bu yapı, günümüz forum siteleri mantığında çalışmaktadır. Kullanıcılar konu açabilmekte, ve bu konularda tartışabilmektedir. Projenin içerisinde ayrı bir yapı olarak yer almaktadır.

 ### Özel Mesajlaşma 
Projede kullanıcılar birbirlerine özel olarak mesaj yollayabilmektedir. SignalR kütüphanesi kullanılarak, mesajlaşma sistemi gerçek zamanlı hale getirilmiştir. 


## Projenin Fonksiyonları

- Sisteme Üye Olma
- Sisteme Giriş Yapma
- Şifre Sıfırlama - Değiştirme
- Yazar Yetki Başvurusunda Bulunma
- Kullanıcı Profillerini Görüntüleme
- Paylaşılan Makaleleri Görüntüleme
- Makalelere Like-Disslike Atabilme
- Makaleleri Favorilerine Ekleyebilme
- Makalelere Yorum Yapabilme
- Soru-Cevap Konusu Oluşturma
- Soru-Cevap Konusuna Yorum Yapma
- Kullanıcılara Özel Mesaj Yollayabilme

Ek olarak, yazar yetkisi için başvuran bir kullanıcı, başvurusu kabul edildiği takdirde yazar yetkisine sahip olur. Bu yetki ile birlikte, projede yer alan, yazar yetkisine sahip kullanıcılara özel fonksiyonları gerçekleştirebilir.

Yazar yetkisine sahip olan kullanıcıların gerçekleştirebileceği fonksiyonlar:

- Makale Paylaşma
- Makale Güncelleme
- Makale Silme
- Makaleye Yapılan Yorumları Gizleme


## Projede Kullanılan Kütüphaneler

```
EntityFramework Core
Fluent Validation
Identity
SignalR
Autofac
Ml.Net
Newtonsoft.Json
```

