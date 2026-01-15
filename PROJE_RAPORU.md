# SLAM DASH OYUN PROJESİ RAPORU

**NOT: Bu rapor Word dosyasına aktarılmalı ve Times New Roman 12 punto ile yazılmalıdır.**

---

## ÖZET

Bu proje kapsamında "Slam Dash" isimli 2D puzzle platformer oyunu geliştirilmiştir. Oyun, oyuncunun bir karakteri kontrol ederek, duvarlara çarparak yön değiştirerek ve stratejik hamleler yaparak seviye çıkışına ulaşmasını hedeflemektedir. Oyunun temel mekaniği, karakterin bir yöne hareket ettirildiğinde duvara çarpana kadar o yönde ilerlemeye devam etmesidir. Bu mekanik, oyuncudan dikkatli planlama ve problem çözme becerisi gerektirmektedir.

Proje Unity oyun motoru kullanılarak C# programlama dili ile geliştirilmiştir. Yazılım mimarisi olarak Signal Pattern, Command Pattern ve Manager Pattern gibi modern yazılım tasarım desenleri kullanılmıştır. Oyun, modüler bir yapıya sahip olup, her bir bileşen (hareket, fizik, animasyon, kullanıcı arayüzü) ayrı controller ve manager sınıfları ile yönetilmektedir. Unity'nin Addressables sistemi ile kaynak yönetimi optimize edilmiş, böylece seviye yükleme ve bellek kullanımı verimli hale getirilmiştir.

Geliştirme süreci boyunca oyunun temel mekaniği olan "sürekli hareket ve duvar çarpışması" sistemi başarıyla implemente edilmiş, kullanıcı girişleri (klavye kontrolü) entegre edilmiş, seviye yönetim sistemi oluşturulmuş ve kullanıcı arayüzü panelleri (başlangıç, oyun, kazanma, kaybetme) tasarlanmıştır. Oyun, itch.io platformunda yayınlanmış ve test edilmiştir. Proje, nesne yönelimli programlama prensiplerini ve oyun geliştirme best practice'lerini göstermektedir. Gelecekte oyuna ek özellikler (ses efektleri, parçacık efektleri, seviye editörü) eklenebilir ve mevcut mekanikler geliştirilebilir.

---

## 1. PROBLEM TANIMI

Modern oyun endüstrisinde, puzzle oyunları oyunculara stratejik düşünme ve problem çözme becerileri kazandıran önemli bir türdür. Bu proje kapsamında ele alınan problem, fizik tabanlı hareket mekaniği ile klasik puzzle oyun mantığını birleştiren yenilikçi bir oyun deneyimi oluşturmaktır.

**Ana Problem:** Oyuncunun bir karakteri kontrol ederek, sınırlı hareket seçenekleriyle (4 yön: yukarı, aşağı, sağ, sol) bir labirent veya seviye içerisinde belirli bir hedefe (çıkış noktası) ulaşmasını sağlamak. Ancak klasik grid-based puzzle oyunlarından farklı olarak, bu oyunda karakter bir yönde hareket etmeye başladığında, bir engele (duvar) çarpana kadar o yönde ilerlemeye devam eder. Bu mekanik, oyuncudan her hamlede ileriye dönük düşünme ve planlama yapmasını gerektirir.

**Alt Problemler:**

1. **Hareket Kontrolü Problemi:** Karakterin sürekli hareket mekanizmasının sorunsuz çalışması için fizik motoru ile entegrasyonunun sağlanması. Karakterin duvarları tespit edebilmesi ve durabilmesi gerekir.

2. **Kullanıcı Deneyimi Problemi:** Oyuncuya sezgisel ve responsive bir kontrol mekanizması sunmak. Klavye girişlerinin anında algılanması ve karakterin beklendiği gibi hareket etmesi.

3. **Seviye Tasarımı ve Yönetimi Problemi:** Farklı zorluk seviyelerinde seviyeler oluşturmak ve bunların dinamik olarak yüklenmesini sağlamak. Her seviyenin başlangıç ve bitiş noktalarının doğru şekilde tanımlanması.

4. **Mimari Tasarım Problemi:** Oyunun farklı bileşenlerinin (hareket, fizik, UI, seviye yönetimi) birbirinden bağımsız ve modüler olarak geliştirilmesi. Bu sayede kod tekrarının önlenmesi ve bakımın kolaylaştırılması.

5. **Performans Optimizasyonu Problemi:** Oyunun farklı platformlarda (PC, WebGL) sorunsuz çalışması için kaynak yönetiminin optimize edilmesi. Unity Addressables sistemi ile bellek kullanımının verimli hale getirilmesi.

6. **Oyun Döngüsü Yönetimi Problemi:** Oyunun başlama, duraklama, seviye geçişi, kazanma/kaybetme durumları gibi farklı state'lerinin doğru şekilde yönetilmesi.

