Imports System.Threading
Imports ReSharpedClam

Public Class frmMain

    Private Sub ChangeStatusText(ByVal msg As String)
        If lblStatus.InvokeRequired Then
            Dim add_delegate As New Action(Of String)(AddressOf ChangeStatusText)
            Me.Invoke(add_delegate, msg)
        Else
            lblStatus.Text = msg
            lstLog.Items.Add("(" & Now.ToLocalTime & ") " & msg)
        End If
    End Sub

    Private Sub ScanCompleted(ByVal args As Object)
        'Cast 'Object' to 'ClamScanResult'
        Dim scan_result As ClamScanResult = DirectCast(args, ClamScanResult)
        'Change labels text.
        With scan_result
            lblFileName.Text = System.IO.Path.GetFileName(.FullPath)
            lblFullPath.Text = .FullPath
            lblFileSize.Text = .FileSize & " bytes"
            lblScannedData.Text = .ScannedData.ToKbytes() & " KB" 'Convert scanned data to KB (Kbytes)
            lblIsInfected.Text = .IsInfected
            lblVirusName.Text = .VirusName
            lblTimeElapsed.Text = .TimeElapsed.ToString
        End With
        ChangeStatusText("Ready.")
    End Sub

    Private Sub ThreadScanFile(ByVal args As Object)
        'Always use 'Using' statement to handle memory leaking
        ChangeStatusText("Creating engine...")
        Using cl_engine As New ClamEngine()
            'Load database
            Dim loadedSignatureCount As UInteger = 0
            'Change this path according your ClamAV Database directory path.
            ChangeStatusText("Loading database...")
            cl_engine.LoadDatabase("S:\clamav db", loadedSignatureCount)
            ChangeStatusText("Database loaded.")

            'Compile engine
            ChangeStatusText("Compiling engine...")
            cl_engine.CompileEngine()
            ChangeStatusText("Engine compiled.")

            'Scan a file
            'File path stored in 'args' parameter.
            ChangeStatusText("Scan started.")
            Dim scan_result As ClamScanResult = cl_engine.ScanByFile(args.ToString)
            ChangeStatusText("Scan finished.")
            'Invoke 'ScanCompleted' method.
            Me.Invoke(New Action(Of Object)(AddressOf ScanCompleted), scan_result)
        End Using
    End Sub

    Private Sub cmdBrowse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdBrowse.Click
        If ofd.ShowDialog = Windows.Forms.DialogResult.OK Then
            txtFileToScan.Text = ofd.FileName
        End If
    End Sub

    Private Sub Form1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ClamMain.InitializeClamAV()
    End Sub

    Private Sub cmdStartScan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdStartScan.Click
        If txtFileToScan.TextLength = 0 Then
            MsgBox("Please select a file.", vbExclamation, "File not selected.")
            Return
        End If
        'Start file scanning.
        ChangeStatusText("Starting scan...")
        ThreadPool.QueueUserWorkItem(New WaitCallback(AddressOf ThreadScanFile), txtFileToScan.Text)
    End Sub
End Class
