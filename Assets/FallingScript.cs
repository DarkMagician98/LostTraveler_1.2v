using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingScript : MonoBehaviour
{

    [SerializeField] GameObject s_FallingStonePrefab;
    [SerializeField] Vector2 s_LocalScale;
    // Start is called before the first frame update
    void Start()
    {
     
        StartCoroutine(DropStones());
    }

    // Update is called once per frame
    void Update()
    {
      
    }

    IEnumerator DropStones()
    {
        while (true)
        {
    
                GameObject stoneObj = Instantiate(s_FallingStonePrefab);
                stoneObj.transform.localScale *= Random.Range(s_LocalScale.x, s_LocalScale.y);
                stoneObj.transform.position = new Vector2(Random.Range(-15.0f, 15.0f), 14.0f);
                stoneObj.transform.rotation = Quaternion.AngleAxis(Random.Range(0, 360), Vector3.forward);
                stoneObj.transform.SetParent(transform);
                yield return new WaitForSeconds(Random.Range(.25f, .5f));
        }
    }
}
