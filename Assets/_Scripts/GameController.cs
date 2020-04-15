using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

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

    public float semiFadeValue = 0.6f;
    public float totalFadeValue = 1f;
    public float fadeSpeed = 1f;


    private void Start()
    {
        StartIntro();
    }


    private void StartIntro()
    {
        Sequence startSequence = DOTween.Sequence();
        startSequence.PrependInterval(1)
            .Append(intro001.DOFade(semiFadeValue, fadeSpeed))
            .Append(intro002.DOFade(semiFadeValue, fadeSpeed))
            .Join(intro003.DOFade(totalFadeValue, fadeSpeed))
            .Append(intro001.DOFade(0, fadeSpeed))
            .Append(intro002.DOFade(0, fadeSpeed))
            .Join(intro003.DOFade(0, fadeSpeed))
            .OnComplete(StartKeys);
    }

    private void StartKeys()
    {
        currentGameState = GameState.Keys;

        Sequence keysSequence = DOTween.Sequence();
        keysSequence.PrependInterval(1)
            .Append(keys[0].DOFade(semiFadeValue, fadeSpeed))
            .Join(keys[1].DOFade(semiFadeValue, fadeSpeed))
            .Join(keys[2].DOFade(semiFadeValue, fadeSpeed))
            .Join(keys[3].DOFade(semiFadeValue, fadeSpeed))
            .OnComplete(StartGame);
    }

    private void StartGame()
    {
        Sequence keysSequence = DOTween.Sequence();
        keysSequence.PrependInterval(2)
            .Append(keys[0].DOFade(0, fadeSpeed))
            .Join(keys[1].DOFade(0, fadeSpeed))
            .Join(keys[2].DOFade(0, fadeSpeed))
            .Join(keys[3].DOFade(0, fadeSpeed));

        currentGameState = GameState.Game;
        ShowFirstLightball();
    }

    private void ShowFirstLightball()
    {
        //Show the first lightball
        firstLightball.gameObject.SetActive(true);
    }

    public void CollectFirstLightball()
    {
        firstLightball.gameObject.SetActive(false);

        //Show text
        Sequence lightballSequence = DOTween.Sequence();
        lightballSequence.Append(tutorial001.DOFade(semiFadeValue, fadeSpeed))
            .SetDelay(1.0f)
            .Append(tutorial001.DOFade(0, fadeSpeed));
    }

    private void ShowFirstCoral()
    {
        //Show the first Coral
        //firstLightball.gameObject.SetActive(true);
    }

    private void CollectFirstCoral()
    {
    }
}