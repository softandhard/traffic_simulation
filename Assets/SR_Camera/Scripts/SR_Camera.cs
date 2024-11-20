using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;

namespace SR
{
    [RequireComponent(typeof(Camera))]
    [AddComponentMenu("SR Camera")]
    public class SR_Camera : MonoBehaviour
    {
        public int lastTab = 0;
        private Camera m_camera;

        private bool useFixedUpdate = false;

        //use FixedUpdate() or LateUpdate()
        [Header("keyboard Movement Speed")] public float keyboardMovementSpeed = 10f;
        [Header("screen Edge Movement Speed")] public float screenEdgeMovementSpeed = 5f;

        [Header("keyboard Rotation Speed")] public float keyboardRotationSpeed = 3f;

        [Header("adaptive panning Speed")] public bool adaptivepanningSpeed = false;
        [Header("Speed Amend")] public float SpeedAmend = 1f;

        [Header("panning Speed")] public float panningSpeed = 100f;
        [Header("mouse Rotation Speed")] public float mouseRotationSpeed = 10f;
        [Header("Is Fixed X Angle")] public bool IsFixedXAngle = true;
        public float xAngle = 45f;

        [Header("Clamp X Angle")] public bool isClampXAngle = true;
        public float clampMinXAngle = 0;
        public float clampMaxXAngle = 90;
        [Header("height Dampening 0-50")] public float heightDampening = 5f;

        [Header("key boardZooming Sensitivity")]
        public float keyboardZoomingSensitivity = 10f;

        [Header("scrollWheel Zooming Sensitivity")]
        public float scrollWheelZoomingSensitivity = 20f;

        [Header("adaptive ScrollWheel Speed")] public bool adaptiveScrollWheelSpeed = true;
        [Header("adaptive ScrollWheel amend")] public float adaptiveScrollWheelamend = 1.0f;
        private float zoomPos = 0; //value in range (0, 1) used as t in Matf.Lerp

        [SerializeField] [Header("Check Tag")] public List<string> targetsTag;
        [Header("target Select")] public Transform targetSelect;

        private ParameterConfiguration parameter;

        [Header("target Offset")] public Vector3 targetOffset = Vector3.zero;
        [Header("is Foollow Target")] public bool isFoollowTarget = true;

        [Header("click/doubleClick Select Target  Default click")]
        public bool doubleClick = false;

        [Header("is Check UI")] public bool isCheckUI = true;

        
        private float targetDistance = 0;
        private float currentDistance = 0;
        public float ScrollwheelLerp = 5f;
        [Header("max Distance")] public float maxDistance = 100f;
        [Header("min Distance")] public float minDistance = 2f;

        [Header("F key focus Speed")] public float focusSpeed = 10;
        [Header("target Weight")] public float targetWeight = 5;

        private bool isFocusing = false;
        private Vector3 targetPos;
        private float distance;

        public bool isCanSetCamera = true;
        private Vector3 setPostion;
        private Vector3 setRotate;
        private bool isSetting = false;
        Vector3 targetPosCalc = Vector3.zero;

        float x;
        float y;
        Vector2 currentAngle ;
         Vector2 targetAngles;
        #region 事件Event

        public Action<GameObject> SelectTargetAction; //Select the callback for the object
        public Action ResetTargetAction; //Emitted when a target is lost or reset
        public Action<GameObject> FocusStartAction; //Focus on the beginning of the pullback
        public Action<GameObject> FocusEndAction; //Focus on the completed callback

        public Action ResetCameraTransfomCompleteAction; //Emitted when resetting camera position and rotation is complete

        #endregion

        //DefaultParamter
        private float RecordscrollWheelZoomingSensitivity;
        public float RecordmaxDistance = 100f;
        public float RecordminDistance = 2f;
        public float RecordtargetWeight = 5;
        public float RecordmouseRotationSpeed = 200f;

        #region 全局配置

