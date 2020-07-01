using UnityEngine;
using System.Runtime.InteropServices;
using UnityEngine.UI;

public class Link : MonoBehaviour 
{

	public void OpenAboutPage()
	{
		#if !UNITY_EDITOR
		openWindow("https://arthurabeshouse.github.io/Android-Evacuation/");
		#endif
	}

	[DllImport("__Internal")]
	private static extern void openWindow(string url);

}