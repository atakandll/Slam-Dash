# SLAM DASH OYUN PROJESİ RAPORU

---

## 1. GİRİŞ VE OYUN TANITIMI

### Proje Bilgileri
- **Oyun Adı:** Slam Dash
- **Platform:** PC (Windows, WebGL)
- **Tür:** 2D Bulmaca Oyunu
- **Geliştirme Ortamı:** Unity 2021.3.22f1
- **Programlama Dili:** C#
- **Versiyon:** Prototype 0.0.2
- **Yayın:** https://atakandll.itch.io/slam-dash

### Oyun Konsepti
Slam Dash, hassasiyetin önemli olduğu zorlayıcı bir 2D bulmaca prototipidir. Oyuncu, karakterini duvarlardan stratejik olarak sekme hareketi yaparak çıkışa ulaştırmalıdır. Bir yön seçildiğinde karakter o yönde hareket etmeye başlar ve bir duvara çarpana kadar durmaz. Bu mekanik, oyuncunun her hamleyi dikkatli planlamasını gerektirir.

### Teknik Özet
Proje, **2,366 satır** kod ve **24 adet** C# script içermektedir. Modüler mimari ve tasarım desenleri kullanılarak geliştirilmiştir.

---

## 2. TEKNİK MİMARİ VE TASARIM DESENLERİ

### Proje Yapısı
Oyun, temiz kod prensipleri ve MVC benzeri bir mimariye dayanır:

```
Assets/Scripts/Runtime/
├── Managers/           → Oyun yöneticileri (Input, Player, Level, UI)
├── Controllers/        → Oyuncu kontrolleri (Movement, Physics, Animation)
├── Signals/           → Event sistemi (CoreGame, Level, Player, UI)
├── Commands/          → Komut deseni (LevelLoader, LevelDestroyer)
├── Data/              → Veri yapıları ve Scriptable Objects
└── Enums/             → Enum tanımları
```

### Kullanılan Tasarım Desenleri

**1. Singleton Pattern**
Kritik manager'lar için MonoSingleton kullanılmıştır:
```csharp
public class CoreGameSignals : MonoSingleton<CoreGameSignals>
{
    public UnityAction OnPlay = delegate { };
    public UnityAction OnReset = delegate { };
}
```

**2. Observer Pattern (Signals Sistemi)**
Sistemler arası gevşek bağlılık için event-based iletişim:
- `CoreGameSignals`: Oyun durumu (Play, Reset)
- `LevelSignals`: Seviye yönetimi
- `PlayerSignals`: Oyuncu eylemleri
- `CoreUISignals`: UI yönetimi

**3. Command Pattern**
Seviye işlemleri için komut deseni implementasyonu:
```csharp
private LevelLoaderCommand _levelLoader;
private LevelDestroyerCommand _levelDestroyer;

LevelSignals.Instance.OnLevelInitialize += _levelLoader.Execute;
```

---

## 3. ANA BILEŞENLER VE KOD YAPISI

### Manager Sınıfları

**InputManager** - Klavye girişlerini yönetir:
```csharp
private void Update()
{
    if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        direction = Vector2Int.up;
    // ... diğer yönler
    
    if (direction.HasValue)
        PlayerSignals.Instance.onPlayerMove?.Invoke(direction.Value);
}
```
- WASD veya ok tuşları ile kontrol
- Addressable Asset System ile veri yönetimi

**PlayerManager** - Oyuncu sistemlerini koordine eder:
```csharp
[SerializeField] private PlayerMovementController playerMovementController;
[SerializeField] private PlayerPhysicController playerPhysicController;
[SerializeField] private PlayerAnimationController playerAnimationController;
[SerializeField] private PlayerFeelController playerFeelController;
```
- Alt kontrolcüleri yönetir
- Fizik kontrolü için arayüz sağlar

**LevelManager** - Seviye yükleme ve yok etme:
```csharp
private void Init()
{
    _levelLoader = new LevelLoaderCommand(this);
    _levelDestroyer = new LevelDestroyerCommand(this);
}
```

**UIManager** - Panel yönetimi:
- Start Panel, Game Panel, Win Panel, Lose Panel

### Controller Sınıfları

