# nopCommerce Kayan Yazı Duyuru Bandı Eklentisi (Scrolling Announcement Widget)

nopCommerce **4.5.x** (`net6.0`) sürümü için geliştirilmiş, ana sayfada CSS `@keyframes` animasyonu ile akıcı kayan duyurular gösteren modüler widget eklentisi.

> **Önemli:** Bu eklenti `net6.0` hedefler ve **nopCommerce 4.50** kaynak kodunda derlenip test edilmelidir. 4.90 (`net9.0`) solution içinde doğrudan derlenemez.

## Özellikler

- **Modüler mimari:** Core dosyalarında değişiklik yok.
- **Pure CSS animasyon:** `<marquee>` yerine `@keyframes` kullanımı.
- **Admin CRUD:** Metin, renk, gösterim sırası, aktif/pasif yönetimi.
- **FluentMigrator:** Kurulumda tablo oluşturma, kaldırmada otomatik silme.
- **SiteMapNode:** Admin sol menüde kendi yapılandırma sayfası.
- **DI:** `IAnnouncementService` ve core servisler üzerinden bağımlılık enjeksiyonu.

## Kurulum

1. Projeyi nopCommerce 4.50 `src/Plugins/` dizinine kopyalayın.
2. `dotnet build` ile derleyin.
3. Admin → **Yapılandırma** → **Yerel Eklentiler** → **Kur** → **Uygulamayı Yeniden Başlat**.
4. **Kayan Yazı Duyuruları** menüsünden duyuru ekleyin.

---
*Geliştirici: Beyzanur*
