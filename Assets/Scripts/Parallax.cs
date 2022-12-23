using UnityEngine;

public class Parallax : MonoBehaviour
{
    // private GameManager gameManager;

    private MeshRenderer meshRenderer;
    public float animationSpeed = 1f;

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    private void Update()
    {
        // if (!gameManager.isDead)
        // {
            meshRenderer. material.mainTextureOffset += new Vector2(animationSpeed * Time.deltaTime, 0);
        // }
    }
}
