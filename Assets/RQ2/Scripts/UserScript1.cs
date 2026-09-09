using UnityEngine;
using IVH.Core.IntelligentVirtualAgent;
public class UserScript1 : MonoBehaviour
{
    [SerializeField]
    private GeminiLiveAgent agent;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            agent.PerformAction("fgesticthoughtful01");
        }
    }
}
