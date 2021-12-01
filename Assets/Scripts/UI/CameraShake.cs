using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour
{
	// Transform of the camera to shake. Grabs the gameObject's transform
	// if null.
	public Transform camTransform;

	// How long the object should shake for.
	public float shakeDuration = 0f;

	// Amplitude of the shake. A larger value shakes the camera harder.
	public float shakeAmount = 0.7f;
	public float decreaseFactor = 1.0f;

	Vector3 originalPos;
	Vector3 target;

	[SerializeField] private Vector2 s_ShakeLimit;
	private float p_ShakeTime;
	private float p_CurrentTime;
	void Awake()
	{
		target = FindObjectOfType<PlayerMovement>().transform.position;
		if (camTransform == null)
		{
			camTransform = GetComponent(typeof(Transform)) as Transform;
		}
	}

	void OnEnable()
	{
		originalPos = camTransform.localPosition;
		//p_ShakeTime = Random.Range(s_ShakeLimit.x, s_ShakeLimit.y);
	}

	void Update()
	{
		if (shakeDuration > 0)
		{
			camTransform.localPosition = transform.position + Random.insideUnitSphere * shakeAmount;

			shakeDuration -= Time.deltaTime * decreaseFactor;
		}
		else
		{
			shakeDuration = 0f;
		//	camTransform.localPosition = originalPos;
		}

   /*     p_CurrentTime += Time.deltaTime;

        if (p_CurrentTime >= p_ShakeTime)
        {
            shakeDuration += .25f;
            p_CurrentTime = 0;
            p_ShakeTime = Random.Range(s_ShakeLimit.x, s_ShakeLimit.y);
        }*/

    }
}