   M S B A M L   `   `   `  ����=  9AN, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:AN.PrintingViewAN.PrintingView  5      (    "AN.PrintingView.PrintDoneOrderView   5      W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5      R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      /localclr-namespace:AN.PrintingView     5        N� 
Background
  ����� N� Title$ PrintDoneOrderView��5
      .��5      �  � RowDefinitions 5   
   �� 5      $��0.15* �6   �� 5      .+�5   
   � -   5         �Name$ LogoGrid��5      $��0��6   .+�5      �� $��ANGoldWatch��6   $��Center=�6*   $��Center=�6E   $��50(�6b   5      � 5   
   $��1��6    � ColumnDefinitions 5      �� 5      �� 5      $	��10* �6#   �� 5      .+�5      � $��1��6    5      �� 5      $��0.15* �6$   �� 5      �� 5      $
��0.3* �6$   .+�5       � -   5        $ WorkerNameGrid��5       $��0��6/   .+�5!      �� -   5        $��
WorkerName��5!      $	��Top=�6.   $��Nijat Aliyev��6F   $��Center=�6]   $��45(�6z   5"      � 5#      $��1��6    5$      �� 5%      �� 5&      �� 5'      �� 5(      �� 5)      �� 5*      �� 5+      �� 5,      �� 5-      �� 5.      �� 5/      .+�51      � $��0��6   .+�52      �� -   5        $��Model��52       $��Center=�6-   $
��Left=�6H   $��Model -��6c   $��50,0,0,0q�6u   $��30(�6�   53      � 54      $��1��6   .+�55      �� -   5        $��BanchNumber��55       $��Center=�63   $
��Left=�6N   $��Partia -��6i   $��50,0,0,0q�6|   $��30(�6�   56      � 57      $��2��6   .+�58      �� -   5        $��Number��58       $��Center=�6.   $
��Left=�6I   $��Dənə -��6d   $��50,0,0,0q�6u   $��30(�6�   59      � 5:      $��3��6   .+�5;      �� -   5        $��TotalWeight��5;       $��Center=�63   $
��Left=�6N   $��Total çəki -��6i   $��50,0,0,0q�6�   $��30(�6�   5<      � 5=      $��4��6   .+�5>      �� -   5        $��BronzeWeight��5>       $��Center=�64   $
��Left=�6O   $��Korpus çəki -��6j   $��50,0,0,0q�6�   $��30(�6�   5?      � 5@      $��5��6   .+�5A      �� -	   5        $��
GoldWeight��5A       $��Center=�62   $
��Left=�6M   $��Qızıl çəki -��6h   $��50,0,0,0q�6   $��30(�6�   5B      � 5C      $��6��6   .+�5D      �� -
   5        $��WasteWeight��5D       $��Center=�63   $
��Left=�6N   $��Ugar -��6i   $��50,0,0,0q�6z   $��30(�6�   5E      � 5F      $��7��6   .+�5G      �� -   5        $
��Date��5G       $��Center=�6,   $��Right=�6G   $��0,0,0,0q�6c   $��30(�6t   5H      5I      5K      5L      5M      5N      