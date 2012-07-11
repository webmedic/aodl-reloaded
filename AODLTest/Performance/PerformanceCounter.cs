/*
 * Created by SharpDevelop.
 * User: darius.damalakas
 * Date: 2008-12-18
 * Time: 15:27
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

using System;

namespace AODLTest.Performance
{
	public class PerformanceCounter : IDisposable, IPerformanceCounter
	{
		private DateTime m_start = DateTime.MinValue;
		
		public PerformanceCounter()
		{
			this.m_start = DateTime.Now;
		}
		
		public int GetSeconds()
		{
			TimeSpan span = DateTime.Now - m_start;
			return span.Seconds;
		}
		
		public void Dispose()
		{
		}
	}
}
