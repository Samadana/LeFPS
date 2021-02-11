using Mirror;
using UnityEngine;

public class PlayerController : NetworkBehaviour
{
    public TextMesh playerNameText;
    public GameObject floatingInfo;
    public Transform GFX;
    public Rigidbody RigidBody;
    public float speedMovement = 10f;
    public float speedRotation = 100f;
    Camera Cam;

    private Material playerMaterialClone;

    [SyncVar(hook = nameof(OnNameChanged))]
    public string playerName;

    [SyncVar(hook = nameof(OnColorChanged))]
    public Color playerColor = Color.white;

    void Update()
    {
        if (!isLocalPlayer)
        {
            // make non-local players run this
            floatingInfo.transform.LookAt(Camera.main.transform);
            return;
        }        
        

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100))
            {
                Debug.Log("We hit " + hit.collider.name + " " + hit.point);

            }
        }

        float moveX = Input.GetAxis("Horizontal") * Time.deltaTime * speedRotation;
        float moveZ = Input.GetAxis("Vertical") * Time.deltaTime * speedMovement;

        transform.Rotate(0, moveX, 0);
        transform.Translate(0, 0, moveZ);
    }

    void OnNameChanged(string _New )
    {
        Debug.Log("On Name Change");
        playerNameText.text = _New;
    }

    void OnColorChanged(Color _New)
    {
        playerNameText.color = _New;
        playerMaterialClone = new Material(GFX.GetComponent<Renderer>().material);
        playerMaterialClone.color = _New;
        GFX.GetComponent<Renderer>().material = playerMaterialClone;
    }

    public override void OnStartLocalPlayer()
    {
        Cam = Camera.main;
        Camera.main.transform.SetParent(transform);
        Camera.main.transform.localPosition = new Vector3(0, 4f, -6f);
        Camera.main.transform.rotation = Quaternion.Euler(20f, 0, 0);

        floatingInfo.transform.localPosition = new Vector3(0, 1.3f, 0f);
        floatingInfo.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);

        string name = "Player" + Random.Range(100, 999);
        Color color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
        CmdSetupPlayer(name, color);
    }

    [Command]
    public void CmdSetupPlayer(string _name, Color _col)
    {
        Debug.Log("Player want change name and color");
        // player info sent to server, then server updates sync vars which handles it on all clients
        playerName = _name;
        playerColor = _col;
    }    
  
}