using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private GameObject _hero;
    private void LateUpdate()
    {
        
        Vector3 temp = transform.position;

        temp.x = _hero.transform.position.x;
        temp.y = _hero.transform.position.y;

        transform.position = temp;
        //transform.position = new Vector3(_hero.transform.position.x, _hero.transform.position.y);   --Player dissapears--
        
       
    }
}
