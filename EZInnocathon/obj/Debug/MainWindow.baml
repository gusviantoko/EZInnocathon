   M S B A M L   `   `   `  ����G  CEZInnocathon, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:EZInnocathonEZInnocathon  5   	       EZInnocathon.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ,localclr-namespace:EZInnocathon     5   	     � WindowStartupLocation$  CenterScreen=�5   	    � WindowStyle$
 None=�6.    � AllowsTransparency .  � Title$ EZ Organize��5	   	   $	��920��6   $
��1290��6*    � 
ResizeMode$ NoResize=�67   ��6M   Z�  � 	Resources 5       �� 
TargetType   BottomButtons �� Property �� Value	 �� 
TargetType
 �� Triggers P� Property P� Value��%�  &	        '�� �    �� 5   
   " ��6   .�5      ��  � �� �	#FFD8560C6+   �� 5       � �� 5      �� 5      "	 ��6&   .R�5      �� #��z��6!   
���   �$��0q�6_   ._�5      �� $��Center=�6/   $��Center=�6L   5      5      5      -   5        
 5      P� 5       �@J�IsMouseOver . . �5      ��  � �� �	#FFFF4E2763   5      5      �� 5   
   " ��6   .�5      ��  � �� 6+   Z� �� 5        � �� �	#FFD2D3FD6+   �� 5!       � ��$	 100��6'   �� 5"       � ��$	 250��6&   �� 5#       � �� 5$      �� 5%      "	 ��6&   .R�5&      �� #��z��6!   
���   �$��0q�6_   ._�5'      �� $��Center=�6/   $��Center=�6L   5(      5)      5+      -   5        
 5-      P� 5.       �@J�IsMouseOver . . �5/      ��  � �� �	#FF153A7A63   50      52      53      .��54      � $��	40 40 0 0q�6   .+�55   
   �� $"��resource\ezo-logo-shadow.png��6   $��Uniform=�66   $	��250��6I    K� ZIndex$
 5000��6U   $��-31,-66,1031,0q�6i   $	��260��6�   $	��Top=�6�   r� 56   
   -   5        $��buttonSound��56       r� Volume$ 1Q�6*   67   ]� 57   
   ��. ���	#FFD8D8D8.+�58      ]� -   5        $��titleBar��58      $
 5000��6(   $��40��6<   $	��Top=�6H   $	��Top=�6`   ��59      �� 5:       �� EndPoint$ 0.5,1&�6*    �� 
StartPoint$	 1,0&�6;   .��5;      �  � Color$ 	#FF5163B9��6'    � Offset$ 1Q�69   � 5<      $ 	#FF6AADF0��6'   $
 0.51Q�69   � 5=      $ 	#FF3C9DFF��6'   $ 0.071Q�69   5>      .+�5@      ]� .+�5A      �� -   5         ��Name$ 
