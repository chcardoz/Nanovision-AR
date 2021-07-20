using UnityEngine;
using UnityEngine.UI;

namespace MagnifyingGlassNew
{
	public class Demo2D : MonoBehaviour
	{
		public SpriteRenderer m_Sprite;
		public Image m_Image;
		Vector4 m_SprCenterRadial;
		Vector4 m_ImgCenterRadial;

		void Start()
		{
			m_SprCenterRadial = m_Sprite.material.GetVector("_ComplicatedCenterRadial1");
			m_ImgCenterRadial = m_Image.material.GetVector("_ComplicatedCenterRadial1");
		}
		void Update()
		{
			// sprite
			float speed = 0.005f;
			if (Input.GetKey(KeyCode.W))
			{
				m_SprCenterRadial.y += speed * 2;
				m_Sprite.material.SetVector("_ComplicatedCenterRadial1", m_SprCenterRadial);
			}
			if (Input.GetKey(KeyCode.S))
			{
				m_SprCenterRadial.y -= speed * 2;
				m_Sprite.material.SetVector("_ComplicatedCenterRadial1", m_SprCenterRadial);
			}
			if (Input.GetKey(KeyCode.A))
			{
				m_SprCenterRadial.x -= speed;
				m_Sprite.material.SetVector("_ComplicatedCenterRadial1", m_SprCenterRadial);
			}
			if (Input.GetKey(KeyCode.D))
			{
				m_SprCenterRadial.x += speed;
				m_Sprite.material.SetVector("_ComplicatedCenterRadial1", m_SprCenterRadial);
			}

			// image
			if (Input.GetKey(KeyCode.UpArrow))
			{
				m_ImgCenterRadial.y += speed * 2;
				m_Image.material.SetVector("_ComplicatedCenterRadial1", m_ImgCenterRadial);
			}
			if (Input.GetKey(KeyCode.DownArrow))
			{
				m_ImgCenterRadial.y -= speed * 2;
				m_Image.material.SetVector("_ComplicatedCenterRadial1", m_ImgCenterRadial);
			}
			if (Input.GetKey(KeyCode.RightArrow))
			{
				m_ImgCenterRadial.x += speed;
				m_Image.material.SetVector("_ComplicatedCenterRadial1", m_ImgCenterRadial);
			}
			if (Input.GetKey(KeyCode.LeftArrow))
			{
				m_ImgCenterRadial.x -= speed;
				m_Image.material.SetVector("_ComplicatedCenterRadial1", m_ImgCenterRadial);
			}
		}
	}
}