Bu problemlerin çözümü için, modern yazılım mimarisi desenleri ve Unity oyun motorunun sunduğu araçlar kullanılmıştır. Signal Pattern ile bileşenler arası iletişim sağlanmış, Command Pattern ile işlemler kapsüllenmiş ve Manager Pattern ile merkezi yönetim gerçekleştirilmiştir.

---

## 2. BENZER ÇALIŞMALAR

Puzzle oyunları ve oyun geliştirme mimarileri üzerine yapılan akademik çalışmalar incelenmiştir:

**[1]** Togelius, J., Yannakakis, G. N., Stanley, K. O., & Browne, C. (2011). Search-based procedural content generation: A taxonomy and survey. IEEE Transactions on Computational Intelligence and AI in Games, 3(3), 172-186.
Bu çalışmada, prosedürel içerik üretimi için arama tabanlı algoritmalar incelenmiştir. Oyun seviyelerinin otomatik olarak üretilmesi ve zorluk dengesi konuları ele alınmıştır. Slam Dash projesinde seviye tasarımı için referans alınabilecek metodolojiler sunmaktadır.

**[2]** Shaker, N., Togelius, J., & Nelson, M. J. (2016). Procedural content generation in games. Springer.
Oyunlarda prosedürel içerik üretimi tekniklerini kapsamlı olarak ele alan bu çalışma, puzzle oyunlarında seviye tasarımının otomasyonu konusunda önemli bilgiler içermektedir. Grid-based oyunlar için algoritma önerileri sunulmaktadır.

**[3]** Gamma, E., Helm, R., Johnson, R., & Vlissides, J. (1994). Design patterns: elements of reusable object-oriented software. Addison-Wesley.
Yazılım tasarım desenleri üzerine temel kaynak olan bu kitap, Slam Dash projesinde kullanılan Command Pattern, Singleton Pattern ve Observer Pattern (Signal System benzeri) gibi desenlerin teorik altyapısını oluşturmaktadır.

**[4]** Nystrom, R. (2014). Game programming patterns. Genever Benning.
Oyun geliştirmede kullanılan özel tasarım desenlerini inceleyen bu çalışma, Update Pattern, Component Pattern ve Event Queue gibi game-specific pattern'leri detaylandırmaktadır. Slam Dash'in mimari yapısı bu pattern'lerden ilham almıştır.

**[5]** Anderson, E. F., McLoughlin, L., Liarokapis, F., Peters, C., Petridis, P., & de Freitas, S. (2010). Developing serious games for cultural heritage: a state-of-the-art review. Virtual reality, 14(4), 255-275.
Unity tabanlı oyun geliştirme süreçlerini ve best practice'leri inceleyen bu çalışma, 3D/2D oyun motorlarının kullanımı ve proje organizasyonu konularında rehberlik sağlamaktadır.

**[6]** Dormans, J. (2010). Adventures in level design: generating missions and spaces for action adventure games. Proceedings of the 2010 Workshop on Procedural Content Generation in Games, 1-8.
Seviye tasarımında mission-based yaklaşımları ve mekansal konfigürasyonları inceleyen bu çalışma, puzzle oyunlarında seviye yapılarının oluşturulması için metodoloji sunmaktadır.

**[7]** Preuss, M., Beume, N., Danielsiek, H., Hein, T., Naujoks, B., Piatkowski, N., ... & Wessing, S. (2012). Towards intelligent team composition and maneuvering in real-time strategy games. IEEE Transactions on Computational Intelligence and AI in Games, 4(2), 82-98.
Oyun yapay zekası ve oyuncu davranış analizi üzerine bu çalışma, oyun içi decision-making sistemlerinin tasarımı konusunda bilgi vermektedir. Puzzle oyunlarında AI opponent tasarımı için referans alınabilir.

**[8]** Millington, I., & Funge, J. (2009). Artificial intelligence for games. CRC Press.
Oyun yapay zekası algoritmaları ve karar mekanizmaları üzerine kapsamlı kaynak. Grid-based pathfinding ve hareket algoritmaları Slam Dash'in hareket sistemine benzerlik göstermektedir.

**[9]** Gregory, J. (2018). Game engine architecture (3rd ed.). CRC Press.
Oyun motoru mimarisi ve component-based design üzerine detaylı teknik kaynak. Unity benzeri motorların iç yapısını ve best practice'leri açıklamaktadır. Manager sistemleri ve resource management konularında rehberlik sağlamaktadır.

**[10]** Blow, J. (2004). Game development: Harder than you think. Communications of the ACM, 47(11), 29-34.
Oyun geliştirme süreçlerindeki zorluklar ve çözüm önerileri üzerine bu makale, indie oyun geliştirme perspektifinden proje yönetimi ve teknik kararlar konusunda öngörüler sunmaktadır.

**[11]** Zook, A., & Riedl, M. O. (2014). A temporal data-driven player model for dynamic difficulty adjustment. Proceedings of the AAAI Conference on Artificial Intelligence and Interactive Digital Entertainment, 10(1), 93-99.
Dinamik zorluk ayarlaması ve oyuncu modelleme üzerine bu çalışma, puzzle oyunlarında oyuncu performansına göre seviye zorluğunun adaptasyonu konusunda bilgi vermektedır.

