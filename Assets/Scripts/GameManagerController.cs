using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManagerController : MonoBehaviour
{
    public ScoreUIController scoreUIController;
    public GameObject mainScene;
    public GameObject endScene;
    public GameObject startScene;

    public AudioSource gameAudio;
    public AudioSource endAudio;

    public TMP_Text EndMessage;

    public PlayerControls playerControls;

    public TMP_Text[] scoresTxt = new TMP_Text[8];

    public Toggle audioToggle;


    string gameMode = "start";


    private void OnEnable()
    {
        playerControls = new PlayerControls();
        playerControls.Enable();
        playerControls.Restart.action.performed += ctx => RestartGame();
        playerControls.Start.action.performed += ctx => StartGame();
        playerControls.Exit.action.performed += ctx => QuitGame();

    }
    private void OnDisable()
    {
        playerControls.Enable();
        playerControls.Restart.action.performed -= ctx => RestartGame();
        playerControls.Start.action.performed -= ctx => StartGame();
        playerControls.Exit.action.performed -= ctx => QuitGame();
    }
    private void QuitGame()
    {
        Debug.Log("quit on e");
        Application.Quit();
    }
    private void StartGame()
    {
        if (gameMode == "start")
        {
            if (startScene != null)
                startScene.SetActive(false);
            if (mainScene != null)
                mainScene.SetActive(true);
            gameMode = "play";
        }

    }

    void Start()
    {

        scoreUIController.OnEndGame += LoadEndGameScene;
        endScene.SetActive(false);
        mainScene.SetActive(true);
        startScene.SetActive(false);
        if (audioToggle != null)
            audioToggle.onValueChanged.AddListener(OnToggleValueChanged);

    }
    void OnToggleValueChanged(bool isOn)
    {
        if (isOn)
        {
            gameAudio.Stop();
        }
        if (!isOn)
        {
            gameAudio.Play();
        }
    }

    private void LoadEndGameScene()
    {
        Debug.Log("game over scene load");
        EndMessage.text = scoreUIController.endGameMsg;
        for (int i = 7; i >= 0; i--)
        {
            scoresTxt[i].text = scoreUIController.scoresTxt[i].text;
        }
        endScene.SetActive(true);
        mainScene.SetActive(false);
        gameAudio.Stop();
        StartCoroutine(PlayEndMusic());
        gameMode = "End";

    }
    IEnumerator PlayEndMusic()
    {

        yield return new WaitForSeconds(1.5f);
        endAudio.Play();
    }


    void RestartGame()
    {
        if (gameMode == "End")
        {
            string currentSceneName = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(currentSceneName);
            startScene.SetActive(false);
            mainScene.SetActive(true);
            endScene.SetActive(false);
            gameMode = "start";
        }
    }

}
