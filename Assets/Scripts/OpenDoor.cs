using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    // Publiczny GameObject do zniszczenia
    public GameObject objectToDestroy;
    public GameObject hudToDestroy;
    
    private AudioSource audioSource;
    public AudioClip openDoor;

    // Flaga informująca, czy gracz jest w obszarze wyzwalacza
    private bool isInTrigger = false;
    
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Funkcja wywoływana, gdy inny obiekt wchodzi w obszar wyzwalacza
    private void OnTriggerEnter(Collider other)
    {
        // Sprawdzamy, czy obiekt, który wszedł w wyzwalacz, ma tag "Player"
        if (other.CompareTag("Player"))
        {
            isInTrigger = true;
        }
    }

    // Funkcja wywoływana, gdy inny obiekt opuszcza obszar wyzwalacza
    private void OnTriggerExit(Collider other)
    {
        // Gdy gracz opuści obszar wyzwalacza, resetujemy flagę
        if (other.CompareTag("Player"))
        {
            isInTrigger = false;
        }
    }

    // Funkcja wywoływana co klatkę
    private void Update()
    {
        // Sprawdzamy, czy gracz jest w wyzwalaczu i nacisnął klawisz "E"
        if (isInTrigger && Input.GetKeyDown(KeyCode.E))
        {
            // Zniszczenie obiektu
            if (objectToDestroy != null)
            {
                Destroy(objectToDestroy);
                Destroy(hudToDestroy);
                if (openDoor != null && audioSource != null)
                {
                    audioSource.PlayOneShot(openDoor);
                }
            }
        }
    }
}