        public Transform AimOriginPoint;
        [Header("Show Visual Areas")] public bool ShowVisualAreas = true;
        [Header("limit Map")] public bool limitMap = true;
        [Header("Scene center")] public Vector3 center = Vector3.zero;
        [Header("Check ground Mask")] public LayerMask groundMask = -1;
        [Header("aim Max Height")] public float aimMaxHeight = 100f;
        [Header("aim Min Height")] public float aimMinHeight = 0f;

        [Header("x limit of map")] public float aimlimitX = 100f;
        [Header("z limit of map")] public float aimlimitZ = 100f;

        [Header("Camera max Height")] public float maxHeight = 100f;
        [Header("Camera min Height")] public float minHeight = 1f;
        [Header("x limit of map")] public float limitX = 100f;
        [Header("z limit of map")] public float limitZ = 100f;
        [Header("Camera RotateLerp")] public bool CameraRotateLerp = false;

        private float LinerDistance;
        private float LinerAngle;
        [Header("move Lerp Speed")] public float moveLerpSpeed = 5.0f;
        [Header("rotate Lerp Speed")] public float rotateLerpSpeed = 5.0f;

        #endregion

        private bool isAligning = false;

        public bool FollowingTarget
        {
            get { return targetSelect != null; }
        }


        #region Input

        public IIput input;
        public bool useScreenEdgeInput = true;
        public float screenEdgeBorder = 100f;

        public bool useKeyboardInput = true;

        public bool usePanning = true;
        public KeyCode panningKey = KeyCode.Mouse2;

        public bool useKeyboardZooming = true;
        public KeyCode dropKey = KeyCode.E;
        public KeyCode riseKey = KeyCode.Q;

        public bool useScrollwheelZooming = true;
        public bool useKeyFocus = true;
        public KeyCode focusKey = KeyCode.F;

        [Header("Self Rotate")] public bool IsSelfRotate = false;
        public bool useKeyboardRotation = true;
        public KeyCode rotateRightKey = KeyCode.X;
        public KeyCode rotateLeftKey = KeyCode.Z;

        public bool useMouseRotation = true;
        public KeyCode mouseRotationKey = KeyCode.Mouse1;



        private Vector2 KeyboardInput
        {
            get
            {
                return useKeyboardInput
                    ? new Vector2(input.GetAxis(InputAxis.Horizontal), input.GetAxis(InputAxis.Vertical))
                    : Vector2.zero;
            }
        }

        private float ScrollWheel
        {
            get { return input.GetAxis(InputAxis.Z); }
        }
        private Vector2 MouseAxis
        {
            get
            {
                return new Vector2(input.GetAxis(InputAxis.X), input.GetAxis(InputAxis.Y));
            }
        }

        private int ZoomDirection
        {
            get
            {
                bool zoomIn = input.GetKey(dropKey);
                bool zoomOut = input.GetKey(riseKey);
                if (zoomIn && zoomOut)
                    return 0;
                else if (!zoomIn && zoomOut)
                    return 1;
                else if (zoomIn && !zoomOut)
                    return -1;
                else
                    return 0;
            }
        }

        private int RotationDirection
        {
            get
            {
                bool rotateRight = input.GetKey(rotateRightKey);
                bool rotateLeft = input.GetKey(rotateLeftKey);
                if (rotateLeft && rotateRight)
                    return 0;
                else if (rotateLeft && !rotateRight)
                    return -1;
                else if (!rotateLeft && rotateRight)
                    return 1;
                else
                    return 0;
            }
        }

        #endregion

        #region Unity_Methods

        void Awake()
        {
            if (EventSystem.current == null)
            {
                GameObject eventSystem = new GameObject("EventSystem");
                eventSystem.AddComponent<EventSystem>();
                eventSystem.AddComponent<StandaloneInputModule>();
            }
            
        }

        private void Start()
        {
            RecordscrollWheelZoomingSensitivity = scrollWheelZoomingSensitivity;
            RecordmaxDistance = maxDistance;
            RecordminDistance = minDistance;
            RecordtargetWeight = targetWeight;
            RecordmouseRotationSpeed = mouseRotationSpeed;
            //Here you can access the input data of the third-party platform
            input = new PCInput();
          //  input = new ThridPartInput(); 
            m_camera = GetComponent<Camera>();
            Vector3 angles = transform.eulerAngles;
            x = angles.y;
            y = angles.x;
            currentAngle =targetAngles= angles;
            GameObject go = new GameObject("AimPoint");
            AimOriginPoint = go.transform;
            CalcAimPosAndDistance();
        }