**PlayerMovementController** - Oyunun ana mekaniği:
```csharp
private int MoveOnceRecursive(Vector2Int direction, int moveCount = 0)
{
    if (moveCount > 1000)
        return moveCount; // Sonsuz döngü koruması
    
    var nextPosition = transform.position + new Vector3(direction.x, direction.y, 0);
    
    if (playerManager.CheckForWallForMovement(nextPosition))
    {
        transform.position = nextPosition;
        return MoveOnceRecursive(direction, moveCount + 1); // Özyineleme
    }
    
    return moveCount;
}
```
**Özellikler:**
- Recursive (özyinelemeli) algoritma
- Grid-based hareket sistemi
- Sonsuz döngü koruması (max 1000 hareket)

**PlayerPhysicController** - Fizik ve çarpışma:
```csharp
internal bool CheckForWall(Vector3 worldPoint)
{
    Collider2D wall = Physics2D.OverlapPoint(worldPoint, wallLayer);
    return wall == null; // Duvar yoksa true döner
}
```
- Layer-based fizik sistemi
- Performanslı nokta bazlı kontrolü

---

## 4. OYUN MEKANİĞİ İMPLEMENTASYONU

### Hareket Sistemi Akışı
1. **Input Algılama:** InputManager klavye girişini yakalar
2. **Event Tetikleme:** `PlayerSignals.onPlayerMove` eventi gönderilir
3. **Hareket Başlatma:** PlayerMovementController eventi alır
4. **Recursive Hareket:**
   - Bir sonraki pozisyon hesaplanır
   - Duvar kontrolü yapılır  
   - Duvar yoksa → hareket devam eder (recursive çağrı)
   - Duvara çarpılınca → hareket durur
5. **Geri Bildirim:** Toplam hareket sayısı kaydedilir

### Event-Driven Architecture
Sistemler birbirini doğrudan çağırmaz, eventler üzerinden iletişim kurar:

```
Input → InputManager → PlayerSignals.onPlayerMove 
  → PlayerMovementController → Hareket Algoritması 
  → Duvar Çarpma → Feedback
```

**Avantajları:**
- Loosely coupled (gevşek bağlı) sistemler
- Kolay test edilebilirlik
- Genişletilebilirlik

### Veri Yönetimi
Addressable Asset System kullanımı:
```csharp
private void GetData()
{
    var request = data.LoadAssetAsync();
    request.Completed += handle =>
    {
        if (handle.Status == AsyncOperationStatus.Succeeded)
            _data = handle.Result.PlayerInputData;
    };
}
```
- Runtime'da dinamik asset yükleme
- Bellek optimizasyonu

---

## 5. TEKNİK ÖZELLIKLER VE OPTIMIZASYONLAR

### SOLID Prensipleri
- **Single Responsibility:** Her sınıf tek sorumluluk taşır
- **Open/Closed:** Genişletmeye açık, değişikliğe kapalı
- **Dependency Inversion:** Signal sistemi ile soyutlama

### Performans Optimizasyonları
- **Layer-based Collision:** Sadece duvar layer'ı kontrol edilir
- **Addressable Assets:** Dinamik yükleme ile bellek tasarrufu
- **Event Pooling:** Delegate allocation optimizasyonu
- **Grid-based Movement:** Integer pozisyonlar ile hızlı hesaplama

### Kullanılan Unity Özellikleri
- **Physics2D:** 2D fizik motoru ve çarpışma tespiti
- **Addressable System:** Asset yönetimi
- **Prefab System:** Yeniden kullanılabilir objeler
- **Animation System:** Karakter animasyonları
- **Canvas UI:** Modern UI sistemi

### Kod Kalitesi
```
Toplam Script: 24 adet
Toplam Satır: ~2,366
Ortalama: ~98 satır/script

Dağılım:
├── Managers: 4 script (InputManager, PlayerManager, LevelManager, UIManager)
├── Controllers: 7 script (Movement, Physics, Animation, UI vs.)
├── Signals: 5 script (Event sistemi)
├── Commands: 2 script (Level komutları)
└── Diğer: 6 script (Data, Enums, Extensions)
```

---

## 6. GELİŞTİRME SÜRECİ VE KULLANILAN ARAÇLAR