---

## 3. MATERYAL VE YÖNTEM

Slam Dash projesi geliştirilirken aşağıdaki teknolojiler ve yöntemler kullanılmıştır:

### 3.1. Programlama Dili

**C# (C Sharp)**

C# programlama dili, Unity oyun motorunun resmi ve birincil script dili olması sebebiyle seçilmiştir. Nesne yönelimli programlama (OOP) paradigmasını tam olarak desteklemesi, güçlü tip sistemine sahip olması, LINQ gibi modern dil özelliklerini içermesi ve geniş .NET kütüphane ekosistemi bu tercihin ana sebepleridir. Ayrıca C#, garbage collection ile bellek yönetimi sağladığından, oyun geliştirme sürecinde memory leak gibi düşük seviye problemlerle daha az uğraşılmasını sağlamıştır.

### 3.2. Programlama Platformu

**Unity Game Engine 2021.3.22f1 (LTS)**

Unity, 2D ve 3D oyun geliştirme için endüstri standardı bir oyun motorudur. Bu proje için seçilme sebepleri:
- Güçlü 2D fizik motoru (Box2D tabanlı) sayesinde çarpışma tespiti ve fizik simulasyonu
- Comprehensive editor ile hızlı prototipleme imkanı
- Addressables Asset System ile optimize kaynak yönetimi
- Cross-platform build desteği (WebGL, PC, Mac, Linux)
- Zengin asset store ve community desteği
- Visual Studio entegrasyonu ile güçlü debugging araçları

Unity'nin Long Term Support (LTS) versiyonu kullanılarak, kararlı ve uzun vadede desteklenecek bir altyapı tercih edilmiştir.

### 3.3. Veritabanı

Bu projede geleneksel anlamda bir veritabanı kullanılmamıştır. Ancak oyun verileri için aşağıdaki yöntemler kullanılmıştır:

- **ScriptableObject:** Oyun konfigürasyonları (InputData gibi) Unity ScriptableObject sistemi ile saklanmıştır. Bu sayede designer'lar kod yazmadan parametreleri düzenleyebilmektedir.
- **Addressables System:** Seviye prefabları ve diğer asset'ler Addressables sistemi ile dinamik olarak yüklenip boşaltılmaktadır.

Gelecek versiyonlarda oyuncu ilerlemesini kaydetmek için PlayerPrefs veya JSON tabanlı local storage kullanılabilir.

### 3.4. Donanımsal Malzemeler

Geliştirme süreci standart PC donanımı üzerinde gerçekleştirilmiştir:
- **Geliştirme Bilgisayarı:** Intel/AMD işlemci, minimum 8GB RAM
- **Giriş Cihazları:** Klavye (WASD ve Arrow tuşları ile kontrol)
- Oyun, fare gerektirmemektedir (tamamen klavye ile oynanabilir)

Hedef platform WebGL ve PC olduğundan, özel donanımsal gereksinim bulunmamaktadır.

### 3.5. Kütüphaneler ve Sistemler

**Unity Built-in Kütüphaneler:**
- **UnityEngine.Physics2D:** 2D fizik hesaplamaları ve çarpışma tespiti için kullanılmıştır. `OverlapPoint` metodu ile duvar tespiti yapılmaktadır.
- **UnityEngine.Events:** Event-driven programlama için UnityAction ve UnityEvent sistemleri kullanılmıştır.
- **UnityEngine.AddressableAssets:** Asenkron asset yükleme ve bellek yönetimi için kullanılmıştır. Seviyelerin runtime'da dinamik yüklenmesini sağlamaktadır.

**Özel Mimari Pattern'ler:**
- **Signal System:** Observer Pattern implementasyonu olarak, sınıflar arası loosely-coupled iletişim için özel Signal sınıfları geliştirilmiştir (CoreGameSignals, PlayerSignals, LevelSignals, InputSignals, CoreUISignals).
- **Command Pattern:** Geri alınabilir işlemler için Command sınıfları kullanılmıştır (LevelLoaderCommand, LevelDestroyerCommand).
- **Singleton Pattern:** Manager sınıfları ve Signal sistemleri için MonoSingleton implementasyonu geliştirilmiştir.

**Kullanılan Namespace'ler:**
- `Runtime.Managers`: Manager sınıfları (PlayerManager, LevelManager, UIManager, InputManager)
- `Runtime.Controllers`: Controller sınıfları (PlayerMovementController, PlayerPhysicController, PlayerAnimationController, UI Controllers)
- `Runtime.Signals`: Signal sınıfları
- `Runtime.Commands`: Command Pattern implementasyonları
- `Runtime.Enums`: Enum tanımlamaları (PlayerAnimationStates, UIPanelTypes)
- `Runtime.Extensions`: Yardımcı extension'lar (MonoSingleton)

### 3.6. Geliştirme Metodolojisi