        void ResetParameters()
        {
            scrollWheelZoomingSensitivity = RecordscrollWheelZoomingSensitivity;
            maxDistance = RecordmaxDistance;
            minDistance = RecordminDistance;
            targetWeight = RecordtargetWeight;
            mouseRotationSpeed = RecordmouseRotationSpeed;
        }

        public void Update()
        {
            if (!doubleClick)
            {
                if (isCheckUI)
                {
                    if (input.IsTap() && !input.IsPointerOverUIObject())
                    {
                        Ray ray = m_camera.ScreenPointToRay(input.GetPositon());
                        RaycastHit hit;
                        if (Physics.Raycast(ray, out hit))
                        {
                            if (targetsTag.Contains(hit.transform.tag))
                                SetTarget(hit.transform);
                            else
                                ResetTarget();
                        }
                    }
                }
                else
                {
                    if (input.IsTap())
                    {
                        Ray ray = m_camera.ScreenPointToRay(input.GetPositon());
                        RaycastHit hit;
                        if (Physics.Raycast(ray, out hit))
                        {
                            if (targetsTag.Contains(hit.transform.tag))
                                SetTarget(hit.transform);
                            else
                                ResetTarget();
                        }
                    }
                }
            }
            else
            {
                if (isCheckUI)
                {
                    if (input.IsDoubleClick() && !input.IsPointerOverUIObject())
                    {
                        Ray ray = m_camera.ScreenPointToRay(input.GetPositon());
                        RaycastHit hit;
                        if (Physics.Raycast(ray, out hit))
                        {
                            if (targetsTag.Contains(hit.transform.tag))
                                SetTarget(hit.transform);
                            else
                            {
                                ResetTarget();
                            }
                        }
                    }
                }
                else
                {
                    if (input.IsDoubleClick())
                    {
                        Ray ray = m_camera.ScreenPointToRay(input.GetPositon());
                        RaycastHit hit;
                        if (Physics.Raycast(ray, out hit))
                        {
                            if (targetsTag.Contains(hit.transform.tag))
                                SetTarget(hit.transform);
                            else
                            {
                                ResetTarget();
                            }
                        }
                    }
                }
            }
        }

        private void LateUpdate()
        {
            //Debug.Log(targetAngles);
            if (!useFixedUpdate)
                CameraUpdate();
        }

        private void OnDrawGizmos()
        {
            if (ShowVisualAreas)
            {
                //Draw Gizmos
                Gizmos.color = Color.cyan;
                Gizmos.DrawRay(transform.position, Vector3.down * 100);
                Gizmos.color = Color.blue;
                Gizmos.DrawRay(transform.position, transform.forward * 100);
                Gizmos.color = Color.red;
                Gizmos.DrawRay(transform.position, transform.right * 10);

                Gizmos.color = Color.green;
                Gizmos.DrawRay(transform.position, transform.up * 10);

                Quaternion rot = Quaternion.AngleAxis(-transform.eulerAngles.x, transform.right);
                Vector3 dir = rot * transform.forward;
                Gizmos.color = Color.cyan;
                Gizmos.DrawRay(transform.position, dir * 100);
            }
        }

        private void FixedUpdate()
        {
             if (useFixedUpdate)
                CameraUpdate();
        }

        #endregion
        
