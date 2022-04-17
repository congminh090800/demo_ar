using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class ARBullseye : MonoBehaviour
{
    public GameObject bullseyeObj;
    public GameObject cubeObj;
    public ARRaycastManager raycastManager;
    public bool useBullseye = true;

    // Start is called before the first frame update
    void Start()
    {
        bullseyeObj.SetActive(useBullseye);
    }

    // Update is called once per frame
    void Update()
    {
        if (useBullseye)
        {
            UpdateBullseye();
        }



        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
	    if (useBullseye)
	    {
	        GameObject.Instantiate(cubeObj, transform.position, transform.rotation);
	    } else
	    {
		List<ARRaycastHit> raycastHits = new List<ARRaycastHit>();
		raycastManager.Raycast(Input.GetTouch(0).position, raycastHits, UnityEngine.XR.ARSubsystems.TrackableType.Planes);
		if (raycastHits.Count > 0)
		{
		    GameObject.Instantiate(cubeObj, raycastHits[0].pose.position, raycastHits[0].pose.rotation);
		}
	    }
        }
    }
    
    void UpdateBullseye()
    {
    	// bullseye always appear in the middle
        Vector2 middleOfScreen = Camera.main.ViewportToScreenPoint(new Vector2(0.5f, 0.5f));
        
        // detect plane
        List<ARRaycastHit> hits = new List<ARRaycastHit>();
        raycastManager.Raycast(middleOfScreen, hits, UnityEngine.XR.ARSubsystems.TrackableType.Planes);

        if (hits.Count > 0)
        {
            transform.position = hits[0].pose.position;
            transform.rotation = hits[0].pose.rotation;
        }
    }
}
