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

    private string[] strRank;
    private long strLen;

    private bool textLoadBool = false;

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
        User user = new User(username, score.ToString());
        string json = JsonUtility.ToJson(user);
        reference.Child("rank").Child(username).SetRawJsonValueAsync(json);
        reference.OrderByChild("rank");
        roading.text = "Loading..";
    }

    private void DataLoad()
    {
        reference.Child("rank").GetValueAsync().ContinueWith(task =>
        {
            if (task.IsFaulted)
            {
                //에러 데이터로드 실패 시 다시 데이터 로드
                DataLoad();
            }
            else if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;

                int cnt = 0;
                strLen = snapshot.ChildrenCount;
                strRank = new string[strLen];

                foreach (DataSnapshot data in snapshot.Children)
                {
                    //받은 데이터들을 하나씩 잘라 배열에 저장
                    IDictionary rankInfo = (IDictionary)data.Value;

                    strRank[cnt] = rankInfo["username"].ToString() + " | " + string.Format("{0:N2}", rankInfo["score"]);
                    cnt++;
                }
                //LateUpdate의 TextLoad() 함수 실행
                textLoadBool = true;
            }
        });
    }

    private void TextLoad()
    {
        textLoadBool = false;
        try
        {
            //  Array.Sort(strRank, (x, y) => string.Compare(
            // y.Substring(y.Length - 5, 5).ToString() + x.Substring(x.Length - 5, 5).ToString(),
            //x.Substring(x.Length - 5, 5).ToString() + y.Substring(y.Length - 5, 5).ToString()));
            Array.Reverse(strRank);
        }
        catch(NullReferenceException e)
        {
            return;
        }
        for(int i = 0; i < rank.Length; i++)
        {
            if (strLen <= i) return;
            rank[i].text = strRank[i];
        }
        roading.text = "Raking";
    }
}
