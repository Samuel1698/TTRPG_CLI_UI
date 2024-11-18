<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form1))
        FolderBrowserDialog1 = New FolderBrowserDialog()
        TabPage_RunCLI = New TabPage()
        Button_Data_Folder = New Button()
        Label5 = New Label()
        TextBox_Data_Folder = New TextBox()
        Button_Config_File = New Button()
        Label4 = New Label()
        TextBox_Config_File = New TextBox()
        Label3 = New Label()
        Label2 = New Label()
        CheckBoxIndex = New CheckBox()
        Label1 = New Label()
        TextBox_OutputFolder = New TextBox()
        Button_Folder_Browse = New Button()
        CLI_Source = New TextBox()
        TextBox_Command = New TextBox()
        Button_Run_CLI = New Button()
        TabPage_Config = New TabPage()
        Label25 = New Label()
        PictureBox_Info_From = New PictureBox()
        Label24 = New Label()
        Label23 = New Label()
        Label22 = New Label()
        TextBox_Config_Name = New TextBox()
        Button_Build_Json = New Button()
        Label18 = New Label()
        TextBox_Img_Folder = New TextBox()
        Label17 = New Label()
        TextBox_tagPrefix = New TextBox()
        CheckBox_Img_Internal = New CheckBox()
        CheckBox_Img_External = New CheckBox()
        CheckBox_DiceRoller = New CheckBox()
        TextBox_Book = New TextBox()
        TextBox_Adventure = New TextBox()
        Label16 = New Label()
        Label15 = New Label()
        ComboBox_Template_Race = New ComboBox()
        ComboBox_Background_Template = New ComboBox()
        ComboBox_Spell_Template = New ComboBox()
        ComboBox_Item_Template = New ComboBox()
        ComboBox_Monster_Template = New ComboBox()
        Label13 = New Label()
        Label12 = New Label()
        Label11 = New Label()
        Label10 = New Label()
        Label9 = New Label()
        TextBox_Rules_Location = New TextBox()
        TextBox_Compendium_Location = New TextBox()
        TextBox_From = New TextBox()
        TextBox_ConfigFile_Build = New TextBox()
        TabPage_Setup = New TabPage()
        TextBox1 = New TextBox()
        Label21 = New Label()
        Label20 = New Label()
        Button_Image_Folder = New Button()
        TextBox_ImageFolder = New TextBox()
        Label19 = New Label()
        Button_DL_Templates = New Button()
        TextBox_Template_Location = New TextBox()
        Label14 = New Label()
        Button4 = New Button()
        Button3 = New Button()
        Button_Download_SourceData = New Button()
        TextBox_CLIHome = New TextBox()
        Label8 = New Label()
        Label7 = New Label()
        Button_SelectCLIHome = New Button()
        Button_InstallGit = New Button()
        TabControl1 = New TabControl()
        TabPage_Help = New TabPage()
        Button2 = New Button()
        Button1 = New Button()
        Button_ObsTTRPGTutorials = New Button()
        Label6 = New Label()
        TabPage_RunCLI.SuspendLayout()
        TabPage_Config.SuspendLayout()
        CType(PictureBox_Info_From, ComponentModel.ISupportInitialize).BeginInit()
        TabPage_Setup.SuspendLayout()
        TabControl1.SuspendLayout()
        TabPage_Help.SuspendLayout()
        SuspendLayout()
        ' 
        ' TabPage_RunCLI
        ' 
        TabPage_RunCLI.Controls.Add(Button_Data_Folder)
        TabPage_RunCLI.Controls.Add(Label5)
        TabPage_RunCLI.Controls.Add(TextBox_Data_Folder)
        TabPage_RunCLI.Controls.Add(Button_Config_File)
        TabPage_RunCLI.Controls.Add(Label4)
        TabPage_RunCLI.Controls.Add(TextBox_Config_File)
        TabPage_RunCLI.Controls.Add(Label3)
        TabPage_RunCLI.Controls.Add(Label2)
        TabPage_RunCLI.Controls.Add(CheckBoxIndex)
        TabPage_RunCLI.Controls.Add(Label1)
        TabPage_RunCLI.Controls.Add(TextBox_OutputFolder)
        TabPage_RunCLI.Controls.Add(Button_Folder_Browse)
        TabPage_RunCLI.Controls.Add(CLI_Source)
        TabPage_RunCLI.Controls.Add(TextBox_Command)
        TabPage_RunCLI.Controls.Add(Button_Run_CLI)
        TabPage_RunCLI.Location = New Point(4, 24)
        TabPage_RunCLI.Name = "TabPage_RunCLI"
        TabPage_RunCLI.Padding = New Padding(3)
        TabPage_RunCLI.Size = New Size(967, 548)
        TabPage_RunCLI.TabIndex = 2
        TabPage_RunCLI.Text = "Run CLI"
        TabPage_RunCLI.UseVisualStyleBackColor = True
        ' 
        ' Button_Data_Folder
        ' 
        Button_Data_Folder.Location = New Point(672, 126)
        Button_Data_Folder.Name = "Button_Data_Folder"
        Button_Data_Folder.Size = New Size(75, 23)
        Button_Data_Folder.TabIndex = 29
        Button_Data_Folder.Text = "Browse"
        Button_Data_Folder.UseVisualStyleBackColor = True
        ' 
        ' Label5
        ' 
        Label5.AutoSize = True
        Label5.Location = New Point(16, 129)
        Label5.Name = "Label5"
        Label5.Size = New Size(67, 15)
        Label5.TabIndex = 28
        Label5.Text = "Data Folder"
        ' 
        ' TextBox_Data_Folder
        ' 
        TextBox_Data_Folder.Location = New Point(103, 126)
        TextBox_Data_Folder.Name = "TextBox_Data_Folder"
        TextBox_Data_Folder.PlaceholderText = "Select Data Folder"
        TextBox_Data_Folder.Size = New Size(563, 23)
        TextBox_Data_Folder.TabIndex = 27
        ' 
        ' Button_Config_File
        ' 
        Button_Config_File.Location = New Point(672, 97)
        Button_Config_File.Name = "Button_Config_File"
        Button_Config_File.Size = New Size(75, 23)
        Button_Config_File.TabIndex = 26
        Button_Config_File.Text = "Browse"
        Button_Config_File.UseVisualStyleBackColor = True
        ' 
        ' Label4
        ' 
        Label4.AutoSize = True
        Label4.Location = New Point(16, 100)
        Label4.Name = "Label4"
        Label4.Size = New Size(64, 15)
        Label4.TabIndex = 25
        Label4.Text = "Config File"
        ' 
        ' TextBox_Config_File
        ' 
        TextBox_Config_File.Location = New Point(103, 97)
        TextBox_Config_File.Name = "TextBox_Config_File"
        TextBox_Config_File.PlaceholderText = "Select your Config file (*.json)"
        TextBox_Config_File.Size = New Size(563, 23)
        TextBox_Config_File.TabIndex = 24
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.Location = New Point(15, 158)
        Label3.Name = "Label3"
        Label3.Size = New Size(84, 15)
        Label3.TabIndex = 23
        Label3.Text = "CLI Command"
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Location = New Point(16, 17)
        Label2.Name = "Label2"
        Label2.Size = New Size(82, 15)
        Label2.TabIndex = 22
        Label2.Text = "CLI BIN Folder"
        ' 
        ' CheckBoxIndex
        ' 
        CheckBoxIndex.AutoSize = True
        CheckBoxIndex.Location = New Point(103, 43)
        CheckBoxIndex.Name = "CheckBoxIndex"
        CheckBoxIndex.Size = New Size(115, 19)
        CheckBoxIndex.TabIndex = 21
        CheckBoxIndex.Text = "Include --index ?"
        CheckBoxIndex.UseVisualStyleBackColor = True
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Location = New Point(16, 71)
        Label1.Name = "Label1"
        Label1.Size = New Size(81, 15)
        Label1.TabIndex = 20
        Label1.Text = "Output Folder"
        ' 
        ' TextBox_OutputFolder
        ' 
        TextBox_OutputFolder.Location = New Point(103, 68)
        TextBox_OutputFolder.Name = "TextBox_OutputFolder"
        TextBox_OutputFolder.Size = New Size(563, 23)
        TextBox_OutputFolder.TabIndex = 19
        TextBox_OutputFolder.Text = "Output"
        ' 
        ' Button_Folder_Browse
        ' 
        Button_Folder_Browse.Location = New Point(672, 14)
        Button_Folder_Browse.Name = "Button_Folder_Browse"
        Button_Folder_Browse.Size = New Size(75, 23)
        Button_Folder_Browse.TabIndex = 18
        Button_Folder_Browse.Text = "Browse"
        Button_Folder_Browse.UseVisualStyleBackColor = True
        ' 
        ' CLI_Source
        ' 
        CLI_Source.Location = New Point(103, 14)
        CLI_Source.Name = "CLI_Source"
        CLI_Source.PlaceholderText = "C:\CLI\5eTools\bin"
        CLI_Source.Size = New Size(563, 23)
        CLI_Source.TabIndex = 17
        CLI_Source.Text = "C:\CLI\5eTools\bin"
        ' 
        ' TextBox_Command
        ' 
        TextBox_Command.Location = New Point(103, 155)
        TextBox_Command.Name = "TextBox_Command"
        TextBox_Command.Size = New Size(563, 23)
        TextBox_Command.TabIndex = 16
        TextBox_Command.Text = "ttrpg-convert.exe --help"
        ' 
        ' Button_Run_CLI
        ' 
        Button_Run_CLI.Location = New Point(672, 155)
        Button_Run_CLI.Name = "Button_Run_CLI"
        Button_Run_CLI.Size = New Size(75, 23)
        Button_Run_CLI.TabIndex = 15
        Button_Run_CLI.Text = "Run CLI"
        Button_Run_CLI.UseVisualStyleBackColor = True
        ' 
        ' TabPage_Config
        ' 
        TabPage_Config.Controls.Add(Label25)
        TabPage_Config.Controls.Add(PictureBox_Info_From)
        TabPage_Config.Controls.Add(Label24)
        TabPage_Config.Controls.Add(Label23)
        TabPage_Config.Controls.Add(Label22)
        TabPage_Config.Controls.Add(TextBox_Config_Name)
        TabPage_Config.Controls.Add(Button_Build_Json)
        TabPage_Config.Controls.Add(Label18)
        TabPage_Config.Controls.Add(TextBox_Img_Folder)
        TabPage_Config.Controls.Add(Label17)
        TabPage_Config.Controls.Add(TextBox_tagPrefix)
        TabPage_Config.Controls.Add(CheckBox_Img_Internal)
        TabPage_Config.Controls.Add(CheckBox_Img_External)
        TabPage_Config.Controls.Add(CheckBox_DiceRoller)
        TabPage_Config.Controls.Add(TextBox_Book)
        TabPage_Config.Controls.Add(TextBox_Adventure)
        TabPage_Config.Controls.Add(Label16)
        TabPage_Config.Controls.Add(Label15)
        TabPage_Config.Controls.Add(ComboBox_Template_Race)
        TabPage_Config.Controls.Add(ComboBox_Background_Template)
        TabPage_Config.Controls.Add(ComboBox_Spell_Template)
        TabPage_Config.Controls.Add(ComboBox_Item_Template)
        TabPage_Config.Controls.Add(ComboBox_Monster_Template)
        TabPage_Config.Controls.Add(Label13)
        TabPage_Config.Controls.Add(Label12)
        TabPage_Config.Controls.Add(Label11)
        TabPage_Config.Controls.Add(Label10)
        TabPage_Config.Controls.Add(Label9)
        TabPage_Config.Controls.Add(TextBox_Rules_Location)
        TabPage_Config.Controls.Add(TextBox_Compendium_Location)
        TabPage_Config.Controls.Add(TextBox_From)
        TabPage_Config.Controls.Add(TextBox_ConfigFile_Build)
        TabPage_Config.Location = New Point(4, 24)
        TabPage_Config.Name = "TabPage_Config"
        TabPage_Config.Padding = New Padding(3)
        TabPage_Config.Size = New Size(967, 548)
        TabPage_Config.TabIndex = 1
        TabPage_Config.Text = "Create Config File"
        TabPage_Config.UseVisualStyleBackColor = True
        ' 
        ' Label25
        ' 
        Label25.AutoSize = True
        Label25.Location = New Point(249, 491)
        Label25.Name = "Label25"
        Label25.Size = New Size(99, 15)
        Label25.TabIndex = 47
        Label25.Text = "Config File Name"
        ' 
        ' PictureBox_Info_From
        ' 
        PictureBox_Info_From.Image = My.Resources.Resources.Information
        PictureBox_Info_From.InitialImage = CType(resources.GetObject("PictureBox_Info_From.InitialImage"), Image)
        PictureBox_Info_From.Location = New Point(8, 3)
        PictureBox_Info_From.Name = "PictureBox_Info_From"
        PictureBox_Info_From.Size = New Size(32, 21)
        PictureBox_Info_From.SizeMode = PictureBoxSizeMode.Zoom
        PictureBox_Info_From.TabIndex = 46
        PictureBox_Info_From.TabStop = False
        ' 
        ' Label24
        ' 
        Label24.AutoSize = True
        Label24.Location = New Point(644, 6)
        Label24.Name = "Label24"
        Label24.Size = New Size(39, 15)
        Label24.TabIndex = 45
        Label24.Text = "Books"
        ' 
        ' Label23
        ' 
        Label23.AutoSize = True
        Label23.Location = New Point(324, 6)
        Label23.Name = "Label23"
        Label23.Size = New Size(67, 15)
        Label23.TabIndex = 44
        Label23.Text = "Adventures"
        ' 
        ' Label22
        ' 
        Label22.AutoSize = True
        Label22.Location = New Point(46, 6)
        Label22.Name = "Label22"
        Label22.Size = New Size(35, 15)
        Label22.TabIndex = 43
        Label22.Text = "From"
        ' 
        ' TextBox_Config_Name
        ' 
        TextBox_Config_Name.Location = New Point(8, 488)
        TextBox_Config_Name.Name = "TextBox_Config_Name"
        TextBox_Config_Name.Size = New Size(235, 23)
        TextBox_Config_Name.TabIndex = 42
        TextBox_Config_Name.Text = "My.Config.File.json"
        ' 
        ' Button_Build_Json
        ' 
        Button_Build_Json.Location = New Point(8, 517)
        Button_Build_Json.Name = "Button_Build_Json"
        Button_Build_Json.Size = New Size(93, 23)
        Button_Build_Json.TabIndex = 41
        Button_Build_Json.Text = "Save Config"
        Button_Build_Json.UseVisualStyleBackColor = True
        ' 
        ' Label18
        ' 
        Label18.AutoSize = True
        Label18.Location = New Point(249, 462)
        Label18.Name = "Label18"
        Label18.Size = New Size(99, 15)
        Label18.TabIndex = 40
        Label18.Text = "Img Folder Name"
        ' 
        ' TextBox_Img_Folder
        ' 
        TextBox_Img_Folder.Location = New Point(8, 459)
        TextBox_Img_Folder.Name = "TextBox_Img_Folder"
        TextBox_Img_Folder.Size = New Size(235, 23)
        TextBox_Img_Folder.TabIndex = 39
        TextBox_Img_Folder.Text = "5etools-img"
        ' 
        ' Label17
        ' 
        Label17.AutoSize = True
        Label17.Location = New Point(249, 433)
        Label17.Name = "Label17"
        Label17.Size = New Size(58, 15)
        Label17.TabIndex = 38
        Label17.Text = "Tag Prefix"
        ' 
        ' TextBox_tagPrefix
        ' 
        TextBox_tagPrefix.Location = New Point(8, 430)
        TextBox_tagPrefix.Name = "TextBox_tagPrefix"
        TextBox_tagPrefix.Size = New Size(235, 23)
        TextBox_tagPrefix.TabIndex = 37
        TextBox_tagPrefix.Text = "ttrpg-cli"
        ' 
        ' CheckBox_Img_Internal
        ' 
        CheckBox_Img_Internal.AutoSize = True
        CheckBox_Img_Internal.Location = New Point(8, 405)
        CheckBox_Img_Internal.Name = "CheckBox_Img_Internal"
        CheckBox_Img_Internal.Size = New Size(138, 19)
        CheckBox_Img_Internal.TabIndex = 36
        CheckBox_Img_Internal.Text = "Copy Internal Images"
        CheckBox_Img_Internal.UseVisualStyleBackColor = True
        ' 
        ' CheckBox_Img_External
        ' 
        CheckBox_Img_External.AutoSize = True
        CheckBox_Img_External.Location = New Point(8, 380)
        CheckBox_Img_External.Name = "CheckBox_Img_External"
        CheckBox_Img_External.Size = New Size(140, 19)
        CheckBox_Img_External.TabIndex = 35
        CheckBox_Img_External.Text = "Copy External Images"
        CheckBox_Img_External.UseVisualStyleBackColor = True
        ' 
        ' CheckBox_DiceRoller
        ' 
        CheckBox_DiceRoller.AutoSize = True
        CheckBox_DiceRoller.Location = New Point(8, 355)
        CheckBox_DiceRoller.Name = "CheckBox_DiceRoller"
        CheckBox_DiceRoller.Size = New Size(104, 19)
        CheckBox_DiceRoller.TabIndex = 34
        CheckBox_DiceRoller.Text = "Use Dice Roller"
        CheckBox_DiceRoller.UseVisualStyleBackColor = True
        ' 
        ' TextBox_Book
        ' 
        TextBox_Book.Location = New Point(644, 24)
        TextBox_Book.Multiline = True
        TextBox_Book.Name = "TextBox_Book"
        TextBox_Book.ScrollBars = ScrollBars.Vertical
        TextBox_Book.Size = New Size(314, 116)
        TextBox_Book.TabIndex = 33
        ' 
        ' TextBox_Adventure
        ' 
        TextBox_Adventure.Location = New Point(324, 24)
        TextBox_Adventure.Multiline = True
        TextBox_Adventure.Name = "TextBox_Adventure"
        TextBox_Adventure.ScrollBars = ScrollBars.Vertical
        TextBox_Adventure.Size = New Size(314, 116)
        TextBox_Adventure.TabIndex = 32
        ' 
        ' Label16
        ' 
        Label16.AutoSize = True
        Label16.Location = New Point(251, 178)
        Label16.Name = "Label16"
        Label16.Size = New Size(35, 15)
        Label16.TabIndex = 31
        Label16.Text = "Rules"
        ' 
        ' Label15
        ' 
        Label15.AutoSize = True
        Label15.Location = New Point(249, 149)
        Label15.Name = "Label15"
        Label15.Size = New Size(81, 15)
        Label15.TabIndex = 30
        Label15.Text = "Compendium"
        ' 
        ' ComboBox_Template_Race
        ' 
        ComboBox_Template_Race.FormattingEnabled = True
        ComboBox_Template_Race.Location = New Point(8, 297)
        ComboBox_Template_Race.Name = "ComboBox_Template_Race"
        ComboBox_Template_Race.Size = New Size(235, 23)
        ComboBox_Template_Race.TabIndex = 29
        ' 
        ' ComboBox_Background_Template
        ' 
        ComboBox_Background_Template.FormattingEnabled = True
        ComboBox_Background_Template.Location = New Point(8, 208)
        ComboBox_Background_Template.Name = "ComboBox_Background_Template"
        ComboBox_Background_Template.Size = New Size(235, 23)
        ComboBox_Background_Template.TabIndex = 28
        ' 
        ' ComboBox_Spell_Template
        ' 
        ComboBox_Spell_Template.FormattingEnabled = True
        ComboBox_Spell_Template.Location = New Point(8, 326)
        ComboBox_Spell_Template.Name = "ComboBox_Spell_Template"
        ComboBox_Spell_Template.Size = New Size(235, 23)
        ComboBox_Spell_Template.TabIndex = 27
        ' 
        ' ComboBox_Item_Template
        ' 
        ComboBox_Item_Template.FormattingEnabled = True
        ComboBox_Item_Template.Location = New Point(8, 268)
        ComboBox_Item_Template.Name = "ComboBox_Item_Template"
        ComboBox_Item_Template.Size = New Size(235, 23)
        ComboBox_Item_Template.TabIndex = 25
        ' 
        ' ComboBox_Monster_Template
        ' 
        ComboBox_Monster_Template.FormattingEnabled = True
        ComboBox_Monster_Template.Location = New Point(8, 237)
        ComboBox_Monster_Template.Name = "ComboBox_Monster_Template"
        ComboBox_Monster_Template.Size = New Size(235, 23)
        ComboBox_Monster_Template.TabIndex = 24
        ' 
        ' Label13
        ' 
        Label13.AutoSize = True
        Label13.Location = New Point(251, 331)
        Label13.Name = "Label13"
        Label13.Size = New Size(32, 15)
        Label13.TabIndex = 15
        Label13.Text = "Spell"
        ' 
        ' Label12
        ' 
        Label12.AutoSize = True
        Label12.Location = New Point(251, 301)
        Label12.Name = "Label12"
        Label12.Size = New Size(32, 15)
        Label12.TabIndex = 14
        Label12.Text = "Race"
        ' 
        ' Label11
        ' 
        Label11.AutoSize = True
        Label11.Location = New Point(251, 272)
        Label11.Name = "Label11"
        Label11.Size = New Size(31, 15)
        Label11.TabIndex = 13
        Label11.Text = "Item"
        ' 
        ' Label10
        ' 
        Label10.AutoSize = True
        Label10.Location = New Point(249, 244)
        Label10.Name = "Label10"
        Label10.Size = New Size(51, 15)
        Label10.TabIndex = 12
        Label10.Text = "Monster"
        ' 
        ' Label9
        ' 
        Label9.AutoSize = True
        Label9.Location = New Point(249, 211)
        Label9.Name = "Label9"
        Label9.Size = New Size(71, 15)
        Label9.TabIndex = 11
        Label9.Text = "Background"
        ' 
        ' TextBox_Rules_Location
        ' 
        TextBox_Rules_Location.Location = New Point(8, 175)
        TextBox_Rules_Location.Name = "TextBox_Rules_Location"
        TextBox_Rules_Location.Size = New Size(235, 23)
        TextBox_Rules_Location.TabIndex = 3
        TextBox_Rules_Location.Text = "/3-Mechanics/CLI"
        ' 
        ' TextBox_Compendium_Location
        ' 
        TextBox_Compendium_Location.Location = New Point(8, 146)
        TextBox_Compendium_Location.Name = "TextBox_Compendium_Location"
        TextBox_Compendium_Location.Size = New Size(235, 23)
        TextBox_Compendium_Location.TabIndex = 2
        TextBox_Compendium_Location.Text = "/3-Mechanics/CLI"
        ' 
        ' TextBox_From
        ' 
        TextBox_From.Location = New Point(8, 24)
        TextBox_From.Multiline = True
        TextBox_From.Name = "TextBox_From"
        TextBox_From.ScrollBars = ScrollBars.Vertical
        TextBox_From.Size = New Size(310, 116)
        TextBox_From.TabIndex = 1
        ' 
        ' TextBox_ConfigFile_Build
        ' 
        TextBox_ConfigFile_Build.Location = New Point(354, 146)
        TextBox_ConfigFile_Build.Multiline = True
        TextBox_ConfigFile_Build.Name = "TextBox_ConfigFile_Build"
        TextBox_ConfigFile_Build.ScrollBars = ScrollBars.Vertical
        TextBox_ConfigFile_Build.Size = New Size(604, 394)
        TextBox_ConfigFile_Build.TabIndex = 0
        TextBox_ConfigFile_Build.Text = resources.GetString("TextBox_ConfigFile_Build.Text")
        ' 
        ' TabPage_Setup
        ' 
        TabPage_Setup.Controls.Add(TextBox1)
        TabPage_Setup.Controls.Add(Label21)
        TabPage_Setup.Controls.Add(Label20)
        TabPage_Setup.Controls.Add(Button_Image_Folder)
        TabPage_Setup.Controls.Add(TextBox_ImageFolder)
        TabPage_Setup.Controls.Add(Label19)
        TabPage_Setup.Controls.Add(Button_DL_Templates)
        TabPage_Setup.Controls.Add(TextBox_Template_Location)
        TabPage_Setup.Controls.Add(Label14)
        TabPage_Setup.Controls.Add(Button4)
        TabPage_Setup.Controls.Add(Button3)
        TabPage_Setup.Controls.Add(Button_Download_SourceData)
        TabPage_Setup.Controls.Add(TextBox_CLIHome)
        TabPage_Setup.Controls.Add(Label8)
        TabPage_Setup.Controls.Add(Label7)
        TabPage_Setup.Controls.Add(Button_SelectCLIHome)
        TabPage_Setup.Controls.Add(Button_InstallGit)
        TabPage_Setup.Location = New Point(4, 24)
        TabPage_Setup.Name = "TabPage_Setup"
        TabPage_Setup.Padding = New Padding(3)
        TabPage_Setup.Size = New Size(967, 548)
        TabPage_Setup.TabIndex = 0
        TabPage_Setup.Text = "Setup Data"
        TabPage_Setup.UseVisualStyleBackColor = True
        ' 
        ' TextBox1
        ' 
        TextBox1.Location = New Point(10, 242)
        TextBox1.Multiline = True
        TextBox1.Name = "TextBox1"
        TextBox1.ReadOnly = True
        TextBox1.Size = New Size(718, 117)
        TextBox1.TabIndex = 18
        TextBox1.Text = resources.GetString("TextBox1.Text")
        ' 
        ' Label21
        ' 
        Label21.AutoSize = True
        Label21.Location = New Point(149, 188)
        Label21.Name = "Label21"
        Label21.Size = New Size(210, 15)
        Label21.TabIndex = 16
        Label21.Text = "Extract the images to the folder above."
        ' 
        ' Label20
        ' 
        Label20.AutoSize = True
        Label20.Location = New Point(149, 127)
        Label20.Name = "Label20"
        Label20.Size = New Size(233, 15)
        Label20.TabIndex = 15
        Label20.Text = "Extract the source data to the folder above."
        ' 
        ' Button_Image_Folder
        ' 
        Button_Image_Folder.Location = New Point(10, 152)
        Button_Image_Folder.Name = "Button_Image_Folder"
        Button_Image_Folder.Size = New Size(133, 23)
        Button_Image_Folder.TabIndex = 14
        Button_Image_Folder.Text = "Select Image Folder"
        Button_Image_Folder.UseVisualStyleBackColor = True
        ' 
        ' TextBox_ImageFolder
        ' 
        TextBox_ImageFolder.Location = New Point(390, 152)
        TextBox_ImageFolder.Name = "TextBox_ImageFolder"
        TextBox_ImageFolder.Size = New Size(338, 23)
        TextBox_ImageFolder.TabIndex = 13
        ' 
        ' Label19
        ' 
        Label19.AutoSize = True
        Label19.Location = New Point(149, 156)
        Label19.Name = "Label19"
        Label19.Size = New Size(221, 15)
        Label19.TabIndex = 12
        Label19.Text = "Select where the Images will be installed."
        ' 
        ' Button_DL_Templates
        ' 
        Button_DL_Templates.Location = New Point(10, 213)
        Button_DL_Templates.Name = "Button_DL_Templates"
        Button_DL_Templates.Size = New Size(133, 23)
        Button_DL_Templates.TabIndex = 11
        Button_DL_Templates.Text = "Download Templates"
        Button_DL_Templates.UseVisualStyleBackColor = True
        ' 
        ' TextBox_Template_Location
        ' 
        TextBox_Template_Location.Location = New Point(390, 213)
        TextBox_Template_Location.Name = "TextBox_Template_Location"
        TextBox_Template_Location.Size = New Size(338, 23)
        TextBox_Template_Location.TabIndex = 10
        ' 
        ' Label14
        ' 
        Label14.AutoSize = True
        Label14.Location = New Point(149, 217)
        Label14.Name = "Label14"
        Label14.Size = New Size(151, 15)
        Label14.TabIndex = 9
        Label14.Text = "Templates auto installed to:"
        ' 
        ' Button4
        ' 
        Button4.Location = New Point(10, 65)
        Button4.Name = "Button4"
        Button4.Size = New Size(133, 23)
        Button4.TabIndex = 7
        Button4.Text = "Download CLI"
        Button4.UseVisualStyleBackColor = True
        ' 
        ' Button3
        ' 
        Button3.Location = New Point(10, 184)
        Button3.Name = "Button3"
        Button3.Size = New Size(133, 23)
        Button3.TabIndex = 6
        Button3.Text = "Download Images"
        Button3.UseVisualStyleBackColor = True
        ' 
        ' Button_Download_SourceData
        ' 
        Button_Download_SourceData.Location = New Point(10, 123)
        Button_Download_SourceData.Name = "Button_Download_SourceData"
        Button_Download_SourceData.Size = New Size(133, 23)
        Button_Download_SourceData.TabIndex = 5
        Button_Download_SourceData.Text = "Download Source Data"
        Button_Download_SourceData.UseVisualStyleBackColor = True
        ' 
        ' TextBox_CLIHome
        ' 
        TextBox_CLIHome.Location = New Point(390, 95)
        TextBox_CLIHome.Name = "TextBox_CLIHome"
        TextBox_CLIHome.Size = New Size(338, 23)
        TextBox_CLIHome.TabIndex = 4
        ' 
        ' Label8
        ' 
        Label8.AutoSize = True
        Label8.Location = New Point(149, 40)
        Label8.Name = "Label8"
        Label8.Size = New Size(353, 15)
        Label8.TabIndex = 3
        Label8.Text = "This is a pre-req that lets Github be controlled via Command Line."
        ' 
        ' Label7
        ' 
        Label7.AutoSize = True
        Label7.Location = New Point(149, 98)
        Label7.Name = "Label7"
        Label7.Size = New Size(200, 15)
        Label7.TabIndex = 2
        Label7.Text = "Select where the CLI will be installed."
        ' 
        ' Button_SelectCLIHome
        ' 
        Button_SelectCLIHome.Location = New Point(10, 94)
        Button_SelectCLIHome.Name = "Button_SelectCLIHome"
        Button_SelectCLIHome.Size = New Size(133, 23)
        Button_SelectCLIHome.TabIndex = 1
        Button_SelectCLIHome.Text = "Select Data Folder"
        Button_SelectCLIHome.UseVisualStyleBackColor = True
        ' 
        ' Button_InstallGit
        ' 
        Button_InstallGit.Location = New Point(10, 36)
        Button_InstallGit.Name = "Button_InstallGit"
        Button_InstallGit.Size = New Size(133, 23)
        Button_InstallGit.TabIndex = 0
        Button_InstallGit.Text = "Install Git"
        Button_InstallGit.UseVisualStyleBackColor = True
        ' 
        ' TabControl1
        ' 
        TabControl1.Controls.Add(TabPage_Setup)
        TabControl1.Controls.Add(TabPage_Config)
        TabControl1.Controls.Add(TabPage_RunCLI)
        TabControl1.Controls.Add(TabPage_Help)
        TabControl1.Location = New Point(12, 12)
        TabControl1.Name = "TabControl1"
        TabControl1.SelectedIndex = 0
        TabControl1.Size = New Size(975, 576)
        TabControl1.TabIndex = 15
        ' 
        ' TabPage_Help
        ' 
        TabPage_Help.Controls.Add(Label6)
        TabPage_Help.Controls.Add(Button2)
        TabPage_Help.Controls.Add(Button1)
        TabPage_Help.Controls.Add(Button_ObsTTRPGTutorials)
        TabPage_Help.Location = New Point(4, 24)
        TabPage_Help.Name = "TabPage_Help"
        TabPage_Help.Size = New Size(967, 548)
        TabPage_Help.TabIndex = 3
        TabPage_Help.Text = "Help"
        TabPage_Help.UseVisualStyleBackColor = True
        ' 
        ' Button2
        ' 
        Button2.Location = New Point(204, 153)
        Button2.Name = "Button2"
        Button2.Size = New Size(213, 23)
        Button2.TabIndex = 3
        Button2.Text = "Thank Ebulient for her HARD WORK!"
        Button2.UseVisualStyleBackColor = True
        ' 
        ' Button1
        ' 
        Button1.Location = New Point(204, 124)
        Button1.Name = "Button1"
        Button1.Size = New Size(213, 23)
        Button1.TabIndex = 2
        Button1.Text = "Official TTRPC CLI Documentation"
        Button1.UseVisualStyleBackColor = True
        ' 
        ' Button_ObsTTRPGTutorials
        ' 
        Button_ObsTTRPGTutorials.Location = New Point(204, 95)
        Button_ObsTTRPGTutorials.Name = "Button_ObsTTRPGTutorials"
        Button_ObsTTRPGTutorials.Size = New Size(213, 23)
        Button_ObsTTRPGTutorials.TabIndex = 1
        Button_ObsTTRPGTutorials.Text = "Obsidian TTRPG Tutorials"
        Button_ObsTTRPGTutorials.UseVisualStyleBackColor = True
        ' 
        ' Label6
        ' 
        Label6.AutoSize = True
        Label6.Location = New Point(251, 65)
        Label6.Name = "Label6"
        Label6.Size = New Size(115, 15)
        Label6.TabIndex = 4
        Label6.Text = "WORK IN PROGRESS"
        ' 
        ' Form1
        ' 
        AutoScaleDimensions = New SizeF(7.0F, 15.0F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(990, 591)
        Controls.Add(TabControl1)
        MaximizeBox = False
        Name = "Form1"
        Text = "TTRPG CLI UI"
        TabPage_RunCLI.ResumeLayout(False)
        TabPage_RunCLI.PerformLayout()
        TabPage_Config.ResumeLayout(False)
        TabPage_Config.PerformLayout()
        CType(PictureBox_Info_From, ComponentModel.ISupportInitialize).EndInit()
        TabPage_Setup.ResumeLayout(False)
        TabPage_Setup.PerformLayout()
        TabControl1.ResumeLayout(False)
        TabPage_Help.ResumeLayout(False)
        TabPage_Help.PerformLayout()
        ResumeLayout(False)
    End Sub
    Friend WithEvents FolderBrowserDialog1 As FolderBrowserDialog
    Friend WithEvents TabPage_RunCLI As TabPage
    Friend WithEvents TabPage_Config As TabPage
    Friend WithEvents TabPage_Setup As TabPage
    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents Button_Data_Folder As Button
    Friend WithEvents Label5 As Label
    Friend WithEvents TextBox_Data_Folder As TextBox
    Friend WithEvents Button_Config_File As Button
    Friend WithEvents Label4 As Label
    Friend WithEvents TextBox_Config_File As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents CheckBoxIndex As CheckBox
    Friend WithEvents Label1 As Label
    Friend WithEvents TextBox_OutputFolder As TextBox
    Friend WithEvents Button_Folder_Browse As Button
    Friend WithEvents CLI_Source As TextBox
    Friend WithEvents TextBox_Command As TextBox
    Friend WithEvents Button_Run_CLI As Button
    Friend WithEvents TabPage_Help As TabPage
    Friend WithEvents Button_InstallGit As Button
    Friend WithEvents Button_SelectCLIHome As Button
    Friend WithEvents TextBox_CLIHome As TextBox
    Friend WithEvents Label8 As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents Button3 As Button
    Friend WithEvents Button_Download_SourceData As Button
    Friend WithEvents Button4 As Button
    Friend WithEvents TextBox_ConfigFile_Build As TextBox
    Friend WithEvents ComboBox_MonsterTemplate As ComboBox
    Friend WithEvents ComboBox_BackgroundTemplate As ComboBox
    Friend WithEvents TextBox_Rules_Location As TextBox
    Friend WithEvents TextBox_Compendium_Location As TextBox
    Friend WithEvents TextBox_From As TextBox
    Friend WithEvents Label10 As Label
    Friend WithEvents Label9 As Label
    Friend WithEvents Button_ObsTTRPGTutorials As Button
    Friend WithEvents ComboBox_Spell_Template As ComboBox
    Friend WithEvents ComboBox_Items_Template As ComboBox
    Friend WithEvents Label12 As Label
    Friend WithEvents Label11 As Label
    Friend WithEvents Label13 As Label
    Friend WithEvents TextBox_Template_Location As TextBox
    Friend WithEvents Label14 As Label
    Friend WithEvents Button_DL_Templates As Button
    Friend WithEvents ComboBox_Monster_Template As ComboBox
    Friend WithEvents ComboBox_Item_Template As ComboBox
    Friend WithEvents ComboBox_Background_Template As ComboBox
    Friend WithEvents ComboBox_Template_Race As ComboBox
    Friend WithEvents Label15 As Label
    Friend WithEvents Label16 As Label
    Friend WithEvents TextBox_Book As TextBox
    Friend WithEvents TextBox_Adventure As TextBox
    Friend WithEvents CheckBox_Img_External As CheckBox
    Friend WithEvents CheckBox_DiceRoller As CheckBox
    Friend WithEvents Label18 As Label
    Friend WithEvents TextBox_Img_Folder As TextBox
    Friend WithEvents Label17 As Label
    Friend WithEvents TextBox_tagPrefix As TextBox
    Friend WithEvents CheckBox_Img_Internal As CheckBox
    Friend WithEvents Button_Build_Json As Button
    Friend WithEvents TextBox_Config_Name As TextBox
    Friend WithEvents Label19 As Label
    Friend WithEvents TextBox_ImageFolder As TextBox
    Friend WithEvents Button_Image_Folder As Button
    Friend WithEvents Label20 As Label
    Friend WithEvents Label21 As Label
    Friend WithEvents Label22 As Label
    Friend WithEvents Label24 As Label
    Friend WithEvents Label23 As Label
    Friend WithEvents PictureBox_Info_From As PictureBox
    Friend WithEvents Label25 As Label
    Friend WithEvents Button1 As Button
    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents Button2 As Button
    Friend WithEvents Label6 As Label

End Class
