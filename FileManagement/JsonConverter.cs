using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace CirclePhysics.FileManagement
{
	public class JsonConverter
	{
		public T Deserialize<T>(string json)
		{
			return JsonConvert.DeserializeObject<T>(json);
		}

		public string Serialize<T>(T thing)
		{
			return JsonConvert.SerializeObject(thing);
		}
	}
}
