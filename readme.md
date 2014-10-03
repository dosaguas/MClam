SharpClam
===================

A tiny libclamav bindings fro VB.NET and C#.

-------------
How To Use
-------------

Simply, just add reference to SharpClam, and use these lines of code :

> **VB.NET** 

> Imports SharpClam
> 
> Public Sub ScanFile()
> Dim clamEngine As New ManagedClam
> 
> 'Before any call, we must specify the ClamAV database directory.
> 'The directory must have "main.cvd" and "daily.cvd" inside.
>clamEngine.DatabasePath = "D:\database" 
> 
> 'First we have to initialize the engine
> Dim loadedSigs As UInteger = 0
> clamEngine.LoadClamAV(DatabaseOptions.CL_DB_STDOPT, ManagedClam.CL_INIT_STDOPT, loadedSigs)
> 
> 'Now we're ready to start scan files.
> Dim result As ScanResult = clamEngine.ScanFile("D:\file.exe", ScanOptions.CL_SCAN_STDOPT)
> MsgBox(result.VirusName) 'Display a message box
> 
>'After all calls, and we does not need to use the engine anymore, we must release memory allocated for the engine.
>clamEngine.FreeMemory()
>End Sub 

I think it's just same source code in C#. The logic is :

1. Load ClamAV engine (Initialize it, load the database and then compile it).
2. Start scan a file.
3. Release allocated memory.


----------

TODO List and Enchancement
--------------------------------------
Because of this commit is still in Alpha version, some enchacement is required.

1. Analyze and track memory leaking.
2. Optimize P/Invoke to libclamav binary.
3. Release new feature to complete all ClamAV API, because some function in this commit is still not Implemented.
4. Any other bug fixes.


----------

License
---------
This library is licensed under [LGPL v3.0](www%E2%80%8B.gnu.org/licenses/lgpl.html "LGPL v3.0 License").


----------
About Me
-------------

My name is Fahmi Noor Fiqri, from Bogor, West Java,  Indonesia. See my homepage in : [Cyber Tech For Us](http://www.cyber-tech4us.tk "Cyber Tech For Us").

Sorry for my bad english. I'm don't using any translator software.