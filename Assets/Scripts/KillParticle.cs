using UnityEngine;
using System.Collections;

public class KillParticle : MonoBehaviour
{
    void Update()
    {
        if (!particleSystem.IsAlive())
            PoolsManager.Instance.Despawn(this.gameObject.transform);
    }
}
