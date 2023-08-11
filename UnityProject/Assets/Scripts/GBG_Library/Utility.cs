using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using TMPro;
using System.Collections.Generic;
using System.Collections;
using UnityEditor;

namespace GBGUnity.Utility
{
    /// <summary>
    /// Classe di utilità per la gestione delle scene in Unity.
    /// </summary>
    public static class SceneUtility
    {
        /// <summary>
        /// Carica una scena per nome.
        /// </summary>
        /// <param name="sceneName">Il nome della scena da caricare.</param>
        public static void LoadScene(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }

        /// <summary>
        /// Carica una scena per indice.
        /// </summary>
        /// <param name="sceneIndex">L'indice della scena da caricare.</param>
        public static void LoadScene(int sceneIndex)
        {
            SceneManager.LoadScene(sceneIndex);
        }

        /// <summary>
        /// Carica la scena successiva nella sequenza delle scene.
        /// </summary>
        public static void LoadNextScene()
        {
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            int nextSceneIndex = currentSceneIndex + 1;
            if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
            {
                SceneManager.LoadScene(nextSceneIndex);
            }
        }

        /// <summary>
        /// Carica la scena precedente nella sequenza delle scene.
        /// </summary>
        public static void LoadPreviousScene()
        {
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            int previousSceneIndex = currentSceneIndex - 1;
            if (previousSceneIndex >= 0)
            {
                SceneManager.LoadScene(previousSceneIndex);
            }
        }

        /// <summary>
        /// Verifica se una scena con il nome specificato esiste nel progetto.
        /// </summary>
        /// <param name="sceneName">Il nome della scena da verificare.</param>
        /// <returns>True se la scena esiste, altrimenti false.</returns>
        public static bool SceneExists(string sceneName)
        {
            for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
            {
                string scenePath = SceneUtility.GetScenePathByBuildIndex(i);
                string sceneNameInBuildSettings = System.IO.Path.GetFileNameWithoutExtension(scenePath);
                if (sceneNameInBuildSettings == sceneName)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Restituisce il percorso della scena in base all'indice di build.
        /// </summary>
        /// <param name="buildIndex">L'indice di build della scena.</param>
        /// <returns>Il percorso della scena.</returns>
        private static string GetScenePathByBuildIndex(int buildIndex)
        {
            return SceneUtility.GetScenePathByBuildIndex(buildIndex);
        }
    }
}
namespace GBGUnity.Utility
{
    /// <summary>
    /// Classe di utilità per il sistema di salvataggio in Unity.
    /// </summary>
    public static class SaveSystem
    {
        /// <summary>
        /// Salva un oggetto serializzabile su disco.
        /// </summary>
        /// <typeparam name="T">Il tipo di oggetto da salvare.</typeparam>
        /// <param name="data">L'oggetto da salvare.</param>
        /// <param name="saveFileName">Il nome del file di salvataggio.</param>
        public static void Save<T>(T data, string saveFileName)
        {
            string savePath = GetSavePath(saveFileName);
            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream fileStream = new FileStream(savePath, FileMode.Create))
            {
                formatter.Serialize(fileStream, data);
            }
        }

        /// <summary>
        /// Carica un oggetto salvato da disco.
        /// </summary>
        /// <typeparam name="T">Il tipo di oggetto da caricare.</typeparam>
        /// <param name="saveFileName">Il nome del file di salvataggio.</param>
        /// <returns>L'oggetto caricato dal file di salvataggio.</returns>
        public static T Load<T>(string saveFileName)
        {
            string savePath = GetSavePath(saveFileName);
            if (File.Exists(savePath))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                using (FileStream fileStream = new FileStream(savePath, FileMode.Open))
                {
                    return (T)formatter.Deserialize(fileStream);
                }
            }
            else
            {
                Debug.LogWarning("Save file not found: " + saveFileName);
                return default(T);
            }
        }

        /// <summary>
        /// Verifica se esiste un file di salvataggio.
        /// </summary>
        /// <param name="saveFileName">Il nome del file di salvataggio.</param>
        /// <returns>True se il file di salvataggio esiste, altrimenti false.</returns>
        public static bool SaveFileExists(string saveFileName)
        {
            string savePath = GetSavePath(saveFileName);
            return File.Exists(savePath);
        }

        /// <summary>
        /// Cancella un file di salvataggio.
        /// </summary>
        /// <param name="saveFileName">Il nome del file di salvataggio da cancellare.</param>
        public static void DeleteSaveFile(string saveFileName)
        {
            string savePath = GetSavePath(saveFileName);
            if (File.Exists(savePath))
            {
                File.Delete(savePath);
            }
        }

