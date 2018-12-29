using System.IO;
using System.Collections.Generic;

namespace DM
{
	public class BaseTableManager<T,E> where T:new()
	{
		private static T instance = new T();
		protected List<string> linesList;
		public Dictionary<int,E> dataDic;

		public static T Instance {
			get {
				return instance;
			}
		}


		protected void ReadLines(string tableName)
		{
			linesList = new List<string> ();
			var fileStream = File.OpenRead (tableName);
			StreamReader sr = new StreamReader (fileStream);
			string line = sr.ReadLine ();//表头数据不进行保存
			line = sr.ReadLine ();
			while (line!=null) {
				linesList.Add (line);
				line = sr.ReadLine ();
			}
		}

		public E GetEmojiEntry(int id)
		{
			E entry = default(E);
			dataDic.TryGetValue (id, out entry);
			return entry;
		}

		public List<E> GetAllEmojiEntry()
		{
			List<E> entryList = new List<E> ();
			foreach (var item in dataDic.Values) {
				if (item != null)
					entryList.Add (item);
			}
			return entryList;

		}
	}
}

