# Sağlık Ocağı Otomasyonu 🏥

Bu proje, sağlık ocakları ve klinikler için geliştirilmiş; hasta kayıt, doktor atama, poliklinik işlemleri ve sevk süreçlerini yönetmeyi sağlayan bir masaüstü otomasyon sistemidir. 

## 🚀 Özellikler
* **Hasta Yönetimi:** Yeni hasta kaydı oluşturma, hasta geçmişi görüntüleme ve bilgi güncelleme.
* **Poliklinik ve Doktor İşlemleri:** Polikliniklere göre doktor listeleme, arama ve randevu/sevk yönetimi.
* **Sevk İşlemleri:** Hastaların ilgili birimlere sevkinin yapılması ve işlem tutarlarının hesaplanması.
* **Veritabanı Entegrasyonu:** Tüm verilerin güvenli bir şekilde tutulması ve hızlı veri erişimi.

## 🛠️ Kullanılan Teknolojiler
* **Programlama Dili:** C#
* **Arayüz:** Windows Forms (.NET Framework)
**Geliştirme Ortamı:** Visual Studio 
* **Veritabanı:** Microsoft SQL Server
* **Veri Erişim Teknolojisi:** ADO.NET

## 💻 Kurulum ve Çalıştırma

Projeyi kendi bilgisayarınızda çalıştırmak için aşağıdaki adımları izleyebilirsiniz:

1. Bu depoyu bilgisayarınıza klonlayın:
   ```bash
   git clone [https://github.com/03betulpehlivan/saglikocagIOTOMASYONU.git](https://github.com/03betulpehlivan/saglikocagIOTOMASYONU.git)
   SQL Server Management Studio (SSMS) uygulamasını açın.

2.Proje dizininde bulunan veritabanı script dosyasını SSMS üzerinde çalıştırarak (Execute) gerekli tabloları oluşturun.

3.Projeyi Visual Studio ile açın (saglikocagIOTOMASYONU.sln ).

4.VeriBaglantisi.cs dosyası içerisindeki "Connection String" (Bağlantı Cümlesi) kısmını kendi lokal SQL Server sunucu adınıza göre güncelleyin.

5.Projeyi derleyip çalıştırın (Start / F5).

<!-- Uploading "Ekran görüntüsü 2026-03-15 180310.png"... -->

<img width="887" height="147" alt="Image" src="https://github.com/user-attachments/assets/abd392f9-4336-45b7-bbb4-0c9de911be33" />

<img width="1356" height="914" alt="Image" src="https://github.com/user-attachments/assets/4eb086ea-ac74-4e21-96e0-b3984a49c4c9" />