        /// <summary>
        /// Restituisce il percorso completo del file di salvataggio.
        /// </summary>
        /// <param name="saveFileName">Il nome del file di salvataggio.</param>
        /// <returns>Il percorso completo del file di salvataggio.</returns>
        private static string GetSavePath(string saveFileName)
        {
            return Path.Combine(Application.persistentDataPath, saveFileName);
        }
    }
}
namespace GBGUnity.Utility
{
    /// <summary>
    /// Classe di utilità per il sistema di input da tastiera e mouse in Unity.
    /// </summary>
    public static class InputSystem
    {
        /// <summary>
        /// Verifica se un pulsante sulla tastiera è stato premuto.
        /// </summary>
        /// <param name="buttonName">Il nome del pulsante da controllare.</param>
        /// <returns>True se il pulsante è stato premuto, altrimenti false.</returns>
        public static bool GetButton(string buttonName)
        {
            return Input.GetKey(buttonName);
        }

        /// <summary>
        /// Verifica se un pulsante sulla tastiera è stato premuto in questo frame.
        /// </summary>
        /// <param name="buttonName">Il nome del pulsante da controllare.</param>
        /// <returns>True se il pulsante è stato premuto in questo frame, altrimenti false.</returns>
        public static bool GetButtonDown(string buttonName)
        {
            return Input.GetKeyDown(buttonName);
        }

        /// <summary>
        /// Verifica se un pulsante sulla tastiera è stato rilasciato in questo frame.
        /// </summary>
        /// <param name="buttonName">Il nome del pulsante da controllare.</param>
        /// <returns>True se il pulsante è stato rilasciato in questo frame, altrimenti false.</returns>
        public static bool GetButtonUp(string buttonName)
        {
            return Input.GetKeyUp(buttonName);
        }

        /// <summary>
        /// Verifica se un pulsante del mouse è stato premuto.
        /// </summary>
        /// <param name="button">Il pulsante del mouse da controllare.</param>
        /// <returns>True se il pulsante è stato premuto, altrimenti false.</returns>
        public static bool GetMouseButton(MouseButton button)
        {
            return Input.GetMouseButton((int)button);
        }

        /// <summary>
        /// Verifica se un pulsante del mouse è stato premuto in questo frame.
        /// </summary>
        /// <param name="button">Il pulsante del mouse da controllare.</param>
        /// <returns>True se il pulsante è stato premuto in questo frame, altrimenti false.</returns>
        public static bool GetMouseButtonDown(MouseButton button)
        {
            return Input.GetMouseButtonDown((int)button);
        }

        /// <summary>
        /// Verifica se un pulsante del mouse è stato rilasciato in questo frame.
        /// </summary>
        /// <param name="button">Il pulsante del mouse da controllare.</param>
        /// <returns>True se il pulsante è stato rilasciato in questo frame, altrimenti false.</returns>
        public static bool GetMouseButtonUp(MouseButton button)
        {
            return Input.GetMouseButtonUp((int)button);
        }
        /// <summary>
        /// Controlla se un oggetto viene toccato al click del mouse.
        /// </summary>
        /// <param name="gameObject">L'oggetto da controllare.</param>
        /// <returns>True se l'oggetto viene toccato, altrimenti false.</returns>
        public static bool IsObjectTouched(GameObject gameObject)
        {
            if (gameObject != null)
            {
                if (GetMouseButtonDown(MouseButton.Left))
                {
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    RaycastHit hit;
                    if (Physics.Raycast(ray, out hit))
                    {
                        if (hit.collider.gameObject == gameObject)
                        {
                            return true;
                        }
                    }
                }
            }
            else
            {
                Debug.LogWarning("GameObject is null.");
            }
            return false;
        }
        /// <summary>
        /// Controlla cosa viene toccato al click del mouse.
        /// </summary>
        /// <returns>Il GameObject toccato dal click del mouse, o null se non viene toccato nulla.</returns>
        public static GameObject CheckMouseClick()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                return hit.collider.gameObject;
            }