        /// <summary>
        ///Update camera position and rotation
        /// </summary>
        private void CameraUpdate()
        {
            if (isCanSetCamera && isSetting)
            {
                UpdatePositonAndRotate();
                return;
            }

            //Focus Function Click to calculate the focus position
            if (input.GetKeyDown(focusKey) && targetSelect != null)
            {
                CalcFocusPositon();
            }

            //Follow the target
            if (isFoollowTarget && targetSelect != null)
            {
                targetPos = targetSelect.position+ targetOffset;
            }
            Move();
            Rotation();
            HeightCalculation();
            AimOriginPoint.position = Vector3.Lerp(AimOriginPoint.position, targetPos,
                Time.unscaledDeltaTime * moveLerpSpeed);
            currentDistance = Mathf.Lerp(currentDistance, targetDistance,
                Time.unscaledDeltaTime *ScrollwheelLerp);
            //  Debug.DrawLine(AimOriginPoint.position, position,Color.green);
            if (CameraRotateLerp)
            {
                currentAngle= Vector2.Lerp(currentAngle,targetAngles,Time.unscaledDeltaTime * rotateLerpSpeed);
                transform.rotation = Quaternion.Euler(currentAngle);
            }
            else
            {
                transform.rotation = Quaternion.Euler(this.targetAngles);
            }
            transform.position  =AimOriginPoint.position - transform.forward * currentDistance;
            
            if (isFocusing)
            {
                if (Mathf.Abs(currentDistance-targetDistance)<0.1f)
                {  
                    isFocusing = false;
                    if (FocusEndAction != null)
                    {
                        FocusEndAction.Invoke(targetSelect.gameObject);
                    }
                }
              
            }
            if (!isFocusing)
            {
                LimitPosition();
            }
        }

      
        
        /// <summary>
        /// Calculate the target point and distance
        /// </summary>
        void CalcAimPosAndDistance()
        {
            if (RayToGround(transform.forward, ref targetPosCalc))
            {
               targetPos = targetPosCalc;
            }
            else
            {
                Debug.LogWarning(
                    "You need to set the ground layer correctly and add Collider, otherwise you can't determine the target point!!  ");
                targetPos = transform.position + transform.forward * minDistance;
            }

            //Restrict the target point to the area
            if (limitMap)
            {
                targetPos = new Vector3(
                    Mathf.Clamp(targetPos.x, center.x - aimlimitX, center.x + aimlimitX),
                    Mathf.Clamp(targetPos.y, aimMinHeight, aimMaxHeight),
                    Mathf.Clamp(targetPos.z, center.z - aimlimitZ, center.z + aimlimitZ));
    
            }
            AimOriginPoint.position = targetPos;
            currentDistance= targetDistance = Vector3.Distance(transform.position, AimOriginPoint.position);
        }

        void UpdatePositonAndRotate()
        {
            transform.position = Vector3.Lerp(transform.position, setPostion, Time.unscaledDeltaTime * moveLerpSpeed);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(setRotate),
                Time.unscaledDeltaTime * rotateLerpSpeed);
            if (Vector3.Distance(transform.position, setPostion) <= 0.01f &&
                Quaternion.Angle(transform.rotation, Quaternion.Euler(setRotate)) <= 0.05f)
            {
                ResetCameraTransfomCompleteAction?.Invoke();
                transform.position = setPostion;
                transform.rotation = Quaternion.Euler(setRotate);
                isSetting = false;
                Vector3 angles = transform.eulerAngles;
                x = angles.y;
                y = angles.x;
                currentAngle =targetAngles= angles;
                ResetTarget();
                isFocusing = false;
                CalcAimPosAndDistance();
            }
        }

        /// <summary>
        /// Calc Focus 
        /// </summary>
        void CalcFocusPositon()
        {
            AimOriginPoint.position = targetPos = targetSelect.position+targetOffset;
            Bounds bounds = CalculateBounds(targetSelect.gameObject);
            if (bounds.extents == Vector3.zero)
            {
                bounds.extents = Vector3.one * 0.5f;
            }
            float radius = Mathf.Max(bounds.extents.y, bounds.extents.x, bounds.extents.z) * 2.0f;
            float fov = m_camera.fieldOfView * Mathf.Deg2Rad;
            distance = Mathf.Abs(radius / Mathf.Sin(fov / targetWeight));
            distance = Mathf.Clamp(distance, minDistance, maxDistance);
            targetDistance = distance;
            //Debug.Log("--距离:"+distance);
            // Quaternion rot = Quaternion.Euler(y, x, 0.0f);
            // transform.rotation = rot;
            // Vector3 disVector = new Vector3(0.0f, 0.0f, -distance);
            // var pos = rot * disVector + targetSelect.position+targetOffset;
            // Debug.Log("开始时计算出来的位置:"+pos+  " "+"x:"+x+"y:"+y);
            // setPostion = pos;
             isFocusing = true;
            if (FocusStartAction != null)
            {
                FocusStartAction.Invoke(targetSelect.gameObject);
            }
        }

