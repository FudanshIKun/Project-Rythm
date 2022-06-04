using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StageManager : MonoBehaviour
{
    //Game Component
    [SerializeField] AudioSource musicPlayer;
    [SerializeField] Note note;
    [SerializeField] GameObject noteRail;
    //Text
    [SerializeField] TextMeshProUGUI comboText;
    [SerializeField] TextMeshProUGUI scoringText;
    [SerializeField] TextMeshProUGUI totalScoreText;
    [SerializeField] TextMeshProUGUI totalScoreResults;
    [SerializeField] TextMeshProUGUI maxcomboResults;
    [SerializeField] CanvasGroup gameplayGroup;
    [SerializeField] CanvasGroup endgameGroup;
    public Animator scoringAnimator;
    public Animator chaAnimator;
    public Animator effectanimator;
    public Transform hitPoint;
    public Transform spawnPointBlue;
    public Transform spawnPointRed;
    [Header("Song Settings")]
    public float showTime = 2.5f;
    public float songBPM = 146;
    //Song length 420
    public float SongLength = 8; 
    public float timePerBeat;
    [Header("Game Settings")]
    public float perfectInterval = 0.075f;
    public float goodInterval = 0.2f;
    public float missTime = 0.1f;
    //Game System
    public float gameElasTime = 0;
    public int curentNoteNumber = 0;
    //Input System
    public int curInput = 0;
    public bool haveInput = false;
    //Scoring System
    public float score = 0;
    public int combo = 0;
    int maxcombo = 0;
    //Animation System
    [SerializeField] float hitPointMaxScale = 1.2f;
    [SerializeField] float hitPointGrowSpeed = 5f;
    public float noteFadeinTime = 0.75f;
    public float noteScaleTime = 0.5f;
    float curhitPointScale = 1f;
    //Audio
    [SerializeField] AudioClip[] soundEffect = new AudioClip[3];
    [SerializeField] AudioSource audioPlayer;
    void Start()
    {
        timePerBeat = 60f / songBPM;
        StartCoroutine(gameEvent());
    }
    void Update()
    {
        inputUpdate();
        //Animation
        hitPointgrow();
    }
    IEnumerator gameEvent()
    {
        yield return new WaitForSeconds(1f);

        gameElasTime = -showTime;
        while (gameElasTime < 0)
        {
            gameElasTime += Time.deltaTime;
            if (gameElasTime > (curentNoteNumber * timePerBeat) - showTime)
            {
                createNotes();
            }
            yield return null;
        }
        //Start Song
        musicPlayer.Play();
        gameElasTime = musicPlayer.time;
        float songTime = musicPlayer.clip.length;
        while (true)
        {
            gameElasTime += Time.deltaTime;
            if (gameElasTime > (curentNoteNumber * timePerBeat) - showTime)
            {
                if (curentNoteNumber < SongLength)
                {
                    createNotes(); 
                }
                else
                {
                    Debug.Log("EndGame");
                }         
            }
            if (gameElasTime > songTime + 1f)
            {
                break;
            }
            yield return null;
        }
        Debug.Log("EndSong");
        gameplayGroup.alpha = 0;
        yield return new WaitForSeconds(1f);
        endgameGroup.alpha = 1;
        totalScoreResults.text = score.ToString();
        maxcomboResults.text = maxcombo.ToString();
    }
    void createNotes()
    {
        Note instNote = Instantiate(note, noteRail.transform);
        instNote.stageM = this;
        instNote.colorCode = 0;
        instNote.noteNumber = curentNoteNumber;
        instNote.hitTimeStamp = timePerBeat * curentNoteNumber;
        curentNoteNumber += 1;
    }
    void inputUpdate()
    {
        //Check animationInput
        haveInput = false;
        if (Input.GetMouseButton(0) || Input.GetMouseButton(1) || Input.GetKey("x") || Input.GetKey("c"))
        {
            haveInput = true;
        }
        //Check gameInput
        curInput = 0;
        if ((Input.GetMouseButton(0) || Input.GetKey("x")) & (Input.GetMouseButton(1)|| Input.GetKey("c")))
        {
            curInput = 3;
            return;
        }
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown("x"))
        {
            curInput = 1;
        }
        if (Input.GetMouseButtonDown(1) || Input.GetKeyDown("c"))
        {
            curInput = 2;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
            Debug.Log("Exit Game");
        }
    }
    #region scoring System
    public void note_hit(int hitID, float hitTime)
    {
        combo += 1;
        if (combo > maxcombo)
        {
            maxcombo = combo;
        }
        comboText.text = "COMBO " + combo.ToString();
        if(hitID == 0)
        {
            chaAnimator.Play("Attacked");
            effectanimator.Play("Slash");
            audioPlayer.clip = soundEffect[0];

        }
        if (hitID == 1)
        {
            chaAnimator.Play("Attack1");
            effectanimator.Play("Shield");
            audioPlayer.clip = soundEffect[1];
        }
        if (hitID == 2)
        {
            chaAnimator.Play("Attack1");
            effectanimator.Play("Slash");
            audioPlayer.clip = soundEffect[2];
        }
        if (hitTime <= perfectInterval)
        {
            scoringAnimator.Play("Perfect");
            scoringText.text = "Perfect";
            score += 15 + combo;
        }
        else if (hitTime <= goodInterval)
        {
            scoringAnimator.Play("Good");
            scoringText.text = "Good";
            score += 10 + combo;
        }
        totalScoreText.text = score.ToString();
        audioPlayer.Play();
    }
    public void note_miss()
    {
        combo = 0;
        comboText.text = "";
        scoringAnimator.Play("Miss");
        scoringText.text = "Miss";
    }
    #endregion 
    #region Animaiton
    void hitPointgrow()
    {
        float dir = 1;
        if (!haveInput)
        {
            dir = -1;
        }
        curhitPointScale = Mathf.Clamp(curhitPointScale + hitPointGrowSpeed * Time.deltaTime * dir, 1, hitPointMaxScale);
        hitPoint.transform.localScale = new Vector2(curhitPointScale, curhitPointScale);
    }
    #endregion
}
