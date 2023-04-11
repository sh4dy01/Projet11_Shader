using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Material _outlineMaterial;
    public Material OutlineMaterial => _outlineMaterial;
    
    public static GameManager Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
