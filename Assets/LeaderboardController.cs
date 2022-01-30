using UnityEngine;
using UnityEngine.UI;
using LootLocker.Requests;

public class LeaderboardController : MonoBehaviour
{
    public InputField memberID;
    public int ID;
    int maxScores = 10;
    public Text[] entries;

    private void Start()
    {
        LootLockerSDKManager.StartSession("Player", (response) =>
        {
            if (response.success)
            {
                Debug.Log("Session Success");
            }
            else
            {
                Debug.Log("Session Failed");
            }
        });
    }

    public void ShowScores()
    {
        LootLockerSDKManager.GetScoreList(ID, maxScores, (response) =>
        {
            if (response.success)
            {
                LootLockerLeaderboardMember[] scores = response.items;

                for (int i = 0; i < scores.Length; i++)
                {
                    entries[i].text = (scores[i].rank + ".   " + scores[i].score);
                }

                if (scores.Length < maxScores)
                {
                    for (int i = scores.Length; i < maxScores; i++)
                    {
                        entries[i].text = (i + 1).ToString() + ".   -";
                    }
                }
            }
            else
            {
                Debug.Log("Failed");
            }
        });
    }

    public void SubmitScore()
    {
        LootLockerSDKManager.SubmitScore(memberID.text, KillCounter.Instance.GetKills(), ID, (response) =>
        {
            if (response.success)
            {
                Debug.Log("Submit Success");
            }
            else
            {
                Debug.Log("Submit Failed");
            }
        });
    }
}
