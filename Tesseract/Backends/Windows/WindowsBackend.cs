using System;
using Tesseract.Theming;

namespace Tesseract.Backends.Windows
{
	public class WindowsBackend: IBackend
	{
		WindowsWindow mainWindow;
		
		public WindowsBackend()
		{
		}
		
		public bool CanUse()
		{
			return true; // All current mainstream .net platforms have winforms
			
			//return (Environment.OSVersion.Platform != System.PlatformID.Unix);
		}
	
		public void Init()
		{
            System.Windows.Forms.Application.EnableVisualStyles();
		}
		
		public IWindow CreateWindow()
		{
			return new WindowsWindow();
		}
		
		public void Run(IWindow w)
		{
			mainWindow = (WindowsWindow)w;
			
			done = false;
			System.Windows.Forms.Application.Run(mainWindow);
		}
		
		bool done;
		public void Done()
		{
			if (!done)
				System.Windows.Forms.Application.Exit();
			
			done = true;
		}
		
		public IGraphics InternalGraphics()
		{
			return new WindowsGraphics();
		}
	}
}
