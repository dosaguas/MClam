using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.ComponentModel;

namespace SharpClam
{

#region  Enums 
/// <summary>
/// ClamAV engine scan options.
/// </summary>
/// <remarks></remarks>
[Flags()]
public enum ScanOptions
{
	/// <summary>
	/// Use it alone if you want to disable support for special files.
	/// </summary>
	/// <remarks></remarks>
	CL_SCAN_RAW = 0,
	/// <summary>
	/// This flag enables transparent scanning of various archive formats.
	/// </summary>
	/// <remarks></remarks>
	CL_SCAN_ARCHIVE = 1,
	/// <summary>
	/// Enable support for mail files.
	/// </summary>
	/// <remarks></remarks>
	CL_SCAN_MAIL = 2,
	/// <summary>
	/// Enables support for OLE2 containers (used by MS Office and .msi files).
	/// </summary>
	/// <remarks></remarks>
	CL_SCAN_OLE2 = 4,
	/// <summary>
	/// With this flag the library will mark encrypted archives as viruses (Encrypted.Zip, Encrypted.RAR).
	/// </summary>
	/// <remarks></remarks>
	CL_SCAN_BLOCKENCRYPTED = 8,
	/// <summary>
	/// This flag enables HTML normalisation (including ScrEnc decryption).
	/// </summary>
	/// <remarks></remarks>
	CL_SCAN_HTML = 16,
	/// <summary>
	/// This flag enables deep scanning of Portable Executable files and allows libclamav to unpack executables compressed with run-time unpackers.
	/// </summary>
	/// <remarks></remarks>
	CL_SCAN_PE = 32,
	/// <summary>
	/// libclamav will try to detect broken executables andmark themas Broken.Executable.
	/// </summary>
	/// <remarks></remarks>
	CL_SCAN_BLOCKBROKEN = 64,
	/// <summary>
	/// Undocumented flag in ClamAV Documentation.
	/// </summary>
	/// <remarks></remarks>
	CL_SCAN_MAILURL = 128,
	/// <summary>
	/// Undocumented flag in ClamAV Documentation.
	/// </summary>
	/// <remarks></remarks>
	CL_SCAN_BLOCKMAX = 256,
	/// <summary>
	/// Enable algorithmic detection of viruses.
	/// </summary>
	/// <remarks></remarks>
	CL_SCAN_ALGORITHMIC = 512,
	/// <summary>
	/// Phishing module: always block SSL mismatches in URLs.
	/// </summary>
	/// <remarks></remarks>
	CL_SCAN_PHISHING_BLOCKSSL = 2048,
	/// <summary>
	/// Phishing module: always block cloaked URLs.
	/// </summary>
	/// <remarks></remarks>
	CL_SCAN_PHISHING_BLOCKCLOAK = 4096,
	/// <summary>
	/// Enable support for ELF files.
	/// </summary>
	/// <remarks></remarks>
	CL_SCAN_ELF = 8192,
	/// <summary>
	/// Enables scanning within PDF files.
	/// </summary>
	/// <remarks></remarks>
	CL_SCAN_PDF = 16384,
	/// <summary>
	/// Enable the DLP module which scans for credit card and SSN numbers.
	/// </summary>
	/// <remarks></remarks>
	CL_SCAN_STRUCTURED = 32768,
	/// <summary>
	/// Search for SSNs formatted as xx-yy-zzzz.
	/// </summary>
	/// <remarks></remarks>
	CL_SCAN_STRUCTURED_SSN_NORMAL = 65536,
	/// <summary>
	/// Search for SSNs formatted as xxyyzzzz.
	/// </summary>
	/// <remarks></remarks>
	CL_SCAN_STRUCTURED_SSN_STRIPPED = 131072,
	/// <summary>
	/// Scan RFC1341 messages split over many emails. You will need to periodically
	/// clean up $TemporaryDirectory/clamav-partial directory (or use CleanClamavTempoary).
	/// </summary>
	/// <remarks></remarks>
	CL_SCAN_PARTIAL_MESSAGE = 262144,
	/// <summary>
	/// Allow heuristic match to take precedence. When enabled, if a heuristic scan (such
	/// as phishingScan) detects a possible virus/phish it will stop scan immediately. Rec-ommended, saves CPU scan-time. When disabled, virus/phish detected by heuris-tic scans will be reported only at the end of a scan. If an archive contains both a
	/// heuristically detected virus/phishing, and a real malware, the real malware will be
	/// reported.
	/// </summary>
	/// <remarks></remarks>
	CL_SCAN_HEURISTIC_PRECEDENCE = 524288,
	/// <summary>
	/// OLE2 containers, which contain VBA macros will be marked infected (Heuris-tics.OLE2.ContainsMacros).
	/// </summary>
	/// <remarks></remarks>
	CL_SCAN_BLOCKMACROS = 1048576,
	/// <summary>
	/// Undocumented flag in ClamAV Documentation.
	/// </summary>
	/// <remarks></remarks>
	CL_SCAN_ALLMATCHES = 2097152,
	/// <summary>
	/// Enables scanning within SWF files, notably compressed SWF.
	/// </summary>
	/// <remarks></remarks>
	CL_SCAN_SWF = 4194304,
	/// <summary>
	/// Standard scan option recormended by ClamAV Team.
	/// </summary>
	/// <remarks></remarks>
	CL_SCAN_STDOPT = CL_SCAN_ARCHIVE | CL_SCAN_MAIL | CL_SCAN_OLE2 | CL_SCAN_PDF | CL_SCAN_HTML | CL_SCAN_PE | CL_SCAN_ALGORITHMIC | CL_SCAN_ELF | CL_SCAN_SWF
}

/// <summary>
/// ClamAV database loading options.
/// </summary>
/// <remarks></remarks>
[Flags()]
public enum DatabaseOptions
{
	/// <summary>
	/// Load phishing signatures.
	/// </summary>
	/// <remarks></remarks>
	CL_DB_PHISHING = 2,
	/// <summary>
	/// Initialize the phishing detection module and load .wdb and .pdb files.
	/// </summary>
	/// <remarks></remarks>
	CL_DB_PHISHING_URLS = 8,
	/// <summary>
	/// Load signatures for Potentially Unwanted Applications.
	/// </summary>
	/// <remarks></remarks>
	CL_DB_PUA = 16,
	/// <summary>
	/// Only load official signatures from digitally signed databases.
	/// </summary>
	/// <remarks></remarks>
	CL_DB_OFFICIAL_ONLY = 4096,
	/// <summary>
	/// Load bytecode.
	/// </summary>
	/// <remarks></remarks>
	CL_DB_BYTECODE = 8192,
	/// <summary>
	/// This is an alias for a recommended set of scan options.
	/// </summary>
	/// <remarks></remarks>
	CL_DB_STDOPT = CL_DB_PHISHING | CL_DB_PHISHING_URLS | CL_DB_BYTECODE
}

#endregion

#region Supporting Classes and Structures
internal struct SCAN_RESULT_PARAMS
{
    public string FullPath;
    public uint ScannedData;
    public string VirusName;
    public DateTime ScanTime;
    public long ElapsedTime;
}

public class ScanResult
{
    private SCAN_RESULT_PARAMS _hasil;
    public const int CL_COUNT_PRECISION = 4096;

