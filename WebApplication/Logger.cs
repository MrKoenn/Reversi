using System;
using System.IO;
using System.Web;

namespace WebApplication
{
	public static class Logger
	{
		private static StreamWriter writer;

		public static void Log(object message)
		{
			if (message == null) return;

			if (writer == null)
			{
				try
				{
					writer = new StreamWriter(@"D:\log.html");
					writer.WriteLine("<meta http-equiv=\"refresh\" content=\"1;url=file:///D:/log.html\" />");
				}
				catch
				{
					writer?.WriteLine("Logger already exists.");
				}
			}

			var time = DateTime.Now;
			writer?.WriteLine($"[{time:HH:mm:ss}] {HttpUtility.HtmlEncode(message)}<br/>");
			writer?.Flush();
		}
	}
}