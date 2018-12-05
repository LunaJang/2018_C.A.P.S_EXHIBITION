using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class NewBehaviourScript : MonoBehaviour {
     
	// Use this for initialization
	void Start () {
        StreamWriter outputFile = new StreamWriter("inputImage.txt");
	}
	
	// Update is called once per frame
	void Update () {
        // 이미지 데이터 저장 (이미지를 String 형태로 받아온다고 가정, image에 저장)
        string image;
        outputFile.WriteLine(line);

        // cmd 로 "python model.py" 명령어 전송
        Process cmd = new Process();
        cmd.StartInfo.FileName = "cmd.exe";
        cmd.StartInfo.RedirectStandardInput = true;
        cmd.StartInfo.RedirectStandardOutput = true;
        cmd.StartInfo.CreateNoWindow = true;
        cmd.StartInfo.UseShellExecute = false;
        cmd.Start();
        cmd.StandardInput.WriteLine("python model.py");
        cmd.StandardInput.Flush();
        cmd.StandardInput.Close();
        cmd.WaitForExit(1);

        // cmd로 출력된 결과물 읽어옴
        string result = cmd.StandardOutput.ReadToEnd();        
    }
}