        /// <summary>
        /// MoveCamera
        /// </summary>
        private void Move()
        {
            if (useKeyboardInput)
            {
                Vector3 desiredMove = new Vector3(KeyboardInput.x, 0, KeyboardInput.y);
                desiredMove *= keyboardMovementSpeed * Time.unscaledDeltaTime;
                desiredMove = Quaternion.Euler(new Vector3(0f, transform.eulerAngles.y, 0f)) * desiredMove;
                targetPos += desiredMove;
                if (limitMap)
                {
                    targetPos = new Vector3(
                        Mathf.Clamp(targetPos.x, center.x - aimlimitX, center.x + aimlimitX),
                        Mathf.Clamp(targetPos.y, aimMinHeight, aimMaxHeight),
                        Mathf.Clamp(targetPos.z, center.z - aimlimitZ, center.z + aimlimitZ));
                }

                if (KeyboardInput.x != 0 || KeyboardInput.y != 0)
                {
                    ResetTarget();
                }
            }

            if (useScreenEdgeInput)
            {
                if (!input.IsAnyKeyDown())
                {
                    Vector3 desiredMove = new Vector3();
                    Rect leftRect = new Rect(0, 0, screenEdgeBorder, Screen.height);
                    Rect rightRect = new Rect(Screen.width - screenEdgeBorder, 0, screenEdgeBorder, Screen.height);
                    Rect upRect = new Rect(0, Screen.height - screenEdgeBorder, Screen.width, screenEdgeBorder);
                    Rect downRect = new Rect(0, 0, Screen.width, screenEdgeBorder);
                    desiredMove.x = leftRect.Contains(input.GetPositon()) ? -1 :
                        rightRect.Contains(input.GetPositon()) ? 1 : 0;
                    desiredMove.z = upRect.Contains(input.GetPositon()) ? 1 :
                        downRect.Contains(input.GetPositon()) ? -1 : 0;
                    desiredMove *= screenEdgeMovementSpeed;
                    desiredMove *= Time.unscaledDeltaTime;
                    desiredMove = Quaternion.Euler(new Vector3(0f, transform.eulerAngles.y, 0f)) * desiredMove;
                    targetPos += desiredMove;
                    if (limitMap)
                    {
                        targetPos = new Vector3(
                            Mathf.Clamp(targetPos.x, center.x - aimlimitX, center.x + aimlimitX),
                            Mathf.Clamp(targetPos.y, aimMinHeight, aimMaxHeight),
                            Mathf.Clamp(targetPos.z, center.z - aimlimitZ, center.z + aimlimitZ));
                    }

                    if (desiredMove.x != 0 || desiredMove.z != 0)
                    {
                        ResetTarget();
                    }
                }
            }

            if (usePanning && input.GetKey(panningKey))
            {
                Vector3 desiredMove = new Vector3(-MouseAxis.x, 0, -MouseAxis.y);
                if (adaptivepanningSpeed)
                {
                    panningSpeed = currentDistance*SpeedAmend;//Speed correction
                    panningSpeed = Mathf.Clamp(panningSpeed, 2, maxDistance);
                }

                if (isCheckUI)
                {
                    if (input.IsPointerOverUIObject())
                    {
                        desiredMove *= 0;
                    }
                    else
                    {
                        desiredMove *= panningSpeed;
                    }
                }
                else
                {
                    desiredMove *= panningSpeed;
                }

                desiredMove *= Time.unscaledDeltaTime;
                desiredMove = Quaternion.Euler(new Vector3(0f, transform.eulerAngles.y, 0f)) * desiredMove;
                var tempPos = targetPos + desiredMove;
                targetPos = tempPos;
                // if (MoveForecast(tempPos))
                // {
                //     targetPos = tempPos;
                // }
                if (limitMap)
                {
                    targetPos= new Vector3(
                        Mathf.Clamp(targetPos.x, center.x - aimlimitX, center.x + aimlimitX),
                        Mathf.Clamp(targetPos.y, aimMinHeight, aimMaxHeight),
                        Mathf.Clamp(targetPos.z, center.z - aimlimitZ, center.z + aimlimitZ));
                }

                if (desiredMove.magnitude != 0)
                {
                    ResetTarget();
                }
            }
        }

