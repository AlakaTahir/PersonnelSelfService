using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PersonnelSelfService.Core
{
	public class Utility: IUtility
	{
		private static string logPath = string.Format(@"C:\Users\CORE I5\Documents\Log", AppDomain.CurrentDomain.BaseDirectory);

		public Utility() //sample done on authservice controller login endpoint
		{
			if (!Directory.Exists(logPath))
			{
				Directory.CreateDirectory(logPath);
			}
		}

		public string Log(string content, string type = "Error")
		{
			string logfile = string.Format(@"{0}\{1}Log-" + DateTime.Now.ToString("dd-MM-yyyy") + ".log", logPath, type);
			//List<string[]> values = new List<string[]>();

			//write new log
			using (StreamWriter writer = new StreamWriter(logfile, true))
			{
				writer.WriteLine("\r" + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt"));
				writer.WriteLine(content);
				writer.WriteLine("\n\r");
				writer.Close();
				using (StreamReader reader = new StreamReader(logfile, true))
				{
					//while (reader.Peek() >= 0)
					//{
					string value = reader.ReadToEnd();
					return value;
					//}
				}
			}
		}
	}
}
