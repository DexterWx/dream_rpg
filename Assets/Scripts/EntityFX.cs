using System.Collections;
using UnityEngine;

public class EntityFX : MonoBehaviour
{
    [Header("FX")]
    [SerializeField] private Material hitMat;
    [SerializeField] private float flashTime;
    private Material originalMat;


    private SpriteRenderer sr;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
        originalMat = sr.material;
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator FlashFX(){
        sr.material = hitMat;
        yield return new WaitForSeconds(flashTime);
        sr.material = originalMat;
    }

    public void RedColorBlink(){
        if (sr.color != Color.white){
            sr.color = Color.white;
        }

        else{
            sr.color = Color.red;
        }
    }

    public void CancelRedColorBlink(){
        CancelInvoke();
        sr.color = Color.white;
    }
}
