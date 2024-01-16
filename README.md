# Custom WPF Control Library

Can be customized to fit your application design

## Usage

```xaml

# add to window namespace
xmlns:ui="clr-namespace:UI;assembly=UI"


# notification panel
<ui:Notification x:Name="notification"/>

```

```c#

# add info notification to window
notification.AddNotification(Notification.NotificationType.Info, "info title", "info details", 5);

```

## List of Controls
- Notification Panel

## Contributing

Pull requests are welcome. For major changes, please open an issue first
to discuss what you would like to change.

Please make sure to update tests as appropriate.

## License

[MIT](https://choosealicense.com/licenses/mit/)
