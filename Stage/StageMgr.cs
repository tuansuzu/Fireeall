using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageMgr : MonoBehaviour
{
    public static StageMgr Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<StageMgr>();
                if (instance == null)
                {
                    var instanceContainer = new GameObject("StageMgr");
                    instance = instanceContainer.AddComponent<StageMgr>();
                }
            }

            return instance;
        }
    }

    private static StageMgr instance;

    public GameObject Player;

    [System.Serializable]
    public class StartPositionArray
    {
        public List<Transform> StartPosition = new List<Transform>();
    }

    public StartPositionArray[] StartPositionArrays;

    public List<Transform> StartPositionAngel = new List<Transform>();

    public List<Transform> StartPositionBoss = new List<Transform>();

    public Transform StartPositionLastBoss;

    public int currentStage = 0;
    int lastStage = 20;


    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NextStage()
    {
        currentStage++;
        if (currentStage > lastStage)
        {
            return;
        }

        if (currentStage % 5 != 0) // Normal Stage
        {
            int arrayIndex = currentStage / 10;
            int randomIndex = Random.Range(0, StartPositionArrays[arrayIndex].StartPosition.Count);
            Player.transform.position = StartPositionArrays[arrayIndex].StartPosition[randomIndex].position;
            StartPositionArrays[arrayIndex].StartPosition.RemoveAt(randomIndex);
        }
        else // BossRoom or Angel
        {
            if (currentStage % 10 == 5) // Angel
            {
                int randomIndex = Random.Range(0, StartPositionAngel.Count);
                Player.transform.position = StartPositionAngel[randomIndex].position;
                
            }else // Boss
            {
                if (currentStage == lastStage) //LastBoss
                {
                    Player.transform.position = StartPositionLastBoss.position;
                }
                else // Mid Boss
                {
                    int randomIndex = Random.Range(0, StartPositionBoss.Count);
                    Player.transform.position = StartPositionBoss[randomIndex].position;
                    StartPositionBoss.RemoveAt(currentStage / 10);
                }
            }
        }

        CameraMovement.Instance.CameraNextRoom();
    }
}
