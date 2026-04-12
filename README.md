Health Center Automation System

This project is a patient management automation system developed for health centers and small clinics. It enables the structured and secure digital management of patient records, doctor assignments, clinic operations, and referral processes.

The application was developed using C# Windows Forms, and data is stored in Microsoft SQL Server.

🚀 Project Features
👤 Patient Management
Create new patient records
View existing patient information
Update patient data
Track patient medical history
👨‍⚕️ Doctor and Clinic Management
List doctors by clinic
Search and filter doctors
Assign doctors to patients
🔄 Referral Processes
Refer patients to relevant clinics
Record referral operations
View associated fees and transaction details
🗄️ Database Management
Secure data storage for all records
Fast data access via SQL Server
Ensured data integrity using relational database structure
🛠️ Technologies Used
Technology	Description
C#	Main programming language
Windows Forms (.NET Framework)	Desktop UI development
Microsoft SQL Server	Database management
ADO.NET	Data access layer between application and database
Visual Studio	Development environment
🧩 System Architecture

The application is built using a layered architecture approach:

Presentation Layer (UI): Windows Forms interface
Data Access Layer: Database connections using ADO.NET
Database Layer: SQL Server tables for patients, doctors, and operations

This architecture ensures:

Structured data access
Easier maintenance and development
Better scalability
📂 Database Structure

The database is built on the following core tables:

Patients
Doctors
Clinics
Referrals
Transactions

Relationships between these tables ensure proper patient–doctor–clinic management.

💻 Installation and Setup

Follow these steps to run the project locally:

1️⃣ Clone the project
git clone https://github.com/03betulpehlivan/saglikocagIOTOMASYONU.git
2️⃣ Create the database
Open SQL Server Management Studio (SSMS)
Open the .sql script file located in the project folder
Execute the script to create all required tables
3️⃣ Open the project in Visual Studio

Open:

saglikocagIOTOMASYONU.sln
4️⃣ Configure database connection

Open VeriBaglantisi.cs and update the connection string according to your SQL Server configuration:

SqlConnection baglanti = new SqlConnection(
"Server=YOUR_SERVER_NAME;Database=DatabaseName;Trusted_Connection=True;"
);
5️⃣ Run the application

Build and run the project in Visual Studio:

Press F5 (Run)
🎯 Project Purpose

This project was developed to model the basic structure of patient tracking systems used in healthcare institutions. The main goal is to provide practical experience in desktop application development, database integration, and data management.


<img width="887" height="147" alt="Image" src="https://github.com/user-attachments/assets/abd392f9-4336-45b7-bbb4-0c9de911be33" />

<img width="1356" height="914" alt="Image" src="https://github.com/user-attachments/assets/4eb086ea-ac74-4e21-96e0-b3984a49c4c9" />



TÜRKÇE*****


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
