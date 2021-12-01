using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilePositions : MonoBehaviour
{
    [SerializeField] Tilemap tileMap;
    [SerializeField] GameObject prefabDrop;
    [SerializeField] GameObject s_CameraController;

    public List<Vector3> availablePlaces;
    public ArrayList numberList;

    private AudioManager p_AudioManager;
    private int p_RandomPosition;
    private IEnumerator[] coroutineList;
    private bool p_CanDrop;

    void Start()
    {
        p_AudioManager = FindObjectOfType<AudioManager>();
        p_CanDrop = false;
        numberList = new ArrayList();
        availablePlaces = new List<Vector3>();
        for (int n = tileMap.cellBounds.xMin; n < tileMap.cellBounds.xMax; n++)
        {
            for (int p = tileMap.cellBounds.yMin; p < tileMap.cellBounds.yMax; p++)
            {
                Vector3Int localPlace = (new Vector3Int(n, p, (int)tileMap.transform.position.y));
                Vector3 place = tileMap.CellToWorld(localPlace);
                if (tileMap.HasTile(localPlace))
                {
                    availablePlaces.Add(place);
                }
                else
                {

                    //availablePlaces.Add(place);
                    //No tile at "place"
                }
            }
        }
       // coroutine = Sample;
    }

    IEnumerator Sample()
    {
        yield return null;
    }

    private void Update()
    {
        if (p_CanDrop) {

            p_AudioManager.queueSound("fall");
            for (int i = 0; i <= Random.Range(10,15); i++)
            {

                while (true)
                {
                    p_RandomPosition = Random.Range(0, availablePlaces.Count - 1);
                    if (!numberList.Contains(p_RandomPosition)) {
                        numberList.Add(p_RandomPosition);
                        break;
                    }
                }

                Instantiate(prefabDrop).transform.position = availablePlaces[p_RandomPosition] + new Vector3(0f, 2.0f);
            }

            p_CanDrop = false;
            p_AudioManager.queueSound("bang");
            s_CameraController.GetComponent<CameraShake>().shakeDuration = .25f;
        }

        if (numberList != null)
        {
            numberList.Clear();
        }
        //Debug.Log(availablePlaces.Count);
    }

    public void PlayDrop()
    {
      //  p_AudioManager.queueSound("bang");
    }

    public void ShakeGround()
    {
        //Debug.Log("Shake me");
     //   s_CameraController.GetComponent<CameraShake>().shakeDuration = .25f;
    }

    public void DropStones()
    {
      p_CanDrop = true;
    }
}
