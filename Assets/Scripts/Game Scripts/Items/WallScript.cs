using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WallScript : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] Material s_UnlitMaterial;
    [SerializeField] Color s_UnlitColor;
    private Color p_DefaultColor;
    private Material p_DefaultMaterial;
    private GameObject p_Target;
    void Start()
    {
        p_Target = FindObjectOfType<PlayerMovement>().gameObject;
        p_DefaultMaterial = transform.GetComponent<TilemapRenderer>().material;
        p_DefaultColor = transform.GetComponent<Tilemap>().color;
    }

    // Update is called once per frame
    void Update()
    {
    }


}