        /// <summary>
        ///      Motion prediction function of camera
        /// </summary>
        /// <returns></returns>
        // bool MoveForecast(Vector3 pos)
        // {
        //     bool result=true;
        //     var ForeCasetPos = pos - transform.forward * currentDistance;
        //     if (Mathf.Abs(ForeCasetPos.x)>Mathf.Abs(center.x - limitX)||Mathf.Abs(ForeCasetPos.x)>Mathf.Abs(center.x + limitX))
        //     {
        //         result= false;
        //     }
        //     if (ForeCasetPos.y<minHeight||ForeCasetPos.y>maxHeight)
        //     {
        //         result= false;
        //     }
        //     if (Mathf.Abs(ForeCasetPos.z)>Mathf.Abs(center.z - limitZ)||Mathf.Abs(ForeCasetPos.z)>Mathf.Abs(center.z + limitZ))
        //     {
        //         result= false;
        //     }
        //     return result;
        // }
        
        
        /// <summary>
        /// Height/Scale
        /// </summary>
        private void HeightCalculation()
        {
            if (useScrollwheelZooming)
            {
                float targetSpeed = 0;
                if (adaptiveScrollWheelSpeed)
                {
                    targetSpeed = ScrollWheel * Mathf.Clamp(targetDistance, 1, maxDistance);
                    targetSpeed *=  adaptiveScrollWheelamend;
                }
                else
                {
                    targetSpeed = ScrollWheel * scrollWheelZoomingSensitivity;
                }

                if (isCheckUI)
                {
                    if (input.IsPointerOverUIObject())
                    {
                        targetSpeed = 0;
                    }
                    else
                    {
                        targetDistance -= targetSpeed;
                    }
                }
                else
                {
                    targetDistance -= targetSpeed;
                }
                targetDistance = Mathf.Clamp(targetDistance, minDistance, maxDistance);
            }

            if (useKeyboardZooming)
            {
                zoomPos = ZoomDirection * keyboardZoomingSensitivity * Time.unscaledDeltaTime;
                if (zoomPos != 0)
                {
                    ResetTarget();
                }

                targetPos+= new Vector3(0, zoomPos, 0);
                if (limitMap)
                {
                    //Clamp Aim 
                    targetPos= new Vector3(
                        Mathf.Clamp(targetPos.x, center.x - aimlimitX, center.x + aimlimitX),
                        Mathf.Clamp(targetPos.y, aimMinHeight, aimMaxHeight),
                        Mathf.Clamp(targetPos.z, center.z - aimlimitZ, center.z + aimlimitZ));
                }
            }
        }

