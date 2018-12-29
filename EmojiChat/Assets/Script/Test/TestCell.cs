using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using EmojiText;

public class TestCell : MonoBehaviour {

	[SerializeField]
	Button btn;
	[SerializeField]
	GameObject emojiContainer;
	[SerializeField]
	Image normalImage;

	int emojiId;
	EmojiType type;

	public static string GetPrefabPath()
	{
		return "Prefabs/Common/EmojiCell";
	}

	public void Initialize(DM.EmojiTableManager.EmojiEntry entry, Action<string> OnEmojiClick){
		this.emojiId = entry.Id;
		this.type = entry.Type;
		string path = GetEmojiPrefabPath ();
		Debug.Log (path);


		btn.onClick.AddListener (()=>OnEmojiClick(GetEmojiDescription()));
	}

	string GetEmojiDescription()
	{
		return string.Format("<t={0},{1}>",Convert.ToInt32(type),emojiId);
	}

	string GetEmojiPrefabPath(){
		string path = string.Empty;
		switch (type) {
		case EmojiType.emoji:
			path = Config.EmojiFacePrefabPath + emojiId;
			GameObject prefab = Resources.Load<GameObject> (path);
			GameObject gob = Instantiate (prefab);
			gob.GetComponent<RectTransform> ().anchoredPosition = Vector2.zero;//由于预设的位置不是在（0，0），这里做简单调整
			gob.transform.SetParent (emojiContainer.transform, false);
			normalImage.gameObject.SetActive (false);
			break;
		case EmojiType.icon:
			path = Config.EmojiNormalIconPath + emojiId;
			normalImage.sprite = Resources.Load<Sprite> (path);
			normalImage.gameObject.SetActive (true);
			break;
		}
		return path;
	}
		
}
