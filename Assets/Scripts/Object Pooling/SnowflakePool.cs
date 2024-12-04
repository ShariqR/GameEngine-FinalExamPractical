/*
using UnityEngine;

public class SnowflakePool : MonoBehaviour

{
    SnowflakeFactory snowflakeFactory;
    Queue<GameObject> bulletPool = new Queue<GameObject>();
    [SerializeField] int poolSize = 5;

    void Start()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject snowflake = snowflakeFactory.CreateBullet(BulletType.Normal);

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