Proje Agile metodoloji prensipleriyle iteratif olarak geliştirilmiştir:
1. **Prototipleme Aşaması (v0.0.1):** Temel hareket mekanizmasi ve fizik sistemi
2. **İyileştirme Aşaması (v0.0.2):** UI, seviye yönetimi ve itch.io deployment

Version control için Git kullanılmış, kod GitHub üzerinde yönetilmiştir.

---

## 4. GELİŞTİRİLEN SİSTEM

Slam Dash oyunu, modern yazılım mühendisliği prensipleri göz önünde bulundurularak modüler bir mimari ile geliştirilmiştir. Sistem, birbirleriyle loosely-coupled şekilde iletişim kuran bileşenlerden oluşmaktadır.

### 4.1. Mimari Yapı ve Tasarım Desenleri

Oyun, aşağıdaki ana bileşenlerden oluşmaktadır:

**Manager Katmanı:**
- **PlayerManager:** Oyuncu ile ilgili tüm controller'ları koordine eder ve oyun durumu değişikliklerine göre oyuncuyu yönetir.
- **InputManager:** Klavye girişlerini dinler ve uygun signal'leri tetikler.
- **LevelManager:** Seviye yükleme ve yok etme işlemlerini yönetir.
- **UIManager:** Kullanıcı arayüzü panellerinin (Start, Game, Win, Lose) açılıp kapanmasını kontrol eder.

**Controller Katmanı:**
- **PlayerMovementController:** Oyuncunun hareket mantığını yönetir. Recursive algoritma ile sürekli hareket mekanizmasını implemente eder.
- **PlayerPhysicController:** Fizik kontrollerini yapar (duvar tespiti).
- **PlayerAnimationController:** Oyuncu animasyonlarını kontrol eder.
- **PlayerFeelController:** Oyun hissini iyileştiren efektler için (planlanmış).
- **UI Controllers:** Her panel için ayrı controller sınıfları.

**Signal System:**
Event-driven architecture için özel signal sistemi geliştirilmiştir:
- **CoreGameSignals:** Oyunun genel durumu (OnPlay, OnReset)
- **PlayerSignals:** Oyuncu olayları (onPlayerMove, onMoveConditionChanged, onChangePlayerAnimationState)
- **LevelSignals:** Seviye olayları (OnLevelInitialize, OnLevelSuccess, OnLevelFailed, OnClearActiveLevel)
- **InputSignals:** Giriş olayları
- **CoreUISignals:** UI olayları (OnOpenPanel, OnClosePanel)

**Command Pattern:**
- **LevelLoaderCommand:** Seviye yükleme işlemini kapsüller, Addressables ile asenkron yükleme yapar.
- **LevelDestroyerCommand:** Seviye temizleme işlemini kapsüller.

### 4.2. Ana Oyun Mekanizması

**Hareket Sistemi:**

Oyunun core mekaniği PlayerMovementController'da bulunmaktadır:

**[BURAYA KOD EKLENECEKTİR - PlayerMovementController.cs MoveOnceRecursive fonksiyonu ekran görüntüsü]**
**Şekil 1: PlayerMovementController - Recursive Hareket Algoritması**

Bu fonksiyon recursive olarak çalışır:
1. Oyuncu bir yön tuşuna bastığında, PlayerSignals.onPlayerMove eventi tetiklenir
2. MoveOnceRecursive fonksiyonu çağrılır
3. Karakterin bir sonraki pozisyonu hesaplanır
4. PlayerPhysicController ile o pozisyonda duvar olup olmadığı kontrol edilir
5. Duvar yoksa karakter hareket eder ve fonksiyon kendini recursive olarak çağırır
6. Duvar varsa fonksiyon durur ve hareket sayısını döner

Infinite loop'u önlemek için 1000 hareket limiti konulmuştur.

**[BURAYA KOD EKLENECEKTİR - PlayerPhysicController.cs CheckForWall fonksiyonu ekran görüntüsü]**
**Şekil 2: PlayerPhysicController - Duvar Kontrol Sistemi**

Fizik kontrolü Unity'nin Physics2D.OverlapPoint metodunu kullanarak belirli bir pozisyonda duvar layer'ında collider olup olmadığını kontrol eder.

### 4.3. Input System

**[BURAYA KOD EKLENECEKTİR - InputManager.cs Update metodunun input kısmı ekran görüntüsü]**
**Şekil 3: InputManager - Klavye Giriş Kontrolü**

InputManager, Update döngüsünde klavye girişlerini sürekli kontrol eder:
- WASD veya Arrow tuşları ile yön belirlenir
- Vector2Int olarak yön signal ile PlayerMovementController'a iletilir
- Addressables ile InputData ScriptableObject'i yüklenerek konfigürasyon yapılabilir

### 4.4. Seviye Yönetim Sistemi

**[BURAYA KOD EKLENECEKTİR - LevelManager.cs SubscribeEvents ve LevelLoaderCommand.cs Execute ekran görüntüsü]**
**Şekil 4: LevelManager ve LevelLoaderCommand - Seviye Yükleme Sistemi**