### Versiyon Geçmişi
- **Prototype 0.0.1:** Temel hareket mekaniği ve fizik sistemi
- **Prototype 0.0.2:** UI sistemi, seviye yönetimi (itch.io'da yayında)

### Kullanılan Eklentiler ve Araçlar
- **Odin Inspector:** Editor için gelişmiş inspector arayüzü ve serialization
- **TextMesh Pro:** Profesyonel text rendering
- **Addressables:** Modern asset yönetim sistemi
- **Unity Tilemap:** 2D level design

### Best Practices
- Consistent naming conventions (PascalCase, camelCase)
- Region-based code organization
- Serialized field kullanımı
- Event subscription/unsubscription pattern
- Memory leak prevention (OnDestroy cleanup)

---

## 7. SONUÇ VE DEĞERLENDİRME

### Başarılan Hedefler ✓
- Temiz ve modüler kod mimarisi oluşturuldu
- Event-driven tasarım ile loosely-coupled sistemler geliştirildi
- Performanslı recursive hareket algoritması implement edildi
- Ölçeklenebilir seviye sistemi kuruldu
- Profesyonel UI yönetimi sağlandı
- Modern Unity özellikleri (Addressables, Physics2D) kullanıldı

### Öğrenilen Teknikler
**Tasarım Desenleri:**
- Singleton Pattern ile global erişim
- Command Pattern ile işlem yönetimi
- Observer Pattern (Signals) ile event sistemi

**Unity ve C# Kavramları:**
- Addressable Asset System
- Physics2D ve Layer-based collision
- Recursive algorithms
- Event-driven architecture
- Scriptable Objects

**Yazılım Mühendisliği:**
- SOLID prensipleri
- Clean Code pratikleri
- Modular design
- Separation of concerns

### Güçlü Yönler
- **Mimari:** Profesyonel ve ölçeklenebilir tasarım
- **Kod Kalitesi:** Okunabilir ve bakımı kolay kod
- **Performans:** Optimize edilmiş sistemler
- **Genişletilebilirlik:** Yeni özellikler eklemek kolay

### Geliştirilebilir Alanlar
Kod incelemesinde tespit edilen iyileştirme noktaları:
- PlayerMovementController'da yorum satırı olarak bırakılmış özellikler (ses, parçacık efektleri, kamera sarsıntısı)
- Unit test coverage eklenmesi
- XML dokümantasyon yorumları
- Error handling mekanizmaları

### Gelecek Planlar

**Kısa Vade (Gelecek Güncellemeler):**
- Ses sistemi implementasyonu
- Particle efektleri (duvara çarpma, hareket izi)
- Kamera shake efekti
- Daha fazla seviye tasarımı
- Can sistemi

**Uzun Vade (Gelecek Versiyonlar):**
- Mobil platform desteği (Android/iOS)
- Seviye editörü
- Online leaderboard
- Yeni game mekanikleri (engeller, power-up'lar)
- Kullanıcı tarafından oluşturulan seviyeler

### Proje Değerlendirmesi

**Teknik Başarı:** Oyun, modern yazılım mühendisliği prensipleri kullanılarak geliştirilmiştir. Modüler yapısı sayesinde bakımı kolay ve genişletilebilir bir kod tabanına sahiptir.

**Öğrenme Çıktıları:** Proje geliştirme sürecinde Unity oyun motoru, C# programlama, tasarım desenleri ve yazılım mimarisi konularında deneyim kazanılmıştır.

**Oynanabilirlik:** Basit ama zorlayıcı mekanik, oyuncuların stratejik düşünmesini sağlar. Prototype versiyonları olumlu geri bildirimler almıştır.

---

## EKLER

### Event Subscription Pattern Örneği
```csharp
private void OnEnable() => SubscribeEvents();

private void SubscribeEvents()
{
    CoreGameSignals.Instance.OnPlay += OnPlay;
    CoreGameSignals.Instance.OnReset += OnReset;
    LevelSignals.Instance.OnLevelFailed += OnLevelFailed;
}

private void OnDisable() => UnsubscribeEvents();

private void UnsubscribeEvents()
{
    CoreGameSignals.Instance.OnPlay -= OnPlay;
    CoreGameSignals.Instance.OnReset -= OnReset;
    LevelSignals.Instance.OnLevelFailed -= OnLevelFailed;
}
```
Bu pattern sayesinde memory leak önlenir ve event'ler düzgün şekilde temizlenir.

### Proje Kaynakları
- **Unity Docs:** https://docs.unity3d.com/
- **Itch.io:** https://atakandll.itch.io/slam-dash
- **Repository:** GitHub (mevcut)

---

**Rapor Hazırlayan:** Atakan Dilli  
**Tarih:** Ocak 2024  
**Ders:** Proje 1  
**Durum:** Aktif Geliştirme (Prototype 0.0.2)
