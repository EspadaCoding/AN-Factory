   M S B A M L   `   `   `  ����=  9AN, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:AN.PrintingViewAN.PrintingView  5      (    "AN.PrintingView.PrintAllModelsView   5      W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5      R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      /localclr-namespace:AN.PrintingView     5        N� 
Background
  ����� N� Title$ PrintAllModelsView��5
      .��5      �  � RowDefinitions 5   
   �� 5      $��0.15* �6   �� 5      .+�5   
   � -   5         �Name$ LogoGrid��5      $��0��6   .+�5      �� $��ANGoldWatch��6   $��Center=�6*   $��Center=�6E   $��50(�6b   �� 5      -   5         ��Name$ 
PageNumber��5      $
��Page��6(   $��Right=�67   $	��Top=�6S   $��50(�6k   $��	0,20,20,0q�6y   5      � 5   
   $��1��6    � ColumnDefinitions 5      �� 5      �� 5      $	��10* �6#   �� 5      .+�5      � $��1��6    5      �� 5      $	��10* �6$   �� 5      .+�5       � $��0��6   .+�5!      �� -   5         ��Name$ Models��5!      $��20(�6c    �� SelectedItem 6/   �� *SelectedModel+ �� Mode$ TwoWay=�	 �� View	 5"      �� 5#      .P�5$   "   �� 
 �� Width$

 Auto��5%   !   $��Modelər��5&   !    �� DisplayMemberBinding 5'   !   �� *Name+�� 5(   "   $

 Auto��61   $��Ümumi xərc��6>    6S   �� *
TotalPrice+5)      5+      5,      � 5-      $��1��6   .+�5.      �� -   5        $
 Date��5.      $
��Date��6*   $��40(�69   $��Right=�6G   $	��Top=�6c   5/      50      51      52      53      