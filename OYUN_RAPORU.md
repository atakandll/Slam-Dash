# SLAM DASH OYUN PROJESİ RAPORU

## 1. GİRİŞ

### 1.1 Proje Özeti
Slam Dash, Unity oyun motoru kullanılarak geliştirilmiş 2D bulmaca türünde bir oyun prototipidir. Oyunun temel mekaniği, karakterin duvarlardan sekme hareketi yaparak çıkışa ulaşmasına dayanmaktadır. Oyuncu bir yön seçtiğinde, karakter seçilen yönde hareket etmeye başlar ve bir duvara çarpana kadar durmaz. Bu basit ama zorlayıcı mekanik, oyuncunun stratejik düşünmesini ve her hamlesini dikkatli planlamasını gerektirir.

### 1.2 Oyun Tanımı
Oyunun itch.io sayfasındaki açıklaması şu şekildedir:
> "Slam Dash is a challenging 2D puzzle prototype game where precision is key. Navigate your character through each level by strategically bouncing off walls to reach the exit. Choose your direction wisely, as your character will keep moving until they hit wall. Test your decision-making skills in this puzzle experience."

### 1.3 Teknik Bilgiler
- **Platform:** PC (Windows, WebGL)
- **Oyun Motoru:** Unity 2021.3.22f1
- **Programlama Dili:** C#
- **Toplam Kod Satırı:** ~2,366 satır
- **Toplam Script Sayısı:** 24 adet C# script
- **Versiyon:** Prototype 0.0.2

---

## 2. TEKNİK MİMARİ VE TASARIM

### 2.1 Proje Mimarisi
Slam Dash projesi, temiz kod prensipleri ve modüler yapı göz önünde bulundurularak geliştirilmiştir. Proje, MVC (Model-View-Controller) benzeri bir mimariye dayanmaktadır ve aşağıdaki ana bileşenlerden oluşur:

#### 2.1.1 Dizin Yapısı
```
Assets/
├── Scripts/
│   └── Runtime/
│       ├── Managers/          (Oyun yöneticileri)
│       ├── Controllers/       (Oyun kontrolleri)
│       ├── Signals/           (Event sistemi)
│       ├── Commands/          (Komut deseni)
│       ├── Data/              (Veri yapıları)
│       ├── Enums/             (Enum tanımları)
│       └── Extensions/        (Yardımcı sınıflar)
├── Scenes/                    (Unity sahneleri)
├── Prefabs/                   (Prefab'lar)
├── Art/                       (Görsel varlıklar)
└── Animations/                (Animasyonlar)
```

### 2.2 Tasarım Desenleri

#### 2.2.1 Singleton Pattern
Oyunda kritik manager'lar için MonoSingleton kullanılmıştır:

```csharp
// MonoSingleton.cs - Genel Singleton Implementasyonu
public class MonoSingleton<T> : MonoBehaviour where T : Component
{
    private static T _instance;
    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<T>();
                if (_instance == null)
                {
                    GameObject newGo = new GameObject();
                    newGo.name = "TInstance";
                    _instance = newGo.AddComponent<T>();
                    DontDestroyOnLoad(newGo);
                }
            }
            return _instance;
        }
    }
}
```

Bu desen, CoreGameSignals, LevelSignals, PlayerSignals gibi sinyal sınıflarında kullanılarak oyun genelinde tek bir instance'ın kullanılması sağlanmıştır.

#### 2.2.2 Signal (Event) Pattern
Oyundaki farklı sistemler arasındaki iletişim, bir sinyal sistemi ile sağlanmıştır. Bu yaklaşım, loose coupling (gevşek bağlılık) prensibini takip eder:

```csharp
// CoreGameSignals.cs
public class CoreGameSignals : MonoSingleton<CoreGameSignals>
{
    public UnityAction OnPlay = delegate { };
    public UnityAction OnReset = delegate { };
}
```

**Kullanılan Signal Sınıfları:**
- `CoreGameSignals`: Oyunun genel durumu (Play, Reset)
- `LevelSignals`: Seviye yönetimi (LevelInitialize, LevelFailed, LevelSuccess)
- `PlayerSignals`: Oyuncu eylemleri (PlayerMove, AnimationStateChange)
- `CoreUISignals`: UI yönetimi (OpenPanel, ClosePanel)
- `InputSignals`: Input yönetimi

#### 2.2.3 Command Pattern
Seviye yükleme ve yok etme işlemleri için Command Pattern kullanılmıştır:

```csharp
// LevelLoaderCommand.cs
public class LevelLoaderCommand
{
    private LevelManager _levelManager;
    
    public LevelLoaderCommand(LevelManager levelManager)
    {
        _levelManager = levelManager;
    }
    
    public void Execute(byte levelIndex)
    {
        // Seviye yükleme mantığı
    }
}
```

