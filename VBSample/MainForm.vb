Imports SharpClam

Public Class MainForm

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If OpenFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
            TextBox1.Text = OpenFileDialog1.FileName
        End If
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        If TextBox1.Text.Trim = "" Then
            MsgBox("Path is empty!", vbExclamation, "Warning!")
        ElseIf Not IO.File.Exists(TextBox1.Text) Then
            MsgBox("File not found!", vbExclamation, "Warning!")
        End If

        'This database path is relative. Change this line according to 
        'your ClamAV database directory.
        Dim database_path As String = Environment.CurrentDirectory & "\database"

        Dim clam As New ManagedClam
        Dim loaded_signature As UInteger = 0

        With ListBox1.Items
            .Clear()
            clam.DatabasePath = database_path 'Must set this first!
            clam.LoadClamAV(DatabaseOptions.CL_DB_STDOPT, ManagedClam.CL_INIT_STDOPT, loaded_signature)

            .Add("Initializing ClamAV Engine...")
            .Add(String.Format("Loaded signature: {0}", loaded_signature))
            .Add("")
            .Add("Start scanning...")

            Dim result As ScanResult = clam.ScanFile(TextBox1.Text, ScanOptions.CL_SCAN_STDOPT)
            .Add(String.Format("Elapsed time (in ms): {0}", result.ElapsedTime))
            .Add(String.Format("Fullpath: {0}", result.FullPath))
            .Add(String.Format("Scanned data (in MB): {0}", result.ScannedData))
            .Add(String.Format("Scan time: {0}", result.ScanTime.ToString))
            .Add(String.Format("Virus name: {0}", result.VirusName))

            .Add("Dispose all allocated memory...")
            clam.FreeMemory()

            .Add("")
            .Add("Scan has been completed.")
        End With

        clam = Nothing 'Optional
    End Sub
End Class
