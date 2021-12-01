using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LightUpScript : MonoBehaviour
{

    [SerializeField] private Color s_UnlitColor;
    [SerializeField] private Material s_UnlitMaterial;

    private float clairvoyanceTime = 5.0f;
    private GameObject p_Target;
    void Start()
    {
        p_Target = FindObjectOfType<PlayerMovement>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (p_Target.GetComponent<PlayerMovement>().IsClairvoyancing())
        {
            for(int i = 0; i < transform.childCount; i++)
            {
                Transform child = transform.GetChild(i);

                if(child.gameObject.TryGetComponent<TilemapRenderer>(out TilemapRenderer tilemapRenderer))
                {
                    StartCoroutine(LightUpTilemapRenderer(child.gameObject, child.GetComponent<Tilemap>().color, tilemapRenderer.material));
                }
                else if(child.gameObject.TryGetComponent<SpriteRenderer>(out SpriteRenderer spriteRenderer))
                {
                    StartCoroutine(LightUpSpriteRenderer(child.gameObject, spriteRenderer.color, spriteRenderer.material));
                }
            }
        }
    }

    IEnumerator LightUpSpriteRenderer(GameObject child, Color l_DefaultColor, Material l_DefaultMat)
    {
    
            yield return new WaitForSeconds(.5f);
            if (child != null)
            {
                child.GetComponent<SpriteRenderer>().color = s_UnlitColor;
                child.GetComponent<SpriteRenderer>().material = s_UnlitMaterial;
            }
            yield return new WaitForSeconds(clairvoyanceTime);
            if (child != null)
            {
                child.GetComponent<SpriteRenderer>().color = l_DefaultColor;
                child.GetComponent<SpriteRenderer>().material = l_DefaultMat;
            }
  
    }

    IEnumerator LightUpTilemapRenderer(GameObject child, Color l_DefaultColor, Material l_DefaultMat)
    {
        
            yield return new WaitForSeconds(.5f);
            if (child != null)
            {
                child.GetComponent<Tilemap>().color = s_UnlitColor;
                child.GetComponent<TilemapRenderer>().material = s_UnlitMaterial;
            }
            yield return new WaitForSeconds(clairvoyanceTime);
            if (child != null)
            {
                child.GetComponent<Tilemap>().color = l_DefaultColor;
                child.GetComponent<TilemapRenderer>().material = l_DefaultMat;
            }
         
    }
}
