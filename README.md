
# Halı Saha Otomasyonu

Bu proje, halı saha işletmelerinin rezervasyonlarını ve operasyonlarını yönetmelerine yardımcı olmak için tasarlanmış bir otomasyon sistemidir. C# dilinde yazılmıştır ve kullanıcı dostu bir arayüze sahiptir. 


## Özellikler

- Rezervasyon Yönetimi: Kullanıcılar, mevcut zaman dilimlerini görüntüleyebilir ve rezervasyon yapabilirler.
- Müşteri Yönetimi: Müşteri bilgileri kolayca saklanabilir ve yönetilebilir.
- Raporlama: İşletmeler, rezervasyon ve müşteri verilerini analiz edebilir.

  
## Kullanılan Teknolojiler

**İstemci:** .Net, C#, Bunifu Framework

**Sunucu:** SQL, MSSQL

  ## Bilgisayarınızda Çalıştırma

Bu adımlar, projeyi yerel makinenizde çalıştırmanız için gereklidir:

1. **Veri Tabanı Hazırlığı**: 'VeriTabanı' klasöründeki veri tabanı dosyalarını SQL'in 'DATA' klasörüne kopyalayın. (Not: Bu işlemi yaparken SQL Server'ın kapalı olduğundan emin olun.)

2. **SQL Server'ı Yeniden Başlatın**: Kopyalama işlemi tamamlandıktan sonra SQL Server'ı yeniden başlatın ve SQL Server Management Studio'ya girerek 'database' kısmını yenileyin.

3. **Database Bağlantısını Kontrol Edin**: Eğer projeyi çalıştırdığınızda SQL hatası alıyorsanız, bu genellikle SQL'e bağlandığınız bilgisayar isminin proje database yolunda görünmemesinden kaynaklanır. Bu sorunu çözmek için, proje dosyaları içerisindeki database bağlantılarında bulunan "Source=." kısmındaki noktayı, SQL Management Studio'ya bağlandığınız isim ile değiştirin.

4. **Bunifu Framework Üyeliği**: Projedeki form ekranlarının görünmesi için Bunifu Framework'ten üyelik almanız gerekmektedir. (14 gün ücretsiz deneme süresi mevcuttur.) Üyelik sonrası mail adresinize gönderilen lisans anahtarı ile form ekranlarını dilediğiniz gibi kontrol edebilirsiniz.

## Projeden Görseller
![Uygulama Ekran Görüntüsü](https://github.com/yusufaytasss/Hali-Saha-Otomasyonu/blob/main/GirisEkrani.png)
![Uygulama Ekran Görüntüsü](https://github.com/yusufaytasss/Hali-Saha-Otomasyonu/blob/main/KarsilamaEkrani.png)
![Uygulama Ekran Görüntüsü](https://github.com/yusufaytasss/Hali-Saha-Otomasyonu/blob/main/%C4%B0statistik.png)
![Uygulama Ekran Görüntüsü](https://github.com/yusufaytasss/Hali-Saha-Otomasyonu/blob/main/Giris_Tablo.png)
![Uygulama Ekran Görüntüsü](https://github.com/yusufaytasss/Hali-Saha-Otomasyonu/blob/main/Tablo_Otomasyon.png)
