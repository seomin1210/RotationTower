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
        if(nameInput.text.Length > 0 && Input.GetKeyDown(KeyCode.Return))
        {
            InputName();
        }
    }

    public void InputName()
    {
        if (nameInput.text.Length > 10) return;
        if (nameInput.text.Length <= 0) return;
        playerName = nameInput.text;
        GameManager.Instance.username = playerName;
        thisObject.SetActive(false);
        GameManager.Instance.AddRanking();
    }
}
