using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField] private ARPlaneManager arPlaneManager;
    [SerializeField] private GameObject model3DPrefab;
    // Start is called before the first frame update

    private List<ARPlane> planes = new List<ARPlane>();

    private GameObject model3DPlane;

    private void OnEnable()
    {
        arPlaneManager.planesChanged += PlanesFound;
    }

    private void OnDisables()
    {
        arPlaneManager.planesChanged += PlanesFound;
    }

    private void PlanesFound(ARPlanesChangedEventArgs planeData)
    {
        if (planeData.added != null && planeData.added.Count > 0){
            planes.AddRange(planeData.added);
        }
            foreach (var plane in planes)
            {
                if (plane.extents.x * plane.extents.y >= 0.4f && model3DPlane == null)
                {
                    model3DPlane = Instantiate(model3DPrefab);
                    float yOffSet = model3DPlane.transform.localScale.y / 2;
                    model3DPlane.transform.position = new Vector3(plane.center.x, plane.center.y + yOffSet, plane.center.z);
                    model3DPlane.transform.forward = plane.normal;
                    StopPlanesDetection();
                }
            }

    }

    public void StopPlanesDetection()
    {
        foreach(var plane in arPlaneManager.trackables)
        {
            plane.gameObject.SetActive(false);
        }
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
