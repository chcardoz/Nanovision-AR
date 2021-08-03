using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnification : MonoBehaviour
{
	public Material m_Mat = null;
	public float Amount;
	public float RadiusX;
	public float RadiusY;
	public float ComplicatedRadiusInner = 0.3f;
	public float ComplicatedRadiusOuter = 0.6f;
	float m_MouseX;
	float m_MouseY;
	bool m_UseComplicated = false;
	bool m_UseMultiple = false;
	int m_GlassIndex = 0;

	/// <summary>
    /// Housekeeping and caching variables which put the magnification glass in the center of screen. 
    /// </summary>
	void Start()
	{
		QualitySettings.antiAliasing = 8;
		m_MouseX = m_MouseY = 0.5f;
		m_Mat.SetVector("_SimpleCenterRadial1", new Vector4(m_MouseX, m_MouseY, RadiusX, RadiusY));
		m_Mat.SetFloat("_SimpleAmount1", Amount);
	}

	void OnRenderImage(RenderTexture sourceTexture, RenderTexture destTexture)
	{
		// select which pass should we use
		int pass = 0;
		if (m_UseComplicated)
		{
			if (m_UseMultiple)
				pass = 3;
			else
				pass = 2;
		}
		else
		{
			if (m_UseMultiple)
				pass = 1;
			else
				pass = 0;
		}

		// fill material parameters
		int ind = m_GlassIndex;
		string simpleAmount = "_SimpleAmount" + (ind + 1);
		m_Mat.SetFloat(simpleAmount, Amount);
		string simpleCenterRadial = "_SimpleCenterRadial" + (ind + 1);
		m_Mat.SetVector(simpleCenterRadial, new Vector4(m_MouseX, m_MouseY, RadiusX, RadiusY));

		string complicatedAmount = "_ComplicatedAmount" + (ind + 1);
		m_Mat.SetFloat(complicatedAmount, Amount);
		string complicatedCenterRadial = "_ComplicatedCenterRadial" + (ind + 1);
		m_Mat.SetVector(complicatedCenterRadial, new Vector4(m_MouseX, m_MouseY, RadiusX, RadiusY));
		string complicatedRadiusInner = "_ComplicatedRadiusInner" + (ind + 1);
		m_Mat.SetFloat(complicatedRadiusInner, ComplicatedRadiusInner);
		string complicatedRadiusOuter = "_ComplicatedRadiusOuter" + (ind + 1);
		m_Mat.SetFloat(complicatedRadiusOuter, ComplicatedRadiusOuter);

		// let's draw it
		Graphics.Blit(sourceTexture, destTexture, m_Mat, pass);
	}

	public void increaseMagAmount()
    {
		float increasedVal = Amount + 0.01f;
		Amount = Mathf.Clamp(increasedVal, 0, 1);
    }

	public void decreaseMagAmount()
    {
		float increasedVal = Amount - 0.01f;
		Amount = Mathf.Clamp(increasedVal, 0, 1);
	}

}
