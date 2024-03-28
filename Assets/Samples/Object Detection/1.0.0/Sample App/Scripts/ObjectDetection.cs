using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using Unity.Sentis;

namespace Sample
{
    public class ObjectDetection : MonoBehaviour
    {
        [SerializeField, Tooltip("Input Image")] private RenderTexture input_image = null;
        [SerializeField, Tooltip("Weights")] private ModelAsset weights = null;
        [SerializeField, Tooltip("Label List")] private TextAsset names = null;
        [SerializeField, Tooltip("Confidence Score Threshold"), Range(0.0f, 1.0f)] private float score_threshold = 0.6f;
        [SerializeField, Tooltip("IoU Threshold"), Range(0.0f, 1.0f)] private float iou_threshold = 0.4f;
        [SerializeField] private ImageController imageController;

        private HoloLab.DNN.ObjectDetection.ObjectDetectionModel_YOLOX model;
        private Font font;
        private Color color_offset;
        private List<Color> colors;
        private List<string> labels;
        
        private float Interval = 0.2f;
        private float time = 0.0f;
        private Rect detectedObjectRect = new Rect(100, 100, 200, 100);

        private void Start()
        {
            // Create Object Detection Model
            model = new HoloLab.DNN.ObjectDetection.ObjectDetectionModel_YOLOX(weights);

            // Read Label List from Text Asset
            labels = new List<string>(Regex.Split(names.text, "\r\n|\r|\n"));

            // Create Visualizer and Colors
            try
            {
                font = Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font;
            }
            catch
            {
                font = Resources.GetBuiltinResource(typeof(Font), "LegacyRuntime.ttf") as Font;
            }
            colors = HoloLab.DNN.ObjectDetection.Visualizer.GenerateRandomColors(labels.Count);
            color_offset = new Color(0.5f, 0.5f, 0.5f, 0.0f);
        }

        private void Update()
        {
            //1秒おきに物体検出を実行
            time += Time.deltaTime;
            if (time > Interval)
            {
                time = 0.0f;
                OnDetect();
            }
        }

        public void OnDetect()
        {
            // Get Texture from Raw Image
            var input_texture = ConvertToTexture2D(input_image);
            if (input_texture == null)
            {
                Debug.LogError("Input Texture is null.");
                return;
            }

            // Detect Objects
            var objects = model.Detect(input_texture, score_threshold, iou_threshold);

            // Show Objects on Unity Console
            objects.ForEach(o => Debug.Log($"{o.class_id} {labels[o.class_id]} ({o.score:F2}) : {o.rect}"));
            
            detectedObjectRect = objects[0].rect;

            imageController.SetImageToPeople(detectedObjectRect.x, detectedObjectRect.y, detectedObjectRect.width,
                detectedObjectRect.height);

            // Draw Objects on Unity UI
            //HoloLab.DNN.ObjectDetection.Visualizer.ClearBoundingBoxes(input_texture);
            //objects.ForEach(o => HoloLab.DNN.ObjectDetection.Visualizer.DrawBoudingBox(input_texture, o.rect, colors[o.class_id]));

            //HoloLab.DNN.ObjectDetection.Visualizer.ClearLabels(input_texture);
            //objects.ForEach(o => HoloLab.DNN.ObjectDetection.Visualizer.DrawLabel(input_texture, o.rect, colors[o.class_id] - color_offset, $"{labels[o.class_id]} ({o.score:F2})", font));

        }
        
        private Texture2D ConvertToTexture2D(RenderTexture rTex)
        {
            Texture2D texture = new Texture2D(rTex.width, rTex.height, TextureFormat.RGBA32, false);
            RenderTexture.active = rTex;
            texture.ReadPixels(new Rect(0, 0, rTex.width, rTex.height), 0, 0);
            texture.Apply();
            RenderTexture.active = null; // レンダリングを元の状態に戻します
            return texture;
        }
        
        private void OnGUI()
        {
            // 四角形を描画するためのスタイルを定義
            GUIStyle boxStyle = new GUIStyle(GUI.skin.box);
            boxStyle.border = new RectOffset(2, 2, 2, 2); // 枠線の太さを設定
            boxStyle.normal.textColor = Color.red; // 文字色
            boxStyle.normal.background = MakeTex(2, 2, new Color(0f, 0f, 0f, 0f)); // 背景色（透明）

            // 四角形を描画
            GUI.Box(new Rect(detectedObjectRect.x, Screen.height - detectedObjectRect.y - detectedObjectRect.height, detectedObjectRect.width, detectedObjectRect.height), $"0 person (0.80)", boxStyle);
        }
        
        // 一色のテクスチャを生成する補助関数
        private Texture2D MakeTex(int width, int height, Color col)
        {
            Color[] pix = new Color[width * height];
            for (int i = 0; i < pix.Length; i++)
                pix[i] = col;

            Texture2D result = new Texture2D(width, height);
            result.SetPixels(pix);
            result.Apply();

            return result;
        }
        
        private void OnDestroy()
        {
            model?.Dispose();
            model = null;
        }
    }
}
