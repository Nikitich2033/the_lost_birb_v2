using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;
using UnityEngine.UI;

public class Leaderboard : MonoBehaviour
{

    public class LeaderboardTopEight
    {
        public string wallet { get; set; }
        public int score { get; set; }
        public int attempts { get; set; }
    }

    public class PlayerData
    {
        public string wallet { get; set; }
        public int attempts { get; set; }
        public int score { get; set; }
        public int position { get; set; }

    }

    public class Root
    {
        public List<LeaderboardTopEight> leaderboard { get; set; }
        public PlayerData playerData { get; set; }
    }


    public GameObject[] leaderboardArr;
    public GameObject currentPlayer;
    private string wallet = "0x2bdB46441007C395bcC5B97df3941FDfb9d5D78D";
    private string score = "9";


    // Start is called before the first frame update
    void Start()
    {
        // StartCoroutine(PostData());
        StartCoroutine(GetData());
    }

    IEnumerator PostData() {
        WWWForm form = new WWWForm();
        form.AddField("wallet", wallet);
        form.AddField("score", score);
        form.AddField("secretKey", "Nisso-est-bggg");

        UnityWebRequest webRequest = UnityWebRequest.Post("http://localhost:3000/leaderboard", form);

        yield return webRequest.SendWebRequest();

        if(webRequest.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(webRequest.error);
        } else {
            string results = webRequest.downloadHandler.text;
            Debug.Log(results);
        }

        webRequest.Dispose();
    }

    IEnumerator GetData()
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get("http://localhost:3000/leaderboard/"+wallet))
        {
            yield return webRequest.SendWebRequest();

            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError("Something went wrong, {0}: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.Success:
                    
                    Root root = JsonConvert.DeserializeObject<Root>(webRequest.downloadHandler.text);
                    
                    for (int i = 0; i < leaderboardArr.Length-1; i++)
                    {
                        leaderboardArr[i].transform.GetChild(1).GetComponent<Text>().text = root.leaderboard[i].wallet;
                        leaderboardArr[i].transform.GetChild(2).GetComponent<Text>().text = root.leaderboard[i].score.ToString();
                    }

                    currentPlayer.transform.GetChild(1).GetComponent<Text>().text = root.playerData.position.ToString();
                    currentPlayer.transform.GetChild(2).GetComponent<Text>().text = root.playerData.wallet;
                    currentPlayer.transform.GetChild(3).GetComponent<Text>().text = root.playerData.score.ToString();

                    
                    break;
            }
        }
    }

}