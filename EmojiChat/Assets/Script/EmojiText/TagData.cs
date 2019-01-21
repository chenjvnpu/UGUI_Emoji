using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using DM;

namespace EmojiText
{
    public class TagData
    {
        public const float ICON_SCALE = 1.5f;
        public const float EMOJI_SCALE = 1.5f;
        public const float BUTTON_SCALE = 2f;

        public EmojiType Type;
        public int Id;
        public int Length;
        public string PopulateText;//填充文本
        public Vector3 StartPosition;
        public float Width;
        public float Height;
        public int Size;
        public bool Valid = false;//在显示范围内的
		public bool ParseAble = true; //能否转换成富文本

        private int _startIndex;
        private List<Vector4> _boundList;
        public List<Vector4> boundList
        {
            get
            {
                return _boundList;
            }
        }

		/// <summary>
		/// 通过文字的size，计算图片的尺寸，这个尺寸这里写的是固定
		/// </summary>
        public TagData(string param, int size)
        {
            string[] splitArray = param.Split(',');
            this.Type = (EmojiType)int.Parse(splitArray[0]);
            this.Size = size;
            switch (this.Type)
            {
			case EmojiType.icon:
					this.Id = int.Parse (splitArray [1]);
					SetEmojiPopulateText (ICON_SCALE);
                    break;
			case EmojiType.emoji:
					this.Id = int.Parse (splitArray [1]);
					SetEmojiPopulateText (EMOJI_SCALE);
                    break;
                case EmojiType.button:
                    this.Id = int.Parse(splitArray[1]);
                    PopulateText = string.Format("<quad Size={0}, Width={1}>", size.ToString(), BUTTON_SCALE.ToString());
                    Width = size * 2;
                    Height = size;
                    break;
                case EmojiType.hyperlink:
                    PopulateText = string.Format("<color=#{1}>{0}</color>", splitArray[1], splitArray[2]);
                    break;
            }
			this.Length = PopulateText==null ? 0:PopulateText.Length;
        }



        public void SetStartIndex(int index)
        {
            _startIndex = index;
        }

        public int GetEndIndex()
        {
            return _startIndex + this.Length;
        }

        public string GetPrefabPath()
        {
            string result = string.Empty;
            switch (this.Type)
            {
			case EmojiType.icon:
					result = Config.EmojiIconPrefabPath;//"Emoji/Prefab/Image";
                    break;
			case EmojiType.emoji:
					result = string.Format ("{0}{1}", Config.EmojiFacePrefabPath,this.Id.ToString());// string.Format("Emoji/Face/Face_{0}", this.Id.ToString());
                    break;
                case EmojiType.button:
					result =  Config.EmojiButtonPrefabPath;//"Emoji/Prefab/Button";
                    break;
                case EmojiType.hyperlink:
					result = Config.HyperLinkPrefabPath;// "Emoji/Prefab/DummyImage";
                    break;
            }
            return result;
        }

        public string GetIconPath()
        {
            string result = string.Empty;
            switch (this.Type)
            {
                case EmojiType.icon:
						result = string.Format("{0}{1}",Config.EmojiNormalIconPath, this.Id.ToString());
                    break;
                case EmojiType.button:
						result = string.Format("{0}{1}",Config.EmojiButtonIconPath, this.Id.ToString());
                    break;
                default:
                    Debug.LogError("找不到类型:" + this.Type.ToString());
                    break;
            }
            return result;
        }

        public void SetStartPosition(Vector3 position)
        {
            float offsetY = (this.Height - this.Size) / 2f + 2; //2为固定偏移值 可以根据项目情况微调
            position.Set(position.x, position.y - offsetY, position.z);
            StartPosition = position;
        }

        public bool UseQuad()
        {
            return this.Type != EmojiType.hyperlink;
        }

        public void SetValid(bool valid)
        {
            this.Valid = valid;
        }

        public void AddBound(Vector4 bound)
        {
            if (_boundList == null)
            {
                _boundList = new List<Vector4>();
            }
            _boundList.Add(bound);
        }

        public void ClearBound()
        {
            if (_boundList != null)
            {
                _boundList.Clear();
            }
        }

		void SetEmojiPopulateText(float baseScale){
			if (EmojiTableManager.Instance.Contains (Id, Type)) {
				var emojiTable = EmojiTableManager.Instance.GetEmojiEntry (Id);
				if (emojiTable != null) {
					Height = Size * baseScale;
					Width = Size * baseScale*emojiTable.Ratio;
					PopulateText = string.Format ("<quad Size={0} Width={1}>", Size.ToString (), (baseScale*emojiTable.Ratio).ToString ());
				}else
					ParseAble = false;

			} else {
				ParseAble = false;
			}
		}


    }
}