            return null;
        }
    }

    

    /// <summary>
    /// Enumerazione dei pulsanti del mouse.
    /// </summary>
    public enum MouseButton
    {
        Left = 0,
        Right = 1,
        Middle = 2
    }
}
namespace GBGUnity.Utility
{
    /// <summary>
    /// Classe di utilità per la gestione dell'interfaccia utente in Unity.
    /// </summary>
    public static class UIUtility
    {
        /// <summary>
        /// Imposta il testo di un oggetto TextMeshProUGUI.
        /// </summary>
        /// <param name="textMeshProUGUI">L'oggetto TextMeshProUGUI.</param>
        /// <param name="text">Il testo da impostare.</param>
        public static void SetText(TextMeshProUGUI textMeshProUGUI, string text)
        {
            if (textMeshProUGUI != null)
            {
                textMeshProUGUI.text = text;
            }
            else
            {
                Debug.LogWarning("TextMeshProUGUI is null.");
            }
        }

        /// <summary>
        /// Imposta la visibilità di un oggetto GameObject.
        /// </summary>
        /// <param name="gameObject">L'oggetto GameObject.</param>
        /// <param name="isVisible">True per rendere visibile l'oggetto, false per nasconderlo.</param>
        public static void SetObjectVisibility(GameObject gameObject, bool isVisible)
        {
            if (gameObject != null)
            {
                gameObject.SetActive(isVisible);
            }
            else
            {
                Debug.LogWarning("GameObject is null.");
            }
        }

        /// <summary>
        /// Imposta l'interattività di un oggetto Button.
        /// </summary>
        /// <param name="button">L'oggetto Button.</param>
        /// <param name="isInteractable">True per abilitare l'interattività del pulsante, false per disabilitarla.</param>
        public static void SetButtonInteractivity(UnityEngine.UI.Button button, bool isInteractable)
        {
            if (button != null)
            {
                button.interactable = isInteractable;
            }
            else
            {
                Debug.LogWarning("Button is null.");
            }
        }
    }
}
namespace GBGUnity.Utility
{
    /// <summary>
    /// Classe di utilità per la gestione dell'audio in Unity.
    /// </summary>
    public static class AudioUtility
    {
        private static AudioSource sfxAudioSource;
        private static AudioSource soundtrackAudioSource;

        /// <summary>
        /// Inizializza l'AudioUtility con gli AudioSource per gli SFX e la colonna sonora.
        /// </summary>
        public static void Initialize(AudioSource sfxSource, AudioSource soundtrackSource)
        {
            sfxAudioSource = sfxSource;
            soundtrackAudioSource = soundtrackSource;
        }

        /// <summary>
        /// Riproduce un effetto sonoro (SFX).
        /// </summary>
        /// <param name="sfxClip">Il clip audio dell'effetto sonoro da riprodurre.</param>
        public static void PlaySFX(AudioClip sfxClip)
        {
            sfxAudioSource.PlayOneShot(sfxClip);
        }

        /// <summary>
        /// Riproduce la colonna sonora.
        /// </summary>
        /// <param name="soundtrackClip">Il clip audio della colonna sonora da riprodurre.</param>
        public static void PlaySoundtrack(AudioClip soundtrackClip)
        {
            soundtrackAudioSource.clip = soundtrackClip;
            soundtrackAudioSource.Play();
        }

        /// <summary>
        /// Ferma la riproduzione della colonna sonora.
        /// </summary>
        public static void StopSoundtrack()
        {
            soundtrackAudioSource.Stop();
        }

        /// <summary>
        /// Esegue un fade-in della colonna sonora con una durata specificata.
        /// </summary>
        /// <param name="fadeDuration">La durata del fade-in in secondi.</param>
        public static void FadeIn(float fadeDuration)
        {
            soundtrackAudioSource.volume = 0f;
            soundtrackAudioSource.Play();

            float startTime = Time.time;
            float endTime = startTime + fadeDuration;
            while (Time.time < endTime)
            {
                float elapsedTime = Time.time - startTime;
                float normalizedTime = elapsedTime / fadeDuration;
                float volume = Mathf.Lerp(0f, 1f, normalizedTime);
                soundtrackAudioSource.volume = volume;
                // Puoi eventualmente aggiungere un ritardo con yield return null; qui se desideri che l'aggiornamento sia meno frequente.
            }
        }

        /// <summary>
        /// Esegue un fade-out della colonna sonora con una durata specificata e la ferma alla fine del fade-out.
        /// </summary>
        /// <param name="fadeDuration">La durata del fade-out in secondi.</param>
        public static void FadeOut(float fadeDuration)
        {
            float startTime = Time.time;
            float endTime = startTime + fadeDuration;
            while (Time.time < endTime)
            {
                float elapsedTime = Time.time - startTime;
                float normalizedTime = elapsedTime / fadeDuration;
                float volume = Mathf.Lerp(1f, 0f, normalizedTime);
                soundtrackAudioSource.volume = volume;
                // Puoi eventualmente aggiungere un ritardo con yield return null; qui se desideri che l'aggiornamento sia meno frequente.
            }

            soundtrackAudioSource.Stop();
        }
    }
}
namespace GBGUnity.Utility
{
    /// <summary>
    /// Classe di utilità per la gestione del tempo nel gioco.
    /// </summary>
    public static class TimeUtility
    {
        private static float timeScale = 1f;

