   M S B A M L   `   `   `  ����K  GTecheartSln.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:TecheartSln.CoreTecheartSln.Core  5      G(clr-namespace:TecheartSln.Core.View.PaneTecheartSln.Core.View.Pane  5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089T PAvalonDock, Version=2.0.1771.0, Culture=neutral, PublicKeyToken=96637cf0d3eefdfb5   /AvalonDock.Converters.BoolToVisibilityConverter   BoolToVisibilityConverter.  (AvalonDock.Controls.LayoutAnchorableItem  �� 
TargetType  	ToolStyle$  AvalonDock.Controls.LayoutItem �� Property �� Value �� Mode �� 	Converter  System.Windows.Visibility   Hidden �� ConverterParameter  	FileStyle�� 5      D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    :x,http://schemas.microsoft.com/winfx/2006/xaml     5      Bmc;http://schemas.openxmlformats.org/markup-compatibility/2006  5      8d2http://schemas.microsoft.com/expression/blend/2008  5      (localclr-namespace:TecheartSln.Core  5      6	localview(clr-namespace:TecheartSln.Core.View.Pane  5      /
avalonDockhttp://avalondock.codeplex.com  5      %s  &	        &	      7   &	 �       5
      6+   �� 5      "   6   .�5   
   ��  �@ Title 6"   �� *Model.Title+�� 5   
    �@ 
IconSource 6'   �� *Model.IconSource+�� 5   
    � y� 6'   �� *Model.IsVisible+$ TwoWay=�8   # Z �� 5   
    �@ 	ContentId 6&   �� *Model.ContentId+�� 5   
    �@ 
IsSelected 6'   �� *Model.IsSelected+$ TwoWay=��� 5   
    �@ IsActive 6%   �� *Model.IsActive+$ TwoWay=�5      �� 5      "   6   .�5   
   ��  �@ Title 6"   �� *Model.Title+�� 5   
    �@�ToolTip 6$   �� *Model.FilePath+�� 5   
    �@ CloseCommand 6)   �� *Model.CloseCommand+�� 5   
    �@ 
IconSource 6'   �� *Model.IconSource+�� 5   
    �@ 	ContentId 6&   �� *Model.ContentId+5      5      