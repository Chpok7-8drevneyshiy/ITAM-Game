using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class Ends : MonoBehaviour
{
    public GameObject panelEnd;
    public GameObject panelEnd2;
    private void Start()
    {
        panelEnd.SetActive(false);
        panelEnd2.SetActive(false);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Time.timeScale = 0;
        panelEnd.SetActive(true);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Portal")
        {
            panelEnd2.SetActive(true);
        }
    }

    public void Restart()
    {
        panelEnd.SetActive(false);
        panelEnd2.SetActive(false);

        Time.timeScale = 1f;
        SceneManager.LoadScene("Game");
    }

}
