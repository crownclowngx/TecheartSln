   M S B A M L   `   `   `  ����Q  MTecheartSln.Plug.Drive, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullI)clr-namespace:TecheartSln.Plug.Drive.ViewTecheartSln.Plug.Drive.View  5      0    *TecheartSln.Plug.Drive.View.SearchUserView   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      ;local)clr-namespace:TecheartSln.Plug.Drive.View     5      .��5	      �� .+�5
   
   � .+�5      �� $
��Left=�6   $��	10,10,0,0q�62   $	��Top=�6E   $	��120��6]     �� SelectedValuePath$	  Key��6�    �� DisplayMemberPath$ Value��6  ��6i   ��  �� Path$ UserTypeList� �� Mode$ TwoWay=� �� SelectedValue 6�   �� *
UserType+$ TwoWay=� �� UpdateSourceTrigger$ PropertyChanged=�6  �� 5      $
��Left=�6   $��23��61   $��
313,10,0,0q�6=    �� TextWrapping$
 Wrap=�6Q   $	��Top=�6�   $	��120��6�   ��6e   �� *InputSearch+$ TwoWay=�$ PropertyChanged=�6�   �� 5      $��	登录名��6   $
��Left=�6"   $��
262,10,0,0q�6=   $	��Top=�6Q   5      �� 5   
   $
��Auto=�6   .��5      &   System.Windows.Controls.DataGrid     CanUserSortColumns .    CanUserReorderColumns . 	   CanUserResizeColumns	 . 
   CanUserResizeRows
 .  $	��400��6�   $��Bottom=�6�      AutoGenerateColumns .     
IsReadOnly . ��6   �� *QuesionList+   Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$ 用戶id��6L    0  ,MaterialDesignDataGridTextColumnEditingStyle1  +System.Windows.Controls.DataGridBoundColumn   EditingElementStyle# [  6Z      
IsReadOnly .    Binding 6)   �� *UserInfoUserId+6�     5      $ 用户姓名��6N   # [  6\    6)   �� *UserInfoUserName+6\     5      $ 备注��6L   # [  6X    6)   �� *UserInfoExtend+6X     5      $ 用户类型��6Q   # [  6_    .  6)   �� *UserInfoUserTypeStr+6�   5      5      5      5      