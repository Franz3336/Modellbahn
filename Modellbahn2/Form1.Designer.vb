<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Das Formular überschreibt den Löschvorgang, um die Komponentenliste zu bereinigen.
    <System.Diagnostics.DebuggerNonUserCode()> _
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form1))
        Me.Button1 = New System.Windows.Forms.Button()
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.lbl_steuern = New System.Windows.Forms.ToolStripLabel()
        Me.Btn_startsteuern = New System.Windows.Forms.ToolStripButton()
        Me.Btn_addsteuern = New System.Windows.Forms.ToolStripButton()
        Me.LblComAnschluss = New System.Windows.Forms.ToolStripComboBox()
        Me.seperator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.Btn_save = New System.Windows.Forms.ToolStripButton()
        Me.Btn_open = New System.Windows.Forms.ToolStripButton()
        Me.Btn_new = New System.Windows.Forms.ToolStripButton()
        Me.seperator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.lbl_bau = New System.Windows.Forms.ToolStripLabel()
        Me.Btn_gleis = New System.Windows.Forms.ToolStripButton()
        Me.Btn_besetzt = New System.Windows.Forms.ToolStripButton()
        Me.Btn_weiche = New System.Windows.Forms.ToolStripSplitButton()
        Me.Btn_ws = New System.Windows.Forms.ToolStripMenuItem()
        Me.Btn_wk = New System.Windows.Forms.ToolStripMenuItem()
        Me.Btn_löschen = New System.Windows.Forms.ToolStripButton()
        Me.seperator3 = New System.Windows.Forms.ToolStripSeparator()
        Me.Btn_code = New System.Windows.Forms.ToolStripButton()
        Me.Btn_error = New System.Windows.Forms.ToolStripButton()
        Me.seperator4 = New System.Windows.Forms.ToolStripSeparator()
        Me.BtnAusführen = New System.Windows.Forms.ToolStripButton()
        Me.seperator5 = New System.Windows.Forms.ToolStripSeparator()
        Me.Btn_help = New System.Windows.Forms.ToolStripButton()
        Me.btn_settings = New System.Windows.Forms.ToolStripButton()
        Me.lblTimeOfDay = New System.Windows.Forms.ToolStripLabel()
        Me.ImageList2 = New System.Windows.Forms.ImageList(Me.components)
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.TimerBesMeld = New System.Windows.Forms.Timer(Me.components)
        Me.Tbprog = New System.Windows.Forms.RichTextBox()
        Me.SaveFileDialog1 = New System.Windows.Forms.SaveFileDialog()
        Me.HSBau = New System.Windows.Forms.HScrollBar()
        Me.VSBau = New System.Windows.Forms.VScrollBar()
        Me.Lb_error_log = New System.Windows.Forms.ListBox()
        Me.panel_help = New System.Windows.Forms.Panel()
        Me.rtb_help = New System.Windows.Forms.RichTextBox()
        Me.lblhelp = New System.Windows.Forms.Label()
        Me.pic_helpExit = New System.Windows.Forms.PictureBox()
        Me.panel_settings = New System.Windows.Forms.Panel()
        Me.lblBesTimerWorkingText = New System.Windows.Forms.Label()
        Me.lblBesTimerWorking = New System.Windows.Forms.Label()
        Me.BtnDeleteSteuerung = New System.Windows.Forms.Button()
        Me.numericScaling = New System.Windows.Forms.NumericUpDown()
        Me.lblYcoord = New System.Windows.Forms.Label()
        Me.lblXcoord = New System.Windows.Forms.Label()
        Me.lblsettings = New System.Windows.Forms.Label()
        Me.btn_settingsExit = New System.Windows.Forms.PictureBox()
        Me.lblScrollScaling = New System.Windows.Forms.Label()
        Me.panel_kreuzweiche = New System.Windows.Forms.Panel()
        Me.Pic_kor = New System.Windows.Forms.PictureBox()
        Me.Pic_kol = New System.Windows.Forms.PictureBox()
        Me.Pic_k = New System.Windows.Forms.PictureBox()
        Me.lblkreuzweiche = New System.Windows.Forms.Label()
        Me.ToolStrip1.SuspendLayout()
        Me.panel_help.SuspendLayout()
        CType(Me.pic_helpExit, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.panel_settings.SuspendLayout()
        CType(Me.numericScaling, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btn_settingsExit, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.panel_kreuzweiche.SuspendLayout()
        CType(Me.Pic_kor, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Pic_kol, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Pic_k, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Button1
        '
        Me.Button1.BackColor = System.Drawing.Color.Red
        Me.Button1.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Button1.Location = New System.Drawing.Point(702, 2)
        Me.Button1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(75, 30)
        Me.Button1.TabIndex = 0
        Me.Button1.Text = "Ende"
        Me.Button1.UseVisualStyleBackColor = False
        '
        'ImageList1
        '
        Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList1.TransparentColor = System.Drawing.Color.Empty
        Me.ImageList1.Images.SetKeyName(0, "ho.png")
        Me.ImageList1.Images.SetKeyName(1, "hoAn.png")
        Me.ImageList1.Images.SetKeyName(2, "hos.png")
        Me.ImageList1.Images.SetKeyName(3, "hosAn.png")
        Me.ImageList1.Images.SetKeyName(4, "hu.png")
        Me.ImageList1.Images.SetKeyName(5, "huAn.png")
        Me.ImageList1.Images.SetKeyName(6, "hus.png")
        Me.ImageList1.Images.SetKeyName(7, "husAn.png")
        Me.ImageList1.Images.SetKeyName(8, "vl.png")
        Me.ImageList1.Images.SetKeyName(9, "vlAn.png")
        Me.ImageList1.Images.SetKeyName(10, "vls.png")
        Me.ImageList1.Images.SetKeyName(11, "vlsAn.png")
        Me.ImageList1.Images.SetKeyName(12, "vr.png")
        Me.ImageList1.Images.SetKeyName(13, "vrAn.png")
        Me.ImageList1.Images.SetKeyName(14, "vrs.png")
        Me.ImageList1.Images.SetKeyName(15, "vrsAn.png")
        Me.ImageList1.Images.SetKeyName(16, "k.png")
        Me.ImageList1.Images.SetKeyName(17, "kol.png")
        Me.ImageList1.Images.SetKeyName(18, "k.png")
        Me.ImageList1.Images.SetKeyName(19, "kor.png")
        Me.ImageList1.Images.SetKeyName(20, "kol.png")
        Me.ImageList1.Images.SetKeyName(21, "k.png")
        Me.ImageList1.Images.SetKeyName(22, "kol.png")
        Me.ImageList1.Images.SetKeyName(23, "kor.png")
        Me.ImageList1.Images.SetKeyName(24, "kor.png")
        Me.ImageList1.Images.SetKeyName(25, "kol.png")
        Me.ImageList1.Images.SetKeyName(26, "kor.png")
        Me.ImageList1.Images.SetKeyName(27, "k.png")
        '
        'Timer1
        '
        Me.Timer1.Enabled = True
        Me.Timer1.Interval = 50
        '
        'ToolStrip1
        '
        Me.ToolStrip1.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.ToolStrip1.ImageScalingSize = New System.Drawing.Size(25, 25)
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.lbl_steuern, Me.Btn_startsteuern, Me.Btn_addsteuern, Me.LblComAnschluss, Me.seperator1, Me.Btn_save, Me.Btn_open, Me.Btn_new, Me.seperator2, Me.lbl_bau, Me.Btn_gleis, Me.Btn_besetzt, Me.Btn_weiche, Me.Btn_löschen, Me.seperator3, Me.Btn_code, Me.Btn_error, Me.seperator4, Me.BtnAusführen, Me.seperator5, Me.Btn_help, Me.btn_settings, Me.lblTimeOfDay})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(918, 40)
        Me.ToolStrip1.TabIndex = 3
        Me.ToolStrip1.Text = "Buttons"
        '
        'lbl_steuern
        '
        Me.lbl_steuern.Name = "lbl_steuern"
        Me.lbl_steuern.Size = New System.Drawing.Size(97, 34)
        Me.lbl_steuern.Text = "Steuern"
        '
        'Btn_startsteuern
        '
        Me.Btn_startsteuern.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.Btn_startsteuern.Image = CType(resources.GetObject("Btn_startsteuern.Image"), System.Drawing.Image)
        Me.Btn_startsteuern.Name = "Btn_startsteuern"
        Me.Btn_startsteuern.Size = New System.Drawing.Size(46, 34)
        Me.Btn_startsteuern.Text = "Steuern"
        '
        'Btn_addsteuern
        '
        Me.Btn_addsteuern.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.Btn_addsteuern.Image = CType(resources.GetObject("Btn_addsteuern.Image"), System.Drawing.Image)
        Me.Btn_addsteuern.Name = "Btn_addsteuern"
        Me.Btn_addsteuern.Size = New System.Drawing.Size(46, 34)
        Me.Btn_addsteuern.Text = "Schnellsteuerung hinzufügen"
        Me.Btn_addsteuern.Visible = False
        '
        'LblComAnschluss
        '
        Me.LblComAnschluss.Items.AddRange(New Object() {"Testmodus", "COM3", "COM4"})
        Me.LblComAnschluss.Name = "LblComAnschluss"
        Me.LblComAnschluss.Size = New System.Drawing.Size(100, 40)
        '
        'seperator1
        '
        Me.seperator1.Name = "seperator1"
        Me.seperator1.Size = New System.Drawing.Size(6, 40)
        '
        'Btn_save
        '
        Me.Btn_save.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.Btn_save.Image = CType(resources.GetObject("Btn_save.Image"), System.Drawing.Image)
        Me.Btn_save.Name = "Btn_save"
        Me.Btn_save.Size = New System.Drawing.Size(46, 34)
        Me.Btn_save.Text = "Speichern"
        '
        'Btn_open
        '
        Me.Btn_open.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.Btn_open.Image = CType(resources.GetObject("Btn_open.Image"), System.Drawing.Image)
        Me.Btn_open.Name = "Btn_open"
        Me.Btn_open.Size = New System.Drawing.Size(46, 34)
        Me.Btn_open.Text = "Öfnen"
        '
        'Btn_new
        '
        Me.Btn_new.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.Btn_new.Image = CType(resources.GetObject("Btn_new.Image"), System.Drawing.Image)
        Me.Btn_new.Name = "Btn_new"
        Me.Btn_new.Size = New System.Drawing.Size(46, 34)
        Me.Btn_new.Text = "Neu"
        '
        'seperator2
        '
        Me.seperator2.Name = "seperator2"
        Me.seperator2.Size = New System.Drawing.Size(6, 40)
        '
        'lbl_bau
        '
        Me.lbl_bau.Name = "lbl_bau"
        Me.lbl_bau.Size = New System.Drawing.Size(82, 34)
        Me.lbl_bau.Text = "Bauen"
        '
        'Btn_gleis
        '
        Me.Btn_gleis.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.Btn_gleis.Image = CType(resources.GetObject("Btn_gleis.Image"), System.Drawing.Image)
        Me.Btn_gleis.Name = "Btn_gleis"
        Me.Btn_gleis.Size = New System.Drawing.Size(46, 34)
        Me.Btn_gleis.Text = "Gleis"
        '
        'Btn_besetzt
        '
        Me.Btn_besetzt.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.Btn_besetzt.Image = CType(resources.GetObject("Btn_besetzt.Image"), System.Drawing.Image)
        Me.Btn_besetzt.Name = "Btn_besetzt"
        Me.Btn_besetzt.Size = New System.Drawing.Size(46, 34)
        Me.Btn_besetzt.Text = "Besetztmelder"
        '
        'Btn_weiche
        '
        Me.Btn_weiche.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.Btn_weiche.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.Btn_ws, Me.Btn_wk})
        Me.Btn_weiche.Image = CType(resources.GetObject("Btn_weiche.Image"), System.Drawing.Image)
        Me.Btn_weiche.Name = "Btn_weiche"
        Me.Btn_weiche.Size = New System.Drawing.Size(52, 34)
        Me.Btn_weiche.Text = "Weiche"
        '
        'Btn_ws
        '
        Me.Btn_ws.Name = "Btn_ws"
        Me.Btn_ws.Size = New System.Drawing.Size(237, 44)
        Me.Btn_ws.Text = "Standart"
        '
        'Btn_wk
        '
        Me.Btn_wk.Name = "Btn_wk"
        Me.Btn_wk.Size = New System.Drawing.Size(237, 44)
        Me.Btn_wk.Text = "Kreuz"
        '
        'Btn_löschen
        '
        Me.Btn_löschen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.Btn_löschen.Image = CType(resources.GetObject("Btn_löschen.Image"), System.Drawing.Image)
        Me.Btn_löschen.Name = "Btn_löschen"
        Me.Btn_löschen.Size = New System.Drawing.Size(46, 34)
        Me.Btn_löschen.Text = "Löschen"
        '
        'seperator3
        '
        Me.seperator3.Name = "seperator3"
        Me.seperator3.Size = New System.Drawing.Size(6, 40)
        '
        'Btn_code
        '
        Me.Btn_code.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.Btn_code.Image = CType(resources.GetObject("Btn_code.Image"), System.Drawing.Image)
        Me.Btn_code.Name = "Btn_code"
        Me.Btn_code.Size = New System.Drawing.Size(46, 34)
        Me.Btn_code.Text = "Fahrplan/Code - Menü"
        '
        'Btn_error
        '
        Me.Btn_error.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.Btn_error.Image = CType(resources.GetObject("Btn_error.Image"), System.Drawing.Image)
        Me.Btn_error.Name = "Btn_error"
        Me.Btn_error.Size = New System.Drawing.Size(46, 34)
        Me.Btn_error.Text = "ERROR_LOG"
        '
        'seperator4
        '
        Me.seperator4.Name = "seperator4"
        Me.seperator4.Size = New System.Drawing.Size(6, 40)
        Me.seperator4.Visible = False
        '
        'BtnAusführen
        '
        Me.BtnAusführen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.BtnAusführen.Image = CType(resources.GetObject("BtnAusführen.Image"), System.Drawing.Image)
        Me.BtnAusführen.Name = "BtnAusführen"
        Me.BtnAusführen.Size = New System.Drawing.Size(46, 34)
        Me.BtnAusführen.Text = "Ausfuehren"
        Me.BtnAusführen.Visible = False
        '
        'seperator5
        '
        Me.seperator5.Name = "seperator5"
        Me.seperator5.Size = New System.Drawing.Size(6, 40)
        '
        'Btn_help
        '
        Me.Btn_help.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.Btn_help.Image = CType(resources.GetObject("Btn_help.Image"), System.Drawing.Image)
        Me.Btn_help.Name = "Btn_help"
        Me.Btn_help.Size = New System.Drawing.Size(46, 34)
        Me.Btn_help.Text = "Hilfe"
        '
        'btn_settings
        '
        Me.btn_settings.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.btn_settings.Image = CType(resources.GetObject("btn_settings.Image"), System.Drawing.Image)
        Me.btn_settings.Name = "btn_settings"
        Me.btn_settings.Size = New System.Drawing.Size(46, 34)
        Me.btn_settings.Text = "Einstellungen"
        '
        'lblTimeOfDay
        '
        Me.lblTimeOfDay.Name = "lblTimeOfDay"
        Me.lblTimeOfDay.Size = New System.Drawing.Size(73, 32)
        Me.lblTimeOfDay.Text = "Time:"
        '
        'ImageList2
        '
        Me.ImageList2.ImageStream = CType(resources.GetObject("ImageList2.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList2.TransparentColor = System.Drawing.Color.Empty
        Me.ImageList2.Images.SetKeyName(0, "1.png")
        Me.ImageList2.Images.SetKeyName(1, "2.png")
        Me.ImageList2.Images.SetKeyName(2, "3.png")
        Me.ImageList2.Images.SetKeyName(3, "control.png")
        Me.ImageList2.Images.SetKeyName(4, "build.png")
        Me.ImageList2.Images.SetKeyName(5, "weicheR.png")
        Me.ImageList2.Images.SetKeyName(6, "weicheL.png")
        Me.ImageList2.Images.SetKeyName(7, "weicheU.png")
        Me.ImageList2.Images.SetKeyName(8, "weicheO.png")
        Me.ImageList2.Images.SetKeyName(9, "execute.png")
        Me.ImageList2.Images.SetKeyName(10, "pause.png")
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        '
        'TimerBesMeld
        '
        Me.TimerBesMeld.Interval = 300
        '
        'Tbprog
        '
        Me.Tbprog.AcceptsTab = True
        Me.Tbprog.BackColor = System.Drawing.Color.FromArgb(CType(CType(30, Byte), Integer), CType(CType(30, Byte), Integer), CType(CType(30, Byte), Integer))
        Me.Tbprog.Font = New System.Drawing.Font("Consolas", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Tbprog.ForeColor = System.Drawing.Color.Olive
        Me.Tbprog.Location = New System.Drawing.Point(629, 291)
        Me.Tbprog.Name = "Tbprog"
        Me.Tbprog.Size = New System.Drawing.Size(218, 90)
        Me.Tbprog.TabIndex = 4
        Me.Tbprog.Text = "'Program/Fahrplan"
        Me.Tbprog.Visible = False
        '
        'HSBau
        '
        Me.HSBau.LargeChange = 1
        Me.HSBau.Location = New System.Drawing.Point(9, 632)
        Me.HSBau.Name = "HSBau"
        Me.HSBau.Size = New System.Drawing.Size(768, 34)
        Me.HSBau.TabIndex = 7
        '
        'VSBau
        '
        Me.VSBau.LargeChange = 1
        Me.VSBau.Location = New System.Drawing.Point(866, 56)
        Me.VSBau.Name = "VSBau"
        Me.VSBau.Size = New System.Drawing.Size(34, 591)
        Me.VSBau.TabIndex = 8
        '
        'Lb_error_log
        '
        Me.Lb_error_log.Cursor = System.Windows.Forms.Cursors.Arrow
        Me.Lb_error_log.FormattingEnabled = True
        Me.Lb_error_log.ItemHeight = 37
        Me.Lb_error_log.Location = New System.Drawing.Point(642, 56)
        Me.Lb_error_log.Name = "Lb_error_log"
        Me.Lb_error_log.Size = New System.Drawing.Size(165, 115)
        Me.Lb_error_log.TabIndex = 9
        Me.Lb_error_log.Visible = False
        '
        'panel_help
        '
        Me.panel_help.BackColor = System.Drawing.Color.DimGray
        Me.panel_help.Controls.Add(Me.rtb_help)
        Me.panel_help.Controls.Add(Me.lblhelp)
        Me.panel_help.Controls.Add(Me.pic_helpExit)
        Me.panel_help.Location = New System.Drawing.Point(25, 68)
        Me.panel_help.Name = "panel_help"
        Me.panel_help.Size = New System.Drawing.Size(457, 232)
        Me.panel_help.TabIndex = 10
        Me.panel_help.Visible = False
        '
        'rtb_help
        '
        Me.rtb_help.BackColor = System.Drawing.Color.DimGray
        Me.rtb_help.Location = New System.Drawing.Point(3, 34)
        Me.rtb_help.Name = "rtb_help"
        Me.rtb_help.ReadOnly = True
        Me.rtb_help.Size = New System.Drawing.Size(451, 200)
        Me.rtb_help.TabIndex = 2
        Me.rtb_help.Text = ""
        '
        'lblhelp
        '
        Me.lblhelp.AutoSize = True
        Me.lblhelp.Location = New System.Drawing.Point(3, 3)
        Me.lblhelp.Name = "lblhelp"
        Me.lblhelp.Size = New System.Drawing.Size(178, 37)
        Me.lblhelp.TabIndex = 1
        Me.lblhelp.Text = "Hilfe- Menü"
        '
        'pic_helpExit
        '
        Me.pic_helpExit.Image = CType(resources.GetObject("pic_helpExit.Image"), System.Drawing.Image)
        Me.pic_helpExit.Location = New System.Drawing.Point(429, 3)
        Me.pic_helpExit.Name = "pic_helpExit"
        Me.pic_helpExit.Size = New System.Drawing.Size(25, 25)
        Me.pic_helpExit.TabIndex = 0
        Me.pic_helpExit.TabStop = False
        '
        'panel_settings
        '
        Me.panel_settings.BackColor = System.Drawing.Color.DimGray
        Me.panel_settings.Controls.Add(Me.lblBesTimerWorkingText)
        Me.panel_settings.Controls.Add(Me.lblBesTimerWorking)
        Me.panel_settings.Controls.Add(Me.BtnDeleteSteuerung)
        Me.panel_settings.Controls.Add(Me.numericScaling)
        Me.panel_settings.Controls.Add(Me.lblYcoord)
        Me.panel_settings.Controls.Add(Me.lblXcoord)
        Me.panel_settings.Controls.Add(Me.lblsettings)
        Me.panel_settings.Controls.Add(Me.btn_settingsExit)
        Me.panel_settings.Controls.Add(Me.lblScrollScaling)
        Me.panel_settings.Location = New System.Drawing.Point(25, 332)
        Me.panel_settings.Name = "panel_settings"
        Me.panel_settings.Size = New System.Drawing.Size(214, 164)
        Me.panel_settings.TabIndex = 11
        Me.panel_settings.Visible = False
        '
        'lblBesTimerWorkingText
        '
        Me.lblBesTimerWorkingText.AutoSize = True
        Me.lblBesTimerWorkingText.Location = New System.Drawing.Point(116, 30)
        Me.lblBesTimerWorkingText.Name = "lblBesTimerWorkingText"
        Me.lblBesTimerWorkingText.Size = New System.Drawing.Size(162, 37)
        Me.lblBesTimerWorkingText.TabIndex = 8
        Me.lblBesTimerWorkingText.Text = "BesTimer:"
        '
        'lblBesTimerWorking
        '
        Me.lblBesTimerWorking.AutoSize = True
        Me.lblBesTimerWorking.Location = New System.Drawing.Point(120, 50)
        Me.lblBesTimerWorking.Name = "lblBesTimerWorking"
        Me.lblBesTimerWorking.Size = New System.Drawing.Size(153, 37)
        Me.lblBesTimerWorking.TabIndex = 7
        Me.lblBesTimerWorking.Text = "BesTimer"
        '
        'BtnDeleteSteuerung
        '
        Me.BtnDeleteSteuerung.BackColor = System.Drawing.Color.OrangeRed
        Me.BtnDeleteSteuerung.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnDeleteSteuerung.Location = New System.Drawing.Point(10, 130)
        Me.BtnDeleteSteuerung.Name = "BtnDeleteSteuerung"
        Me.BtnDeleteSteuerung.Size = New System.Drawing.Size(98, 23)
        Me.BtnDeleteSteuerung.TabIndex = 6
        Me.BtnDeleteSteuerung.Text = "Steuerung entfernen"
        Me.BtnDeleteSteuerung.UseVisualStyleBackColor = False
        '
        'numericScaling
        '
        Me.numericScaling.Location = New System.Drawing.Point(10, 95)
        Me.numericScaling.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.numericScaling.Name = "numericScaling"
        Me.numericScaling.Size = New System.Drawing.Size(98, 44)
        Me.numericScaling.TabIndex = 4
        Me.numericScaling.Value = New Decimal(New Integer() {10, 0, 0, 0})
        '
        'lblYcoord
        '
        Me.lblYcoord.AutoSize = True
        Me.lblYcoord.Location = New System.Drawing.Point(10, 50)
        Me.lblYcoord.Name = "lblYcoord"
        Me.lblYcoord.Size = New System.Drawing.Size(146, 37)
        Me.lblYcoord.TabIndex = 3
        Me.lblYcoord.Text = "y: ycoord"
        '
        'lblXcoord
        '
        Me.lblXcoord.AutoSize = True
        Me.lblXcoord.Location = New System.Drawing.Point(10, 30)
        Me.lblXcoord.Name = "lblXcoord"
        Me.lblXcoord.Size = New System.Drawing.Size(146, 37)
        Me.lblXcoord.TabIndex = 2
        Me.lblXcoord.Text = "x: xcoord"
        '
        'lblsettings
        '
        Me.lblsettings.AutoSize = True
        Me.lblsettings.Location = New System.Drawing.Point(3, 3)
        Me.lblsettings.Name = "lblsettings"
        Me.lblsettings.Size = New System.Drawing.Size(208, 37)
        Me.lblsettings.TabIndex = 1
        Me.lblsettings.Text = "Einstellungen"
        '
        'btn_settingsExit
        '
        Me.btn_settingsExit.Image = CType(resources.GetObject("btn_settingsExit.Image"), System.Drawing.Image)
        Me.btn_settingsExit.Location = New System.Drawing.Point(186, 3)
        Me.btn_settingsExit.Name = "btn_settingsExit"
        Me.btn_settingsExit.Size = New System.Drawing.Size(25, 25)
        Me.btn_settingsExit.TabIndex = 0
        Me.btn_settingsExit.TabStop = False
        '
        'lblScrollScaling
        '
        Me.lblScrollScaling.AutoSize = True
        Me.lblScrollScaling.Location = New System.Drawing.Point(6, 73)
        Me.lblScrollScaling.Name = "lblScrollScaling"
        Me.lblScrollScaling.Size = New System.Drawing.Size(215, 37)
        Me.lblScrollScaling.TabIndex = 5
        Me.lblScrollScaling.Text = "Scroll scaling:"
        '
        'panel_kreuzweiche
        '
        Me.panel_kreuzweiche.BackColor = System.Drawing.Color.DimGray
        Me.panel_kreuzweiche.Controls.Add(Me.Pic_kor)
        Me.panel_kreuzweiche.Controls.Add(Me.Pic_kol)
        Me.panel_kreuzweiche.Controls.Add(Me.Pic_k)
        Me.panel_kreuzweiche.Controls.Add(Me.lblkreuzweiche)
        Me.panel_kreuzweiche.Location = New System.Drawing.Point(306, 332)
        Me.panel_kreuzweiche.Name = "panel_kreuzweiche"
        Me.panel_kreuzweiche.Size = New System.Drawing.Size(200, 100)
        Me.panel_kreuzweiche.TabIndex = 12
        Me.panel_kreuzweiche.Visible = False
        '
        'Pic_kor
        '
        Me.Pic_kor.Image = CType(resources.GetObject("Pic_kor.Image"), System.Drawing.Image)
        Me.Pic_kor.Location = New System.Drawing.Point(144, 50)
        Me.Pic_kor.Name = "Pic_kor"
        Me.Pic_kor.Size = New System.Drawing.Size(25, 25)
        Me.Pic_kor.TabIndex = 4
        Me.Pic_kor.TabStop = False
        '
        'Pic_kol
        '
        Me.Pic_kol.Image = CType(resources.GetObject("Pic_kol.Image"), System.Drawing.Image)
        Me.Pic_kol.Location = New System.Drawing.Point(31, 50)
        Me.Pic_kol.Name = "Pic_kol"
        Me.Pic_kol.Size = New System.Drawing.Size(25, 25)
        Me.Pic_kol.TabIndex = 3
        Me.Pic_kol.TabStop = False
        '
        'Pic_k
        '
        Me.Pic_k.Image = CType(resources.GetObject("Pic_k.Image"), System.Drawing.Image)
        Me.Pic_k.Location = New System.Drawing.Point(87, 50)
        Me.Pic_k.Name = "Pic_k"
        Me.Pic_k.Size = New System.Drawing.Size(25, 25)
        Me.Pic_k.TabIndex = 2
        Me.Pic_k.TabStop = False
        '
        'lblkreuzweiche
        '
        Me.lblkreuzweiche.AutoSize = True
        Me.lblkreuzweiche.Location = New System.Drawing.Point(28, 0)
        Me.lblkreuzweiche.Name = "lblkreuzweiche"
        Me.lblkreuzweiche.Size = New System.Drawing.Size(289, 74)
        Me.lblkreuzweiche.TabIndex = 1
        Me.lblkreuzweiche.Text = "Weichenart wählen" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "      off-position"
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(19.0!, 37.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Silver
        Me.CancelButton = Me.Button1
        Me.ClientSize = New System.Drawing.Size(918, 675)
        Me.Controls.Add(Me.panel_kreuzweiche)
        Me.Controls.Add(Me.panel_settings)
        Me.Controls.Add(Me.panel_help)
        Me.Controls.Add(Me.Lb_error_log)
        Me.Controls.Add(Me.VSBau)
        Me.Controls.Add(Me.HSBau)
        Me.Controls.Add(Me.Tbprog)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Cursor = System.Windows.Forms.Cursors.Cross
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Name = "Form1"
        Me.ShowIcon = False
        Me.Text = "modeba"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.panel_help.ResumeLayout(False)
        Me.panel_help.PerformLayout()
        CType(Me.pic_helpExit, System.ComponentModel.ISupportInitialize).EndInit()
        Me.panel_settings.ResumeLayout(False)
        Me.panel_settings.PerformLayout()
        CType(Me.numericScaling, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btn_settingsExit, System.ComponentModel.ISupportInitialize).EndInit()
        Me.panel_kreuzweiche.ResumeLayout(False)
        Me.panel_kreuzweiche.PerformLayout()
        CType(Me.Pic_kor, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Pic_kol, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Pic_k, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents ImageList1 As System.Windows.Forms.ImageList
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents lbl_steuern As System.Windows.Forms.ToolStripLabel
    Friend WithEvents Btn_startsteuern As System.Windows.Forms.ToolStripButton
    Friend WithEvents Btn_addsteuern As System.Windows.Forms.ToolStripButton
    Friend WithEvents seperator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents Btn_save As System.Windows.Forms.ToolStripButton
    Friend WithEvents Btn_open As System.Windows.Forms.ToolStripButton
    Friend WithEvents Btn_new As System.Windows.Forms.ToolStripButton
    Friend WithEvents seperator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents lbl_bau As System.Windows.Forms.ToolStripLabel
    Friend WithEvents Btn_gleis As System.Windows.Forms.ToolStripButton
    Friend WithEvents Btn_besetzt As System.Windows.Forms.ToolStripButton
    Friend WithEvents Btn_weiche As System.Windows.Forms.ToolStripSplitButton
    Friend WithEvents Btn_ws As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Btn_wk As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Btn_löschen As System.Windows.Forms.ToolStripButton
    Friend WithEvents ImageList2 As System.Windows.Forms.ImageList
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents TimerBesMeld As System.Windows.Forms.Timer
    Friend WithEvents Tbprog As System.Windows.Forms.RichTextBox
    Friend WithEvents SaveFileDialog1 As System.Windows.Forms.SaveFileDialog
    Friend WithEvents LblComAnschluss As System.Windows.Forms.ToolStripComboBox
    Friend WithEvents HSBau As System.Windows.Forms.HScrollBar
    Friend WithEvents VSBau As System.Windows.Forms.VScrollBar
    Friend WithEvents seperator3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents Btn_code As System.Windows.Forms.ToolStripButton
    Friend WithEvents Btn_error As System.Windows.Forms.ToolStripButton
    Friend WithEvents Btn_help As System.Windows.Forms.ToolStripButton
    Friend WithEvents Lb_error_log As System.Windows.Forms.ListBox
    Friend WithEvents seperator4 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents BtnAusführen As System.Windows.Forms.ToolStripButton
    Friend WithEvents seperator5 As ToolStripSeparator
    Friend WithEvents btn_settings As ToolStripButton
    Friend WithEvents panel_help As Panel
    Friend WithEvents lblhelp As Label
    Friend WithEvents pic_helpExit As PictureBox
    Friend WithEvents panel_settings As Panel
    Friend WithEvents lblsettings As Label
    Friend WithEvents btn_settingsExit As PictureBox
    Friend WithEvents rtb_help As RichTextBox
    Friend WithEvents panel_kreuzweiche As Panel
    Friend WithEvents Pic_kor As PictureBox
    Friend WithEvents Pic_kol As PictureBox
    Friend WithEvents Pic_k As PictureBox
    Friend WithEvents lblkreuzweiche As Label
    Friend WithEvents numericScaling As NumericUpDown
    Friend WithEvents lblYcoord As Label
    Friend WithEvents lblXcoord As Label
    Friend WithEvents lblScrollScaling As Label
    Friend WithEvents BtnDeleteSteuerung As Button
    Friend WithEvents lblTimeOfDay As ToolStripLabel
    Friend WithEvents lblBesTimerWorking As Label
    Friend WithEvents lblBesTimerWorkingText As Label
End Class