        /// <summary>
        /// Restituisce il tempo trascorso in secondi.
        /// </summary>
        public static float Time
        {
            get { return UnityEngine.Time.time; }
        }

        /// <summary>
        /// Restituisce il tempo trascorso in secondi dalla scena è stata avviata.
        /// </summary>
        public static float RealTime
        {
            get { return UnityEngine.Time.realtimeSinceStartup; }
        }

        /// <summary>
        /// Restituisce il tempo trascorso in secondi dall'ultimo frame.
        /// </summary>
        public static float DeltaTime
        {
            get { return UnityEngine.Time.deltaTime * timeScale; }
        }

        /// <summary>
        /// Restituisce il tempo trascorso in secondi dall'ultimo frame non ponderato dal time scale.
        /// </summary>
        public static float UnscaledDeltaTime
        {
            get { return UnityEngine.Time.unscaledDeltaTime; }
        }

        /// <summary>
        /// Restituisce o imposta la scala del tempo.
        /// </summary>
        public static float TimeScale
        {
            get { return timeScale; }
            set { timeScale = value; }
        }

        /// <summary>
        /// Mette in pausa il gioco impostando il time scale a 0.
        /// </summary>
        public static void PauseGame()
        {
            TimeScale = 0f;
        }

        /// <summary>
        /// Riprende il gioco impostando il time scale al valore precedente alla pausa.
        /// </summary>
        public static void ResumeGame()
        {
            TimeScale = 1f;
        }
    }
}
namespace GBGUnity.Utility
{
    public static class AnimationUtility
    {
        /// <summary>
        /// Avvia l'animazione su un oggetto specifico.
        /// </summary>
        /// <param name="gameObject">L'oggetto su cui avviare l'animazione.</param>
        /// <param name="animationClip">La clip di animazione da avviare.</param>
        public static void PlayAnimation(GameObject gameObject, AnimationClip animationClip)
        {
            Animator animator = gameObject.GetComponent<Animator>();
            if (animator != null)
            {
                animator.Play(animationClip.name);
            }
            else
            {
                Debug.LogWarning("Animator component not found on the game object: " + gameObject.name);
            }
        }

        /// <summary>
        /// Avvia l'animazione su un oggetto specifico con un ritardo.
        /// </summary>
        /// <param name="gameObject">L'oggetto su cui avviare l'animazione.</param>
        /// <param name="animationClip">La clip di animazione da avviare.</param>
        /// <param name="delay">Il ritardo in secondi prima di avviare l'animazione.</param>
        public static void PlayAnimationWithDelay(GameObject gameObject, AnimationClip animationClip, float delay)
        {
            Animator animator = gameObject.GetComponent<Animator>();
            if (animator != null)
            {
                animator.CrossFadeInFixedTime(animationClip.name, delay);
            }
            else
            {
                Debug.LogWarning("Animator component not found on the game object: " + gameObject.name);
            }
        }

        /// <summary>
        /// Interrompe l'animazione su un oggetto specifico.
        /// </summary>
        /// <param name="gameObject">L'oggetto su cui interrompere l'animazione.</param>
        public static void StopAnimation(GameObject gameObject)
        {
            Animator animator = gameObject.GetComponent<Animator>();
            if (animator != null)
            {
                animator.speed = 0f;
            }
            else
            {
                Debug.LogWarning("Animator component not found on the game object: " + gameObject.name);
            }
        }

        /// <summary>
        /// Ripristina l'animazione su un oggetto specifico.
        /// </summary>
        /// <param name="gameObject">L'oggetto su cui ripristinare l'animazione.</param>
        public static void ResumeAnimation(GameObject gameObject)
        {
            Animator animator = gameObject.GetComponent<Animator>();
            if (animator != null)
            {
                animator.speed = 1f;
            }
            else
            {
                Debug.LogWarning("Animator component not found on the game object: " + gameObject.name);
            }
        }
    }
}
namespace GBGUnity.Utility
{
    /// <summary>
    /// Classe di utilità per il sistema di inventario.
    /// </summary>
    public class InventorySystem
    {
        /// <summary>
        /// Classe che rappresenta un oggetto nel inventario.
        /// </summary>
        public class Item
        {
            /// <summary>
            /// Il nome dell'oggetto.
            /// </summary>
            public string Name { get; set; }

