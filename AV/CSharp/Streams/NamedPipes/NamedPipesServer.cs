/***********************************
Author:			Duane Billue
Date: 			20190610
Description:	Named Pipes Server
File Name:		NamedPipesServer.cs
***********************************/
using System;
using System.IO;
using System.IO.Pipes;
using System.Text;
using System.Threading;

namespace NamedPipesServert
{
	public class Program
	{
		private static int numThreads = 4;
		
		public static void Main(string[] args)
		{
			int i;
			Thread[] servers = new Thread[numThreads];
			
			// Debug.
			// Console.WriteLine("NamedPipesServer.cs :: Line 26");
			
			// Debug.
			Console.WriteLine("Named Pipes Server Begin: {0}", System.DateTime.Now.ToString());
			
			// Waiting for connections.
			Console.WriteLine("Named server pipe waiting for client connection(s).");
			
			for(i = 0; i < numThreads; i++)
			{
				servers[i] = new Thread(ServerThread);
				servers[i].Start();
			}
			Thread.Sleep(250); // ms
			
			// Ensure thread count is decremented task completed.
			while (i > 0)
			{
				for (int j = 0; j < numThreads; j++)
				{
					if (servers[j] != null)
					{
						if (servers[j].Join(250))
						{
							Console.WriteLine("Server thread[{0}] finished.", servers[j].ManagedThreadId);
							servers[j] = null;
							i--;    // decrement the thread watch count
						}
					}
				}
			}
			
			// Thread ceiling count reached.  Terminate program.
			Console.WriteLine("\nServer threads exhausted, exiting.");
			
			// Debug.
			Console.WriteLine("Named Pipes Server End: {0}", System.DateTime.Now.ToString());			
		}
		
		private static void ServerThread(object data)
		{
			NamedPipeServerStream pipeServer = 
				new  NamedPipeServerStream("testpipe", PipeDirection.InOut, numThreads);
			
			int threadId = Thread.CurrentThread.ManagedThreadId;
			
			// Wait for incoming connections.
			pipeServer.WaitForConnection();
			
			// Connection attempted.
			Console.WriteLine($"Client pipe connecting  on thread {threadId}.");
			try
			{
				// Create StreamString object.
				StreamString ss = new StreamString(pipeServer);
				
				ss.WriteString("Delphi");
				string filename = ss.ReadString();
				
				// Read file.
				ReadFileToStream fileReader = new ReadFileToStream(ss, filename);
				
			    Console.WriteLine("Reading file: {0} on thread[{1}] as user: {2}.",
							filename, 
							threadId, 
							pipeServer.GetImpersonationUserName());
							
				// Run the NamedPipeServerStream as a client (connection success).
				pipeServer.RunAsClient(fileReader.Start);
			}
			catch (IOException e)
			{
				Console.WriteLine("ERROR: {0}", e.Message);
			}
			pipeServer.Close();
		}
	}
	
	public class StreamString
	{
		private Stream ioStream;
		private UnicodeEncoding streamEncoding;
		
		public StreamString(Stream ioStream)
		{
			this.ioStream = ioStream;
			streamEncoding = new UnicodeEncoding();
		}
		
		public string ReadString()
		{
			int len = 0;

			len = ioStream.ReadByte() * 256;
			len += ioStream.ReadByte();
			byte[] inBuffer = new byte[len];
			ioStream.Read(inBuffer, 0, len);

			return streamEncoding.GetString(inBuffer);
		}
		
		public int WriteString(string outString)
		{
			byte[] outBuffer = streamEncoding.GetBytes(outString);
			int len = outBuffer.Length;
			if (len > UInt16.MaxValue)
			{
				len = (int)UInt16.MaxValue;
			}
			ioStream.WriteByte((byte)(len / 256));
			ioStream.WriteByte((byte)(len & 255));
			ioStream.Write(outBuffer, 0, len);
			ioStream.Flush();

			return outBuffer.Length + 2;
		}
	}
		
	// Contains the method executed in the context of the impersonated user
	public class ReadFileToStream
	{
		private string fn;
		private StreamString ss;

		public ReadFileToStream(StreamString str, string filename)
		{
			fn = filename;
			ss = str;
		}

		public void Start()
		{
			string contents = File.ReadAllText(fn);
			ss.WriteString(contents);
		}
	}
}
