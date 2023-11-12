using UnityEngine;

public class Fruit : MonoBehaviour
{
    //public GameObject whole;
    //public GameObject sliced;

    private Rigidbody fruitRigidbody;
    private Collider fruitCollider;
    public ParticleSystem juiceEffect;

    public int points = 1;

    private void Awake()
    {
        fruitRigidbody = GetComponent<Rigidbody>();
        fruitCollider = GetComponent<Collider>();
        //juiceEffect = GetComponentInChildren<ParticleSystem>();
    }

    private void Slice(Vector3 direction, Vector3 position, float force)
    {
        juiceEffect.transform.SetParent(null);
        FindObjectOfType<GameManager>().IncreaseScore(points);
        juiceEffect.Play();
        // Disable the whole fruit
        fruitCollider.enabled = false;
        //whole.SetActive(false);
        //Destroy(this.gameObject);
        // Enable the sliced fruit
        //sliced.SetActive(true);
        this.gameObject.SetActive(false);

        // Rotate based on the slice angle
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        //sliced.transform.rotation = Quaternion.Euler(0f, 0f, angle);

        //Rigidbody[] slices = sliced.GetComponentsInChildren<Rigidbody>();

        // Add a force to each slice based on the blade direction
        /*foreach (Rigidbody slice in slices)
        {
            slice.velocity = fruitRigidbody.velocity;
            slice.AddForceAtPosition(direction * force, position, ForceMode.Impulse);
        }*/
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Blade blade = other.GetComponent<Blade>();
            Slice(blade.direction, blade.transform.position, blade.sliceForce);
        }
    }

}
