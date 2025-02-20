﻿using System;
using System.IO;

namespace DexOfCards.Utilities;

public static class FilePaths
{
#if !DEBUG
    public static readonly string Resources = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly()?.Location)!, "Resources");
    public static readonly string WwwRoot = Path.GetDirectoryName(Assembly.GetEntryAssembly()?.Location, "wwwroot");
#else
    public static readonly string Resources = Path.Combine(Path.Combine("..\\..\\..\\..\\"), "Resources");
    public static readonly string WwwRoot = Path.Combine("..\\..\\..\\..\\", "wwwroot");
#endif

    public static readonly string AppData = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "DexOfCards");
}