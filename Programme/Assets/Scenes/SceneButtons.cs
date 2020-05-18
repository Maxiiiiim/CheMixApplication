using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneButtons : MonoBehaviour
{
    public void SceneMenu()
    {
        SceneManager.LoadScene(1);
    }

    public void SceneMetalAndNonmetal()
    {
        SceneManager.LoadScene(2);
    }

    public void SceneFlameColor()
    {
        SceneManager.LoadScene(3);
    }

    public void SceneAcidAndHydroxide()
    {
        SceneManager.LoadScene(4);
    }

    public void SceneAcidAndOxide()
    {
        SceneManager.LoadScene(5);
    }

    public void SceneHydroxodeAndOxide()
    {
        SceneManager.LoadScene(6);
    }

    public void SceneAcidAndMetal()
    {
        SceneManager.LoadScene(7);
    }

    public void SceneIndecatorColor()
    {
        SceneManager.LoadScene(8);
    }

    public void ExitButton()
    {
        SceneManager.LoadScene(1);
    }
}
