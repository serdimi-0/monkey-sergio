using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverScript : MonoBehaviour
{
    public CanvasGroup gameOverCanvasGroup;
    public float fadeDuration = 1f;

    // Start is called before the first frame update
    void Start()
    {
        gameOverCanvasGroup = gameObject.GetComponent<CanvasGroup>();
        gameOverCanvasGroup.alpha = 0;
        gameOverCanvasGroup.interactable = false;
        gameOverCanvasGroup.blocksRaycasts = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowGameOver()
    {
        StartCoroutine(FadeCanvasGroup(gameOverCanvasGroup, gameOverCanvasGroup.alpha, 1));
    }

    public void HideGameOver()
    {
        StartCoroutine(FadeCanvasGroup(gameOverCanvasGroup, gameOverCanvasGroup.alpha, 0));
    }

    private IEnumerator FadeCanvasGroup(CanvasGroup canvasGroup, float start, float end)
    {
        float counter = 0f;

        while (counter < fadeDuration)
        {
            counter += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(start, end, counter / fadeDuration);
            yield return null;
        }
    }
}
