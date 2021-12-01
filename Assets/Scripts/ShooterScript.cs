using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterScript : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject s_BulletObject;
    [SerializeField] private GameObject s_BulletParent;
    [SerializeField] private float s_ShootSpeed;
    [SerializeField] private Vector2 s_ShootDirection;
    [SerializeField] private float s_ShootRate;
    [SerializeField] private bool s_IsVertical;


    private 
    void Start()
    {
        if (s_IsVertical)
        {
            GetComponent<Animator>().SetTrigger("isVertical");
        }
        //StartCoroutine(Shoot());
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    IEnumerator Shoot()
    {
        while (true)
        {
            GameObject l_BulletObject = Instantiate(s_BulletObject);
            l_BulletObject.transform.SetParent(transform);
            l_BulletObject.transform.position = transform.position;
            l_BulletObject.transform.GetComponent<Rigidbody2D>().AddForce(s_ShootSpeed * s_ShootDirection * Time.deltaTime);
            yield return new WaitForSeconds(s_ShootRate);
        }
    }

    public void ShootBullet()
    {

        GameObject l_BulletObject = Instantiate(s_BulletObject);
        l_BulletObject.transform.SetParent(s_BulletParent.transform);
        l_BulletObject.transform.position = transform.position;
        l_BulletObject.transform.GetComponent<Rigidbody2D>().AddForce(s_ShootSpeed * s_ShootDirection * Time.deltaTime);
    }
}
