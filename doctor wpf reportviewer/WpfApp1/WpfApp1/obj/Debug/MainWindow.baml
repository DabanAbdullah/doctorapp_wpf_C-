   M S B A M L   `   `   `  ����B  >WpfApp1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:WpfApp1WpfApp1  5   	   !clr-namespace:WpfApp1WpfApp1  5
          WpfApp1.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35n jSAPBusinessObjects.WPF.ViewerShared, Version=13.0.3500.0, Culture=neutral, PublicKeyToken=692fbea5521e1304H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   'localclr-namespace:WpfApp1     5   	   r nTelerik.Windows.Controls.ImageEditor, Version=2019.3.1023.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7o kTelerik.Windows.Controls.GridView, Version=2019.3.1023.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7f	 bTelerik.Windows.Controls, Version=2019.3.1023.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7r
 nTelerik.Windows.Controls.FileDialogs, Version=2019.3.1023.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7l hTelerik.Windows.Documents.Core, Version=2019.3.1021.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7g cTelerik.Windows.Documents, Version=2019.3.1023.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7b ^Telerik.Windows.Data, Version=2019.3.1023.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7l hTelerik.Windows.Controls.Input, Version=2019.3.1023.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7q mTelerik.Windows.Controls.Navigation, Version=2019.3.1023.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7l hTelerik.Windows.Controls.Media, Version=2019.3.1023.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7Qtelerik1http://schemas.telerik.com/2008/xaml/presentation
   	 
       5   	   2behaclr-namespace:WpfApp1
   	 
       5
      -   5        $��LeftToRight=�5   	     � Title$  
AzherGroup��5   	   $	��450��6   $	��800��6)    � WindowStartupLocation$ CenterScreen=�65    � WindowState$ 	Maximized=�6Z    � WindowStyle$
 None=�6r   .��5      �  � RowDefinitions 5   
   �� 5      $��* �6   6)   .+�5   
   0  *Telerik.Windows.Controls.RadNavigationView  -   5          Name$ navigationView��5   $      
PaneHeader$
 Menu��6<   $��1��6N      
IsPaneOpen .  @�5      4  .Telerik.Windows.Controls.RadNavigationViewItem  5      -   5        $��patientm��5   B   $��patient��60      Icon 5      ' 	 !Telerik.Windows.Controls.RadGlyph  5      	   Glyph$
	 user��6+   
   FontSize$
 22(�68   5        5      -   5        $��visitm��5   @   $��visit��60    5         5!      $		 ��6+   $
 22(�6<   5#        5%      -   5        $��gallerym��5%   B   $��gallery��60    5&        5'      $		 ��6+   $
 22(�6<   5)        5+      -   5        $��infom��5+   F   $��Information��60    5,        5-      $		 ��6+   $
 22(�6<   5/        55      -   5        $��usersm��55   @   $��Users��60    56        57      $		 ��6+   $
 22(�6<   59        5:      -   5        $��backupm��5:   A   $��Backup��60    5;        5<      $		 ��6+   $
 22(�6<   5>        5@      -	   5        $��aboutm��5@   @   $��About��60    5A        5B      $		 ��6+   $
 22(�6<   5D        5F      -
   5          Name$ updatem��5F   A   $��Update��60    5G        5H      $		 ��6+   $
 22(�6<   5J        5M      -   5        $��report��5M   A   $��Report��60    5N        5O      $		 ��6+   $
 22(�6<   5Q        5T      -   5        $��exitm��5T   ?   $
��Exit��60    5U        5V      $		 ��6+   $
 22(�6<   5X         Content 5^       � 5`      -   5        $��frame1��5`      
�������   WpfApp1.DisableNavigation   Disable . 6;      
PaneFooter 5c        5d      -   5        $��Settings��5d   0   ��5f   0   ��  �� ElementName$ navigationView��   IconTemplate 5g   0   Z�  5h        5i      $		 ��6+   $
 20(�6<   5k      5m      5q      5r      