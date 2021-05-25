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
    public bool Wongame = false;

    [SerializeField] 
    private Image fillImage; 
    public void OnPointerDown(PointerEventData eventdata) 
    { 
        onClick.Invoke(); 
        Wongame = false;
        
        pointerDown = true; 
        Debug.Log("OnPointerDown"); 
    } 
    public void OnPointerUp(PointerEventData eventdata) 
    { 
        if (Wongame == false)
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
                Wongame = true;
                if(onLongClick != null) 
                onLongClick.Invoke(); 
               // Reset(); 
                
            } 
            fillImage.fillAmount = pointerDownTimer/ requiredHoldTime; 
        } 

    } 
    private void Reset() 
    { 
        onClick.Invoke();
        Wongame = false;
        pointerDown = false;
        pointerUp = false; 
        pointerDownTimer = 0; 
       LongClick.Invoke(); 
         fillImage.fillAmount = pointerDownTimer / requiredHoldTime; 
    } 
} 