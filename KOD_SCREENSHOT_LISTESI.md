# KOD EKRAN GÖRÜNTÜLERİ - DETAYLI LİSTE

## ŞEKİL 1: PlayerMovementController - Recursive Hareket Algoritması

**DOSYA YOLU:**
Assets/Scripts/Runtime/Controllers/Player/PlayerMovementController.cs

**SATIR NUMARALARI:** 39-61

**EKRAN GÖRÜNTÜSÜNE ALINACAK KOD:**

```csharp
private int MoveOnceRecursive(Vector2Int direction, int moveCount = 0)
{
    if (moveCount > 1000)
    {
        Debug.Log("Too many moves");
        return moveCount;
    }

    //if (_isAvailableForInput)
    
        var nextPosition = transform.position + new Vector3(direction.x, direction.y, 0);
 
        if (playerManager.CheckForWallForMovement(nextPosition))
        {
            transform.position = nextPosition;

            return MoveOnceRecursive(direction, moveCount + 1); // Recursive call
    
        }
    
    
    return moveCount;
}
```

**RAPORDA NEREYE:** Bölüm 4.2 (Sayfa ~10)

---

## ŞEKİL 2: PlayerPhysicController - Duvar Kontrol Sistemi

**DOSYA YOLU:**
Assets/Scripts/Runtime/Controllers/Player/PlayerPhysicController.cs

**SATIR NUMARALARI:** 14-19

**EKRAN GÖRÜNTÜSÜNE ALINACAK KOD:**

```csharp
internal bool CheckForWall(Vector3 worldPoint)
{
    Collider2D wall = Physics2D.OverlapPoint(worldPoint,wallLayer);

    return wall == null;
}
```

**RAPORDA NEREYE:** Bölüm 4.2 (Sayfa ~11)

---

## ŞEKİL 3: InputManager - Klavye Giriş Kontrolü

**DOSYA YOLU:**
Assets/Scripts/Runtime/Managers/InputManager.cs

**SATIR NUMARALARI:** 64-80

**EKRAN GÖRÜNTÜSÜNE ALINACAK KOD:**

```csharp
private void Update()
{
    //if (!_isAvailableForInput) return;
    //if (!Input.anyKey) return;

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

**RAPORDA NEREYE:** Bölüm 4.3 (Sayfa ~11)

---

## ŞEKİL 4A: LevelManager - SubscribeEvents

**DOSYA YOLU:**
Assets/Scripts/Runtime/Managers/LevelManager.cs

**SATIR NUMARALARI:** 47-51

**EKRAN GÖRÜNTÜSÜNE ALINACAK KOD:**

```csharp
private void SubscribeEvents()
{
    LevelSignals.Instance.OnLevelInitialize += _levelLoader.Execute;
    LevelSignals.Instance.OnClearActiveLevel += _levelDestroyer.Execute;
}
```

**RAPORDA NEREYE:** Bölüm 4.4 (Sayfa ~12)

---

## ŞEKİL 4B: LevelLoaderCommand - Execute

**DOSYA YOLU:**
Assets/Scripts/Runtime/Commands/Level/LevelLoaderCommand.cs

**SATIR NUMARALARI:** 18-28

**EKRAN GÖRÜNTÜSÜNE ALINACAK KOD:**

```csharp
internal void Execute(byte levelIndex)
{
    _request = Addressables.LoadAssetAsync<GameObject>($"Prefabs/LevelPrefabs/Level {levelIndex}");
    _request.Completed += handle =>
    {
        var newLevel = Object.Instantiate(_request.Result as GameObject, Vector3.zero, Quaternion.identity);
        if(newLevel!=null) newLevel.transform.SetParent(_levelManager.levelHolder.transform);

    };

}
```

**RAPORDA NEREYE:** Bölüm 4.4 (Sayfa ~12)

**NOT:** Şekil 4A ve 4B yan yana veya alt alta aynı şekilde gösterilebilir.

---

## ŞEKİL 5A: MonoSingleton Pattern

**DOSYA YOLU:**
Assets/Scripts/Runtime/Extensions/MonoSingleton.cs

**SATIR NUMARALARI:** 1-34 (TAMAMINI)

**EKRAN GÖRÜNTÜSÜNE ALINACAK KOD:**

```csharp
using UnityEngine;

namespace Runtime.Extensions
{
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

        protected virtual void Awake()
        {
            _instance = this as T;
        }


    }
}
```

**RAPORDA NEREYE:** Bölüm 4.5 (Sayfa ~13)

---

## ŞEKİL 5B: CoreGameSignals

**DOSYA YOLU:**
Assets/Scripts/Runtime/Signals/CoreGameSignals.cs

**SATIR NUMARALARI:** 1-12 (TAMAMINI)

**EKRAN GÖRÜNTÜSÜNE ALINACAK KOD:**

```csharp
using Runtime.Extensions;
using UnityEngine.Events;

