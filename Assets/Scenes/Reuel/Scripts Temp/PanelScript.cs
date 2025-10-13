using UnityEngine;
using UnityEngine.Audio;
using TMPro;
public class WinLose : MonoBehaviour
{

    //Text
    [SerializeField] private TextMeshProUGUI _stateText;
    [SerializeField] private TextMeshProUGUI _timerText;
    [SerializeField] private TextMeshProUGUI _totalTimeField;
    [SerializeField] private TextMeshProUGUI _livesLeftField;
    [SerializeField] private TextMeshProUGUI _fragmentsField;
    [SerializeField] private TextMeshProUGUI _scoreField;

    //Scoring
    [SerializeField] private int _totalTime;    
    [SerializeField] private int _livesLeftScore;
    [SerializeField] private int _fragmentsScore;
    
    //Timer
    [SerializeField] private float _timer;
    [SerializeField]private bool timerEnabled = true;

    //UI
    public GameObject Panel;
    public GameObject nextLevelButton;

    public int score;
    public int previousScoreLevel;
    public static int  nextScoreLevel;

    //PlayerScript
    [SerializeField] private PCReuel _playerScript;

    //SFX 
    [SerializeField] private AudioMixer mixer;

    void Start()
    {
       previousScoreLevel = nextScoreLevel;
       Panel.SetActive(false);
       _timer = 10;
    }

    // Update is called once per frame
    void Update()
    {
        
        
        if(timerEnabled){
            if(_timer <= 0){
                GameOver();
            }else{

                _timer -= Time.deltaTime;
                _timerText.text = $"Timer: {(int)_timer}";
            }
        }
    }
    void GameOver()
    {
        Panel.SetActive(true);

        _stateText.text = "Game Over";
        nextLevelButton.SetActive(false);

        //Accumulate the score
        _totalTime = (int)_playerScript.totalTime;
        int totalTimeScore = 120 - _totalTime / 5;
        _livesLeftScore = _playerScript.lives * 10;
        _fragmentsScore = _playerScript.fragmentsCollected * 2;
        score = totalTimeScore + _livesLeftScore + _fragmentsScore + previousScoreLevel;

        _totalTimeField.text = _totalTime.ToString();
        _livesLeftField.text = _playerScript.lives.ToString();
        _fragmentsField.text = _playerScript.fragmentsCollected.ToString();
        _scoreField.text = score.ToString();
        Time.timeScale = 0f;
        DisableSFX();
    }
    void ExitLevel()
    {
        
        //Accumulate the score
        Panel.SetActive(true);
        _totalTime = (int)_playerScript.totalTime;
        int totalTimeScore = 120 - _totalTime / 5;
        _livesLeftScore = _playerScript.lives * 10;
        _fragmentsScore = _playerScript.fragmentsCollected * 2;

        score = totalTimeScore + _livesLeftScore + _fragmentsScore + previousScoreLevel;
        nextScoreLevel = score;

        _totalTimeField.text = _totalTime.ToString();
        _livesLeftField.text = _playerScript.lives.ToString();
        _fragmentsField.text = _playerScript.fragmentsCollected.ToString();
        _scoreField.text = score.ToString();
        Time.timeScale = 0f;
        DisableSFX();
    }

    void DisableSFX(){
        mixer.SetFloat("SoundEffects", -80f);
    }
    void OnTriggerEnter2D(Collider2D other){
        
        if(other.gameObject.CompareTag("Player"))
        {
            
            ExitLevel();
        }
    }
}
