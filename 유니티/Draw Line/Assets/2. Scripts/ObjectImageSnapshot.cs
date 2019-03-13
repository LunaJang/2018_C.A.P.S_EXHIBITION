using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ObjectImageSnapshot : MonoBehaviour {

    public Camera objectImageCamera;
    [HideInInspector]
    public int objectImageLayer;

    public int snapshotTextureWidth = 128;
    public int snapshotTextureHeight = 128;
    public Vector3 defaultPosition = new Vector3(0, 0, 0);
    public Vector3 defaultRotation = new Vector3(345.8529f, 313.8297f, 14.28433f);
    public Vector3 defaultRotation1 = new Vector3(0, 0, 0);
    public Vector3 defaultScale = new Vector3(10, 10, 10);

    void Start () {
	}

    void Update()
    {
        GameObject tmp = GameObject.Find("paper");
        
        objectImageCamera.transform.rotation = tmp.transform.rotation;
        if(objectImageCamera.transform.eulerAngles.y < 45 && objectImageCamera.transform.eulerAngles.y >= -45)
            objectImageCamera.transform.position = new Vector3(tmp.transform.position.x, tmp.transform.position.y, tmp.transform.position.z-0.3f);
        else
            objectImageCamera.transform.position = new Vector3(tmp.transform.position.x - 0.3f, tmp.transform.position.y, tmp.transform.position.z);
    }

    void SetLayerRecursively(GameObject o, int layer)
    {
        foreach (Transform t in o.GetComponentsInChildren<Transform>(true))
            t.gameObject.layer = layer;
    }

    public Texture2D TakeObjectSnapshot(GameObject prefab)
    {
        return TakeObjectSnapshot(prefab, defaultPosition, Quaternion.Euler(defaultRotation), defaultScale);
    }


    public Texture2D TakeObjectSnapshot(GameObject prefab, Vector3 position)
    {
        return TakeObjectSnapshot(prefab, position, Quaternion.Euler(defaultRotation), defaultScale);
    }

    public Texture2D TakeObjectSnapshot(GameObject prefab, Vector3 position, Quaternion rotation, Vector3 scale)
    {

        GameObject gameObject = GameObject.Instantiate(prefab, position, rotation) as GameObject;
        gameObject.transform.localScale = scale;

        SetLayerRecursively(gameObject, objectImageLayer);
        
        objectImageCamera.targetTexture = RenderTexture.GetTemporary(snapshotTextureWidth, snapshotTextureHeight, 24);
        objectImageCamera.Render();
        
        RenderTexture saveActive = RenderTexture.active;
        RenderTexture.active = objectImageCamera.targetTexture;
        Texture2D texture = new Texture2D(objectImageCamera.targetTexture.width, objectImageCamera.targetTexture.height);
        texture.ReadPixels(new Rect(0, 0, objectImageCamera.targetTexture.width, objectImageCamera.targetTexture.height), 0, 0);
        texture.Apply();
        RenderTexture.active = saveActive;

        byte[] bytes = texture.EncodeToPNG();
        File.WriteAllBytes(Application.dataPath+"/picture.png",bytes);
        
        RenderTexture.ReleaseTemporary(objectImageCamera.targetTexture);
        GameObject.DestroyImmediate(gameObject);

        return texture;
    }
}
