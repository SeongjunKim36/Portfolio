using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PadController : MonoBehaviour {
    //동적으로 생성할 라인랜더리 컴포넌트를 저장할 변수
    private LineRenderer lineRenderer;
    private Transform tr;

    //레이저의 거리
    public float range = 1000.0f;

    public Color defaultColor = Color.white;
    private Material mt;

    //포인터 프리팹
    private GameObject pointerPrefab;
    //동적으로 생성해서 라인렌더러 끝에 위치시킬 객체
    private GameObject pointer;
    //Raycast 충돌한 지점의 정보를 반환할 구조체
    private RaycastHit hit;

    public static bool isclicked = false;

    private GameObject prevButton;
    private GameObject currButton;
    private PadController caster;
    private bool isGrab = false;
    private GameObject Box;
    private Quaternion prevHorizontalRotation;
    private Quaternion currHorizontalRotation;
    private Quaternion prevVerticalRotation;
    private Quaternion currVerticalRotation;
    private bool isFinishedRotate = true;
    private bool isHorizontalRotateStart = false;
    private bool isVerticalRotateStart = false;
    private bool isDragStart = false;
    private Vector3 rightContollerPostion;

    private Vector3 prevPostion;
    private Vector3 currPosition;
    private Vector3 deltaPosition;
    private Vector3 TargetPositon;

    public static GameObject Tetris;
    private bool isMoveStart = false;
    public static GameObject initializer; 

    private string[] matTagNumber = new string[8];
    private int tagNumber = 0;
    private string MatName;
    public Material isGrabedMat;

    

    // public static bool left;
    // private bool right;
    // private bool up;
    // private bool down;
    public static bool[] TopTetrisMoved = new bool[4];
    public static bool[] TopTetrisRotated = new bool[4];

    void Start () {
        tr = GetComponent<Transform> ();
        //프로젝트 뷰의 Resources 폴더에 있는 Line 에셋을 로드
        mt = Resources.Load<Material> ("Line");
        pointerPrefab = Resources.Load<GameObject> ("Pointer");
        Tetris = this.gameObject;
        prevHorizontalRotation = OVRInput.GetLocalControllerRotation (OVRInput.Controller.RTouch);
        currHorizontalRotation = OVRInput.GetLocalControllerRotation (OVRInput.Controller.RTouch);
        initializer = GameObject.FindGameObjectWithTag("INIT");
        CreateLine ();

        //isGrabedMat = Resources.Load("Materials/GrabedMat",typeof(Material)) as Material;
    }

    void Update () {
        

        if (Physics.Raycast (tr.position, tr.forward, out hit, range)) {

            if (OVRInput.GetDown (OVRInput.Button.SecondaryHandTrigger) ||
                OVRInput.GetDown (OVRInput.Button.SecondaryIndexTrigger)) {
                Tetris = hit.collider.gameObject;
                Debug.Log("!!!!!!!!!!!!!!!!!!!");
                

                foreach(Transform cube in Tetris.transform)
                {
                    //cube.GetComponent<MeshRenderer>().material = null;
                    cube.GetComponent<MeshRenderer>().material = isGrabedMat;
                   

                    
                }
                
            }

            else if (OVRInput.GetUp (OVRInput.Button.SecondaryHandTrigger) ||
                OVRInput.GetUp (OVRInput.Button.SecondaryIndexTrigger)) {
                    
                    ChangeToOriginalMat();
                    Tetris = initializer;
                    isHorizontalRotateStart = false;
                    isVerticalRotateStart = false;
                    isFinishedRotate = true;
                    isMoveStart = false;
                    Debug.Log("@@@@@@@@@@@@@@@@@@@@@");

                    

            }

        }

        
        

        if (isFinishedRotate == true && Tetris.layer == 10 && OVRInput.Get (OVRInput.Button.SecondaryHandTrigger)) {

            if (!isHorizontalRotateStart) {
                prevHorizontalRotation = OVRInput.GetLocalControllerRotation (OVRInput.Controller.RTouch);
                isHorizontalRotateStart = true;
            }
            isGrab = true;

            currHorizontalRotation = OVRInput.GetLocalControllerRotation (OVRInput.Controller.RTouch);

            Vector3 vecPrev = prevHorizontalRotation.eulerAngles;
            Vector3 vecCurr = currHorizontalRotation.eulerAngles;
            Vector3 deltaVec = vecPrev - vecCurr;

            

            
            // 테트리스 회전

            if (deltaVec.y > 10f && isHorizontalRotateStart ) // 상단 테트리스 오른쪽 회전
            {
                isFinishedRotate = false;
                //iTween.RotateAdd (Tetris, iTween.Hash ("y", 90, "time", 0.15f, "space", Space.World, "easetype", iTween.EaseType.easeOutBounce, "oncomplete", "FinishedRotate", "oncompletetarget", this.gameObject));
                Tetris.gameObject.transform.Rotate(new Vector3(0,90,0),Space.World);
                FinishedRotate();
                prevHorizontalRotation = currHorizontalRotation;
                vecPrev = vecCurr;
                isHorizontalRotateStart = false;
                TopTetrisRotated[0] = true;
                

            } else if (deltaVec.y < -10f && isHorizontalRotateStart) // 상단 테트리스 좌측 회전
            {
                isFinishedRotate = false;
                //iTween.RotateAdd (Tetris, iTween.Hash ("y", -90, "time", 0.15f, "space", Space.World, "easetype", iTween.EaseType.easeOutBounce, "oncomplete", "FinishedRotate", "oncompletetarget", this.gameObject));
                Tetris.gameObject.transform.Rotate(new Vector3(0,-90,0),Space.World);
                FinishedRotate();
                prevHorizontalRotation = currHorizontalRotation;
                vecPrev = vecCurr;
                isHorizontalRotateStart = false;
                TopTetrisRotated[1] = true;
                
            }
            else if (deltaVec.x > 10f && isHorizontalRotateStart  ) // 상단 테트리스 좌측 회전
            {
                isFinishedRotate = false;
                //iTween.RotateAdd (Tetris, iTween.Hash ("x", 90, "time", 0.15f, "space", Space.World, "easetype", iTween.EaseType.easeOutBounce, "oncomplete", "FinishedRotate", "oncompletetarget", this.gameObject));
                Tetris.gameObject.transform.Rotate(new Vector3(90,0,0),Space.World);
                FinishedRotate();
                prevHorizontalRotation = currHorizontalRotation;
                vecPrev = vecCurr;
                isHorizontalRotateStart = false;
                TopTetrisRotated[2] = true;
                
            }
            else if (deltaVec.x < -10f && isHorizontalRotateStart ) // 상단 테트리스 좌측 회전
            {
                isFinishedRotate = false;
                //iTween.RotateAdd (Tetris, iTween.Hash ("x", -90, "time", 0.15f, "space", Space.World, "easetype", iTween.EaseType.easeOutBounce, "oncomplete", "FinishedRotate", "oncompletetarget", this.gameObject));
                Tetris.gameObject.transform.Rotate(new Vector3(-90,0,0),Space.World);
                FinishedRotate();
                prevHorizontalRotation = currHorizontalRotation;
                vecPrev = vecCurr;
                isHorizontalRotateStart = false;
                TopTetrisRotated[3] = true;
                
            }
            

        } 
        
        // else if (isMoveStart==false && isFinishedRotate == true && Tetris.layer == 10 && OVRInput.Get (OVRInput.Button.SecondaryIndexTrigger)) {
            
        //     if (!isVerticalRotateStart) {
        //         prevVerticalRotation = OVRInput.GetLocalControllerRotation (OVRInput.Controller.RTouch);
        //         isVerticalRotateStart = true;
        //     }
        //     isGrab = true;

        //     currVerticalRotation = OVRInput.GetLocalControllerRotation (OVRInput.Controller.RTouch);

        //     Vector3 vecPrev = prevVerticalRotation.eulerAngles;
        //     Vector3 vecCurr = currVerticalRotation.eulerAngles;
        //     Vector3 deltaVec = vecPrev - vecCurr;

            

        //     //테트리스 회전

        //     if (deltaVec.x > 1f && isVerticalRotateStart) // 상단 테트리스 회전
        //     {
        //         isFinishedRotate = false;
        //         iTween.RotateAdd (Tetris, iTween.Hash ("x", 90, "time", 0.15f, "space", Space.World, "easetype", iTween.EaseType.easeOutBounce, "oncomplete", "FinishedRotate", "oncompletetarget", this.gameObject));

        //         prevVerticalRotation = currVerticalRotation;
        //         vecPrev = vecCurr;
        //         isVerticalRotateStart = false;
                
        //     } else if (deltaVec.x < -1f && isVerticalRotateStart) // 상단 테트리스 회전
        //     {
        //         isFinishedRotate = false;
        //         iTween.RotateAdd (Tetris, iTween.Hash ("x", -90, "time", 0.15f, "space", Space.World, "easetype", iTween.EaseType.easeOutBounce, "oncomplete", "FinishedRotate", "oncompletetarget", this.gameObject));

        //         prevVerticalRotation = currVerticalRotation;
        //         vecPrev = vecCurr;
        //         isVerticalRotateStart = false;
        //     }
        // } 
        
        else if (Tetris.layer == 10 && OVRInput.Get (OVRInput.Button.SecondaryIndexTrigger))
                 {

            if (!isVerticalRotateStart) {
                prevVerticalRotation = OVRInput.GetLocalControllerRotation (OVRInput.Controller.RTouch);
                isVerticalRotateStart = true;
                isMoveStart = true;
            }
            
            isGrab = true;
            isFinishedRotate = false;

            currVerticalRotation = OVRInput.GetLocalControllerRotation (OVRInput.Controller.RTouch);

            Vector3 vecPrev = prevVerticalRotation.eulerAngles;
            Vector3 vecCurr = currVerticalRotation.eulerAngles;
            Vector3 deltaVec = vecPrev - vecCurr;

            

            // 테트리스 이동

            if (deltaVec.y > 3f && isVerticalRotateStart) 
            {
                
                //iTween.MoveAdd (Tetris, iTween.Hash ("amount", new Vector3 (-1, 0, 0), "space", Space.World, "speed", 10.0f, "easetype", iTween.EaseType.easeOutBounce));
                Tetris.transform.position += new Vector3(-1,0,0);
                prevVerticalRotation = currVerticalRotation;
                vecPrev = vecCurr;
                isVerticalRotateStart = false;
                isMoveStart = false;
                isFinishedRotate = true;
                TopTetrisMoved[0] = true;
                

            } else if (deltaVec.y < -3f && isVerticalRotateStart) 
            {
                
                //iTween.MoveAdd (Tetris, iTween.Hash ("amount", new Vector3 (1, 0, 0), "space", Space.World, "speed", 10.0f, "easetype", iTween.EaseType.easeOutBounce));
                Tetris.transform.position += new Vector3(1,0,0);
                prevVerticalRotation = currVerticalRotation;
                vecPrev = vecCurr;
                isVerticalRotateStart = false;
                isMoveStart = false;
                isFinishedRotate = true;
                TopTetrisMoved[1] = true;
            }
            else if (deltaVec.x > 3f && isVerticalRotateStart) 
            {
                
                //Tween.MoveAdd (Tetris, iTween.Hash ("amount", new Vector3 (0, 0, -1), "space", Space.World, "speed", 10.0f, "easetype", iTween.EaseType.easeOutBounce));
                Tetris.transform.position += new Vector3(0,0,-1);
                prevVerticalRotation = currVerticalRotation;
                vecPrev = vecCurr;
                isVerticalRotateStart = false;
                isMoveStart = false;
                isFinishedRotate = true;
                TopTetrisMoved[2] = true;
            }
            else if (deltaVec.x < -3f && isVerticalRotateStart) 
            {
                
                //iTween.MoveAdd (Tetris, iTween.Hash ("amount", new Vector3 (0, 0, 1), "space", Space.World, "speed", 10.0f, "easetype", iTween.EaseType.easeOutBounce));
                Tetris.transform.position += new Vector3(0,0,1);
                prevVerticalRotation = currVerticalRotation;
                vecPrev = vecCurr;
                isVerticalRotateStart = false;
                isMoveStart = false;
                isFinishedRotate = true;
                TopTetrisMoved[3] = true;
            }
        }
        // else
        // {
        //         //Tetris = null;
        //         isHorizontalRotateStart = false;
        //         isVerticalRotateStart = false;
        //         isDragStart = false;
        //         isMoveStart = false;
        // }
        



        
        // 오른쪽 테트리스 컨트롤
        else if (isFinishedRotate == true && Tetris.layer == 11 && OVRInput.Get (OVRInput.Button.SecondaryHandTrigger)) {

            if (!isHorizontalRotateStart) {
                prevHorizontalRotation = OVRInput.GetLocalControllerRotation (OVRInput.Controller.RTouch);
                isHorizontalRotateStart = true;
            }
            isGrab = true;

            currHorizontalRotation = OVRInput.GetLocalControllerRotation (OVRInput.Controller.RTouch);

            Vector3 vecPrev = prevHorizontalRotation.eulerAngles;
            Vector3 vecCurr = currHorizontalRotation.eulerAngles;
            Vector3 deltaVec = vecPrev - vecCurr;

            
            // 테트리스 회전

            if (deltaVec.y > 4f && isHorizontalRotateStart ) // 상단 테트리스 오른쪽 회전
            {
                isFinishedRotate = false;
                //iTween.RotateAdd (Tetris, iTween.Hash ("y", 90, "time", 0.15f, "space", Space.World, "easetype", iTween.EaseType.easeOutBounce, "oncomplete", "FinishedRotate", "oncompletetarget", this.gameObject));
                Tetris.gameObject.transform.Rotate(new Vector3(0,90,0),Space.World);
                FinishedRotate();
                prevHorizontalRotation = currHorizontalRotation;
                vecPrev = vecCurr;
                isHorizontalRotateStart = false;
                isDragStart = false;                
                prevPostion = currPosition;

            } else if (deltaVec.y < -4f && isHorizontalRotateStart) // 상단 테트리스 좌측 회전
            {
                isFinishedRotate = false;
                //iTween.RotateAdd (Tetris, iTween.Hash ("y", -90, "time", 0.15f, "space", Space.World, "easetype", iTween.EaseType.easeOutBounce, "oncomplete", "FinishedRotate", "oncompletetarget", this.gameObject));
                Tetris.gameObject.transform.Rotate(new Vector3(0,-90,0),Space.World);
                FinishedRotate();
                prevHorizontalRotation = currHorizontalRotation;
                vecPrev = vecCurr;
                isHorizontalRotateStart = false;
                isDragStart = false;                
                prevPostion = currPosition;
            }
            else if (deltaVec.x > 4f && isHorizontalRotateStart  ) // 상단 테트리스 좌측 회전
            {
                isFinishedRotate = false;
                //iTween.RotateAdd (Tetris, iTween.Hash ("z", -90, "time", 0.15f, "space", Space.World, "easetype", iTween.EaseType.easeOutBounce, "oncomplete", "FinishedRotate", "oncompletetarget", this.gameObject));
                Tetris.gameObject.transform.Rotate(new Vector3(0,0,-90),Space.World);
                FinishedRotate();
                prevHorizontalRotation = currHorizontalRotation;
                vecPrev = vecCurr;
                isHorizontalRotateStart = false;
                isDragStart = false;                
                prevPostion = currPosition;
            }
            else if (deltaVec.x < -4f && isHorizontalRotateStart ) // 상단 테트리스 좌측 회전
            {
                isFinishedRotate = false;
                //iTween.RotateAdd (Tetris, iTween.Hash ("z", 90, "time", 0.15f, "space", Space.World, "easetype", iTween.EaseType.easeOutBounce, "oncomplete", "FinishedRotate", "oncompletetarget", this.gameObject));
                Tetris.gameObject.transform.Rotate(new Vector3(0,0,90),Space.World);
                FinishedRotate();
                prevHorizontalRotation = currHorizontalRotation;
                vecPrev = vecCurr;
                isHorizontalRotateStart = false;
                isDragStart = false;                
                prevPostion = currPosition;
            }
            

        } 
        
    //     // else if (isMoveStart==false && isFinishedRotate == true && Tetris.layer == 10 && OVRInput.Get (OVRInput.Button.SecondaryIndexTrigger)) {
            
    //     //     if (!isVerticalRotateStart) {
    //     //         prevVerticalRotation = OVRInput.GetLocalControllerRotation (OVRInput.Controller.RTouch);
    //     //         isVerticalRotateStart = true;
    //     //     }
    //     //     isGrab = true;

    //     //     currVerticalRotation = OVRInput.GetLocalControllerRotation (OVRInput.Controller.RTouch);

    //     //     Vector3 vecPrev = prevVerticalRotation.eulerAngles;
    //     //     Vector3 vecCurr = currVerticalRotation.eulerAngles;
    //     //     Vector3 deltaVec = vecPrev - vecCurr;

            

    //     //     //테트리스 회전

    //     //     if (deltaVec.x > 1f && isVerticalRotateStart) // 상단 테트리스 회전
    //     //     {
    //     //         isFinishedRotate = false;
    //     //         iTween.RotateAdd (Tetris, iTween.Hash ("x", 90, "time", 0.15f, "space", Space.World, "easetype", iTween.EaseType.easeOutBounce, "oncomplete", "FinishedRotate", "oncompletetarget", this.gameObject));

    //     //         prevVerticalRotation = currVerticalRotation;
    //     //         vecPrev = vecCurr;
    //     //         isVerticalRotateStart = false;
                
    //     //     } else if (deltaVec.x < -1f && isVerticalRotateStart) // 상단 테트리스 회전
    //     //     {
    //     //         isFinishedRotate = false;
    //     //         iTween.RotateAdd (Tetris, iTween.Hash ("x", -90, "time", 0.15f, "space", Space.World, "easetype", iTween.EaseType.easeOutBounce, "oncomplete", "FinishedRotate", "oncompletetarget", this.gameObject));

    //     //         prevVerticalRotation = currVerticalRotation;
    //     //         vecPrev = vecCurr;
    //     //         isVerticalRotateStart = false;
    //     //     }
    //     // } 
        
        else if (Tetris.layer == 11 && OVRInput.Get (OVRInput.Button.SecondaryIndexTrigger) 
                ) {

            if (!isHorizontalRotateStart) {
                prevHorizontalRotation = OVRInput.GetLocalControllerRotation (OVRInput.Controller.RTouch);
                isHorizontalRotateStart = true;
                isMoveStart = true;
            }
            
            isGrab = true;
            isFinishedRotate = false;

            currHorizontalRotation = OVRInput.GetLocalControllerRotation (OVRInput.Controller.RTouch);

            Vector3 vecPrev = prevHorizontalRotation.eulerAngles;
            Vector3 vecCurr = currHorizontalRotation.eulerAngles;
            Vector3 deltaVec = vecPrev - vecCurr;

            

            // 테트리스 이동

            if (deltaVec.y > 1f && isHorizontalRotateStart) 
            {
                
                iTween.MoveAdd (Tetris, iTween.Hash ("amount", new Vector3 (0, 0, 1), "space", Space.World, "speed", 10.0f, "easetype", iTween.EaseType.easeOutBounce));
                prevHorizontalRotation = currHorizontalRotation;
                vecPrev = vecCurr;
                isHorizontalRotateStart = false;
                isMoveStart = false;
                isFinishedRotate = true;

            } else if (deltaVec.y < -1f && isHorizontalRotateStart) 
            {
                
                iTween.MoveAdd (Tetris, iTween.Hash ("amount", new Vector3 (0, 0, -1), "space", Space.World, "speed", 10.0f, "easetype", iTween.EaseType.easeOutBounce));

                prevHorizontalRotation = currHorizontalRotation;
                vecPrev = vecCurr;
                isHorizontalRotateStart = false;
                isMoveStart = false;
                isFinishedRotate = true;
            }
            else if (deltaVec.x > 1f && isHorizontalRotateStart) 
            {
                
                iTween.MoveAdd (Tetris, iTween.Hash ("amount", new Vector3 (0, 1, 0), "space", Space.World, "speed", 10.0f, "easetype", iTween.EaseType.easeOutBounce));

                prevHorizontalRotation = currHorizontalRotation;
                vecPrev = vecCurr;
                isHorizontalRotateStart = false;
                isMoveStart = false;
                isFinishedRotate = true;
            }
            else if (deltaVec.x < -1f && isHorizontalRotateStart) 
            {
                
                iTween.MoveAdd (Tetris, iTween.Hash ("amount", new Vector3 (0, -1, 0), "space", Space.World, "speed", 10.0f, "easetype", iTween.EaseType.easeOutBounce));

                prevHorizontalRotation = currHorizontalRotation;
                vecPrev = vecCurr;
                isHorizontalRotateStart = false;
                isMoveStart = false;
                isFinishedRotate = true;
            }
        }
        
        
        

        else if (isFinishedRotate == true && Tetris.layer == 12 && OVRInput.Get (OVRInput.Button.SecondaryHandTrigger)) {

            if (!isHorizontalRotateStart) {
                prevHorizontalRotation = OVRInput.GetLocalControllerRotation (OVRInput.Controller.RTouch);
                isHorizontalRotateStart = true;
            }
            isGrab = true;

            currHorizontalRotation = OVRInput.GetLocalControllerRotation (OVRInput.Controller.RTouch);

            Vector3 vecPrev = prevHorizontalRotation.eulerAngles;
            Vector3 vecCurr = currHorizontalRotation.eulerAngles;
            Vector3 deltaVec = vecPrev - vecCurr;

            
            // 테트리스 회전

            if (deltaVec.y > 4f && isHorizontalRotateStart ) // 상단 테트리스 오른쪽 회전
            {
                isFinishedRotate = false;
                iTween.RotateAdd (Tetris, iTween.Hash ("y", 90, "time", 0.15f, "space", Space.World, "easetype", iTween.EaseType.easeOutBounce, "oncomplete", "FinishedRotate", "oncompletetarget", this.gameObject));
                prevHorizontalRotation = currHorizontalRotation;
                vecPrev = vecCurr;
                isHorizontalRotateStart = false;
                isDragStart = false;                
                prevPostion = currPosition;

            } else if (deltaVec.y < -4f && isHorizontalRotateStart) // 상단 테트리스 좌측 회전
            {
                isFinishedRotate = false;
                iTween.RotateAdd (Tetris, iTween.Hash ("y", -90, "time", 0.15f, "space", Space.World, "easetype", iTween.EaseType.easeOutBounce, "oncomplete", "FinishedRotate", "oncompletetarget", this.gameObject));

                prevHorizontalRotation = currHorizontalRotation;
                vecPrev = vecCurr;
                isHorizontalRotateStart = false;
                isDragStart = false;                
                prevPostion = currPosition;
            }
            else if (deltaVec.x > 4f && isHorizontalRotateStart  ) // 상단 테트리스 좌측 회전
            {
                isFinishedRotate = false;
                iTween.RotateAdd (Tetris, iTween.Hash ("z", 90, "time", 0.15f, "space", Space.World, "easetype", iTween.EaseType.easeOutBounce, "oncomplete", "FinishedRotate", "oncompletetarget", this.gameObject));

                prevHorizontalRotation = currHorizontalRotation;
                vecPrev = vecCurr;
                isHorizontalRotateStart = false;
                isDragStart = false;                
                prevPostion = currPosition;
            }
            else if (deltaVec.x < -4f && isHorizontalRotateStart ) // 상단 테트리스 좌측 회전
            {
                isFinishedRotate = false;
                iTween.RotateAdd (Tetris, iTween.Hash ("z", -90, "time", 0.15f, "space", Space.World, "easetype", iTween.EaseType.easeOutBounce, "oncomplete", "FinishedRotate", "oncompletetarget", this.gameObject));

                prevHorizontalRotation = currHorizontalRotation;
                vecPrev = vecCurr;
                isHorizontalRotateStart = false;
                isDragStart = false;                
                prevPostion = currPosition;
            }
            

        } 
        
    //     // else if (isMoveStart==false && isFinishedRotate == true && Tetris.layer == 10 && OVRInput.Get (OVRInput.Button.SecondaryIndexTrigger)) {
            
    //     //     if (!isVerticalRotateStart) {
    //     //         prevVerticalRotation = OVRInput.GetLocalControllerRotation (OVRInput.Controller.RTouch);
    //     //         isVerticalRotateStart = true;
    //     //     }
    //     //     isGrab = true;

    //     //     currVerticalRotation = OVRInput.GetLocalControllerRotation (OVRInput.Controller.RTouch);

    //     //     Vector3 vecPrev = prevVerticalRotation.eulerAngles;
    //     //     Vector3 vecCurr = currVerticalRotation.eulerAngles;
    //     //     Vector3 deltaVec = vecPrev - vecCurr;

            

    //     //     //테트리스 회전

    //     //     if (deltaVec.x > 1f && isVerticalRotateStart) // 상단 테트리스 회전
    //     //     {
    //     //         isFinishedRotate = false;
    //     //         iTween.RotateAdd (Tetris, iTween.Hash ("x", 90, "time", 0.15f, "space", Space.World, "easetype", iTween.EaseType.easeOutBounce, "oncomplete", "FinishedRotate", "oncompletetarget", this.gameObject));

    //     //         prevVerticalRotation = currVerticalRotation;
    //     //         vecPrev = vecCurr;
    //     //         isVerticalRotateStart = false;
                
    //     //     } else if (deltaVec.x < -1f && isVerticalRotateStart) // 상단 테트리스 회전
    //     //     {
    //     //         isFinishedRotate = false;
    //     //         iTween.RotateAdd (Tetris, iTween.Hash ("x", -90, "time", 0.15f, "space", Space.World, "easetype", iTween.EaseType.easeOutBounce, "oncomplete", "FinishedRotate", "oncompletetarget", this.gameObject));

    //     //         prevVerticalRotation = currVerticalRotation;
    //     //         vecPrev = vecCurr;
    //     //         isVerticalRotateStart = false;
    //     //     }
    //     // } 
        
        else if (Tetris.layer == 12 && OVRInput.Get (OVRInput.Button.SecondaryIndexTrigger) 
                ) {

            if (!isHorizontalRotateStart) {
                prevHorizontalRotation = OVRInput.GetLocalControllerRotation (OVRInput.Controller.RTouch);
                isHorizontalRotateStart = true;
                isMoveStart = true;
            }
            
            isGrab = true;
            isFinishedRotate = false;

            currHorizontalRotation = OVRInput.GetLocalControllerRotation (OVRInput.Controller.RTouch);

            Vector3 vecPrev = prevHorizontalRotation.eulerAngles;
            Vector3 vecCurr = currHorizontalRotation.eulerAngles;
            Vector3 deltaVec = vecPrev - vecCurr;

            

            // 테트리스 회전

            if (deltaVec.y > 1f && isHorizontalRotateStart) 
            {
                
                iTween.MoveAdd (Tetris, iTween.Hash ("amount", new Vector3 (0, 0, -1), "space", Space.World, "speed", 10.0f, "easetype", iTween.EaseType.easeOutBounce));
                prevHorizontalRotation = currHorizontalRotation;
                vecPrev = vecCurr;
                isHorizontalRotateStart = false;
                isMoveStart = false;
                isFinishedRotate = true;

            } else if (deltaVec.y < -1f && isHorizontalRotateStart) 
            {
                
                iTween.MoveAdd (Tetris, iTween.Hash ("amount", new Vector3 (0, 0, 1), "space", Space.World, "speed", 10.0f, "easetype", iTween.EaseType.easeOutBounce));

                prevHorizontalRotation = currHorizontalRotation;
                vecPrev = vecCurr;
                isHorizontalRotateStart = false;
                isMoveStart = false;
                isFinishedRotate = true;
            }
            else if (deltaVec.x > 1f && isHorizontalRotateStart) 
            {
                
                iTween.MoveAdd (Tetris, iTween.Hash ("amount", new Vector3 (0, 1, 0), "space", Space.World, "speed", 10.0f, "easetype", iTween.EaseType.easeOutBounce));

                prevHorizontalRotation = currHorizontalRotation;
                vecPrev = vecCurr;
                isHorizontalRotateStart = false;
                isMoveStart = false;
                isFinishedRotate = true;
            }
            else if (deltaVec.x < -1f && isHorizontalRotateStart) 
            {
                
                iTween.MoveAdd (Tetris, iTween.Hash ("amount", new Vector3 (0, -1, 0), "space", Space.World, "speed", 10.0f, "easetype", iTween.EaseType.easeOutBounce));

                prevHorizontalRotation = currHorizontalRotation;
                vecPrev = vecCurr;
                isHorizontalRotateStart = false;
                isMoveStart = false;
                isFinishedRotate = true;
            }
        }
       

    // if (OVRInput.Get (OVRInput.Button.SecondaryThumbstickUp) && isGrab == true) {
    //     Debug.Log (hit.collider.gameObject.GetComponent<Transform> ().position);
    //     hit.collider.gameObject.GetComponent<Transform> ().position += new Vector3 (0, 1, 0);
    // }
    // if (OVRInput.Get (OVRInput.Button.SecondaryThumbstickDown) && isGrab == true) {
    //     hit.collider.gameObject.GetComponent<Transform> ().position -= new Vector3 (0, 1, 0);
    // }
    // if (OVRInput.Get (OVRInput.Button.SecondaryThumbstickLeft) && isGrab == true) {
    //     hit.collider.gameObject.GetComponent<Transform> ().position -= new Vector3 (0, 0, 1);
    // }
    // if (OVRInput.Get (OVRInput.Button.SecondaryThumbstickRight) && isGrab == true) {
    //     hit.collider.gameObject.GetComponent<Transform> ().position += new Vector3 (0, 0, 1);
    // }

    //Debug.Log(isMoveStart+"isMoveStart");
    //Debug.Log(isFinishedRotate+"isFinishedRotate");
    

}



//라인렌더러 생성
void CreateLine () {
    lineRenderer = this.gameObject.AddComponent<LineRenderer> ();
    lineRenderer.useWorldSpace = false;
    lineRenderer.widthMultiplier = 0.05f;
    lineRenderer.SetPosition (1, new Vector3 (0, 0, range));

    //머테리얼 생성 대입
    // Material mt = new Material(Shader.Find("Unlit/Color"));
    // mt.color = defaultColor;
    lineRenderer.sharedMaterial = mt;

    //포인터 생성
    pointer = Instantiate (pointerPrefab, transform.position + lineRenderer.GetPosition (1), Quaternion.identity, transform);

}

void GrabTetris () {
    //&& Quaternion.Angle(prevHorizontalRotation,currHorizontalRotation)>= 25f 
    if (Physics.Raycast (tr.position, tr.forward, out hit, range)) {
        if (hit.collider.gameObject.layer == 10 && OVRInput.GetDown (OVRInput.Button.SecondaryHandTrigger)) {
            if (OVRInput.Get (OVRInput.Button.PrimaryThumbstickUp)) {
                hit.collider.gameObject.GetComponent<Transform> ().position += new Vector3 (0, 0, 1);
            }
            if (OVRInput.Get (OVRInput.Button.PrimaryThumbstickDown)) {
                hit.collider.gameObject.GetComponent<Transform> ().position -= new Vector3 (0, 0, 1);
            }
            if (OVRInput.Get (OVRInput.Button.PrimaryThumbstickLeft)) {
                hit.collider.gameObject.GetComponent<Transform> ().position += new Vector3 (1, 0, 0);
            }
            if (OVRInput.Get (OVRInput.Button.PrimaryThumbstickRight)) {
                hit.collider.gameObject.GetComponent<Transform> ().position -= new Vector3 (1, 0, 0);
            }
        }
    }

}

void FinishedRotate () {
    //Debug.Log ("Finished Rotate");
    isFinishedRotate = true;
}
    int GetTetrisMatNumber(int tagNumber)
        {
            matTagNumber[0] = "FullI";
            matTagNumber[1] = "L";
            matTagNumber[2] = "O";
            matTagNumber[3] = "S";
            matTagNumber[4] = "ShortI";
            matTagNumber[5] = "T";
            matTagNumber[6] = "V";
            matTagNumber[7] = "HalfI";

            
            for(tagNumber = 0; tagNumber<8; tagNumber++)
            {
                if(Tetris.gameObject.tag == matTagNumber[tagNumber])
                {
                    //Debug.Log(tagNumber+"tagNumber!!@@!@");
                    return tagNumber;
                    
                }
            }
            

            return tagNumber = 0;        
        }

    void ChangeToOriginalMat()
    {
        foreach(Transform cube in Tetris.transform)
                {
                    //cube.GetComponent<MeshRenderer>().material = null;
                    cube.GetComponent<MeshRenderer>().material = (Material)Resources.Load(GetTetrisMat());
                    //Debug.Log("빠뀌니?");

                    
                }
        //Tetris.GetComponent<MeshRenderer>().material = (Material)Resources.Load(TetrisMat);//typeof(Material)) as Material;
    }
    string GetTetrisMat()
    {
        int tagNum;
        
        tagNum = GetTetrisMatNumber(tagNumber);
        string TetrisMat = "Materials/FullI-shape";
        switch(tagNum)
        {
            case 0:
                TetrisMat = "Materials/FullI-shape";
                break;
            case 1:
                TetrisMat = "Materials/L-shape";
                break;
            case 2:
                TetrisMat = "Materials/O-shape";
                break;
            case 3:
                TetrisMat = "Materials/S-shape";
                break;
            case 4:
                TetrisMat = "Materials/ShortI-shape";
                break;
            case 5:
                TetrisMat = "Materials/T-shape";
                break;
            case 6:
                TetrisMat = "Materials/V-shape";
                break;
            case 7:
                TetrisMat = "Materials/HalfI-shape";
                break;
        }
        return TetrisMat;
    }

    





}