miniButton��5A      ���	#FF9B9B9B$��1170, 0,0,0q�6^   $
��30.5��6s   $
��Auto��6�   $��Center=�6�   ��6H   Z� .��5B      �� $��resource\minimize.png��6    $��Uniform=�6?   $��24��6Q    J� Opacity$	 0.7Q�6\   5C      �� 5D      -   5        $ 
exitButton��5D      ���	#FF9B9B9B$��5, 0,0,0q�6^   $
��30.5��6p   $
��Auto��6}   $��Center=�6�   ��6H   Z� .��5E      �� $'��!resource\power-on-semicircle-.png��6    $��Uniform=�6K   $��24��6]   $	 0.7Q�6h   5F      5G      5H      ]� 5I      -   5         ]�Name$ 	sidePanel��5I      $
��Left=�6+   $	��250��6A   ��.  ���	#FF5163B9.+�5J      ]� $	��130��6   $	��Top=�6)   ��.  5K      ]� 5L      $	��160��6   $	��Top=�6)   ��.  $y�	Collapsed=�6T   .+�5M      ]� $��50��6    $	��top=�6,   ]� 5N      $	��top=�6    $��58 0 0 0q�65   .+�5O      �� -   5        $��dayText��5O   $   $��	Wednesday��63   $��25(�6D   $��Center=�6R   $��Century Gothic)�6m   $
��Left=�6�   
�������6�   5P      �� 5Q       �� Orientation$ 
Horizontal=�6!   $	��Top=�6:   .+�5R      ]� $
��Left=�6$   .+�5S      �� -	   5        $��dateText��5S   (   $��17��68   $��50(�6B   $��Center=�6P   $��Century Gothic)�6k   
�������$��	58 0 10 0q�6�   6�   5T      ]� 5U      $
��Left=�6$   .+�5V      �� -
   5        $��	monthText��5V   (   $��February��69   $��15(�6I   $	��Top=�6W   $��Century Gothic)�6o   
�������$��0 10 0 0q�6�   6�   5W      5X      5Y      �� 5Z      -   5        $��EZOrganizer��5Z   1   $	��Top=�6   $��EZ ORGANIZER��6D   $��25(�6X   $	��Top=�6f   $��Century Gothic)�6~   
�������$��0 0 0 0q�6�   $��Center=�6�   6�   �� 5\      $	��Top=�6   $ 
Horizontal=�62   $��Center=�6K   .+�5^      ]� $��50��6    $	��Top=�6,   .+�5_      �� $��resource\paint-brush.png��6    $��0 0 3 0q�6B   $��Uniform=�6S   $��25��6e   6r   �� 5`      -   5        $��countNormalItem��5`   $   $��5 0 20 0q�6;   $��1��6M   $��24(�6V   $��Center=�6d   $��Century Gothic)�6   
�������6�   5a      �� 5b      $��|��6    $��35(�6)   ���	#33FFFFFF$	��Top=�6N   6h   ]� 5c      $��50��6    $	��Top=�6,   .+�5d      �� $��resource\alarm-clock.png��6    $��20 0 3 0q�6B   $��Uniform=�6T   $��25��6f   6s   �� 5e      -   5        $��countSchedItem��5e   $   $��5 0 20 0q�6:   $��3��6L   $��24(�6U   $��Center=�6c   $��Century Gothic)�6~   
�������6�   5f      5g      ]� 5h      $	��100��6   $	��Top=�6)   6@   �� 5i      -   5        $ 
MainButton��5i      $	��Top=�6-   ���	#FFEAEAEA.��5j      ]� $
��Left=�6    .+�5k      �� $��resource\briefcase.png��6    $��Uniform=�6@   $��45��6R   $	 0.9Q�6]   �� 5l      $��    Work Item��6$   $��24(�69   $��Center=�6G   $��Century Gothic)�6b   5m      5n      �� 5o      -   5        $ ImportButton��5o      $	��Top=�6/   ���	#FFEAEAEA.��5p      �� $ 
Horizontal=�6!   $
��Left=�6:   .+�5q      �� $��resource\boxline.png��6    $��Uniform=�6>   $��45��6P   $	 0.9Q�6[   �� 5r      $��     Import      ��6$   $��24(�6=   $��Center=�6K   $��Century Gothic)�6f   5s      5t      �� 5u      -   5        $ 
HelpButton��5u      $	��Top=�6-   ���	#FFEAEAEA.��5v      �� $ 
Horizontal=�6!   .+�5w      �� $��resource\question.png��6    $��Uniform=�6?   $��45��6Q   $	 0.7Q�6\   �� 5x      $��     Help         ��6$   $��24(�6>   $��Center=�6L   $��Century Gothic)�6g   5y      5z      �� 5{      -   5        $ CreditButton��5{      $	��Top=�6/   ���	#FFEAEAEA.��5|      �� $ 
Horizontal=�6!   .+�5}      �� $��resource\chef (1).png��6    $��Uniform=�6?   $��45��6Q   �� 5~      $��     Credit      ��6$   $��24(�6=   $��Center=�6K   $��Century Gothic)�6f   5      5�      5�      ]� 5�      -   5        $ topStripPanel��5�      $	��Top=�6/   $��20��6D   5�      5�      ]� 5�   
   -   5        $ 
