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

    private bool isGameStart = false;
    private List<bool> clearStage = new List<bool>();

    private void Start()
    {
        if (Instance != null)
            Debug.LogError("GameManager is Multi Playing");
        Instance = this;
        if(clearStage.Count != stageList.Count)
        {
            for(int i = clearStage.Count; i < stageList.Count; i++)
            {
                clearStage.Add(false);
            }
        }
        if(clearStage[0] == false) clearStage[0] = true;
        CloseSetting();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(isOpenSetting == false)
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
        if(select != 1)
        {
            if (clearStage[select - 2] == false) return;
        }
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
            GoMenu();
            return;
        }
        Application.Quit();
    }

    private void GoMenu()
    {
        isGameStart = false;
        CloseSetting();
        floor.gameObject.SetActive(false);
        Destroy(FindObjectOfType<MazeStageMove>().gameObject);
        start.SetActive(true);
        tower.SetActive(true);
    }

    public void ClearStage()
    {
        clearStage[stage] = true;
        NextStage();
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
    }

    public void CloseSetting()
    {
        isOpenSetting = false;
        Time.timeScale = 1f;
        setting.SetActive(false);
    }

    public void Init()
    {
        
    }
}
