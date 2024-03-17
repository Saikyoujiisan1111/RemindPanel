using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stageMaker : MonoBehaviour
{
    public GameObject normal;
    public GameObject jump;
    public GameObject none;
    public GameObject wall;
    public GameObject areaGoal;
    public GameObject warpA;
    public GameObject warpB;
    public GameObject lightMasu;
    public GameObject[] panels;

    public GameObject warpParticlePrehab;
    public float[] colorsHue;

    public GameObject startplace;
    public GameObject movePoint2;
    public GameObject light1;
    public GameObject light2;
    public GameObject light3;
    public GameObject character;
    public GameObject clearWall;//characterがステージ外に行かないように、後ろに配置する透明な壁です。
    //public int[,] panel;

    public float yokoHaba; //GameSceneCamera_moveで利用

    public TextAsset stageInfo;
    public TextAsset warpInfo;

    private int donoArea = 1;

    // Start is called before the first frame update
    void Start()
    {
        colorsHue = new float[] { 0.3f, 0.63f, 1, 0.16f, 0.5f, 0.83f, 0.16f, 0.73f };
        panels = new GameObject[]{ none, wall, jump, normal, areaGoal, warpA, warpB, lightMasu};

        string[] arrayStage = PickStageText(stageInfo);
        string[] arrayWarp = PickStageText(warpInfo);

        for (int zPos = 0; zPos < arrayStage.Length; zPos++)
        {
            for (int xPos = 0; xPos < arrayStage[zPos].Length; xPos++)
            {
                int num = int.Parse(arrayStage[zPos].Substring(xPos, 1));
                GameObject donoPanel = panels[num];
                GameObject obj = Instantiate(donoPanel, new Vector3(zPos, -0.5f, xPos), Quaternion.identity);

                if (donoPanel == areaGoal)
                {
                    donoArea++;
                }
                else if (donoPanel != wall)//どのエリアなのかの情報を付与
                {
                    obj.GetComponent<WhichAreaInfo>().whichArea = donoArea;
                }

                if(donoPanel == warpA || donoPanel == warpB)//ワープマスの設定
                {
                    //ワープマスのグループ分け
                    int warpGroup = int.Parse(arrayWarp[zPos].Substring(xPos, 1));
                    warpMasu script = obj.GetComponent<warpMasu>();
                    script.whichGroup = warpGroup;

                    //ワープマスのパーティクル設置
                    Vector3 objPos = obj.transform.position;
                    Vector3 ParticlePos = new Vector3(objPos.x, 0.03f, objPos.z);
                    Quaternion ParticleRot = Quaternion.Euler(-90, Random.Range(0, 360), 0);
                    GameObject warpParticle = Instantiate(warpParticlePrehab, ParticlePos, ParticleRot);

                    ParticleSystem.MainModule main = warpParticle.GetComponent<ParticleSystem>().main;
                    main.startColor = Color.HSVToRGB(colorsHue[warpGroup - 2], 0.7f, 0.3f);  //-2はwarpInfoで、0と1をワープ以外で使っているから
                    ParticleSystem.MainModule childmain = warpParticle.transform.GetChild(0).GetComponent<ParticleSystem>().main;
                    childmain.startColor = Color.HSVToRGB(colorsHue[warpGroup - 2], 0.9f, 0.4f);//ちゃんと色変わるの何で？
                }
            }
        }
        startplace = GameObject.Find("StartPlace");
        movePoint2 = GameObject.Find("movePoint2");
        float horizontalLength = arrayStage[0].Length - 1;
        yokoHaba = horizontalLength;
        horizontalLength /= 2f;

        character.transform.position = new Vector3(0, 0.5f, horizontalLength);
        clearWall.transform.position = new Vector3(- 1, 0.5f, horizontalLength);

        float CamXPos = 0;
        float Intensity = 0;
        if (arrayStage[0].Length - 2 == 3)//幅が3×3なら
        {
            CamXPos = 4.7f;
            Intensity = 4.5f;
        }
        if(arrayStage[0].Length - 2 == 5)//幅が5×5なら
        {
            CamXPos = 6.4f;
            Intensity = 7.3f;
        }
        if (arrayStage[0].Length - 2 == 7)//幅が7×7なら
        {
            CamXPos = 8.1f;
            Intensity = 12;
        }

        startplace.transform.position = new Vector3(-1f, -0.5f, horizontalLength);
        movePoint2.transform.position = new Vector3(CamXPos + startplace.transform.position.x, horizontalLength + 4, horizontalLength);

        light1.transform.position = new Vector3(horizontalLength, horizontalLength + 1, horizontalLength);
        light2.transform.position = new Vector3(horizontalLength + yokoHaba, horizontalLength + 1, horizontalLength);
        light3.transform.position = new Vector3(horizontalLength + 2*yokoHaba, horizontalLength + 1, horizontalLength);
        light1.GetComponent<Light>().intensity = Intensity;
        light2.GetComponent<Light>().intensity = Intensity;
        light3.GetComponent<Light>().intensity = Intensity;
    }

    string[] PickStageText(TextAsset InfoText)
    {
        int stagenum = crossSceneInfoManage.crossSceneInfo.whichStage;

        string stage = $"stage{stagenum}";
        int textstart = InfoText.text.IndexOf(stage);
        int textend = InfoText.text.IndexOf($"stage{stagenum + 1}");

        if (textend == -1)//次のステージがないなら。最後のステージなら。
        {
            textend = InfoText.text.Length + 1;//最後の文字を取得
        }

        string numtext = InfoText.text.Substring(textstart + stage.Length + 1, textend - textstart - stage.Length - 2);//+1,-2は改行の分
        string[] arrayform = numtext.Split('\n');

        return arrayform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /*stage0
11311
10301
10301
10301
11411
10301
10301
10301
11411
10301
10301
10301
11411*/
}