            /// <summary>
            /// La descrizione dell'oggetto.
            /// </summary>
            public string Description { get; set; }

            /// <summary>
            /// La quantità dell'oggetto.
            /// </summary>
            public int Quantity { get; set; }
        }

        private List<Item> items;

        /// <summary>
        /// Costruttore di default per l'InventorySystem.
        /// </summary>
        public InventorySystem()
        {
            items = new List<Item>();
        }

        /// <summary>
        /// Aggiunge un oggetto all'inventario.
        /// </summary>
        /// <param name="item">L'oggetto da aggiungere.</param>
        public void AddItem(Item item)
        {
            items.Add(item);
        }

        /// <summary>
        /// Rimuove un oggetto dall'inventario.
        /// </summary>
        /// <param name="item">L'oggetto da rimuovere.</param>
        public void RemoveItem(Item item)
        {
            items.Remove(item);
        }

        /// <summary>
        /// Ottiene tutti gli oggetti nell'inventario.
        /// </summary>
        /// <returns>Una lista di tutti gli oggetti nell'inventario.</returns>
        public List<Item> GetAllItems()
        {
            return items;
        }

        /// <summary>
        /// Ottiene un oggetto specifico nell'inventario in base al suo nome.
        /// </summary>
        /// <param name="name">Il nome dell'oggetto da ottenere.</param>
        /// <returns>L'oggetto corrispondente al nome specificato, null se non viene trovato.</returns>
        public Item GetItemByName(string name)
        {
            return items.Find(item => item.Name == name);
        }
    }
}
namespace GBGUnity.Utility
{
    /// <summary>
    /// Classe di utilità per funzioni matematiche.
    /// </summary>
    public static class MathUtility
    {
        /// <summary>
        /// Calcola la distanza tra due punti nel piano XY.
        /// </summary>
        /// <param name="point1">Il primo punto.</param>
        /// <param name="point2">Il secondo punto.</param>
        /// <returns>La distanza tra i due punti.</returns>
        public static float Distance(Vector2 point1, Vector2 point2)
        {
            return Vector2.Distance(point1, point2);
        }

        /// <summary>
        /// Effettua un'operazione di somma tra due vettori.
        /// </summary>
        /// <param name="vector1">Il primo vettore.</param>
        /// <param name="vector2">Il secondo vettore.</param>
        /// <returns>La somma dei due vettori.</returns>
        public static Vector3 VectorAddition(Vector3 vector1, Vector3 vector2)
        {
            return vector1 + vector2;
        }

        /// <summary>
        /// Effettua un'operazione di sottrazione tra due vettori.
        /// </summary>
        /// <param name="vector1">Il primo vettore.</param>
        /// <param name="vector2">Il secondo vettore.</param>
        /// <returns>La differenza tra i due vettori.</returns>
        public static Vector3 VectorSubtraction(Vector3 vector1, Vector3 vector2)
        {
            return vector1 - vector2;
        }

        /// <summary>
        /// Applica una forza fisica a un oggetto con un rigidbody.
        /// </summary>
        /// <param name="rigidbody">Il rigidbody dell'oggetto.</param>
        /// <param name="force">La forza da applicare.</param>
        public static void ApplyForce(Rigidbody rigidbody, Vector3 force)
        {
            rigidbody.AddForce(force);
        }
        /// <summary>
        /// Calcola l'angolo in gradi tra un punto dell'oggetto e la posizione del mouse.
        /// </summary>
        /// <param name="objectPosition">La posizione dell'oggetto nel mondo.</param>
        /// <returns>L'angolo in gradi tra il punto dell'oggetto e la posizione del mouse.</returns>
        public static Quaternion GetQuaternionToObjectMousePosition(Vector3 objectPosition)
        {
            // Vector3 mousePosition = Input.mousePosition;
            // mousePosition.z = Camera.main.transform.position.z - objectPosition.z;
            // Vector3 targetPosition = Camera.main.ScreenToWorldPoint(mousePosition);

            // Vector3 direction = targetPosition - objectPosition;
            // float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            // return angle;
            // Get the mouse position in screen coordinates
            Vector3 mousePosition = Input.mousePosition;

            // Convert the screen coordinates to world coordinates
            Vector3 worldMousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

            // Calculate the direction from the object to the mouse position
            Vector3 direction = worldMousePosition - objectPosition;

            // Calculate the angle in degrees
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            return Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }
}
namespace GBGUnity.Utility
{
    /// <summary>
    /// Classe di utilità per il movimento di un personaggio non giocante (NPC) verso un obiettivo desiderato.
    /// </summary>
    public class AIManager
    {
        /// <summary>
        /// Costruttore della classe AIManager che configura un personaggio non giocante (NPC) per il movimento verso un obiettivo desiderato.
        /// </summary>
        /// <param name="NewTarget">Il Transform dell'obiettivo verso cui il personaggio si muoverà.</param>
        /// <param name="AINPC">Il GameObject del personaggio non giocante (NPC) da controllare.</param>
        /// <param name="newMovementSpeed">La velocità di movimento del personaggio non giocante (NPC) (opzionale, default: 5f).</param>
        public AIManager(Transform NewTarget, GameObject AINPC, float newMovementSpeed = 5f)
        {
            AINPC.AddComponent<AIController>();
            AIController controllerAI = AINPC.GetComponent<AIController>();
            controllerAI.target = NewTarget;
            controllerAI.movementSpeed = newMovementSpeed;
        }

