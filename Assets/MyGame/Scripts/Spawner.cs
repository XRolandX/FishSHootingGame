using UnityEngine;

public class Spawner : MonoBehaviour
{
    
    [SerializeField] private GameObject _enemy;


    
    void Start()
    {
        Instantiate(_enemy, transform.position, Quaternion.identity);
    }

}
