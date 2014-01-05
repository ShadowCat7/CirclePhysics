using Newtonsoft.Json;

namespace CirclePhysics.FileManagement
{
	public static class JsonConverter
	{
		public static T Deserialize<T>(string json)
			where T : class
		{
			try
			{
				return JsonConvert.DeserializeObject<T>(json);
			}
			catch
			{
				return null;
			}
		}

		public static string Serialize<T>(T thing)
		{
			return JsonConvert.SerializeObject(thing);
		}
	}
}
