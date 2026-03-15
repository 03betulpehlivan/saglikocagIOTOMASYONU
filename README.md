🏥 Sağlık Ocağı Otomasyonu

Bu proje, sağlık ocakları ve küçük klinikler için geliştirilmiş bir hasta yönetim otomasyon sistemidir.
Sistem sayesinde hasta kayıtları, doktor atamaları, poliklinik işlemleri ve sevk süreçleri dijital ortamda düzenli ve güvenli şekilde yönetilebilir.

Uygulama C# Windows Forms kullanılarak geliştirilmiş olup veriler Microsoft SQL Server üzerinde saklanmaktadır.

🚀 Proje Özellikleri
👤 Hasta Yönetimi

Yeni hasta kaydı oluşturma

Mevcut hasta bilgilerini görüntüleme

Hasta bilgilerinin güncellenmesi

Hasta geçmiş kayıtlarının takip edilmesi

👨‍⚕️ Doktor ve Poliklinik Yönetimi

Polikliniklere göre doktor listeleme

Doktor arama ve filtreleme

Hastaya doktor atama işlemleri

🔄 Sevk İşlemleri

Hastaların ilgili polikliniklere sevk edilmesi

Sevk edilen işlemlerin kayıt altına alınması

Yapılan işlemlere ait ücret/tutar bilgilerinin görüntülenmesi

🗄️ Veritabanı Yönetimi

Tüm verilerin güvenli şekilde saklanması

SQL Server üzerinden hızlı veri erişimi

İlişkisel veritabanı yapısı ile veri bütünlüğünün korunması

🛠️ Kullanılan Teknolojiler
Teknoloji	Açıklama
C#	Uygulamanın ana programlama dili
Windows Forms (.NET Framework)	Masaüstü kullanıcı arayüzü geliştirme
Microsoft SQL Server	Veritabanı yönetimi
ADO.NET	Uygulama ile veritabanı arasındaki veri erişim katmanı
Visual Studio	Geliştirme ortamı
🧩 Sistem Mimarisi

Uygulama katmanlı bir yapı mantığıyla geliştirilmiştir:

Presentation Layer (UI) → Windows Forms arayüzü

Data Access Layer → ADO.NET ile veritabanı bağlantıları

Database Layer → SQL Server üzerinde hasta, doktor ve işlem tabloları

Bu yapı sayesinde:

veri erişimi düzenli hale getirilmiş

uygulamanın bakım ve geliştirilmesi kolaylaştırılmıştır.

📂 Veritabanı Yapısı

Veritabanı aşağıdaki temel tablolar üzerine kurulmuştur:

Hastalar

Doktorlar

Poliklinikler

Sevkler

İşlemler

Bu tablolar arasında ilişkiler kurularak hasta-doktor-poliklinik yönetimi sağlanmaktadır.

💻 Kurulum ve Çalıştırma

Projeyi kendi bilgisayarınızda çalıştırmak için aşağıdaki adımları izleyebilirsiniz:

1️⃣ Projeyi klonlayın
git clone https://github.com/03betulpehlivan/saglikocagIOTOMASYONU.git
2️⃣ Veritabanını oluşturun

SQL Server Management Studio (SSMS) uygulamasını açın

Proje klasöründe bulunan SQL komut dosyasını (.sql) açın

Komutları çalıştırarak gerekli tabloları oluşturun (Execute)

3️⃣ Projeyi Visual Studio’da açın
saglikocagIOTOMASYONU.sln

dosyasını Visual Studio ile açın.

4️⃣ Veritabanı bağlantısını düzenleyin

VeriBaglantisi.cs dosyasını açın ve aşağıdaki connection string kısmını kendi SQL Server sunucu adınıza göre güncelleyin.

Örnek:

SqlConnection baglanti = new SqlConnection(
"Server=YOUR_SERVER_NAME;Database=DatabaseName;Trusted_Connection=True;"
);
5️⃣ Uygulamayı çalıştırın

Visual Studio üzerinden projeyi derleyip çalıştırabilirsiniz:

F5  → Run
🎯 Projenin Amacı

Bu proje, sağlık kurumlarında kullanılan hasta takip sistemlerinin temel mantığını modellemek amacıyla geliştirilmiştir.
Amaç, masaüstü uygulama geliştirme, veritabanı entegrasyonu ve veri yönetimi konularında pratik bir çözüm üretmektir.
<!-- Uploading "Ekran görüntüsü 2026-03-15 180310.png"... -->

<img width="887" height="147" alt="Image" src="https://github.com/user-attachments/assets/abd392f9-4336-45b7-bbb4-0c9de911be33" />

<img width="1356" height="914" alt="Image" src="https://github.com/user-attachments/assets/4eb086ea-ac74-4e21-96e0-b3984a49c4c9" />
