   M S B A M L   `   `   `  ����   LogicCalculator!    LogicalProofTool.MainWindow   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    >d2http://schemas.microsoft.com/expression/blend/2008    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006       � WindowStartupLocation$  CenterScreen=� � 
ResizeMode$ CanMinimize=� � Title$ Logical Proof Tool��$	��820��$	��780��.��]� .+�o� $	��Top=�.@�m� $��_File��.@�m� -   $
��_New�� m� Icon �� $��new.ico��$��16��$��16��m� -   $��_Open�� �� $��open.ico��$��16��$��16��m� -   $��_Save�� �� $��save.ico��$��16��$��16���� m� -   $��_Exit�� �� $��exit.ico��$��16��$��16��m� $��_Help��.@�m� -   $��_User Manual�� �� $��help.ico��$��16��$��16��m� -   $��_About�� �� $��help.ico��$��16��$��16��� -   $��GridMain��$��5q� � ColumnDefinitions �� $	��73* ��� $	��99* ��� $
��476* ��� $
��116* � � RowDefinitions �� $	��550 ��� $��* �.+�� -   $��GridPermanent��$��4�� �� $��* ��� -	   $��mainTab��$��4��.@��� $��Logical Proof��.��� -
   $��GridLogicalProof�� �� $��6* ��� $	��36* ��� $
��204* ��� $
��233* � �� $	��130 ��� $��7* ��� $��50 �.+��� -   $��spLogicExpression��$��Center=�$��4��$��0,5,-0.6,5.4q�.+��� $��Question Title�� �� TextAlignment$ Center=�$
��Bold"��� -   $��tbQTitle�� �� TextAlignment$ Center=�$��200,0q��� $&�� Enter a valid logical expression��$ Center=�$
��Bold"��� -   $��
tbEquation��$ Center=�$
��80,0q�� $��0,5,0,0q� �� $��40 ��� $��35 ��� $	��360 ��� $��95 ��� $��62 ��� $��62 ��� $��62 �.+��� -   $��MasterCheck��$��0,0,6,15q� d� 	IsChecked$ FalseY�$y�Hidden=�$��Center=�$��Center=��� $
��Line��$��Center=�$
��Bold"�$��1���� $��	Statement��$��Center=�$��2��$
��Bold"��� $
��Rule��$��Center=�$��3��$
��Bold"��� $��Center=�$��4��$
��Bold"�.���� $
��Wrap=�$��Center=��� 	 �� FlowDirection$	 RightToLeft=�$	�   ���� $�First Segment���� $��Center=�$��5��$
��Bold"�.���� $
��Wrap=�$��Center=��� $	 RightToLeft=�$� ���� $�Second Segment���� $��Center=�$��6��$
��Bold"�.���� $
��Wrap=�$��Center=��� $	 RightToLeft=�$	�   ���� $�Third Segment���� $��4��$��1��$��1��$
��Auto=�.���� -   $��spGridTable��� -   $��GridButtonsLine��$��2��$��4�� �� $��* ��� $��* ��� $��* ��� $��* ��� $��* ��� $��* ��� $��* �.+��� -   $��
btnAddLine��
 �� 	IsDefault
 . $��New Line��$��5q�$��2��$��0���� -   $��btnClear��
 . $��Clear��$��5q�$��2��{�.  �� -   $��	btnRemove��
 . $��Remove��$��5q�$��3��{�.  �� -   $��btnAddBefore��
 . $	��Add��$��5q�$��4��{�.  �� -   $��btncheckButton��
 . $��Check��$��5q�$��2��$��5���� $��Text Editor��.���� -   $��tbEditor�� �� TextWrapping$
 Wrap=� �� AcceptsReturn . $
��Auto=�$��10q��� $��3��$��2,6,2,2q�$��0q�
���   �$��4��
���   �.��� -   $��gridKeyboard�� �� $��* � �� $��* ��� $��* ��� $��* ��� $��* � � 	Resources  �� 
TargetType �� Property �� Value��%-   '��       �� " ��.���  � ��$ 18(�.+�� -   $��gridGreekChars��$��2��$��2�� �� $��* ��� $��* ��� $��* ��� $��* ��� $��* ��� $��* ��� $��* ��� $��* ��� $��* ��� $��* � �� $��* ��� $��* �.+��� -   $��
btnCapBeta��$��Β��$��Center=�$��2q�$��0��$��0���� -   $��
btnCapGama��$��Γ��$��Center=�$��2q�$��1��$��0���� -   $��btnCapDelta��$��Δ��$��Center=�$��2q�$��2��$��0���� -   $��btnCapEpsilon��$��Ε��$��Center=�$��2q�$��3��$��0���� -   $��
btnCapTeta��$��Θ��$��Center=�$��2q�$��4��$��0���� -   $��	btnCapPai��$��Π��$��Center=�$��2q�$��5��$��0���� -   $��	btnCapPhi��$��Φ��$��Center=�$��2q�$��6��$��0���� -    $��	btnCapChi��$��Χ��$��Center=�$��2q�$��7��$��0���� -!   $��	btnCapPsi��$��Ψ��$��Center=�$��2q�$��8��$��0���� -"   $��btnCapOmega��$��Ω��$��Center=�$��2q�$��9��$��0���� -#   $��btnBeta��$��β��$��Center=�$��2q�$��0��$��1���� -$   $��btnGama��$��γ��$��Center=�$��2q�$��1��$��1���� -%   $��btnDelta��$��δ��$��Center=�$��2q�$��2��$��1���� -&   $��
btnEpsilon��$��ε��$��Center=�$��2q�$��3��$��1���� -'   $��btnTeta��$��θ��$��Center=�$��2q�$��4��$��1���� -(   $��btnPai��$��π��$��Center=�$��2q�$��5��$��1���� -)   $��btnPhi��$��φ��$��Center=�$��2q�$��6��$��1���� -*   $��btnChi��$��χ��$��Center=�$��2q�$��7��$��1���� -+   $��btnPsi��$��ψ��$��Center=�$��2q�$��8��$��1���� -,   $��btnOmega��$��ω��$��Center=�$��2q�$��9��$��1��� $��0�� �� $��* ��� $��* ��� $��* ��� $��* ��� $��* ��� $��* �.+��� --   $��	btnFalsum��$	��⊥��$��Center=�$��2q�$��5��$��2���� -.   $��btnNot��$��2q�$��¬��$��Center=�$��0��$��2���� -/   $��btnOr��$	��∨��$��Center=�$��2q�$��3��$��2���� -0   $��btnTurnstile��$	��⊢��$��Center=�$��2q�$��4��$��2���� -1   $��btnArrow��$	��→��$��Center=�$��2q�$��1��$��2���� -2   $��btnAnd��$	��∧��$��Center=�$��2q�$��2��$��3��� $��1�� �� $��* ��� $��* ��� $��* ��� $��* ��� $��* ��� $��* ��� $��* ��� $��* ��� $��* ��� $��* ��� $��* ��� $��* ��� $��* ��� $��* �.+��� -3   $��	btnExists��$	��∃��$��Center=�$��2q�$��2��$��2���� -4   $��	btnForAll��$	��∀��$��Center=�$��2q�$��2��$��10���� -5   $
��btnX��$	��X_0��$��Center=�$��2q�$��2��$��0���� -6   $
��btnY��$	��Y_0��$��Center=�$��2q�$��2��$��12���� -7   $��btnCreateBox��$��
Create Box��$��Center=�$��2q�$��6��$��4��