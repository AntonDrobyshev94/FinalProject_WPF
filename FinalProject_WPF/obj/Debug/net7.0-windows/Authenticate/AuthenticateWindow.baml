   M S B A M L   `   `   `  ����K  GFinalProject_WPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullM+clr-namespace:FinalProject_WPF.AuthenticateFinalProject_WPF.Authenticate  5   	   6    0FinalProject_WPF.Authenticate.AuthenticateWindow   5   	   W SPresentationCore, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   =local+clr-namespace:FinalProject_WPF.Authenticate     5   	     � Title$  AuthenticateWindow��5   	   $	��330��6$   $	��370��61    � WindowStartupLocation$ CenterScreen=�6=    � WindowStyle$
 None=�6b   ��5	      �� 5
   
    �� EndPoint$ 0.5,1&�6    �� 
StartPoint$ 0.5,0&�6/   .��5      �  � Color$ 	#FF1000D8��6   � 5      $ White��6    � Offset$ 1Q�6)   � 5      $ 	#FF8700D8��6   $ 0.326Q�6-   � 5      $ 	#FF3985DA��6   $ 0.643Q�6-   5      .��5      �  � RowDefinitions 5   
   �� 5      $��50 �6   �� 5      $��30 �6   �� 5      $��5 �6   �� 5      $��30 �6   �� 5      $��5 �6   �� 5      $��30 �6   �� 5      $��5 �6   �� 5      $��30 �6   �� 5      $��5 �6   �� 5      $��40 �6   �� 5      $��5 �6   �� 5      $��40 �6   �� 5      $��5 �6    � ColumnDefinitions 5!   
   �� 5"      $	��100 �6   �� 5#      $	��150 �6   �� 5$      $	��100 �6   .+�5&   
   � -   5        $��ToolBar��5&      
������ $��0��67   $��3��6D   $��5��6X   .+�5'      �� 	 �� Orientation$	 
Horizontal=�6   $��Right=�62   $	��Top=�6N   .+�5(      �� -   5        $��CollapseButton��5(         FPCollapseButton#��[  6.   $��25��6X   $��25��6d   $��0,5,10,5q�6o   
 J� Effect
 5)      -  'System.Windows.Media.Effects.BlurEffect  5*         Radius$ 1Q�6%   62   5,      �� 5-      -   5        $��CrossButton��5-        FPCrossButton#��[ 6+   $��25��6R   $��25��6^   
 5.        5/      $ 1Q�6%   62   51      52      53      �� 54   
   $��1��6   $��3��6#   �� 55   
   $��1��6   $��3��6!   $��1q�6.    �� CornerRadius$ 2��6B   ���#0029e0���#111112._�56      �� -   5         ��Name$ TxtLogin��56      
������� �� TextWrapping$
 Wrap=�6;   $��18(�6O   $�� ��6]   ��6e   Z� 57      �� 58   
   $��1��6   $��7��6!   $��1q�6.   $ 2��6B   ���#0029e0���#111112._�59      G� -   5         G�Name$ PasswordBox��59      
�������$��18(�6B   ��6P   Z� 5:      �� 5<   
   -   5         ��Name$ EnterButton��5<      $��
Войти��6&     FPButtonStyle#��[ 66   
�������$��18(�6p   $��1��6~   $��9��6�   �� 5=   
   -   5        $ RegistrationButton��5=      $��Регистрация��6-   #��[ 6C   
�������$��18(�6}   $��1��6�   $��11��6�   �� 5>   
   $
��Wrap=�6   $��
Логин��6(   $��DemiBold"�65   
������� �� TextAlignment$ Center=�6b   $��18(�6y   $��1��6�   $��1��6�   �� 5?   
   $
��Wrap=�6   $��Пароль��6(   $��DemiBold"�66   
�������$ Center=�6c   $��18(�6z   $��1��6�   $��5��6�   5@      5A      