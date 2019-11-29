using UnityEngine;
using UnityEngine.SceneManagement;

public class ScrChamaClip : MonoBehaviour
{
    float TempoChamaCena = 30f;
    float Redutor = 1f;

    void Update()
    {
        TempoChamaCena -= Redutor * Time.deltaTime;
        if (TempoChamaCena <= 0)
        {
            SceneManager.LoadScene("Clipe");
        }
    }
}
