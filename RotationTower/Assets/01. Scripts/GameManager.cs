using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Audio;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public int stage = 0;
    public List<GameObject> stageList = new List<GameObject>();

    public GameObject start = null;
    public GameObject tower = null;
    public TextMeshProUGUI floor;

    public bool isOpenSetting = false;
    public GameObject setting = null;
    public GameObject restartButton = null;
    public GameObject warning = null;

    public string username = "Player";
    public GameObject inputName = null;
    public GameObject ranking = null;

    private bool isGameStart = false;
    private bool isDie = false;

    private bool firstGame = true;
    public GameObject help = null;

    private bool isTimeAttack = false;
    private float time = 0f;
    public TextMeshProUGUI timeText = null;

    private bool isMuteAudio = false;
    public Image audioImage = null;
    public AudioMixer mainMixer = null;
    public Sprite[] audioImages = new Sprite[2];
    private BGMPlayer bgm;

    public GameObject clear = null;
    private void Start()
    {
        if (Instance != null)
            Debug.LogError("GameManager is Multi Playing");
        Instance = this;
        bgm = gameObject.transform.parent.GetComponentInChildren<BGMPlayer>();
        CloseSetting();
        if(firstGame == true)
        {
            firstGame = false;
            OpenHelp();
        }
        bgm.OnStartBGM();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(isOpenSetting == false && isDie == false)
            {
                OpenSetting();
            }
            else
            {
                CloseSetting();
            }
        }
        if(isTimeAttack == true)
        {
            if(time > 0f)
            {
                time -= Time.deltaTime;
                UpdateTimeUI();
            }
            else
            {
                time = 0;
                isTimeAttack = false;
                Die();
            }
        }
    }

    public void GameStart(int select)
    {
        stage = select - 2; //1스테이지 선택 -> -1 NextStage() 시 ++stage로 리스트의 0번째(1스테이지) 실행
        start.SetActive(false);
        tower.SetActive(false);
        floor.gameObject.SetActive(true);
        isGameStart = true;
        bgm.OnGameBGM();
        NextStage();
    }

    public void GameExit()
    {
        if(isGameStart == true)
        {
            Warning();
            return;
        }
        Application.Quit();
    }

    private void Warning()
    {
        warning.SetActive(true);
    }

    public void GoMenu()
    {
        isGameStart = false;
        CloseSetting();
        floor.gameObject.SetActive(false);
        ranking.SetActive(false);
        Destroy(FindObjectOfType<MazeStageMove>()?.gameObject);
        start.SetActive(true);
        tower.SetActive(true);
        bgm.OnStartBGM();
    }

    public void NextStage()
    {
        if(stage >= stageList.Count - 1)
        {
            clear.SetActive(true);
            Invoke("Die", 3f);
            return;
        }
        Instantiate(stageList[++stage], null);
        floor.SetText((stage + 1) + "F");
        if(stage < 30)
        {
            isTimeAttack = true;
            time = 150f;
            timeText.gameObject.SetActive(true);
        }
        else if(stage >= 30 && isTimeAttack == true)
        {
            isTimeAttack = false;
            timeText.gameObject.SetActive(false);
        }
    }

    private void UpdateTimeUI()
    {
        timeText.text = string.Format("{0:N1}", time);
    }

    public void RestartStage()
    {
        CloseSetting();
        Destroy(FindObjectOfType<MazeStageMove>().gameObject);
        Instantiate(stageList[stage], null);
    }

    public void OpenSetting()
    {
        isOpenSetting = true;
        Time.timeScale = 0f;
        setting.SetActive(true);
        if (isGameStart == false)
        {
            restartButton.SetActive(false);
        }
        else
        {
            restartButton.SetActive(true);
        }
    }

    public void CloseSetting()
    {
        isOpenSetting = false;
        Time.timeScale = 1f;
        setting.SetActive(false);
        warning.SetActive(false);
    }

    public void Die()
    {
        clear.SetActive(false);
        isDie = true;
        if (username == null || username == "Player")
        {
            inputName.SetActive(true);
        }
        else
        {
            AddRanking();
        }
    }

    public void AddRanking()
    {
        ranking.SetActive(true);
        CFireBase.Instance.writeNewUser(username, stage);
    }
    
    public void OpenRanking()
    {
        ranking.SetActive(true);
        CFireBase.Instance.roading.text = "Loading..";
    }

    public void OpenHelp()
    {
        help.SetActive(true);
    }

    public void CloseHelp()
    {
        help.SetActive(false);
    }

    public void AudioButton()
    {
        if(isMuteAudio == true)
        {
            OnAudio();
        }
        else
        {
            MuteAudio();
        }
    }

    private void MuteAudio()
    {
        isMuteAudio = true;
        mainMixer.SetFloat("Master", 0f);
        audioImage.sprite = audioImages[1];
    }

    private void OnAudio()
    {
        isMuteAudio = false;
        mainMixer.SetFloat("Master", 1f);
        audioImage.sprite = audioImages[0];
    }
}
