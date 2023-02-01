using System.Collections;
using UnityEngine;

public class GameStage : MonoBehaviour
{
    Spwan Game;

    private int SpawnAmountPerSec = 1;
    private GameObject DefaultSpawnOrigin = null;
    private Coroutine CoroutineDefaultSpawn = null;
    
    IEnumerator DefaultSpawn()
    {
        while (true)
        {
            if (Game.IsActive)
                if (DefaultSpawnOrigin != null)
                    Game.SpawnMonster(DefaultSpawnOrigin, SpawnAmountPerSec / 10 != 0 ? SpawnAmountPerSec / 10 : SpawnAmountPerSec);
            yield return new WaitForSeconds(SpawnAmountPerSec / 10 != 0 ? 0.1f/((float)SpawnAmountPerSec/10) : 1.0f);
        }
    }
    IEnumerator MassiveSpawn(GameObject origin, int amountPerSec, int duration)
    {
        float init = 0;
        while (init < duration)
        {
            if (Game.IsActive)
            {
                if (origin != null)
                    Game.SpawnMonster(origin, amountPerSec / 10 != 0 ? amountPerSec / 10 : amountPerSec);
            }
            init += amountPerSec / 10 != 0 ? 0.1f / ((float)amountPerSec / 10) : 1.0f;
            yield return new WaitForSeconds(amountPerSec / 10 != 0 ? 0.1f / ((float)amountPerSec / 10) : 1.0f);
        }
    }

    void Awake()
    {
        GameObject go = GameObject.Find("@GameManager");
        if(go == null)
            go = new GameObject("@GameManager");
        Game = go.GetComponent<Spwan>();
        if (Game == null)
            Game = go.AddComponent<Spwan>();

        if (gameObject.name != "@GameManager")
        {
            go.AddComponent<GameStage>();
            Object.Destroy(this);
        }
    }

    private void Start()
    {
        DefaultSpawnOrigin = Managers.Instance.ResourceManager.Load<GameObject>("Prefabs/testPrefab");
        CoroutineDefaultSpawn = StartCoroutine(DefaultSpawn());
    }

    void OnApplicationQuit()
    {
        if (CoroutineDefaultSpawn != null)
            StopCoroutine(CoroutineDefaultSpawn);
    }


}
