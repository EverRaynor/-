[General]
SyntaxVersion=2
BeginHotkey=121
BeginHotkeyMod=0
PauseHotkey=0
PauseHotkeyMod=0
StopHotkey=123
StopHotkeyMod=0
RunOnce=1
EnableWindow=
MacroID=5237f4ba-a286-4a45-aae5-80f11c0d9696
Description=我的脚本1
Enable=1
AutoRun=0
[Repeat]
Type=0
Number=1
[SetupUI]
Type=2
QUI=
[Relative]
SetupOCXFile=
[Comment]

[Script]
Dim TEXT
Dim i
i = 0

Rem flag//标记A点，末尾时返回，造成从这里开始的循环
FindColorEx 400,300,624,468,"B53323",0,0.5,intX,intY
If intX > 0 And intY > 0 Then 
//MsgBox "red"
i = 1
Else i = 0
Call Plugin.Sys.SetCLB(i)
Delay 500
//RunApp "D:\receive.txt"//打开TXT看看
End If


Goto flag
