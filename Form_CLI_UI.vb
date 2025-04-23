Imports System.IO
Imports System.Net.Http
Imports System.IO.Compression
Imports Newtonsoft.Json.Linq
Imports System.Linq
Imports System.Windows.Forms.VisualStyles.VisualStyleElement

Public Class Form_CLI_UI



    Private Sub Form_CLI_UI_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Load the saved value for Template Location
        TextBox_Template_Location.Text = My.Settings.TemplateLocation
        TextBox_CLIHome.Text = My.Settings.CLIInstallLocation
        ComboBox_Background_Template.Text = My.Settings.Setting_Background
        ComboBox_Monster_Template.Text = My.Settings.Setting_Monster
        ComboBox_Item_Template.Text = My.Settings.Setting_Item
        ComboBox_Template_Race.Text = My.Settings.Setting_Race
        ComboBox_Spell_Template.Text = My.Settings.Setting_Spell
        TextBox_From.Text = My.Settings.Setting_From
        TextBox_Adventure.Text = My.Settings.Setting_Adventure
        TextBox_Book.Text = My.Settings.Setting_Book
        TextBox_Config_File.Text = My.Settings.Setting_JSON
        TextBox_Data_Folder.Text = My.Settings.Setting_DataFolder
        TextBox_ImageFolder.Text = My.Settings.Setting_ImageFolder
        TextBox_SourceData.Text = My.Settings.Setting_DataFolder_Full
        TextBox_Homebrew_Folder.Text = My.Settings.Setting_Homebrew_Folder
        TextBox_Homebrew_Content.Text = My.Settings.Setting_Homebrew
        TextBox_VaultLocation_Rules.Text = "VaultRoot" + My.Settings.Setting_Compendium
        TextBox_VaultLocation_Compendium.Text = "VaultRoot" + My.Settings.Setting_Rules
        TextBox_Compendium_Location.Text = My.Settings.Setting_Compendium
        TextBox_Rules_Location.Text = My.Settings.Setting_Rules
        ComboBox_ReprintBehaviour.Text = My.Settings.Setting_ReprintBehaviour
        Text_CLI_Source.Text = My.Settings.CLIInstallLocation

        Label_TagPrefix.Cursor = Cursors.Hand
        Label_ReprintBehaviour.Cursor = Cursors.Hand

        ToolTip_ReprintBehaviour.SetToolTip(Label_ReprintBehaviour, "newest (default): Only includes notes for the most recent version of reprinted content.
edition: Focuses on preserving content across incompatible editions (especially for 5e rules).
         Example: The edition check will preserve 2014 edition-specific class and subclass definitions. 
         Other resources (that are not different across editions) will follow the reprints to include new content.
all: Includes notes for all reprinted versions from enabled sources")

        ' Set the CheckBox value based on the Setting_Use_FantasyStat setting
        CheckBox_FantStatPlugin.Checked = My.Settings.Setting_Use_FantasyStat
        CheckBox_DiceRoller.Checked = My.Settings.Setting_Use_DiceRoller
        CheckBox_Img_External.Checked = My.Settings.Setting_Copy_ExternalImages
        CheckBox_Img_Internal.Checked = My.Settings.Setting_Copy_InternalImages

        ' Call the BuildConfigFile method to generate the text for TextBox_ConfigFile_Build
        BuildConfigFile()

    End Sub

    Private Sub BuildConfigFile()
        Dim configText As String = "{"

        ' "sources" section
        configText &= vbCrLf & "  ""sources"" : {"

        ' "adventure" array
        configText &= vbCrLf & "    ""adventure"" : ["
        If Not String.IsNullOrEmpty(My.Settings.Setting_Adventure) Then
            Dim adventureArray = My.Settings.Setting_Adventure.Split(","c).Select(Function(item) """" & item.Trim() & """")
            configText &= vbCrLf & "      " & String.Join("," & vbCrLf & "      ", adventureArray)
        End If
        configText &= vbCrLf & "    ],"

        ' "book" array
        configText &= vbCrLf & "    ""book"" : ["
        If Not String.IsNullOrEmpty(My.Settings.Setting_Book) Then
            Dim bookArray = My.Settings.Setting_Book.Split(","c).Select(Function(item) """" & item.Trim() & """")
            configText &= vbCrLf & "      " & String.Join("," & vbCrLf & "      ", bookArray)
        End If
        configText &= vbCrLf & "    ],"

        ' "reference" array
        configText &= vbCrLf & "    ""reference"" : ["
        If Not String.IsNullOrEmpty(My.Settings.Setting_Reference) Then
            Dim referenceArray = My.Settings.Setting_Reference.Split(","c).Select(Function(item) """" & item.Trim() & """")
            configText &= vbCrLf & "      " & String.Join("," & vbCrLf & "      ", referenceArray)
        End If
        configText &= vbCrLf & "    ],"

        ' "homebrew" array
        configText &= vbCrLf & "    ""homebrew"" : ["
        If Not String.IsNullOrEmpty(My.Settings.Setting_Homebrew) Then
            ' Removed .Replace(";", "/") so semicolons are preserved.
            Dim homebrewArray = My.Settings.Setting_Homebrew.Split(","c).Select(Function(item) """" & item.Trim() & """")
            configText &= vbCrLf & "      " & String.Join("," & vbCrLf & "      ", homebrewArray)
        End If
        configText &= vbCrLf & "    ]"


        configText &= vbCrLf & "  },"   ' Close "sources" section

        ' "paths" section
        configText &= vbCrLf & "  ""paths"" : {"
        ' These are static values based on your example; change as needed if you have settings for these.
        configText &= vbCrLf & "    ""rules"" : """ & TextBox_Rules_Location.Text & ""","
        configText &= vbCrLf & "    ""compendium"" : """ & TextBox_Compendium_Location.Text & """"
        configText &= vbCrLf & "  },"

        ' "images" section
        configText &= vbCrLf & "  ""images"" : {"
        configText &= vbCrLf & "    ""copyInternal"" : " & If(CheckBox_Img_Internal.Checked, "true", "false") & ","
        configText &= vbCrLf & "    ""copyExternal"" : " & If(CheckBox_Img_External.Checked, "true", "false") & ","
        configText &= vbCrLf & "    ""internalRoot"" : """ & TextBox_Img_Folder.Text & """"
        configText &= vbCrLf & "  },"

        ' --- INSERT: optional yamlStatblocks ---
        If CheckBox_FantStatPlugin.Checked Then
            configText &= vbCrLf & "  ""yamlStatblocks"" : true,"
        End If

        ' "reprintBehavior" section (static value "all")
        configText &= vbCrLf & "  ""reprintBehavior"" : """ & ComboBox_ReprintBehaviour.Text & ""","

        ' "useDiceRoller" section
        configText &= vbCrLf & "  ""useDiceRoller"" : " & If(CheckBox_DiceRoller.Checked, "true", "false") & ","

        ' "tagPrefix" section
        configText &= vbCrLf & "  ""tagPrefix"" : """ & TextBox_tagPrefix.Text & ""","

        ' "template" section
        configText &= vbCrLf & "  ""template"" : {"
        configText &= vbCrLf & "    ""background"" : """ & My.Settings.Setting_RelativeTemplatePath & "/" & My.Settings.Setting_Background & ""","
        configText &= vbCrLf & "    ""monster"" : """ & My.Settings.Setting_RelativeTemplatePath & "/" & My.Settings.Setting_Monster & ""","
        configText &= vbCrLf & "    ""item"" : """ & My.Settings.Setting_RelativeTemplatePath & "/" & My.Settings.Setting_Item & ""","
        configText &= vbCrLf & "    ""race"" : """ & My.Settings.Setting_RelativeTemplatePath & "/" & My.Settings.Setting_Race & ""","
        configText &= vbCrLf & "    ""spell"" : """ & My.Settings.Setting_RelativeTemplatePath & "/" & My.Settings.Setting_Spell & """"
        configText &= vbCrLf & "  }"

        ' Close main JSON object
        configText &= vbCrLf & "}"

        ' Assign the generated text to the TextBox
        TextBox_ConfigFile_Build.Text = configText
    End Sub

    Private Sub ComboBox_ReprintBehaviour_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox_ReprintBehaviour.SelectedIndexChanged
        ' Store the selected value in settings
        My.Settings.Setting_ReprintBehaviour = ComboBox_ReprintBehaviour.SelectedItem?.ToString
        My.Settings.Save()

        ' Call the BuildConfigFile method to generate the text for TextBox_ConfigFile_Build
        BuildConfigFile()
    End Sub

    Private Sub PopulateBackgroundTemplates()
        ' Build the folder path using the value from TextBox_Template_Location.
        Dim folderPath As String = Path.Combine(TextBox_CLIHome.Text, TextBox_Template_Location.Text)

        ' Check if the folder exists.
        If Not Directory.Exists(folderPath) Then
            MessageBox.Show("Background Template Folder not found: " & folderPath)
            Return
        End If

        ' Clear any existing items.
        ComboBox_Background_Template.Items.Clear()

        ' Get all .txt files in the folder.
        Dim txtFiles As String() = Directory.GetFiles(folderPath, "*.txt")

        ' Filter to include only files whose names contain "background" (case-insensitive).
        Dim backgroundFiles = txtFiles.Where(Function(file) file.ToLower().Contains("background")).ToArray()

        ' Add just the file names (without full path) to the ComboBox.
        For Each filePath In backgroundFiles
            ComboBox_Background_Template.Items.Add(Path.GetFileName(filePath))
        Next

        ' Optionally, select the first item if available.
        If ComboBox_Background_Template.Items.Count > 0 Then
            ComboBox_Background_Template.SelectedIndex = 0
        End If
    End Sub

    Private Sub PopulateMonsterTemplates()
        ' Build the folder path using the value from TextBox_Template_Location.
        Dim folderPath_Monster As String = Path.Combine(TextBox_CLIHome.Text, TextBox_Template_Location.Text)

        ' Check if the folder exists.
        If Not Directory.Exists(folderPath_Monster) Then
            MessageBox.Show("Monster Template Folder not found: " & folderPath_Monster)
            Return
        End If

        ' Clear any existing items.
        ComboBox_Monster_Template.Items.Clear()

        ' Get all .txt files in the folder.
        Dim txtFiles_Monster As String() = Directory.GetFiles(folderPath_Monster, "*.txt")

        Dim filteredFiles As IEnumerable(Of String)
        If CheckBox_FantStatPlugin.Checked Then
            ' Only show files that contain both "monster" and "statblock".
            filteredFiles = txtFiles_Monster.Where(Function(file) file.ToLower().Contains("monster") AndAlso file.ToLower().Contains("statblock"))
        Else
            ' Show files that contain "monster" but do not contain "statblock".
            filteredFiles = txtFiles_Monster.Where(Function(file) file.ToLower().Contains("monster") AndAlso Not file.ToLower().Contains("statblock"))
        End If

        ' Add just the file names (without full path) to the ComboBox.
        For Each filePath In filteredFiles
            ComboBox_Monster_Template.Items.Add(Path.GetFileName(filePath))
        Next

        ' Optionally, select the first item if available.
        If ComboBox_Monster_Template.Items.Count > 0 Then
            ComboBox_Monster_Template.SelectedIndex = 0
        End If
    End Sub


    Private Sub PopulateItemTemplates()
        ' Build the folder path using the value from TextBox_Template_Location.
        Dim folderPath As String = Path.Combine(TextBox_CLIHome.Text, TextBox_Template_Location.Text)

        ' Check if the folder exists.
        If Not Directory.Exists(folderPath) Then
            MessageBox.Show("Item Template Folder not found: " & folderPath)
            Return
        End If

        ' Clear any existing items.
        ComboBox_Item_Template.Items.Clear()

        ' Get all .txt files in the folder.
        Dim txtFiles As String() = Directory.GetFiles(folderPath, "*.txt")

        ' Filter to include only files whose names contain "background" (case-insensitive).
        Dim backgroundFiles = txtFiles.Where(Function(file) file.ToLower().Contains("item")).ToArray()

        ' Add just the file names (without full path) to the ComboBox.
        For Each filePath In backgroundFiles
            ComboBox_Item_Template.Items.Add(Path.GetFileName(filePath))
        Next

        ' Optionally, select the first item if available.
        If ComboBox_Item_Template.Items.Count > 0 Then
            ComboBox_Item_Template.SelectedIndex = 0
        End If
    End Sub


    Private Sub PopulateRaceTemplates()
        ' Build the folder path using the value from TextBox_Template_Location.
        Dim folderPath As String = Path.Combine(TextBox_CLIHome.Text, TextBox_Template_Location.Text)

        ' Check if the folder exists.
        If Not Directory.Exists(folderPath) Then
            MessageBox.Show("Spell Template Folder not found: " & folderPath)
            Return
        End If

        ' Clear any existing items.
        ComboBox_Template_Race.Items.Clear()

        ' Get all .txt files in the folder.
        Dim txtFiles As String() = Directory.GetFiles(folderPath, "*.txt")

        ' Filter to include only files whose names contain "background" (case-insensitive).
        Dim backgroundFiles = txtFiles.Where(Function(file) file.ToLower().Contains("race")).ToArray()

        ' Add just the file names (without full path) to the ComboBox.
        For Each filePath In backgroundFiles
            ComboBox_Template_Race.Items.Add(Path.GetFileName(filePath))
        Next

        ' Optionally, select the first item if available.
        If ComboBox_Template_Race.Items.Count > 0 Then
            ComboBox_Template_Race.SelectedIndex = 0
        End If
    End Sub

    Private Sub PopulateSpellTemplates()
        ' Build the folder path using the value from TextBox_Template_Location.
        Dim folderPath As String = Path.Combine(TextBox_CLIHome.Text, TextBox_Template_Location.Text)

        ' Check if the folder exists.
        If Not Directory.Exists(folderPath) Then
            MessageBox.Show("Spell Template Folder not found: " & folderPath)
            Return
        End If

        ' Clear any existing items.
        ComboBox_Spell_Template.Items.Clear()

        ' Get all .txt files in the folder.
        Dim txtFiles As String() = Directory.GetFiles(folderPath, "*.txt")

        ' Filter to include only files whose names contain "background" (case-insensitive).
        Dim backgroundFiles = txtFiles.Where(Function(file) file.ToLower().Contains("spell")).ToArray()

        ' Add just the file names (without full path) to the ComboBox.
        For Each filePath In backgroundFiles
            ComboBox_Spell_Template.Items.Add(Path.GetFileName(filePath))
        Next

        ' Optionally, select the first item if available.
        If ComboBox_Spell_Template.Items.Count > 0 Then
            ComboBox_Spell_Template.SelectedIndex = 0
        End If
    End Sub

    Private Sub Button_Folder_Browse_Click(sender As Object, e As EventArgs)
        ' Create a new instance of FolderBrowserDialog
        Using folderDialog As New FolderBrowserDialog
            ' Optional: Set the description displayed in the dialog
            folderDialog.Description = "Select a folder for the CLI source path."

            ' Optional: Set the root folder (e.g., Desktop or My Computer)
            folderDialog.RootFolder = Environment.SpecialFolder.Desktop

            ' Show the dialog and check if the user selected a folder
            If folderDialog.ShowDialog = DialogResult.OK Then
                ' Copy the selected folder path to CLI_Source
                Text_CLI_Source.Text = folderDialog.SelectedPath
            End If
        End Using
    End Sub

    Private Sub Button_Config_File_Click(sender As Object, e As EventArgs)
        ' Create a new instance of OpenFileDialog
        Using openFileDialog As New OpenFileDialog
            ' Set the filter to show only .json files
            openFileDialog.Filter = "JSON Files (*.json)|*.json"
            openFileDialog.Title = "Select a Configuration File"

            ' Show the dialog and check if the user selected a file
            If openFileDialog.ShowDialog = DialogResult.OK Then
                ' Get only the file name and update TextBox_Config_File
                TextBox_Config_File.Text = IO.Path.GetFileName(openFileDialog.FileName)
            End If
        End Using
    End Sub

    Private Async Sub Button_Download_CLI_Click(sender As Object, e As EventArgs) Handles Button_Download_CLI.Click
        Dim githubApiUrl As String = "https://api.github.com/repos/ebullient/ttrpg-convert-cli/releases/latest"

        ' Use the value in TextBox_CLIHome as the download and extraction directory.
        Dim destinationFolder As String = TextBox_CLIHome.Text.Trim()
        If Not Directory.Exists(destinationFolder) Then
            Directory.CreateDirectory(destinationFolder)
        End If

        ' Define the full path for the ZIP file in the destination folder.
        Dim zipFilePath As String = Path.Combine(destinationFolder, "cli_latest_release.zip")

        Try
            Using client As New HttpClient()
                client.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (Windows NT 10.0; Win64; x64)")

                ' Fetch the latest release JSON data.
                Dim jsonResponse As String = Await client.GetStringAsync(githubApiUrl)
                Dim releaseInfo As JObject = JObject.Parse(jsonResponse)
                Dim assets As JArray = releaseInfo("assets")

                Dim downloadUrl As String = ""
                If assets IsNot Nothing AndAlso assets.Count > 0 Then
                    ' Loop through the assets to find one that contains "windows-x86_64.zip" in its name.
                    For Each asset As JObject In assets
                        Dim assetName As String = asset("name").ToString()
                        If assetName.ToLower().Contains("windows-x86_64.zip") Then
                            downloadUrl = asset("browser_download_url").ToString()
                            Exit For
                        End If
                    Next

                    If String.IsNullOrEmpty(downloadUrl) Then
                        MessageBox.Show("No asset with 'windows-x86_64.zip' found in the latest release.")
                        Return
                    End If

                    ' Optional: Debug message to confirm the download URL.
                    MessageBox.Show("Download commencing from: " & downloadUrl)

                    ' Download the asset as a byte array.
                    Dim fileBytes() As Byte = Await client.GetByteArrayAsync(downloadUrl)

                    ' Save the ZIP file to the destination folder.
                    File.WriteAllBytes(zipFilePath, fileBytes)
                    MessageBox.Show("Download complete: " & zipFilePath)

                    ' Unzip the file to the destination folder.
                    ZipFile.ExtractToDirectory(zipFilePath, destinationFolder)
                    MessageBox.Show("Unzipping complete. Files extracted to: " & destinationFolder)

                    ' Search recursively for ttrpg-convert.exe within the extracted files.
                    Dim exeFilePath As String = Directory.GetFiles(destinationFolder, "ttrpg-convert.exe", SearchOption.AllDirectories).FirstOrDefault()

                    If Not String.IsNullOrEmpty(exeFilePath) Then
                        ' Define the destination path for ttrpg-convert.exe (e.g., in the root of destinationFolder)
                        Dim finalExePath As String = Path.Combine(destinationFolder, "ttrpg-convert.exe")

                        ' Copy the file, overwriting if it already exists.
                        File.Copy(exeFilePath, finalExePath, True)
                        MessageBox.Show("ttrpg-convert.exe copied to: " & finalExePath)
                    Else
                        MessageBox.Show("ttrpg-convert.exe was not found in the extracted files.")
                    End If
                Else
                    MessageBox.Show("No assets found for the latest release.")
                End If
            End Using
        Catch ex As Exception
            MessageBox.Show("Error downloading or processing release: " & ex.Message)
        End Try
    End Sub

    Private Sub Button_Data_Folder_Click(sender As Object, e As EventArgs)
        ' Ensure CLI_Source is populated
        If String.IsNullOrWhiteSpace(Text_CLI_Source.Text) Then
            MessageBox.Show("Please select or specify a folder in CLI_Source first.", "Warning")
            Return
        End If

        ' Get the CLI_Source path
        Dim basePath = Text_CLI_Source.Text

        ' Validate that the CLI_Source path exists
        If Not IO.Directory.Exists(basePath) Then
            MessageBox.Show("The folder specified in CLI_Source does not exist.", "Error")
            Return
        End If

        ' Create a FolderBrowserDialog to select a folder
        Using folderDialog As New FolderBrowserDialog
            folderDialog.Description = "Select a data folder."
            folderDialog.RootFolder = Environment.SpecialFolder.Desktop

            ' Show the dialog and check if a folder was selected
            If folderDialog.ShowDialog = DialogResult.OK Then
                Dim selectedPath = folderDialog.SelectedPath

                ' Check if the selected path starts with the base path
                If selectedPath.StartsWith(basePath, StringComparison.OrdinalIgnoreCase) Then
                    ' Calculate the relative path
                    Dim relativePath = selectedPath.Substring(basePath.Length).TrimStart(IO.Path.DirectorySeparatorChar)
                    TextBox_Data_Folder.Text = relativePath
                Else
                    MessageBox.Show("The selected folder is not within the folder specified in CLI_Source.", "Error")
                End If
            End If
        End Using
    End Sub

    ' Event Handler for when the value in TextBox_OutputFolder changes
    Private Sub TextBox_OutputFolder_TextChanged(sender As Object, e As EventArgs) Handles TextBox_OutputFolder.TextChanged
        UpdateCommandText()
    End Sub

    ' Event Handler for when the value in TextBox_Config_File changes
    Private Sub TextBox_Config_File_TextChanged(sender As Object, e As EventArgs) Handles TextBox_Config_File.TextChanged
        UpdateCommandText()

        ' Save the new value to settings
        My.Settings.Setting_Config = TextBox_Config_File.Text
        My.Settings.Save()
    End Sub

    ' Event Handler for when the value in TextBox_Data_Folder changes
    Private Sub TextBox_Data_Folder_TextChanged(sender As Object, e As EventArgs) Handles TextBox_Data_Folder.TextChanged
        UpdateCommandText()

        ' Save the new value to settings
        My.Settings.Setting_DataFolder = TextBox_Data_Folder.Text
        My.Settings.Save()
    End Sub

    ' Event Handler for when the checkbox is checked or unchecked
    Private Sub CheckBoxIndex_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxIndex.CheckedChanged
        UpdateCommandText()
    End Sub

    ' Event Handler for when the CLI_Source path changes
    Private Sub CLI_Source_TextChanged(sender As Object, e As EventArgs) Handles Text_CLI_Source.TextChanged
        UpdateCommandText()
    End Sub

    ' Function to update the command in TextBox_Command dynamically
    Private Sub UpdateCommandText()
        ' Construct the base command with the executable
        Dim command As String = "ttrpg-convert.exe"

        ' Include the --index flag if CheckBoxIndex is checked
        If CheckBoxIndex.Checked Then
            command &= " --index"
        End If

        ' Add the output folder (-o)
        If Not String.IsNullOrEmpty(TextBox_OutputFolder.Text) Then
            command &= " -o """ & TextBox_OutputFolder.Text & """"
        End If

        ' Add the config file (-c)
        If Not String.IsNullOrEmpty(TextBox_Config_File.Text) Then
            command &= " -c """ & TextBox_Config_File.Text & """"
        End If

        ' Add the data folder (folder to process)
        If Not String.IsNullOrEmpty(TextBox_Data_Folder.Text) Then
            command &= " """ & TextBox_Data_Folder.Text & """"
        End If

        ' Update the TextBox_Command with the new constructed command
        TextBox_Command.Text = command
    End Sub

    ' Event Handler for the Button_SelectCLIHome click event

    Private Sub TabPage3_Click(sender As Object, e As EventArgs) Handles TabPage_RunCLI.Click

    End Sub

    Private Sub Button_InstallGit_Click(sender As Object, e As EventArgs) Handles Button_InstallGit.Click
        ' URL of the Git website
        Dim gitUrl As String = "https://git-scm.com/"

        ' Use ProcessStartInfo to open the URL in the default browser
        Try
            Dim processStartInfo As New ProcessStartInfo(gitUrl)
            processStartInfo.UseShellExecute = True
            Process.Start(processStartInfo)
        Catch ex As Exception
            MessageBox.Show($"Failed to open the website: {ex.Message}", "Error")
        End Try
    End Sub

    Private Sub Button_SelectCLIHome_Click(sender As Object, e As EventArgs) Handles Button_SelectCLIHome.Click
        ' Create a new instance of FolderBrowserDialog
        Using folderDialog As New FolderBrowserDialog
            ' Optional: Set the description displayed in the dialog
            folderDialog.Description = "Select the CLI home folder."

            ' Optional: Set the root folder (e.g., Desktop or My Computer)
            folderDialog.RootFolder = Environment.SpecialFolder.Desktop

            ' Show the dialog and check if the user selected a folder
            If folderDialog.ShowDialog = DialogResult.OK Then
                ' Copy the selected folder path to TextBox_CLIHome
                TextBox_CLIHome.Text = folderDialog.SelectedPath
                Text_CLI_Source.Text = folderDialog.SelectedPath
            End If
        End Using
    End Sub

    Private Sub Button_Folder_Browse_Click_1(sender As Object, e As EventArgs) Handles Button_Folder_Browse.Click
        ' Create a new instance of FolderBrowserDialog
        Using folderDialog As New FolderBrowserDialog
            ' Optional: Set the description displayed in the dialog
            folderDialog.Description = "Select a folder for the CLI source path."

            ' Optional: Set the root folder (e.g., Desktop or My Computer)
            folderDialog.RootFolder = Environment.SpecialFolder.Desktop

            ' Show the dialog and check if the user selected a folder
            If folderDialog.ShowDialog = DialogResult.OK Then
                ' Copy the selected folder path to CLI_Source
                Text_CLI_Source.Text = folderDialog.SelectedPath
            End If
        End Using
    End Sub

    Private Sub Button_Config_File_Click_1(sender As Object, e As EventArgs) Handles Button_Config_File.Click
        ' Create a new instance of OpenFileDialog
        Using openFileDialog As New OpenFileDialog
            ' Set the filter to show only .json files
            openFileDialog.Filter = "JSON Files (*.json)|*.json"
            openFileDialog.Title = "Select a Configuration File"

            ' Show the dialog and check if the user selected a file
            If openFileDialog.ShowDialog = DialogResult.OK Then
                ' Get only the file name and update TextBox_Config_File
                TextBox_Config_File.Text = Path.GetFileName(openFileDialog.FileName)
            End If
        End Using
    End Sub

    Private Sub Button_Data_Folder_Click_1(sender As Object, e As EventArgs) Handles Button_Data_Folder.Click
        ' Ensure CLI_Source is populated
        If String.IsNullOrWhiteSpace(Text_CLI_Source.Text) Then
            MessageBox.Show("Please select or specify a folder in CLI_Source first.", "Warning")
            Return
        End If

        ' Get the CLI_Source path
        Dim basePath = Text_CLI_Source.Text

        ' Validate that the CLI_Source path exists
        If Not IO.Directory.Exists(basePath) Then
            MessageBox.Show("The folder specified in CLI_Source does not exist.", "Error")
            Return
        End If

        ' Create a FolderBrowserDialog to select a folder
        Using folderDialog As New FolderBrowserDialog
            folderDialog.Description = "Select a data folder."
            folderDialog.RootFolder = Environment.SpecialFolder.Desktop

            ' Show the dialog and check if a folder was selected
            If folderDialog.ShowDialog = DialogResult.OK Then
                Dim selectedPath = folderDialog.SelectedPath

                ' Check if the selected path starts with the base path
                If selectedPath.StartsWith(basePath, StringComparison.OrdinalIgnoreCase) Then
                    ' Calculate the relative path
                    Dim relativePath = selectedPath.Substring(basePath.Length).TrimStart(IO.Path.DirectorySeparatorChar)
                    TextBox_Data_Folder.Text = relativePath
                Else
                    MessageBox.Show("The selected folder is not within the folder specified in CLI_Source.", "Error")
                End If
            End If
        End Using
    End Sub

    Private Sub TextBox_Command_TextChanged(sender As Object, e As EventArgs) Handles TextBox_Command.TextChanged

    End Sub



    Private Sub Button_Run_CLI_Click(sender As Object, e As EventArgs) Handles Button_Run_CLI.Click
        Dim command = TextBox_Command.Text ' Command to run
        Dim folderPath = Text_CLI_Source.Text ' Folder path from CLI_Source

        If Not String.IsNullOrWhiteSpace(command) AndAlso Not String.IsNullOrWhiteSpace(folderPath) Then
            Try
                ' Create a new process to run the command
                Dim process As New Process
                process.StartInfo.FileName = "cmd.exe"
                process.StartInfo.Arguments = $"/K {command}" ' /K keeps the Command Prompt open after execution
                process.StartInfo.WorkingDirectory = folderPath ' Set the starting directory
                process.StartInfo.UseShellExecute = True ' Allows the window to be visible
                process.StartInfo.CreateNoWindow = False ' Ensures the Command Prompt window is created

                process.Start()
            Catch ex As Exception
                MessageBox.Show($"An error occurred: {ex.Message}", "Error")
            End Try
        Else
            If String.IsNullOrWhiteSpace(folderPath) Then
                MessageBox.Show("Please enter a valid folder path in CLI_Source.", "Warning")
            End If
            If String.IsNullOrWhiteSpace(command) Then
                MessageBox.Show("Please enter a command in TextBox1.", "Warning")
            End If
        End If
    End Sub

    Private Sub Button_Download_SourceData_Click(sender As Object, e As EventArgs) Handles Button_Download_SourceData.Click
        Dim downloadUrl = "https://github.com/5etools-mirror-3/5etools-src/releases/latest"

        Try
            ' Using ProcessStartInfo to open the URL
            Dim processInfo As New ProcessStartInfo(downloadUrl) With {
            .UseShellExecute = True
        }
            Process.Start(processInfo)
        Catch ex As Exception
            MessageBox.Show("An error occurred while trying to open the link: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim downloadUrl = "https://github.com/5etools-mirror-3/5etools-img/releases/latest"

        Try
            ' Using ProcessStartInfo to open the URL
            Dim processInfo As New ProcessStartInfo(downloadUrl) With {
            .UseShellExecute = True
        }
            Process.Start(processInfo)
        Catch ex As Exception
            MessageBox.Show("An error occurred while trying to open the link: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Label10_Click(sender As Object, e As EventArgs) Handles Label10.Click

    End Sub

    Private Sub Label12_Click(sender As Object, e As EventArgs) Handles Label12.Click

    End Sub

    Private Sub Label14_Click(sender As Object, e As EventArgs)

    End Sub

    Private Async Sub Button_DL_Templates_Click(sender As Object, e As EventArgs) Handles Button_DL_Templates.Click
        Dim githubApiUrl As String = "https://api.github.com/repos/ebullient/ttrpg-convert-cli/releases/latest"

        ' Use the value in TextBox_CLIHome as the download and extraction directory.
        Dim destinationFolder As String = TextBox_CLIHome.Text.Trim()
        If Not Directory.Exists(destinationFolder) Then
            Directory.CreateDirectory(destinationFolder)
        End If

        ' Define the full path for the ZIP file in the destination folder.
        Dim zipFilePath As String = Path.Combine(destinationFolder, "templates_examples.zip")

        Try
            Using client As New HttpClient()
                client.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (Windows NT 10.0; Win64; x64)")

                ' Fetch the latest release JSON data.
                Dim jsonResponse As String = Await client.GetStringAsync(githubApiUrl)
                Dim releaseInfo As JObject = JObject.Parse(jsonResponse)
                Dim assets As JArray = releaseInfo("assets")

                Dim downloadUrl As String = ""
                If assets IsNot Nothing AndAlso assets.Count > 0 Then
                    ' Loop through the assets to find one that contains "examples.zip" in its name.
                    For Each asset As JObject In assets
                        Dim assetName As String = asset("name").ToString()
                        If assetName.ToLower().Contains("examples.zip") Then
                            downloadUrl = asset("browser_download_url").ToString()
                            Exit For
                        End If
                    Next

                    If String.IsNullOrEmpty(downloadUrl) Then
                        MessageBox.Show("No asset with 'examples.zip' found in the latest release.")
                        Return
                    End If

                    ' Optional: Debug message to confirm the download URL.
                    MessageBox.Show("Download commencing from: " & downloadUrl)

                    ' Download the asset as a byte array.
                    Dim fileBytes() As Byte = Await client.GetByteArrayAsync(downloadUrl)

                    ' Save the ZIP file to the destination folder.
                    File.WriteAllBytes(zipFilePath, fileBytes)
                    MessageBox.Show("Download complete: " & zipFilePath)

                    ' Unzip the file to the destination folder.
                    ZipFile.ExtractToDirectory(zipFilePath, destinationFolder)
                    MessageBox.Show("Unzipping complete. Files extracted to: " & destinationFolder)
                Else
                    MessageBox.Show("No assets found for the latest release.")
                End If
            End Using
        Catch ex As Exception
            MessageBox.Show("Error downloading or processing templates release: " & ex.Message)
        End Try

        ' Update TextBox_Template_Location
        TextBox_Template_Location.Text = Path.Combine("examples/templates/tools5e") ' HARD CODED?
        My.Settings.Setting_RelativeTemplatePath = TextBox_Template_Location.Text


    End Sub



    Private Sub TextBox_Template_Location_TextChanged(sender As Object, e As EventArgs) Handles TextBox_Template_Location.TextChanged
        ' Save the new location to settings
        My.Settings.TemplateLocation = TextBox_Template_Location.Text
        My.Settings.Save()

        ' Call the BuildConfigFile method to generate the text for TextBox_ConfigFile_Build
        BuildConfigFile()
    End Sub

    Private Sub TextBox_CLIHome_TextChanged(sender As Object, e As EventArgs) Handles TextBox_CLIHome.TextChanged
        ' Save the new value to settings
        My.Settings.CLIInstallLocation = TextBox_CLIHome.Text
        My.Settings.Save()

        ' Call the BuildConfigFile method to generate the text for TextBox_ConfigFile_Build
        BuildConfigFile()
    End Sub

    Private Sub ComboBox_Item_Template_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox_Item_Template.SelectedIndexChanged
        ' Store the selected value in settings
        My.Settings.Setting_Item = ComboBox_Item_Template.SelectedItem?.ToString()
        My.Settings.Save()

        ' Call the BuildConfigFile method to generate the text for TextBox_ConfigFile_Build
        BuildConfigFile()
    End Sub

    Private Sub ComboBox_Background_Template_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox_Background_Template.SelectedIndexChanged
        ' Store the selected value in settings
        My.Settings.Setting_Background = ComboBox_Background_Template.SelectedItem?.ToString
        My.Settings.Save()

        ' Call the BuildConfigFile method to generate the text for TextBox_ConfigFile_Build
        BuildConfigFile()
    End Sub

    Private Sub Text_CLI_Source_TextChanged(sender As Object, e As EventArgs) Handles Text_CLI_Source.TextChanged
        My.Settings.CLIInstallLocation = Text_CLI_Source.Text
        ' Optionally, save the settings to persist changes
        My.Settings.Save()
    End Sub

    Private Sub ComboBox_Monster_Template_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox_Monster_Template.SelectedIndexChanged
        ' Store the selected value in settings
        My.Settings.Setting_Monster = ComboBox_Monster_Template.SelectedItem?.ToString()
        My.Settings.Save()

        ' Call the BuildConfigFile method to generate the text for TextBox_ConfigFile_Build
        BuildConfigFile()
    End Sub

    Private Sub ComboBox_Template_Race_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox_Template_Race.SelectedIndexChanged
        ' Store the selected value in settings
        My.Settings.Setting_Race = ComboBox_Template_Race.SelectedItem?.ToString()
        My.Settings.Save()

        ' Call the BuildConfigFile method to generate the text for TextBox_ConfigFile_Build
        BuildConfigFile()
    End Sub

    Private Sub ComboBox_Spell_Template_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox_Spell_Template.SelectedIndexChanged
        ' Store the selected value in settings
        My.Settings.Setting_Spell = ComboBox_Spell_Template.SelectedItem?.ToString()
        My.Settings.Save()

        ' Call the BuildConfigFile method to generate the text for TextBox_ConfigFile_Build
        BuildConfigFile()
    End Sub

    Private Sub Label15_Click(sender As Object, e As EventArgs) Handles Label15.Click

    End Sub

    Private Sub Form1_MaximizedBoundsChanged(sender As Object, e As EventArgs) Handles Me.MaximizedBoundsChanged

    End Sub

    Private Sub TextBox_Adventure_TextChanged(sender As Object, e As EventArgs) Handles TextBox_Adventure.TextChanged
        ' Store the selected value in settings
        My.Settings.Setting_Adventure = TextBox_Adventure.Text?.ToString()
        My.Settings.Save()

        ' Call the BuildConfigFile method to generate the text for TextBox_ConfigFile_Build
        BuildConfigFile()
    End Sub

    Private Sub TextBox_From_TextChanged(sender As Object, e As EventArgs) Handles TextBox_From.TextChanged
        ' Store the selected value in settings
        My.Settings.Setting_Reference = TextBox_From.Text?.ToString()
        My.Settings.Save()

        ' Call the BuildConfigFile method to generate the text for TextBox_ConfigFile_Build
        BuildConfigFile()
    End Sub

    Private Sub TextBox_Book_TextChanged(sender As Object, e As EventArgs) Handles TextBox_Book.TextChanged
        ' Store the selected value in settings
        My.Settings.Setting_Book = TextBox_Book.Text?.ToString
        My.Settings.Save()

        ' Call the BuildConfigFile method to generate the text for TextBox_ConfigFile_Build
        BuildConfigFile()
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox_ConfigFile_Build.TextChanged
        ' Call the BuildConfigFile method to generate the text for TextBox_ConfigFile_Build
        BuildConfigFile()
    End Sub

    Private Sub TextBox_Compendium_Location_TextChanged(sender As Object, e As EventArgs) Handles TextBox_Compendium_Location.TextChanged
        ' Save the current value of TextBox_Compendium_Location to Setting_Compendium
        My.Settings.Setting_Compendium = TextBox_Compendium_Location.Text
        ' Optionally, save settings immediately
        My.Settings.Save()

        ' Call the BuildConfigFile method to generate the text for TextBox_ConfigFile_Build
        BuildConfigFile()
    End Sub

    Private Sub TextBox_Rules_Location_TextChanged(sender As Object, e As EventArgs) Handles TextBox_Rules_Location.TextChanged
        ' Save the current value of TextBox_Rules_Location to Setting_Compendium
        My.Settings.Setting_Rules = TextBox_Rules_Location.Text
        ' Optionally, save settings immediately
        My.Settings.Save()

        ' Call the BuildConfigFile method to generate the text for TextBox_ConfigFile_Build
        BuildConfigFile()
    End Sub

    Private Sub CheckBox_DiceRoller_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox_DiceRoller.CheckedChanged
        ' Save the new value to settings
        My.Settings.Setting_Use_DiceRoller = CheckBox_DiceRoller.Checked
        My.Settings.Save()

        ' Call the BuildConfigFile method to generate the text for TextBox_ConfigFile_Build
        BuildConfigFile()
    End Sub

    Private Sub CheckBox_Img_External_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox_Img_External.CheckedChanged
        ' Save the new value to settings
        My.Settings.Setting_Copy_ExternalImages = CheckBox_Img_External.Checked
        My.Settings.Save()

        ' Call the BuildConfigFile method to generate the text for TextBox_ConfigFile_Build
        BuildConfigFile()
    End Sub

    Private Sub CheckBox_Img_Internal_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox_Img_Internal.CheckedChanged
        ' Save the new value to settings
        My.Settings.Setting_Copy_InternalImages = CheckBox_Img_Internal.Checked
        My.Settings.Save()

        ' Call the BuildConfigFile method to generate the text for TextBox_ConfigFile_Build
        BuildConfigFile()
    End Sub

    Private Sub TextBox_tagPrefix_TextChanged(sender As Object, e As EventArgs) Handles TextBox_tagPrefix.TextChanged
        ' Call the BuildConfigFile method to generate the text for TextBox_ConfigFile_Build
        BuildConfigFile()
    End Sub

    Private Sub TextBox_Img_Folder_TextChanged(sender As Object, e As EventArgs) Handles TextBox_Img_Folder.TextChanged
        ' Call the BuildConfigFile method to generate the text for TextBox_ConfigFile_Build
        BuildConfigFile()
    End Sub

    Private Sub TextBox_Config_Name_TextChanged(sender As Object, e As EventArgs) Handles TextBox_Config_Name.TextChanged
        ' Store the selected value in settings
        My.Settings.Setting_JSON = TextBox_Config_Name.Text?.ToString()
        My.Settings.Save()
    End Sub

    Private Sub Button_Build_Json_Click(sender As Object, e As EventArgs) Handles Button_Build_Json.Click
        ' Call BuildConfigFile to ensure the JSON content is up to date
        BuildConfigFile()

        ' Retrieve the file name and location
        Dim fileName As String = My.Settings.Setting_JSON
        Dim savePath As String = System.IO.Path.Combine(My.Settings.CLIInstallLocation, fileName)

        Try
            ' Write the contents of TextBox_ConfigFile_Build to the JSON file
            System.IO.File.WriteAllText(savePath, TextBox_ConfigFile_Build.Text)

            ' Notify the user of successful save
            MessageBox.Show("JSON file saved successfully to: " & savePath, "Save Successful", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            ' Notify the user of any errors during the save
            MessageBox.Show("An error occurred while saving the file: " & ex.Message, "Save Failed", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        TextBox_Config_File.Text = My.Settings.Setting_JSON

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs)
        ' Get the absolute paths from settings
        Dim cliInstallPath = My.Settings.CLIInstallLocation
        Dim templatePath = My.Settings.TemplateLocation

        ' Remove the CLIInstallLocation part from the TemplateLocation path to get the relative part
        Dim relativePath = templatePath.Replace(cliInstallPath, "")

        ' Remove leading backslash (if any) from the relative path
        relativePath = relativePath.TrimStart("\"c)

    End Sub

    Private Sub Label19_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Button_Image_Folder_Click(sender As Object, e As EventArgs)
        ' Create a new instance of FolderBrowserDialog
        Using folderDialog As New FolderBrowserDialog
            ' Set the initial directory to the current path of TextBox_ImageFolder, if needed
            folderDialog.SelectedPath = TextBox_ImageFolder.Text

            ' Show the folder dialog and check if the user selected a folder
            If folderDialog.ShowDialog = DialogResult.OK Then
                ' Set the selected folder path in TextBox_ImageFolder
                TextBox_ImageFolder.Text = folderDialog.SelectedPath

                ' Optionally, you can save the selected folder location to My.Settings if you want to persist it
                My.Settings.Setting_ImageFolder = folderDialog.SelectedPath
                My.Settings.Save()
            End If
        End Using

        ' Extract only the folder name from the full path stored in My.Settings.Setting_ImageFolder
        Dim fullPath = My.Settings.Setting_ImageFolder
        Dim folderName = Path.GetFileName(fullPath.TrimEnd("\"c))

        ' Set the TextBox_Img_Folder.Text to the folder name
        TextBox_Img_Folder.Text = folderName




    End Sub

    Private Sub PictureBox_Info_From_Click(sender As Object, e As EventArgs) Handles PictureBox_Info_From.Click
        ' Show the message box with multiline text
        MessageBox.Show("The REFERENCE section should contain the content that you want just the elements from." & vbCrLf &
                               "For example: 1 note per monster, spell, item, etc." & vbCrLf &
                               "The ADVENTURE section should contain the Adventure content that you want markdown copies of the adventures from." & vbCrLf &
                               "The BOOKS section should contain the Rule Book content that you want markdown copies of." & vbCrLf &
                               "Anything you put in BOOKS or ADVENTURE does not need to go in REFERENCE." & vbCrLf &
                               "ADVENTURES do not need to be put in books. Think of books are rule books and adventures as adventure modules." & vbCrLf & vbCrLf &
                               "Enter the sources as SourceAcronym," & vbCrLf &
                               "The last source does not need a ," & vbCrLf &
                               "Example:" & vbCrLf & vbCrLf &
                               "MM," & vbCrLf &
                               "PHB," & vbCrLf &
                               "DMG", "Information", MessageBoxButtons.OK, MessageBoxIcon.None)

    End Sub

    Private Sub Button_ObsTTRPGTutorials_Click(sender As Object, e As EventArgs)
        Dim downloadUrl = "https://obsidianttrpgtutorials.com"

        Try
            ' Using ProcessStartInfo to open the URL
            Dim processInfo As New ProcessStartInfo(downloadUrl) With {
            .UseShellExecute = True
        }
            Process.Start(processInfo)
        Catch ex As Exception
            MessageBox.Show("An error occurred while trying to open the link: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs)
        Dim downloadUrl = "https://github.com/ebullient/ttrpg-convert-cli"

        Try
            ' Using ProcessStartInfo to open the URL
            Dim processInfo As New ProcessStartInfo(downloadUrl) With {
            .UseShellExecute = True
        }
            Process.Start(processInfo)
        Catch ex As Exception
            MessageBox.Show("An error occurred while trying to open the link: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


    Private Sub Button2_Click_1(sender As Object, e As EventArgs)
        Dim downloadUrl = "https://buymeacoffee.com/ebullient"

        Try
            ' Using ProcessStartInfo to open the URL
            Dim processInfo As New ProcessStartInfo(downloadUrl) With {
            .UseShellExecute = True
        }
            Process.Start(processInfo)
        Catch ex As Exception
            MessageBox.Show("An error occurred while trying to open the link: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub PictureBox_CLIHelp_Click(sender As Object, e As EventArgs) Handles PictureBox_CLIHelp.Click
        ' Show the message box with multiline text
        MessageBox.Show("Download the latest Windows release. eg: ttrpg-convert-cli-X.X.XX-windows-x86_64.zip", "Information", MessageBoxButtons.OK, MessageBoxIcon.None)
    End Sub

    Private Sub Button_Clone_Source_Data_Click(sender As Object, e As EventArgs) Handles Button_Clone_Source_Data.Click
        ' Retrieve the folder path from TextBox_CLIHome
        Dim cliHomePath As String = TextBox_CLIHome.Text

        ' Define the source data folder path
        Dim sourceDataFolderPath As String = Path.Combine(cliHomePath, "5etools-src\data")

        ' Update TextBox_Template_Location
        TextBox_Template_Location.Text = Path.Combine(cliHomePath, "examples\templates\tools5e") ' HERE

        ' Proceed with downloading templates (previous code)
        If Not Directory.Exists(cliHomePath) Then
            MessageBox.Show("The specified CLI home folder does not exist. Please select a valid folder.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        Dim gitCommand As String = "git clone --depth 1 https://github.com/5etools-mirror-3/5etools-src"

        Try
            Dim startInfo As New ProcessStartInfo With {
            .FileName = "cmd.exe",
            .WorkingDirectory = cliHomePath,
            .Arguments = $"/k {gitCommand}", ' Use /k to keep the window open after the command runs
            .UseShellExecute = True, ' Required to show the command window
            .CreateNoWindow = False ' Ensures the command window is visible
        }

            Process.Start(startInfo) ' Start the process

            MessageBox.Show("The template cloning process has started. Please check the command window for progress.", "Process Started", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            MessageBox.Show($"An error occurred while running the command: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        ' Update TextBox_SourceData with the source data folder path
        TextBox_SourceData.Text = sourceDataFolderPath
        TextBox_Data_Folder.Text = sourceDataFolderPath

    End Sub

    Private Sub Button_Clone_Images_Click(sender As Object, e As EventArgs) Handles Button_Clone_Images.Click
        ' Retrieve the folder path from TextBox_CLIHome
        Dim cliHomePath = TextBox_CLIHome.Text

        ' Validate the CLI home folder exists
        If Not Directory.Exists(cliHomePath) Then
            MessageBox.Show("The specified CLI home folder does not exist. Please select a valid folder.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        ' Get the drive info for the CLI home folder
        Dim driveRoot As String = Path.GetPathRoot(cliHomePath)
        Dim drive As DriveInfo = New DriveInfo(driveRoot)

        ' Define the required free space (19 GB)
        Dim requiredSpaceBytes As Long = 19L * 1024 * 1024 * 1024

        ' Check if there is enough free space
        If drive.AvailableFreeSpace < requiredSpaceBytes Then
            MessageBox.Show("There is not enough free space on the drive (" & driveRoot & "). At least 19GB of free space is required.", "Insufficient Disk Space", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        ' Define the path for the images folder
        Dim imagePath = Path.Combine(cliHomePath, "5etools-img")

        ' Git clone command for downloading images
        Dim gitCommand = "git clone --depth 1 https://github.com/5etools-mirror-3/5etools-img"

        Try
            ' Set up the process for Git clone with the command window visible
            Dim startInfo As New ProcessStartInfo With {
            .FileName = "cmd.exe",
            .WorkingDirectory = cliHomePath,
            .Arguments = $"/k {gitCommand}", ' Use /k to keep the window open after the command runs
            .UseShellExecute = True, ' Required to show the command window
            .CreateNoWindow = False ' Ensures the command window is visible
        }

            ' Run the Git clone command
            Process.Start(startInfo)

            MessageBox.Show("The image cloning process has started. This is a large download (approx. 19GB). Please be patient and check the command window for progress.", "Process Started", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            ' Handle any errors during the process
            MessageBox.Show($"An error occurred while running the command: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        ' Update TextBox_ImageFolder with the image folder path
        If Directory.Exists(imagePath) Then
            TextBox_ImageFolder.Text = imagePath
        Else
            MessageBox.Show("The image folder could not be found after cloning. Please check the repository structure.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If

    End Sub


    Private Sub Button_Pull_Data_Click(sender As Object, e As EventArgs) Handles Button_Pull_Data.Click
        ' Retrieve the folder path from TextBox_SourceData
        Dim sourceDataPath As String = TextBox_SourceData.Text

        ' Validate the folder path
        If String.IsNullOrWhiteSpace(sourceDataPath) OrElse Not Directory.Exists(sourceDataPath) Then
            MessageBox.Show("The specified source data folder does not exist. Please select or define a valid folder.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        ' Define the Git Pull command
        Dim gitCommand As String = "git pull"

        Try
            ' Set up the process to run the Git Pull command
            Dim startInfo As New ProcessStartInfo With {
            .FileName = "cmd.exe",
            .WorkingDirectory = sourceDataPath,
            .Arguments = $"/k {gitCommand}", ' Use /k to keep the window open after the command runs
            .UseShellExecute = True, ' Required to show the command window
            .CreateNoWindow = False ' Ensures the command window is visible
        }

            ' Start the process
            Process.Start(startInfo)

            ' Notify the user
            MessageBox.Show("The Git Pull process has started. Please check the command window for progress.", "Process Started", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            ' Handle any errors
            MessageBox.Show($"An error occurred while running the Git Pull command: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        My.Settings.Setting_DataFolder_Full = sourceDataPath

    End Sub

    Private Sub Button_Pull_Images_Click(sender As Object, e As EventArgs) Handles Button_Pull_Images.Click
        ' Retrieve the folder path from TextBox_CLIHome
        Dim cliHomePath As String = TextBox_CLIHome.Text

        ' Update TextBox_ImageFolder
        cliHomePath = Path.Combine(cliHomePath, "5etools-img")
        TextBox_ImageFolder.Text = cliHomePath


        ' Proceed with downloading templates (previous code)
        If Not Directory.Exists(cliHomePath) Then
            MessageBox.Show("The specified CLI home folder does not exist. Please select a valid folder.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        Dim gitCommand As String = "git pull https://github.com/5etools-mirror-3/5etools-img"

        Try
            Dim startInfo As New ProcessStartInfo With {
            .FileName = "cmd.exe",
            .WorkingDirectory = cliHomePath,
            .Arguments = $"/k {gitCommand}", ' Use /k to keep the window open after the command runs
            .UseShellExecute = True, ' Required to show the command window
            .CreateNoWindow = False ' Ensures the command window is visible
        }

            Process.Start(startInfo) ' Start the process

            MessageBox.Show("The template cloning process has started. Please check the command window for progress.", "Process Started", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            MessageBox.Show($"An error occurred while running the command: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub Button_Template_Site_Click(sender As Object, e As EventArgs) Handles Button_Template_Site.Click
        Dim downloadUrl = "https://github.com/ebullient/ttrpg-convert-cli"

        Try
            ' Using ProcessStartInfo to open the URL
            Dim processInfo As New ProcessStartInfo(downloadUrl) With {
            .UseShellExecute = True
        }
            Process.Start(processInfo)
        Catch ex As Exception
            MessageBox.Show("An error occurred while trying to open the link: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub TextBox_ImageFolder_TextChanged(sender As Object, e As EventArgs) Handles TextBox_ImageFolder.TextChanged
        ' Save the new value to settings
        My.Settings.Setting_ImageFolder = TextBox_ImageFolder.Text
        My.Settings.Save()

        ' Call the BuildConfigFile method to generate the text for TextBox_ConfigFile_Build
        BuildConfigFile()
    End Sub

    Private Sub CheckBox_FantStatPlugin_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox_FantStatPlugin.CheckedChanged

        PopulateMonsterTemplates() 'Refresh the options in the monster template combo box.

        ' Save the new value to settings
        My.Settings.Setting_Use_FantasyStat = CheckBox_FantStatPlugin.Checked
        My.Settings.Save()

    End Sub

    Private Sub Button_Clone_Homebrew_Click(sender As Object, e As EventArgs) Handles Button_Clone_Homebrew.Click
        ' Retrieve the folder path from TextBox_CLIHome
        Dim cliHomePath = TextBox_CLIHome.Text

        ' Define the path for the images
        Dim homebrewPath = Path.Combine(cliHomePath, "homebrew")

        ' Validate the CLI home folder
        If Not Directory.Exists(cliHomePath) Then
            MessageBox.Show("The specified homebrew folder does not exist. Please select a valid folder.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        ' Git clone command for downloading templates
        Dim gitCommand = "git clone --depth 1 https://github.com/TheGiddyLimit/homebrew"

        Try
            ' Set up the process for Git clone with the command window visible
            Dim startInfo As New ProcessStartInfo With {
            .FileName = "cmd.exe",
            .WorkingDirectory = cliHomePath,
            .Arguments = $"/k {gitCommand}", ' Use /k to keep the window open after the command runs
            .UseShellExecute = True, ' Required to show the command window
            .CreateNoWindow = False ' Ensures the command window is visible
        }

            ' Run the Git clone command
            Process.Start(startInfo)

            ' Update TextBox_ImageFolder with the image folder path
            If Directory.Exists(homebrewPath) Then
                TextBox_Homebrew_Folder.Text = homebrewPath
            Else
                MessageBox.Show("The homebrew folder could not be found after cloning. Please check the repository structure.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If

            MessageBox.Show("The homebrew cloning process has started. Please check the command window for progress.", "Process Started", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            ' Handle any errors during the process
            MessageBox.Show($"An error occurred while running the command: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub Button_Pull_Templates_Click(sender As Object, e As EventArgs)
        ' Retrieve the folder path from TextBox_CLIHome
        Dim cliHomePath = TextBox_CLIHome.Text

        ' Update TextBox_Template_Location
        cliHomePath = Path.Combine(cliHomePath, "BUGSPLAT") ' cliHomePath = Path.Combine(cliHomePath, "ttrpg-convert-cli")
        My.Settings.Setting_InstallTemplateLocation = cliHomePath

        ' Proceed with downloading templates (previous code)
        If Not Directory.Exists(cliHomePath) Then
            MessageBox.Show("The specified template folder does not exist. Please select a valid folder.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        Dim gitCommand = "git pull https://github.com/ebullient/ttrpg-convert-cli"

        Try
            Dim startInfo As New ProcessStartInfo With {
            .FileName = "cmd.exe",
            .WorkingDirectory = cliHomePath,
            .Arguments = $"/k {gitCommand}", ' Use /k to keep the window open after the command runs
            .UseShellExecute = True, ' Required to show the command window
            .CreateNoWindow = False ' Ensures the command window is visible
        }

            Process.Start(startInfo) ' Start the process

            MessageBox.Show("The template cloning process has started. Please check the command window for progress.", "Process Started", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            MessageBox.Show($"An error occurred while running the command: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub TabPage_Setup_Click(sender As Object, e As EventArgs) Handles TabPage_Setup.Click

    End Sub

    Private Sub Button_Pull_Homebrew_Click(sender As Object, e As EventArgs) Handles Button_Pull_Homebrew.Click
        ' Retrieve the folder path from TextBox_CLIHome
        Dim cliHomePath As String = TextBox_CLIHome.Text

        ' Update TextBox_Template_Location
        cliHomePath = Path.Combine(cliHomePath, "homebrew")
        TextBox_Homebrew_Folder.Text = cliHomePath


        ' Proceed with downloading templates (previous code)
        If Not Directory.Exists(cliHomePath) Then
            MessageBox.Show("The specified homebrew folder does not exist. Please select a valid folder.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        Dim gitCommand As String = "git pull https://github.com/TheGiddyLimit/homebrew"

        Try
            Dim startInfo As New ProcessStartInfo With {
            .FileName = "cmd.exe",
            .WorkingDirectory = cliHomePath,
            .Arguments = $"/k {gitCommand}", ' Use /k to keep the window open after the command runs
            .UseShellExecute = True, ' Required to show the command window
            .CreateNoWindow = False ' Ensures the command window is visible
        }

            Process.Start(startInfo) ' Start the process

            MessageBox.Show("The homebrew cloning process has started. Please check the command window for progress.", "Process Started", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            MessageBox.Show($"An error occurred while running the command: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try


    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Dim downloadUrl = "https://github.com/TheGiddyLimit/homebrew"

        Try
            ' Using ProcessStartInfo to open the URL
            Dim processInfo As New ProcessStartInfo(downloadUrl) With {
            .UseShellExecute = True
        }
            Process.Start(processInfo)
        Catch ex As Exception
            MessageBox.Show("An error occurred while trying to open the link: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub TextBox_Homebrew_Folder_TextChanged(sender As Object, e As EventArgs) Handles TextBox_Homebrew_Folder.TextChanged
        ' Save the new location to settings
        My.Settings.Setting_Homebrew_Folder = TextBox_Homebrew_Folder.Text
        My.Settings.Save()

        ' Call the BuildConfigFile method to generate the text for TextBox_ConfigFile_Build
        BuildConfigFile()
    End Sub

    Private Sub Button_Add_Homebrew_Click(sender As Object, e As EventArgs) Handles Button_Add_Homebrew.Click
        ' Retrieve the folder path from the setting
        Dim homebrewFolder = My.Settings.Setting_Homebrew_Folder

        ' Check if the folder exists
        If Not Directory.Exists(homebrewFolder) Then
            MessageBox.Show("The specified Homebrew folder does not exist. Please select a valid folder.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        ' Open the file dialog to let the user select a file
        Using openFileDialog As New OpenFileDialog
            openFileDialog.InitialDirectory = homebrewFolder ' Start in the folder defined in Setting_Homebrew_Folder
            openFileDialog.Filter = "JSON Files (*.json)|*.json|All Files (*.*)|*.*" ' Only show .json files

            ' Show the file dialog and check if the user selected a file
            If openFileDialog.ShowDialog = DialogResult.OK Then
                ' Get the selected file path
                Dim selectedFilePath = openFileDialog.FileName

                ' Format the file path as required
                Dim folderName = Path.GetFileName(Path.GetDirectoryName(selectedFilePath)) ' Get folder name
                Dim fileName = Path.GetFileName(selectedFilePath) ' Get file name

                ' Format the string with a "/" instead of ";" between the folder and file
                Dim formattedPath = $"homebrew/{folderName}/{fileName}"

                ' If the TextBox is not empty, append a comma and newline before adding the new file
                If Not String.IsNullOrEmpty(TextBox_Homebrew_Content.Text) Then
                    ' Add a comma and newline to the end of the current text if it's not already there
                    If Not TextBox_Homebrew_Content.Text.EndsWith(Environment.NewLine) Then
                        TextBox_Homebrew_Content.Text &= "," & Environment.NewLine
                    End If
                End If

                ' Add the new file path (no comma needed here as it’s a new line)
                TextBox_Homebrew_Content.Text &= formattedPath
            End If
        End Using
    End Sub

    Private Sub TextBox_Homebrew_Content_TextChanged(sender As Object, e As EventArgs) Handles TextBox_Homebrew_Content.TextChanged
        ' Store the selected value in settings
        My.Settings.Setting_Homebrew = TextBox_Homebrew_Content.Text?.ToString
        My.Settings.Save()

        ' Call the BuildConfigFile method to generate the text for TextBox_ConfigFile_Build
        BuildConfigFile()
    End Sub

    Private Sub Button_Source_Map_Click(sender As Object, e As EventArgs) Handles Button_Source_Map.Click
        Dim downloadUrl = "https://github.com/ebullient/ttrpg-convert-cli/blob/main/docs/sourceMap.md"

        Try
            ' Using ProcessStartInfo to open the URL
            Dim processInfo As New ProcessStartInfo(downloadUrl) With {
            .UseShellExecute = True
        }
            Process.Start(processInfo)
        Catch ex As Exception
            MessageBox.Show("An error occurred while trying to open the link: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub TextBox2_TextChanged(sender As Object, e As EventArgs) Handles TextBox2.TextChanged

    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        ' Show the message box with multiline text
        MessageBox.Show("You need to extract the zip file you downloaded above into the folder." & vbCrLf &
                        "Example: You might create this folder: C:\CLI\5eTools\" & vbCrLf &
                        "Inside you place the extraced contents of the zip file." & vbCrLf &
                        "Which would result in: C:\CLI\5eTools\bin\" & vbCrLf &
                        "Inside the bin folder is the CLI executable: ttrpg-convert.exe" & vbCrLf &
                        "In the next steps you will be adding more folders into the bin folder.", "Information", MessageBoxButtons.OK, MessageBoxIcon.None)
    End Sub

    Private Sub TextBox4_TextChanged(sender As Object, e As EventArgs) Handles TextBox4.TextChanged

    End Sub

    Private Sub TextBox1_TextChanged_1(sender As Object, e As EventArgs) Handles TextBox1.TextChanged

    End Sub

    Private Sub TextBox_SourceData_TextChanged(sender As Object, e As EventArgs) Handles TextBox_SourceData.TextChanged

    End Sub

    Private Sub Button_Browse_Data_Source_Click(sender As Object, e As EventArgs) Handles Button_Browse_Data_Source.Click
        ' Create and configure a FolderBrowserDialog
        Using folderDialog As New FolderBrowserDialog
            folderDialog.Description = "Select a Data Source Folder"
            folderDialog.ShowNewFolderButton = True

            ' Check if there's an existing path in the settings and use it as the initial path
            If Not String.IsNullOrEmpty(My.Settings.Setting_DataFolder_Full) Then
                folderDialog.SelectedPath = My.Settings.Setting_DataFolder_Full
            End If

            ' Show the dialog and handle the result
            If folderDialog.ShowDialog() = DialogResult.OK Then
                ' Update the selected folder path in the settings
                My.Settings.Setting_DataFolder_Full = folderDialog.SelectedPath
                My.Settings.Save()

                ' Optionally show the selected path in a MessageBox or another control
                MessageBox.Show($"Data source folder updated to: {folderDialog.SelectedPath}", "Folder Selected", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        End Using


        TextBox_SourceData.Text = My.Settings.Setting_DataFolder_Full
    End Sub

    Private Sub Button_Browse_Image_Folder_Click(sender As Object, e As EventArgs) Handles Button_Browse_Image_Folder.Click
        ' Create and configure a FolderBrowserDialog
        Using folderDialog As New FolderBrowserDialog
            folderDialog.Description = "Select a Image Source Folder"
            folderDialog.ShowNewFolderButton = True

            ' Check if there's an existing path in the settings and use it as the initial path
            If Not String.IsNullOrEmpty(My.Settings.Setting_ImageFolder) Then
                folderDialog.SelectedPath = My.Settings.Setting_ImageFolder
            End If

            ' Show the dialog and handle the result
            If folderDialog.ShowDialog() = DialogResult.OK Then
                ' Update the selected folder path in the settings
                My.Settings.Setting_ImageFolder = folderDialog.SelectedPath
                My.Settings.Save()

                ' Optionally show the selected path in a MessageBox or another control
                MessageBox.Show($"Image source folder updated to: {folderDialog.SelectedPath}", "Folder Selected", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        End Using

        TextBox_ImageFolder.Text = My.Settings.Setting_ImageFolder
    End Sub

    Private Sub Button_Browse_Templates_Click(sender As Object, e As EventArgs) Handles Button_Browse_Templates.Click
        ' Create and configure a FolderBrowserDialog
        Using folderDialog As New FolderBrowserDialog
            folderDialog.Description = "Select a Template Source Folder"
            folderDialog.ShowNewFolderButton = True

            ' Check if there's an existing path in the settings and use it as the initial path
            If Not String.IsNullOrEmpty(My.Settings.TemplateLocation) Then
                folderDialog.SelectedPath = My.Settings.TemplateLocation
            End If

            ' Show the dialog and handle the result
            If folderDialog.ShowDialog() = DialogResult.OK Then
                ' Update the selected folder path in the settings
                My.Settings.TemplateLocation = folderDialog.SelectedPath
                My.Settings.Save()

                ' Optionally show the selected path in a MessageBox or another control
                MessageBox.Show($"Template source folder updated to: {folderDialog.SelectedPath}", "Folder Selected", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        End Using

        TextBox_Template_Location.Text = My.Settings.TemplateLocation
    End Sub

    Private Sub Button_Browse_Homebrew_Click(sender As Object, e As EventArgs) Handles Button_Browse_Homebrew.Click
        ' Create and configure a FolderBrowserDialog
        Using folderDialog As New FolderBrowserDialog
            folderDialog.Description = "Select a Homebrew Source Folder"
            folderDialog.ShowNewFolderButton = True

            ' Check if there's an existing path in the settings and use it as the initial path
            If Not String.IsNullOrEmpty(My.Settings.Setting_Homebrew_Folder) Then
                folderDialog.SelectedPath = My.Settings.Setting_Homebrew_Folder
            End If

            ' Show the dialog and handle the result
            If folderDialog.ShowDialog() = DialogResult.OK Then
                ' Update the selected folder path in the settings
                My.Settings.Setting_Homebrew_Folder = folderDialog.SelectedPath
                My.Settings.Save()

                ' Optionally show the selected path in a MessageBox or another control
                MessageBox.Show($"Template source folder updated to: {folderDialog.SelectedPath}", "Folder Selected", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        End Using

        TextBox_Homebrew_Folder.Text = My.Settings.Setting_Homebrew_Folder
    End Sub

    Private Sub Label22_Click(sender As Object, e As EventArgs) Handles Label22.Click

    End Sub

    Private Sub TabControl1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TabControl1.SelectedIndexChanged

        PopulateBackgroundTemplates()
        PopulateMonsterTemplates()
        PopulateItemTemplates()
        PopulateSpellTemplates()
        PopulateRaceTemplates()
    End Sub

    Private Sub PictureBox_SourceMap_Click(sender As Object, e As EventArgs) Handles PictureBox_SourceMap.Click
        Dim url = "https://github.com/ebullient/ttrpg-convert-cli/blob/main/docs/sourceMap.md" ' Replace with your desired URL.
        Try
            Dim psi As New ProcessStartInfo With {
            .FileName = url,
            .UseShellExecute = True
        }
            Process.Start(psi)
        Catch ex As Exception
            MessageBox.Show("Could not open the website: " & ex.Message)
        End Try
    End Sub

    Private Sub PictureBox_SourceMap2_Click(sender As Object, e As EventArgs) Handles PictureBox_SourceMap2.Click
        Dim url = "https://github.com/ebullient/ttrpg-convert-cli/blob/main/docs/sourceMap.md" ' Replace with your desired URL.
        Try
            Dim psi As New ProcessStartInfo With {
            .FileName = url,
            .UseShellExecute = True
        }
            Process.Start(psi)
        Catch ex As Exception
            MessageBox.Show("Could not open the website: " & ex.Message)
        End Try
    End Sub


    Private Sub PictureBox_SourceMap3_Click(sender As Object, e As EventArgs) Handles PictureBox_SourceMap3.Click
        Dim url = "https://github.com/ebullient/ttrpg-convert-cli/blob/main/docs/sourceMap.md"
        Try
            Dim psi As New ProcessStartInfo With {
            .FileName = url,
            .UseShellExecute = True
        }
            Process.Start(psi)
        Catch ex As Exception
            MessageBox.Show("Could not open the website: " & ex.Message)
        End Try
    End Sub

    Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles Button1.Click
        Dim url = "https://github.com/ebullient/ttrpg-convert-cli/blob/main/docs/configuration.md"
        Try
            Dim psi As New ProcessStartInfo With {
            .FileName = url,
            .UseShellExecute = True
        }
            Process.Start(psi)
        Catch ex As Exception
            MessageBox.Show("Could not open the website: " & ex.Message)
        End Try
    End Sub

    Private Sub Button2_Click_2(sender As Object, e As EventArgs) Handles Button2.Click
        Dim url = "https://github.com/ebullient/ttrpg-convert-cli?tab=readme-ov-file#convert-homebrew-json-data"
        Try
            Dim psi As New ProcessStartInfo With {
            .FileName = url,
            .UseShellExecute = True
        }
            Process.Start(psi)
        Catch ex As Exception
            MessageBox.Show("Could not open the website: " & ex.Message)
        End Try
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Dim url = "https://obsidianttrpgtutorials.com/Obsidian+TTRPG+Tutorials/Plugin+Tutorials/TTRPG-Convert-CLI/TTRPG-Convert-CLI+5e#Common+Errors"
        Try
            Dim psi As New ProcessStartInfo With {
            .FileName = url,
            .UseShellExecute = True
        }
            Process.Start(psi)
        Catch ex As Exception
            MessageBox.Show("Could not open the website: " & ex.Message)
        End Try
    End Sub

    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        Dim url = "https://github.com/ebullient/ttrpg-convert-cli/blob/main/docs/configuration.md#reporting-content-errors-to-5etools"
        Try
            Dim psi As New ProcessStartInfo With {
            .FileName = url,
            .UseShellExecute = True
        }
            Process.Start(psi)
        Catch ex As Exception
            MessageBox.Show("Could not open the website: " & ex.Message)
        End Try
    End Sub

    Private Sub Button10_Click(sender As Object, e As EventArgs) Handles Button10.Click
        Dim url = "https://github.com/ebullient/ttrpg-convert-cli/issues"
        Try
            Dim psi As New ProcessStartInfo With {
            .FileName = url,
            .UseShellExecute = True
        }
            Process.Start(psi)
        Catch ex As Exception
            MessageBox.Show("Could not open the website: " & ex.Message)
        End Try
    End Sub

    Private Sub Label_TagPrefix_Click(sender As Object, e As EventArgs) Handles Label_TagPrefix.Click
        Dim url As String = "https://github.com/ebullient/ttrpg-convert-cli/blob/main/docs/configuration.md#tag-prefix"
        Try
            Dim psi As New ProcessStartInfo With {
                .FileName = url,
                .UseShellExecute = True
            }
            Process.Start(psi)
        Catch ex As Exception
            MessageBox.Show("Could not open the website: " & ex.Message)
        End Try
    End Sub

    Private Sub Label_ReprintBehaviour_Click(sender As Object, e As EventArgs) Handles Label_ReprintBehaviour.Click
        Dim url As String = "https://github.com/ebullient/ttrpg-convert-cli/blob/main/docs/configuration.md#reprint-behavior"
        Try
            Dim psi As New ProcessStartInfo With {
                .FileName = url,
                .UseShellExecute = True
            }
            Process.Start(psi)
        Catch ex As Exception
            MessageBox.Show("Could not open the website: " & ex.Message)
        End Try
    End Sub

    Private Sub PictureBox2_Click(sender As Object, e As EventArgs) Handles PictureBox2.Click
        Dim url = "https://github.com/ebullient/ttrpg-convert-cli/blob/main/docs/configuration.md#use-the-dice-roller-plugin"
        Try
            Dim psi As New ProcessStartInfo With {
                .FileName = url,
                .UseShellExecute = True
            }
            Process.Start(psi)
        Catch ex As Exception
            MessageBox.Show("Could not open the website: " & ex.Message)
        End Try
    End Sub

    Private Sub PictureBox5_Click(sender As Object, e As EventArgs) Handles PictureBox5.Click
        Dim url = "https://github.com/ebullient/ttrpg-convert-cli/blob/main/docs/configuration.md#render-with-fantasy-statblocks"
        Try
            Dim psi As New ProcessStartInfo With {
                .FileName = url,
                .UseShellExecute = True
            }
            Process.Start(psi)
        Catch ex As Exception
            MessageBox.Show("Could not open the website: " & ex.Message)
        End Try
    End Sub

    Private Sub PictureBox3_Click(sender As Object, e As EventArgs) Handles PictureBox3.Click
        Dim url = "https://github.com/ebullient/ttrpg-convert-cli/blob/main/docs/configuration.md#images"
        Try
            Dim psi As New ProcessStartInfo With {
                .FileName = url,
                .UseShellExecute = True
            }
            Process.Start(psi)
        Catch ex As Exception
            MessageBox.Show("Could not open the website: " & ex.Message)
        End Try
    End Sub

    Private Sub PictureBox4_Click(sender As Object, e As EventArgs) Handles PictureBox4.Click
        Dim url = "https://github.com/ebullient/ttrpg-convert-cli/blob/main/docs/configuration.md#images"
        Try
            Dim psi As New ProcessStartInfo With {
                .FileName = url,
                .UseShellExecute = True
            }
            Process.Start(psi)
        Catch ex As Exception
            MessageBox.Show("Could not open the website: " & ex.Message)
        End Try
    End Sub

    Private Sub PictureBox6_Click(sender As Object, e As EventArgs) Handles PictureBox6.Click
        Dim url = "https://github.com/ebullient/ttrpg-convert-cli/blob/main/docs/configuration.md#homebrew"
        Try
            Dim psi As New ProcessStartInfo With {
                .FileName = url,
                .UseShellExecute = True
            }
            Process.Start(psi)
        Catch ex As Exception
            MessageBox.Show("Could not open the website: " & ex.Message)
        End Try
    End Sub

    Private Sub PictureBox7_Click(sender As Object, e As EventArgs) Handles PictureBox7.Click
        Dim url = "https://github.com/ebullient/ttrpg-convert-cli/blob/main/docs/configuration.md#specify-target-paths-paths-key"
        Try
            Dim psi As New ProcessStartInfo With {
                .FileName = url,
                .UseShellExecute = True
            }
            Process.Start(psi)
        Catch ex As Exception
            MessageBox.Show("Could not open the website: " & ex.Message)
        End Try
    End Sub

    Private Sub PictureBox8_Click(sender As Object, e As EventArgs) Handles PictureBox8.Click
        Dim url = "https://github.com/ebullient/ttrpg-convert-cli/blob/main/docs/configuration.md#specify-target-paths-paths-key"
        Try
            Dim psi As New ProcessStartInfo With {
                .FileName = url,
                .UseShellExecute = True
            }
            Process.Start(psi)
        Catch ex As Exception
            MessageBox.Show("Could not open the website: " & ex.Message)
        End Try
    End Sub

    Private Sub PictureBox9_Click(sender As Object, e As EventArgs) Handles PictureBox9.Click
        Dim url = "https://github.com/ebullient/ttrpg-convert-cli/blob/main/docs/configuration.md#templates"
        Try
            Dim psi As New ProcessStartInfo With {
                .FileName = url,
                .UseShellExecute = True
            }
            Process.Start(psi)
        Catch ex As Exception
            MessageBox.Show("Could not open the website: " & ex.Message)
        End Try
    End Sub

    Private Sub PictureBox10_Click(sender As Object, e As EventArgs) Handles PictureBox10.Click

    End Sub

    Private Sub Label38_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Dim url = "https://buymeacoffee.com/ebullient"
        Try
            Dim psi As New ProcessStartInfo With {
                .FileName = url,
                .UseShellExecute = True
            }
            Process.Start(psi)
        Catch ex As Exception
            MessageBox.Show("Could not open the website: " & ex.Message)
        End Try
    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        Dim url = "https://obsidianttrpgtutorials.com"
        Try
            Dim psi As New ProcessStartInfo With {
            .FileName = url,
            .UseShellExecute = True
        }
            Process.Start(psi)
        Catch ex As Exception
            MessageBox.Show("Could not open the website: " & ex.Message)
        End Try
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        Dim url = "https://github.com/ebullient/ttrpg-convert-cli/"
        Try
            Dim psi As New ProcessStartInfo With {
            .FileName = url,
            .UseShellExecute = True
        }
            Process.Start(psi)
        Catch ex As Exception
            MessageBox.Show("Could not open the website: " & ex.Message)
        End Try
    End Sub

    Private Sub TextBox3_TextChanged(sender As Object, e As EventArgs) Handles TextBox3.TextChanged

    End Sub

    Private Sub Button11_Click(sender As Object, e As EventArgs) Handles Button11.Click
        ' Build the examples/css-snippets path under the CLI home folder
        Dim examplesPath = Path.Combine(TextBox_CLIHome.Text, "examples", "css-snippets")

        If Directory.Exists(examplesPath) Then
            ' Open the folder in Windows Explorer
            Dim psi As New ProcessStartInfo With {
                .FileName = "explorer.exe",
                .Arguments = """" & examplesPath & """",
                .UseShellExecute = False
            }
            Process.Start(psi)
        Else
            MessageBox.Show($"Folder not found:{Environment.NewLine}{examplesPath}", "Path Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If
    End Sub

    Private Sub Label14_Click_1(sender As Object, e As EventArgs) Handles Label14.Click

    End Sub

    Private Sub Button12_Click(sender As Object, e As EventArgs) Handles Button12.Click
        Dim url = "https://obsidianttrpgtutorials.com/Obsidian+TTRPG+Tutorials/Tutorials/Basic+Tool+Usage/How+To+-+Install+Custom+CSS"
        Try
            Dim psi As New ProcessStartInfo With {
            .FileName = url,
            .UseShellExecute = True
        }
            Process.Start(psi)
        Catch ex As Exception
            MessageBox.Show("Could not open the website: " & ex.Message)
        End Try
    End Sub
End Class