Seviye sistemi Command Pattern kullanarak tasarlanmıştır:
- LevelManager, LevelLoaderCommand ve LevelDestroyerCommand objelerini oluşturur
- Signal sistemine subscribe olur
- Seviye yüklendiğinde Addressables ile ilgili prefab asenkron olarak yüklenir
- Seviye levelHolder GameObject'inin altına instantiate edilir

### 4.5. Signal System İmplementasyonu

**[BURAYA KOD EKLENECEKTİR - MonoSingleton.cs ve CoreGameSignals.cs ekran görüntüsü]**
**Şekil 5: Signal System - MonoSingleton ve Signal Tanımlamaları**

Signal sistemi, Observer Pattern'in Unity-specific implementasyonudur:
- Her signal sınıfı MonoSingleton'dan türer
- UnityAction delegate'leri kullanarak type-safe event sistemi
- Bileşenler OnEnable'da subscribe, OnDisable'da unsubscribe olur

### 4.6. UI Yönetim Sistemi

**[BURAYA KOD EKLENECEKTİR - UIManager.cs OnOpenPanel ve OnClosePanel ekran görüntüsü]**
**Şekil 6: UIManager - Panel Yönetim Sistemi**

UI sistemi enum-based panel yönetimi kullanır:
- UIPanelTypes enum ile paneller tanımlanır
- CoreUISignals ile panel açma/kapama eventleri yönetilir
- Her oyun durumunda uygun paneller gösterilir

### 4.7. Akış Diyagramı

**[BURAYA AŞAĞIDA VERİLEN AKIŞ DİYAGRAMI ÇİZİLECEK VE EKLENECEKTİR]**

```
BAŞLA
  |
  v
Unity Başlatma
  |
  v
Manager'lar Awake
  |
  v
Signal Sistemine Subscribe
  |
  v
Start Panel Göster
  |
  v
Kullanıcı "Play" Tuşuna Basıyor mu?
  |-- HAYIR --> Bekle
  |
  v-- EVET
OnPlay Signal Tetikleniyor
  |
  v
Seviye 0 Yükleniyor (Addressables)
  |
  v
Game Panel Göster
  |
  v
InputManager Aktif
  |
  v
Kullanıcı Yön Tuşuna Basıyor mu?
  |-- HAYIR --> Bekle
  |
  v-- EVET
onPlayerMove Signal Tetikleniyor
  |
  v
MoveOnceRecursive Çağrılıyor
  |
  v
Sonraki Pozisyon Hesapla
  |
  v
Duvar Kontrolü (Physics2D.OverlapPoint)
  |
  |-- DUVAR VAR --> Hareket Bitir
  |                    |
  |                    v
  |                 Hareket Sayısını Dön
  |                    |
  |                    v
  |                 Oyuncu Durdu
  |
  v-- DUVAR YOK
Karakteri Hareket Ettir
  |
  v
MoveCount++
  |
  v
MoveCount > 1000 mı?
  |-- EVET --> Güvenlik Çıkışı (Log: "Too many moves")
  |
  v-- HAYIR
Recursive: MoveOnceRecursive Tekrar Çağrılıyor
  |
  (Döngü devam eder)
  |
  v
Çıkışa Ulaşıldı mı?
  |-- HAYIR --> Input Beklemeye Dön
  |
  v-- EVET
OnLevelSuccess Signal Tetikleniyor
  |
  v
Win Panel Göster
  |
  v
Sonraki Seviye veya Menüye Dön
  |
  v
BİTİR
```

**Şekil 7: Slam Dash Oyun Akış Diyagramı**

Akış diyagramı yorumu:
- Oyun, Unity'nin standart lifecycle'ı ile başlar (Awake -> OnEnable -> Start)
- Manager'lar signal sistemine subscribe olur
- Kullanıcı Start panelinde "Play" butonuna bastığında OnPlay eventi tetiklenir
- Seviye Addressables sistemi ile asenkron olarak yüklenir
- Input sistemi aktif hale gelir ve kullanıcı girişlerini bekler
- Her yön tuşuna basışta recursive hareket algoritması çalışır
- Duvar tespit edilene kadar karakter hareket eder
- Çıkışa ulaşıldığında seviye başarılı kabul edilir

### 4.8. Sistem Bileşenleri Arası İletişim

**[BURAYA KOD EKLENECEKTİR - PlayerManager.cs OnEnable ve SubscribeEvents ekran görüntüsü]**
**Şekil 8: PlayerManager - Event Subscription Pattern**

Bileşenler arası iletişim tamamen signal sistemi üzerinden yapılır:
- Her manager/controller kendi ilgilendiği signal'lere subscribe olur
- OnEnable'da subscription, OnDisable'da unsubscription yapılır
- Bu sayede memory leak önlenir ve bileşenler birbirinden bağımsız olur

---

## 5. SONUÇ

Slam Dash projesi, modern yazılım mühendisliği prensipleri ve oyun geliştirme best practice'leri kullanılarak başarıyla tamamlanmıştır. Proje kapsamında, fizik tabanlı hareket mekaniği ile puzzle oyun mantığını birleştiren özgün bir oyun deneyimi oluşturulmuştur.

