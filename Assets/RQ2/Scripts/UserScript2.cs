using IVH.Core.IntelligentVirtualAgent;
using UnityEngine;

public class UserScript2 : MonoBehaviour
{
    [SerializeField]
    private GeminiLiveAgent agent;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            agent.PerformAction("fgesticshrug01");
        }
    }
}
