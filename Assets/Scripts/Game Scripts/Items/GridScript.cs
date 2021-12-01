using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridScript : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Material s_UnlitMaterial;
    [SerializeField] Color s_UnlitColor;
    [SerializeField] GameObject trapMap;
    private Color p_DefaultColor;
    private Material p_DefaultMaterial;

    private GameObject p_Player;
    void Start()
    {
        p_Player = FindObjectOfType<PlayerMovement>().gameObject;
        p_DefaultColor = trapMap.GetComponent<Tilemap>().color;
        p_DefaultMaterial = trapMap.GetComponent<TilemapRenderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        if (p_Player.GetComponent<PlayerMovement>().IsClairvoyancing())
        {
            StartCoroutine(LightUp(trapMap));
        }
    }

    IEnumerator LightUp(GameObject a_Object)
    {
        while (true)
        {
            yield return new WaitForSeconds(.5f);
            a_Object.GetComponent<Tilemap>().color = s_UnlitColor;
            a_Object.GetComponent<TilemapRenderer>().material = s_UnlitMaterial;
            yield return new WaitForSeconds(3.0f);
            a_Object.GetComponent<Tilemap>().color = p_DefaultColor;
            a_Object.GetComponent<TilemapRenderer>().material = p_DefaultMaterial;
            break;

        }
    }
}
