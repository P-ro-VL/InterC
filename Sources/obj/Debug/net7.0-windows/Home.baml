   M S B A M L   `   `   `  ����A  =InterC, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:InterCInterC  5   	       InterC.Home   5   	   W SPresentationCore, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   &localclr-namespace:InterC     5   	     � WindowStartupLocation$  CenterScreen=�5   	    � WindowStyle$
 None=�5	   	    � 
ResizeMode$ NoResize=�5
   	    � Title$ InterC��5   	   $	��576��6   $
��1024��6%   .��5      � .+�5   
   �� $��/src/main_ui.png��6   �� 5   
   $	��252��6   $	��414��6#   $��748 117 24 45q�60   $��Hidden=�6G   .��5      �� -   5        $��problem_tray��5       �� View 5      �� 5       �� AllowsColumnReorder$ False��6    �� ColumnHeaderToolTip$+ %Danh sách các bài toán hiện có��6;   .P�5      �� $��ID��6)    �� Width$ 50��65    �� DisplayMemberBinding 6@   �� *ID+�� 5      $��	Tên bài��6)   $	 130��6:    6F   �� *Name+�� 5      $��
Độ khó��6)   $ 50��69    6D   �� *
Difficulty+5      5      5      �� 5   
   $	��692��6   $	��414��6#   $��24 117 308 45q�60   $��Hidden=�6G   .��5      � -   5        $��displayInfoPanel��5      $	��692��60   	 � Orientation$	 Vertical=�6<   6U   5      �� 5   
   -   5        $��submitCodeBtn��5      $	��168��6%   $��47��61   $��548 53 308 476q�6=   
 � Cursor$

 Hand��5       $y�Hidden=�6   $��/src/submit_code_btn.png��62   $��UniformToFill=�5!      �� 5#   
   -   5        $��viewHistoryButton��5#      $	��209��6-   $��47��69   $��769 53 46 476q�6E   $

 Hand��6\   �� 5%   
   -   5        $
��exit��5%      $��25��6    $��25��6+   $��990 0 0 542q�67   $

 Hand��6L   � 5'   
   5)      5*      5+      