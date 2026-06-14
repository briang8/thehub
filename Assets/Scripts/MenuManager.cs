using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject aboutPanel;

    public void StartGame()
    {
        SceneManager.LoadScene("HubScene");
    }

    public void ShowAbout()
    {
        aboutPanel.SetActive(true);
    }

    public void CloseAbout()
    {
        aboutPanel.SetActive(false);
    }
}