using UnityEngine;
using TMPro;

public class AlertIndicator : MonoBehaviour
{
    TextMeshPro tmp;
    Alertness alertness;
    
    void Start()
    {
        tmp = GetComponent<TextMeshPro>();
        alertness = GetComponentInParent<Alertness>();
    }

    // Update is called once per frame
    void Update()
    {
        if (alertness.alertLevel == 2)      tmp.text = "!!!";
        else if (alertness.alertLevel == 1) tmp.text = "???";
        else                                tmp.text = "...";
    }
}
