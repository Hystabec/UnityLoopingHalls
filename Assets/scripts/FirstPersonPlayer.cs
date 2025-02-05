using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor.UIElements;
using UnityEngine.UIElements;
using UnityEngine.WSA;




#if UNITY_EDITOR
using UnityEditor;
    using System.Net;
#endif


public class FirstPersonPlayer : MonoBehaviour
{
    public Camera playerCamera;
    
    public bool useGravity = true;

    public bool useCustomCameraOffset = false;
    public Vector3 customCameraOffset;

    public bool CanLookAround = true;
    public float maxLookAngle = 90.0f;
    public float FOV = 60.0f;

    //rotation of the cam in X and Y
    private float camX = 0.0f;
    private float camY = 0.0f;

    public bool CanMove = true;
    public float movementSpeed = 10.0f;
    public float maxChangeInVelocity = 5.0f;

    public bool CanSprit = true;
    public KeyCode sprintKey = KeyCode.LeftShift;
    public float sprintSpeedMultiplyer = 2.0f;

    public KeyCode ToggleMouseLockKey = KeyCode.Escape;
    public bool LockMouse = true;

    public KeyCode ToggleMouseVisability = KeyCode.Escape;
    public bool ShowMouse = false;

    public float mouseSensitivity = 1f;

    Rigidbody rb;

    void Start()
    {
        //destroy this if no camera is found
        if (playerCamera == null)
        {
            Debug.LogError("FirstPersonPlayer: No camera assigned to GameObject '" + gameObject.name + "'");
            this.enabled = false;
            return;
        }

        #region rigidBodySetup

        //if a rb is found use it or add one if it isnt found
        TryGetComponent<Rigidbody>(out rb);
        if(rb == null)
            rb = gameObject.AddComponent<Rigidbody>();

        //locks the X & Z rotations
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
        //use gravity?
        rb.useGravity = useGravity;

        #endregion

        #region cameraSetup

        //parent the camera to this.gameObject
        playerCamera.transform.parent = transform;
        //makes the camera have the same rotation as the object
        playerCamera.transform.rotation = transform.rotation;
        //move the camera to this.gameObject
        if (useCustomCameraOffset)
            playerCamera.transform.localPosition = customCameraOffset;
        else
            playerCamera.transform.localPosition = new(0, 0.75f, 0);

        playerCamera.fieldOfView = FOV;

        #endregion

        #region Mouse

        if (LockMouse)
            UnityEngine.Cursor.lockState = CursorLockMode.Locked;  
        else
            UnityEngine.Cursor.lockState = CursorLockMode.None;

        UnityEngine.Cursor.visible = ShowMouse;

        #endregion
    }

    void FixedUpdate()
    {
        #region Movement
        if (CanMove)
        {
            Vector3 targVel = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

            float tempMovementSpeed = movementSpeed;

            if (CanSprit && Input.GetKey(sprintKey))
                tempMovementSpeed *= sprintSpeedMultiplyer;

            targVel = transform.TransformDirection(targVel) * tempMovementSpeed;

            Vector3 currentVel = rb.velocity;
            Vector3 velChange = (targVel - currentVel);

            velChange.x = Mathf.Clamp(velChange.x, -maxChangeInVelocity, maxChangeInVelocity);
            velChange.z = Mathf.Clamp(velChange.z, -maxChangeInVelocity, maxChangeInVelocity);
            velChange.y = 0;

            rb.AddForce(velChange, ForceMode.VelocityChange);
        }
        #endregion
    }

    void Update()
    {
        #region Camera
        if (CanLookAround)
        {
            camX = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * mouseSensitivity;

            camY -= Input.GetAxis("Mouse Y") * mouseSensitivity;

            camY = Mathf.Clamp(camY, -maxLookAngle, maxLookAngle);

            transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, camX, transform.localEulerAngles.z);
            playerCamera.transform.localEulerAngles = new Vector3(camY, playerCamera.transform.localEulerAngles.y, playerCamera.transform.localEulerAngles.z);
        }
        #endregion

        #region Mouse

        if(Input.GetKeyDown(ToggleMouseLockKey))
        {
            if (LockMouse)
            {
                UnityEngine.Cursor.lockState = CursorLockMode.None;
                LockMouse = false;
            }
            else
            {
                UnityEngine.Cursor.lockState = CursorLockMode.Locked;
                LockMouse = true;
            }
        }

        if (Input.GetKeyDown(ToggleMouseVisability))
        {
            ShowMouse = !ShowMouse;
            UnityEngine.Cursor.visible = ShowMouse;
        }

