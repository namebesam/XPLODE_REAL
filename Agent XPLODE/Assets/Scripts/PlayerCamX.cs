using UnityEngine;

public class PlayerCamX : MonoBehaviour
{
    public float sensX;
    public float sensY;

    public Transform orientation;

    public Transform playerObj;

    float xRotation;
    float yRotation;


    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; //locks cursor in middle of screen and makes invisible
        Cursor.visible = false;
        
        xRotation = transform.eulerAngles.x;
        yRotation = transform.eulerAngles.y;

        SettingsHolder settingsHolder = FindAnyObjectByType<SettingsHolder>();
        if (settingsHolder)
        {
            sensX = settingsHolder.playerSens;
            sensY = settingsHolder.playerSens;
        }
    }

    // Update is called once per frame
    void Update()
    {
        PauseBehavior pauseBehavior = GameObject.FindAnyObjectByType<PauseBehavior>();
        if (!pauseBehavior.isGamePaused)
        {
            //get mouse input
            float mouseX = Input.GetAxisRaw("Mouse X") * sensX;
            float mouseY = Input.GetAxisRaw("Mouse Y") * sensY;

            yRotation += mouseX;

            xRotation -= mouseY;

            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            // rotate cam and orientation
            transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
            orientation.rotation = Quaternion.Euler(xRotation, yRotation, 0);
            playerObj.rotation = Quaternion.Euler(0, yRotation, 0);
        }
    }
}
