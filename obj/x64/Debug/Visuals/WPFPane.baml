   M S B A M L   `   `   `  ����F  BMailWarden2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:MailWarden2MailWarden2  5          MailWarden2.WPFPane   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      +localclr-namespace:MailWarden2     5      $��340.164��5      $��505.328��6/   .��5      �   � RowDefinitions  5	   
   �� 5
      $
��0.1* �6   �� 5      $
��0.8* �6   �� 5      $
��0.1* �6   .+�5   
   �� $��Mails to be handled��6   $��Center=�6.   $��Center=�6K   $��0��6f   R� 5   
   -   5        $��Mails��5      $��1��6    $��5,5,5,5q�6-    %� VirtualizationMode$ 	Recycling=�6>   ��6o   �� ��5      �� 5       �� ItemsSource 6+   �� *Mails+.R�5      �� ��6    �� *
FolderName+ �� ItemTemplate 5      �� 5      .R�5      ��  �� Orientation$ 
Horizontal=�6)   .+�5   "   ��  �� Padding$ 0 5 0 5q�6C   ��6,   �� *Timer+6C   �� 5   "   $	�� - ��6,   $ 0 5 0 5q�67   �� 5   "   $ 0 5 0 5q�6E   $	��250��6W   $��CharacterEllipsis=�6c   ��6,   �� *	Subject+6c   5      5      5       � 	Resources 5       �� 
TargetType �� Property	 �� Value��%i   'Q�       �� 5      " Q�6   .�5       ��  �@Q�
IsExpanded	 . 63   -   5        5"      5#      5$      5%      5&      