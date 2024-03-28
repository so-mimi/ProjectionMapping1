using UnityEngine;
using UnityEngine.UI;

public class WebCam : MonoBehaviour
{
    public int index;
    public RenderTexture targetTexture;
    //カメラの解像度，FPS
    public int width = 1920, height = 1080, fps = 30;
    int prevIndex;

    WebCamTexture webcamTexture;

    void Start () {
        prevIndex = index;
        SetWebCamTexture(index);
    }
	
    void Update () {
        if(index != prevIndex)
        {
            //利用可能だが，処理落ちするためコメントアウト
            //SetWebCamTexture(index);
        }
        //テクスチャをコピー
        Graphics.Blit(webcamTexture, targetTexture);

        prevIndex = index;
    }

    void SetWebCamTexture(int index)
    {
        if(webcamTexture != null && webcamTexture.isPlaying)
            webcamTexture.Stop();
        WebCamDevice[] devices = WebCamTexture.devices;
        try
        {
            webcamTexture = new WebCamTexture(devices[index].name, this.width, this.height, this.fps);
        }catch(System.Exception e)
        {
            webcamTexture = new WebCamTexture(devices[0].name, this.width, this.height, this.fps);
        }
        webcamTexture.Play();
    }
 
    //解像度を設定
    public void SetResolution(int w, int h)
    {
        width = w;
        height = h;
    }
}
