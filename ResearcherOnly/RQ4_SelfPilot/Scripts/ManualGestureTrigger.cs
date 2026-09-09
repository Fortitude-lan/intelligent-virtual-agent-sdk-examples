using UnityEngine;
using IVH.Core.IntelligentVirtualAgent;

public class ManualGestureTrigger : MonoBehaviour
{
    [SerializeField]
    private GeminiLiveAgent agent;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            agent.PerformAction("fcheer01");
        }
    }
}