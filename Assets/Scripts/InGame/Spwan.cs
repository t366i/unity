using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spwan : MonoBehaviour
{
    private Coroutine CoroutineCheckRect;
    private Coroutine CoroutineTeleport;
    private static WaitForSeconds Cache = new WaitForSeconds(0.2f);
    private static WaitForSeconds Cache2 = new WaitForSeconds(1f);
    Vector3[] PosOutScreen = new Vector3[4];
    GameObject Root;

    public float TotalTime { get; private set; } = 0;
    public bool IsActive = true;

    void Update()
    {
        if (IsActive)
            TotalTime += Time.deltaTime;
    }

    private IEnumerator CheckRect()
    {
        Ray[] rays = new Ray[4];
        RaycastHit[] rayHits = new RaycastHit[4];
        for (int i = 0; i < 4; i++)
        {
            rayHits[i] = new RaycastHit();
            PosOutScreen[i] = new Vector3();
        }

        const float distance = 300;
        while (true)
        {
            Rect rect = Camera.main.pixelRect;
            
            rays[0] = Camera.main.ScreenPointToRay(new Vector3(0, rect.height/2)); // left
            rays[1] = Camera.main.ScreenPointToRay(new Vector3(rect.width, rect.height / 2)); // right
            rays[2] = Camera.main.ScreenPointToRay(new Vector3(rect.width/2, rect.height)); // up
            rays[3] = Camera.main.ScreenPointToRay(new Vector3(rect.width/2, 0)); // down

            for (int i = 0; i < 4; i++)
                Debug.DrawRay(rays[i].origin, rays[i].direction * distance, Color.red, 0.3f);

            for(int i = 0; i<4; i++)
                if (Physics.Raycast(rays[i], out rayHits[i], distance, (1 << 10))) // Layer Mask 10 : Floor
                    PosOutScreen[i] = rayHits[i].point;

            // 이 오류가 뜨면 distance를 늘려줘야 됌. 혹은 floor를 Scale 해줘야 함. 문제 없으면 지우기, 최적화 문제
            for (int i = 0; i < 4; i++)
                if (PosOutScreen[i].magnitude == 0)
                    Debug.Log("Error : MonsterSpawn, can't find Floor");

            yield return Cache;
        }
    }

    private IEnumerator Teleport()
    {
        while (true)
        {
            if (IsActive)
            {
                Vector3 widthVec = PosOutScreen[1] - PosOutScreen[0]; // dir : Left To Right
                Vector3 HeightVec = PosOutScreen[2] - PosOutScreen[3]; // dir : Down To Up
                Vector3 posCenter = (PosOutScreen[3] + PosOutScreen[2]) / 2;
                for (int i=0; i < Root.transform.childCount; i++)
                {
                    if (i % 100 == 0 && i != 0)
                    {
                        yield return Cache;
                        widthVec = PosOutScreen[1] - PosOutScreen[0]; // dir : Left To Right
                        HeightVec = PosOutScreen[2] - PosOutScreen[3]; // dir : Down To Up
                        posCenter = (PosOutScreen[3] + PosOutScreen[2]) / 2;
                    }
                    Vector3 vec = Root.transform.GetChild(i).position - Camera.main.transform.position;
                    vec.y = 0;
                    if(vec.magnitude > widthVec.magnitude*2 || vec.magnitude > HeightVec.magnitude * 2f)
                    {
                        float rand = Random.Range(-0.5f, 0.5f);
                        float rand2 = Random.Range(1.1f, 1.5f);
                        if (Mathf.Abs(vec.x) > Mathf.Abs(vec.z))
                        {
                            if(vec.x < 0)
                                Root.transform.GetChild(i).position = posCenter + widthVec * (rand2 / 2 + 0.05f) * (rand > 0 ? 1.2f + rand * rand * rand : 1) + HeightVec * rand * 1.3f;
                            else
                                Root.transform.GetChild(i).position = posCenter - widthVec * (rand2 / 2 + 0.05f) * (rand > 0 ? 1.2f + rand * rand * rand : 1) + HeightVec * rand * 1.3f;
                        }
                        else
                        {
                            if(vec.z > 0)
                                Root.transform.GetChild(i).position = PosOutScreen[2] - HeightVec * rand2 + (widthVec * rand * 1.7f);
                            else
                                Root.transform.GetChild(i).position = PosOutScreen[3] + HeightVec * rand2 + (widthVec * rand * 1.7f);
                        }
                    }

                }
            }
            yield return Cache2;
        }
    }

    public bool SpawnMonster(string path, int count = 1)
    {
        return SpawnMonster(Managers.Instance.ResourceManager.Load<GameObject>(path), count);
    }

    public bool SpawnMonster(GameObject origin, int count = 1)
    {
        if (origin == null)
            return false;
        GameObject go;

        // Math Coordination
        Vector3 widthVec = PosOutScreen[1] - PosOutScreen[0]; // dir : Left To Right
        Vector3 HeightVec = PosOutScreen[2] - PosOutScreen[3]; // dir : Down To Up
        Vector3 posCenter = (PosOutScreen[3] + PosOutScreen[2]) / 2;

        for (int i=0; i < count; i++)
        {
            go = Managers.Instance.ResourceManager.Instantiate(origin, Root.transform);
            float rand = Random.Range(-0.5f, 0.5f);
            float rand2 = Random.Range(1.1f, 1.5f);
            switch (Random.Range(0, 10)) // 카메라의 Perspective 때문에 위아래 비율이 일정하지 않아서 다음과 같이 테스트해보면서 설정함.
            {
                case 0: // left
                    go.transform.position = posCenter + widthVec * (rand2/2 + 0.05f) * (rand > 0 ? 1.2f + rand*rand*rand : 1) + HeightVec* rand*1.3f;
                    break;
                case 1: // left
                    go.transform.position = posCenter + widthVec * (rand2 / 2 + 0.05f) * (rand > 0 ? 1.2f + rand * rand * rand : 1) + HeightVec * rand * 1.3f;
                    break;
                case 2: // right
                    go.transform.position = posCenter - widthVec * (rand2/2 + 0.05f) * (rand > 0 ? 1.2f + rand *rand*rand : 1) + HeightVec * rand * 1.3f;
                    break;
                case 3: // right
                    go.transform.position = posCenter - widthVec * (rand2 / 2 + 0.05f) * (rand > 0 ? 1.2f + rand * rand * rand : 1) + HeightVec * rand * 1.3f;
                    break;
                case 4: // up
                    go.transform.position = PosOutScreen[3] + HeightVec * rand2 + (widthVec * rand*1.7f);
                    break;
                case 5: // up
                    go.transform.position = PosOutScreen[3] + HeightVec * rand2 + (widthVec * rand * 1.7f);
                    break;
                case 6: // up
                    go.transform.position = PosOutScreen[3] + HeightVec * rand2 + (widthVec * rand * 1.7f);
                    break;
                case 7: // down
                    go.transform.position = PosOutScreen[2] - HeightVec * rand2 + (widthVec * rand * 1.7f);
                    break;
                case 8: // down
                    go.transform.position = PosOutScreen[2] - HeightVec * rand2 + (widthVec * rand * 1.7f);
                    break;
                case 9: // down
                    go.transform.position = PosOutScreen[2] - HeightVec * rand2 + (widthVec * rand * 1.7f);
                    break;
            }
            Collider collider = go.GetComponent<Collider>();
            if (collider != null)
                go.transform.position = new Vector3(go.transform.position.x,collider.bounds.center.y,go.transform.position.z);
        }
        return true;
    }

    IEnumerator ClearSpawn()
    {
        if (Root != null)
        {
            while (Root.transform.childCount != 0)
            {
                for (int i = 0; i < Root.transform.childCount; i++)
                    Managers.Instance.ResourceManager.Destroy(Root.transform.GetChild(i).gameObject);
                yield return new WaitForSeconds(0.1f);
            }
        }
    }

    void Awake()
    {
        if (gameObject.name != "@GameManager")
            Object.Destroy(this);
        Root = new GameObject("@Object");
        Root.transform.parent = gameObject.transform;
    }

    void Start()
    {
        if (CoroutineCheckRect != null)
            StopCoroutine(CoroutineCheckRect);
        if (CoroutineTeleport != null)
            StopCoroutine(CoroutineTeleport);
        IsActive = true;
        CoroutineCheckRect = StartCoroutine(CheckRect());
        CoroutineTeleport = StartCoroutine(Teleport());
    }

    void OnApplicationQuit()
    {
        if(CoroutineCheckRect != null)
        {
            StopCoroutine(CoroutineCheckRect);
            CoroutineCheckRect = null;
        }
        if (CoroutineTeleport != null)
        {
            StopCoroutine(CoroutineTeleport);
            CoroutineTeleport = null;
        }
        IsActive = false;
        StartCoroutine(ClearSpawn());
    }
}