    internal ScanResult(SCAN_RESULT_PARAMS hasil)
    {
        this._hasil = hasil;
    }

    /// <summary>
    /// Fullpath to scanned file.
    /// </summary>
    public string FullPath { get { return _hasil.FullPath; } }
    /// <summary>
    /// Amount of scanned data, in MB percision.
    /// </summary>
    public double ScannedData { get { return _hasil.ScannedData * (CL_COUNT_PRECISION / 1024.0) / 1024.0; } }
    /// <summary>
    /// Virus name, if the file is infected.
    /// </summary>
    public string VirusName { get { return _hasil.VirusName; } }
    /// <summary>
    /// Date of file is scanned.
    /// </summary>
    public DateTime ScanTime { get { return _hasil.ScanTime; } }
    /// <summary>
    /// Elapsed time to scan this file. In miliseconds percision.
    /// </summary>
    public long ElapsedTime { get { return _hasil.ElapsedTime; } }
}
#endregion

public class ManagedClam
    {
    #region PInvokes
    private const string ClamavLibrary = "libclamav.dll";
    [DllImport(ClamavLibrary, EntryPoint = "cl_engine_free", CallingConvention = CallingConvention.Cdecl)]
    static internal extern int cl_engine_free(IntPtr engine);

    [DllImport(ClamavLibrary, EntryPoint = "cl_engine_compile", CallingConvention = CallingConvention.Cdecl)]
    static internal extern int cl_engine_compile(System.IntPtr engine);

