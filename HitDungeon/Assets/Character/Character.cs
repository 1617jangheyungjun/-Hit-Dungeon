using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    // 첫 위치
    Vector2 FirstMousePosition;
    // 마우스를 땐 위치
    Vector2 SecondMousePosition;
    // 날리기 약화
    [SerializeField] [Range(1, 100)] float min;
    Rigidbody2D rd;

    float Shootx;
    float Shooty;
    void Start()
    {
        rd = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            FirstMousePosition = Input.mousePosition;
            Debug.Log("발사준비" + FirstMousePosition.x + " " + FirstMousePosition.y);
        }

        if(Input.GetMouseButtonUp(0))
        {
            Shoot();
        }
    }


    
    void Shoot() 
    {
        SecondMousePosition = Input.mousePosition;
        Debug.Log("발사 위치" + SecondMousePosition.x + " " + SecondMousePosition.y);
        Shootx = FirstMousePosition.x / min - SecondMousePosition.x / min;
        Shooty = FirstMousePosition.y / min - SecondMousePosition.y / min;
        Debug.Log("발사!");
        rd.AddRelativeForce(new Vector2(Shootx, Shooty), ForceMode2D.Impulse);  
    }
}
