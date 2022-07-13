using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultUsernameInput : MonoBehaviour
{


    public InputField nameInput;
    private string playerName = null;
    private GameObject thisObject = null;
    private void Awake()
    {
        nameInput = GetComponent<InputField>();
        thisObject = this.gameObject.transform.parent.parent.gameObject;
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return))
        {
            InputName();
        }
    }

    public void InputName()
    {
        if (nameInput.text.Length > 6) return;
        if (nameInput.text.Length <= 0) nameInput.text = "Player";
        playerName = nameInput.text;
        GameManager.Instance.username = playerName;
        thisObject.SetActive(false);
        GameManager.Instance.AddRanking();
    }
}
