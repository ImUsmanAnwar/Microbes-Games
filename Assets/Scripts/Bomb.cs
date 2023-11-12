using UnityEngine;

public class Bomb : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GetComponent<Collider>().enabled = false;
            Destroy(this.gameObject);
            FindObjectOfType<GameManager>().Explode();
        }
    }

}
