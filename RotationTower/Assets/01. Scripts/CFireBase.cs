using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Analytics;
using Firebase.Auth;
using Firebase.Database;
using Firebase.Unity;
using TMPro;
using System;
using System.Linq;
public class CFireBase : MonoBehaviour
{
    public static CFireBase Instance;

    public class User
    {
        public string username;
        public string score;
        public User(string username, string score)
        {
            this.username = username;
            this.score = score;
        }
    }
    DatabaseReference reference;

    public TextMeshProUGUI roading = null;
    public TextMeshProUGUI[] rank = new TextMeshProUGUI[7];

    public string[] strRank;
    private long strLen;

    private bool textLoadBool = false;

    public struct UserData
    {
        public string name;
        public int score;
    }

    public UserData[] userData;

    void Awake()
    {
        reference = FirebaseDatabase.DefaultInstance.RootReference;
        Instance = this;
    }

    private void Update()
    {
        if (roading.text == "Loading..")
        {
            DataLoad();
        }
    }

    private void LateUpdate()
    {
        if(textLoadBool == true)
        {
            TextLoad();
        }
        if (Time.timeScale != 0f) Time.timeScale = 0f;
    }

    public void writeNewUser(string username, int score)
    {
        int cnt = 0;
        reference.Child("rank").GetValueAsync().ContinueWith(task =>
        {
            if (task.IsFaulted)
            {
                //���� �����ͷε� ���� �� �ٽ� ������ �ε�
                DataLoad();
            }
            else if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;

                strLen = snapshot.ChildrenCount;
                userData = new UserData[strLen];

                foreach (DataSnapshot data in snapshot.Children)
                {
                    //���� �����͵��� �ϳ��� �߶� �迭�� ����
                    IDictionary rankInfo = (IDictionary)data.Value;
                    userData[cnt].name = rankInfo["username"].ToString();
                    userData[cnt].score = Int32.Parse(rankInfo["score"].ToString());
                    cnt++;
                }
            }
        });
        cnt = 0;
        foreach (var data in userData)
        {
            if(data.name == username)
            {
                if(data.score < score)
                {
                    User user = new User(username, score.ToString());
                    string json = JsonUtility.ToJson(user);
                    reference.Child("rank").Child(username).SetRawJsonValueAsync(json);
                }
                break;
            }
        }
        reference.OrderByChild("rank");
        roading.text = "Loading..";
    }

    private void DataLoad()
    {
        reference.Child("rank").GetValueAsync().ContinueWith(task =>
        {
            if (task.IsFaulted)
            {
                //���� �����ͷε� ���� �� �ٽ� ������ �ε�
                DataLoad();
            }
            else if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;

                int cnt = 0;
                strLen = snapshot.ChildrenCount;
                strRank = new string[strLen];
                userData = new UserData[strLen];

                foreach (DataSnapshot data in snapshot.Children)
                {
                    //���� �����͵��� �ϳ��� �߶� �迭�� ����
                    IDictionary rankInfo = (IDictionary)data.Value;
                    userData[cnt].name = rankInfo["username"].ToString();
                    userData[cnt].score = Int32.Parse(rankInfo["score"].ToString());
                    cnt++;
                }
                //LateUpdate�� TextLoad() �Լ� ����
                textLoadBool = true;
            }
        });
    }

    private void TextLoad()
    {
        int cnt = 0;
        textLoadBool = false;
        List<UserData> SortedList = userData.OrderByDescending(x => x.score).ToList();

        foreach(var data in SortedList)
        {
            strRank[cnt] = data.name + " | " + string.Format("{0}", data.score);
            cnt++;
         }
        for(int i = 0; i < rank.Length; i++)
        {
            if (strLen <= i) break;
            rank[i].text = strRank[i];
        }
        roading.text = "Raking";
    }
}
