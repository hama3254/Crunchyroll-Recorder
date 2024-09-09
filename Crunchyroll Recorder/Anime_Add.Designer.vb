<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Anime_Add
    Inherits MetroFramework.Forms.MetroForm


    'Das Formular überschreibt den Löschvorgang, um die Komponentenliste zu bereinigen.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Wird vom Windows Form-Designer benötigt.
    Private components As System.ComponentModel.IContainer

    'Hinweis: Die folgende Prozedur ist für den Windows Form-Designer erforderlich.
    'Das Bearbeiten ist mit dem Windows Form-Designer möglich.  
    'Das Bearbeiten mit dem Code-Editor ist nicht möglich.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Anime_Add))
        Me.groupBox1 = New System.Windows.Forms.GroupBox()
        Me.CB_AudioDevices = New MetroFramework.Controls.MetroComboBox()
        Me.StatusLabel = New MetroFramework.Controls.MetroLabel()
        Me.Pfad_tBox = New MetroFramework.Controls.MetroTextBox()
        Me.Url_tBox = New MetroFramework.Controls.MetroTextBox()
        Me.Name_tBox = New MetroFramework.Controls.MetroTextBox()
        Me.groupBox2 = New System.Windows.Forms.GroupBox()
        Me.bt_Cancel_mass = New System.Windows.Forms.Button()
        Me.comboBox4 = New MetroFramework.Controls.MetroComboBox()
        Me.ComboBox1 = New MetroFramework.Controls.MetroComboBox()
        Me.comboBox3 = New MetroFramework.Controls.MetroComboBox()
        Me.Add_Display = New MetroFramework.Controls.MetroLabel()
        Me.Btn_min = New System.Windows.Forms.PictureBox()
        Me.Btn_Close = New System.Windows.Forms.PictureBox()
        Me.btn_dl = New System.Windows.Forms.Button()
        Me.BackgroundWorker1 = New System.ComponentModel.BackgroundWorker()
        Me.groupBox1.SuspendLayout()
        Me.groupBox2.SuspendLayout()
        CType(Me.Btn_min, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Btn_Close, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'groupBox1
        '
        Me.groupBox1.BackColor = System.Drawing.Color.Transparent
        Me.groupBox1.Controls.Add(Me.CB_AudioDevices)
        Me.groupBox1.Controls.Add(Me.StatusLabel)
        Me.groupBox1.Controls.Add(Me.Pfad_tBox)
        Me.groupBox1.Controls.Add(Me.Url_tBox)
        Me.groupBox1.Controls.Add(Me.Name_tBox)
        Me.groupBox1.Location = New System.Drawing.Point(15, 70)
        Me.groupBox1.Name = "groupBox1"
        Me.groupBox1.Size = New System.Drawing.Size(720, 280)
        Me.groupBox1.TabIndex = 33
        Me.groupBox1.TabStop = False
        '
        'CB_AudioDevices
        '
        Me.CB_AudioDevices.BackColor = System.Drawing.Color.White
        Me.CB_AudioDevices.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CB_AudioDevices.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.CB_AudioDevices.FormattingEnabled = True
        Me.CB_AudioDevices.ItemHeight = 23
        Me.CB_AudioDevices.Location = New System.Drawing.Point(18, 190)
        Me.CB_AudioDevices.Name = "CB_AudioDevices"
        Me.CB_AudioDevices.Size = New System.Drawing.Size(693, 29)
        Me.CB_AudioDevices.Sorted = True
        Me.CB_AudioDevices.TabIndex = 39
        Me.CB_AudioDevices.UseSelectable = True
        '
        'StatusLabel
        '
        Me.StatusLabel.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.StatusLabel.BackColor = System.Drawing.Color.Transparent
        Me.StatusLabel.FontSize = MetroFramework.MetroLabelSize.Tall
        Me.StatusLabel.FontWeight = MetroFramework.MetroLabelWeight.Regular
        Me.StatusLabel.ForeColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.StatusLabel.Location = New System.Drawing.Point(18, 228)
        Me.StatusLabel.Name = "StatusLabel"
        Me.StatusLabel.Size = New System.Drawing.Size(693, 46)
        Me.StatusLabel.TabIndex = 38
        Me.StatusLabel.Text = "Status: idle"
        Me.StatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Pfad_tBox
        '
        Me.Pfad_tBox.BackColor = System.Drawing.Color.White
        Me.Pfad_tBox.Cursor = System.Windows.Forms.Cursors.Hand
        '
        '
        '
        Me.Pfad_tBox.CustomButton.Image = Nothing
        Me.Pfad_tBox.CustomButton.Location = New System.Drawing.Point(665, 1)
        Me.Pfad_tBox.CustomButton.Name = ""
        Me.Pfad_tBox.CustomButton.Size = New System.Drawing.Size(27, 27)
        Me.Pfad_tBox.CustomButton.Style = MetroFramework.MetroColorStyle.Blue
        Me.Pfad_tBox.CustomButton.TabIndex = 1
        Me.Pfad_tBox.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light
        Me.Pfad_tBox.CustomButton.UseSelectable = True
        Me.Pfad_tBox.CustomButton.Visible = False
        Me.Pfad_tBox.FontSize = MetroFramework.MetroTextBoxSize.Medium
        Me.Pfad_tBox.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.Pfad_tBox.Lines = New String() {"Main Directory"}
        Me.Pfad_tBox.Location = New System.Drawing.Point(18, 140)
        Me.Pfad_tBox.MaxLength = 32767
        Me.Pfad_tBox.Name = "Pfad_tBox"
        Me.Pfad_tBox.PasswordChar = Global.Microsoft.VisualBasic.ChrW(0)
        Me.Pfad_tBox.ReadOnly = True
        Me.Pfad_tBox.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.Pfad_tBox.SelectedText = ""
        Me.Pfad_tBox.SelectionLength = 0
        Me.Pfad_tBox.SelectionStart = 0
        Me.Pfad_tBox.ShortcutsEnabled = True
        Me.Pfad_tBox.Size = New System.Drawing.Size(693, 29)
        Me.Pfad_tBox.TabIndex = 36
        Me.Pfad_tBox.TabStop = False
        Me.Pfad_tBox.Text = "Main Directory"
        Me.Pfad_tBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.Pfad_tBox.UseSelectable = True
        Me.Pfad_tBox.WaterMarkColor = System.Drawing.Color.FromArgb(CType(CType(109, Byte), Integer), CType(CType(109, Byte), Integer), CType(CType(109, Byte), Integer))
        Me.Pfad_tBox.WaterMarkFont = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel)
        '
        'Url_tBox
        '
        Me.Url_tBox.BackColor = System.Drawing.Color.White
        Me.Url_tBox.Cursor = System.Windows.Forms.Cursors.Hand
        '
        '
        '
        Me.Url_tBox.CustomButton.Image = Nothing
        Me.Url_tBox.CustomButton.Location = New System.Drawing.Point(665, 1)
        Me.Url_tBox.CustomButton.Name = ""
        Me.Url_tBox.CustomButton.Size = New System.Drawing.Size(27, 27)
        Me.Url_tBox.CustomButton.Style = MetroFramework.MetroColorStyle.Blue
        Me.Url_tBox.CustomButton.TabIndex = 1
        Me.Url_tBox.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light
        Me.Url_tBox.CustomButton.UseSelectable = True
        Me.Url_tBox.CustomButton.Visible = False
        Me.Url_tBox.FontSize = MetroFramework.MetroTextBoxSize.Medium
        Me.Url_tBox.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.Url_tBox.Lines = New String() {"URL"}
        Me.Url_tBox.Location = New System.Drawing.Point(18, 40)
        Me.Url_tBox.MaxLength = 32767
        Me.Url_tBox.Name = "Url_tBox"
        Me.Url_tBox.PasswordChar = Global.Microsoft.VisualBasic.ChrW(0)
        Me.Url_tBox.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.Url_tBox.SelectedText = ""
        Me.Url_tBox.SelectionLength = 0
        Me.Url_tBox.SelectionStart = 0
        Me.Url_tBox.ShortcutsEnabled = True
        Me.Url_tBox.Size = New System.Drawing.Size(693, 29)
        Me.Url_tBox.TabIndex = 4
        Me.Url_tBox.TabStop = False
        Me.Url_tBox.Text = "URL"
        Me.Url_tBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.Url_tBox.UseSelectable = True
        Me.Url_tBox.WaterMarkColor = System.Drawing.Color.FromArgb(CType(CType(109, Byte), Integer), CType(CType(109, Byte), Integer), CType(CType(109, Byte), Integer))
        Me.Url_tBox.WaterMarkFont = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel)
        '
        'Name_tBox
        '
        Me.Name_tBox.BackColor = System.Drawing.Color.White
        Me.Name_tBox.Cursor = System.Windows.Forms.Cursors.Hand
        '
        '
        '
        Me.Name_tBox.CustomButton.Image = Nothing
        Me.Name_tBox.CustomButton.Location = New System.Drawing.Point(665, 1)
        Me.Name_tBox.CustomButton.Name = ""
        Me.Name_tBox.CustomButton.Size = New System.Drawing.Size(27, 27)
        Me.Name_tBox.CustomButton.Style = MetroFramework.MetroColorStyle.Blue
        Me.Name_tBox.CustomButton.TabIndex = 1
        Me.Name_tBox.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light
        Me.Name_tBox.CustomButton.UseSelectable = True
        Me.Name_tBox.CustomButton.Visible = False
        Me.Name_tBox.FontSize = MetroFramework.MetroTextBoxSize.Medium
        Me.Name_tBox.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.Name_tBox.Lines = New String() {"Use Custom Name"}
        Me.Name_tBox.Location = New System.Drawing.Point(18, 90)
        Me.Name_tBox.MaxLength = 32767
        Me.Name_tBox.Name = "Name_tBox"
        Me.Name_tBox.PasswordChar = Global.Microsoft.VisualBasic.ChrW(0)
        Me.Name_tBox.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.Name_tBox.SelectedText = ""
        Me.Name_tBox.SelectionLength = 0
        Me.Name_tBox.SelectionStart = 0
        Me.Name_tBox.ShortcutsEnabled = True
        Me.Name_tBox.Size = New System.Drawing.Size(693, 29)
        Me.Name_tBox.TabIndex = 5
        Me.Name_tBox.TabStop = False
        Me.Name_tBox.Text = "Use Custom Name"
        Me.Name_tBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.Name_tBox.UseSelectable = True
        Me.Name_tBox.WaterMarkColor = System.Drawing.Color.FromArgb(CType(CType(109, Byte), Integer), CType(CType(109, Byte), Integer), CType(CType(109, Byte), Integer))
        Me.Name_tBox.WaterMarkFont = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel)
        '
        'groupBox2
        '
        Me.groupBox2.BackColor = System.Drawing.Color.Transparent
        Me.groupBox2.Controls.Add(Me.bt_Cancel_mass)
        Me.groupBox2.Controls.Add(Me.comboBox4)
        Me.groupBox2.Controls.Add(Me.ComboBox1)
        Me.groupBox2.Controls.Add(Me.comboBox3)
        Me.groupBox2.Controls.Add(Me.Add_Display)
        Me.groupBox2.Location = New System.Drawing.Point(15, 70)
        Me.groupBox2.Name = "groupBox2"
        Me.groupBox2.Size = New System.Drawing.Size(720, 280)
        Me.groupBox2.TabIndex = 44
        Me.groupBox2.TabStop = False
        Me.groupBox2.Visible = False
        '
        'bt_Cancel_mass
        '
        Me.bt_Cancel_mass.BackgroundImage = Global.Crunchyroll_Recorder.My.Resources.Resources.add_mass_cancel
        Me.bt_Cancel_mass.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.bt_Cancel_mass.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.bt_Cancel_mass.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.bt_Cancel_mass.ForeColor = System.Drawing.SystemColors.Control
        Me.bt_Cancel_mass.Location = New System.Drawing.Point(159, 231)
        Me.bt_Cancel_mass.Name = "bt_Cancel_mass"
        Me.bt_Cancel_mass.Size = New System.Drawing.Size(403, 36)
        Me.bt_Cancel_mass.TabIndex = 37
        Me.bt_Cancel_mass.Text = "Cancel"
        Me.bt_Cancel_mass.UseVisualStyleBackColor = True
        '
        'comboBox4
        '
        Me.comboBox4.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.comboBox4.FormattingEnabled = True
        Me.comboBox4.ItemHeight = 23
        Me.comboBox4.Location = New System.Drawing.Point(13, 154)
        Me.comboBox4.Name = "comboBox4"
        Me.comboBox4.Size = New System.Drawing.Size(693, 29)
        Me.comboBox4.TabIndex = 2
        Me.comboBox4.UseSelectable = True
        '
        'ComboBox1
        '
        Me.ComboBox1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ComboBox1.FormattingEnabled = True
        Me.ComboBox1.ItemHeight = 23
        Me.ComboBox1.Location = New System.Drawing.Point(13, 50)
        Me.ComboBox1.Name = "ComboBox1"
        Me.ComboBox1.Size = New System.Drawing.Size(693, 29)
        Me.ComboBox1.TabIndex = 1
        Me.ComboBox1.UseSelectable = True
        '
        'comboBox3
        '
        Me.comboBox3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.comboBox3.FormattingEnabled = True
        Me.comboBox3.ItemHeight = 23
        Me.comboBox3.Location = New System.Drawing.Point(13, 102)
        Me.comboBox3.Name = "comboBox3"
        Me.comboBox3.Size = New System.Drawing.Size(693, 29)
        Me.comboBox3.TabIndex = 1
        Me.comboBox3.UseSelectable = True
        '
        'Add_Display
        '
        Me.Add_Display.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Add_Display.BackColor = System.Drawing.Color.Transparent
        Me.Add_Display.FontSize = MetroFramework.MetroLabelSize.Tall
        Me.Add_Display.FontWeight = MetroFramework.MetroLabelWeight.Regular
        Me.Add_Display.ForeColor = System.Drawing.Color.Black
        Me.Add_Display.Location = New System.Drawing.Point(20, 228)
        Me.Add_Display.Name = "Add_Display"
        Me.Add_Display.Size = New System.Drawing.Size(691, 42)
        Me.Add_Display.TabIndex = 36
        Me.Add_Display.Text = "..."
        Me.Add_Display.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Btn_min
        '
        Me.Btn_min.BackColor = System.Drawing.Color.Transparent
        Me.Btn_min.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.Btn_min.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Btn_min.Image = CType(resources.GetObject("Btn_min.Image"), System.Drawing.Image)
        Me.Btn_min.Location = New System.Drawing.Point(567, 1)
        Me.Btn_min.Margin = New System.Windows.Forms.Padding(0)
        Me.Btn_min.Name = "Btn_min"
        Me.Btn_min.Size = New System.Drawing.Size(25, 25)
        Me.Btn_min.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.Btn_min.TabIndex = 73
        Me.Btn_min.TabStop = False
        '
        'Btn_Close
        '
        Me.Btn_Close.BackColor = System.Drawing.Color.Transparent
        Me.Btn_Close.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.Btn_Close.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Btn_Close.Image = Global.Crunchyroll_Recorder.My.Resources.Resources.main_close
        Me.Btn_Close.Location = New System.Drawing.Point(592, 1)
        Me.Btn_Close.Margin = New System.Windows.Forms.Padding(0)
        Me.Btn_Close.Name = "Btn_Close"
        Me.Btn_Close.Size = New System.Drawing.Size(40, 40)
        Me.Btn_Close.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.Btn_Close.TabIndex = 72
        Me.Btn_Close.TabStop = False
        '
        'btn_dl
        '
        Me.btn_dl.BackgroundImage = Global.Crunchyroll_Recorder.My.Resources.Resources.main_button_download_default
        Me.btn_dl.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btn_dl.FlatAppearance.BorderSize = 0
        Me.btn_dl.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn_dl.Font = New System.Drawing.Font("Microsoft Sans Serif", 27.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_dl.ForeColor = System.Drawing.SystemColors.Control
        Me.btn_dl.Location = New System.Drawing.Point(106, 377)
        Me.btn_dl.Name = "btn_dl"
        Me.btn_dl.Size = New System.Drawing.Size(538, 50)
        Me.btn_dl.TabIndex = 75
        Me.btn_dl.Text = "Download"
        Me.btn_dl.UseVisualStyleBackColor = True
        '
        'Anime_Add
        '
        Me.ApplyImageInvert = True
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.BorderStyle = MetroFramework.Forms.MetroFormBorderStyle.FixedSingle
        Me.ClientSize = New System.Drawing.Size(750, 450)
        Me.Controls.Add(Me.btn_dl)
        Me.Controls.Add(Me.Btn_min)
        Me.Controls.Add(Me.Btn_Close)
        Me.Controls.Add(Me.groupBox1)
        Me.Controls.Add(Me.groupBox2)
        Me.Font = New System.Drawing.Font("Arial", 24.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "Anime_Add"
        Me.Padding = New System.Windows.Forms.Padding(10, 60, 20, 20)
        Me.Text = "Add Video"
        Me.TextAlign = MetroFramework.Forms.MetroFormTextAlign.Center
        Me.groupBox1.ResumeLayout(False)
        Me.groupBox2.ResumeLayout(False)
        CType(Me.Btn_min, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Btn_Close, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Public WithEvents groupBox2 As GroupBox
    Public WithEvents groupBox1 As GroupBox
    Public WithEvents StatusLabel As MetroFramework.Controls.MetroLabel
    Public WithEvents Add_Display As MetroFramework.Controls.MetroLabel
    Friend WithEvents MetroTextBox1 As MetroFramework.Controls.MetroTextBox
    Public WithEvents Url_tBox As MetroFramework.Controls.MetroTextBox
    Public WithEvents Pfad_tBox As MetroFramework.Controls.MetroTextBox
    Public WithEvents Name_tBox As MetroFramework.Controls.MetroTextBox
    Public WithEvents comboBox4 As MetroFramework.Controls.MetroComboBox
    Public WithEvents ComboBox1 As MetroFramework.Controls.MetroComboBox
    Public WithEvents comboBox3 As MetroFramework.Controls.MetroComboBox
    Private WithEvents Btn_min As PictureBox
    Private WithEvents Btn_Close As PictureBox
    Friend WithEvents btn_dl As Button
    Friend WithEvents BackgroundWorker1 As System.ComponentModel.BackgroundWorker
    Friend WithEvents bt_Cancel_mass As Button
    Public WithEvents CB_AudioDevices As MetroFramework.Controls.MetroComboBox
End Class
