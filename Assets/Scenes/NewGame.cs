using UnityEngine.SceneManagement;
using UnityEngine;

public class NewGame : MonoBehaviour
{
    public void NewGameButtom()
    {
        SceneManager.LoadScene("CutScene");
    }
}
