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
	public interface IPerformanceCounter : IDisposable
	{
		int GetSeconds();
	}
}
