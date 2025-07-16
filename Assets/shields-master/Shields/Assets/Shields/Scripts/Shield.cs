using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class ShieldEffect : MonoBehaviour
{
    Renderer _renderer;
    [SerializeField] AnimationCurve _DisplacementCurve;
    [SerializeField] float _DisplacementMagnitude;
    [SerializeField] float _LerpSpeed;
    [SerializeField] float _DisolveSpeed;
    bool _shieldOn;
    Coroutine _disolveCoroutine;

    Material _mat;  // only onem material

   
    void Start()
    {
        _renderer = GetComponent<Renderer>();
        _mat = _renderer.material;
      
       
        GetComponent<Collider>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
      
    }

    public void HitShield(Vector3 hitPos)
    {
        _mat.SetVector("_HitPos", hitPos);
        StopAllCoroutines();
        StartCoroutine(Coroutine_HitDisplacement());
    }

    IEnumerator Coroutine_HitDisplacement()
    {
        float lerp = 0;
        while (lerp < 1)
        {
            _mat.SetFloat("_DisplacementStrength", _DisplacementCurve.Evaluate(lerp) * _DisplacementMagnitude);
            lerp += Time.deltaTime*_LerpSpeed;
            yield return null;
        }
    }

    IEnumerator Coroutine_DisolveShield(float target)
    {
        float start = _mat.GetFloat("_Disolve");
        float lerp = 0;
        while (lerp < 1)
        {
            //_mat.SetFloat("_Disolve", Mathf.Lerp(start,target,lerp));
            //lerp += Time.deltaTime * _DisolveSpeed;
            //yield return null;
            lerp += Time.deltaTime * _DisolveSpeed;
            float t = Mathf.Clamp01(lerp); // Clamp to ensure it reaches 1

            float value = Mathf.Lerp(start, target, t);
            _mat.SetFloat("_Disolve", value);

            yield return null;
        }

        _mat.SetFloat("_Disolve", target);
    }

    public void EnableShield()
    {
        StartCoroutine(EnableShieldDelayed());
    }

    IEnumerator EnableShieldDelayed()
    {
        yield return null; // wait one frame to avoid visual glitch
        OpenShield();

        Collider col = GetComponent<Collider>();
        if (col != null)
        {
            col.enabled = true;
        }

        _shieldOn = true;
    }

    public void DisableShield()
    {
        Debug.Log("Shield disabled");
        CloseSheild(); // Hide shield effect

        Collider col = GetComponent<Collider>();
        if (col != null)
        {
            col.enabled = false;
        }

        _shieldOn = false;
    }

    private void OpenShield()
    {
        if (_disolveCoroutine != null)
        {
            StopCoroutine(_disolveCoroutine);
        }
        _disolveCoroutine = StartCoroutine(Coroutine_DisolveShield(0));
    }
    private void CloseSheild()
    {
        if (_disolveCoroutine != null)
        {
            StopCoroutine(_disolveCoroutine);
        }
        _disolveCoroutine = StartCoroutine(Coroutine_DisolveShield(1));
    }

    

    private void OnCollisionEnter(Collision other)
    {
      
        if (other.gameObject.CompareTag("Enemy"))
        {
           
            // Approximate the hit point from the enemy toward the shield
            Vector3 hitPoint = GetComponent<Collider>().ClosestPoint(other.transform.position);

            HitShield(hitPoint);

        }
    }
}
