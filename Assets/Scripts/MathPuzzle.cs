using UnityEngine;
using UnityEngine.UI;

public class MathPuzzle : MonoBehaviour
{
    public Text equationText;
    public Button option1, option2, option3;
    private int correctAnswer;

    void OnEnable()
    {
        GenerateEquation();
    }

    void GenerateEquation()
    {
        int num1 = Random.Range(1, 10);
        int num2 = Random.Range(1, 10);
        int operation = Random.Range(0, 3); // 0 = +, 1 = -, 2 = *
        string equation = "";

        switch (operation)
        {
            case 0:
                correctAnswer = num1 + num2;
                equation = num1 + " + " + num2 + " = ?";
                break;
            case 1:
                correctAnswer = num1 - num2;
                equation = num1 + " - " + num2 + " = ?";
                break;
            case 2:
                correctAnswer = num1 * num2;
                equation = num1 + " × " + num2 + " = ?";
                break;
        }

        equationText.text = equation;

        // Remove previous listeners.
        option1.onClick.RemoveAllListeners();
        option2.onClick.RemoveAllListeners();
        option3.onClick.RemoveAllListeners();

        // Generate wrong answers.
        int wrongAnswer1 = correctAnswer + Random.Range(1, 5);
        int wrongAnswer2 = correctAnswer - Random.Range(1, 5);

        Button[] buttons = { option1, option2, option3 };
        int correctPos = Random.Range(0, 3);
        buttons[correctPos].GetComponentInChildren<Text>().text = correctAnswer.ToString();
        buttons[correctPos].onClick.AddListener(() => CorrectAnswer());

        int index = 0;
        for (int i = 0; i < 3; i++)
        {
            if (i == correctPos) continue;
            int wrongAnswer = (index == 0 ? wrongAnswer1 : wrongAnswer2);
            buttons[i].GetComponentInChildren<Text>().text = wrongAnswer.ToString();
            buttons[i].onClick.AddListener(() => WrongAnswer());
            index++;
        }
    }

    void CorrectAnswer()
    {
        Debug.Log("✅ Correct Answer! Animal Rescued.");
        // Use the currentAnimal reference to rescue the correct animal.
        if (AnimalHelp.currentAnimal != null)
        {
            AnimalHelp.currentAnimal.SolvePuzzle();
        }
    }

    public void WrongAnswer()
{
    Debug.Log("❌ Wrong Answer! Animal Not Rescued.");
    // Pass 0f so that the message disappears immediately.
    ScoreManager.instance.ShowErrorPrompt("Not Rescued!", 1f);
    if (AnimalHelp.currentAnimal != null)
    {
        AnimalHelp.currentAnimal.FailPuzzle();
    }
}

}
