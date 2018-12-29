using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;
using DM;
using System.Linq;

/// <summary>
/// 
/// </summary>
public class Main : MonoBehaviour {
	[SerializeField]
	EmojiText.EmojiText testText;
	[SerializeField]
	GameObject emojiIconContent;
	[SerializeField]
	GameObject emojiGiftContent;
	[SerializeField]
	InputField messageInputField;
	[SerializeField]
	Button sendBtn;
	[SerializeField]
	Button showEmojiBtn;
	[SerializeField]
	Button showIconBtn;
	[SerializeField]
	Button fullScreenBtn;

	bool isShowEmojiIconPanel;
	bool isShowEmojiGiftPanel;


	void Start () {
		string path = Application.dataPath + "/Resources/Emoji/Config/emoji.tsv";
		EmojiTableManager.Instance.Initialize (path);
		sendBtn.onClick.AddListener (OnSend);
		showEmojiBtn.onClick.AddListener (OnShowEmojiGift);
		showIconBtn.onClick.AddListener (OnShowEmojiIcon);
		fullScreenBtn.onClick.AddListener (OnFullScreen);
		InitEmojiFaceCell(EmojiText.EmojiType.emoji,emojiGiftContent.transform);
		InitEmojiFaceCell(EmojiText.EmojiType.icon,emojiIconContent.transform);
		isShowEmojiIconPanel = false;
		isShowEmojiGiftPanel = false;


		testText.text = "本组件支持图片：<t=1,90001>，表情：<t=2,7>，按钮：<t=3,1>，<t=4,超链接,FFFF00>";
		testText.OnHyperlinkClick = () => {
			Debug.Log("设置超链接文本点击方法");
		};

	}


	void OnEmojiClick(string message){
		messageInputField.text = messageInputField.text+message;

	}

	void OnSend(){
		CloseAllEmojiPanel ();
		testText.text = reBuildMessage (messageInputField.text);
	}

	void OnShowEmojiIcon(){
		isShowEmojiIconPanel = true;
		isShowEmojiGiftPanel = false;
		UpdateEmojiPanelVisible ();
	}

	void OnShowEmojiGift(){
		isShowEmojiGiftPanel = true;
		isShowEmojiIconPanel = false;
		UpdateEmojiPanelVisible ();
	}

	void OnFullScreen()
	{
		CloseAllEmojiPanel ();
	}



	void InitEmojiFaceCell(EmojiText.EmojiType emojiType,Transform gobParent){
		string prefabPath = TestCell.GetPrefabPath ();
		var emojiEntryList = EmojiTableManager.Instance.GetAllEmojiEntry ();
		for (int i = 1; i < emojiEntryList.Count; i++) {
			if (emojiEntryList [i].Type == emojiType) {
				var prefab = Resources.Load<GameObject> (prefabPath);
				GameObject gob = Instantiate (prefab);
				var cell = gob.GetComponent<TestCell>();
				cell.transform.SetParent (gobParent,false);
				cell.Initialize (emojiEntryList[i],OnEmojiClick);
			}
		}
	}

	void CloseAllEmojiPanel()
	{
		isShowEmojiIconPanel = false;
		isShowEmojiGiftPanel = false;
		UpdateEmojiPanelVisible ();
	}

	void UpdateEmojiPanelVisible()
	{
		emojiIconContent.transform.parent.gameObject.SetActive (isShowEmojiIconPanel);
		emojiGiftContent.transform.parent.gameObject.SetActive (isShowEmojiGiftPanel);
	}


/// <summary>
/// 	对字符进行特殊处理
/// </summary>
	string reBuildMessage(string message){
		return message;
	}
}
