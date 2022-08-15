

		
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

public class movie : MonoBehaviour
{
	
	RawImage l_rawImage;
	//  2) 图片的网址
	string imgUrl;

	void Start ()
	{
		// 代码获取我们的RawImage
		l_rawImage =
			GameObject.Find ("RawImage").GetComponent<RawImage> ();
		// 网址赋值
		imgUrl = "http://g.hiphotos.baidu.com/image/h%3D360/sign=07ad353ef403738dc14a0a24831ab073/08f790529822720eb25fa86479cb0a46f31fab9f.jpg";
		//开启下载图片的协程。
		StartCoroutine (LoadImage ());

	}
	// 实现加载协程的方法
	IEnumerator LoadImage ()
	{
		WWW www = new WWW (imgUrl);
		yield return www;
		l_rawImage.texture = www.texture;

	}
	// Update is called once per frame
	void Update ()
	{
	
	}

}




