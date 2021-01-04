using System.Collections;
using TMPro;
using UnityEngine;

public class SC_DebugHelper : MonoBehaviour {

    static SC_DebugHelper Instance;

    TextMeshProUGUI t;

    void Awake () {

        Instance = this;

        t = GameObject.FindWithTag ("Debugger").GetComponent<TextMeshProUGUI> ();

    }

    public static void Print (string debug) {

        Instance.StopCoroutine ("Clear");

        Instance.t.text = debug;

        print (debug);

        Instance.StartCoroutine ("Clear");

    }

    IEnumerator Clear () {

        yield return new WaitForSeconds (5);

        GameObject.FindWithTag ("Debugger").GetComponent<TextMeshProUGUI> ().text = "";

    }

}
