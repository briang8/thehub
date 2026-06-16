using UnityEngine;

public class HideMobileControls : MonoBehaviour
{
    void Start()
    {
        
        #if UNITY_WEBGL && !UNITY_EDITOR
            gameObject.SetActive(false);
        #endif
    }
}   