using UnityEngine;

public class CameraOcclusion : MonoBehaviour
{
    public Transform playerTransform; // Referencja do transformu postaci
    public LayerMask obstacleMask; // Maski warstw, które będą traktowane jako przeszkody
    public Material transparentMaterial; // Materiał, który zostanie nałożony na przeszkody
    public float detectionRadius = 0.5f; // Promień sfery do wykrywania przeszkód

    private Material[] originalMaterials; // Tablica do przechowywania oryginalnych materiałów przeszkód
    private Renderer[] currentObstacleRenderers; // Renderery aktualnych przeszkód

    void Update()
    {
        HandleOcclusion();
    }

    void HandleOcclusion()
    {
        // Obliczenie kierunku między kamerą a postacią
        Vector3 directionToPlayer = playerTransform.position - transform.position;
        float distanceToPlayer = directionToPlayer.magnitude;

        // Wykonanie SphereCastAll, aby wykryć wszystkie przeszkody na drodze
        RaycastHit[] hits = Physics.SphereCastAll(transform.position, detectionRadius, directionToPlayer, distanceToPlayer, obstacleMask);

        // Resetowanie poprzednich przeszkód
        RestoreOriginalMaterials();

        // Jeśli trafiamy na przeszkody, zamieniamy ich materiały na przezroczyste
        if (hits.Length > 0)
        {
            currentObstacleRenderers = new Renderer[hits.Length];
            originalMaterials = new Material[hits.Length];

            for (int i = 0; i < hits.Length; i++)
            {
                Renderer obstacleRenderer = hits[i].collider.GetComponent<Renderer>();
                if (obstacleRenderer != null)
                {
                    currentObstacleRenderers[i] = obstacleRenderer;

                    // Zapisz oryginalne materiały przeszkody
                    originalMaterials[i] = obstacleRenderer.material;

                    // Zmień materiał przeszkody na przezroczysty
                    Material[] newMaterials = new Material[obstacleRenderer.materials.Length];
                    for (int j = 0; j < newMaterials.Length; j++)
                    {
                        newMaterials[j] = transparentMaterial;
                    }
                    obstacleRenderer.materials = newMaterials;
                }
            }
        }
    }

    void RestoreOriginalMaterials()
    {
        if (currentObstacleRenderers != null)
        {
            for (int i = 0; i < currentObstacleRenderers.Length; i++)
            {
                if (currentObstacleRenderers[i] != null)
                {
                    currentObstacleRenderers[i].materials = new Material[] { originalMaterials[i] };
                }
            }

            // Czyszczenie danych
            currentObstacleRenderers = null;
            originalMaterials = null;
        }
    }
}
