using UnityEngine;
using UnityEngine.UI;
using Unity.Cinemachine;

public class GameManager : MonoBehaviour
{
    private int currentEnergy;
    [SerializeField] private int energyThreshold = 3;
    [SerializeField] private GameObject boss;
    [SerializeField] private GameObject enemySpawner;
    private bool bossCalled = false;
    [SerializeField] private Image energyBar;
    [SerializeField] GameObject gameUi;
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject gameOverMenu;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject winMenu;
    [SerializeField] private GameObject red;
    [SerializeField] private AudioManager audioManager;
    [SerializeField] private CinemachineCamera cam;
    void Start()
    {
        currentEnergy = 0;
        UpdateEnergyBar();
        boss.SetActive(false);
        MainMenu();
        cam.Lens.OrthographicSize = 8f;
        red.SetActive(false);
    }

    public void AddEnergy()
    {
        if(bossCalled)
        {
            return;
        }
        currentEnergy += 1;
        UpdateEnergyBar();
        if(currentEnergy == energyThreshold)
        {
            CallBoss();
        }
    }
    public void CallBoss()
    {
        bossCalled = true;
        boss.SetActive(true);
        enemySpawner.SetActive(false);
        gameUi.SetActive(false);
        audioManager.PlayBossAudio();
        cam.Lens.OrthographicSize = 10f;
        red.SetActive(true);
    }
    private void UpdateEnergyBar()
    {
        if(energyBar != null)
        {
            float fillAmount=Mathf.Clamp01((float)currentEnergy/(float)energyThreshold);
            energyBar.fillAmount=fillAmount;
        }
    }
    public void MainMenu()
    {
        mainMenu.SetActive(true);
        gameOverMenu.SetActive(false);
        pauseMenu.SetActive(false);
        winMenu.SetActive(false);
        Time.timeScale = 0f;
    }
    public void GameOverMenu()
    {
        gameOverMenu.SetActive(true);
        mainMenu.SetActive(false);
        pauseMenu.SetActive(false);
        winMenu.SetActive(false);
        Time.timeScale = 0f;
    }
    public void PauseGameMenu()
    {
        pauseMenu.SetActive(true);
        mainMenu.SetActive(false);
        gameOverMenu.SetActive(false);
        winMenu.SetActive(false);
        Time.timeScale = 0f;
    }
    public void StartGame()
    {
        mainMenu.SetActive(false);
        gameOverMenu.SetActive(false);
        pauseMenu.SetActive(false);
        winMenu.SetActive(false);
        Time.timeScale = 1f;
        audioManager.PlayDefaultAudio();
    }
    public void ResumeGame()
    {
        mainMenu.SetActive(false);
        gameOverMenu.SetActive(false);
        pauseMenu.SetActive(false);
        winMenu.SetActive(false);
        Time.timeScale = 1f;
    }
    public void WinMenu()
    {
        winMenu.SetActive(true);        
        mainMenu.SetActive(false);
        gameOverMenu.SetActive(false);
        pauseMenu.SetActive(false);
        Time.timeScale = 0f;
    }
}
