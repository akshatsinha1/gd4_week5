using UnityEngine;

public class LoopTest : MonoBehaviour
{
    public GameObject[] gameobjects;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        forLoop();
        foreachLoop();
    }

    private void Update()
    {
        //whileLoop();
    }
    void forLoop()
    {
       for(int i = 0; i <gameobjects.Length; i++)
        {
            // gameobjects[i].SetActive(false);
            gameobjects[i].transform.position = new Vector3(gameobjects[i].transform.position.x, i, gameobjects[i].transform.position.z);
        }
    }

    void foreachLoop()
    {
        foreach(GameObject obj in gameobjects)
        {
            obj.AddComponent<Rigidbody>();

            if(obj.transform.tag == "Enemy")
            {
                Destroy(obj);
            }
        }
    }

    void whileLoop()
    {
        while(Time.time < 5)
        {
            gameobjects[0].transform.Translate(Vector3.forward * Time.deltaTime);
        }
    }
}
