using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class LifeUIManager : MonoBehaviour
{
    public static LifeUIManager instance;

    [SerializeField]
    private TextMeshProUGUI gameOverText;

    [SerializeField]
    private int currentLife = 4;
 

    [SerializeField]
    private Sprite[] _livesSprites;
    [SerializeField]
    private Image _livesImage;
    [SerializeField] private CanvasGroup blackScreenCanvasGroup;
     [SerializeField]
 



    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Make this script persistent across scenes for reload
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }
  
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Find the _livesImage by its tag and reassign the reference
        GameObject livesImageObject = GameObject.FindGameObjectWithTag("LivesImage");
        if (livesImageObject)
        {
            _livesImage = livesImageObject.GetComponent<Image>();
            UpdateLifeUI();  // Update the UI once you reassign the reference
        }
        // Attempt to find the gameOverText object
        GameObject gameOverObject = GameObject.Find("GameOver");
        if (gameOverObject != null)
        {
            gameOverText = gameOverObject.GetComponent<TextMeshProUGUI>();
            gameOverText.gameObject.SetActive(false);
        }
        if (blackScreenCanvasGroup == null) 
        {
        blackScreenCanvasGroup = GameObject.FindGameObjectWithTag("BlackScreen").GetComponent<CanvasGroup>();
        blackScreenCanvasGroup.alpha = 0; // Set the initial alpha to 0 (completely transparent)
        }
    }

    public void HandleEnemyCollision()
    {
        AudioSource playerAudioSource = GetComponent<AudioSource>();
        if (playerAudioSource != null)
        {
            Debug.Log("Playing audio...");
            // Play the audio clip
            playerAudioSource.Play();
        }
        ReduceLife();
        // Check if game over
        if (currentLife <= 1)
        {
            gameOverText.gameObject.SetActive(true);

            GameOver();
        }
        else
        {
            // Else, Reset the level
            StartCoroutine(ReloadSceneAfterDelay(0.74f));

        }

    }
    IEnumerator ReloadSceneAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public int ReduceLife()
    {
        currentLife--;
        UpdateLifeUI();
       

        return currentLife;
    }


    private void UpdateLifeUI()
    {
        if (currentLife > 0 && currentLife <= _livesSprites.Length)
        {
            _livesImage.sprite = _livesSprites[currentLife - 1];
        }
    }
    private void GameOver() {

        blackScreenCanvasGroup.alpha = 1f;
        Invoke("QuitGame", 3f);
    }
    void QuitGame()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }


}
