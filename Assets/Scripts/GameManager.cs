using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GameManager : MonoBehaviour
{
    public GameObject currentObject;

    public class MyObjetcs 
    {
        public bool cake;
        public bool donut;
        public bool hamburger;
        public bool icecream;
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
        yield return getFirestoreData.SendWebRequest();

        string firestoreData = getFirestoreData.downloadHandler.text;

        if(getFirestoreData.isNetworkError || getFirestoreData.isHttpError) 
        {
            Debug.Log(getFirestoreData.error);
        } else if(firestoreData != null){
            Debug.Log(firestoreData);
            CreateFromJSON(firestoreData);
        }
    }

    void CreateFromJSON(string firestoreData)
    {
        bool cakeValue = JsonUtility.FromJson<MyObjetcs>(firestoreData).cake;
        bool donutValue = JsonUtility.FromJson<MyObjetcs>(firestoreData).donut;
        bool hamburgerValue = JsonUtility.FromJson<MyObjetcs>(firestoreData).hamburger;
        bool icecreamValue = JsonUtility.FromJson<MyObjetcs>(firestoreData).icecream;

        ShowObjects(cakeValue, "Cake");
        ShowObjects(donutValue, "Donuts");
        ShowObjects(hamburgerValue, "Hamburger");
        ShowObjects(icecreamValue, "IceCream");
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
