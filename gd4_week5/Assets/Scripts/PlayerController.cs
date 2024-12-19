using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float moveSpeed = 4;
    Rigidbody rb;
    [SerializeField] Transform focalPoint;
    Vector3 startPosition;
    [SerializeField] float yRange;
    public bool hasRepelPower;
    [SerializeField] float repelForce;
    [SerializeField] GameObject repelPowerIndicator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float verticalInput = Input.GetAxis("Vertical"); //up-down/W-S keys
        float horizontalInput = Input.GetAxis("Horizontal"); //left-right/a-d keys
        Vector2 moverDir = new Vector2(horizontalInput, verticalInput).normalized;

        // rb.AddForce(focalPoint.forward * moveSpeed * verticalInput);

        rb.AddForce((focalPoint.forward * moverDir.y + focalPoint.right * moverDir.x) * moveSpeed,ForceMode.Force);

        //keeps the focal point where the player is, centering the camera to the player
        focalPoint.position = transform.position;

        if(transform.position.y < yRange)
        {
            transform.position = startPosition;
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

        //if (hasRepelPower == true) repelPowerIndicator.SetActive(true);
        //else repelPowerIndicator.SetActive(false);

        //turns the indicator on/off based on the boolean
        repelPowerIndicator.SetActive(hasRepelPower);

        //make the indicator follow the  player
        repelPowerIndicator.transform.position = transform.position + new Vector3(0,2.3f,0);

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "PowerUpRepel")
        {
            //destroying the powerup icon
            Destroy(other.gameObject);
            //turning on the power up

            //turning off the power up in a few seconds
            StartCoroutine(repelPowerCooldown());
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == "Enemy" && hasRepelPower == true)
        {
            //have a knockback funtion
            //calculate the direction of the knockback
            Vector3 repelDirection = (collision.transform.position - transform.position).normalized;
            //apply it to the rigidbody of the enemy
            Rigidbody enemyRb = collision.transform.GetComponent<Rigidbody>();

            enemyRb.AddForce(repelDirection * repelForce, ForceMode.Impulse);
        }
    }
   
    private IEnumerator repelPowerCooldown()
    {
        hasRepelPower = true;
        //wait for 5 seconds
        yield return new WaitForSeconds(5);
        hasRepelPower = false;

        yield return new WaitForSeconds(3);
        hasRepelPower = true;

        yield return new WaitForSeconds(2);
        hasRepelPower = false;


    }

}
