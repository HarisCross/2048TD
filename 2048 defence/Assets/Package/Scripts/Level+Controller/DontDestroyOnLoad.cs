using UnityEngine;

public class DontDestroyOnLoad : MonoBehaviour
{
    // Use this for initialization
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}