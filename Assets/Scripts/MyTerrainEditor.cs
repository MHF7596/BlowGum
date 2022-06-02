using UnityEngine;

public class MyTerrainEditor : MonoBehaviour
{
    public enum DeformMode {RaiseLower}
    DeformMode deformMode = DeformMode.RaiseLower;
    string[] deformModeNames = new string[] { "Raise Lower"};

    public Terrain terrain;
    public Texture2D deformTexture;
    public float strength;
    public float area;

    Transform buildTarget;
    Vector3 buildTargPos;
    Light spotLight;

    //GUI
    bool onWindow = false;
    bool onTerrain;
    Texture2D newTex;
    float strengthSave;

    //Raycast
    private RaycastHit hit;

    //Deformation variables
    private int xRes;
    private int yRes;
    private float[,] saved;
    Color[] craterData;

    TerrainData tData;
    [SerializeField] private Transform fingerImage;
    // Start is called before the first frame update
    void Start()
    {
        //Create build target object
        GameObject tmpObj = new GameObject("BuildTarget");
        buildTarget = tmpObj.transform;

        //Add Spot Light to build target
        GameObject spotLightObj = new GameObject("SpotLight");
        spotLightObj.transform.SetParent(buildTarget);
        spotLightObj.transform.localPosition = new Vector3(0, 2, 0);
        spotLightObj.transform.localEulerAngles = new Vector3(90, 0, 0);
        spotLight = spotLightObj.AddComponent<Light>();
        spotLight.type = LightType.Spot;
        spotLight.range = 20;

        tData = terrain.terrainData;
        if (tData)
        {
            //Save original height data
            xRes = tData.heightmapResolution;
            yRes = tData.heightmapResolution;
            saved = tData.GetHeights(0, 0, xRes, yRes);
        }

        //Change terrain layer to UI
        terrain.gameObject.layer = 5;
        strength = 5000;
        area = 2f;
        brushScaling();
    }

    void FixedUpdate()
    {
        raycastHit();

        if (onTerrain && !onWindow)
        {
            terrainDeform();
        }

        //Update Spot Light Angle according to the Area value
        spotLight.spotAngle = area * 25f;
    }

    private void Update()
    {
        if (Application.isEditor)
        {
            if (Input.GetMouseButtonDown(0))
            {
                fingerImage.gameObject.SetActive(true);
            }

            if (Input.GetMouseButton(0))
            {
                fingerImage.position = Input.mousePosition;
            }

            if (Input.GetMouseButtonUp(0))
            {
                fingerImage.gameObject.SetActive(false);
            }
        }
    }
    void raycastHit()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        hit = new RaycastHit();
        //Do Raycast hit only against UI layer
        if (Physics.Raycast(ray, out hit, 300, 1 << 5))
        {
            onTerrain = true;
            if (buildTarget)
            {
                buildTarget.position = Vector3.Lerp(buildTarget.position, hit.point + new Vector3(0, 1, 0), Time.time);
            }
        }
        else
        {
            if (buildTarget)
            {
                Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 200);
                Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint);
                buildTarget.position = curPosition;
                onTerrain = false;
            }
        }
    }

    void terrainDeform()
    {
        if (Input.GetMouseButtonDown(0))
        {
            buildTargPos = buildTarget.position - terrain.GetPosition();
            //float x = Mathf.Clamp01(buildTargPos.x / tData.size.x);
            //float y = Mathf.Clamp01(buildTargPos.z / tData.size.z);
        }

        //Terrain deform
        if (Input.GetMouseButton(0))
        {
            buildTargPos = buildTarget.position - terrain.GetPosition();

            if (newTex && tData && craterData != null)
            {
                int x = (int)Mathf.Lerp(0, xRes, Mathf.InverseLerp(0, tData.size.x, buildTargPos.x));
                int z = (int)Mathf.Lerp(0, yRes, Mathf.InverseLerp(0, tData.size.z, buildTargPos.z));
                x = Mathf.Clamp(x, newTex.width / 2, xRes - newTex.width / 2);
                z = Mathf.Clamp(z, newTex.height / 2, yRes - newTex.height / 2);
                int startX = x - newTex.width / 2;
                int startY = z - newTex.height / 2;
                float[,] areaT = tData.GetHeights(startX, startY, newTex.width, newTex.height);
                for (int i = 0; i < newTex.height; i++)
                {
                    for (int j = 0; j < newTex.width; j++)
                    {
                        if (deformMode == DeformMode.RaiseLower)
                        {
                            areaT[i, j] = areaT[i, j] - craterData[i * newTex.width + j].a * strength / 15000;
                        }
                    }
                }
                tData.SetHeights(x - newTex.width / 2, z - newTex.height / 2, areaT);
            }
        }
    }

    void brushScaling()
    {
        //Apply current deform texture resolution 
        newTex = Instantiate(deformTexture) as Texture2D;
        //TextureScale.Point(newTex, deformTexture.width * (int)area / 10, deformTexture.height * (int)area / 10);
        TextureScale.Point(newTex, deformTexture.width * (int)area / 10 * 20, deformTexture.height * (int)area / 10);
        newTex.Apply();
        craterData = newTex.GetPixels();
    }

    void OnApplicationQuit()
    {
        ResetTerrainHeights();
    }

    public void ResetTerrainHeights()
    {
        tData.SetHeights(0, 0, saved);
    }
}