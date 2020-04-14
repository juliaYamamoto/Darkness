using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public enum GameState
{
    Intro,
    Keys,
    Game,
    BadEnd,
    GoodEnd
}

public class GameController : MonoBehaviour
{
    public GameState currentGameState = GameState.Intro;

    public Text intro001;
    public Text intro002;
    public Text intro003;

    public Image[] keys;

    public GameObject firstLightball;

    public Text tutorial001;
    public Text tutorial002;
    public Text tutorial003;

    public Text endBad001;
    public Text endBad002;

    public Text endGood001;
    public Text endGood002;
    public Text endGood003;

    public Text endFinal;


    private void Start()
    {
        currentGameState = GameState.Intro;

        StartIntro();
    }

    private void StartIntro()
    {

    }
}