---

## 3. ANA BILEŞENLER VE KOD YAPISI

### 3.1 Manager Sınıfları

#### 3.1.1 InputManager
Input yönetiminden sorumlu olan bu sınıf, oyuncunun klavye girişlerini algılar ve uygun sinyalleri gönderir:

```csharp
// InputManager.cs - Update Metodu
private void Update()
{
    if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        direction = Vector2Int.up;
    else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        direction = Vector2Int.down;
    else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        direction = Vector2Int.left;
    else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        direction = Vector2Int.right;

    if (direction.HasValue)
        PlayerSignals.Instance.onPlayerMove?.Invoke(direction.Value);
}
```

**Özellikler:**
- W/Yukarı Ok: Yukarı hareket
- S/Aşağı Ok: Aşağı hareket
- A/Sol Ok: Sola hareket
- D/Sağ Ok: Sağa hareket
- Addressable Asset System kullanımı

#### 3.1.2 PlayerManager
Oyuncu ile ilgili tüm kontrolcüleri yöneten merkezi sınıf:

```csharp
// PlayerManager.cs
public class PlayerManager : MonoBehaviour
{
    [SerializeField] private PlayerMovementController playerMovementController;
    [SerializeField] private PlayerPhysicController playerPhysicController;
    [SerializeField] private PlayerAnimationController playerAnimationController;
    [SerializeField] private PlayerFeelController playerFeelController;
    
    private void SubscribeEvents()
    {
        CoreGameSignals.Instance.OnPlay += OnPlay;
        CoreGameSignals.Instance.OnReset += OnReset;
        LevelSignals.Instance.OnLevelFailed += OnLevelFailed;
        LevelSignals.Instance.OnLevelSuccess += OnLevelSuccess;
    }
    
    internal bool CheckForWallForMovement(Vector3 worldPoint)
    {
        return playerPhysicController.CheckForWall(worldPoint);
    }
}
```

**Sorumlulukları:**
- Oyuncu kontrolcülerini koordine etme
- Oyun durumu değişikliklerini dinleme
- Fizik kontrolü için arayüz sağlama

#### 3.1.3 LevelManager
Seviye yükleme ve yok etme işlemlerini yöneten sınıf:

```csharp
// LevelManager.cs
public class LevelManager : MonoBehaviour
{
    [SerializeField] internal GameObject levelHolder;
    [SerializeField] private byte totalLeventCount;
    
    private LevelLoaderCommand _levelLoader;
    private LevelDestroyerCommand _levelDestroyer;
    
    private void Init()
    {
        _levelLoader = new LevelLoaderCommand(this);
        _levelDestroyer = new LevelDestroyerCommand(this);
    }
    
    private void SubscribeEvents()
    {
        LevelSignals.Instance.OnLevelInitialize += _levelLoader.Execute;
        LevelSignals.Instance.OnClearActiveLevel += _levelDestroyer.Execute;
    }
}
```

#### 3.1.4 UIManager
Oyun içi tüm UI panellerini yöneten sınıf:

```csharp
// UIManager.cs
public class UIManager : MonoBehaviour
{
    [SerializeField] private UIPanelControllers uiPanelControllers;
    
    internal void ChangeStartPanel()
    {
        CoreGameSignals.Instance.OnPlay?.Invoke();
        LevelSignals.Instance.OnLevelInitialize?.Invoke(0);
    }
    
    private void OnOpenPanel(UIPanelTypes type) 
        => uiPanelControllers.ChangePanel(type, true);
    
    private void OnClosePanel(UIPanelTypes type) 
        => uiPanelControllers.ChangePanel(type, false);
}
```

**Panel Türleri:**
- Start Panel (Başlangıç ekranı)
- Game Panel (Oyun içi)
- Win Panel (Kazanma ekranı)
- Lose Panel (Kaybetme ekranı)

### 3.2 Controller Sınıfları

#### 3.2.1 PlayerMovementController
Oyunun ana mekaniğini içeren en kritik kontrolcü:

```csharp
// PlayerMovementController.cs
private void OnPlayerMove(Vector2Int direction)
{
    int count = MoveOnceRecursive(direction);
    if (count > 0)
    {
        // Duvara çarpma efektleri
        Debug.Log(count);
    }
}

private int MoveOnceRecursive(Vector2Int direction, int moveCount = 0)
{
    if (moveCount > 1000)
    {
        Debug.Log("Too many moves");
        return moveCount;
    }
    
    var nextPosition = transform.position + new Vector3(direction.x, direction.y, 0);
    
    if (playerManager.CheckForWallForMovement(nextPosition))
    {
        transform.position = nextPosition;
        return MoveOnceRecursive(direction, moveCount + 1);
    }
    
    return moveCount;
}
```