aboutPanel��5�      $
��1000��6(   ��.  $��
250,40,0,0q�6K   $
 1001��6z   ��5�      �� 5�       �� ImageSource$! Resource/sayagata-400px.png��6    j� TileMode$
 Tile=�6H    j� ViewportUnits$ Absolute=�6X    j� Viewport$ 0,0,300,300�6q   .+�5�      �� $	��Top=�6   .��5�      �� .+�5�      ]� $��0 50 0 0q�6    $	��Top=�62   $��80��6G   .+�5�      �� $��SPONSORS��6$   $��Century Gothic)�64   $��60(�6P   $��Center=�6^   ���	#FF2929296{   5�      ]� 5�      $	��Top=�6    .+�5�      �� $ 
Horizontal=�6%   $��Center=�6>   .+�5�      �� $��resource\stark.jpg��6$   $��50 25 50 15q�6@   $	��Top=�6U   $	��220��6j   �� 5�      $!��resource\mitrais_teaser.jpg��6$   $��	0 25 0 15q�6I   $	��Top=�6\   $	��300��6q   �� 5�      $��resource\wayne.jpg��6$   $��50 25 50 15q�6@   $	��Top=�6U   $	��220��6j   5�      5�      ]� 5�      $��0 50 0 0q�6    $	��Top=�62   $��70��6G   .+�5�      �� $��	THE CHEFS��6$   $��Century Gothic)�65   $��60(�6Q   $��Center=�6_   ���	#FF2929296|   5�      ]� 5�      $��	100 0 0 0q�6    $
��Left=�63   ��.  .+�5�      �� $��resource\cardVian.png��6    $	��Top=�6?   $	��600��6T   �� 5�      $��resource\cardAstri800.png��6    $	��Top=�6C   $	��600��6X   �� 5�      $��resource\cardEdo800.png��6    $	��Top=�6A   $	��600��6V   5�      ]� 5�      $��0 0 0 0q�6    $	��Top=�61   .+�5�      ]� $	��Top=�6$   .+�5�      �� $��	RESOURCES��6(   $��Century Gothic)�69   $��60(�6U   $��Center=�6c   ���	#FF2929296�   5�      �� 5�      $	��Top=�6%   $ Vertical=�6:   $��Center=�6Q   .+�5�      �� $��	0 50 0 90q�6(   $a��[Icons made by www.freepik.com from www.flaticon.com is licensed by Creative Commons BY 3.0 ��6;   $��Century Gothic)�6�   $��20(�6�   $
��Left=�6�   ���	#FF2929296�   5�      5�      5�      5�      5�      ]� 5�   
   -   5        $ 	helpPanel��5�      $
��1000��6'   ��.  $��
250,40,0,0q�6J   $
 1000��6y   ��5�      �� 5�      $! Resource/sayagata-400px.png��6   $
 Tile=�6H   $ Absolute=�6X   $ 0,0,300,300�6q   .+�5�      �� .��5�      �� $��30q�6   $	��920��6)   .+�5�      �� $��
