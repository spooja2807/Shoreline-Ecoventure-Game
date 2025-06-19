using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Collections;

public class RewardData : MonoBehaviour
{
    public GameObject rewardPopupPanel;
    public GameObject rewardButtonPrefab;
    public Transform rewardGridParent;
    public TextMeshProUGUI rewardCollectedText;

    [System.Serializable]
    public class Reward
    {
        public string rewardName;
        public Sprite icon;
    }

    public Reward[] rewards;

    private Action onRewardCollectedCallback;

    void Start()
    {
        rewardPopupPanel.SetActive(false);
        rewardCollectedText.text = "";
    }

    public void ShowRewards(Action onRewardCollected)
    {
        rewardPopupPanel.SetActive(true);
        rewardCollectedText.text = "Choose a reward!";
        onRewardCollectedCallback = onRewardCollected;

        // Clear previous buttons
        foreach (Transform child in rewardGridParent)
        {
            Destroy(child.gameObject);
        }

        // Create reward buttons
        foreach (Reward r in rewards)
        {
            GameObject btn = Instantiate(rewardButtonPrefab, rewardGridParent);
            btn.transform.Find("Icon").GetComponent<Image>().sprite = r.icon;

            btn.GetComponent<Button>().onClick.AddListener(() =>
            {
                rewardCollectedText.text = "Reward Collected!!: " + r.rewardName + "!";
                StartCoroutine(HideRewardTextAfterSeconds(2f));
                rewardPopupPanel.SetActive(false);

                // Notify that reward was collected
                onRewardCollectedCallback?.Invoke();
            });
        }
    }

    IEnumerator HideRewardTextAfterSeconds(float delay)
    {
        yield return new WaitForSeconds(delay);
        rewardCollectedText.text = "";
    }
}
