using System;
using EmojiText;
using System.Collections.Generic;
using System.IO;

namespace DM
{
	public class EmojiTableManager:BaseTableManager<EmojiTableManager,EmojiTableManager.EmojiEntry>
	{
		public class EmojiEntry
		{
			public int Id{ get; private set;}
			public EmojiType Type{ get; private set;}
			public string Name{ get; private set;}
			public float Width{ get; private set;}
			public float Height{ get; private set;}
			public float Ratio{
				get{ 
					return Width / Height;
				}
			}

			public EmojiEntry(string[] rowArray){
				Id = Convert.ToInt32(rowArray[0]);
				Type = (EmojiType)Convert.ToInt32(rowArray[1]);
				Name = rowArray[2];
				Width = float.Parse(rowArray[3]);
				Height = float.Parse(rowArray[4]);
			}

		}







		public void Initialize(string tableName){
			dataDic = new Dictionary<int, EmojiEntry> ();
			ReadLines (tableName);
			for (int i = 0; i < linesList.Count; i++) {
				var item = linesList [i].Split('\t');
				if (item.Length == 5) {
					EmojiEntry table = new EmojiEntry (item);
					dataDic.Add (table.Id,table);
				}
			}
		}



		public bool Contains(int id,EmojiType type)
		{
			if(dataDic.ContainsKey(id)){
				if (dataDic [id] != null && dataDic [id].Type == type)
					return true;
			}

			return false;
		}

	}
}

