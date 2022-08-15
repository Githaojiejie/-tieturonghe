

		
//--心心心心心心心心心心
//心心心心心心心心心心心心	
//-心心心心心心心心心心心
//---心心心心心心心心心
//------心心心心心心
//--------心心心心
//----------心心
//挂载:
//功能:
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.IO;
using UnityEditor;

public class DLVideoScript : MonoBehaviour
{
	public Button playerButton;
	public RawImage playerRawImage;
	public Slider playerSlider;
	MovieTexture m_MovieTexture;
	public AudioSource m_Audio;


	//文件存储路径
	private string pathFile;
	//文件名
	private  string fileName = "wiiu独占游戏僵尸U宣传片SDCC白金汉宫_高清";

	void Start ()
	{
		pathFile = Application.dataPath + "/Resources/" + fileName;
	}

	void Update ()
	{
		StartCoroutine ("PlayerMovie");
	}

	IEnumerator PlayerMovie ()
	{
		//while (m_MovieTexture == null) {
		m_MovieTexture = Resources.Load ("wiiu独占游戏僵尸U宣传片SDCC白金汉宫_高清") as MovieTexture;//后面不能+数据格式
		print ("文件加载找到,开始播放...");
		yield return m_MovieTexture;
		//}
		print ("1");
		playerRawImage.texture = m_MovieTexture;
		m_Audio.clip = m_MovieTexture.audioClip;
		m_MovieTexture.loop = true;
		m_Audio.loop = true;
	
		m_MovieTexture.Play ();
		m_Audio.Play ();
	
	
	}

}