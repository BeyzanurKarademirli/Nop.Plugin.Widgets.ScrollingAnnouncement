# nopCommerce Kayan Yazı Duyuru Bandı Eklentisi (Scrolling Announcement Widget)

nopCommerce 4.5x ve 4.90.x sürümleri için geliştirilmiş, ana sayfada CSS `@keyframes` animasyonu ile akıcı kayan duyurular gösteren modüler widget eklentisi.

## 🚀 Özellikler

- **Modüler Mimari:** Core dosyalarında hiçbir değişiklik yapılmadan %100 modüler eklenti yapısı.
- **Pure CSS Animasyon:** `<marquee>` kullanılmadan modern `@@keyframes` ile akıcı kayan yazı animasyonu.
- **Yönetim Paneli (CRUD):** Admin panelinden duyuru metni, HEX renk kodu, gösterim sırası ve Aktif/Pasif durum yönetimi.
- **Veritabanı Migration:** `FluentMigrator` mimarisi ile otomatik veritabanı tablosu oluşturma.
- **Dinamik Sol Menü (SiteMapNode):** Admin paneli sol menüsüne özel "Kayan Yazı Duyuruları" başlığı entegrasyonu.
- **Temiz Yaşam Döngüsü (Lifecycle):** Kurulumda dil kaynakları yüklenir; kaldırıldığında veritabanı ve dil kaynakları otomatik temizlenir.

## 🛠️ Kurulum Adımları

1. `Nop.Plugin.Widgets.ScrollingAnnouncement` projesini nopCommerce `src/Plugins/` dizinine kopyalayın.
2. Projeyi derleyin (`dotnet build`).
3. Admin Paneli -> **Yapılandırma** -> **Yerel Eklentiler** sayfasına gidin.
4. "Kayan Yazı Duyuru Bandı" eklentisinin yanındaki **Kur (Install)** butonuna ve ardından **Uygulamayı Yeniden Başlat** butonuna tıklayın.
5. Admin Paneli -> **Yapılandırma** -> **Gereçler (Widgets)** sayfasından eklentiyi **Aktif** hale getirin.
6. **Kayan Yazı Duyuruları** sayfasından ilk duyurunuzu ekleyin!

---
*Geliştirici: Beyzanur*