**Önemli Noktalar:**
- Recursive (özyinelemeli) algoritma kullanımı
- Her seferinde 1 birim hareket
- Duvar kontrolü ile hareketin sınırlandırılması
- Sonsuz döngü koruması (max 1000 hareket)

#### 3.2.2 PlayerPhysicController
Fizik kontrolü ve çarpışma tespiti:

```csharp
// PlayerPhysicController.cs
public class PlayerPhysicController : MonoBehaviour
{
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] private PlayerManager playerManager;
    
    internal bool CheckForWall(Vector3 worldPoint)
    {
        Collider2D wall = Physics2D.OverlapPoint(worldPoint, wallLayer);
        return wall == null;
    }
}
```

**Özellikler:**
- Layer-based fizik sistemi
- Nokta bazlı çarpışma kontrolü
- Performanslı fizik sorguları

---

## 4. OYUN MEKANİĞİ İMPLEMENTASYONU

### 4.1 Hareket Sistemi
Oyunun temel mekaniği şu şekilde çalışır:

1. **Input Algılama:** InputManager klavye girişini algılar
2. **Sinyal Gönderimi:** PlayerSignals.onPlayerMove eventi tetiklenir
3. **Hareket Başlatma:** PlayerMovementController hareketi başlatır
4. **Recursive Hareket:** 
   - Bir sonraki pozisyon hesaplanır
   - Duvar kontrolü yapılır
   - Eğer duvar yoksa hareket devam eder (recursive call)
   - Duvara çarpılınca hareket durur
5. **Feedback:** Duvara çarpma sayısı kaydedilir

### 4.2 Event-Driven Architecture (Olay Güdümlü Mimari)
Oyun, loosely-coupled (gevşek bağlı) bir yapıya sahiptir. Sistemler birbirini doğrudan çağırmak yerine, olaylar üzerinden iletişim kurar:

**Örnek Event Flow:**
```
Player Input → InputManager → PlayerSignals.onPlayerMove 
    → PlayerMovementController → Movement Logic → Wall Hit
    → Feedback System
```

### 4.3 Veri Yönetimi
Oyun, Scriptable Objects ve Addressable Asset System kullanarak veri yönetimini optimize eder:

```csharp
// InputManager.cs - Addressable kullanımı
private void GetData()
{
    var request = data.LoadAssetAsync();
    request.Completed += handle =>
    {
        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            _data = handle.Result.PlayerInputData;
        }
    };
}
```

**Avantajları:**
- Runtime'da dinamik yükleme
- Bellek optimizasyonu
- Kolay güncelleme

---

## 5. ÖNE ÇIKAN TEKNİK ÖZELLIKLER

### 5.1 Modüler Yapı
Proje, SOLID prensiplerine uygun şekilde tasarlanmıştır:
- **Single Responsibility:** Her sınıfın tek bir sorumluluğu var
- **Open/Closed:** Sistemler genişletmeye açık, değişikliğe kapalı
- **Dependency Inversion:** Soyutlamalara bağımlılık (Signals sistemi)

### 5.2 Scalability (Ölçeklenebilirlik)
- Yeni seviyeler kolayca eklenebilir (Command Pattern)
- Yeni panel türleri eklemek kolay (Enum-based system)
- Input sistemi genişletilebilir (Data-driven design)

### 5.3 Performans
- Layer-based çarpışma kontrolü
- Addressable Asset System ile bellek yönetimi
- Event pooling ile GC (Garbage Collection) optimizasyonu

### 5.4 Kullanılan Unity Özellikleri
- **Physics2D:** 2D fizik motoru
- **Addressable Assets:** Asset yönetimi
- **Prefab System:** Yeniden kullanılabilir objeler
- **Animation System:** Karakter animasyonları
- **UI System:** Canvas-based UI

---

## 6. GELİŞTİRME SÜRECİ