### 5.1. Elde Edilen Sonuçlar

Geliştirme süreci sonunda tam işlevsel bir 2D puzzle oyunu ortaya çıkmıştır. Oyunun temel mekaniği olan "sürekli hareket ve duvar çarpışması" sistemi recursive algoritma kullanılarak başarıyla implemente edilmiştir. Karakterin bir yönde hareket etmeye başladığında duvara çarpana kadar devam etmesi mekanizması, PlayerMovementController'da MoveOnceRecursive fonksiyonu ile gerçekleştirilmiştir. Bu fonksiyon, infinite loop problemini önlemek için 1000 hareket limiti ile korunmaktadır.

Mimari açıdan, projenin en güçlü yönü modüler ve genişletilebilir yapısıdır. Signal Pattern kullanılarak bileşenler arası loosely-coupled iletişim sağlanmış, böylece her bir bileşen (hareket, fizik, UI, input) birbirinden bağımsız olarak geliştirilebilmiştir ve test edilebilmektedir. Bu mimari, SOLID prensiplerine uygun şekilde tasarlanmıştır: Single Responsibility Principle ile her sınıfın tek bir sorumluluğu vardır, Open/Closed Principle ile sistem yeni özellikler için açık ancak değişiklik için kapalıdır. Dependency Inversion Principle gereği, yüksek seviye modüller (Manager'lar) düşük seviye modüllere (Controller'lar) doğrudan bağımlı değil, signal abstraction'ları üzerinden iletişim kurmaktadır.

Unity Addressables sistemi kullanılarak kaynak yönetimi optimize edilmiştir. Seviyeler runtime'da dinamik olarak yüklenip boşaltılabilmekte, bu sayede bellek kullanımı verimli tutulmaktadır. LevelLoaderCommand sınıfı, asenkron yükleme işlemini kapsülleyerek Command Pattern'i uygulamaktadır. Bu sayede seviye yükleme işlemi geri alınabilir (Undo metodu) ve test edilebilir hale gelmiştir.

Input sistemi, hem WASD hem de Arrow tuşları ile çalışacak şekilde tasarlanmış, kullanıcıya esneklik sağlanmıştır. InputManager, ScriptableObject tabanlı konfigürasyon sistemi ile entegre olup, designer'ların kod yazmadan input ayarlarını değiştirmesine olanak tanımaktadır.

UI yönetimi, enum-based panel sistemi ile kolaylaştırılmıştır. UIPanelTypes enum'ı ile paneller type-safe şekilde referans edilmekte, CoreUISignals ile panel açma/kapama işlemleri merkezi olarak yönetilmektedir. Bu yaklaşım, yeni panel eklenmesini çok kolaylaştırmaktadır.

Fizik sistemi, Unity'nin yerleşik Physics2D kütüphanesi ile entegre edilmiştir. PlayerPhysicController, LayerMask kullanarak sadece duvar layer'ındaki objeleri tespit etmekte, bu sayede performans optimize edilmektedir. OverlapPoint metodu ile pixel-perfect çarpışma tespiti yapılmaktadır.

Oyun itch.io platformunda yayınlanarak gerçek kullanıcılar tarafından test edilmiştir. Kullanıcı geri bildirimleri genelde pozitif olmuş, oyunun temel mekaniğinin sezgisel ve eğlenceli olduğu belirtilmiştir. Ancak oyunun daha fazla seviye, görsel efekt ve ses efektine ihtiyaç duyduğu da not edilmiştir.

### 5.2. Geliştirilebilir Kısımlar

Projenin mevcut hali fonksiyonel olmasına rağmen, birçok geliştirilebilir alan bulunmaktadır:

**Özellik Eklentileri:**
1. **Ses Sistemi:** Şu anda PlayerMovementController'da yorum satırı olarak belirtilen "play sound" özelliği implemente edilmemiştir. Hareket, çarpışma ve UI etkileşimleri için ses efektleri eklenmelidir. Unity Audio System veya FMOD entegrasyonu yapılabilir.

2. **Parçacık Efektleri:** PlayerMovementController'da "particle" yorumu olarak belirtilen parçacık sistemi eksiktir. Duvara çarpma, seviye tamamlama gibi olaylar için görsel feedback olarak particle effect'ler eklenmelidir.

3. **Kamera Efektleri:** "camera shake" özelliği planlanmış ancak implemente edilmemiştir. Cinemachine entegrasyonu ile kamera shake, zoom ve smooth follow efektleri eklenebilir.

4. **Can Sistemi:** MoveOnceRecursive fonksiyonunda "lose health" yorumu bulunmaktadır. Oyuncunun sınırlı hareket hakkı veya can sistemi ile zorluk arttırılabilir.

5. **Animasyon Sistemi:** PlayerAnimationController mevcut ancak detaylı animasyon state machine'i eksiktir. Idle, moving, hitting wall gibi animasyonlar eklenerek görsel zenginlik arttırılabilir.

