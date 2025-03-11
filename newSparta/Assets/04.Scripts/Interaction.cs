using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class Interaction : MonoBehaviour
{
    public float checkRate = 0.05f;
    private float lastCheckTime ;
    public float  maxCheckDistance;
    public LayerMask layerMask;
    public GameObject curInteractGameObject;
    private IInteractable curInteractable;
    public TextMeshProUGUI promptText;
    private Camera camera;


    // Start is called before the first frame update
    void Start()
    {
        camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time - lastCheckTime > checkRate)
        {

            lastCheckTime = Time.time;

            Ray ray = camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
            RaycastHit hit;

            if(Physics.Raycast(ray, out hit, maxCheckDistance, layerMask))
            {
                if(hit.collider.gameObject != curInteractGameObject)
                {
                    curInteractGameObject = hit.collider.gameObject;
                    curInteractable = hit.collider.GetComponent<IInteractable>();
                    SetPromptText();
                }
            }
            else
            {
                curInteractGameObject = null;
                curInteractable = null;
                promptText.gameObject.SetActive(false);
            }
        }
    }
    private void SetPromptText()
    {
        promptText.gameObject.SetActive(true);
        promptText.text = curInteractable.GetInteractPromp();
    }
    public void OnInteractInput(InputAction.CallbackContext context)
{
    if (context.phase == InputActionPhase.Started)
    {
        if (curInteractable == null)
        {
            Debug.LogWarning("상호작용할 대상이 없습니다!");
            return;
        }

        curInteractable.OnInteract();
        curInteractGameObject = null;
        curInteractable = null;

        if (promptText != null)
        {
            promptText.gameObject.SetActive(false);
        }
    }
}

}