        /// <summary>
        /// Classe che gestisce il movimento del personaggio non giocante (NPC).
        /// </summary>
        public class AIController : MonoBehaviour
        {
            /// <summary>
            /// L'obiettivo verso cui il personaggio non giocante (NPC) si muoverà.
            /// </summary>
            public Transform target;

            /// <summary>
            /// La velocità di movimento del personaggio non giocante (NPC).
            /// </summary>
            public float movementSpeed = 5f;

            private void Update()
            {
                if (target != null)
                {
                    // Calcola la direzione verso il target
                    Vector3 direction = target.position - transform.position;
                    direction.y = 0f; // Assicurati che il personaggio resti sul piano orizzontale

                    // Sposta il personaggio verso il target
                    transform.Translate(direction.normalized * movementSpeed * Time.deltaTime);

                    // Ruota il personaggio per guardare verso il target
                    transform.rotation = Quaternion.LookRotation(direction);
                }
            }
        }
    }
}
namespace GBGUnity.Utility
{
    /// <summary>
    /// Classe di utilità per la gestione degli effetti di particelle nel gioco.
    /// </summary>
    public static class ParticleManager
    {
        /// <summary>
        /// Riproduce un effetto di particelle in una posizione specifica.
        /// </summary>
        /// <param name="particlePrefab">Il prefab dell'effetto di particelle da riprodurre.</param>
        /// <param name="position">La posizione in cui riprodurre l'effetto di particelle.</param>
        public static void PlayParticleEffect(GameObject particlePrefab, Vector3 position)
        {
            GameObject particleEffect = GameObject.Instantiate(particlePrefab, position, Quaternion.identity);
            ParticleSystem particleSystem = particleEffect.GetComponent<ParticleSystem>();
            if (particleSystem != null)
            {
                particleSystem.Play();
            }
            else
            {
                Debug.LogWarning("The particle prefab does not have a ParticleSystem component.");
            }
        }

        /// <summary>
        /// Ferma tutti gli effetti di particelle presenti nel gioco.
        /// </summary>
        public static void StopAllParticleEffects()
        {
            ParticleSystem[] particleSystems = GameObject.FindObjectsOfType<ParticleSystem>();
            foreach (ParticleSystem particleSystem in particleSystems)
            {
                particleSystem.Stop();
            }
        }

        /// <summary>
        /// Mette in pausa tutti gli effetti di particelle presenti nel gioco.
        /// </summary>
        public static void PauseAllParticleEffects()
        {
            ParticleSystem[] particleSystems = GameObject.FindObjectsOfType<ParticleSystem>();
            foreach (ParticleSystem particleSystem in particleSystems)
            {
                particleSystem.Pause();
            }
        }

        /// <summary>
        /// Riprende tutti gli effetti di particelle in pausa nel gioco.
        /// </summary>
        public static void ResumeAllParticleEffects()
        {
            ParticleSystem[] particleSystems = GameObject.FindObjectsOfType<ParticleSystem>();
            foreach (ParticleSystem particleSystem in particleSystems)
            {
                particleSystem.Play();
            }
        }
    }
}
namespace GBGUnity.Utility
{
    /// <summary>
    /// Classe per la gestione delle azioni temporizzate.
    /// </summary>
    public static class ActionTime
    {
        /// <summary>
        /// Esegue un'azione con un ritardo di tempo specificato.
        /// </summary>
        /// <param name="actionMakerController">Il controller dell'oggetto che esegue l'azione ritardata.</param>
        /// <param name="action">L'azione da eseguire.</param>
        /// <param name="timer">Il ritardo in secondi prima di eseguire l'azione.</param>
        public static void ActionDelayed(GameObject actionMakerController, Action action, float timer)
        {
            if (!actionMakerController.GetComponent<AD>())
            {
                actionMakerController.AddComponent<AD>();
            }
            AD actionDelayed = actionMakerController.GetComponent<AD>();
            actionDelayed.SetAD(action, timer);
        }

