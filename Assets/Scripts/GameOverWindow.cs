/* 
    ------------------- Code Monkey -------------------

    Thank you for downloading this package
    I hope you find it useful in your projects
    If you have any questions let me know
    Cheers!

               unitycodemonkey.com
    --------------------------------------------------
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CodeMonkey.Utils;
using TMPro;

public class GameOverWindow : MonoBehaviour {

    private Text scoreText;
    private Text highscoreText;
    public TMP_InputField PlayerName;

    private bool waitforName = false;

    private void Awake() {
        scoreText = transform.Find("scoreText").GetComponent<Text>();
        highscoreText = transform.Find("highscoreText").GetComponent<Text>();
        
        transform.Find("retryBtn").GetComponent<Button_UI>().ClickFunc = () => { Loader.Load(Loader.Scene.GameScene); };
        transform.Find("retryBtn").GetComponent<Button_UI>().AddButtonSounds();
        
        transform.Find("mainMenuBtn").GetComponent<Button_UI>().ClickFunc = () => { if (!waitforName || PlayerName.text != "") { Loader.Load(Loader.Scene.MainMenu); } };
        transform.Find("mainMenuBtn").GetComponent<Button_UI>().AddButtonSounds();
        PlayerName.onEndEdit.AddListener((string s) => { Score.TrySetNewName(s); if (s == "resetcheat") { Score.ResetHighscore(); } });
        transform.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
    }

    private void Start() {
        Bird.GetInstance().OnDied += Bird_OnDied;
        Hide();
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Home) && Input.GetKeyDown(KeyCode.Z)) {
            // Retry
            Loader.Load(Loader.Scene.GameScene);
            Score.ResetHighscore();
        }
    }

    private void Bird_OnDied(object sender, System.EventArgs e) {
        scoreText.text = Level.GetInstance().GetPipesPassedCount().ToString();

        if (Level.GetInstance().GetPipesPassedCount() >= Score.GetHighscore()) {
            // New Highscore!
            highscoreText.text = "NEW HIGHSCORE";
            transform.Find("retryBtn").gameObject.SetActive(false);
            PlayerName.gameObject.SetActive(true);
            waitforName = true;
        } else {
            waitforName = false;
            highscoreText.text = "HIGHSCORE: " + Score.GetHighscore() + " By: " + Score.GetName();
            transform.Find("retryBtn").gameObject.SetActive(true);
            PlayerName.gameObject.SetActive(false);
        }

        Show();
    }

    private void Hide() {
        gameObject.SetActive(false);
    }

    private void Show() {
        gameObject.SetActive(true);
    }

}
