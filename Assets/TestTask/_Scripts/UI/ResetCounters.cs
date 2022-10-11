using UnityEngine;

public class ResetCounters : MonoBehaviour
{
    public void ResetGameCounters()
    {
        UIManager.Instance.ResetTimer();
    }
}
