   M S B A M L   `   `   `  ����B  >WpfApp1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:WpfApp1WpfApp1  5   	       WpfApp1.userwindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35n jSAPBusinessObjects.WPF.ViewerShared, Version=13.0.3500.0, Culture=neutral, PublicKeyToken=692fbea5521e1304H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   'localclr-namespace:WpfApp1     5   	   r nTelerik.Windows.Controls.ImageEditor, Version=2019.3.1023.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7o kTelerik.Windows.Controls.GridView, Version=2019.3.1023.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7f	 bTelerik.Windows.Controls, Version=2019.3.1023.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7r
 nTelerik.Windows.Controls.FileDialogs, Version=2019.3.1023.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7l hTelerik.Windows.Documents.Core, Version=2019.3.1021.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7g cTelerik.Windows.Documents, Version=2019.3.1023.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7b ^Telerik.Windows.Data, Version=2019.3.1023.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7l hTelerik.Windows.Controls.Input, Version=2019.3.1023.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7q mTelerik.Windows.Controls.Navigation, Version=2019.3.1023.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7l hTelerik.Windows.Controls.Media, Version=2019.3.1023.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7Qtelerik1http://schemas.telerik.com/2008/xaml/presentation
   	 
       5   	   -   5        $��LeftToRight=�5
   	     � Title$  
userwindow��5   	   $	��450��6   $	��800��6)    � WindowStartupLocation$ CenterScreen=�65    � WindowState$ 	Maximized=�6Z   .��5      � ���	#FFEFFFFF � ColumnDefinitions 5   
   �� 5      $��3* �6   6,   �� 5      $��1* �6   6,   .+�5   
   �� $
��Auto=�6   $��0��6:   $��1��6G   $��0,0,0,0q�6X   $��0,0,0,0q�6i   .��5      �� -   5        $��list1��5      
���   �$��2q�6=   ��5      �� 5      .R�5       � $��5q�6#    � RowDefinitions 5!   "   �� 5"   &   $��35 �64   6C   �� 5#   &   $��25 �64   6C   .+�5*   "   ��  �� Orientation$ 
Horizontal=�6-   $��0��6G   $	��Top=�6V   $��2��6n   .+�5,   &   �� $	��100��6.   $��24��6:   $	��Top=�6F   $��
20,-5,10,0q�6_    �� TextWrapping$
 Wrap=�6�   ��6s   �� *uname+6�   �� 5-   &   $	��100��61   $��24��6=   $	��Top=�6I   $��
20,-5,10,0q�6b   $
 Wrap=�6�   ��6v   �� *upass+6�   5/   #   �� 51   "   $ 
Horizontal=�6-   $��1��6F   $��2��6T   $	��Top=�6h   .+�54   &   �� $	��200��6.   $��24��6:   $	��Top=�6F   $��
20,-5,10,0q�6_   $
 Wrap=�6�   ��6s   �� *role+6�   59   #   5<      5>      5@      5F      � 5M   
    5N      �� 5O      $��2* �6    6.   �� 5P      $��2* �6    6.   .+�5U      � ���	#FFF3FFF7 5X      �� 5Y      $��1* �6$   62   �� 5Z      $��1* �6$   62   �� 5[      $��1* �6$   62    5^      �� 5_      $��1* �6'   64   �� 5`      $��3* �6'   64   .+�5c      �� $
��Name��6   $��calibri)�6(   $��19(�6=   
���   �$��Center=�6^   $��Center=�6y   �� 5e      $��Password��6   $��calibri)�6,   $��1��6A   $��19(�6N   
���   �$��Center=�6o   $��Center=�6�   �� 5g      -   5        $��uname��5g      $��calibri)�6(   $��19(�6=   $��3q�6K   $��30��6V   $��1��6c   
���  ��6s   - 	 'Telerik.Windows.Controls.RadPasswordBox  5i      -   5        $��upass��5i   )   $��3q�66   $��calibri)�6A   $��1��6V   $��19(�6c   $��30��6q   $��1��6~   
���  ��6�   �� 5m      $ 
Horizontal=�6   $��1��66   $��10q�6F   $��2��6S   .+�5o      �� -   5        $	��add��5o   T   
���   �$��32��61   $��48��6=   $��3q�6H   ��5p      �� 5q       �� ImageSource$ Image/save.png��6)    j� Stretch$ Uniform=�6F   5s      �� 5u      -   5        $
��edit��5u   S   
���   �$��32��61   $��48��6=   $��3q�6H   ��5v      �� 5w      $ Image/edit.png��6)   $ Uniform=�6F   5y      �� 5{      -   5        $��trashdel��5{   1   
���   �$��32��6X   $��48��6d   $��3q�6o   ��5|      �� 5}      $ image/trash.png��6)   $ Uniform=�6G   5      5�      5�      �� 5�      $
��Auto=�6   $��1��6>   $��0,0,0,0q�6K   $��0,0,0,0q�6\   ���	#FFF6FAEE.��5�      � -   5        $��pr��5�      .+�5�      �� -	   5        $��productlist��5�   #   ��5�      �� 5�      .R�5�   "   � 5�      ��5�      �� 5�      .R�5�   "   �� $ 
Horizontal=�6-   $��20q�6F   .+�5�   &   �� -
   5        $��calibri)�5�   N   $��16(�6c   	 d� 	IsChecked	 6/   �� *check+6q   �� 5�   &   $��calibri)�6F   $��16(�6[   ��60   �� *role+6k   5�   #   5�      5�      5�      5�      5�      5�      5�      