using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloLab.DNN.ObjectDetection;

public class Test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var model = new ObjectDetectionModel_YOLOX("./weights.onnx");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
