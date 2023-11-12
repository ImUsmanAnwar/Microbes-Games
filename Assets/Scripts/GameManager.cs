using System.Collections;
using UnityEngine;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    public Text scoreText;
    public Text damageText;
    public Image fadeImage;
    public GameObject gameOverPanel;
    public Text highScore;
    public Text countdownText;


    float currentTime;
    public float startTime;


    private Blade blade;
    private Spawner spawner;

    public int damageCounter;

    private int score;

    private void Awake()
    {
        blade = FindObjectOfType<Blade>();
        spawner = FindObjectOfType<Spawner>();
    }

    private void Start()
    {
        currentTime = startTime;
        NewGame();
    }


    private void Update()
    {
        currentTime -= 1 * Time.deltaTime;
        countdownText.text = currentTime.ToString("0");
        if (currentTime<=0)
        {
            currentTime = 0;
            blade.enabled = false;
            spawner.enabled = false;
            gameOverPanel.SetActive(true);
            highScore.text = scoreText.text;

        }
    }

    public void NewGame()
    {
        Time.timeScale = 1f;

        ClearScene();

        blade.enabled = true;
        spawner.enabled = true;

        score = 0;
        scoreText.text = score.ToString();
        gameOverPanel.SetActive(false);
    }

    private void ClearScene()
    {
        Fruit[] fruits = FindObjectsOfType<Fruit>();

        foreach (Fruit fruit in fruits) {
            Destroy(fruit.gameObject);
        }

        Bomb[] bombs = FindObjectsOfType<Bomb>();

        foreach (Bomb bomb in bombs) {
            Destroy(bomb.gameObject);
        }
    }

    public void IncreaseScore(int points)
    {
        score += points;
        scoreText.text = score.ToString();
    }

    public void Explode()
    {
        damageCounter++;
        print(damageCounter);
        damageText.text =  damageCounter.ToString();
        if (damageCounter>=3)
        {
            blade.enabled = false;
            spawner.enabled = false;
            gameOverPanel.SetActive(true);
            highScore.text=scoreText.text;
            //StartCoroutine(ExplodeSequence());
        }

    }

    private IEnumerator ExplodeSequence()
    {
        float elapsed = 0f;
        float duration = 0.5f;

        // Fade to white
        while (elapsed < duration)
        {
            float t = Mathf.Clamp01(elapsed / duration);
            fadeImage.color = Color.Lerp(Color.clear, Color.white, t);

            Time.timeScale = 1f - t;
            elapsed += Time.unscaledDeltaTime;

            yield return null;
        }

        yield return new WaitForSecondsRealtime(1f);

        NewGame();

        elapsed = 0f;

        // Fade back in
        while (elapsed < duration)
        {
            float t = Mathf.Clamp01(elapsed / duration);
            fadeImage.color = Color.Lerp(Color.white, Color.clear, t);

            elapsed += Time.unscaledDeltaTime;

            yield return null;
        }
    }

}
