//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré par un outil.
//     Version du runtime :4.0.30319.34014
//
//     Les modifications apportées à ce fichier peuvent provoquer un comportement incorrect et seront perdues si
//     le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------
using System;
using UnityEngine;
using AssemblyCSharp;

//bubble type
public enum BubbleType{Rectangle, Round};

namespace AssemblyCSharp
{
	[System.Serializable]
	public class PixelBubble
	{
		//main message in the bubble
		public string vMessage = ""; 
		public BubbleType vMessageForm = BubbleType.Rectangle;
		public Color vBorderColor = Color.black;
		public Color vBodyColor = Color.white;
		public Color vFontColor = Color.black;
		public bool vClickToCloseBubble = false;
	}
}


