using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;


public class LifeUIManager : MonoBehaviour
{
    public static LifeUIManager instance;
 
    private TextMeshProUGUI gameOverText;
    [SerializeField]
    private int currentLife = 4;
 

    [SerializeField]
    private Sprite[] _livesSprites;
    [SerializeField]
    private Image _livesImage;


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
       
    }
    
    public void HandleEnemyCollision()
    {
        ReduceLife();

        // Check if game over
        if (GetCurrentLife() <= 1)
        {
            // Trigger a "GameOver" animation if you have one.

            // Call the GameOver method after a delay
            Invoke("GameOver", 3f);
        }
        else
        {
            // Else, Reset the level
            Reload();
        }
    }

    public void ReduceLife()
    {
        Debug.Log("ReduceLife called. Current life before reduction: " + currentLife);

        currentLife--;
        UpdateLifeUI();

        Debug.Log("Life after reduction: " + currentLife);
    }


    private void UpdateLifeUI()
    {
        if (currentLife > 0 && currentLife <= _livesSprites.Length)
        {
            _livesImage.sprite = _livesSprites[currentLife - 1];
        }
    }

    public int GetCurrentLife()
    {
        return currentLife;
    }
    public void Reload() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        
    }
    
    public void GameOver()
    {
        gameOverText.gameObject.SetActive(true);
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
