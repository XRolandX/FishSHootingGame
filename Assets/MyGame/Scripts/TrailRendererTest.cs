using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailRendererTest : MonoBehaviour
{

    [SerializeField] private GameObject _trailRendererObj;

    private Vector3 _clickPosition;

    public void TrailController()
    {
        _trailRendererObj.SetActive(false);
        Vector3 temp = Input.mousePosition;
        _clickPosition = Camera.main.ScreenToWorldPoint(temp);
        _clickPosition.z = 0;

        _trailRendererObj.SetActive(true);
        _trailRendererObj.transform.position = _clickPosition;
    }

    public void TrailCounter()
    {
        _trailRendererObj.SetActive(false);
        Touch touch = Input.GetTouch(0);
        _clickPosition = Camera.main.ScreenToWorldPoint(touch.position);
        _clickPosition.z = 0;

        _trailRendererObj.SetActive(true);
        _trailRendererObj.transform.position = _clickPosition;
    }

    void FixedUpdate()
    {
        if (Input.GetMouseButton(0))
        {
            TrailController();
        }



        if (Input.touchCount > 0)
        {
            TrailCounter();
        }
    }
}