        /// <summary>
        ///RotateCamera
        /// </summary>
        private void Rotation()
        {
            if (IsSelfRotate)
            {
                if (useKeyboardRotation)
                {
                    transform.Rotate(Vector3.up, RotationDirection * Time.unscaledDeltaTime * keyboardRotationSpeed,
                        Space.World);
                    if (RotationDirection != 0)
                    {
                        ResetTarget();
                    }
                }

                if (useMouseRotation && input.GetKey(mouseRotationKey))
                {
                    transform.Rotate(Vector3.up, -MouseAxis.x * Time.unscaledDeltaTime * mouseRotationSpeed, Space.World);
                    ResetTarget();
                }
            }
            else
            {
                if (useMouseRotation && input.GetKey(mouseRotationKey))
                {
                    if (isCheckUI)
                    {
                        if (input.IsPointerOverUIObject())
                        {
                            x += 0;
                            y -= 0;
                        }
                        else
                        {
                            x += MouseAxis.x * mouseRotationSpeed ;
                            y -= MouseAxis.y * mouseRotationSpeed ;
                        }
                    }
                    else
                    {
                        x += MouseAxis.x * mouseRotationSpeed ;
                        y -= MouseAxis.y * mouseRotationSpeed ;
                    }
                }

                if (IsFixedXAngle)
                {
                    y = xAngle;
                }

                if (isClampXAngle)
                {
                    y = ClampAngle(y, clampMinXAngle, clampMaxXAngle);
                }

                targetAngles = new Vector2(y, x);
            }
        }

        /// <summary>
        /// Calculate the boundary size of the object
        /// </summary>
        /// <param name="go"></param>
        /// <returns></returns>
        Bounds CalculateBounds(GameObject go)
        {
            Bounds b = new Bounds(go.transform.position, Vector3.zero);
            UnityEngine.Object[] rList = go.GetComponentsInChildren(typeof(Renderer));
            foreach (Renderer r in rList)
            {
                b.Encapsulate(r.bounds);
            }

            return b;
        }
        