        #endregion
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(FirstPersonPlayer)), InitializeOnLoadAttribute]
public class FirstPersonPlayerEditor : Editor
{
    FirstPersonPlayer fpp;

    static bool showDefaultInspector = false;

    void OnEnable()
    {
        fpp = (FirstPersonPlayer)target;
    }

    public override void OnInspectorGUI()
    {    

        #region CameraSetup

        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
        GUILayout.Label("Camera", new GUIStyle(GUI.skin.label) { alignment = TextAnchor.MiddleCenter, fontStyle = FontStyle.Bold, fontSize = 12 });

        fpp.playerCamera = (Camera)EditorGUILayout.ObjectField(new GUIContent("Camera", "Camera to be used by the player."), fpp.playerCamera, typeof(Camera), true);

        fpp.useCustomCameraOffset = EditorGUILayout.Toggle(new GUIContent("Use Custom Camera Offset", "Will allow for the custom offset position of the camera."), fpp.useCustomCameraOffset);

        if(fpp.useCustomCameraOffset)
        {
            EditorGUI.indentLevel++;

            fpp.customCameraOffset = EditorGUILayout.Vector3Field("Custom Camera Offset", fpp.customCameraOffset);

            EditorGUI.indentLevel--;
        }

        EditorGUILayout.Space();

        fpp.CanLookAround = EditorGUILayout.Toggle("Player Can Look Around", fpp.CanLookAround);
        fpp.mouseSensitivity = EditorGUILayout.FloatField("Camera Sensitivity", fpp.mouseSensitivity);
        fpp.maxLookAngle = EditorGUILayout.FloatField("Max Camera Angle", fpp.maxLookAngle);
        fpp.FOV = EditorGUILayout.FloatField("Camera Field of View", fpp.FOV);

        #endregion

        #region Mouse

        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
        GUILayout.Label("Mouse", new GUIStyle(GUI.skin.label) { alignment = TextAnchor.MiddleCenter, fontStyle = FontStyle.Bold, fontSize = 12 });
        fpp.ShowMouse = EditorGUILayout.Toggle(new GUIContent("Show Mouse Cursor", "Decides if the mouse will show or hide during play"), fpp.ShowMouse);
        fpp.ToggleMouseVisability = (KeyCode)EditorGUILayout.EnumPopup("Toggle Mouse Visability Keybind", fpp.ToggleMouseVisability);
        EditorGUILayout.Space();
        fpp.LockMouse = EditorGUILayout.Toggle(new GUIContent("Lock Mouse Cursor", "Decides if the mouse will be locked to the centre of the screen"), fpp.LockMouse);
        fpp.ToggleMouseLockKey = (KeyCode)EditorGUILayout.EnumPopup("Toggle Mouse Lock Keybind", fpp.ToggleMouseLockKey);

        #endregion

        #region MovementSetup

        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
        GUILayout.Label("Movement", new GUIStyle(GUI.skin.label) { alignment = TextAnchor.MiddleCenter, fontStyle = FontStyle.Bold, fontSize = 12 });
        fpp.CanMove = EditorGUILayout.Toggle("Player Can Move", fpp.CanMove);
        fpp.movementSpeed = EditorGUILayout.FloatField("Movement Speed", fpp.movementSpeed);
        fpp.maxChangeInVelocity = EditorGUILayout.FloatField("Max Change in Velocity", fpp.maxChangeInVelocity);

        EditorGUILayout.Space();

        fpp.CanSprit = EditorGUILayout.Toggle("Player Can Sprint", fpp.CanSprit);

        if(fpp.CanSprit)
        {
            fpp.sprintKey = (KeyCode)EditorGUILayout.EnumPopup("Sprint key", fpp.sprintKey);
            fpp.sprintSpeedMultiplyer = EditorGUILayout.FloatField("Sprint Speed Multiplyer", fpp.sprintSpeedMultiplyer);
        }
        
        #endregion


        #region DefaultInspector

        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
        GUILayout.Label("Default Editor", new GUIStyle(GUI.skin.label) { alignment = TextAnchor.MiddleCenter, fontStyle = FontStyle.Bold, fontSize = 12 });
        showDefaultInspector = EditorGUILayout.ToggleLeft("Show Default Inspector", showDefaultInspector);
        if (showDefaultInspector)
        {
            EditorGUILayout.Space();
            DrawDefaultInspector();
        }

        #endregion
    }
} 
#endif