### 6.1 Prototype Versiyonları
- **Prototype 0.0.1:** Temel hareket mekaniği
- **Prototype 0.0.2:** UI sistemi, seviye yönetimi (itch.io'da yayında)

### 6.2 Kullanılan Eklentiler
- **Odin Inspector:** Editor için gelişmiş inspector arayüzü
- **TextMesh Pro:** Gelişmiş text rendering
- **Addressables:** Asset yönetim sistemi

### 6.3 Kod İstatistikleri
```
Toplam Script Sayısı: 24 adet
Toplam Kod Satırı: ~2,366 satır
Ortalama Script Uzunluğu: ~98 satır

Kategori Dağılımı:
- Managers: 4 script
- Controllers: 7 script
- Signals: 5 script
- Commands: 2 script
- Data: 2 script
- Diğer: 4 script
```

---

## 7. SONUÇ VE DEĞERLENDİRME

### 7.1 Başarılan Hedefler
✓ Temiz ve modüler kod mimarisi
✓ Event-driven tasarım ile loosely-coupled sistemler
✓ Performanslı recursive hareket algoritması
✓ Ölçeklenebilir seviye sistemi
✓ Profesyonel UI yönetimi
✓ Data-driven input sistemi

### 7.2 Öğrenilen Teknikler
- **Tasarım Desenleri:** Singleton, Command, Observer (Signals)
- **Unity Özellikleri:** Addressables, Physics2D, UI System
- **Programlama Kavramları:** Recursive algorithms, Event-driven architecture
- **Best Practices:** SOLID principles, Clean Code, Modular Design

### 7.3 Gelecek Planlar ve İyileştirmeler

#### 7.3.1 Kısa Vadeli İyileştirmeler
- **Ses Sistemi:** Duvara çarpma, hareket sesleri
- **Görsel Efektler:** Particle sistemleri, kamera sarsıntısı
- **Animasyon Geliştirme:** Daha akıcı karakter animasyonları
- **Seviye Çeşitliliği:** Daha fazla seviye eklenmesi

#### 7.3.2 Uzun Vadeli Hedefler
- **Mobil Platform Desteği:** Android ve iOS portları
- **Seviye Editörü:** Kullanıcıların kendi seviyelerini oluşturması
- **Online Özellikler:** Leaderboard, seviye paylaşımı
- **Yeni Mekanikler:** Farklı engeller, güç artırıcılar

### 7.4 Teknik Borçlar ve İyileştirme Alanları

Kod incelemesi sırasında tespit edilen geliştirilmesi gereken alanlar:
- PlayerMovementController'da yorum satırı olarak bırakılmış özellikler (can sistemi, efektler)
- InputManager'da input availability kontrolünün yorum satırında olması
- Unit test eksikliği
- Dokümantasyon genişletilmesi

### 7.5 Proje Değerlendirmesi

**Güçlü Yönler:**
- Temiz ve okunabilir kod yapısı
- Modern Unity best practices kullanımı
- Modüler ve genişletilebilir mimari
- Profesyonel tasarım desenleri uygulaması

**Geliştirilebilir Yönler:**
- Test coverage artırılabilir
- Kod dokümantasyonu zenginleştirilebilir
- Error handling mekanizmaları eklenebilir
- Performans profiling yapılabilir

---

## 8. KAYNAKLAR VE REFERANSLAR

### 8.1 Teknik Kaynaklar
- Unity Documentation: https://docs.unity3d.com/
- C# Programming Guide
- Game Programming Patterns by Robert Nystrom
- Clean Code by Robert C. Martin

### 8.2 Kullanılan Teknolojiler
- Unity 2021.3.22f1
- C# 9.0
- .NET Framework
- Odin Inspector
- Addressable Asset System

### 8.3 Proje Linkleri
- Itch.io Sayfası: https://atakandll.itch.io/slam-dash
- GitHub Repository: (Mevcut repository)

---

## EKLER

### Ek A: Önemli Kod Snippetleri

**Event Subscription Pattern:**
```csharp
private void OnEnable() => SubscribeEvents();

private void SubscribeEvents()
{
    CoreGameSignals.Instance.OnPlay += OnPlay;
    CoreGameSignals.Instance.OnReset += OnReset;
}

private void OnDisable() => UnsubscribeEvents();

private void UnsubscribeEvents()
{
    CoreGameSignals.Instance.OnPlay -= OnPlay;
    CoreGameSignals.Instance.OnReset -= OnReset;
}
```

### Ek B: Proje Klasör Yapısı Detayı
```
Assets/
├── Animations/           # Karakter ve UI animasyonları
├── Art/                  # Sprite'lar ve görseller
├── Data/                 # Scriptable Object'ler
├── Fonts/                # Font dosyaları
├── Plugins/              # Third-party eklentiler
│   └── Sirenix/         # Odin Inspector
├── Prefabs/              # Prefab'lar
│   ├── LevelPrefabs/    # Seviye prefab'ları
│   ├── Panels/          # UI panel prefab'ları
│   └── Player/          # Oyuncu prefab'ı
├── Scenes/               # Unity Scene dosyaları
├── Scripts/              # C# Script'ler
│   └── Runtime/
│       ├── Commands/
│       ├── Controllers/
│       ├── Data/
│       ├── Enums/
│       ├── Extensions/
│       ├── Managers/
│       └── Signals/
├── TextMesh Pro/         # TMP assets
└── Tiles/                # Tilemap assets
```

---

**Rapor Tarihi:** Ocak 2024  
**Proje Durumu:** Aktif Geliştirme (Prototype 0.0.2)  
**Geliştirici:** Atakan Dilli
