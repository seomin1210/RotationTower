using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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

    private void Start()
    {
        if (Instance != null)
            Debug.LogError("GameManager is Multi Playing");
        Instance = this;
        CloseSetting();
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
    }

    public void GameStart(int select)
    {
        stage = select - 2; //1스테이지 선택 -> -1 NextStage() 시 ++stage로 리스트의 0번째(1스테이지) 실행
        start.SetActive(false);
        tower.SetActive(false);
        floor.gameObject.SetActive(true);
        isGameStart = true;
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
        Destroy(FindObjectOfType<MazeStageMove>().gameObject);
        start.SetActive(true);
        tower.SetActive(true);
    }

    public void NextStage()
    {
        Instantiate(stageList[++stage], null);
        floor.SetText((stage + 1) + "F");
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
}
