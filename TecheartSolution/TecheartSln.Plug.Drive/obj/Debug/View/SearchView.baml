   M S B A M L   `   `   `  ����Q  MTecheartSln.Plug.Drive, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullI)clr-namespace:TecheartSln.Plug.Drive.ViewTecheartSln.Plug.Drive.View  5      ,    &TecheartSln.Plug.Drive.View.SearchView   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      ;local)clr-namespace:TecheartSln.Plug.Drive.View     5      .��5	      � {�6   ��   �� Path$  RefusePeople� �� Mode$ TwoWay=�.+�5
   
   �� .+�5      � .+�5      �� $��YY号��6   $
��Left=�6&   $��	10,10,0,0q�6A   $	��Top=�6T   �� 5      $��查询��6   $
��Left=�6&   $��	77,72,0,0q�6A   $	��Top=�6T   $	��213��6l   ��6x   �� $  SearchCommand��� 5      $
��Left=�6   $��23��65   $��	77,14,0,0q�6A    �� TextWrapping$
 Wrap=�6T   $	��Top=�6�   $	��213��6�   ��6h   �� $  YYNumber�$ TwoWay=�6�   �� 5      $��真实姓名��6   $
��Left=�6'   $��	10,42,0,0q�6B   $	��Top=�6U   �� 5      $
��Left=�6   $��23��65   $��	77,37,0,0q�6A   $
 Wrap=�6T   $	��Top=�6�   $	��213��6�   ��6h   �� $  RealName�$ TwoWay=�6�   �� 5      $��角色��6   $
��Left=�6%   $��
295,14,0,0q�6@   $	��Top=�6T   $��40��6l   �� 5      $
��Left=�6   $��
335,16,0,0q�66   $	��Top=�6J   $	��120��6b    �� SelectedValuePath$	 Key��6   �� DisplayMemberPath$ Value��6   ��6n   �� $  OperationManagerList�$ TwoWay=� �� SelectedValue 6�   �� *OperationManager+$ TwoWay=� �� UpdateSourceTrigger$ PropertyChanged=�6   �� 5      $
��Left=�6   $��
460,16,0,0q�66   $	��Top=�6J   $	��120��6b   $	 Key��6�   $ Value��6  ��6n   �� $  StarManagerList�$ TwoWay=� 6�   �� *StarManager+$ TwoWay=�$ PropertyChanged=�6  �� 5      $
��Left=�6   $��
585,16,0,0q�66   $	��Top=�6J   $	��120��6b   $	 Key��6�   $ Value��6	  ��6n   �� $  StarList�$ TwoWay=� 6�   �� *Star+$ TwoWay=�$ PropertyChanged=�6	  �� 5      $��查询��6   $
��Left=�6&   $��
335,41,0,0q�6A   $	��Top=�6U   $	��370��6m   ��6y   �� *SearchArrangementCommand+5      �� 5      $
��Auto=�6   .��5      &   System.Windows.Controls.DataGrid     CanUserSortColumns .    CanUserReorderColumns . 	   CanUserResizeColumns	 . 
   CanUserResizeRows
 . $	��400��6�   $��Bottom=�6�      AutoGenerateColumns .     
IsReadOnly . ��6   �� *QuesionList+   Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$ 用戶id��6P    0  ,MaterialDesignDataGridTextColumnEditingStyle1  +System.Windows.Controls.DataGridBoundColumn   EditingElementStyle# [  6^      Binding 6-   �� *UserInfoUserId+6^     5      $ 用户姓名��6R   # [  6`    6-   �� *UserInfoUserName+6`     5      $ 用户备注��6P   # [  6^    6-   �� *UserInfoExtend+6^     5      $ 用户类型��6U   # [  6c    6-   �� *UserInfoUserTypeStr+6c     5      $ YY号��6T   # [  6a    6-   �� *AnchorInfoYYNumber+6a     5      $ 昵称��6V   # [  6b    6-   �� *AnchorInfoAnchorName+6b     5       $ 艺人分类��6Y   # [  6g    6-   �� *AnchorInfoAnchorTypeStr+6g     5!      $ 分成方式��6Y   # [  6g    6-   �� *AnchorInfoAnchorTypeStr+6g     5"      $ 签约开始时间��6U   # [  6e    6-   �� *AnchorInfoStartTime+6e     5#      $ 签约截止时间��6S   # [  6c    6-   �� *AnchorInfoEndTime+6c     5$      $ 艺人的真实姓名��6T   # [  6e    6-   �� *AnchorInfoRealName+6e     5%      $ 主播备注��6R   # [  6`    6-   �� *AnchorInfoExtend+6`   5'      5(      5*      5,      5-      