    [DllImport(ClamavLibrary, EntryPoint = "cl_scanfile", CallingConvention = CallingConvention.Cdecl)]
    static internal extern int cl_scanfile([InAttribute(), MarshalAs(UnmanagedType.LPStr)] string filename, ref IntPtr virname, ref uint scanned, IntPtr engine, uint scanoptions);

    [DllImport(ClamavLibrary, EntryPoint = "cl_init", CallingConvention = CallingConvention.Cdecl)]
    static internal extern int cl_init(uint initoptions);

    [DllImport(ClamavLibrary, EntryPoint = "cl_engine_new", CallingConvention = CallingConvention.Cdecl)]
    static internal extern IntPtr cl_engine_new();

    [DllImport(ClamavLibrary, EntryPoint = "cl_load", CallingConvention = CallingConvention.Cdecl)]
    static internal extern int cl_load([InAttribute()] IntPtr path, IntPtr engine, ref uint signo, uint dboptions);

    [DllImport(ClamavLibrary, EntryPoint = "cl_strerror", CallingConvention = CallingConvention.Cdecl)]
    static internal extern IntPtr cl_strerror(int clerror);

    [DllImport(ClamavLibrary, EntryPoint = "cl_countsigs", CallingConvention = CallingConvention.Cdecl)]
    static internal extern int cl_countsigs([InAttribute(), MarshalAs(UnmanagedType.LPStr)] string path, uint countoptions, ref uint sigs);

    [DllImport(ClamavLibrary, EntryPoint = "cl_retver", CallingConvention = CallingConvention.Cdecl)]
    static internal extern IntPtr cl_retver();

    //[DllImport(ClamavLibrary, EntryPoint = "cl_engine_set_num", CallingConvention = CallingConvention.Cdecl)]
    //internal extern int cl_engine_set_num(IntPtr engine, EngineFields field, long num);

    //[DllImport(ClamavLibrary, EntryPoint = "cl_engine_get_num", CallingConvention = CallingConvention.Cdecl)]
    //internal extern long cl_engine_get_num([InAttribute(), MarshalAsAttribute(UnmanagedType.LPStr)] IntPtr engine, EngineFields field, ref int err);

    //[DllImport(ClamavLibrary, EntryPoint = "cl_engine_set_str", CallingConvention = CallingConvention.Cdecl)]
    //internal extern int cl_engine_set_str(IntPtr engine, EngineFields field, [InAttribute(), MarshalAs(UnmanagedType.LPStr)]string str);

    //[DllImport(ClamavLibrary, EntryPoint = "cl_engine_get_str", CallingConvention = CallingConvention.Cdecl)]
    //internal extern IntPtr cl_engine_get_str([InAttribute(), MarshalAsAttribute(UnmanagedType.LPStr)]IntPtr engine, EngineFields field, ref int err);
    #endregion
    #region Constants
    private const int CL_SUCCESS = 0;
    private const int CL_CLEAN = 0;
    private const int CL_VIRUS = 0;

    public const int CL_INIT_STDOPT = 0;

    private const int CL_COUNTSIGS_OFFICIAL = 1;
    private const int CL_COUNTSIGS_UNOFFICIAL = 2;
    private const int CL_COUNTSIGS_ALL = (CL_COUNTSIGS_OFFICIAL | CL_COUNTSIGS_UNOFFICIAL);
    #endregion 
    #region Properties
    public string DatabasePath {get; set;}

    /// <summary>
    /// Count official signature from database.
    /// </summary>
    public uint CVD_OfficialCount
    {
        get
        {
            uint countSigs = 0;
            int response = cl_countsigs(DatabasePath , CL_COUNTSIGS_OFFICIAL, ref countSigs);
            ThrowIfCodeError(response);
            return countSigs;
        }
    }

/// <summary>
/// Count unofficial / third party signature from database.
/// </summary>
    public uint CVD_UnofficialCount
    {
        get
        {
            uint countSigs = 0;
            int response = cl_countsigs(DatabasePath , CL_COUNTSIGS_UNOFFICIAL, ref countSigs);
            ThrowIfCodeError(response);
            return countSigs;
        }
    }

    /// <summary>
    /// Count both official and unofficial signature from database.
    /// </summary>
    public uint CVD_AllCount
    {
        get
        {
            uint countSigs = 0;
            int response = cl_countsigs(DatabasePath, CL_COUNTSIGS_ALL, ref countSigs);
            ThrowIfCodeError(response);
            return countSigs;
        }
    }

