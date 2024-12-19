using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 4;
    Rigidbody rb;
    Transform playerTransform;
    bool canFollow;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        playerTransform = GameObject.FindFirstObjectByType<PlayerController>().transform;
        //playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        //playerTransform = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 moveDirection = (playerTransform.position - transform.position).normalized;
        if(canFollow)rb.AddForce(moveDirection * moveSpeed);

        if (transform.position.y < -15) Destroy(gameObject);
    }

    private void OnCollisionStay(Collision collision)
    {
        canFollow = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        canFollow = false;
    }

    
}
