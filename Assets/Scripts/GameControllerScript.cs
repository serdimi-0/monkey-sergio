using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameControllerScript : MonoBehaviour
{
    public GameObject characterPrefab;
    public GameObject transformationPrefab;
    public GameObject cameraObject;
    public GameOverScript gameOverScript;
    public Slider healthBar;
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI keyNumberText;
    public TextMeshProUGUI hintText;
    public int time = 300;
    public List<GameObject> keys;
    public Animator playerAnimator;
    public AudioClip keyAudio;
    public AudioClip finalAudio;
    public AudioSource musicSource;
    public Image blackImage;

    private int health = 100;
    public int keyNumber = 0;

    private GameObject player;
    private int timeLeft;
    private AudioSource audioSource;
    private bool isFinal = false;


    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        player = GameObject.Find("Character");
        healthBar.maxValue = health;
        healthBar.value = health;

        timeLeft = time;
        // start timer
        StartCoroutine(ChangeTimer(time));
    }

    // Update is called once per frame
    void Update()
    {
        //e key pressed
        if (Input.GetKeyDown(KeyCode.E))
        {
            // change camera target
            GameObject transformation = Instantiate(transformationPrefab, player.transform.position, player.transform.rotation);
            cameraObject.GetComponent<CameraScript>().target = transformation.transform;

            Destroy(player);


            // wait for 5 seconds and destroy transformation, instantiate character
            StartCoroutine(RevertTransformation(5, transformation));
        }
    }

    IEnumerator RevertTransformation(int v, GameObject transformation)
    {
        yield return new WaitForSeconds(7f);

        Destroy(transformation);

        player = Instantiate(characterPrefab, transformation.transform.position, transformation.transform.rotation);
        player.transform.position = new Vector3(player.transform.position.x, player.transform.position.y - .5f, player.transform.position.z);

        cameraObject.GetComponent<CameraScript>().target = player.transform;
    }

    public void TakeDamage(int damage)
    {
        if (health >= 0)
        {
            health -= damage;
            healthBar.value = health;
        }
        else if (!playerAnimator.GetBool("isDead"))
            GameOver();
    }

    public void AddKey()
    {
        audioSource.PlayOneShot(keyAudio);
        keyNumber++;
        keyNumberText.text = keyNumber.ToString();
    }

    public void RemoveKey()
    {
        keyNumber--;
        keyNumberText.text = keyNumber.ToString();
    }

    public void ShowHint(string hint)
    {
        hintText.text = hint;
        StartCoroutine(ClearHint(4));
    }

    private IEnumerator ClearHint(int seconds)
    {
        yield return new WaitForSeconds(seconds);
        hintText.text = "";
    }

    private IEnumerator ChangeTimer(int seconds)
    {
        while (timeLeft > 0 && !isFinal)
        {
            timeLeft--;
            timeText.text = $"{(timeLeft / 60).ToString("D2")}:{(timeLeft % 60).ToString("D2")}";
            yield return new WaitForSeconds(1);
        }
    }

    private void GameOver()
    {
        playerAnimator.SetBool("isDead", true);
        playerAnimator.SetTrigger("Death");
        gameOverScript.ShowGameOver();
        StartCoroutine(RestartGame(3));
    }

    private IEnumerator RestartGame(int seconds)
    {
        yield return new WaitForSeconds(seconds);
        gameOverScript.HideGameOver();
        health = 100;
        healthBar.value = health;
        timeLeft = time;
        keyNumber = 0;
        keyNumberText.text = keyNumber.ToString();
        player.transform.position = new Vector3(0, 0, 0);
        playerAnimator.SetBool("isDead", false);
    }

    public void Final()
    {
        isFinal = true;
        musicSource.Stop();
        audioSource.PlayOneShot(finalAudio);
        cameraObject.GetComponent<CameraScript>().zOffset = -15;
        StartCoroutine(EndGame(5));

    }

    private IEnumerator EndGame(int seconds)
    {
        yield return new WaitForSeconds(seconds);
        StartCoroutine(FadeCanvasGroup(blackImage.GetComponent<CanvasGroup>(), 0, 1));
    }

    private IEnumerator FadeCanvasGroup(CanvasGroup canvasGroup, float start, float end)
    {
        float counter = 0f;

        while (counter < 2f)
        {
            counter += Time.deltaTime;
            musicSource.volume = Mathf.Lerp(1, 0, counter / 2f);
            canvasGroup.alpha = Mathf.Lerp(start, end, counter / 2f);
            yield return null;
        }

        StartCoroutine(GoToMenu(2));
    }

    private IEnumerator GoToMenu(int seconds)
    {
        yield return new WaitForSeconds(seconds);

        UnityEngine.SceneManagement.SceneManager.LoadScene("Menu");
    }
}
