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
    [SerializeField] [Range(1, 100)] float mul;
    //게이지바 스프라이트 렌더러
    SpriteRenderer Shootgage;
    GameObject Shootgage_;
    //게이지바 오브젝트
    GameObject GageBar;
    public Vector3 point;
    Rigidbody2D rd;
    CircleCollider2D CharacterObject;
    float Raduse;
    float Shootx;
    float Shooty;
    public float distance = 0;
    public float upmin = 0;
    public float MaxLong;
    public float MaxShootPower;
    void Start()
    {
        Shootgage = GameObject.FindGameObjectWithTag("ShootGage").GetComponent<SpriteRenderer>();//게이지바의 스프라이트 가져오기
        Shootgage.color = new Color(Shootgage.color.r, Shootgage.color.g, Shootgage.color.b, 0);//게이지바 숨기기
        //게이지바 가져오기
        GageBar = GameObject.FindGameObjectWithTag("ShootGagePoint");
        Shootgage_ = GameObject.FindGameObjectWithTag("ShootGage");
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
        
        if (Vector2.Distance(point, FirstMousePosition) /  upmin + distance <= MaxLong)
        {
            Shootgage_.transform.localScale = new Vector3(Shootgage_.transform.localScale.x, Vector2.Distance(point, FirstMousePosition) /  upmin + distance, 0);
        }
        
        else
        {
            Shootgage_.transform.localScale = new Vector3(Shootgage_.transform.localScale.x, MaxLong, 0);
        }
        Debug.Log(Vector2.SignedAngle(FirstMousePosition, point));  
        if(Input.GetMouseButtonDown(0))
        {
            FirstMousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z));
            Shootgage_.transform.localScale = new Vector3(Shootgage_.transform.localScale.x, Vector2.Distance(point, FirstMousePosition) /  upmin + distance, 0);
            Shootgage.color = new Color(Shootgage.color.r, Shootgage.color.g, Shootgage.color.b, 1);//마우스 클릭시 게이지바 생성
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
        Shootx = FirstMousePosition.x - point.x;
        Shooty = FirstMousePosition.y - point.y;
        Debug.Log("발사!");
        Debug.Log(Shootx * mul + "바뀌지 않음" + Shooty * mul);
        if(Shootx >= MaxShootPower && Shootx >= Shooty)
        {
            float minshoot = Shootx / MaxShootPower;
            minshoot = Mathf.Abs(minshoot);
            Shootx /= minshoot;
            Shooty /= minshoot;
        }
        else if(Shooty >= MaxShootPower)
        {
            float minshoot = Shootx / MaxShootPower;
            minshoot = Mathf.Abs(minshoot);
            Shootx /= minshoot;
            Shooty /= minshoot;
        }
        Debug.Log(Shootx * mul + "바뀌" + Shooty * mul);
        rd.AddRelativeForce(new Vector2(Shootx * mul, Shooty * mul), ForceMode2D.Impulse);  
    }
}
