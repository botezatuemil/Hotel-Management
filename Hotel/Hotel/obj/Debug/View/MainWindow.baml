   M S B A M L   `   `   `  ????@  <Hotel, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:HotelHotel  5   	   1clr-namespace:Hotel.ViewModelHotel.ViewModel  5          Hotel.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   %localclr-namespace:Hotel     5   	   4
viewmodelsclr-namespace:Hotel.ViewModel     5        ? Title$  
MainWindow??5	   	   $	??450??6   $	??800??6)    ? DataContext 5
      #  Hotel.ViewModel.MenuViewModel  5   
   .??5      ? .+?5   
   ?? $??Login??6   $	??120??6!   $??30??6-   $??Center=?69   $	??Top=?6V   $	??140q?6n   ??6{   ?? *LoginCommand+6?   ?? 5   
   $??SignUp??6   $	??120??6"   $??30??6.   $??Center=?6:   $??Center=?6W   $??0q?6r   ??6}   ?? *RegisterCommand+6?   ?? 5   
   $??Guest??6   $	??120??6!   $??30??6-   $??Center=?69   $??Bottom=?6V   $	??140q?6q   ??6~   ?? *GuestCommand+6?   ?? 5   
   $
??Exit??6   $	??120??6    $??20??6,   $
??Left=?68   $	??Top=?6S   $??30q?6k   ??6w   ?? *ExitCommand+6?   5      5      