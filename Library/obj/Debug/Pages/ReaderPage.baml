   M S B A M L   `   `   `  ����B  >Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:Library.PagesLibrary.Pages  5          Library.Pages.ReaderPage   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      -localclr-namespace:Library.Pages     5      -   5          N� Title$  
ReaderPage��5	      .��5      �  � ColumnDefinitions 5   
   �� 5      $	��51* �6   �� 5      $	��46* �6   �� 5      $	��46* �6   �� 5      $	��45* �6   �� 5      $	��44* �6   �� 5      $
��397* �6   �� 5      $
��118* �6   �� 5      $	��53* �6    � RowDefinitions 5   
   �� 5      $	��100 �6   6+   �� 5      $
��156* �6   6,   �� 5      $	��19* �6   .+�5   
   ��  �� Orientation$ 
Horizontal=�6   $��1��6.   $��6��6>   .+�5      �� -   5         ��Name$ cbGenre��5      $��30��6(   $	��150��67   $
��0,35q�6F    �� DisplayMemberPath$ .��6T   6j   �� 5      -   5        $ cbPublisher��5      $��30��6,   $	��150��6;   $��15,35q�6J   $ .��6Y   6p   �� 5      -   5        $ cbAuthor��5      $��30��6)   $	��150��68   $
��0,35q�6G   $ .��6U   6k   �� 5      $��Дата��6   $��	5 15 5 20q�6%   .��5       �� $ 
Horizontal=�6   $��Center=�66   .+�5!      (  "System.Windows.Controls.DatePicker    5"      5#      5$      5%      &   System.Windows.Controls.DataGrid  5&   
   -   5          Name$ DgReaderBook��5&   1   $��1��6   $��1��6!   ���	#FFDCF6F0$��6��6^      AutoGenerateColumns .     CanUserAddRows .  	   Columns	 5'      0  *System.Windows.Controls.DataGridTextColumn  5(      ,  &System.Windows.Controls.DataGridColumn
   Header$%
 Срок возвращения��6%   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6?   �� *retDay+6\     5)      $!
 Автор Название��6%    6=   �� *book.authorTitle+6d     5*      $
 Издательство��6%    6;   �� *book.publisherEx+6b     5+      $
 Количество��6%    69   �� *book.height+6[   4  .System.Windows.Controls.DataGridTemplateColumn  5,      $.
 (Отметка о возвращении��6)      CellTemplate 5-      �� 5.      .R�5/      �� $��40��6A   $��40��6L   ��6$   ��  �� Path$
 path�6L   50      52      54      �� 55   
   -   5         ��Name$
 word��55      $��1��6   $��2��60   $��\Icon\img_400436.png��6>   6\   �� 56   
   -   5        $ xl��56      $��2��6   $��2��6-   $ ��\Icon/exicon-1024x1024.png��6;   6_   �� 57   
   -   5        $	 pdf��57      $��3��6   $��2��6/   $��\Icon/img_210051.png��6=   6[   �� 58   
   -	   5        $
 graf��58      $��4��6   $��2��6/   $��\Icon/img_99111.png��6=   �� 59   
   -
   5        $
 back��59      $��6��6   $��2��6/   $
��Left=�6<   $H��B\Icon\returns-3-of-the-margin-1e07d250d5f71c08ba8c529f43576b4f.png��6X   $��38��6�   6�   �� 5:   
   $��6��6   $��2��6!   $��Right=�6.   $��/icon/logo_vis222.png��6K   $��75��6j   5;      5<      