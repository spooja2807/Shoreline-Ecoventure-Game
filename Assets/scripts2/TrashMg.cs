using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class TrashMg : MonoBehaviour
{
    public int trashScore = 0;
    public TextMeshProUGUI scoreText;
    public Image[] starImages;
    public TextMeshProUGUI rewardMessageText;

    public int[] starThresholds = { 20, 30, 60 };

    public RewardData rewardDataScript;
    public Level5Manager levelManager;

    [Header("Three Star Popup UI")]
    public GameObject threeStarPopup;
    public Button collectRewardsButton;
    public Button visitSafeZoneButton;

    [Header("Safe Zone UI")]
    public GameObject safeZonePanel;
    public Button closeSafeZoneButton;

    private bool hasOpenedRewardPopup = false;

    void Start()
    {
        UpdateUI();
        rewardMessageText.text = "";

        if (closeSafeZoneButton != null)
        {
            closeSafeZoneButton.onClick.AddListener(() =>
            {
                safeZonePanel.SetActive(false);
                if (!levelManager.levelEnded)
                    levelManager.CheckForLevelCompletion(trashScore);
            });
        }
    }

    public void AddScore(int points)
    {
        trashScore += points;
        UpdateUI();
        ShowRewardMessage();

        if (trashScore >= 60 && !hasOpenedRewardPopup)
        {
            threeStarPopup.SetActive(true);
        }
    }

    void UpdateUI()
    {
        scoreText.text = "" + trashScore;

        for (int i = 0; i < starImages.Length; i++)
        {
            if (i < starThresholds.Length)
            {
                starImages[i].color = (trashScore >= starThresholds[i]) ? Color.yellow : Color.gray;
            }
        }
    }

    void ShowRewardMessage()
    {
        StopAllCoroutines();

        if (trashScore >= starThresholds[2]) // 60 points
        {
            rewardMessageText.text = "You can now collect rewards or visit the safe zone!";
            rewardMessageText.color = Color.green;
            StartCoroutine(HideMessageAfterSeconds(3f));

            if (!hasOpenedRewardPopup)
            {
                threeStarPopup.SetActive(true);

                collectRewardsButton.onClick.RemoveAllListeners();
                collectRewardsButton.onClick.AddListener(() =>
                {
                    rewardDataScript.ShowRewards(() =>
                    {
                        levelManager.CheckForLevelCompletion(trashScore);
                    });

                    threeStarPopup.SetActive(false);
                    hasOpenedRewardPopup = true;
                });

                visitSafeZoneButton.onClick.RemoveAllListeners();
                visitSafeZoneButton.onClick.AddListener(() =>
                {
                    ShowSafeZone();
                    threeStarPopup.SetActive(false);
                    hasOpenedRewardPopup = true;
                });
            }
        }
        else
        {
            rewardMessageText.text = "Not eligible to claim. Keep cleaning!";
            rewardMessageText.color = Color.red;
            StartCoroutine(HideMessageAfterSeconds(3f));
        }
    }

    IEnumerator HideMessageAfterSeconds(float delay)
    {
        yield return new WaitForSeconds(delay);
        rewardMessageText.text = "";
    }

    void ShowSafeZone()
    {
        if (safeZonePanel != null)
        {
            safeZonePanel.SetActive(true);
        }
    }
}
