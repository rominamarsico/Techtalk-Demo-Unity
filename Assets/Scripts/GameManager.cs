using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GameManager : MonoBehaviour
{
    public GameObject currentObject;

    public class MyObjetcs 
    {
        public bool cube;
        public bool donut;
        public bool rectangle;
        public bool sphere;
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GetFirestoreValues());
    }

    IEnumerator GetFirestoreValues() 
    {
        UnityWebRequest getFirestoreData = new UnityWebRequest("https://us-central1-techtalk-demo-6ecbb.cloudfunctions.net/readFromFirestore");
        getFirestoreData.downloadHandler = new DownloadHandlerBuffer();
        getFirestoreData.chunkedTransfer = false;
        yield return getFirestoreData.SendWebRequest();

        string firestoreData = getFirestoreData.downloadHandler.text;

        if(getFirestoreData.isNetworkError || getFirestoreData.isHttpError) 
        {
            Debug.Log(getFirestoreData.error);
        } else {
            Debug.Log(firestoreData);
            CreateFromJSON(firestoreData);
        }
    }

    void CreateFromJSON(string firestoreData)
    {
        bool cubeValue = JsonUtility.FromJson<MyObjetcs>(firestoreData).cube;
        bool donutValue = JsonUtility.FromJson<MyObjetcs>(firestoreData).donut;
        bool rectangleValue = JsonUtility.FromJson<MyObjetcs>(firestoreData).rectangle;
        bool sphereValue = JsonUtility.FromJson<MyObjetcs>(firestoreData).sphere;

        ShowObjects(cubeValue, "Cube");
        ShowObjects(sphereValue, "Sphere");
    }

    void ShowObjects(bool showObject, string objectName)
    {
        GameObject objectResource = Resources.Load<GameObject>(objectName);

        if(showObject == true) {
            GameObject currentObject = Instantiate(objectResource);
        } else {
            Destroy(currentObject);
        }
    }
}
