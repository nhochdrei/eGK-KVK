﻿using System.Windows;
using CardReader;
using CardReader.Results;
using Newtonsoft.Json;

namespace eGK
{
	class Program
	{
		static void Main()
		{
			using (CardTerminalClient cardTerminalClient = new CardTerminalClient("ctacs.dll"))
			{
				cardTerminalClient.ResetCT();

				string result = cardTerminalClient.RequestICC();
				if (result=="9000")
				{
					cardTerminalClient.SelectKVK();
					KvkResult kvkResult = cardTerminalClient.ReadKVK();

					string json = JsonConvert.SerializeObject(kvkResult);
					MessageBox.Show(json);
				}
				else
				{
					cardTerminalClient.SelectEGK();
					EgkResult egkResult = cardTerminalClient.ReadEGK();

					string json = JsonConvert.SerializeObject(egkResult);
					MessageBox.Show(json);
				}

				cardTerminalClient.EjectICC();
			}
		}
	}
}
