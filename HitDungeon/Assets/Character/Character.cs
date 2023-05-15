using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    // 첫 위치
    public Vector3 FirstMousePosition;
    // 마우스를 땐 위치
    Vector2 SecondMousePosition;
    //마우스 현재 위치
    [SerializeField] Vector2 CurrentPoint;
    //마우스가 오브젝트 안에 들어왔는가?
    bool isObject = false;
    // 날리기 약화
    [SerializeField] [Range(1, 100)] float min;
    //게이지바 스프라이트 렌더러
    SpriteRenderer Shootgage;
    //게이지바 오브젝트
    GameObject GageBar;
    public Vector3 point;
    Rigidbody2D rd;
    CircleCollider2D CharacterObject;
    float Raduse;
    float Shootx;
    float Shooty;
    void Start()
    {
        Shootgage = GameObject.FindGameObjectWithTag("ShootGage").GetComponent<SpriteRenderer>();//게이지바의 스프라이트 가져오기
        Shootgage.color = new Color(Shootgage.color.r, Shootgage.color.g, Shootgage.color.b, 0);//게이지바 숨기기
        //게이지바 가져오기
        GageBar = GameObject.FindGameObjectWithTag("ShootGagePoint");

        rd = GetComponent<Rigidbody2D>();
        CharacterObject = GetComponent<CircleCollider2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        CurrentPoint = Camera.main.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
        Raduse = CharacterObject.radius;

        point = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z));//마우스 위치 받아오기 
        GageBar.transform.rotation =  Quaternion.Euler(0, 0, Quaternion.FromToRotation(Vector3.up, FirstMousePosition - point).eulerAngles.z);
        Debug.Log(Vector2.SignedAngle(FirstMousePosition, point));  
        if(Input.GetMouseButtonDown(0))
        {
            Shootgage.color = new Color(Shootgage.color.r, Shootgage.color.g, Shootgage.color.b, 1);//마우스 클릭시 게이지바 생성
            FirstMousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z));
            Debug.Log("발사준비" + FirstMousePosition.x + " " + FirstMousePosition.y);
        }

        if(Input.GetMouseButtonUp(0))
        {
            Shootgage.color = new Color(Shootgage.color.r, Shootgage.color.g, Shootgage.color.b, 0);//마우스 클릭 해제시 게이지바 숨김
            Shoot();
        }
    }


    
    void Shoot() 
    {
        SecondMousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z));
        Debug.Log("발사 각도" + Vector2.SignedAngle(FirstMousePosition, SecondMousePosition) + " " + Vector2.SignedAngle(FirstMousePosition, point));
        Shootx = FirstMousePosition.x / min - point.x / min;
        Shooty = FirstMousePosition.y / min - point.y / min;
        Debug.Log("발사!");
        rd.AddRelativeForce(new Vector2(Shootx, Shooty), ForceMode2D.Impulse);  
    }
}
