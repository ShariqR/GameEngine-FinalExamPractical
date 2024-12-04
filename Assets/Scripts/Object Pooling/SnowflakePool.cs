/*
using UnityEngine;

public class SnowflakePool : MonoBehaviour

{
    Queue<GameObject> snowflakePool = new Queue<GameObject>();
    [SerializeField] int poolSize = 5;

    void Start()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject snowflake = snowflakeFactory.CreateSnowflake(SnowflakeType.Normal);

            snowflake.SetActive(false);

            SnowflakePool.Enqueue(Snowflake);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
*/