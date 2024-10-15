using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Button QuitButton;
    public Button PlayButton;
    public Material trapMat;
    public Material goalMat;
    public Toggle colorblindMode;



    void Start()
    {
        colorblindMode.isOn = !colorblindMode.isOn;
            if (QuitButton != null)
        {
            QuitButton.onClick.AddListener(OnQuitButtonPressed);
        }

        if (PlayButton != null)
        {
            PlayButton.onClick.AddListener(OnPlayButtonPressed);
        }
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("menu");
        }
       
    }

    public void PlayMaze()
    {
        SceneManager.LoadScene("maze");
        Color32 trapMaterialColor = new Color32(255, 112, 0, 255);
        
        if (colorblindMode.isOn)
        {
            trapMat.color = trapMaterialColor;
            goalMat.color = Color.blue;
        }
    }


    public void OpenOptions()
    {
        SceneManager.LoadScene("OptionsScene");
    }

    public void QuitMaze()
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }


    void OnQuitButtonPressed()
    {
        QuitMaze();
    }

    void OnPlayButtonPressed()
    {
        PlayMaze();
    }
}