    /// <summary>
    /// Get installed ClamAV engine version.
    /// </summary>
    public string EngineVersion
    {
        get
        {
            return Marshal.PtrToStringAnsi(cl_retver());
        }
    }
    #endregion
    #region Supporting Modules
    private void ThrowIfCodeError(int code)
    {
        if (code != CL_SUCCESS)
        {
            string err_str = Marshal.PtrToStringAnsi(cl_strerror(code));
            throw new ApplicationException(err_str);
        }
    }
    #endregion

    private bool _IsClamavInitialized = false;
    private bool _Compiled = false;
    private IntPtr cl_engine = IntPtr.Zero;

    /// <summary>
    /// Initialize, load database and compile new ClamAV engine.
    /// </summary>
    /// <param name="dbOptions">Database load options.</param>
    /// <param name="initOption">ClamAV initialize option, use <see cref="CL_INIT_STDOPT"/> if you not sure.</param>
    /// <param name="LoadedSignature">Number of loaded signature from database.</param>
    /// <param name="dbPath">Fullpath to directory which contains main.cvd and daily.cvd files.</param>
    [System.Runtime.ExceptionServices.HandleProcessCorruptedStateExceptions]
    public unsafe void LoadClamAV(DatabaseOptions dbOptions, uint initOption, ref uint LoadedSignature)
    {
        try
        {
            int cl = 0;

            //Initalize ClamAV
            //Only call this once
            if (_IsClamavInitialized == false)
            {
                cl = cl_init(initOption);
                ThrowIfCodeError(cl);
                _IsClamavInitialized = true;
            }

            //Create new engine
            cl_engine = cl_engine_new();

            //Load signatures
            IntPtr cvd_path = Marshal.StringToHGlobalAnsi(DatabasePath);
            cl = cl_load(cvd_path, cl_engine, ref LoadedSignature, (uint)dbOptions);
            if (cl != CL_SUCCESS)
            {
                FreeMemory();
                ThrowIfCodeError(cl);
            }

            //Compile engine
            cl = cl_engine_compile(cl_engine);
            if (cl != CL_SUCCESS)
            {
                FreeMemory();
                ThrowIfCodeError(cl);
            }
            _Compiled = true;
        }
        catch (Exception ex)
        {
            throw new ApplicationException("An unidentified exception was found. See InnerException for more details.", ex);
        }
    }

    /// <summary>
    /// Scan a file.
    /// </summary>
    /// <param name="FullPath">Fullpath to file.</param>
    /// <param name="sOptions">Scan options, use <see cref="ScanOptions.CL_SCAN_STDOPT"/> if you don't sure.</param>
    /// <returns><see cref="ScanResult"/> class which contains all scan result information.</returns>
    [System.Runtime.ExceptionServices.HandleProcessCorruptedStateExceptions]
    public unsafe ScanResult ScanFile(string FullPath, ScanOptions sOptions)
    {
        try
        {
            if (_Compiled == false)
            {
                throw new InvalidOperationException("ClamAV engine is not loaded.");
            }
            System.Diagnostics.Stopwatch tmr = new System.Diagnostics.Stopwatch();
            tmr.Start();
            IntPtr virName = IntPtr.Zero;
            uint tScanned = 0;
            SCAN_RESULT_PARAMS hasil = new SCAN_RESULT_PARAMS();

            hasil.FullPath = FullPath;
            hasil.ScanTime = DateTime.Now;

            int cl = cl_scanfile(FullPath, ref virName, ref tScanned, cl_engine, (uint)sOptions);
            if (cl == CL_CLEAN)
            {
                hasil.VirusName = "CLEAN";
            }
            else if (cl == CL_VIRUS)
            {
                hasil.VirusName = Marshal.PtrToStringAnsi(virName);
            }
            else
            {
                ThrowIfCodeError(cl);
            }

            tmr.Stop();
            hasil.ScannedData = tScanned;
            hasil.ElapsedTime = tmr.ElapsedMilliseconds;
            ScanResult ret = new ScanResult(hasil);
            return ret;
        }
        catch (Exception ex)
        {
            throw new ApplicationException("An unidentified exception was found. See InnerException for more details.", ex);
        }
    }

    /// <summary>
    /// Clean up memory. Call this after calling to <see cref="LoadClamAV"/>.
    /// </summary>
    public unsafe  void FreeMemory()
    {
        cl_engine_free(cl_engine);
        cl_engine = IntPtr.Zero;
        _Compiled = false;
    }
  }
}
