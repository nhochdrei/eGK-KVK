﻿using System;
using CardReader;
using Newtonsoft.Json;

namespace eGK
{
	class Program
	{
		static void Main()
		{
			CardResult result = new CardResult();

			using (CardTerminalClient cardTerminalClient = new CardTerminalClient("ctacs.dll"))
			{
				cardTerminalClient.ResetCT();
				cardTerminalClient.RequestICC();

				try
				{
					cardTerminalClient.SelectEGK();
					result.EgkResult = cardTerminalClient.ReadEGK();
				}
				catch { }

				try
				{
					cardTerminalClient.SelectKVK();
					result.KvKResult = cardTerminalClient.ReadKVK();
				}
				catch { }

				cardTerminalClient.EjectICC();
			}

			string json = JsonConvert.SerializeObject(result,Formatting.Indented);
			Console.WriteLine(json);

			Console.ReadKey();
		}
	}
}
