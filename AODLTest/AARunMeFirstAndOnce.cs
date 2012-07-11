/*************************************************************************
 *
 * DO NOT ALTER OR REMOVE COPYRIGHT NOTICES OR THIS FILE HEADER
 * 
 * Copyright 2008 Sun Microsystems, Inc. All rights reserved.
 * 
 * Use is subject to license terms.
 * 
 * Licensed under the Apache License, Version 2.0 (the "License"); you may not
 * use this file except in compliance with the License. You may obtain a copy
 * of the License at http://www.apache.org/licenses/LICENSE-2.0. You can also
 * obtain a copy of the License at http://odftoolkit.org/docs/license.txt
 * 
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS, WITHOUT
 * WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * 
 * See the License for the specific language governing permissions and
 * limitations under the License.
 *
 ************************************************************************/

using System;
using NUnit.Framework;
using System.IO;

namespace AODLTest
{
	[TestFixture]
	public class AARunMeFirstAndOnce
	{
		private static string generatedFolder	= @"\\generatedfiles\\"; //System.Configuration.ConfigurationSettings.AppSettings["writefiles"];
		private static string readFromFolder	= @"\\files\\"; //System.Configuration.ConfigurationSettings.AppSettings["readfiles"];
		public static string outPutFolder		= Environment.CurrentDirectory+generatedFolder;
		public static string inPutFolder		= Environment.CurrentDirectory+readFromFolder;

		[Test]
		public void AARunMeFirstAndOnceDir()
		{
			if (Directory.Exists(outPutFolder))
				Directory.Delete(outPutFolder, true);
			Directory.CreateDirectory(outPutFolder);
		}
	}
}


/*
 * $Log: AARunMeFirstAndOnce.cs,v $
 * Revision 1.2  2008/04/29 15:39:58  mt
 * new copyright header
 *
 * Revision 1.1  2007/02/25 09:01:26  larsbehr
 * initial checkin, import from Sourceforge.net to OpenOffice.org
 *
 * Revision 1.2  2006/01/29 19:30:24  larsbm
 * - Added app config support for NUnit tests
 *
 * Revision 1.1  2006/01/29 11:26:02  larsbm
 * - Changes for the new version. 1.2. see next changelog for details
 *
 */