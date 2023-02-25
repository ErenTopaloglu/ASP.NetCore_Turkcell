using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;
using RequiredAttribute = System.ComponentModel.DataAnnotations.RequiredAttribute;

namespace ASP.NetCore_Turkcell.ViewModel
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        [Remote(action:"HasProductName", controller:"Products")]  //aynı isimde ürün var mı yok mu onu kontrol eden hasproductname metodunu çalıştırıyor
        [Required(ErrorMessage = "İsim alanı boş olamaz")]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "5-50 karakter arasında olmalıdır.")]
        public string? Name { get; set; }
        [Required(ErrorMessage = "Fiyat alanı boş olamaz")]
        [Range(1,1000, ErrorMessage = "1-1000 arası bir birim giriniz")]
        /* [RegularExpression(@"^[0-9]+(\.[0-9]{1,2})", ErrorMessage ="Fiyat alanında virgülden sonra 2 basamak olmalıdır.")]*/ // bende olmadı ama özelleştirmede kullanılan regular expr. örnek
        public decimal? Price { get; set; }
        [Required(ErrorMessage = "Stok alanı boş olamaz")]
        [Range( 1, 200, ErrorMessage = "1-200 arası bir birim giriniz")]

        public int? Stock { get; set; }
        [Required(ErrorMessage = "Renk alanı boş olamaz")]
        public string? Color { get; set; }

        public bool IsPublish { get; set; }
        [Required(ErrorMessage = "Yayınlanma süresi boş olamaz")]
        public int? Expire { get; set; }
        [Required(ErrorMessage = "Açıklama alanı boş olamaz")]
        [StringLength(300, MinimumLength = 20, ErrorMessage = "50-300 karakter arasında olmalıdır.")]

        public string Description { get; set; }
        [Required(ErrorMessage = "Yayınlanma tarihi boş olamaz")]
        public DateTime? PublishDate { get; set; }

    }
}
