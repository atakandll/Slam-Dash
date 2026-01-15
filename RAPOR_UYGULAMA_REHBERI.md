# SLAM DASH PROJE RAPORU - HIZLI UYGULAMA REHBERİ

## Genel Bilgiler

Ana rapor dosyası: `PROJE_RAPORU.md`

Bu dosyayı Word formatına aktarmanız ve aşağıdaki adımları izlemeniz gerekmektedir.

## Adım 1: Word Dosyasını Oluşturma

1. Microsoft Word'de yeni bir doküman açın
2. PROJE_RAPORU.md dosyasının içeriğini kopyalayın
3. Word'e yapıştırın

## Adım 2: Formatlamayı Düzenleme

### Temel Format
- **Yazı Tipi:** Times New Roman
- **Font Boyutu:** 12 punto (kod blokları hariç)
- **Satır Aralığı:** 1.5
- **Kenar Boşlukları:** 2.5 cm (her kenar)

### Başlıklar
- **Ana Başlıklar (1., 2., 3. vb.):** Bold, 14 punto
- **Alt Başlıklar (3.1., 4.2. vb.):** Bold, 12 punto

### Kod Blokları
- **Yazı Tipi:** Consolas veya Courier New
- **Font Boyutu:** 9-10 punto
- **Arka Plan:** Gri (#F0F0F0)

### Şekil Yazıları
- **Format:** Arial, 10 punto, italik
- **Pozisyon:** Şeklin altında, ortalı
- **Örnek:** "Şekil 1: PlayerMovementController - Recursive Hareket Algoritması"

## Adım 3: Kod Ekran Görüntülerini Ekleme

Raporun "Ekler" bölümünde detaylı olarak belirtilmiş 8 kod ekran görüntüsü eklemeniz gerekmektedir:

### Şekil 1: PlayerMovementController - Recursive Hareket Algoritması
- **Dosya:** `Assets/Scripts/Runtime/Controllers/Player/PlayerMovementController.cs`
- **Satırlar:** 39-61
- **İçerik:** MoveOnceRecursive fonksiyonunun tamamı
- **Nereye:** Bölüm 4.2 - Ana Oyun Mekanizması (Sayfa ~10)

### Şekil 2: PlayerPhysicController - Duvar Kontrol Sistemi
- **Dosya:** `Assets/Scripts/Runtime/Controllers/Player/PlayerPhysicController.cs`
- **Satırlar:** 14-19
- **İçerik:** CheckForWall metodu
- **Nereye:** Bölüm 4.2 - Ana Oyun Mekanizması (Sayfa ~11)

### Şekil 3: InputManager - Klavye Giriş Kontrolü
- **Dosya:** `Assets/Scripts/Runtime/Managers/InputManager.cs`
- **Satırlar:** 64-80
- **İçerik:** Update metodundaki input handling
- **Nereye:** Bölüm 4.3 - Input System (Sayfa ~11)

### Şekil 4: LevelManager ve LevelLoaderCommand - Seviye Yükleme
- **Dosya A:** `Assets/Scripts/Runtime/Managers/LevelManager.cs` (satır 47-51)
- **Dosya B:** `Assets/Scripts/Runtime/Commands/Level/LevelLoaderCommand.cs` (satır 18-28)
- **İçerik:** SubscribeEvents ve Execute metodları
- **Nereye:** Bölüm 4.4 - Seviye Yönetim Sistemi (Sayfa ~12)

### Şekil 5: Signal System - MonoSingleton ve Signal Tanımlamaları
- **Dosya A:** `Assets/Scripts/Runtime/Extensions/MonoSingleton.cs` (tamamı, 1-34)
- **Dosya B:** `Assets/Scripts/Runtime/Signals/CoreGameSignals.cs` (tamamı, 1-12)
- **İçerik:** MonoSingleton pattern ve Signal implementasyonu
- **Nereye:** Bölüm 4.5 - Signal System İmplementasyonu (Sayfa ~13)

### Şekil 6: UIManager - Panel Yönetim Sistemi
- **Dosya:** `Assets/Scripts/Runtime/Managers/UIManager.cs`
- **Satırlar:** 57-58
- **İçerik:** OnOpenPanel ve OnClosePanel metodları
- **Nereye:** Bölüm 4.6 - UI Yönetim Sistemi (Sayfa ~13)

### Şekil 7: Slam Dash Oyun Akış Diyagramı
- **İçerik:** Raporun 4.7 bölümünde verilen akış diyagramı text'i
- **Nasıl:** Draw.io, Lucidchart, veya Word'ün SmartArt özelliği ile flowchart çizin
- **Nereye:** Bölüm 4.7 - Akış Diyagramı (Sayfa ~14-15)

### Şekil 8: PlayerManager - Event Subscription Pattern
- **Dosya:** `Assets/Scripts/Runtime/Managers/PlayerManager.cs`
- **Satırlar:** 33-41
- **İçerik:** OnEnable ve SubscribeEvents metodları
- **Nereye:** Bölüm 4.8 - Sistem Bileşenleri Arası İletişim (Sayfa ~16)

## Adım 4: Kod Ekran Görüntüsü Alma İpuçları

### Visual Studio / VS Code'dan Screenshot Alma
1. İlgili .cs dosyasını açın
2. Belirtilen satır numaralarına gidin
3. Satır numaralarının görünür olduğundan emin olun (VS: View -> Line Numbers)
4. İlgili kodu ekrana sığdırın
5. Windows: Win+Shift+S veya Snipping Tool
6. Mac: Cmd+Shift+4
7. Görüntüyü kırpın ve Word'e ekleyin

### Screenshot Kalitesi
- **Çözünürlük:** En az 1920x1080 ekrandan alın
- **Okunabilirlik:** Kod net okunabilir olmalı
- **Tema:** Light theme tercih edin (daha iyi basılır)
- **Zoom:** %100-125 arası (çok küçük veya büyük olmasın)

## Adım 5: Akış Diyagramını Çizme

Raporun 4.7 bölümünde verilen text-based akış diyagramını görsel flowchart'a dönüştürün:

### Araçlar
- **Önerilen:** Draw.io (ücretsiz, web-based)
- **Alternatifler:** Lucidchart, Microsoft Visio, Word SmartArt

### Diyagram Elemanları
- **Oval:** Başla/Bitir
- **Dikdörtgen:** İşlem/Süreç
- **Baklava:** Karar/Koşul
- **Ok:** Akış yönü

### Renkler
- Başla/Bitir: Yeşil
- Normal İşlemler: Mavi
- Kararlar: Turuncu
- Recursive Çağrı: Kırmızı

## Adım 6: Son Kontroller

### İçerik Kontrolü
- [ ] Özet en az 200 kelime
- [ ] Sonuç en az 250 kelime
- [ ] 11 akademik kaynak var
- [ ] 8 şekil eklendi
- [ ] Her şekle metin içinde referans verildi

### Format Kontrolü
- [ ] Times New Roman 12 punto
- [ ] Satır aralığı 1.5
- [ ] Kenar boşlukları 2.5 cm
- [ ] Sayfa numaraları eklendi (alt bilgi, ortalı)
- [ ] Başlıklar bold
- [ ] Şekil yazıları italik ve ortalı

### Kalite Kontrolü
- [ ] Kod ekran görüntüleri net
- [ ] Akış diyagramı anlaşılır
- [ ] Türkçe yazım kurallarına uygun
- [ ] Teknik terimler doğru kullanılmış

## Adım 7: PDF'e Dönüştürme

1. Word dosyasını kaydedin
2. File -> Save As -> PDF seçin
3. "Standard (publishing online and printing)" seçeneğini seçin
4. Save butonuna tıklayın

## Rapor Kısaltma (İsteğe Bağlı)

Rapor şu halde ~15-18 sayfa civarı. Eğer 5 sayfa civarı isterseniz:

### Kısaltılabilir Bölümler
1. **Benzer Çalışmalar:** 11 makaleden 5 tanesini kullanın
2. **Materyal ve Yöntem:** 3.5 ve 3.6 bölümlerini kısaltın
3. **Geliştirilen Sistem:** 4.1-4.8 arası bazı alt bölümleri birleştirin
4. **Kod Ekran Görüntüleri:** 8 yerine 4-5 tane ekleyin

### Korunması Gerekenler (Mutlaka Dahil Edin)
- Özet (200+ kelime)
- Problem Tanımı
- En az 5 benzer çalışma
- Materyal ve Yöntem (temel bilgiler)
- Akış Diyagramı
- En az 3-4 kod ekran görüntüsü
- Sonuç (250+ kelime)
- Kaynaklar

## İletişim ve Sorular

Bu rapor, Slam Dash oyun projesi için hazırlanmıştır.
Proje Repository: https://github.com/atakandll/Slam-Dash
Oyun: https://atakandll.itch.io/slam-dash

## Dosya Konumları (Referans)

Tüm kod dosyaları repository içinde şu yapıdadır:
```
/home/runner/work/Slam-Dash/Slam-Dash/Assets/Scripts/Runtime/
├── Managers/
├── Controllers/
│   └── Player/
├── Signals/
├── Commands/
│   └── Level/
└── Extensions/
```

## Başarılar!

Raporunuzu hazırlarken başarılar dilerim. Tüm gerekli bilgiler detaylı olarak verilmiştir.