        /// <summary>
        /// Esegue un'azione un certo numero di volte e chiama il callback quando tutte le iterazioni sono completate.
        /// </summary>
        /// <param name="action">L'azione da eseguire.</param>
        /// <param name="times">Il numero di volte in cui eseguire l'azione.</param>
        /// <param name="callback">Il callback da invocare quando tutte le iterazioni sono completate.</param>
        public static void Execute(Action action, int times, Action callback = null)
        {
            Action wrappedAction = null;
            wrappedAction = () =>
            {
                action?.Invoke();
                times--;
                if (times > 0)
                {
                    Execute(wrappedAction, times, callback);
                }
                else
                {
                    callback?.Invoke();
                }
            };

            wrappedAction.Invoke();
        }

        private class AD : MonoBehaviour
        {
            private float delay;
            private Action delayedAction;

            public void SetAD(Action action, float timer)
            {
                delay = timer;
                delayedAction = action;
            }

            private void Awake()
            {
                StartCoroutine(DelayedActionCoroutine());
            }

            private IEnumerator DelayedActionCoroutine()
            {
                yield return new WaitForSeconds(delay);
                delayedAction?.Invoke();
            }
        }
    }
}
namespace GBGUnity.Utility
{
    /// <summary>
    /// Fornisce metodi per il movimento orizzontale e verticale.
    /// </summary>
    public static class MovementObject
    {
        /// <summary>
        /// Sposta l'oggetto in orizzontale in base all'input.
        /// </summary>
        /// <param name="speed">La velocità a cui l'oggetto deve muoversi.</param>
        /// <returns>Il valore dello spostamento orizzontale.</returns>
        public static float MoveHorizontal(float speed)
        {
            return Input.GetAxis("Horizontal") * speed;
        }
        
        /// <summary>
        /// Sposta l'oggetto in verticale in base all'input.
        /// </summary>
        /// <param name="speed">La velocità a cui l'oggetto deve muoversi.</param>
        /// <returns>Il valore dello spostamento verticale.</returns>
        public static float MoveVertical(float speed)
        {
            return Input.GetAxis("Vertical") * speed;
        }
        
        /// <summary>
        /// Sposta l'oggetto in una prospettiva top-down utilizzando un Rigidbody2D.
        /// </summary>
        /// <param name="rigid">Il componente Rigidbody2D dell'oggetto da muovere.</param>
        /// <param name="speed">La velocità a cui l'oggetto deve muoversi.</param>
        public static void MoveTopDown(Rigidbody2D rigid, float speed)
        {
            Vector2 movement = new Vector2(Input.GetAxis("Horizontal") * speed, Input.GetAxis("Vertical") * speed);
            rigid.velocity = movement;
        }

        public static void MoveSide(Rigidbody2D rigid, float speed)
        {
            Vector2 movement = new Vector2(Input.GetAxis("Horizontal") * speed, 0);
            rigid.velocity = movement;
        }

        public static void Jump(Rigidbody2D rigid, float impulse){
            rigid.AddForce(new Vector2(0, 1) * impulse, ForceMode2D.Impulse);
        }

    }
}
namespace GBGUnity.ScriptableObjects

{
    [CreateAssetMenu(menuName = "GBG_ScriptableObject/Ability", fileName ="Ability", order = 1)]
    public class Ability : ScriptableObject
    {
        public string AbilityName;
        public string AbilityDescription;
        
        public enum AbilityUtility
        {
            HEALING,
            DAMAGING,
            MOVEMENT
        }
        public float movementSpeed;
        public float damageDealing;
        public float HPHealing;
        public AbilityUtility abilityScope;
        
        public float cooldownAbility;

    }

    #if UNITY_EDITOR
    [CustomEditor(typeof(Ability))]
    public class AbilityEditor : Editor
    {
        private SerializedProperty abilityNameProperty;
        private SerializedProperty AbilityDescriptionProperty;
        private SerializedProperty abilityScopeProperty;
        private SerializedProperty movementSpeedProperty;
        private SerializedProperty dmgProperty;
        private SerializedProperty hpProperty;
        private SerializedProperty cooldownProperty;

