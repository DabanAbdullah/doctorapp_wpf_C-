   M S B A M L   `   `   `  ����B  >WpfApp1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:WpfApp1WpfApp1  5   	       WpfApp1.drugandtestwindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35n jSAPBusinessObjects.WPF.ViewerShared, Version=13.0.3500.0, Culture=neutral, PublicKeyToken=692fbea5521e1304F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   'localclr-namespace:WpfApp1     5   	   r nTelerik.Windows.Controls.ImageEditor, Version=2019.3.1023.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7o kTelerik.Windows.Controls.GridView, Version=2019.3.1023.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7f bTelerik.Windows.Controls, Version=2019.3.1023.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7r	 nTelerik.Windows.Controls.FileDialogs, Version=2019.3.1023.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7l
 hTelerik.Windows.Documents.Core, Version=2019.3.1021.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7g cTelerik.Windows.Documents, Version=2019.3.1023.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7b ^Telerik.Windows.Data, Version=2019.3.1023.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7l hTelerik.Windows.Controls.Input, Version=2019.3.1023.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7q mTelerik.Windows.Controls.Navigation, Version=2019.3.1023.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7l hTelerik.Windows.Controls.Media, Version=2019.3.1023.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7Qtelerik1http://schemas.telerik.com/2008/xaml/presentation
    	 
      5   	   $��LeftToRight=�5	   	     � Title$  Information��5
   	   $	��450��6   $	��800��6*    � WindowState$ 	Maximized=�66    � WindowStartupLocation$ CenterScreen=�6N    � 	Resources 5         WpfApp1.ViewModel   	ViewModel   WpfApp1.ViewModel2  
ViewModel2��%:   &	        &	        5   
   6     5   
   6   5      .��5      �  � RowDefinitions 5   
   �� 5      $	��100 �6   6+   �� 5      $��* �6   6)    � ColumnDefinitions 5   
   �� 5      $��1* �6   6,   �� 5      $��1* �6   6,   .+�5"   
   �  5#      �� 5$      $��1* �6#   60   �� 5%      $	��120 �6#   61   .+�5'      1  +Telerik.Windows.Controls.RadAutoCompleteBox  -   5        $��drugtext��50   -   $��0��5'   )   $��0��66   $��10q�6G   $��70��6T      SelectionMode$ Single=�5(   1      WatermarkContent$ 	Drug Name��5)   1      DisplayMemberPath$ drugname��5*      	   TextSearchPath$	 drugname��5,   !   
   TextSearchMode$
 Contains=�5-   !      AutoCompleteMode$ SuggestAppend=�5.   0   50   -   0  *Telerik.Windows.Controls.RadDropDownButton  57      $��1��6)   $��Center=�69   $��Actions��59         DropDownContent 5:      -  'Telerik.Windows.Controls.RadContextMenu  5;      $��LeftToRight=�61   .@�5<      *  $Telerik.Windows.Controls.RadMenuItem  -   5        $��Add New��5<   2   6D     5=      -   5        $
��Edit��5=   2   6@     5>      -   5        $��Delete��5>   2   6B   5?      5A      5D      � 5J   
   $��1��6    5K      �� 5L      $��1* �6#   60   �� 5M      $	��120 �6#   61   .+�5P        -   5        $��testtext��5Y   -   $��0��5P   )   $��0��66   $��10q�6F   $��70��6T   $ 	Test Name��5R   1   $
 name��5S      $
	 name��5U   !   $
 Contains=�5V   !   $ SuggestAppend=�5W   0   5Y   -     5\      $��1��6)   $��Center=�69   $��Actions��5^       5_        5`      $��LeftToRight=�6-   .@�5a        -   5        $��Add New��5a   .   6?     5b      -   5        $
��Edit��5b   .   6<     5c      -   5        $��Delete��5c   .   6>   5d      5f      5i      �� 5r   
   $
��Auto=�6   $��1��6;   $��3q�6J   $��0,0,0,0q�6U   .��5u      �� -	   5        $��list2��5u      
���   �$��2q�6<   $��Stretch=�6Q   $��Stretch=�6m   ��5x      �� 5y      .R�5�      � $��2��6   $��2��6/    5�      �� 5�   "   $
��auto �60   6@    5�      �� 5�   "   $��* �63   6@   .+�5�      �� -
   5        $
��Wrap=�5�   B   $��calibri)�6V   $��16(�6k   $��0��6�   $��2��6�   $��0,0,0,1q�6�   ��6(   �� *
drugname+��5�   "   �� 5�   &    �� EndPoint$ 0.5,1&�6:    �� 
StartPoint$ 0.5,0&�6K   .��5�   *   �  � Color$ Black��67    � Offset$ 0Q�6E   � 5�   *   $ 	#FF0094FC��67   $ 1Q�6I   5�   '   5�      5�      5�      5�      5�      �� 5�   
   $
��Auto=�6   $��1��6;   $��1��6H   $��3q�6Z   $��0,0,0,0q�6e   .��5�      �� -   5        $��list1��5�      
���   �$��2q�6<   $��Stretch=�6Q   $��Stretch=�6m   ��5�      �� 5�      .R�5�      � $��2��6   $��2��6/    5�      �� 5�   "   $
��auto �60   6@    5�      �� 5�   "   $��* �63   6?   .+�5�      �� -   5        $
��Wrap=�5�   >   $��calibri)�6S   $��16(�6�   $��0��6�   $��2��6�   $��0,0,0,1q�6�   ��6(   �� *name+��5�   "   �� 5�   &   $ 0.5,1&�6:   $ 0.5,0&�6K   .��5�   *   � $ Black��67   $ 0Q�6E   � 5�   *   $ 	#FF0094FC��67   $ 1Q�6I   5�   '   5�      �� 5�       �� Orientation$ 
Horizontal=�6)   $��1��6B   $��2��6P   $	��Top=�6d   5�      5�      5�      5�      5�      5�      5�      