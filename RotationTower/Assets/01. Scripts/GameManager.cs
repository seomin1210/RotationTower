using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public int stage = 0;
    public List<GameObject> stageList = new List<GameObject>();

    private bool isOpenSetting = false;

    private void Start()
    {
        if (Instance != null)
            Debug.LogError("GameManager is Multi Playing");
        Instance = this;
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

    public void NextStage()
    {
        Instantiate(stageList[++stage], null);
    }

    public void OpenSetting()
    {
        isOpenSetting = true;
    }

    public void CloseSetting()
    {
        isOpenSetting = false;
    }
}
