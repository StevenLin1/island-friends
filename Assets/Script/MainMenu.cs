using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject aboutPanel;  // ← 在 Inspector 里赋值

    public void OnPlayButton()
    {
        SceneManager.LoadScene("game scene");
    }

    public void OnAboutButton()
    {
        if (aboutPanel != null)
            aboutPanel.SetActive(true);   // 显示玩法说明面板
    }

    public void OnCloseAboutButton()
    {
        if (aboutPanel != null)
            aboutPanel.SetActive(false);  // 隐藏玩法说明面板
    }
}
