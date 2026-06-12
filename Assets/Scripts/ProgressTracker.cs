using UnityEngine;

public class ProgressTracker : MonoBehaviour
{
    public static ProgressTracker Instance;

    private bool mentorshipDone = false;
    private bool devicesDone = false;
    private bool skillsDone = false;
    private bool communityDone = false;

    private void Awake()
    {
        Instance = this;
    }

    public void UnlockZone(string zoneName)
    {
        switch (zoneName)
        {
            case "MentorshipLounge": mentorshipDone = true; break;
            case "DevicesRoom": devicesDone = true; break;
            case "SkillsLab": skillsDone = true; break;
            case "CommunityBoard": communityDone = true; break;
        }

        CheckWinCondition();
    }

    public bool IsZoneDone(string zoneName)
    {
        switch (zoneName)
        {
            case "MentorshipLounge": return mentorshipDone;
            case "DevicesRoom": return devicesDone;
            case "SkillsLab": return skillsDone;
            case "CommunityBoard": return communityDone;
            default: return false;
        }
    }

    private void CheckWinCondition()
    {
        if (mentorshipDone && devicesDone && skillsDone && communityDone)
        {
            Debug.Log("ALL ZONES COMPLETE - TRIGGER WIN");
        }
    }
}