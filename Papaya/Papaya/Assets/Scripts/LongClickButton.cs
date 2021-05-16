using UnityEngine; 
using UnityEngine.Events; 
using UnityEngine.EventSystems; 
using UnityEngine.UI; 

public class LongClickButton : MonoBehaviour , IPointerDownHandler ,IPointerUpHandler 
{ 
    private bool pointerDown; 
     private bool pointerUp; 
    private float pointerDownTimer; 
    public float requiredHoldTime; 
    public UnityEvent onLongClick; 
    public UnityEvent onClick; 
    public UnityEvent LongClick; 

    [SerializeField] 
    private Image fillImage; 
    public void OnPointerDown(PointerEventData eventdata) 
    { 
        onClick.Invoke(); 
        pointerDown = true; 
        Debug.Log("OnPointerDown"); 
    } 
    public void OnPointerUp(PointerEventData eventdata) 
    { 
        Reset(); 
        Debug.Log("PointerUp"); 
    } 
    private void Update() 
    { 
        if(pointerDown) 
        { 
            pointerDownTimer += Time.deltaTime; 
            if(pointerDownTimer >= requiredHoldTime) 
            { 
                if(onLongClick != null) 
                onLongClick.Invoke(); 
                Reset(); 
            } 
            fillImage.fillAmount = pointerDownTimer/ requiredHoldTime; 
        } 

    } 
    private void Reset() 
    { 
        pointerDown = false;
        pointerUp = false; 
        pointerDownTimer = 0; 
       //LongClick.Invoke(); 
         fillImage.fillAmount = pointerDownTimer / requiredHoldTime; 
    } 
} 