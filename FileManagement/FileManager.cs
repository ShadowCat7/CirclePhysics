using System.IO;
using CirclePhysics.FileManagement;

namespace CirclePhysics.FileManagement
{
	public static class FileManager
	{
		private const string GAME_DATA_PATH = "/game-data/";
		private const string SAVE_DATA_PATH = "/save-data/";

		public static TGameData LoadGameData<TGameData>(string fileName)
			where TGameData : class, IGameData
		{
			using (StreamReader reader = new StreamReader(File.OpenRead(GAME_DATA_PATH + fileName)))
			{
				return JsonConverter.Deserialize<TGameData>(reader.ReadToEnd());
			}
		}

		public static TSaveData LoadSaveData<TSaveData>(string fileName, TSaveData saveData)
			where TSaveData : class, ISaveData
		{
			using (StreamReader reader = new StreamReader(File.OpenRead(SAVE_DATA_PATH + fileName)))
			{
				return JsonConverter.Deserialize<TSaveData>(reader.ReadToEnd());
			}
		}

		public static void SaveData<TSaveData>(string fileName, TSaveData saveData)
			where TSaveData : class, ISaveData
		{
			using (StreamWriter writer = new StreamWriter(File.OpenWrite(SAVE_DATA_PATH + fileName)))
			{
				writer.AutoFlush = true;
				writer.Write(JsonConverter.Serialize<TSaveData>(saveData));
			}
		}
	}
}