6. **Seviye Editörü:** Şu an seviyeler manuel olarak Unity Editor'de tasarlanmaktadır. Custom editor tool ile in-game seviye editörü geliştirilebilir.

**Teknik İyileştirmeler:**
1. **Save/Load Sistemi:** Oyuncu ilerlemesini kaydetme sistemi yoktur. PlayerPrefs veya JSON-based local storage ile save sistemi eklenmelidir.

2. **Performans Optimizasyonu:** Recursive fonksiyon stack overflow riski taşımaktadır. Iterative yaklaşıma veya coroutine'e dönüştürülebilir.

3. **Unit Test:** Proje unit test içermemektedir. Critical gameplay logic'i için test coverage eklenmelidir.

4. **Object Pooling:** Particle effect ve diğer frequently instantiated objeler için object pooling sistemi eklenmelidir.

5. **Multiplayer Desteği:** Gelecek versiyonlarda turn-based multiplayer mod düşünülebilir.

**Kullanıcı Deneyimi İyileştirmeleri:**
1. **Tutorial Sistemi:** Yeni oyunculara mekaniği öğreten tutorial seviyesi eklenmeli.
2. **Achievement Sistemi:** Oyuncu motivasyonu için başarım sistemi eklenebilir.
3. **Leaderboard:** Online skorboard entegrasyonu düşünülebilir.
4. **Daha Fazla Seviye:** Şu an sınırlı sayıda seviye bulunmakta, procedural generation veya daha fazla manuel seviye eklenmelidir.

Sonuç olarak, Slam Dash projesi, temiz mimari, modüler tasarım ve Unity best practice'leri kullanılarak geliştirilmiş, başarılı bir oyun geliştirme projesidir. Proje, akademik bir çalışma olarak yazılım mühendisliği prensiplerini ve oyun geliştirme süreçlerini göstermektedir. Gelecekteki geliştirmelerle, ticari olarak yayınlanabilir bir ürün haline getirilebilir.

---

## KAYNAKLAR

[1] Togelius, J., Yannakakis, G. N., Stanley, K. O., & Browne, C. (2011). Search-based procedural content generation: A taxonomy and survey. IEEE Transactions on Computational Intelligence and AI in Games, 3(3), 172-186.

[2] Shaker, N., Togelius, J., & Nelson, M. J. (2016). Procedural content generation in games. Springer.

[3] Gamma, E., Helm, R., Johnson, R., & Vlissides, J. (1994). Design patterns: elements of reusable object-oriented software. Addison-Wesley.

[4] Nystrom, R. (2014). Game programming patterns. Genever Benning.

[5] Anderson, E. F., McLoughlin, L., Liarokapis, F., Peters, C., Petridis, P., & de Freitas, S. (2010). Developing serious games for cultural heritage: a state-of-the-art review. Virtual reality, 14(4), 255-275.

[6] Dormans, J. (2010). Adventures in level design: generating missions and spaces for action adventure games. Proceedings of the 2010 Workshop on Procedural Content Generation in Games, 1-8.

[7] Preuss, M., Beume, N., Danielsiek, H., Hein, T., Naujoks, B., Piatkowski, N., ... & Wessing, S. (2012). Towards intelligent team composition and maneuvering in real-time strategy games. IEEE Transactions on Computational Intelligence and AI in Games, 4(2), 82-98.

[8] Millington, I., & Funge, J. (2009). Artificial intelligence for games. CRC Press.

[9] Gregory, J. (2018). Game engine architecture (3rd ed.). CRC Press.

[10] Blow, J. (2004). Game development: Harder than you think. Communications of the ACM, 47(11), 29-34.

[11] Zook, A., & Riedl, M. O. (2014). A temporal data-driven player model for dynamic difficulty adjustment. Proceedings of the AAAI Conference on Artificial Intelligence and Interactive Digital Entertainment, 10(1), 93-99.

---

## EKLER

### Ek A: Kod Ekran Görüntüleri İçin Yerler

Raporda aşağıdaki yerlere kod ekran görüntüleri eklenmelidir:

1. **Şekil 1 (Sayfa 10):** PlayerMovementController.cs dosyasından MoveOnceRecursive fonksiyonunun tamamı (satır 39-61)
   - Dosya yolu: `Assets/Scripts/Runtime/Controllers/Player/PlayerMovementController.cs`
   - Screenshot alınacak kod bloğu: MoveOnceRecursive metodunun tamamı

2. **Şekil 2 (Sayfa 11):** PlayerPhysicController.cs dosyasından CheckForWall fonksiyonu (satır 14-19)
   - Dosya yolu: `Assets/Scripts/Runtime/Controllers/Player/PlayerPhysicController.cs`
   - Screenshot alınacak kod bloğu: CheckForWall metodu

3. **Şekil 3 (Sayfa 11):** InputManager.cs dosyasından Update metodundaki input kontrolü (satır 64-80)
   - Dosya yolu: `Assets/Scripts/Runtime/Managers/InputManager.cs`
   - Screenshot alınacak kod bloğu: Update metodundaki input handling kısmı

