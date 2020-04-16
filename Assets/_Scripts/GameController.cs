using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

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

    public int currentCorals = 0;
    public int totalCorals = 10;

    public Text intro001;
    public Text intro002;
    public Text intro003;

    public Image[] keys;

    public GameObject firstLightball;
    public GameObject firstCoral;

    public Button playAgainButton;

    public SpriteRenderer characterSpriteRenderer;
    public Animator characterAnimator;

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
            .Append(tutorial001.DOFade(0, fadeSpeed)).OnComplete(ShowFirstCoral);
    }

    private void ShowFirstCoral()
    {
        //Show the first Coral
        firstCoral.gameObject.SetActive(true);
    }

    public void CollectFirstCoral()
    {
        //Show text
        Sequence lightballSequence = DOTween.Sequence();
        lightballSequence.Append(tutorial002.DOFade(semiFadeValue, fadeSpeed))
            .Append(tutorial003.DOFade(semiFadeValue, fadeSpeed))
            .Append(tutorial002.DOFade(0, fadeSpeed))
            .Append(tutorial003.DOFade(0, fadeSpeed));
    }

    public void AddToTotalCorals()
    {
        currentCorals++;
        print("CORAL! " + currentCorals + "/ " + totalCorals);
        if (currentCorals >= totalCorals)
        {
            GoodEnd();
        }
    }

    private void GoodEnd()
    {
        currentGameState = GameState.GoodEnd;

        //Show text
        Sequence lightballSequence = DOTween.Sequence();
        lightballSequence.Append(endGood001.DOFade(semiFadeValue, fadeSpeed))
            .Append(endGood002.DOFade(semiFadeValue, fadeSpeed))
            .Append(endGood001.DOFade(0, fadeSpeed))
            .Append(endGood002.DOFade(0, fadeSpeed))
            .OnComplete(HideCharacter);

    }

    public void BadEnd()
    {
        currentGameState = GameState.BadEnd;

        //Show text
        Sequence lightballSequence = DOTween.Sequence();
        lightballSequence.Append(endBad001.DOFade(semiFadeValue, fadeSpeed))
            .Append(endBad002.DOFade(semiFadeValue, fadeSpeed))
            .Append(endBad001.DOFade(0, fadeSpeed))
            .Append(endBad002.DOFade(0, fadeSpeed))
            .OnComplete(HideCharacter);
    }

    private void HideCharacter()
    {
        characterAnimator.enabled = false;
        characterSpriteRenderer.enabled = false;
        
        //Show text
        Sequence lightballSequence = DOTween.Sequence();
        lightballSequence.Append(endFinal.DOFade(semiFadeValue, fadeSpeed))
            .OnComplete(EnablePlayAgain);
    }

    private void EnablePlayAgain()
    {
        //Make it possible to play again
        playAgainButton.gameObject.SetActive(true);
        playAgainButton.interactable = true;
    }


    public void PlayAgain()
    {
        SceneManager.LoadScene(0);
    }

}