HELP TOPIC��6    $��Century Gothic)�62   $��60(�6N   $��Center=�6\   ���	#FF292929$��
0 30 10 30q�6�   9� 5�      $�� What is EZ Organizer?��6   $��Century Gothic)�6?   $��30(�6[   $��70 10 10 20q�6i   .��5�      �� $����EZ Organizer (EZO) is a desktop application that helps you prepare the essential work item you need everytime you need it in a very easy way. Whether you are programmers, designers, architects, stock traders, whatever you do, as long as you work with a computer, EZ Prepare can help you.��6$   $��Century Gothic)�6K  $��20(�6g  $
��left=�6u  ���	#FF292929$
��Wrap=�6�  $��30q�6�  5�      9� 5�      $�� Is it free?��6   $��Century Gothic)�65   $��30(�6Q   $��70 10 10 20q�6_   .��5�      �� $����EZ Organizer is free for a certain functionality, without any time limitation. To get all functionality, please purchase the full version of EZO.��6$   $��Century Gothic)�6�   $��20(�6�   $
��left=�6�   ���	#FF292929$
��Wrap=�6  $��30q�6-  5�      9� 5�      $'��! What do I get from full version?��6   $��Century Gothic)�6J   $��30(�6f   $��70 10 10 20q�6t   .��5�      �� $����You will be able to set up more than 4 work items, you can also download a shortcut file that would enable you to open all work item that have been set up whenever you want without opening EZO. Please send an enquiry to hi@ezorganizer.com or visit our website www.ezorganizer.com. our team will guide you with the procedure.��6$   $��Century Gothic)�6p  $��20(�6�  $
��left=�6�  ���	#FF292929$
��Wrap=�6�  $��30q�6�  5�      9� 5�      $�� How do sponsor EZO?��6   $��Century Gothic)�6=   $��30(�6Y   $��70 10 10 20q�6g   .��5�      �� $����By sponsoring EZO, your company logo will be displayed in EZO Credit page and also on our website. Please send an enquiry to hi@ezorganizer.com or visit our website www.ezorganizer.com. our team will guide you with the procedure.��6$   $��Century Gothic)�6  $��20(�6-  $
��left=�6;  ���	#FF292929$
��Wrap=�6m  $��30q�6�  5�      9� 5�      $&��  How do I import the saved file?��6   $��Century Gothic)�6I   $��30(�6e   $��70 10 10 20q�6s   .��5�      �� $��Century Gothic)�6$   $��20(�6@   $
��left=�6N   ���	#FF292929$
��Wrap=�6�   $��30q�6�   �� 6�   $�Simply click ��6�   �� 6�   $�Export Work Item ��6�   �� 6�   $���button in Work Item menu. this acts like a configuration file that saves all your work items. you can load up this configuration file back to any EZO via Import menu.��6�   6�  5�      9� 5�      $&�� Where does the shortcut created?��6   $��Century Gothic)�6I   $��30(�6e   $��70 10 10 20q�6s   .��5�      �� $��Century Gothic)�6$   $��20(�6@   $
��left=�6N   ���	#FF292929$
��Wrap=�6�   $��30q�6�   �� 6�   $@�:The shortcut will be put in your desktop folder with name ��6�   �� 6�   $�Run EZO Work Items��6�   �� 6	  $���.bat. you can click the shortcut to open all your work items. please note that only non-scheduled item can be process by the shortcut. ��6  6�  5�      9� 5�      $,��&What files can be run by EZ Organizer?��6   $��Century Gothic)�6O   $��30(�6k   $��70 10 10 20q�6y   .��5�      �� $����You can open basically any file, as long there is program installed in your machine to handle the file. You can also open folder, network locations, website, and programs. You can even open program with special parameters that come with it.��6$   $��Century Gothic)�6  $��20(�68  $
��left=�6F  ���	#FF292929$
��Wrap=�6x  $��30q�6�  5�      5�      5�      5�      ]� 5�   
   -   5        $ importPanel��5�      $
��1000��6)   ��.  $��
250,40,0,0q�6L   $
 1002��6`   ��5�      �� 5�      $! Resource/sayagata-400px.png��6   $
 Tile=�6H   $ Absolute=�6X   $ 0,0,300,300�6q   .+�5�      ]� $��50 50 150 50q�6   $	��Top=�6.   $	��100��6C   .+�5�      �� $��IMPORT��6   $��Century Gothic)�6*   $��60(�6F   $��Center=�6T   ���	#FF2929296q   5�      ]� 5�      $	��Top=�6   .+�5�      �� $��
50 0 150 0q�6   $5��/Import a saved configuration file for EZ Manage��60   $��Century Gothic)�6g   $��25(�6�   $��Center=�6�   ���	#FF2929296�   5�      ]� 5�      $	��Top=�6   .+�5�      �� $��
50 0 150 0q�6   $6��0to replace current configuration and work items ��60   $��Century Gothic)�6h   $��25(�6�   $��Center=�6�   ���	#FF2929296�   5�      ]� 5�      $��	0 0 110 0q�6   $	��Top=�6+   ��.  $��Center=�6V   .+�5�      ]� $��0q�6   $
��Left=�6'   .+�5�      �� -   5         ��Name$ ImportTextBox��5�      $��
0 50 20 50q�65   $��  Select a file..��6I   $	��500��6b   $��40��6n   $��24(�6z   $��Century Gothic)�6�   ���	#FF4644446�   5�      ]� 5�      $
��Left=�6   .+�5�      �� -   5        $��browseButton��5�      $��40��61   $	��150��6=   ���	#FFD8560C.��5�      �� $��Browse��6$   $��25(�62   $��Center=�6@   $��Century Gothic)�6[   
�������6w   5�      5�      5�      ]� 5�      $��0 0 90 0q�6   $	��Top=�6*   .+�5�      �� -   5        $��importButton��5�      $��80��6-   $	��250��69   ���	#FFD8560C.��5�      �� $ 
Horizontal=�6!   .+�5�      �� $��resource\boxes.png��6    $��Uniform=�6<   $��40��6N   $	 0.9Q�6Y   $��0 0 10 0q�6g   �� 5�      $��Import Work Item��6$   $��22(�6<   $��Center=�6J   $��Century Gothic)�6e   
�������6�   5�      5�      5�      5�      ]� 5�   
   -   5        $ 	mainPanel��5�      ��.  $��
250,40,0,0q�6=   $
 1003��6Q   ��5�      �� 5�      $! Resource/sayagata-400px.png��6   $
 Tile=�6H   $ Absolute=�6X   $ 0,0,300,300�6q   .+�5�      ]� $��40 10 10 10q�6   $	��Top=�6-   $	��730��6B   .+�5�      ]� $��10q�6   $	��Top=�6)   .+�5�      �� -   5        $��Header��5�       $��  ��6.   $��20(�68   $��Century Gothic)�6F   ���	#FF464444$
��Left=�6y   6�   5�      �� 5�      .��5�      �� -   5        $��WorkItemWrapper��5�   !   $ Vertical=�68   5�      5�      5�      ]� 5�      $��0 0 40 0q�6   $��Bottom=�6*   ��.  $��Right=�6X   .+�5      �� -   5        $ addItemButton��5      $��10q�60   $��70��6<   $	��200��6H   #���   6p   .��5     �� $ 
Horizontal=�6!   $
��Left=�6:   .+�5     �� $��resource\plus.png��6    $��Uniform=�6;   $��20��6M   $��	 0 0 15 0q�6X   �� 5     $��Add New Item��6$   $��20(�68   $��Center=�6F   $��Century Gothic)�6a   
�������6}   5     5     ]� 5     -   5        $��emptySpaceDockLOL��5     $��0��65   6A   �� 5     -   5        $ 
saveButton��5     $��10q�6-   $��70��69   $	��230��6E   #���   6j   .��5     �� $ 
Horizontal=�6!   $
��Left=�6:   .+�5	     �� $��resource\saveicon.png��6    $��Uniform=�6?   $��20��6Q   $��	 0 0 15 0q�6\   $	 0.8Q�6o   �� 5
     $��Export Work Items��6$   $��20(�6=   $��Center=�6K   $��Century Gothic)�6f   
�������6�   5     5     �� 5     -   5        $ shortcutButton��5     $��10q�61   $��70��6=   $	��220��6I   #���   6r   .��5     �� $ 
Horizontal=�6!   $
��Left=�6:   .+�5     �� $��resource\writing.png��6    $��Uniform=�6>   $��30��6P   $��	 0 0 15 0q�6[   $	 0.8Q�6n   �� 5     $��Create Shortcut��6$   $��20(�6;   $��Center=�6I   $��Century Gothic)�6d   
�������6�   5     5     �� 5     -    5        $ runAllButton��5     $��10q�6/   $��70��6;   $	��220��6G   #���   6n   .��5     �� $ 
Horizontal=�6!   $
��Left=�6:   .+�5     �� $��resource\rocket.png��6    $��Uniform=�6=   $��30��6O   $��	 0 0 15 0q�6Z   $	 0.8Q�6m   �� 5     $��Run Work Items��6$   $��20(�6:   $��Center=�6H   $��Century Gothic)�6c   
�������6   5     5     5     5     5     5     