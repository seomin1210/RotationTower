using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public int stage = 0;
    public List<GameObject> stageList = new List<GameObject>();

    public GameObject start = null;

    public bool isOpenSetting = false;
    public GameObject setting = null;

    private bool isGameStart = false;

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
        stage = select - 2; //1스테이지 선택 -> -1 NextStage() 시 ++stage로 리스트의 0번째(1스테이지) 실행
        start.SetActive(false);
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
        Destroy(FindObjectOfType<MazeStageMove>().gameObject);
        start.SetActive(true);
    }

    public void NextStage()
    {
        Instantiate(stageList[++stage], null);
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
    public void Die()
    {

    }
}
