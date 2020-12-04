Imports System.IO.Ports
Imports System.IO

Public Class Form1

    Dim index As Integer 'angeklicktes object
    Public ComAdapter As New SerialPort ' serial Port

    Dim WithEvents BGW As New System.ComponentModel.BackgroundWorker With {.WorkerReportsProgress = True, .WorkerSupportsCancellation = True}
    'bachgroundworker , der thread ausfuehrt in dem user-code executed wird

    '*****************************************************************************************System (-layout)

    Private Sub Form1_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load

        Me.Cursor = Cursors.WaitCursor
        Try

            Me.Location = New Point(0, 0)
            Button1.Location = New Point(Me.Size.Width - 75, 0)
            'Programm beenden Button in obere rechte ecke setzen
            Lb_error_log.Items.Add("Clear all items")

        Catch ex As Exception
            MsgBox("Program konnte nicht gestartet werden", MsgBoxStyle.Critical, Title:="unbekannter fehler")
            Button1.PerformClick()
        End Try

        Try
            VSBau.Location = New Point(Me.Size.Width - 25, 33)
            HSBau.Location = New Point(0, Me.Size.Height - 25)

            VSBau.Size = New Size(25, Me.Size.Height - 58)
            HSBau.Size = New Size(Me.Size.Width - 25, 25)
            'positionierd die scrollbars an den raendern der form

            'positionierd code text box (diese ist aber am start unsichtbar)
            Tbprog.Location = New Point(1, 35)
            Tbprog.Size = New Size(Me.Size.Width - 2, Me.Size.Height - 35)

            bau = 1 'im gleisbau modus beginnen

            Steuerung_Bauen(k_str)
            k_str += 1
            strpanel(1).Visible = False 'erste steuerung setzen aber unsichtbar da im baumodus beginnen

            Quad_setzen(1)
            quad_box(1).Visible = True
            quad_box(1).Image = ImageList2.Images(0)

            Quad_setzen(2)
            quad_box(2).Image = ImageList2.Images(0)

            Quad_setzen(3)
            quad_box(3).BackColor = Color.Aquamarine
            'quadboxen erstellen

            scalingXY = 10
            'scaling factor fuer scrollbars 

        Catch ex As Exception
            Error_log("failed in building starting-buttens", True)
        End Try

        'H a r d w a r e:  ComSchnittstelle suchen:

        Try 'ist eine Com-Schnittstelle vorhanden? *)

            Dim portname As Array = System.IO.Ports.SerialPort.GetPortNames
            For i As Integer = LBound(portname) To UBound(portname)
                LblComAnschluss.Items.Add(portname(i))
            Next

            'Lösung ohne Combobox: Eigenschaften der 0. Schnittstelle eingeben:
            With ComAdapter 'Einrichtung der Schnittstelle:
                .PortName = InputBox("COM?") ' portname(0)    'greife auf das 0. Element des Arrays Portname zu
                .RtsEnable = True 'Request to Send (RTS)-Signal  wichtig???
                .DataBits = 8
                .BaudRate = 9600
                .Parity = IO.Ports.Parity.None
                '.StopBits = StopBits.Two
                .Open()
                LblComAnschluss.Text = ComAdapter.PortName
                'Anzeige, ob Realbetrieb oder Simulation läuft:
                LblComAnschluss.BackColor = Color.GreenYellow
            End With
        Catch ex As Exception ' *)keine Schnittstelle vorhanden? Dann weiter im Simulationsmodus o. Abbruch:
            LblComAnschluss.Text = "Testmodus"
            LblComAnschluss.BackColor = Color.Red
        End Try

        Me.Cursor = Cursors.Default

    End Sub

    Private Sub LblComAnschluss_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles LblComAnschluss.TextChanged
        Try

            Dim comname_text As String = LblComAnschluss.Text.ToUpper
            'gross-klein schreibung unwichtig da .toUpper

            If LSet(comname_text, 3) = "COM" Then
                If Len(comname_text) > 3 Then
                    ComAdapter.PortName = comname_text
                    LblComAnschluss.BackColor = Color.GreenYellow
                    'wenn text "COM?" dann portname auf diese comstelle setzen und lbl gruen markieren
                End If
            Else
                LblComAnschluss.BackColor = Color.Red
                'sonst da keine gültige schnittstelle lbl rot markieren
            End If

        Catch ex As Exception
            Error_log("COM-error :" & LblComAnschluss.Text, True)
        End Try
    End Sub

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        Try

            Me.Cursor = Cursors.WaitCursor
            'stopt alle zuege
            For i As Integer = 1 To 128
                Fahren(i, 0, 0)
            Next

            Me.Cursor = Cursors.Default

        Catch ex As Exception
            Error_log("couldn't stop train", True)
        End Try
        Application.Exit()
        'Exit button beendet Programm
    End Sub

    Private Sub ShowHideBauButtons(ShowHide As Boolean)
        Btn_gleis.Visible = ShowHide
        Btn_besetzt.Visible = ShowHide
        Btn_weiche.Visible = ShowHide
        Btn_löschen.Visible = ShowHide
        lbl_bau.Visible = ShowHide
        seperator2.Visible = ShowHide

        quad_box(1).Visible = ShowHide
    End Sub

    Private Sub ShowHideSaveButtons(ShowHide As Boolean)
        Btn_save.Visible = ShowHide
        Btn_open.Visible = ShowHide
        Btn_new.Visible = ShowHide
        seperator1.Visible = ShowHide
    End Sub

    Private Sub ShowHideSteuernButtons(ShowHide As Boolean)
        Btn_addsteuern.Visible = ShowHide

        'macht alle schnell-steuerungen sichtbar
        For i As Integer = 1 To k_str - 1
            strpanel(i).Visible = ShowHide
        Next
    End Sub

    Private Sub ShowHideCodeButtons(SHowHide As Boolean)
        Tbprog.Visible = SHowHide
        BtnAusführen.Visible = SHowHide
        seperator4.Visible = SHowHide

        HSBau.Visible = Not SHowHide
        VSBau.Visible = Not SHowHide
    End Sub

    Private Sub Btn_startsteuern_Click(sender As System.Object, e As System.EventArgs) Handles Btn_startsteuern.Click

        Me.Cursor = Cursors.WaitCursor

        If bau > -1 Then

            'versteckt alle bau buttons zeigt steuerungs buttons an
            bau = -1
            ShowHideBauButtons(False)
            ShowHideSaveButtons(False)
            ShowHideSteuernButtons(True)

            Btn_startsteuern.Text = "Bauen"
            Btn_startsteuern.Image = ImageList2.Images(4)
            TimerBesMeld.Enabled = True

            'beendet bau modus und setzt alle weichen auf 0
            Try
                For i As Integer = LBound(weiche) To UBound(weiche)
                    If Wary(i, 1) <> 0 Then

                        Dim bytearray() As Byte = {Wary(i, 1) + 128, 0}
                        ComAdapter.Write(bytearray, 0, 2)
                    End If
                Next

            Catch ex As Exception
                Error_log("couldn't turn weiche off", True)
            End Try

        Else
            'macht alle bau buttons sichtbar und versteckt steuerungs buttons
            bau = 0
            ShowHideBauButtons(True)
            ShowHideSaveButtons(True)
            ShowHideSteuernButtons(False)

            Btn_startsteuern.Text = "Steuern"
            Btn_startsteuern.Image = ImageList2.Images(3)
            TimerBesMeld.Enabled = False
            Button1.Select()

            'startet baumodus und stoppt alle züge

            Try
                For i As Integer = 1 To 128
                    Fahren(i, 0, 0)
                Next
            Catch ex As Exception
                Error_log("couldn't stop train", True)
            End Try

            'eventuell panels im baumodeus unsichtbar und im fahren wieder sichtbar machen 
        End If

        Me.Cursor = Cursors.Default
    End Sub

    Public quad_box(3) As PictureBox

    Private Sub Quad_setzen(ByVal k As Integer)

        'erstellt k-te quad_box
        quad_box(k) = New PictureBox

        With quad_box(k)
            .Parent = Me
            .Parent.Controls.Add(quad_box(k))
            .Name = "quad_box" & k
            .Size = New Size(25, 25)
            .Location = New Point(-50, -50)
            .BackColor = Color.Silver
            .TabIndex = k
            .Visible = False
            AddHandler .Click, AddressOf Bau_click
        End With
    End Sub

    Private Function GetIndex(ClickedItem As Object, objectarray As Object) As Integer
        Try

            For i As Integer = LBound(objectarray) To UBound(objectarray)
                If objectarray(i) Is ClickedItem Then
                    Return i
                End If
            Next
            Return UBound(objectarray)
        Catch ex As Exception
            Lb_error_log.Items.Add("couldn't GetType index")
            Return 1
        End Try
    End Function

    '*****************************************************************************************Steuerungen Bauen

    Private ReadOnly strpanel(128) As Panel
    Private ReadOnly strscroll(128) As HScrollBar
    Private ReadOnly strbtnlicht(128) As Button
    Private ReadOnly strtb(128) As TextBox
    Private ReadOnly strlbltempo(128) As Label
    Private ReadOnly strbtnstop(128) As Button
    Dim k_str As Integer = 1

    Private Sub Btn_addsteuern_Click(sender As System.Object, e As System.EventArgs) Handles Btn_addsteuern.Click
        Try
            If Contains(strpanel(k_str)) Then
                strpanel(k_str).Visible = True
                strpanel(k_str).Location = New Point(100, 100)
            Else
                Steuerung_Bauen(k_str)
            End If
            k_str += 1
        Catch ex As System.NullReferenceException
            Error_log("couldn't build steuerung :" & k_str, True)
        End Try
        'Fügt neue Steuerung hinzu und erhöht zähler
    End Sub

    Private Sub Steuerung_Bauen(k As Integer) 'k= zähler für steuerungen
        Try
            Me.Cursor = Cursors.WaitCursor

            strpanel(k) = New Panel

            With strpanel(k)

                .Parent = Me
                .Parent.Controls.Add(strpanel(k))
                .Name = "panel" & CStr(k)
                .TabIndex = k
                .Size = New Size(150, 120)
                .Location = New Point(100, 100)
                .Visible = True
                .BackColor = Color.Gray
                AddHandler .MouseCaptureChanged, AddressOf Strpanel_MouseCaptureChange
                AddHandler .MouseDown, AddressOf Strpanel_MouseDown
            End With
            'Panel zur steuerung erzeugt (Panel als "Form" für steuerung

            strscroll(k) = New HScrollBar

            With strscroll(k)

                .Parent = strpanel(k)
                .Parent.Controls.Add(strscroll(k))
                .Name = "scrolltempo" & CStr(k)
                .TabIndex = k
                .Size = New Size(130, 30)
                .Location = New Point(10, 50)
                .Minimum = -31
                .Maximum = 31
                .Value = 0
                .LargeChange = 1
                .Visible = True
                AddHandler .Scroll, AddressOf Strtempo_scroll
            End With
            'Fügt scrolleiste für tempo dem Panel hinzu

            strtb(k) = New TextBox

            With strtb(k)

                .Parent = strpanel(k)
                .Parent.Controls.Add(strtb(k))
                .Name = "tb" & CStr(k)
                .TabIndex = k
                .Size = New Size(40, 23)
                .Location = New Point(10, 10)
                .Visible = True
                .BackColor = Color.White
            End With
            'Textbox für zugnummer

            strbtnlicht(k) = New Button

            With strbtnlicht(k)

                .Parent = strpanel(k)
                .Parent.Controls.Add(strbtnlicht(k))
                .Name = "btnLicht" & CStr(k)
                .Text = "Licht an"
                .TabIndex = k
                .Size = New Size(55, 27)
                .Location = New Point(85, 85)
                .Visible = True
                .BackColor = Color.Gray
                AddHandler .Click, AddressOf Strlicht_click
            End With
            'button für licht

            strlbltempo(k) = New Label

            With strlbltempo(k)

                .Parent = strpanel(k)
                .Parent.Controls.Add(strlbltempo(k))
                .Name = "lbltempo" & CStr(k)
                .Text = "Tempo: 0"
                .TabIndex = k
                .Size = New Size(90, 23)
                .Location = New Point(50, 13)
                .Visible = True
                .BackColor = Color.Gray
            End With
            'label zur tempoanzeige

            strbtnstop(k) = New Button

            With strbtnstop(k)

                .Parent = strpanel(k)
                .Parent.Controls.Add(strbtnstop(k))
                .Name = "btnStop" & CStr(k)
                .Text = "Stop"
                .TabIndex = k
                .Size = New Size(55, 27)
                .Location = New Point(15, 85)
                .Visible = True
                .BackColor = Color.Red
                AddHandler .Click, AddressOf StrStop_click
            End With

            Me.Cursor = Cursors.Default

        Catch ex As Exception
            Error_log("couldn't build steuerung :" & k, True)
        End Try
    End Sub

    Public beweg_str_panel As Integer
    'if = 0 then no panel is moved else move panel with this value to the cursors position

    Private Sub Strpanel_MouseCaptureChange(ByVal sender As Object, ByVal e As System.EventArgs)
        If beweg_str_panel > 0 Then
            beweg_str_panel = 0
            Me.Cursor = Cursors.Default
            'wird das panel losgelassen soll es nicht mehr bewegt werden
        End If
    End Sub

    Private Sub Strpanel_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)

        Dim itemClicked As Panel = CType(sender, Panel)

        index = GetIndex(itemClicked, strpanel)
        beweg_str_panel = index
        strpanel(index).BringToFront()
        Me.Cursor = Cursors.SizeAll

        'wird das panel angeklickt wird es in bewegungsmodus gesetzt
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick

        'verfolgt maus wenn ein panel im bewegungsmodus ist
        If beweg_str_panel > 0 Then
            strpanel(beweg_str_panel).Location = New Point(Cursor.Position.X - 2, Cursor.Position.Y - 2)

        End If

        '****************************************************move helpwindow

        Try
            If moveHelpWindow Then
                panel_help.Location = New Point(Cursor.Position.X - 2, Cursor.Position.Y - 2)
                'bewegt help-window
            End If

        Catch ex As Exception
            Error_log("failed to move help-panel", True)
        End Try

        '****************************************************move SettingWindow

        Try
            If moveSettingWindow Then
                panel_settings.Location = New Point(Cursor.Position.X - 2, Cursor.Position.Y - 2)
                'bewegt settings-window
            End If

        Catch ex As Exception
            Error_log("failed to move Setting-panel", True)
        End Try


        '*******************************************************Bauen

        Try
            quad_box(1).SendToBack()
            quad_box(3).BringToFront()
            Button1.BringToFront()
            lblTimeOfDay.Text = TimeOfDay
            lblXcoord.Text = CInt(quad_box(1).Location.X - 12.5)
            lblYcoord.Text = CInt(quad_box(1).Location.Y - 12.5)

            If bau = 0 Or bau = 5 Or bau = 2 Then
                quad_box(1).Location = New Point(Cursor.Position.X - 12.5, Cursor.Position.Y - 12.5)
                'if bau 5 (loeschen) or 2 (besmelder) place at cursor position
            ElseIf bau = 1 Then
                'if bau 1 then quad-box is placed every 12.5px bc we are building gleis or besetztmelder to fit the connections to weichen
                quad_box(1).Location = New Point((CInt(Cursor.Position.X / 12.5)) * 12.5 - 12.5, (CInt(Cursor.Position.Y / 12.5)) * 12.5 - 12.5)
                If quad_box(1).Location.X = quad_box(2).Location.X And quad_box(2).Visible = True Then
                    If quad_box(1).Location.Y > quad_box(2).Location.Y Then
                        quad_box(3).Size = New Size(1, quad_box(1).Location.Y - quad_box(2).Location.Y)
                        quad_box(3).Location = New Point(quad_box(1).Location.X + 12.5, quad_box(2).Location.Y + 12.5)
                    Else
                        quad_box(3).Size = New Size(1, quad_box(2).Location.Y - quad_box(1).Location.Y)
                        quad_box(3).Location = New Point(quad_box(1).Location.X + 12.5, quad_box(1).Location.Y + 12.5)
                    End If
                    quad_box(3).Visible = True
                ElseIf quad_box(1).Location.Y = quad_box(2).Location.Y And quad_box(2).Visible = True Then
                    If quad_box(1).Location.X > quad_box(2).Location.X Then
                        quad_box(3).Size = New Size(quad_box(1).Location.X - quad_box(2).Location.X, 1)
                        quad_box(3).Location = New Point(quad_box(2).Location.X + 12.5, quad_box(1).Location.Y + 12.5)
                    Else
                        quad_box(3).Size = New Size(quad_box(2).Location.X - quad_box(1).Location.X, 1)
                        quad_box(3).Location = New Point(quad_box(1).Location.X + 12.5, quad_box(1).Location.Y + 12.5)
                    End If
                    quad_box(3).Visible = True
                Else
                    quad_box(3).Visible = False
                End If
            ElseIf bau = 3 Or bau = 4 Then
                'if quad is 3 or 4 then we are building weichen and the box is placed every 25px to fit the connection to gleis
                quad_box(1).Location = New Point((CInt(Cursor.Position.X / 25)) * 25 - 12.5, (CInt(Cursor.Position.Y / 25)) * 25 - 12.5)
            End If

            If bau = 3 And quad_box(2).Visible = True And xy(3) = -1 Then
                'if baumode = placing weichen and screen already once clicked adjust position of quad_box and change image to fit cursor rotation
                If (Cursor.Position.X - xy(1)) ^ 2 > (Cursor.Position.Y - xy(2)) ^ 2 Then
                    If Cursor.Position.X > xy(1) Then
                        If Cursor.Position.Y > xy(2) Then
                            quad_box(2).Image = ImageList1.Images(4)
                            xy(4) = 4
                        Else
                            quad_box(2).Image = ImageList1.Images(2)
                            xy(4) = 2
                        End If
                        quad_box(2).Location = New Point(xy(1), xy(2) - 12.5)
                    Else
                        If Cursor.Position.Y > xy(2) Then
                            quad_box(2).Image = ImageList1.Images(6)
                            xy(4) = 6
                        Else
                            quad_box(2).Image = ImageList1.Images(0)
                            xy(4) = 0
                        End If
                        quad_box(2).Location = New Point(xy(1) - 25, xy(2) - 12.5)
                    End If
                Else
                    If Cursor.Position.Y > xy(2) Then
                        If Cursor.Position.X > xy(1) Then
                            quad_box(2).Image = ImageList1.Images(14)
                            xy(4) = 14
                        Else
                            quad_box(2).Image = ImageList1.Images(8)
                            xy(4) = 8
                        End If
                        quad_box(2).Location = New Point(xy(1) - 12.5, xy(2))
                    Else
                        If Cursor.Position.X > xy(1) Then
                            quad_box(2).Image = ImageList1.Images(12)
                            xy(4) = 12
                        Else
                            quad_box(2).Image = ImageList1.Images(10)
                            xy(4) = 10
                        End If
                        quad_box(2).Location = New Point(xy(1) - 12.5, xy(2) - 25)
                    End If
                End If
            End If
        Catch ex As Exception
            Error_log("failed to build object :" & bau, True)
        End Try
    End Sub

    Dim bau As Integer
    'button ändert den baumodus und setzt löschenmodus zurück wenn nicht im fahrmodus (-1) gearbeitet wird

    Private ReadOnly gleis(1000000) As PictureBox
    Dim g As Integer = 0
    Dim gTag As Integer = 1 'tag gleicher zahl = eine linie

    Private ReadOnly lbldecb(255) As Label
    Dim b As Integer = 0

    Private ReadOnly lbldec(255) As Label
    Private ReadOnly weiche(255) As PictureBox
    Dim W As Integer = 0

    Public Wary(255, 4) As Integer
    '1 decoder
    '2 byte
    '3 bild
    '4 an/aus
    Public Bary(255, 3) As Integer
    '1 decoder
    '2 byte
    '3 gleisTag
    'variabeln für den bau

    Private Sub Bau_setMode(bau_mode As Integer, quad_image As Integer)
        If bau > -1 Then
            bau = bau_mode
            'changes bau modus and quad image to fit bau modus
            quad_box(1).Image = ImageList2.Images(quad_image)
        End If
    End Sub

    Private Sub Btn_gleis_Click(sender As System.Object, e As System.EventArgs) Handles Btn_gleis.Click
        Bau_setMode(1, 0)
    End Sub

    Private Sub Btn_besetzt_Click(sender As System.Object, e As System.EventArgs) Handles Btn_besetzt.Click
        Bau_setMode(2, 1)
    End Sub

    Private Sub Btn_weiche_ButtonClick(sender As System.Object, e As System.EventArgs) Handles Btn_weiche.ButtonClick
        Bau_setMode(3, 0)
    End Sub

    Private Sub Btn_ws_Click(sender As System.Object, e As System.EventArgs) Handles Btn_ws.Click
        Btn_weiche.PerformButtonClick()
        'führt selbes wie btn_weiche aus
    End Sub

    Private Sub Btn_wk_Click(sender As System.Object, e As System.EventArgs) Handles Btn_wk.Click
        Bau_setMode(4, 0)
    End Sub

    Private Sub Btn_baulöschen_Click(sender As System.Object, e As System.EventArgs) Handles Btn_löschen.Click
        Bau_setMode(5, 2)
    End Sub

    Public xy(4) As Integer

    Private Sub Place_QuadBox2()
        xy(1) = quad_box(1).Location.X + 12.5
        xy(2) = quad_box(1).Location.Y + 12.5
        quad_box(2).Location = New Point(quad_box(1).Location.X, quad_box(1).Location.Y)
        quad_box(2).Image = ImageList2.Images(0)
        quad_box(2).Visible = True
    End Sub

    Private Sub Bau_click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            If bau = 1 Then
                If xy(1) = 0 Then
                    'first coords of gleis place quadbox to indicate point and store coords
                    Place_QuadBox2()
                Else
                    xy(3) = quad_box(1).Location.X + 12.5
                    xy(4) = quad_box(1).Location.Y + 12.5
                    'second coords
                    'calculate line from point(1,2) to point(3,4) and build line

                    gTag += 1

                    If xy(1) = xy(3) Then
                        If xy(2) > xy(4) Then
                            PlaceGleis(xy(1), xy(4), 2)
                            gleis(g - 1).Size = New Size(2, xy(2) - xy(4))
                        Else
                            PlaceGleis(xy(1), xy(2), 2)
                            gleis(g - 1).Size = New Size(2, xy(4) - xy(2))
                        End If
                    ElseIf xy(2) = xy(4) Then
                        If xy(1) > xy(3) Then
                            PlaceGleis(xy(3), xy(2), xy(1) - xy(3))
                        Else
                            PlaceGleis(xy(1), xy(2), xy(3) - xy(1))
                        End If
                    Else

                        Dim m As Double = (xy(3) - xy(1)) / (xy(4) - xy(2))
                        'steigung der geraden

                        Dim upDown As Integer
                        'if line goes up or down in y-direction
                        If m > 0 Then
                            upDown = 1
                        Else
                            upDown = -1
                        End If

                        Dim s_plus As Integer
                        'make line thinner or fatter 
                        If (m ^ 2) < (1 / 4) Then
                            s_plus = 2
                        Else
                            s_plus = 1
                        End If

                        If xy(1) < xy(3) Then

                            For i As Integer = xy(2) To xy(4) - upDown Step upDown
                                PlaceGleis((i - xy(2)) * m + xy(1), i, (m * upDown) + s_plus)
                            Next
                        ElseIf xy(1) > xy(3) Then

                            For i As Integer = xy(2) To xy(4) + upDown Step upDown * -1
                                PlaceGleis((i - xy(2)) * m + xy(1) - (m * upDown) + s_plus, i, (m * upDown) + s_plus)
                            Next
                        End If
                    End If

                    xy(1) = 0
                    quad_box(2).Visible = False
                End If
            ElseIf bau = 2 Then
                'setzt label fuer decoder;byte
                PlaceBesetztmelder(quad_box(1).Location.X, quad_box(1).Location.Y)

                'fragt decoder;byte werte ab und speichert diese im besetztmelderarray
                Bary(b - 1, 1) = InputBox("Decoder bitte eingeben:", Title:="Decoder")
                Bary(b - 1, 2) = InputBox("Byte bitte eingeben:", Title:="Byte")
                lbldecb(b - 1).Text = Bary(b - 1, 1) & " ; " & Bary(b - 1, 2)
                'zeigt dec;byte an

                Dim decVorhanden As Boolean = False

                'wenn eingegebener decoder bereits im besetztmelder-test-array vorhanden decvor = true
                For i As Integer = LBound(besdecoder) To UBound(besdecoder)
                    If besdecoder(i) = Bary(b - 1, 1) Then
                        decVorhanden = True
                        Exit For
                    End If

                Next
                'sollte eingegebener dec neu sein alle elemente im array eins nach hinten verschieben
                If Not decVorhanden Then
                    For i As Integer = 255 To 2 Step -1
                        besdecoder(i) = besdecoder(i - 1)
                    Next
                    besdecoder(1) = Bary(index, 1)
                    'und neune dec hinzufuegen
                End If
                'besdecoder-array fuer abfrage von besetztmeldern damit nicht bary benutzt wird
                'so werden dec's nicht 2 mal abgefragt

            ElseIf bau = 3 Then
                If xy(1) = 0 Then
                    'coords of weiche place quadbox to indicate clicked point
                    Place_QuadBox2()
                    xy(3) = -1

                ElseIf xy(3) = -1 Then
                    'second click after choosing the orientation of the weiche
                    Select Case xy(4)
                        Case 0
                            PlaceWeiche(xy(1) - 25, xy(2) - 12.5, xy(4), 3)
                        Case 2
                            PlaceWeiche(xy(1), xy(2) - 12.5, xy(4), 3)
                        Case 4
                            PlaceWeiche(xy(1), xy(2) - 12.5, xy(4), 1)
                        Case 6
                            PlaceWeiche(xy(1) - 25, xy(2) - 12.5, xy(4), 1)
                        Case 8
                            PlaceWeiche(xy(1) - 12.5, xy(2), xy(4), 2)
                        Case 10
                            PlaceWeiche(xy(1) - 12.5, xy(2) - 25, xy(4), 2)
                        Case 12
                            PlaceWeiche(xy(1) - 12.5, xy(2) - 25, xy(4), 4)
                        Case 14
                            PlaceWeiche(xy(1) - 12.5, xy(2), xy(4), 4)
                    End Select

                    quad_box(2).Visible = False
                    xy(1) = 0

                    Wary(W - 1, 1) = InputBox("Decoder bitte eingeben:", Title:="Decoder")
                    Wary(W - 1, 2) = InputBox("Byte bitte eingeben:", Title:="Byte")
                    lbldec(W - 1).Text = Wary(W - 1, 1) & " ; " & Wary(W - 1, 2)
                End If
            ElseIf bau = 4 Then
                'special case for kreuzweiche choose orientation from special window
                'therefore no second click needed

                Place_QuadBox2()

                panel_kreuzweiche.Location = New Point((Me.Size.Width - 200) / 2, (Me.Size.Height - 100) / 2)

                orientationOfKweiche = 0

                Pic_k.BackColor = Color.DimGray
                Pic_kol.BackColor = Color.DimGray
                Pic_kor.BackColor = Color.DimGray

                panel_kreuzweiche.Visible = True
                panel_kreuzweiche.BringToFront()

            End If

        Catch ex As Exception
            Error_log("couldn't build object :" & bau & " (" & xy(1) & ")", True)
        End Try
    End Sub

    Dim orientationOfKweiche As Integer

    '**kreuzweichen auswaehlen
    Private Sub Pic_k_Click(sender As Object, e As EventArgs) Handles Pic_k.Click
        If orientationOfKweiche = 0 Then
            orientationOfKweiche = 16
            Pic_k.BackColor = Color.Blue
            lblkreuzweiche.Text = "Weichenart wählen" & vbLf & "      on-position"
        Else
            If orientationOfKweiche = 20 Then
                PlaceWeiche(xy(1), xy(2), orientationOfKweiche, 5)
            Else
                PlaceWeiche(xy(1), xy(2), orientationOfKweiche + 2, 5)
            End If

            panel_kreuzweiche.Visible = False
            lblkreuzweiche.Text = "Weichenart wählen" & vbLf & "      off-position"
            quad_box(2).Visible = False

            Wary(W - 1, 1) = InputBox("Decoder bitte eingeben:", Title:="Decoder")
            Wary(W - 1, 2) = InputBox("Byte bitte eingeben:", Title:="Byte")
            lbldec(W - 1).Text = Wary(W - 1, 1) & " ; " & Wary(W - 1, 2)

            xy(1) = 0
        End If
    End Sub

    Private Sub Pic_kol_Click(sender As Object, e As EventArgs) Handles Pic_kol.Click
        If orientationOfKweiche = 0 Then
            orientationOfKweiche = 20
            Pic_kol.BackColor = Color.Blue
            lblkreuzweiche.Text = "Weichenart wählen" & vbLf & "      on-position"
        Else
            PlaceWeiche(xy(1), xy(2), orientationOfKweiche, 5)

            panel_kreuzweiche.Visible = False
            lblkreuzweiche.Text = "Weichenart wählen" & vbLf & "      off-position"
            quad_box(2).Visible = False

            Wary(W - 1, 1) = InputBox("Decoder bitte eingeben:", Title:="Decoder")
            Wary(W - 1, 2) = InputBox("Byte bitte eingeben:", Title:="Byte")
            lbldec(W - 1).Text = Wary(W - 1, 1) & " ; " & Wary(W - 1, 2)

            xy(1) = 0
        End If
    End Sub

    Private Sub Pic_kor_Click(sender As Object, e As EventArgs) Handles Pic_kor.Click
        If orientationOfKweiche = 0 Then
            orientationOfKweiche = 24
            Pic_kor.BackColor = Color.Blue
            lblkreuzweiche.Text = "Weichenart wählen" & vbLf & "      on-position"
        Else

            PlaceWeiche(xy(1), xy(2), orientationOfKweiche + 2, 5)

            panel_kreuzweiche.Visible = False
            lblkreuzweiche.Text = "Weichenart wählen" & vbLf & "      off-position"
            quad_box(2).Visible = False

            Wary(W - 1, 1) = InputBox("Decoder bitte eingeben:", Title:="Decoder")
            Wary(W - 1, 2) = InputBox("Byte bitte eingeben:", Title:="Byte")
            lbldec(W - 1).Text = Wary(W - 1, 1) & " ; " & Wary(W - 1, 2)

            xy(1) = 0
        End If
    End Sub

    'placing gleis at x y wich size s
    Private Sub PlaceGleis(ByVal x As Integer, ByVal y As Integer, ByVal s As Integer)
        Try
            gleis(g) = New PictureBox

            With gleis(g)

                .Parent = Me
                .Parent.Controls.Add(gleis(g))
                .Name = "gleis" & CStr(g)
                .TabIndex = g
                .Tag = gTag
                .Size = New Size(s, 2)
                .Location = New Point(x, y)
                .Visible = True
                .BackColor = Color.Black
                .BringToFront()
                AddHandler .MouseMove, AddressOf Gleis_MouseMove
            End With
            g += 1

            Me.Update()
        Catch ex As Exception
            Error_log("failed to build gleis :" & g - 1, True)
        End Try
    End Sub

    'place weiche at x y with image img and rotation r (rotation of lbldec)
    Private Sub PlaceWeiche(ByVal x As Integer, ByVal y As Integer, ByVal img As Integer, ByVal r As Integer)
        Try
            weiche(W) = New PictureBox

            With weiche(W)

                .Parent = Me
                .Parent.Controls.Add(weiche(W))
                .Name = "Weiche" & CStr(W)
                .TabIndex = W
                .Size = New Size(25, 25)
                .Location = New Point(x, y)
                .Visible = True
                .BringToFront()
                .Image = ImageList1.Images(img)
                AddHandler .Click, AddressOf Weiche_click
                AddHandler .MouseMove, AddressOf Weiche_MouseMove
            End With

            lbldec(W) = New Label

            With lbldec(W)
                .Parent = Me
                .Parent.Controls.Add(lbldec(W))
                .Name = "lbl_decoder" & CStr(W)
                .TabIndex = W
                .Visible = True
                .BackColor = Color.Transparent
                .Text = "dec ; b"
                AddHandler .MouseMove, AddressOf Lbldec_MouseMove
            End With

            If r = 1 Then
                lbldec(W).Location = New Point(x - 10, y - 30)
            ElseIf r = 2 Then
                lbldec(W).Location = New Point(x + 30, y + 2.5)
            ElseIf r = 3 Then
                lbldec(W).Location = New Point(x - 10, y + 30)
            ElseIf r = 4 Then
                lbldec(W).Location = New Point(x - 45, y + 2.5)
            Else
                lbldec(W).Location = New Point(x - 45, y - 30)
            End If

            Wary(W, 3) = img
            W += 1
        Catch ex As Exception
            Error_log("failed to build weiche :" & W - 1, True)
        End Try
    End Sub

    'place bes-lbl at x y
    Private Sub PlaceBesetztmelder(ByVal x As Integer, ByVal y As Integer)
        Try
            lbldecb(b) = New Label

            With lbldecb(b)
                .Parent = Me
                .Parent.Controls.Add(lbldecb(b))
                .Name = "lbl_besmelddecoder" & CStr(b)
                .TabIndex = b
                .Location = New Point(x, y)
                .Visible = True
                .BackColor = Color.Transparent
                .Text = "dec ; b"
                AddHandler .MouseMove, AddressOf Lbldecb_MouseMove
            End With

            b += 1
        Catch ex As Exception
            Error_log("failed to build besetztmelder :" & b - 1, True)
        End Try
    End Sub

    'if loeschen on and cursor hovers over gleis delete gleisabschnitt if bes on and cursor hovers over gleis make gleisabschnitt bes-melder
    Private Sub Gleis_MouseMove(sender As Object, e As System.Windows.Forms.MouseEventArgs)

        Dim itemMove As PictureBox = CType(sender, PictureBox)
        index = GetIndex(itemMove, gleis)

        Dim testgtag As Integer = gleis(index).Tag

        If gleis(index).BackColor = Color.Blue Then
            Exit Sub
        End If

        For i As Integer = 0 To g - 1
            If gleis(i).Tag = testgtag Then

                If bau = 2 Then
                    If Bary(b, 3) = 0 Or Bary(b, 3) = testgtag Then
                        gleis(i).BackColor = Color.Blue
                        Bary(b, 3) = testgtag
                    End If

                ElseIf bau = 5 Then

                    If Me.Controls.Contains(gleis(i)) Then
                        RemoveHandler gleis(i).MouseMove, AddressOf Gleis_MouseMove
                        Me.Controls.Remove(gleis(i))
                        gleis(i).Dispose()
                    End If
                End If
            End If
        Next
        'löscht gleis wenn löschen an und über gleis gefahren wird
    End Sub

    Private Sub Weiche_MouseMove(sender As Object, e As System.Windows.Forms.MouseEventArgs)
        Try
            If bau = 5 Then

                Dim itemMove As PictureBox = CType(sender, PictureBox)

                index = GetIndex(itemMove, weiche)

                Remove_weiche(index)
            End If
            'löscht weiche (sihe gleis)

        Catch ex As Exception
            Error_log("failed to get weichen-index :" & index, True)
        End Try
    End Sub

    Private Sub Remove_weiche(ByVal index_weiche As Integer)
        Try

            If Me.Controls.Contains(weiche(index_weiche)) Then
                RemoveHandler weiche(index_weiche).Click, AddressOf Weiche_click
                RemoveHandler weiche(index_weiche).MouseMove, AddressOf Weiche_MouseMove
                Me.Controls.Remove(weiche(index_weiche))
                weiche(index_weiche).Dispose()

                RemoveHandler lbldec(index_weiche).MouseMove, AddressOf Lbldec_MouseMove
                Me.Controls.Remove(lbldec(index_weiche))
                lbldec(index_weiche).Dispose()

                Wary(index_weiche, 1) = 0
                Wary(index_weiche, 2) = 0
                Wary(index_weiche, 3) = 0
            End If
        Catch ex As Exception
            Error_log("couldn't remove weiche :" & index_weiche, True)
        End Try

    End Sub

    Private Sub Lbldec_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)
        Try
            If bau = 5 Then

                Dim itemMove As Label = CType(sender, Label)

                index = GetIndex(itemMove, lbldec)

                Remove_weiche(index)
            End If
            'löscht label für decoder (sihe gleis)

        Catch ex As Exception
            Error_log("failed to get weichen-index :" & index, True)
        End Try
    End Sub

    Private Sub Lbldecb_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)
        Try
            If bau = 5 Then

                Dim itemMove As Label = CType(sender, Label)

                index = GetIndex(itemMove, lbldecb)

                If Me.Controls.Contains(lbldecb(index)) Then

                    RemoveHandler lbldecb(index).MouseMove, AddressOf Lbldecb_MouseMove
                    Me.Controls.Remove(lbldecb(index))
                    lbldecb(index).Dispose()
                    Bary(index, 1) = 0
                    Bary(index, 2) = 0
                    For i As Integer = 1 To g - 1
                        If gleis(i).Tag = Bary(index, 3) Then
                            gleis(i).BackColor = Color.Black
                        End If
                    Next
                    'make all gleise with correct tag black again
                    Bary(index, 3) = 0
                    'bary(1) dec
                    'bary(2) byte
                    'bary(3) tag des gleises
                End If
            End If
            'loescht besetztmelder

        Catch ex As Exception
            Error_log("couldn't remove bes :" & index, True)
        End Try
    End Sub

    '*************************************************bauwindow extension

    Dim oldY As Integer
    Dim oldX As Integer

    Dim scalingXY As Integer
    Private Sub VSBau_Scroll(sender As Object, e As ScrollEventArgs) Handles VSBau.Scroll

        For i As Integer = 0 To g - 1
            gleis(i).Location = New Point(gleis(i).Location.X, gleis(i).Location.Y + (oldY - VSBau.Value) * scalingXY)
        Next

        For i As Integer = 0 To W - 1
            weiche(i).Location = New Point(weiche(i).Location.X, weiche(i).Location.Y + (oldY - VSBau.Value) * scalingXY)
            lbldec(i).Location = New Point(lbldec(i).Location.X, lbldec(i).Location.Y + (oldY - VSBau.Value) * scalingXY)
        Next

        For i As Integer = 0 To b - 1
            lbldecb(i).Location = New Point(lbldecb(i).Location.X, lbldecb(i).Location.Y + (oldY - VSBau.Value) * scalingXY)
        Next

        oldY = VSBau.Value
        HSBau.BringToFront()
        VSBau.BringToFront()
        ToolStrip1.BringToFront()
        'move all objects up or down according to value of scrollbar
    End Sub

    Private Sub HSBau_Scroll(sender As Object, e As ScrollEventArgs) Handles HSBau.Scroll

        For i As Integer = 0 To g - 1
            gleis(i).Location = New Point(gleis(i).Location.X + (oldX - HSBau.Value) * scalingXY, gleis(i).Location.Y)
        Next

        For i As Integer = 0 To W - 1
            weiche(i).Location = New Point(weiche(i).Location.X + (oldX - HSBau.Value) * scalingXY, weiche(i).Location.Y)
            lbldec(i).Location = New Point(lbldec(i).Location.X + (oldX - HSBau.Value) * scalingXY, lbldec(i).Location.Y)
        Next

        For i As Integer = 0 To b - 1
            lbldecb(i).Location = New Point(lbldecb(i).Location.X + (oldX - HSBau.Value) * scalingXY, lbldecb(i).Location.Y)
        Next

        oldX = HSBau.Value
        HSBau.BringToFront()
        VSBau.BringToFront()
        ToolStrip1.BringToFront()
        'move all objects left or right according to value of scrollbar
    End Sub


    '****************************************************************************************Speichern/oeffnen/neu

    Private Sub Btn_new_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_new.Click
        'neu
        Try
            If Tbprog.Visible Then
                Tbprog.Text = "'Program/Fahrplan"
                'deletes written code if codewindow visible
            Else
                'deletes all objects
                For i As Integer = LBound(gleis) To UBound(gleis)

                    If Me.Controls.Contains(gleis(i)) Then
                        RemoveHandler gleis(i).MouseMove, AddressOf Gleis_MouseMove
                        Me.Controls.Remove(gleis(i))
                        gleis(i).Dispose()
                    End If
                Next

                For i As Integer = LBound(weiche) To UBound(weiche)

                    If Me.Controls.Contains(weiche(i)) Then
                        RemoveHandler weiche(i).Click, AddressOf Weiche_click
                        RemoveHandler weiche(i).MouseMove, AddressOf Weiche_MouseMove
                        Me.Controls.Remove(weiche(i))
                        weiche(i).Dispose()
                        Wary(i, 3) = 0
                    End If

                    If Me.Controls.Contains(lbldec(i)) Then
                        RemoveHandler lbldec(i).MouseMove, AddressOf Lbldec_MouseMove
                        Me.Controls.Remove(lbldec(i))
                        lbldec(i).Dispose()
                        Wary(i, 1) = 0
                        Wary(i, 2) = 0
                    End If
                Next

                For i As Integer = LBound(lbldecb) To UBound(lbldecb)
                    If Me.Controls.Contains(lbldecb(i)) Then

                        RemoveHandler lbldecb(i).MouseMove, AddressOf Lbldecb_MouseMove
                        Me.Controls.Remove(lbldecb(i))
                        lbldecb(i).Dispose()
                        Bary(i, 1) = 0
                        Bary(i, 2) = 0
                        Bary(i, 3) = 0
                    End If
                Next

                g = 0
                W = 0
                b = 0
                gTag = 1
            End If

        Catch ex As Exception
            Error_log("error occured while deleting data", True)
        End Try
    End Sub

    Private Sub Btn_open_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_open.Click
        Try

            OpenFileDialog1.ShowDialog()
            Me.Cursor = Cursors.WaitCursor

            Dim speich As String = My.Computer.FileSystem.ReadAllText(OpenFileDialog1.FileName)
            'text of opened file is read and stored 

            If Tbprog.Visible Then
                Tbprog.Text = ""
                Tbprog.ForeColor = Color.White
                Tbprog.Text = speich

                For i As Integer = 8 To Len(Tbprog.Text)
                    Highlighting(i, Tbprog.GetLineFromCharIndex(i))
                Next
                'if code window visible load as code-text
            Else
                'decript file to build gleisplan
                Dim str_speichern As String = speich

                Dim val(5) As Integer
                Dim k_val As Integer = 1

                For i As Integer = 1 To Len(speich)
                    Select Case Mid(str_speichern, 1, 1)
                        Case "g"
                            If val(4) = 2 Then
                                PlaceGleis(val(2), val(3), val(5))
                            Else
                                PlaceGleis(val(2), val(3), 1)
                                gleis(g - 1).Size = New Size(val(5), val(4))
                            End If
                            gleis(g - 1).Tag = val(1)

                            k_val = 1
                            For k As Integer = 1 To 5
                                val(k) = 0
                            Next

                        Case "i"
                            Wary(W, 1) = val(4)
                            Wary(W, 2) = val(5)
                            Wary(W, 3) = val(1)
                            Dim r As Integer

                            If val(1) = 0 Or val(1) = 1 Or val(1) = 2 Or val(1) = 3 Then
                                r = 3
                            ElseIf val(1) = 4 Or val(1) = 5 Or val(1) = 6 Or val(1) = 7 Then
                                r = 1
                            ElseIf val(1) = 8 Or val(1) = 9 Or val(1) = 10 Or val(1) = 11 Then
                                r = 2
                            ElseIf val(1) = 12 Or val(1) = 13 Or val(1) = 14 Or val(1) = 15 Then
                                r = 4
                            ElseIf val(1) = 16 Or val(1) = 17 Or val(1) = 18 Then
                                r = 5
                            End If

                            PlaceWeiche(val(2), val(3), val(1), r)
                            lbldec(W - 1).Text = Wary(W - 1, 1) & " ; " & Wary(W - 1, 2)

                            k_val = 1
                            For k As Integer = 1 To 5
                                val(k) = 0
                            Next

                        Case "t"
                            Bary(b, 1) = val(4)
                            Bary(b, 2) = val(5)
                            Bary(b, 3) = val(1)

                            For k As Integer = 1 To g - 1
                                If gleis(k).Tag = Bary(b, 3) Then
                                    gleis(k).BackColor = Color.Blue
                                End If
                            Next
                            PlaceBesetztmelder(val(2), val(3))
                            lbldecb(b - 1).Text = Bary(b - 1, 1) & " ; " & Bary(b - 1, 2)

                            k_val = 1
                            For k As Integer = 1 To 5
                                val(k) = 0
                            Next

                        Case "x"
                            k_val = 2
                        Case "y"
                            k_val = 3
                        Case "d"
                            k_val = 4
                        Case "b"
                            k_val = 5
                        Case "h"
                            k_val = 4
                        Case "w"
                            k_val = 5
                        Case "e"
                            Exit For
                        Case Else
                            val(k_val) = val(k_val) * 10 + CInt(Mid(str_speichern, 1, 1))
                    End Select

                    str_speichern = Mid(str_speichern, 2)
                Next
                'decriped text to build gleisplan
            End If

            Me.Cursor = Cursors.Default
        Catch ex As Exception
            Error_log("error occured while loading file", True)
        End Try
    End Sub

    Private Sub Btn_save_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_save.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            'saves the current gleisplan as .txt

            Dim SpeicherData As String = ""

            If Tbprog.Visible Then
                SpeicherData = Tbprog.Text
                'or save current code as .txt
            Else

                For i As Integer = 0 To g - 1
                    SpeicherData = SpeicherData & gleis(i).Tag & "x" & gleis(i).Location.X & "y" & gleis(i).Location.Y & "h" & gleis(i).Size.Height & "w" & gleis(i).Size.Width & "g"
                Next

                For i As Integer = 0 To W - 1
                    SpeicherData = SpeicherData & Wary(i, 3) & "x" & weiche(i).Location.X & "y" & weiche(i).Location.Y & "d" & Wary(i, 1) & "b" & Wary(i, 2) & "i"
                Next

                For i As Integer = 0 To b - 1
                    SpeicherData = SpeicherData & Bary(i, 3) & "x" & lbldecb(i).Location.X & "y" & lbldecb(i).Location.Y & "d" & Bary(i, 1) & "b" & Bary(i, 2) & "t"
                Next

                SpeicherData &= "e"
            End If

            SaveFileDialog1.Filter = "TXT Files (*.txt*)|*.txt"

            If SaveFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
                My.Computer.FileSystem.WriteAllText(SaveFileDialog1.FileName, SpeicherData, True)
            End If
            'saves everything as .txt

            Me.Cursor = Cursors.Default
        Catch ex As Exception
            Error_log("error occured while saving file", True)
        End Try
    End Sub

    '****************************************************Weichen ansteuern

    Private Sub Weiche_click(ByVal sender As System.Object, ByVal e As System.EventArgs)

        Dim itemClicked As PictureBox = CType(sender, PictureBox)

        index = GetIndex(itemClicked, weiche)

        Try
            If bau = -1 Then

                Dim wewe As Integer = 0
                'weichenwerte

                'wary4 ist wert 0 oder 1 ob weiche ON oder OFF
                'wary3 ist bild der weiche
                'wary1 ist decoder der weiche
                'wary2 is byte der weiche

                If Wary(index, 4) = 0 Then
                    'if weiche currently off then
                    Wary(index, 4) = 1
                    weiche(index).Image = ImageList1.Images(Wary(index, 3) + 1)
                    'change image to on 
                Else
                    Wary(index, 4) = 0
                    weiche(index).Image = ImageList1.Images(Wary(index, 3))
                    'change image to off
                End If

                'check for every weiche if they have the same decoder as the clicked weiche
                'if they have the same decoder add their value to wewe
                'so they dont get set off
                For i As Integer = LBound(weiche) To UBound(weiche)
                    If Wary(index, 1) = Wary(i, 1) Then
                        wewe += (Wary(i, 4) * (2 ^ (Wary(i, 2))))
                    End If
                Next

                Dim bytearray() As Byte = {Wary(index, 1) + 128, wewe}

                ComAdapter.Write(bytearray, 0, 2)

                'wenn nicht im bau modus(-1) dann wird signal gesendet dass weiche umgestellt werden soll
            End If
        Catch ex As Exception

            Error_log("failed to send weichen-data :" & Wary(index, 1) & ";" & Wary(index, 2), True)
        End Try
    End Sub

    '********************************************************************************Besetztmelder ansteuern

    Public besdecoder(255) As Integer

    Private Sub TimerBesMeld_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TimerBesMeld.Tick
        Try
            lblBesTimerWorking.Text = TimeOfDay
            Dim dec As Integer
            For i As Integer = LBound(besdecoder) To UBound(besdecoder)
                dec = Besetztanfrage(besdecoder(i)) 'dec = Besetztanfrage(i)   vorher ?
                UmrechnungInBit(dec)

                'for every element in besetztmelder-array (barry )
                For j As Integer = 1 To 255

                    If Bary(j, 1) = dec Then
                        If besary(Bary(j, 2) - 1) Then

                            For k As Integer = 1 To g
                                If gleis(k).Tag = Bary(j, 3) Then
                                    gleis(k).BackColor = Color.Aqua
                                End If
                            Next
                            'change color of gleis if correct tag
                        Else
                            For k As Integer = 1 To g
                                If gleis(k).Tag = Bary(j, 3) Then
                                    gleis(k).BackColor = Color.Blue
                                End If
                            Next
                        End If
                    End If
                Next
            Next
            lblBesTimerWorking.BackColor = Color.Green
        Catch ex As Exception
            Error_log("bes-timer error", False)
            lblBesTimerWorking.BackColor = Color.Red
        End Try
    End Sub

    Public besary(8) As Boolean

    Private Sub UmrechnungInBit(ByVal besetztWert As Integer)

        For i As Integer = 0 To 7
            If (besetztWert And (2 ^ i)) / (2 ^ i) = 1 Then
                besary(i + 1) = True
            Else
                besary(i + 1) = False
            End If
            'rechnet beswerte in binaer um
        Next
    End Sub

    Private Function Besetztanfrage(ByVal Addresse As Byte) As Byte
        Try
            'list beswerte aus
            ComAdapter.Write({Addresse, 0}, 0, 2)
            Return ComAdapter.ReadByte
        Catch ex As Exception
            Return 0
        End Try
    End Function

    '********************************************************************************Fahren

    Private Sub Strlicht_click(ByVal sender As System.Object, ByVal e As System.EventArgs)

        Dim itemClick As Button = CType(sender, Button)
        index = GetIndex(itemClick, strbtnlicht)

        If strbtnlicht(index).BackColor = Color.Yellow Then
            strbtnlicht(index).BackColor = Color.Gray
        Else
            strbtnlicht(index).BackColor = Color.Yellow
        End If
        Tempozeigen(index)
        'wechselt zwischen lich an und aus
    End Sub

    Private Sub StrStop_click(ByVal sender As System.Object, ByVal e As System.EventArgs)

        Dim itemClick As Button = CType(sender, Button)

        index = GetIndex(itemClick, strbtnstop)

        strscroll(index).Value = 0
        strbtnlicht(index).BackColor = Color.Gray
        Tempozeigen(index)
        'stopt zug
    End Sub

    Private Sub Strtempo_scroll(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ScrollEventArgs)

        Dim itemScroll As HScrollBar = CType(sender, HScrollBar)

        Tempozeigen(GetIndex(itemScroll, strscroll))
        'ändert tempo beim scrollen
    End Sub

    Private Sub Tempozeigen(ByVal ThisIndex As Integer)
        'rechnet tempo um und schickt an Fahren-sub
        Dim zugnr As Integer

        Try
            zugnr = CInt(strtb(ThisIndex).Text)
        Catch ex As Exception
            Error_log("couldn't get train number", True)
            Exit Sub
        End Try
        Dim tempo As Integer = strscroll(ThisIndex).Value


        strlbltempo(ThisIndex).Text = "Tempo: " & tempo

        If tempo < 0 Then
            tempo = (tempo * -1) + 32
        End If

        If strbtnlicht(ThisIndex).BackColor = Color.Yellow Then
            Fahren(zugnr, tempo, 64)
            Error_log(zugnr & " :" & tempo + 64, True)
        Else
            Fahren(zugnr, tempo, 0)
            Error_log(zugnr & " :" & tempo, True)
        End If

    End Sub

    Private Sub Fahren(ByVal zugnr As Integer, ByVal Tempo As Integer, ByVal licht As Integer)
        Try
            If LblComAnschluss.BackColor <> Color.Red Then
                'im Testmodus nichts senden
                Dim Fahrt() As Byte = {zugnr + 128, Tempo + licht}
                ComAdapter.Write(Fahrt, 0, 2)  '<= wird nuhr gesendet wenn anschluss vorhanden
            End If
        Catch ex As Exception
            Error_log("error while sending driving-data :" & zugnr & ";" & Tempo, True)
        End Try
    End Sub

    '**************************************************HotKeySteuerung

    Private Sub Button1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Button1.KeyDown
        If e.KeyCode = Keys.B Then
            'besetztmelder
            Btn_besetzt.PerformClick()
        ElseIf e.KeyCode = Keys.W Then
            'weiche
            Btn_ws.PerformClick()
        ElseIf e.KeyCode = Keys.G Then
            'gleis
            Btn_gleis.PerformClick()
        ElseIf e.KeyCode = Keys.L Then
            'loeschen
            Btn_löschen.PerformClick()
        ElseIf e.KeyCode = Keys.C Then
            'code
            Btn_code.PerformClick()
        ElseIf e.KeyCode = Keys.S Then
            'start (bau-steuern)
            Btn_startsteuern.PerformClick()
        ElseIf e.KeyCode = Keys.E Then
            'error-log
            Btn_error.PerformClick()
        ElseIf e.KeyCode = Keys.H Then
            'hilfe
            Btn_help.PerformClick()
        ElseIf e.KeyCode = Keys.M Then
            Me.WindowState = FormWindowState.Minimized
            'minimizes window
        End If
    End Sub

    '*******************************************Fahrplan/Program

    Private Sub Btn_code_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_code.Click
        If Tbprog.Visible Then
            If bau > -1 Then
                ShowHideBauButtons(True)
                'show all build buttons
            Else
                'or all steuern buttons
                ShowHideSaveButtons(False)
                ShowHideSteuernButtons(True)
            End If

            lbl_steuern.Visible = True
            Btn_startsteuern.Visible = True
            'hide code buttons and show other buttons
            ShowHideCodeButtons(False)
        Else
            'hide all build and steuern buttons
            ShowHideBauButtons(False)
            lbl_steuern.Visible = False
            Btn_startsteuern.Visible = False
            ShowHideSteuernButtons(False)
            'and show code buttons
            ShowHideSaveButtons(True)
            Lb_error_log.Visible = False
            ShowHideCodeButtons(True)
            Tbprog.BringToFront()
        End If
    End Sub

    Private Sub Highlighting(ByVal indexOfChar As Integer, ByVal indexOfLine As Integer)
        Try
            If indexOfChar - Tbprog.GetFirstCharIndexFromLine(indexOfLine) <> 0 Then

                If LSet(Tbprog.Lines(indexOfLine), 1) <> "" Then
                    'highlight text in code window
                    If Mid(Tbprog.Text, indexOfChar - 2, 3) = "if " Then
                        Tbprog.Select(indexOfChar - 3, 3)
                        Tbprog.SelectionColor = Color.MediumVioletRed

                    ElseIf Mid(Tbprog.Text, indexOfChar - 5, 6) = "while " Then
                        Tbprog.Select(indexOfChar - 6, 6)
                        Tbprog.SelectionColor = Color.MediumVioletRed

                    ElseIf Mid(Tbprog.Text, indexOfChar - 3, 4) = "log " Then
                        Tbprog.Select(indexOfChar - 4, 4)
                        Tbprog.SelectionColor = Color.DarkGoldenrod

                    ElseIf Mid(Tbprog.Text, indexOfChar - 3, 4) = "run " Then
                        Tbprog.Select(indexOfChar - 4, 4)
                        Tbprog.SelectionColor = Color.DarkGoldenrod

                    ElseIf Mid(Tbprog.Text, indexOfChar - 2, 3) = "bes" Then
                        Tbprog.Select(indexOfChar - 3, 3)
                        Tbprog.SelectionColor = Color.SpringGreen

                    ElseIf Mid(Tbprog.Text, indexOfChar - 2, 3) = "zug" Then
                        Tbprog.Select(indexOfChar - 3, 3)
                        Tbprog.SelectionColor = Color.SpringGreen

                    ElseIf Mid(Tbprog.Text, indexOfChar - 6, 7) = "lichtAn" Then
                        Tbprog.Select(indexOfChar - 7, 7)
                        Tbprog.SelectionColor = Color.SpringGreen

                    ElseIf Mid(Tbprog.Text, indexOfChar - 7, 8) = "lichtAus" Then
                        Tbprog.Select(indexOfChar - 8, 8)
                        Tbprog.SelectionColor = Color.SpringGreen

                    ElseIf Mid(Tbprog.Text, indexOfChar - 2, 3) = " = " Then
                        Tbprog.Select(indexOfChar - 3, 3)
                        Tbprog.SelectionColor = Color.Aqua

                    ElseIf Mid(Tbprog.Text, indexOfChar - 2, 3) = " > " Then
                        Tbprog.Select(indexOfChar - 3, 3)
                        Tbprog.SelectionColor = Color.Aqua

                    ElseIf Mid(Tbprog.Text, indexOfChar - 2, 3) = " < " Then
                        Tbprog.Select(indexOfChar - 3, 3)
                        Tbprog.SelectionColor = Color.Aqua

                    ElseIf Mid(Tbprog.Text, indexOfChar - 2, 3) = " + " Then
                        Tbprog.Select(indexOfChar - 3, 3)
                        Tbprog.SelectionColor = Color.Aqua

                    ElseIf Mid(Tbprog.Text, indexOfChar - 2, 3) = " - " Then
                        Tbprog.Select(indexOfChar - 3, 3)
                        Tbprog.SelectionColor = Color.Aqua

                    ElseIf Mid(Tbprog.Text, indexOfChar - 2, 3) = " * " Then
                        Tbprog.Select(indexOfChar - 3, 3)
                        Tbprog.SelectionColor = Color.Aqua

                    ElseIf Mid(Tbprog.Text, indexOfChar - 2, 3) = " / " Then
                        Tbprog.Select(indexOfChar - 3, 3)
                        Tbprog.SelectionColor = Color.Aqua

                    ElseIf Mid(Tbprog.Text, indexOfChar - 2, 3) = " ^ " Then
                        Tbprog.Select(indexOfChar - 3, 3)
                        Tbprog.SelectionColor = Color.Aqua
                    Else
                        'highlight comments in code window
                        If LSet(Tbprog.Lines(indexOfLine), 1) = "'" Then
                            Tbprog.Select(Tbprog.GetFirstCharIndexFromLine(indexOfLine), Tbprog.Lines(indexOfLine).Length)
                            Tbprog.SelectionColor = Color.Olive
                        Else
                            'highlight strings in codewindow
                            If Tbprog.Lines(indexOfLine).Contains("""") Then
                                Tbprog.Select(Tbprog.Lines(indexOfLine).IndexOf("""") + Tbprog.GetFirstCharIndexFromLine(indexOfLine), Tbprog.Lines(indexOfLine).LastIndexOf("""") - Tbprog.Lines(indexOfLine).IndexOf("""") + 1)
                                Tbprog.SelectionColor = Color.IndianRed
                            End If
                        End If

                    End If
                End If
            End If

        Catch ex As Exception
            Error_log("highlighting error2 at index :" & indexOfChar, True)
        End Try
    End Sub

    Private Sub Tbprog_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Tbprog.TextChanged
        Dim currentChar As Integer
        Dim thisLine As Integer

        Try
            'check new text for highlighting
            thisLine = Tbprog.GetLineFromCharIndex(Tbprog.GetFirstCharIndexOfCurrentLine)
            currentChar = Tbprog.SelectionStart
            Highlighting(currentChar, thisLine)

            Tbprog.DeselectAll()
            Tbprog.SelectionStart = currentChar
            Tbprog.SelectionColor = Color.White

        Catch ex As Exception
            Error_log("highlighting error", True)
        End Try
    End Sub

    Public linearray() As String

    Private Sub BtnAusführen_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnAusführen.Click
        Try
            'execute code
            If Not BGW.IsBusy Then
                BtnAusführen.Image = ImageList2.Images(10)
                BtnAusführen.Text = "abbrechen"
                linearray = Tbprog.Lines
                BGW.RunWorkerAsync(linearray)
            Else
                'or cancel execution
                BGW.CancelAsync()
            End If

        Catch ex As Exception
            Error_log("couldn't execute program", True)
        End Try
    End Sub

    Private Sub BGW_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BGW.RunWorkerCompleted
        If e.Cancelled Then
            Lb_error_log.Items.Add("code abgebrochen")
        End If
        'show end-status of code
        BtnAusführen.Text = "ausführen"
        BtnAusführen.Image = ImageList2.Images(9)
    End Sub

    Public code_var(255) As Integer
    Public code_var_name(255) As String

    'all commands with * are WorkInProgress

    'if <var1> = <var2>                                    wenn var1 = var2 dann run code1
    '   <code1>                                            weiche(dec;byte) = 1 ; bes(dec;byte) = 1
    '*else                                                 sonst
    '   <code2>                                            run code2

    'run <C:\...txt>                                       execute program (saved as .txt)

    '*send(dec, byte)                                      sends dec and byte true com
    'zug<Nr>.lichtAn/Aus = <tempo>                         sets zugNr tempo and light to given value

    '*build.gleis(x1, y1, x2, y2)                          build gleis von xy1 nach xy2
    '*build.weiche(x, y, rotation, dec, byte)              build weiche an xy mit rotation und dec;byte
    '*build.bes(x1, y1, x2, y2, dec, byte)                 build bes von xy1 nach xy2 mit dec;byte

    '*build.steuerung(x, y)                                builds steuerung an xy

    'while <k> = 1                                         solange k = 1 run code
    '   <code>

    'log "text"                                            writes text in error_log

    '*function <name>                                      runs code and returns value
    '   <code>
    '   return <value>

    '*<var1> = <function>                                  executes function and returns value

    '<var1> = <value>                                      schreibt value in variable 

    '*<array1> = [v1, v2, ...]                             schreibt value1,2... in array

    'tab = "	"
    '

    Private Sub BGW_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BGW.DoWork

        Dim line As Integer
        Dim tab_anzahl As Integer

        Dim CurrentLine As String
        Dim tabAnzahlInThisLine As Integer

        Dim IfEndJumpToThisLine(linearray.Length) As Integer 'for some reason this needs a bound bc otherewise ther is an "is null exception" idfk
        Dim linearray2() As String = linearray

        'execute code in different thread

        While Not line > UBound(linearray2) 'linearray.GetUpperBound(0)
            If BGW.CancellationPending Then
                e.Cancel = True
                Exit While
            End If

            CurrentLine = linearray2(line)
            tabAnzahlInThisLine = 0

            For i As Integer = 1 To tab_anzahl + 1
                If "...." = LSet(CurrentLine, 4) Then
                    CurrentLine = Mid(CurrentLine, 5)
                    tabAnzahlInThisLine = i
                Else
                    If tabAnzahlInThisLine <> tab_anzahl Then

                        line = IfEndJumpToThisLine(tab_anzahl - 1)
                        CurrentLine = linearray2(IfEndJumpToThisLine(tab_anzahl - 1))
                        tab_anzahl -= 1
                        Exit For
                    Else
                        If CurrentLine <> "" And CurrentLine <> "'" Then

                            If LSet(CurrentLine, 4) = "log " Then
                                If LSet(CurrentLine, 5) = "log """ Then
                                    Me.Lb_error_log.Invoke(Sub() WriteInLog(Mid(CurrentLine, 6, Len(CurrentLine) - 6)))
                                Else
                                    Me.Lb_error_log.Invoke(Sub() WriteInLog(Code_calc_value(Mid(CurrentLine, 5))))
                                End If

                            ElseIf LSet(CurrentLine, 3) = "if " Then
                                If Mid(CurrentLine, 4).Contains(" = ") Then
                                    If Code_calc_value(Mid(CurrentLine, 4, CurrentLine.IndexOf("=") - 4)) = Code_calc_value(Mid(CurrentLine, CurrentLine.IndexOf("=") + 3)) Then

                                        IfEndJumpToThisLine(tab_anzahl) = Code_endOfThis(line + 1, tab_anzahl + 1, linearray2)
                                        tab_anzahl += 1
                                    Else
                                        line = Code_endOfThis(line + 1, tab_anzahl + 1, linearray2) - 1
                                    End If

                                ElseIf Mid(CurrentLine, 4).Contains(" > ") Then
                                    If Code_calc_value(Mid(CurrentLine, 4, CurrentLine.IndexOf(">") - 4)) > Code_calc_value(Mid(CurrentLine, CurrentLine.IndexOf(">") + 3)) Then

                                        IfEndJumpToThisLine(tab_anzahl) = Code_endOfThis(line + 1, tab_anzahl + 1, linearray2)
                                        tab_anzahl += 1
                                    Else
                                        line = Code_endOfThis(line + 1, tab_anzahl + 1, linearray2) - 1
                                    End If

                                ElseIf Mid(CurrentLine, 4).Contains(" < ") Then
                                    If Code_calc_value(Mid(CurrentLine, 4, CurrentLine.IndexOf("<") - 4)) < Code_calc_value(Mid(CurrentLine, CurrentLine.IndexOf("<") + 3)) Then

                                        IfEndJumpToThisLine(tab_anzahl) = Code_endOfThis(line + 1, tab_anzahl + 1, linearray2)
                                        tab_anzahl += 1
                                    Else
                                        line = Code_endOfThis(line + 1, tab_anzahl + 1, linearray2) - 1
                                    End If
                                End If

                            ElseIf LSet(CurrentLine, 6) = "while " Then
                                If Mid(CurrentLine, 7).Contains(" = ") Then
                                    If Code_calc_value(Mid(CurrentLine, 7, CurrentLine.IndexOf("=") - 7)) = Code_calc_value(Mid(CurrentLine, CurrentLine.IndexOf("=") + 3)) Then

                                        IfEndJumpToThisLine(tab_anzahl) = line
                                        tab_anzahl += 1
                                    Else
                                        line = Code_endOfThis(line + 1, tab_anzahl + 1, linearray2) - 1
                                    End If
                                ElseIf Mid(CurrentLine, 7).Contains(" > ") Then
                                    If Code_calc_value(Mid(CurrentLine, 7, CurrentLine.IndexOf(">") - 7)) > Code_calc_value(Mid(CurrentLine, CurrentLine.IndexOf(">") + 3)) Then

                                        IfEndJumpToThisLine(tab_anzahl) = line
                                        tab_anzahl += 1
                                    Else
                                        line = Code_endOfThis(line + 1, tab_anzahl + 1, linearray2) - 1
                                    End If
                                ElseIf Mid(CurrentLine, 7).Contains(" < ") Then
                                    If Code_calc_value(Mid(CurrentLine, 7, CurrentLine.IndexOf("<") - 7)) < Code_calc_value(Mid(CurrentLine, CurrentLine.IndexOf("<") + 3)) Then

                                        IfEndJumpToThisLine(tab_anzahl) = line
                                        tab_anzahl += 1
                                    Else
                                        line = Code_endOfThis(line + 1, tab_anzahl + 1, linearray2) - 1
                                    End If
                                End If

                            ElseIf LSet(CurrentLine, 3) = "zug" Then

                                Dim lichtAnAus As Integer
                                Dim tempo As Integer
                                Dim zugNr As Integer

                                If CurrentLine.Contains(".lichtAn") Then
                                    lichtAnAus = 64

                                ElseIf CurrentLine.Contains(".lichtAus") Then
                                    lichtAnAus = 0
                                End If

                                zugNr = Code_calc_value(Mid(CurrentLine, 4, CurrentLine.IndexOf(".") - 3))
                                tempo = Code_calc_value(Mid(CurrentLine, CurrentLine.IndexOf("=") + 3))

                                Fahren(zugNr, tempo, lichtAnAus)
                                Me.Lb_error_log.Invoke(Sub() WriteInLog("gesendet :" & zugNr & ", " & tempo))

                            ElseIf LSet(CurrentLine, 4) = "run " Then
                                'C:\Users\mcier\Desktop\modellbahn\test.txt
                                Dim CodeAtPfad As String = My.Computer.FileSystem.ReadAllText(Mid(CurrentLine, 5))

                                Dim lineNum As Integer

                                For k As Integer = 1 To Len(CodeAtPfad)
                                    If Mid(CodeAtPfad, k, 1) = vbLf Then
                                        lineNum += 1
                                    End If
                                Next

                                Dim LinesOfCode(lineNum) As String
                                Dim k_line As Integer = 0

                                Dim lastK As Integer = 1

                                For k As Integer = 1 To Len(CodeAtPfad)
                                    If Mid(CodeAtPfad, k, 1) = vbLf Then
                                        LinesOfCode(k_line) = Mid(CodeAtPfad, lastK, k - lastK)
                                        Debug.Print("line " & k_line & ":" & LinesOfCode(k_line))

                                        lastK = k + 1
                                        k_line += 1
                                    End If
                                Next

                                Dim TabsInThisLine As String = ""

                                For k As Integer = 1 To tabAnzahlInThisLine
                                    TabsInThisLine &= "...."
                                Next

                                Dim CurrentLengthOfLineArray As Integer = linearray2.Length
                                ReDim Preserve linearray2(CurrentLengthOfLineArray + k_line)

                                For k As Integer = CurrentLengthOfLineArray To line Step -1
                                    linearray2(k + k_line - 1) = linearray2(k)
                                Next

                                For k As Integer = line To line + k_line - 1
                                    linearray2(k) = TabsInThisLine & LinesOfCode(k - line + 1)

                                Next

                            Else

                                If CurrentLine.Contains(" = ") Then
                                    code_var(Var_Index(LSet(CurrentLine, CurrentLine.IndexOf("=") - 1))) = Code_calc_value(Mid(CurrentLine, CurrentLine.IndexOf("=") + 3))
                                End If
                            End If
                        End If

                        line += 1
                    End If
                End If
            Next
        End While
    End Sub


    Private Function Code_endOfThis(ByVal line As Integer, ByVal tab_anzahl As Integer, ThisLinearray() As String) As Integer
        'returns end_line of if or while
        Dim CurrentLine As String
        Dim tabAnzahlInThisLine As Integer

        For i As Integer = line To ThisLinearray.GetUpperBound(0)

            CurrentLine = ThisLinearray(i)
            tabAnzahlInThisLine = 0

            For k As Integer = 1 To tab_anzahl + 1
                If "...." = LSet(CurrentLine, 4) Then
                    CurrentLine = Mid(CurrentLine, 5)
                    tabAnzahlInThisLine += 1
                Else
                    If tabAnzahlInThisLine <> tab_anzahl Then
                        Return i
                    End If
                End If
            Next
        Next

        Return line + 1
    End Function

    'writes in error log (only for code)
    Private Sub WriteInLog(ByVal writingstring As String)
        Me.Lb_error_log.Items.Add(writingstring)
    End Sub

    Private Function Var_Index(ByVal varString As String) As Integer
        'returns index of given variable(as string)
        For i As Integer = 1 To 255
            If varString = code_var_name(i) Then
                Return i
            End If
        Next

        For i As Integer = 1 To 255
            If code_var_name(i) = "" Then
                code_var_name(i) = varString
                Return i
            End If
        Next
        Return 0
    End Function

    Private Function Code_calc_value(ByVal code_String As String) As Integer
        'calculates value of given variable(as string)
        For i As Integer = 1 To 255
            If code_String = code_var_name(i) Then
                Return code_var(i)
            End If
        Next

        If code_String.Contains(" + ") Then
            Return Code_calc_value(Mid(code_String, 1, code_String.LastIndexOf("+") - 1)) + Code_calc_value(Mid(code_String, code_String.LastIndexOf("+") + 3))

        ElseIf code_String.Contains(" - ") Then
            Return Code_calc_value(Mid(code_String, 1, code_String.LastIndexOf("-") - 1)) - Code_calc_value(Mid(code_String, code_String.LastIndexOf("-") + 3))

        ElseIf code_String.Contains(" * ") Then
            Return Code_calc_value(Mid(code_String, 1, code_String.LastIndexOf("*") - 1)) * Code_calc_value(Mid(code_String, code_String.LastIndexOf("*") + 3))

        ElseIf code_String.Contains(" / ") Then
            Return Code_calc_value(Mid(code_String, 1, code_String.LastIndexOf("/") - 1)) / Code_calc_value(Mid(code_String, code_String.LastIndexOf("/") + 3))

        ElseIf code_String.Contains(" ^ ") Then
            Return Code_calc_value(Mid(code_String, 1, code_String.LastIndexOf("^") - 1)) ^ Code_calc_value(Mid(code_String, code_String.LastIndexOf("^") + 3))

        Else
            If LSet(code_String, 3) = "bes" Then
                'if not a variable but bes-melder
                Dim thisDec As Integer
                thisDec = Mid(code_String, 4, code_String.IndexOf(".") - 3)

                UmrechnungInBit(Besetztanfrage(thisDec))

                If besary(Mid(code_String, code_String.IndexOf(".") + 2)) Then
                    Return 1
                Else
                    Return 0
                End If
            Else

                Return CInt(code_String)
            End If
        End If
    End Function


    '********************************************ERROR-LOG

    Private Sub Lb_error_log_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Lb_error_log.SelectedIndexChanged
        If Lb_error_log.SelectedIndex = 0 Then
            Lb_error_log.Items.Clear()
            Lb_error_log.Items.Add("Clear all items")
        End If
    End Sub

    Private Sub Btn_error_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_error.Click
        'show or hide error log
        If Lb_error_log.Visible Then
            Lb_error_log.Visible = False
        Else
            Lb_error_log.Visible = True
            Lb_error_log.BringToFront()
            Lb_error_log.Location = New Point(Me.Size.Width - 280, 45)
            Lb_error_log.Size = New Size(250, Me.Size.Height - 58)
        End If
    End Sub

    Private Sub Error_log(ByVal thisError As String, ByVal showThisError As Boolean)
        'if error should be shown write error in error log
        If showThisError Then
            Lb_error_log.Items.Add(thisError)
        End If
    End Sub

    '*************************************************help
    Private Sub Pic_helpExit_Click(sender As Object, e As EventArgs) Handles pic_helpExit.Click
        panel_help.Visible = False
        'exit help-window
    End Sub

    'if cursor hovers on exit-window button show button in red to indicate exit-possibility
    Private Sub Pic_helpExit_MouseEnter(sender As Object, e As EventArgs) Handles pic_helpExit.MouseEnter
        pic_helpExit.BackColor = Color.Red
    End Sub

    Private Sub Pic_helpExit_MouseLeave(sender As Object, e As EventArgs) Handles pic_helpExit.MouseLeave
        pic_helpExit.BackColor = Color.DimGray
    End Sub

    Private Sub Btn_help_Click(sender As Object, e As EventArgs) Handles Btn_help.Click
        If Not panel_help.Visible Then
            panel_help.Visible = True
            panel_help.Location = New Point(100, 100)
            panel_help.BringToFront()
            'show help-text
            Try
                rtb_help.Text = My.Computer.FileSystem.ReadAllText(My.Computer.FileSystem.CurrentDirectory.ToString & "\help.txt")

            Catch ex As Exception
                rtb_help.Text = "help-file not found" & My.Computer.FileSystem.CurrentDirectory.ToString
                Error_log("help-file not found", True)
            End Try
        End If
    End Sub

    Dim moveHelpWindow As Boolean
    'move help-window if draged
    Private Sub Panel_help_MouseDown(sender As Object, e As MouseEventArgs) Handles panel_help.MouseDown
        moveHelpWindow = True
        panel_help.BringToFront()
        Me.Cursor = Cursors.SizeAll
    End Sub

    Private Sub Panel_help_MouseCaptureChanged(sender As Object, e As EventArgs) Handles panel_help.MouseCaptureChanged
        If moveHelpWindow Then
            moveHelpWindow = False
            Me.Cursor = Cursors.Default
        End If
    End Sub

    '******************************************************settings

    Private Sub Btn_settings_Click(sender As Object, e As EventArgs) Handles btn_settings.Click
        If panel_settings.Visible Then
            panel_settings.Visible = False
        Else
            panel_settings.Visible = True
            panel_settings.Location = New Point(100, 100)
            panel_settings.BringToFront()
        End If
    End Sub

    'same as help window but for settings window
    Dim moveSettingWindow As Boolean
    Private Sub Btn_settingsExit_Click(sender As Object, e As EventArgs) Handles btn_settingsExit.Click
        panel_settings.Visible = False
    End Sub

    Private Sub Panel_settings_MouseCaptureChanged(sender As Object, e As EventArgs) Handles panel_settings.MouseCaptureChanged
        If moveSettingWindow Then
            moveSettingWindow = False
            Me.Cursor = Cursors.Default
        End If
    End Sub

    Private Sub Panel_settings_MouseDown(sender As Object, e As MouseEventArgs) Handles panel_settings.MouseDown
        moveSettingWindow = True
        panel_settings.BringToFront()
        Me.Cursor = Cursors.SizeAll
    End Sub

    Private Sub Btn_settingsExit_MouseEnter(sender As Object, e As EventArgs) Handles btn_settingsExit.MouseEnter
        btn_settingsExit.BackColor = Color.Red
    End Sub

    Private Sub Btn_settingsExit_MouseLeave(sender As Object, e As EventArgs) Handles btn_settingsExit.MouseLeave
        btn_settingsExit.BackColor = Color.DimGray
    End Sub

    Private Sub NumericScaling_ValueChanged(sender As Object, e As EventArgs) Handles numericScaling.ValueChanged
        scalingXY = numericScaling.Value
    End Sub

    Private Sub NumericScaling_TextChanged(sender As Object, e As EventArgs) Handles numericScaling.TextChanged
        scalingXY = numericScaling.Value
    End Sub

    Private Sub BtnDeleteSteuerung_Click(sender As Object, e As EventArgs) Handles BtnDeleteSteuerung.Click
        Try
            If k_str > 2 Then

                strpanel(k_str - 1).Visible = False
                k_str -= 1
            End If
        Catch ex As Exception
            Error_log("couldn't remove last Steuerung", True)
        End Try
    End Sub
End Class