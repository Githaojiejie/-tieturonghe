


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
using System.Collections.Generic;

public class tietu : MonoBehaviour
{
	//原来的墙壁纹理
	public Texture2D m_oldtex;
	//新建的墙壁纹理图片
	public Texture2D m_newtex;
	//子弹纹理图片
	public Texture2D m_bullet;

	float m_wallwidth;
	float m_wallhight;

	float m_bulletwidth;
	float m_bullethight;
	// Use this for initialization
	void Awake ()
	{
		//原来的墙壁纹理 为保证以后修改了纹理还能还原 备份一下

		m_oldtex = GetComponent<MeshRenderer> ().material.mainTexture as Texture2D;
		m_newtex = Instantiate (m_oldtex);
		//现在使用备份的纹理图片 这样就算修改也只是修改备份;
		GetComponent<MeshRenderer> ().material.mainTexture = m_newtex;


		//拿到墙壁纹理和子弹纹理的宽高;
		m_wallwidth = m_newtex.width;
		m_wallhight = m_newtex.height;


		m_bulletwidth = m_bullet.width;
		m_bullethight = m_bullet.height;
	}
	//鼠标点击位置的到的UV坐标地  使用队列
	Queue<Vector2 > uvque = new Queue<Vector2> ();


	// Update is called once per frame
	void Update ()
	{

		//鼠标射线
		if (Input.GetMouseButton (0)) {

			Ray m_ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit m_hit;
			if (Physics.Raycast (m_ray, out m_hit)) {
				if (m_hit.collider.gameObject.name == "Plane") {

					Vector2 uv = m_hit.textureCoord;
				
					uvque.Enqueue (uv);


					//实现将子弹纹理图片和墙壁纹理图片融合;
					//shizhi 实质遍历像素点.
					for (int i = 0; i < m_bulletwidth; i++) {
						for (int j = 0; j < m_bullethight; j++) {
							//要想遍历所有子弹纹理图片的区域像素点  我们从UV原点开始扫描;
							//点击是墙  uv*墙的宽高 拿到的就是鼠标在墙上位置的UV坐标
							float w =	(uv.x * m_wallwidth - m_bulletwidth / 2) + i;
							float h = (uv.y * m_wallhight - m_bullethight / 2) + j;
							//得到子弹纹理中所有的像素
							Color bulletpixles = m_bullet.GetPixel (i, j);
							//得到墙壁纹理中子弹纹理大小的像素
							Color walletpixles = m_newtex.GetPixel ((int)w, (int)h);
							m_newtex.SetPixel ((int)w, (int)h, bulletpixles * walletpixles);
						}
					}
					//queren 修改完毕 提交修改
					m_newtex.Apply ();
					//	Invoke ("ReBack", 2f);
				}
			}
		}

	}

	void ReBack ()
	{
		Vector2 uv = uvque.Dequeue ();
		for (int i = 0; i < m_bulletwidth; i++) {
			for (int j = 0; j < m_bullethight; j++) {
				//要想遍历所有子弹纹理图片的区域像素点  我们从UV原点开始扫描;
				//点击是墙  uv*墙的宽高 拿到的就是鼠标在墙上位置的UV坐标
				float w =	(uv.x * m_wallwidth - m_bulletwidth / 2) + i;
				float h = (uv.y * m_wallhight - m_bullethight / 2) + j;
				Color walletpixles = m_oldtex.GetPixel ((int)w, (int)h);
				m_newtex.SetPixel ((int)w, (int)h, walletpixles);
			}
		}
		m_newtex.Apply ();
	}
}
