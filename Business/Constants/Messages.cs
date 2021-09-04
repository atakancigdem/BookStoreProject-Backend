using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Constants
{
    public static class Messages
    {
        //Book
        internal static string BooksListed = "Kitaplar listelendi";
        internal static string BooksListedByName = "Aradığınız kitap bulundu";
        internal static string BookFoundById = "Aradığınız Id ile eşleşen kitap bulundu";
        internal static string BookDetails = "Kitapların detayları görüntülendi";
        internal static string BookListedByCategory = "Aradığınız Kategori Id ile eşleşen kitaplar bulundu";
        internal static string BookListedBySubheading = "Aradığınız Alt başlık Id ile eşleşen kitaplar bulundu";
        internal static string BookListedBySOS = "Aradığınız Alt başlığın alt başlığı Id ile eşleşen kitaplar bulundu";
        internal static string BookListedByAuthor = "Aradığınız Yazar Id ile eşleşen kitaplar bulundu";
        internal static string BookListedByPublisher = "Aradığınız Yayıncı Id ile eşleşen kitaplar bulundu";
        internal static string BookListedByLanguage = "Aradığınız Dil Id ile eşleşen kitaplar bulundu";
        internal static string BookListedByPrice = "Giriş yaptığınız fiyat aralığındaki kitaplar bulundu";
        internal static string BookAdded = "Kitap eklendi";
        internal static string BookUpdated = "Kitap güncellendi";
        internal static string BookDelete = "Kitap silindi";

        //Author
        internal static string AuthorsListed = "Yazarlar listelendi";
        internal static string AuthorFoundById = "Aradığınız Id ile eşleşen yazar bulundu";
        internal static string AuthorFoundByName = "Aradığınız yazar bulundu";
        internal static string AuthorAdded = "Yazar eklendi";
        internal static string AuthorUpdate = "Yazar güncellendi";
        internal static string AuthorDelete = "Yazar silindi";

        //Category
        internal static string CategoriesListed = "Kategoriler listelendi";
        internal static string CategoryFoundById = "Aradığınız Id ile eşleşen kategori bulundu";
        internal static string CategoryFoundByName = "Aradığınız kategori bulundu";
        internal static string CategoryAdded = "Kategori eklendi";
        internal static string CategoryUpdate = "Kategori güncellendi";
        internal static string CategoryDelete = "Kategori silindi";

        //Customer
        internal static string CustomersListed = "Müşteriler listelendi";
        internal static string CustomerListedByUserId = "Kullanıcı Id ile eşleşen müşteriler listelendi";
        internal static string CustomerFoundById = "Aradığınız Id ile eşleşen müşteri bulundu";
        internal static string CustomerListedByCompanyName = "Aradığınız şirket isimi ile eşleşen müşteriler bulundu";
        internal static string CustomerAdded = "Müşteri eklendi";
        internal static string CustomerUpdate = "Müşteri güncellendi";
        internal static string CustomerDelete = "Müşteri silindi";

        //Language
        internal static string LanguagesListed = "Diller listelendi";
        internal static string LanguageFoundById = "Aradığınız Id ile eşleşen dil bulundu";
        internal static string LanguageFoundByName = "Aradığınız dil bulundu";
        internal static string LanguageAdded = "Dil eklendi";
        internal static string LanguageUpdate = "Dil güncellendi";
        internal static string LanguageDelete = "Dil silindi";

        //Publisher
        internal static string PublishersListed = "Yayıncılar listelendi";
        internal static string PublisherFoundById = "Aradığınız Id ile eşleşen yayıncı bulundu";
        internal static string PublisherFoundByName = "Aradığınız yayın bulundu";
        internal static string PublisherAdded = "Yayın eklendi";
        internal static string PublisherUpdate = "Yayın güncellendi";
        internal static string PublisherDelete = "Yayın silindi";

        //SubheadingOfSubheading
        internal static string SOSsListed = "Listelendi";
        internal static string SOSDetail = "Detaylar görünütlendi";
        internal static string SOSFoundByCategoryId = "Aradığınız Kategori Id ile eşleşenler görüntülendi";
        internal static string SOSFoundBySubheadingId = "Aradığınız Alt Başlık Id ile eşleşenler görüntülendi";
        internal static string SOSFoundById = "Aradığınız Id ile eşleşen bulundu";
        internal static string SOSFoundByName = "Aradığınız bulundu";
        internal static string SOSAdded = "Eklendi";
        internal static string SOSUpdate = "Güncellendi";
        internal static string SOSDelete = "Silindi";

        //Subheading
        internal static string SubheadingsListed = "Alt Başlıklar listelendi";
        internal static string SubheadingFoundById = "Aradığınız Id ile eşleşen Alt Başlık bulundu";
        internal static string SubheadingFoundByName = "Aradığınız Alt başlık bulundu";
        internal static string SubheadingListedByCategory = "Aradığınız Kategori Id ile eşleşen Alt başlıklar bulundu";
        internal static string SubheadingDetail = "Alt başlıkların detayları görüntülendi";
        internal static string SubheadingAdded = "Alt başlık eklendi";
        internal static string SubheadingUpdate = "Alt başlık güncellendi";
        internal static string SubheadingDelete = "Alt başlık silindi";

        //BookImage
        internal static string OverflowBookImageMessage = "Kitabın 5 ten fazla resmi olamaz";
        internal static string BookImageAdded = "Kitap resmi başarı ile eklendi";
        internal static string BookImageNotFound = "Resim bulunamadı";
        internal static string BookImageDeleted = "Kitap resmi silindi";
        internal static string BookImagesListed = "Kitapların resimleri listelendi";
        internal static string BookImageListed = "Kitabın resmi görüntülendi";
        internal static string BookImageUpdated = "Kitabın resmi güncellendi";
        internal static string BookImageLimitExceeded = "Fotoğraf yükleme limitini aştınız (=5)";


        //User
        internal static string UserRegistered = "Kullanıcı başarıyla oluşturuldu.";
        internal static string UsersListed = "Kullanıcılar listelendi";
        internal static string UserNotFound = "Kullanıcı Bulunamadı.";
        internal static string UserAlreadyExists = "Bu kullanıcı zaten mevcut";
        internal static string PasswordError = "Şifre Hatalı";
        internal static string SuccessfulLogin = "Sisteme giriş başarılı";

        //Auth
        internal static string AuthorizationDenied = "Yetkiniz yok.";
        internal static string AccessTokenCreated = "Access token başarıyla oluşturuldu.";
    }
}
