USE master;
GO

-- Eðer veritabaný varsa tüm baðlantýlarý zorla kes ve veritabanýný sil
IF EXISTS (SELECT name FROM sys.databases WHERE name = N'SaglikOcagiDB')
BEGIN
    ALTER DATABASE SaglikOcagiDB SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE SaglikOcagiDB;
END
GO

CREATE DATABASE SaglikOcagiDB;
GO

USE SaglikOcagiDB;
GO

-- 1. Poliklinik Tanýtma
CREATE TABLE Poliklinik (
    PoliklinikID INT IDENTITY PRIMARY KEY,
    PoliklinikAd NVARCHAR(50),
    Durum BIT DEFAULT 1
);

-- 2. Kullanýcý Tanýtma
CREATE TABLE Kullanici (
    KullaniciKodu INT IDENTITY PRIMARY KEY,
    KullaniciAd NVARCHAR(50),
    Sifre NVARCHAR(50),
    Yetki BIT DEFAULT 0 
);

-- 3. Hasta Bilgileri
CREATE TABLE Hasta (
    DosyaNo INT PRIMARY KEY,
    Ad NVARCHAR(50),
    Soyad NVARCHAR(50),
    TCKIMLIKNO NVARCHAR(11),
    KurumAdi NVARCHAR(100),
    Telefon NVARCHAR(15),
    Adres NVARCHAR(200),
    DogumTarihi DATE
);

-- 4. Sevk (Hasta Giriþ/Çýkýþ Takibi)
CREATE TABLE Sevk (
    SevkID INT IDENTITY PRIMARY KEY,
    DosyaNo INT FOREIGN KEY REFERENCES Hasta(DosyaNo),
    SevkTarihi DATETIME, 
    PoliklinikID INT FOREIGN KEY REFERENCES Poliklinik(PoliklinikID),
    SiraNo INT, 
    Saat NVARCHAR(10), 
    CikisSaati NVARCHAR(10) NULL, -- Taburcu anýnda yazýlacak 
    Taburcu BIT DEFAULT 0,
    ToplamTutar DECIMAL(18,2) DEFAULT 0 -- Griddeki iþlemlerin toplamý
);

-- 5. Yapýlan Tahlil ve Ýþlemler
CREATE TABLE SevkIslemleri (
    IslemID INT IDENTITY PRIMARY KEY,
    SevkID INT FOREIGN KEY REFERENCES Sevk(SevkID),
    YapilanIslem NVARCHAR(100), 
    DrKodu INT, -- Her doktorun sahip olduðu kod 
    Miktar INT DEFAULT 1, 
    BirimFiyat DECIMAL(18,2), 
    IslemSaati NVARCHAR(10)
);

-- 6. Doktor Tanýtma
CREATE TABLE Doktor (
    DrKodu INT PRIMARY KEY,
    Ad NVARCHAR(50),
    Soyad NVARCHAR(50),
    Unvan NVARCHAR(50)
);

-- BAÞLANGIÇ VERÝLERÝ
INSERT INTO Poliklinik (PoliklinikAd, Durum) VALUES ('Dahiliye', 1), ('Kardiyoloji', 1);
INSERT INTO Kullanici (KullaniciAd, Sifre, Yetki) VALUES ('admin', '1234', 1);
INSERT INTO Doktor (DrKodu, Ad, Soyad, Unvan) VALUES (101, 'Ahmet', 'Yýlmaz', 'Doç. Dr.');
INSERT INTO Hasta (DosyaNo, Ad, Soyad, TCKIMLIKNO, KurumAdi, Telefon, Adres, DogumTarihi) 
VALUES (1, 'Ahmet', 'Derin', '11111111111', 'SGK', '05551234567', 'Ýstanbul Mah. No:1', '1990-01-01');
