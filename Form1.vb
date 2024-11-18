Imports System.IO

Public Class Form1

    Private WithEvents infoToolTip As New ToolTip()

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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


        ' Call the BuildConfigFile method to generate the text for TextBox_ConfigFile_Build
        BuildConfigFile()

    End Sub

    Private Sub UpdateTemplateRootPath()
        ' Private subroutine to update TextBox_Template_Root
        ' Get the absolute paths from settings
        Dim cliInstallPath As String = My.Settings.CLIInstallLocation
        Dim templatePath As String = My.Settings.TemplateLocation

        ' Remove the CLIInstallLocation part from the TemplateLocation path to get the relative part
        Dim relativePath As String = templatePath.Replace(cliInstallPath, "")

        ' Remove leading backslash (if any) from the relative path
        relativePath = relativePath.TrimStart("\"c)

        ' Replace all backslashes with forward slashes
        relativePath = relativePath.Replace("\"c, "/"c)

        ' Store the calculated relative path in My.Settings.Setting_RelativeTemplatePath
        My.Settings.Setting_RelativeTemplatePath = relativePath
    End Sub

    Private Sub BuildConfigFile()
        ' Initialize the configText variable
        Dim configText As String = "{"

        ' "from" section
        configText &= vbCrLf & "  ""from"" : ["
        If Not String.IsNullOrEmpty(My.Settings.Setting_From) Then
            Dim fromArray = My.Settings.Setting_From.Split(","c).Select(Function(item) """" & item.Trim() & """")
            configText &= vbCrLf & "    " & String.Join("," & vbCrLf & "    ", fromArray)
        End If
        configText &= vbCrLf & "  ],"

        ' "paths" section
        configText &= vbCrLf & "  ""paths"" : {"
        configText &= vbCrLf & "    ""compendium"" : """ & My.Settings.Setting_Compendium & "/3-Mechanics/CLI/"","
        configText &= vbCrLf & "    ""rules"" : """ & My.Settings.Setting_Rules & "/3-Mechanics/CLI/rules/"""
        configText &= vbCrLf & "  },"

        ' "template" section
        configText &= vbCrLf & "  ""template"" : {"
        configText &= vbCrLf & "    ""background"" : """ & My.Settings.Setting_RelativeTemplatePath & "/" & My.Settings.Setting_Background & ""","
        configText &= vbCrLf & "    ""monster"" : """ & My.Settings.Setting_RelativeTemplatePath & "/" & My.Settings.Setting_Monster & ""","
        configText &= vbCrLf & "    ""item"" : """ & My.Settings.Setting_RelativeTemplatePath & "/" & My.Settings.Setting_Item & ""","
        configText &= vbCrLf & "    ""race"" : """ & My.Settings.Setting_RelativeTemplatePath & "/" & My.Settings.Setting_Race & ""","
        configText &= vbCrLf & "    ""spell"" : """ & My.Settings.Setting_RelativeTemplatePath & "/" & My.Settings.Setting_Spell & """"
        configText &= vbCrLf & "  },"

        ' "useDiceRoller" section
        configText &= vbCrLf & "  ""useDiceRoller"" : " & If(CheckBox_DiceRoller.Checked, "true", "false") & ","

        ' "tagPrefix" section
        configText &= vbCrLf & "  ""tagPrefix"" : """ & TextBox_tagPrefix.Text & ""","

        ' "yamlStatblocks" section
        configText &= vbCrLf & "  ""yamlStatblocks"" : true,"

        ' "images" section
        configText &= vbCrLf & "  ""images"" : {"
        configText &= vbCrLf & "    ""copyExternal"" : " & If(CheckBox_Img_External.Checked, "true", "false") & ","
        configText &= vbCrLf & "    ""copyInternal"" : " & If(CheckBox_Img_Internal.Checked, "true", "false") & ","
        configText &= vbCrLf & "    ""internalRoot"" : ""5etools-img"""
        configText &= vbCrLf & "  },"

        ' "full-source" section
        configText &= vbCrLf & "  ""full-source"" : {"

        ' "adventure" section
        configText &= vbCrLf & "    ""adventure"" : ["
        If Not String.IsNullOrEmpty(My.Settings.Setting_Adventure) Then
            Dim adventureArray = My.Settings.Setting_Adventure.Split(","c).Select(Function(item) """" & item.Trim() & """")
            configText &= vbCrLf & "      " & String.Join("," & vbCrLf & "      ", adventureArray)
        End If
        configText &= vbCrLf & "    ],"

        ' "book" section
        configText &= vbCrLf & "    ""book"" : ["
        If Not String.IsNullOrEmpty(My.Settings.Setting_Book) Then
            Dim bookArray = My.Settings.Setting_Book.Split(","c).Select(Function(item) """" & item.Trim() & """")
            configText &= vbCrLf & "      " & String.Join("," & vbCrLf & "      ", bookArray)
        End If
        configText &= vbCrLf & "    ]"
        configText &= vbCrLf & "  }"

        configText &= vbCrLf & "}"

        ' Assign the text to the TextBox
        TextBox_ConfigFile_Build.Text = configText
    End Sub

    Private Sub UpdateSpellTemplateList()
        ' Clear existing items in the ComboBox
        ComboBox_Spell_Template.Items.Clear()

        ' Get the location from TextBox_Template_Location
        Dim templateLocation As String = TextBox_Template_Location.Text

        ' Ensure the directory exists
        If Directory.Exists(templateLocation) Then
            ' Get all *.txt files containing "spell" in the name
            Dim spellFiles = Directory.GetFiles(templateLocation, "*.txt").
                         Where(Function(f) f.ToLower().Contains("spell")).ToArray()

            ' Add these files to the ComboBox
            For Each file As String In spellFiles
                ComboBox_Spell_Template.Items.Add(Path.GetFileName(file))
            Next
        End If
    End Sub

    Private Sub UpdateRaceTemplateList()
        ' Clear existing items in the ComboBox
        ComboBox_Template_Race.Items.Clear()

        ' Get the location from TextBox_Template_Location
        Dim templateLocation As String = TextBox_Template_Location.Text

        ' Ensure the directory exists
        If Directory.Exists(templateLocation) Then
            ' Get all *.txt files containing "race" in the name
            Dim raceFiles = Directory.GetFiles(templateLocation, "*.txt").
                        Where(Function(f) f.ToLower().Contains("race")).ToArray()

            ' Add these files to the ComboBox
            For Each file As String In raceFiles
                ComboBox_Template_Race.Items.Add(Path.GetFileName(file))
            Next
        End If
    End Sub

    Private Sub UpdateItemTemplateList()
        ' Clear existing items in the ComboBox
        ComboBox_Item_Template.Items.Clear()

        ' Get the location from TextBox_Template_Location
        Dim templateLocation As String = TextBox_Template_Location.Text

        ' Ensure the directory exists
        If Directory.Exists(templateLocation) Then
            ' Get all *.txt files containing "item" in the name
            Dim itemFiles = Directory.GetFiles(templateLocation, "*.txt").
                         Where(Function(f) f.ToLower().Contains("item")).ToArray()

            ' Add these files to the ComboBox
            For Each file As String In itemFiles
                ComboBox_Item_Template.Items.Add(Path.GetFileName(file))
            Next
        End If
    End Sub
    Private Sub UpdateBackgroundTemplateList()
        ' Clear existing items in the ComboBox
        ComboBox_Background_Template.Items.Clear()

        ' Get the location from TextBox_Template_Location
        Dim templateLocation As String = TextBox_Template_Location.Text

        ' Ensure the directory exists
        If Directory.Exists(templateLocation) Then
            ' Get all *.txt files containing "background" in the name
            Dim backgroundFiles = Directory.GetFiles(templateLocation, "*.txt").
                               Where(Function(f) f.ToLower().Contains("background")).ToArray()

            ' Add these files to the ComboBox
            For Each file As String In backgroundFiles
                ComboBox_Background_Template.Items.Add(Path.GetFileName(file))
            Next
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
                CLI_Source.Text = folderDialog.SelectedPath
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

    Private Sub Label5_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Button_Data_Folder_Click(sender As Object, e As EventArgs)
        ' Ensure CLI_Source is populated
        If String.IsNullOrWhiteSpace(CLI_Source.Text) Then
            MessageBox.Show("Please select or specify a folder in CLI_Source first.", "Warning")
            Return
        End If

        ' Get the CLI_Source path
        Dim basePath = CLI_Source.Text

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
    Private Sub CLI_Source_TextChanged(sender As Object, e As EventArgs) Handles CLI_Source.TextChanged
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
            End If
        End Using
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        ' URL of the CLI website
        Dim gitUrl2 As String = "https://github.com/ebullient/ttrpg-convert-cli/releases"

        ' Use ProcessStartInfo to open the URL in the default browser
        Try
            Dim processStartInfo As New ProcessStartInfo(gitUrl2)
            processStartInfo.UseShellExecute = True
            Process.Start(processStartInfo)
        Catch ex As Exception
            MessageBox.Show($"Failed to open the website: {ex.Message}", "Error")
        End Try
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
                CLI_Source.Text = folderDialog.SelectedPath
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
                TextBox_Config_File.Text = IO.Path.GetFileName(openFileDialog.FileName)
            End If
        End Using
    End Sub

    Private Sub Button_Data_Folder_Click_1(sender As Object, e As EventArgs) Handles Button_Data_Folder.Click
        ' Ensure CLI_Source is populated
        If String.IsNullOrWhiteSpace(CLI_Source.Text) Then
            MessageBox.Show("Please select or specify a folder in CLI_Source first.", "Warning")
            Return
        End If

        ' Get the CLI_Source path
        Dim basePath = CLI_Source.Text

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
        Dim folderPath = CLI_Source.Text ' Folder path from CLI_Source

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
        Dim downloadUrl As String = "https://github.com/5etools-mirror-3/5etools-src/releases/tag/v1.210.4"

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
        Dim downloadUrl = "https://github.com/5etools-mirror-3/5etools-img/releases/tag/v1.210.4"

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

    Private Sub Label14_Click(sender As Object, e As EventArgs) Handles Label14.Click

    End Sub

    Private Sub Button_DL_Templates_Click(sender As Object, e As EventArgs) Handles Button_DL_Templates.Click
        ' Retrieve the folder path from TextBox_CLIHome
        Dim cliHomePath As String = TextBox_CLIHome.Text

        ' Update TextBox_Template_Location
        TextBox_Template_Location.Text = Path.Combine(cliHomePath, "ttrpg-convert-cli\examples\templates\tools5e")

        ' Proceed with downloading templates (previous code)
        If Not Directory.Exists(cliHomePath) Then
            MessageBox.Show("The specified CLI home folder does not exist. Please select a valid folder.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        Dim gitCommand As String = "git clone --depth 1 https://github.com/ebullient/ttrpg-convert-cli"

        Try
            Dim startInfo As New ProcessStartInfo With {
            .FileName = "cmd.exe",
            .WorkingDirectory = cliHomePath,
            .Arguments = $"/c {gitCommand}",
            .UseShellExecute = False,
            .CreateNoWindow = True
        }

            Using process As Process = Process.Start(startInfo)
                process.WaitForExit()
            End Using

            MessageBox.Show("Templates have been downloaded successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            MessageBox.Show($"An error occurred while running the command: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        UpdateTemplateRootPath()
    End Sub

    Private Sub UpdateMonsterTemplateList()
        ' Clear the ComboBox
        ComboBox_Monster_Template.Items.Clear()

        ' Get the directory path from TextBox_Template_Location
        Dim templateLocation As String = TextBox_Template_Location.Text

        ' Validate the directory exists
        If Not Directory.Exists(templateLocation) Then
            MessageBox.Show("The specified template location does not exist. Please provide a valid path.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        ' Get all .txt files containing 'monster' in the filename
        Dim monsterFiles As String() = Directory.GetFiles(templateLocation, "*.txt").
                                        Where(Function(f) f.ToLower().Contains("monster")).ToArray()

        ' Add files to the ComboBox
        For Each file As String In monsterFiles
            ComboBox_Monster_Template.Items.Add(Path.GetFileName(file))
        Next

        ' Optionally select the first item
        If ComboBox_Monster_Template.Items.Count > 0 Then
            ComboBox_Monster_Template.SelectedIndex = 0
        End If
    End Sub

    Private Sub TextBox_Template_Location_TextChanged(sender As Object, e As EventArgs) Handles TextBox_Template_Location.TextChanged
        ' Save the new location to settings
        My.Settings.TemplateLocation = TextBox_Template_Location.Text
        My.Settings.Save()

        ' Update ComboBoxes
        UpdateMonsterTemplateList()
        UpdateBackgroundTemplateList()
        UpdateItemTemplateList()
        UpdateRaceTemplateList()
        UpdateSpellTemplateList()

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
        My.Settings.Setting_Background = ComboBox_Background_Template.SelectedItem?.ToString()
        My.Settings.Save()

        ' Call the BuildConfigFile method to generate the text for TextBox_ConfigFile_Build
        BuildConfigFile()
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
        My.Settings.Setting_From = TextBox_From.Text?.ToString()
        My.Settings.Save()

        ' Call the BuildConfigFile method to generate the text for TextBox_ConfigFile_Build
        BuildConfigFile()
    End Sub

    Private Sub TextBox_Book_TextChanged(sender As Object, e As EventArgs) Handles TextBox_Book.TextChanged
        ' Store the selected value in settings
        My.Settings.Setting_Book = TextBox_Book.Text?.ToString()
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
        ' Save the current value of TextBox_Compendium_Location to Setting_Compendium
        My.Settings.Setting_Rules = TextBox_Rules_Location.Text
        ' Optionally, save settings immediately
        My.Settings.Save()

        ' Call the BuildConfigFile method to generate the text for TextBox_ConfigFile_Build
        BuildConfigFile()
    End Sub

    Private Sub CheckBox_DiceRoller_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox_DiceRoller.CheckedChanged
        ' Call the BuildConfigFile method to generate the text for TextBox_ConfigFile_Build
        BuildConfigFile()
    End Sub

    Private Sub CheckBox_Img_External_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox_Img_External.CheckedChanged
        ' Call the BuildConfigFile method to generate the text for TextBox_ConfigFile_Build
        BuildConfigFile()
    End Sub

    Private Sub CheckBox_Img_Internal_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox_Img_Internal.CheckedChanged
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

    Private Sub Label19_Click(sender As Object, e As EventArgs) Handles Label19.Click

    End Sub

    Private Sub Button_Image_Folder_Click(sender As Object, e As EventArgs) Handles Button_Image_Folder.Click
        ' Create a new instance of FolderBrowserDialog
        Using folderDialog As New FolderBrowserDialog()
            ' Set the initial directory to the current path of TextBox_ImageFolder, if needed
            folderDialog.SelectedPath = TextBox_ImageFolder.Text

            ' Show the folder dialog and check if the user selected a folder
            If folderDialog.ShowDialog() = DialogResult.OK Then
                ' Set the selected folder path in TextBox_ImageFolder
                TextBox_ImageFolder.Text = folderDialog.SelectedPath

                ' Optionally, you can save the selected folder location to My.Settings if you want to persist it
                My.Settings.Setting_ImageFolder = folderDialog.SelectedPath
                My.Settings.Save()
            End If
        End Using
    End Sub

    Private Sub PictureBox_Info_From_Click(sender As Object, e As EventArgs) Handles PictureBox_Info_From.Click
        ' Show the message box with multiline text
        MessageBox.Show("The FROM section should contain the content that you want just the elements from." & vbCrLf &
                               "For example: 1 note per monster, spell, item, etc." & vbCrLf &
                               "The ADVENTURE section should contain the Adventure content that you want markdown copies of the adventures from." & vbCrLf &
                               "The BOOKS section should contain the Rule Book content that you want markdown copies of." & vbCrLf &
                               "Anything you put in BOOKS or ADVENTURE does not need to go in FROM." & vbCrLf &
                               "ADVENTURES do not need to be put in books. Think of books are rule books and adventures as adventure modules." & vbCrLf & vbCrLf &
                               "Enter the sources as SourceAcronym," & vbCrLf &
                               "The last source does not need a ," & vbCrLf &
                               "Example:" & vbCrLf & vbCrLf &
                               "MM," & vbCrLf &
                               "PHB," & vbCrLf &
                               "DMG", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)

    End Sub

    Private Sub Button_ObsTTRPGTutorials_Click(sender As Object, e As EventArgs) Handles Button_ObsTTRPGTutorials.Click
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

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
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

    Private Sub TextBox1_TextChanged_1(sender As Object, e As EventArgs) Handles TextBox1.TextChanged

    End Sub

    Private Sub Button2_Click_1(sender As Object, e As EventArgs) Handles Button2.Click
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
End Class

