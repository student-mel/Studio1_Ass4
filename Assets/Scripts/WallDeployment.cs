using UnityEngine;

public class WallDeployment : MonoBehaviour
{
    [Tooltip("Attach the Wall prefab here. This is needed so that objects to fall off the sides.")] public GameObject wallPrefab;

    Camera cam;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (wallPrefab == null)
        {
                wallPrefab = Resources.Load<GameObject>("Prefabs/Wall"); // Load the wall prefab from the Resources folder
        }
        cam = Camera.main;
        Vector3 screenLeft = new Vector3(0, Screen.height / 2, 0);
        Vector3 worldLeft = cam.ScreenToWorldPoint(screenLeft);
        GameObject stageNegLimit = GameObject.Instantiate(wallPrefab, worldLeft, Quaternion.identity); //redeployed this to be the stage's left most limit at start of game        
        stageNegLimit.transform.position = new Vector3(worldLeft.x, 0, 0); //just in case the prefab's position is not exactly at the left edge of the screen, this will ensure it is.
        
        Vector3 screenRight = new Vector3(Screen.width, Screen.height / 2, 0);
        Vector3 worldRight = cam.ScreenToWorldPoint(screenRight);
        GameObject stagePosLimit = GameObject.Instantiate(wallPrefab, worldRight, Quaternion.identity); //redeployed this to be the stage's right most limit at start of game        
        stagePosLimit.transform.position = new Vector3(worldRight.x, 0, 0); //just in case the prefab's position is not exactly at the right edge of the screen, this will ensure it is
        //deploy stage limit to the left edge of the screen at start of game

    }


}