namespace Runtime.Signals
{
    public class CoreGameSignals : MonoSingleton<CoreGameSignals>
    {
        public UnityAction OnPlay = delegate { };
        public UnityAction OnReset = delegate { };
        
    }
}
```

**RAPORDA NEREYE:** Bölüm 4.5 (Sayfa ~13)

**NOT:** Şekil 5A ve 5B yan yana veya alt alta aynı şekilde gösterilebilir.

---

## ŞEKİL 6: UIManager - Panel Yönetimi

**DOSYA YOLU:**
Assets/Scripts/Runtime/Managers/UIManager.cs

**SATIR NUMARALARI:** 57-58

**EKRAN GÖRÜNTÜSÜNE ALINACAK KOD:**

```csharp
private void OnClosePanel(UIPanelTypes type) => uiPanelControllers.ChangePanel(type,false);
private void OnOpenPanel(UIPanelTypes type) => uiPanelControllers.ChangePanel(type,true);
```

**RAPORDA NEREYE:** Bölüm 4.6 (Sayfa ~13)

**NOT:** Daha anlaşılır olması için 30-35. satırlar arası SubscribeEvents de eklenebilir.

---

## ŞEKİL 7: Akış Diyagramı

**DOSYA:** Yeni oluşturulacak (Draw.io, Lucidchart vb.)

**İÇERİK:** PROJE_RAPORU.md dosyasının 4.7 bölümünde verilen text-based akış diyagramı

**RAPORDA NEREYE:** Bölüm 4.7 (Sayfa ~14-15)

**ÇİZİM ELEMANLARI:**

1. BAŞLA (Oval, Yeşil)
2. Unity Başlatma (Dikdörtgen, Mavi)
3. Manager'lar Awake (Dikdörtgen, Mavi)
4. Signal Sistemine Subscribe (Dikdörtgen, Mavi)
5. Start Panel Göster (Dikdörtgen, Mavi)
6. Kullanıcı "Play" Tuşuna Basıyor mu? (Baklava, Turuncu)
   - HAYIR -> Bekle
   - EVET -> Devam
7. OnPlay Signal Tetikleniyor (Dikdörtgen, Mavi)
8. Seviye 0 Yükleniyor (Dikdörtgen, Mavi)
9. Game Panel Göster (Dikdörtgen, Mavi)
10. InputManager Aktif (Dikdörtgen, Mavi)
11. Kullanıcı Yön Tuşuna Basıyor mu? (Baklava, Turuncu)
    - HAYIR -> Bekle
    - EVET -> Devam
12. onPlayerMove Signal Tetikleniyor (Dikdörtgen, Mavi)
13. MoveOnceRecursive Çağrılıyor (Dikdörtgen, Kırmızı)
14. Sonraki Pozisyon Hesapla (Dikdörtgen, Mavi)
15. Duvar Kontrolü (Physics2D.OverlapPoint) (Baklava, Turuncu)
    - DUVAR VAR -> Hareket Bitir
    - DUVAR YOK -> Devam
16. Karakteri Hareket Ettir (Dikdörtgen, Mavi)
17. MoveCount++ (Dikdörtgen, Mavi)
18. MoveCount > 1000 mı? (Baklava, Turuncu)
    - EVET -> Güvenlik Çıkışı
    - HAYIR -> Recursive çağrı (geri 13'e)
19. Çıkışa Ulaşıldı mı? (Baklava, Turuncu)
    - HAYIR -> Input Beklemeye Dön (geri 11'e)
    - EVET -> Devam
20. OnLevelSuccess Signal (Dikdörtgen, Mavi)
21. Win Panel Göster (Dikdörtgen, Mavi)
22. BİTİR (Oval, Yeşil)

---

## ŞEKİL 8: PlayerManager - Event Subscription Pattern

**DOSYA YOLU:**
Assets/Scripts/Runtime/Managers/PlayerManager.cs

**SATIR NUMARALARI:** 33-41

**EKRAN GÖRÜNTÜSÜNE ALINACAK KOD:**

```csharp
private void OnEnable() => SubscribeEvents();
   
private void SubscribeEvents()
{
    CoreGameSignals.Instance.OnPlay += OnPlay;
    CoreGameSignals.Instance.OnReset += OnReset;
    LevelSignals.Instance.OnLevelFailed += OnLevelFailed;
    LevelSignals.Instance.OnLevelSuccess += OnLevelSuccess;
}
```

**RAPORDA NEREYE:** Bölüm 4.8 (Sayfa ~16)

---

## GENEL EKRAN GÖRÜNTÜSÜ İPUÇLARI

1. **Satır Numaralarını Göster:** Visual Studio'da View -> Options -> Text Editor -> All Languages -> Line Numbers
2. **Uygun Zoom:** %100-125 arası
3. **Light Theme Kullan:** Daha iyi basılır ve okunur
4. **Screenshot Aracı:** Windows: Snipping Tool (Win+Shift+S), Mac: Cmd+Shift+4
5. **Kod Formatı:** Her dosya düzgün formatlı olmalı (Ctrl+K, Ctrl+D ile format)
6. **Font Boyutu:** Kod editöründe 12-14pt font kullanın (çok küçük olmasın)

## WORD'E EKLEME

1. Insert -> Pictures -> This Device
2. Resmi seç ve ekle
3. Resmi sağ tıkla -> Wrap Text -> In Line with Text
4. Resmin altına şekil yazısı ekle (Arial, 10pt, italik, ortalı)
5. Örnek: "Şekil 1: PlayerMovementController - Recursive Hareket Algoritması"

## ÖNEMLİ NOTLAR

- Her şekle metin içinde mutlaka referans verin: "Şekil 1'de görüldüğü gibi..."
- Kod ekran görüntüleri net ve okunabilir olmalı
- Satır numaraları görünür olmalı
- Dosya adı ve namespace'ler görünüyorsa daha iyi
- Tüm kodlar context ile birlikte eklenmelidir (sadece bir satır değil)
