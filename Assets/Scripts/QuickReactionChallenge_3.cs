//using UnityEngine;
//using UnityEngine.UI;
//using TMPro;  // Import TextMeshPro
//using System.Collections;

//public class QuickReactionChallenge : MonoBehaviour
//{
//    public GameObject challengePanel;  // UI panel for the challenge
//    public RectTransform fillBar;  // The moving progress bar
//    public float barSpeed = 500f;  // Speed of bar movement
//    public RectTransform greenZone;  // The "success" area
//    public TMP_Text feedbackText;  //  TMP_Text for feedback

//    private bool movingRight = true;
//    private bool challengeActive = false;
//    private AnimalRescue currentAnimal; // Reference to the animal being rescued

//    void Update()
//    {
//        if (challengeActive)
//        {
//            // Move the bar back and forth
//            float moveAmount = barSpeed * Time.deltaTime;
//            if (movingRight)
//            {
//                fillBar.anchoredPosition += new Vector2(moveAmount, 0);
//                if (fillBar.anchoredPosition.x >= 200) // Adjust based on UI size
//                {
//                    movingRight = false;
//                }
//            }
//            else
//            {
//                fillBar.anchoredPosition -= new Vector2(moveAmount, 0);
//                if (fillBar.anchoredPosition.x <= -200) // Adjust based on UI size
//                {
//                    movingRight = true;
//                }
//            }

//            // Detect space press
//            if (Input.GetKeyDown(KeyCode.Space))
//            {
//                CheckIfSuccessful();
//            }
//        }
//    }

//    public void StartChallenge(AnimalRescue animal)
//    {
//        challengePanel.SetActive(true);
//        challengeActive = true;
//        fillBar.anchoredPosition = new Vector2(-200, 0);
//        movingRight = true;
//        currentAnimal = animal;

//        feedbackText.text = "";  //  Clear previous feedback
//    }

//    private void CheckIfSuccessful()
//    {
//        if (fillBar.anchoredPosition.x >= greenZone.anchoredPosition.x - greenZone.rect.width / 2 &&
//            fillBar.anchoredPosition.x <= greenZone.anchoredPosition.x + greenZone.rect.width / 2)
//        {
//            StartCoroutine(CloseChallenge(true)); // Success
//        }
//        else
//        {
//            StartCoroutine(CloseChallenge(false)); // Failure
//        }
//    }

//    IEnumerator CloseChallenge(bool success)
//    {
//        if (success)
//        {
//            feedbackText.text = "Rescue Successful!";
//            feedbackText.color = Color.green;
//        }
//        else
//        {
//            feedbackText.text = " Try Again!";
//            feedbackText.color = Color.red;
//        }

//        yield return new WaitForSeconds(1.5f); // Show feedback for 1.5 seconds

//        feedbackText.text = "";

//        challengePanel.SetActive(false);
//        challengeActive = false;

//        if (success && currentAnimal != null)
//        {
//            currentAnimal.CompleteRescue(); // Rescue the animal
//        }
//    }
//}using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;
using UnityEngine;

public class QuickReactionChallenge_3 : MonoBehaviour
{
    public GameObject challengePanel;
    public RectTransform fillBar;
    public float barSpeed = 500f;
    public RectTransform greenZone;
    public TMP_Text feedbackText;

    private bool movingRight = true;
    private bool challengeActive = false;
    private AnimalRescue_3 currentAnimal;

    void Update()
    {
        if (challengeActive)
        {
            float moveAmount = barSpeed * Time.deltaTime;
            if (movingRight)
            {
                fillBar.anchoredPosition += new Vector2(moveAmount, 0);
                if (fillBar.anchoredPosition.x >= 200)
                {
                    movingRight = false;
                }
            }
            else
            {
                fillBar.anchoredPosition -= new Vector2(moveAmount, 0);
                if (fillBar.anchoredPosition.x <= -200)
                {
                    movingRight = true;
                }
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                CheckIfSuccessful();
            }
        }
    }

    public void StartChallenge(AnimalRescue_3 animal)
    {
        InputBlocker.IsInputBlocked = true; //  Block other input
        challengePanel.SetActive(true);
        challengeActive = true;
        fillBar.anchoredPosition = new Vector2(-200, 0);
        movingRight = true;
        currentAnimal = animal;
        feedbackText.text = "";
    }

    private void CheckIfSuccessful()
    {
        if (fillBar.anchoredPosition.x >= greenZone.anchoredPosition.x - greenZone.rect.width / 2 &&
            fillBar.anchoredPosition.x <= greenZone.anchoredPosition.x + greenZone.rect.width / 2)
        {
            feedbackText.text = "Rescue Successful!";
            feedbackText.color = Color.green;
            StartCoroutine(CloseChallenge(true));
        }
        else
        {
            feedbackText.text = "Try Again!";
            feedbackText.color = Color.red;
            StartCoroutine(CloseChallenge(false));
        }
    }

    IEnumerator CloseChallenge(bool success)
    {
        yield return new WaitForSeconds(1.5f);

        challengePanel.SetActive(false);
        challengeActive = false;
        InputBlocker.IsInputBlocked = false; //  Unblock input

        if (success && currentAnimal != null)
        {
            currentAnimal.CompleteRescue();
            ScoreManager_3.instance.AddAnimalScore(10);
        }
    }
}
