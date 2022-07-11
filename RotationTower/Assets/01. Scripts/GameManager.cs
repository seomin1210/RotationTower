using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public int stage = 0;
    public List<GameObject> stageList = new List<GameObject>();

    private void Start()
    {
        if (Instance != null)
            Debug.LogError("GameManager is Multi Playing");
        Instance = this;
    }

    public void NextStage()
    {
        Instantiate(stageList[++stage], null);
    }
}
