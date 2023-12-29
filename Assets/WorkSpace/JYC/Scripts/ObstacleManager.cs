using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    public GameObject[] obstaclePrefabs;

    public Transform cameraTrans;
    public Vector3 targetPos;
    public float lowestY;

    List<GameObject>[] spawnedObstacles;


    void Awake()
    {
        spawnedObstacles = new List<GameObject>[obstaclePrefabs.Length];
        for (int i = 0; i < spawnedObstacles.Length; i++)
        {
            spawnedObstacles[i] = new List<GameObject>();
        }
    }

    void Start()
    {
        targetPos = Vector3.zero;

        SpawnObstacle();
    }
    

    void Update()
    {
        float targetY = targetPos.y;
        float screenY = Camera.main.orthographicSize * 2;

        if(targetY > cameraTrans.position.y - 2 * screenY)
            SpawnObstacle();
        
    }

    void SpawnObstacle()
    {
        if (GameManager.instance.isOverY) return;

        // 장애물 소환하고, 그 다음 소환 위치 지정하기
        int spawnRandom = Random.Range(0, obstaclePrefabs.Length);



        GameObject obstacle = GetObstacle(spawnRandom);
        obstacle.GetComponent<Obstacle>().Spawn(targetPos);
        targetPos = obstacle.GetComponent<Obstacle>().lowest.position;
        lowestY = targetPos.y;
        RemoveObstacle();
    }

    void RemoveObstacle()
    {
        // 이중 배열로 장애물을 모두 저장하고
        // 소환할 때 랜덤 인덱스 값을 얻고
        // 풀링 메니저 처럼 실행하다가 없으면 추가 소환
        // 굳이 항상 할 필요 없이 소환하고 나면 후에 없애는 식으로 해도 충분할 듯

        for (int index = 0; index < obstaclePrefabs.Length; index++)
        {
            // foreach (GameObject item in spawnedObstacles[index].obstacles)
            foreach (GameObject item in spawnedObstacles[index])
            {
                if(item.activeSelf)
                {
                    float screenY = Camera.main.orthographicSize * 2;
                    if (item.GetComponent<Transform>().position.y > cameraTrans.position.y + screenY)
                        item.SetActive(false);
                }
            }
        }
        
    }

    GameObject GetObstacle(int index)
    {
        GameObject obstacle = null;

        // foreach (GameObject item in spawnedObstacles[index].obstacles)
        foreach (GameObject item in spawnedObstacles[index])
        {
                if(!item.activeSelf)
                {
                    obstacle = item;
                    obstacle.SetActive(true);
                    break;
                }
        }

        if (!obstacle)
        {
            obstacle = Instantiate(obstaclePrefabs[index], transform);
            spawnedObstacles[index].Add(obstacle);
        }
            

        return obstacle;
    }
    
    public void ChangeImage()
    {
        
    }

}
