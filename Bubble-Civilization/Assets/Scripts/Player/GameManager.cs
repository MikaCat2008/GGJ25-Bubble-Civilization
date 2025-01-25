using UnityEngine;
using UnityEngine.SceneManagement;




public class  GameManager : MonoBehaviour
{
    [SerializeField] BubbleManager bubbleManager;
    [SerializeField] GameplayTimeManager GameplayTimeManager;

    void Start()
    {
        bubbleManager.OnPopulationDied += HandleGameEnd;
    }

    void HandleGameEnd()
    {
        SceneManager.LoadScene("EndGameScene");

    }
}