        /// <summary>
        /// Limit the camera area to X and Z axes
        /// </summary>
        private void LimitPosition()
        {
            if (!limitMap)
                return;
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, center.x - limitX, center.x + limitX),
                Mathf.Clamp(transform.position.y, minHeight, maxHeight),
                Mathf.Clamp(transform.position.z, center.z - limitZ, center.z + limitZ));
        }

        /// <summary>
        /// Ray Test Ground
        /// </summary>
        /// <returns></returns>
        private bool RayToGround(Vector3 dir, ref Vector3 point)
        {
            Ray ray = new Ray(transform.position, dir);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, groundMask.value))
            {
                point = hit.point;
                return true;
            }

            return false;
        }

        //Limited rotation Angle
        private float ClampAngle(float angle, float min, float max)
        {
            if (angle < -360)
                angle += 360;
            if (angle > 360)
                angle -= 360;
            return Mathf.Clamp(angle, min, max);
        }


        /// <summary>
        /// SetTarget
        /// </summary>
        /// <param name="target"></param>
        public void SetTarget(Transform target)
        {
            //Prevents repeated selection of the current object  
            if (target == targetSelect)
            {
                return;
            }

            if (target.GetComponent<ParameterConfiguration>() != null)
            {
                parameter = target.GetComponent<ParameterConfiguration>();
                scrollWheelZoomingSensitivity = parameter.scrollWheelZoomingSensitivity;
                maxDistance = parameter.maxDistance;
                minDistance = parameter.minDistance;
                targetWeight = parameter.targetWeight;
                mouseRotationSpeed = parameter.mouseRotationSpeed;
               targetDistance = parameter.targetDistance;
                if (parameter.isSettingRotate)
                {
                    x = parameter.currentRotate.y;
                    y = parameter.currentRotate.x; 
                    targetAngles= new Vector2(y, x);
                    currentAngle = new Vector2( ClampAngle(currentAngle.x, -180, 180) ,ClampAngle(currentAngle.y, -180, 180));
                }
            }

            targetSelect = target;
            if (SelectTargetAction != null)
            {
                SelectTargetAction.Invoke(target.gameObject);
            }
            targetPos = targetSelect.position+targetOffset;
        }

        /// <summary>
        ///ResetTarget
        /// </summary>
        public void ResetTarget()
        {
            targetSelect = null;
            parameter = null;
            isFocusing = false;
            ResetParameters();
            if (ResetTargetAction != null)
            {
                ResetTargetAction();
            }
        }

        /// <summary>
        /// Set camera position and rotation
        /// </summary>
        /// <param name="positon">camera to pos</param>
        /// <param name="rotation">camera rotate</param>
        /// <param name="isForce">Forced to arrive instantly</param>
        public void SetCameraPostionRotation(Vector3 positon, Vector3 rotation,bool isForce=false)
        {
            this.setPostion = new Vector3(Mathf.Clamp(positon.x, center.x - limitX, center.x + limitX),
                Mathf.Clamp(positon.y, minHeight, maxHeight),
                Mathf.Clamp(positon.z, center.z - limitZ, center.z + limitZ));

            if (IsFixedXAngle)
            {
                this.setRotate = new Vector3(xAngle, rotation.y, rotation.z);
            }
            else if (isClampXAngle)
            {
                if (rotation.x < clampMinXAngle || rotation.x > clampMaxXAngle)
                {
                    Debug.LogError("Outside the X-axis Angle limit:" + rotation.x);
                }

                this.setRotate = new Vector3(ClampAngle(rotation.x, clampMinXAngle, clampMaxXAngle), rotation.y,
                    rotation.z);
            }
            else
            {
                this.setRotate = new Vector3(rotation.x, rotation.y, rotation.z);
            }

            if (isForce)
            {
                ResetCameraTransfomCompleteAction();
                transform.position = setPostion;
                transform.rotation = Quaternion.Euler(setRotate);
                isSetting = false;
                Vector3 angles = transform.eulerAngles;
                x = angles.y;
                y = angles.x;
                currentAngle  =targetAngles=  angles;
                ResetTarget();
                isFocusing = false;
                CalcAimPosAndDistance();
            }
            else
            {
                isSetting = true;
            }

           
        }
        
        
        /// <summary>
        /// Manually set the movable area of camera and target point
        /// </summary>
        /// <param name="centerPos">Range center point</param>
        /// <param name="aimBoxExtend">Extended offset of target point based on center point</param>
        /// <param name="cameraBoxExtend">Extended range based on center point camera</param>
        public  void SetAimAndCameraClamp(Vector3 centerPos, Vector3 aimBoxExtend, Vector3 cameraBoxExtend)
        {
            if (limitMap)
            {
                center = centerPos;
                aimlimitX = aimBoxExtend.x;
                aimlimitZ = aimBoxExtend.z;
                aimMaxHeight = aimBoxExtend.y;
                limitX = cameraBoxExtend.x;
                limitZ = cameraBoxExtend.z;
                maxHeight = cameraBoxExtend.y;
            }
        }
        
        /// <summary>
        /// Set the maximum and minimum distance between the camera and the target point
        /// </summary>
        /// <param name="minDis">minimum distance</param>
        /// <param name="MaxDis">maximum distance</param>
        public  void SetAimAndCameraDistance(float minDis=1,float MaxDis=50)
        {
            minDistance = minDis;
            maxDistance = MaxDis;
        }

        
        /// <summary>
        /// Turn the pan tab on or off
        /// </summary>
        /// <param name="enablekeybordInput">keyboard entry</param>
        /// <param name="enableMouseInput">Mouse entry</param>
        /// <param name="useScreenEdgeInput">ScreenEdge entry</param>
        public void EnablePaning(bool enablekeybordInput=true,bool enableMouseInput=true,bool useScreenEdgeInput=true)
        {
            useKeyboardInput = enablekeybordInput;
            usePanning = enableMouseInput;
            this.useScreenEdgeInput = useScreenEdgeInput;
        }


       /// <summary>
       /// Whether the rotation function is activated
       /// </summary>
       /// <param name="enableMouseRotation"></param>
        public void EnableRotate(bool enableMouseRotation=true)
        {
            useMouseRotation = enableMouseRotation;
        }
       
       /// <summary>
       /// Turn zoom on or off
       /// </summary>
       /// <param name="enableScrollwheelZooming"></param>
       /// <param name="enableKeybordZooming"></param>
       public void EnableZoom(bool enableScrollwheelZooming=true,bool enableKeybordZooming=true)
       {
           useKeyboardZooming = enableKeybordZooming;
               useScrollwheelZooming = enableScrollwheelZooming;
       }
       
 
       
    }
}