using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChallengeManager : MonoBehaviour
{
    public static ChallengeManager Instance;

    [Header("UI References")]
    [SerializeField] private GameObject challengePanel;
    [SerializeField] private TextMeshProUGUI questionText;
    [SerializeField] private Button answerBtn1;
    [SerializeField] private Button answerBtn2;
    [SerializeField] private Button answerBtn3;
    [SerializeField] private GameObject completionParticlePrefab;

    private int correctAnswerIndex;
    private string currentZone;

    private void Awake()
    {
        Instance = this;
    }

    public void OpenChallenge(string zoneName)
    {
        currentZone = zoneName;
        challengePanel.SetActive(true);
        Time.timeScale = 0f;

        switch (zoneName)
        {
            case "MentorshipLounge":
                SetupChallenge(
                    "Your mentor asks: you want to learn coding but don't know where to start. What do you do?",
                    "Watch random YouTube videos and hope for the best",
                    "Ask your mentor for a structured learning path",
                    "Buy every programming book you can find",
                    2
                );
                break;

            case "DevicesRoom":
                SetupChallenge(
                    "Which device is best for controlling a hardware sensor?",
                    "Tablet",
                    "Laptop",
                    "Raspberry Pi",
                    3
                );
                break;

            case "SkillsLab":
                SetupChallenge(
                    "Which of these is a programming language?",
                    "HTML",
                    "Python",
                    "Photoshop",
                    2
                );
                break;

            case "CommunityBoard":
                SetupChallenge(
                    "A hackathon just dropped at the hub. What is a hackathon?",
                    "A cybersecurity attack on a computer system",
                    "A cooking competition for tech enthusiasts",
                    "A sprint event where people build tech solutions together",
                    3
                );
                break;
        }
    }

    private void SetupChallenge(string question, string a1, string a2, string a3, int correct)
    {
        questionText.text = question;
        answerBtn1.GetComponentInChildren<TextMeshProUGUI>().text = a1;
        answerBtn2.GetComponentInChildren<TextMeshProUGUI>().text = a2;
        answerBtn3.GetComponentInChildren<TextMeshProUGUI>().text = a3;
        correctAnswerIndex = correct;

        answerBtn1.onClick.RemoveAllListeners();
        answerBtn2.onClick.RemoveAllListeners();
        answerBtn3.onClick.RemoveAllListeners();

        answerBtn1.onClick.AddListener(() => CheckAnswer(1));
        answerBtn2.onClick.AddListener(() => CheckAnswer(2));
        answerBtn3.onClick.AddListener(() => CheckAnswer(3));
    }

    private void CheckAnswer(int selected)
    {
        if (selected == correctAnswerIndex)
        {
            Debug.Log("Correct! Zone unlocked: " + currentZone);
            ProgressTracker.Instance.UnlockZone(currentZone);
            CloseChallenge();
            GameObject zone = GameObject.Find(currentZone);
            if (zone != null)
            {
                GameObject particle = Instantiate(completionParticlePrefab, zone.transform.position, Quaternion.identity);
                ParticleSystem ps = particle.GetComponent<ParticleSystem>();
                if (ps != null)
                {
                    var main = ps.main;
                    main.simulationSpace = ParticleSystemSimulationSpace.World;
                    ps.Play();
                }
            }
        }
        else
        {
            Debug.Log("Wrong answer, try again");
            StartCoroutine(FlashWrong(selected));
        }
    }

    private System.Collections.IEnumerator FlashWrong(int btnIndex)
    {
        Button btn = btnIndex == 1 ? answerBtn1 : btnIndex == 2 ? answerBtn2 : answerBtn3;
        ColorBlock colors = btn.colors;
        Color original = colors.normalColor;
        colors.normalColor = Color.red;
        btn.colors = colors;
        yield return new WaitForSecondsRealtime(0.5f);
        colors.normalColor = original;
        btn.colors = colors;
    }

    public void CloseChallenge()
    {
        challengePanel.SetActive(false);
        Time.timeScale = 1f;
    }
}