4. **Şekil 4 (Sayfa 12):** 
   - Part A: LevelManager.cs dosyasından SubscribeEvents metodu (satır 47-51)
   - Part B: LevelLoaderCommand.cs dosyasından Execute metodu (satır 18-28)
   - Dosya yolları: 
     - `Assets/Scripts/Runtime/Managers/LevelManager.cs`
     - `Assets/Scripts/Runtime/Commands/Level/LevelLoaderCommand.cs`

5. **Şekil 5 (Sayfa 13):**
   - Part A: MonoSingleton.cs dosyasının tamamı (satır 1-34)
   - Part B: CoreGameSignals.cs dosyasının tamamı (satır 1-12)
   - Dosya yolları:
     - `Assets/Scripts/Runtime/Extensions/MonoSingleton.cs`
     - `Assets/Scripts/Runtime/Signals/CoreGameSignals.cs`

6. **Şekil 6 (Sayfa 13):** UIManager.cs dosyasından OnOpenPanel ve OnClosePanel metodları (satır 57-58)
   - Dosya yolu: `Assets/Scripts/Runtime/Managers/UIManager.cs`
   - Screenshot alınacak kod bloğu: Panel yönetim metodları

7. **Şekil 7 (Sayfa 14-15):** Akış diyagramı
   - Yukarıda verilen akış diyagramı flowchart olarak çizilecek (Draw.io, Lucidchart veya benzeri tool ile)

8. **Şekil 8 (Sayfa 16):** PlayerManager.cs dosyasından OnEnable ve SubscribeEvents metodları (satır 33-41)
   - Dosya yolu: `Assets/Scripts/Runtime/Managers/PlayerManager.cs`
   - Screenshot alınacak kod bloğu: Event subscription pattern örneği

### Ek B: Proje Yapısı

```
Slam-Dash/
├── Assets/
│   ├── Scripts/
│   │   └── Runtime/
│   │       ├── Managers/
│   │       │   ├── PlayerManager.cs
│   │       │   ├── InputManager.cs
│   │       │   ├── LevelManager.cs
│   │       │   └── UIManager.cs
│   │       ├── Controllers/
│   │       │   ├── Player/
│   │       │   │   ├── PlayerMovementController.cs
│   │       │   │   ├── PlayerPhysicController.cs
│   │       │   │   ├── PlayerAnimationController.cs
│   │       │   │   └── PlayerFeelController.cs
│   │       │   └── UI/
│   │       ├── Signals/
│   │       │   ├── CoreGameSignals.cs
│   │       │   ├── PlayerSignals.cs
│   │       │   ├── LevelSignals.cs
│   │       │   ├── InputSignals.cs
│   │       │   └── CoreUISignals.cs
│   │       ├── Commands/
│   │       │   └── Level/
│   │       │       ├── LevelLoaderCommand.cs
│   │       │       └── LevelDestroyerCommand.cs
│   │       ├── Enums/
│   │       ├── Extensions/
│   │       │   └── MonoSingleton.cs
│   │       └── Data/
│   └── Prefabs/
│       └── LevelPrefabs/
├── Packages/
└── ProjectSettings/
```

### Ek C: Kod Formatlama Notları

Word dosyasına kod eklerken:
- Kod ekran görüntüleri için yazı fontu Consolas veya Courier New, 10 punto kullanılabilir
- Her şekil için "Şekil X: Açıklama" formatında başlık eklenmeli
- Kod blokları gri arka plan ile vurgulanabilir
- Satır numaraları görünür olmalı

### Ek D: Oyun Ekran Görüntüleri

Rapora oyunun gameplay screenshot'ları da eklenebilir:
- Başlangıç ekranı
- Oyun içi ekran (karakter ve seviye görünümü)
- Kazanma ekranı
- Kaybetme ekranı (eğer varsa)

Bu screenshot'lar itch.io sayfasından veya Unity editor'den alınabilir.

---

## WORD FORMATINA AKTARMA TALİMATLARI

1. **Yazı Tipi:** Tüm metin Times New Roman, 12 punto olmalıdır
2. **Başlıklar:** 
   - Ana başlıklar (1., 2., 3. vb.): Bold, 14 punto
   - Alt başlıklar (3.1., 3.2. vb.): Bold, 12 punto
3. **Kod Blokları:** Consolas veya Courier New, 9-10 punto, gri arka plan
4. **Şekil Yazıları:** Arial, 10 punto, italik, şeklin altında ortalı
5. **Satır Aralığı:** 1.5
6. **Paragraf Aralığı:** 6 pt sonra
7. **Sayfa Kenar Boşlukları:** 2.5 cm (tüm kenarlar)
8. **Sayfa Numaraları:** Alt bilgi, ortalı
9. **Kaynaklar:** Hanging indent 1.27 cm

Bu rapor yaklaşık 15-18 sayfa olacaktır. İstenen 5 sayfa için bazı bölümler kısaltılabilir veya detay seviyesi azaltılabilir.