        private void OnEnable()
        {
            abilityNameProperty = serializedObject.FindProperty("AbilityName");
            AbilityDescriptionProperty = serializedObject.FindProperty("AbilityDescription");
            abilityScopeProperty = serializedObject.FindProperty("abilityScope");
            movementSpeedProperty = serializedObject.FindProperty("movementSpeed");
            dmgProperty = serializedObject.FindProperty("damageDealing");
            hpProperty = serializedObject.FindProperty("HPHealing");
            cooldownProperty = serializedObject.FindProperty("cooldownAbility");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            Ability.AbilityUtility selectedAbilityScope = (Ability.AbilityUtility)abilityScopeProperty.enumValueIndex;

            EditorGUILayout.PropertyField(abilityNameProperty);
            EditorGUILayout.PropertyField(AbilityDescriptionProperty);
            EditorGUILayout.PropertyField(abilityScopeProperty);

            switch (selectedAbilityScope)
            {
                case Ability.AbilityUtility.MOVEMENT:
                    EditorGUILayout.PropertyField(movementSpeedProperty);
                    break;
                case Ability.AbilityUtility.DAMAGING:
                    EditorGUILayout.PropertyField(dmgProperty);
                    break;
                case Ability.AbilityUtility.HEALING:
                    EditorGUILayout.PropertyField(hpProperty);
                    break;
                default:
                    break;
            }
            EditorGUILayout.PropertyField(cooldownProperty);
            serializedObject.ApplyModifiedProperties();
        }
    }
    #endif

    [CreateAssetMenu(menuName = "GBG_ScriptableObject/Stats", fileName ="Stats", order = 1)]
    public class Stats : ScriptableObject
    {
        
    }

    
}
namespace GBGUnity.Dialogs
{
    /// <summary>
    /// Rappresenta un oggetto di dialogo che contiene informazioni sulla persona che parla e il testo da dire.
    /// </summary>
    public class DialogObject
    {
        /// <summary>
        /// La persona che parla nel dialogo.
        /// </summary>
        public string personWhoSpeak;

        /// <summary>
        /// Il testo da dire nel dialogo.
        /// </summary>
        public string textToSay;
    }

    /// <summary>
    /// Rappresenta un array di dialoghi che contiene tutte le parti del dialogo.
    /// </summary>
    public class DialogArray
    {
        /// <summary>
        /// L'array di DialogArray che contiene tutte le parti del dialogo.
        /// </summary>
        public DialogArray[] everyPartDialog;
    }

    /// <summary>
    /// Classe per la gestione dei dialoghi.
    /// </summary>
    public class DialogManager : MonoBehaviour
    {
        /// <summary>
        /// Carica i dati dei dialoghi da un file JSON.
        /// </summary>
        /// <param name="jsonFileName">Il nome del file JSON da caricare.</param>
        /// <returns>L'oggetto DialogArray contenente i dati dei dialoghi, o null in caso di errore.</returns>
        public DialogArray LoadDialogData(string jsonFileName)
        {
            string jsonFilePath = Path.Combine(Application.streamingAssetsPath, jsonFileName);

            if (!File.Exists(jsonFilePath))
            {
                Debug.LogError("Il file JSON specificato non esiste: " + jsonFilePath);
                return null;
            }

            string jsonContent = File.ReadAllText(jsonFilePath);

            try
            {
                DialogArray dialogArray = JsonUtility.FromJson<DialogArray>(jsonContent);
                return dialogArray;
            }
            catch (System.Exception e)
            {
                Debug.LogError("Errore durante la deserializzazione del file JSON: " + e.Message);
                return null;
            }
        }
    }
}
namespace GBGUnity.CraftingUtility
{
    /// <summary>
    /// Rappresenta un oggetto che può essere utilizzato nelle ricette di crafting.
    /// </summary>
    public class Item
    {
        /// <summary>
        /// Il nome dell'oggetto.
        /// </summary>
        public string itemName;
    }

    /// <summary>
    /// Rappresenta una ricetta di crafting che specifica gli oggetti richiesti e l'oggetto creato come risultato.
    /// </summary>
    public class Recipe
    {
        /// <summary>
        /// L'array di oggetti necessari per creare l'oggetto risultante.
        /// </summary>
        public Item[] itemsNeeded;

        /// <summary>
        /// Il nome dell'oggetto che sarà creato utilizzando gli ingredienti forniti.
        /// </summary>
        public string itemResultCrafting;
    }
}

