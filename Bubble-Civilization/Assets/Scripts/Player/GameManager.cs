using UnityEngine;




public class  GameManager : MonoBehaviour
{
    public delegate void OnGameEndDelegate();
    public static OnGameEndDelegate OnGameEnd;

    [SerializeField] GameObject MainMenuObject;
    [SerializeField] BubbleManager bubbleManager;
    [SerializeField] GameplayTimeManager GameplayTimeManager;


    private MainMenu mainMenu;

    void Start()
    {
        mainMenu = MainMenuObject.GetComponent<MainMenu>();
        bubbleManager.OnPopulationDied += HandleGameEnd;
    }

    void HandleGameEnd()
    {
        mainMenu.menuEngGame();
    }

}
