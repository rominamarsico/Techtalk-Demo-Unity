using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GameManager : MonoBehaviour
{
    public GameObject cube;
    public GameObject sphere;

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

    // Update is called once per frame
    void Update()
    {
    }

    IEnumerator GetFirestoreValues() 
    {
        UnityWebRequest getFirestoreData = new UnityWebRequest("https://us-central1-techtalk-demo-6ecbb.cloudfunctions.net/readFromFirestore");
        // UnityWebRequest getFirestoreData = new UnityWebRequest("http://localhost:5000/techtalk-demo-6ecbb/us-central1/readFromFirestore");
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
        Debug.Log(JsonUtility.FromJson<MyObjetcs>(firestoreData).donut);
    }
}
