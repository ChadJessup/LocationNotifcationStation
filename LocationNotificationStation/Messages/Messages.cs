using CommunityToolkit.Mvvm.Messaging.Messages;

namespace LocationNotificationStation;

// https://www.youtube.com/watch?v=Q_renpfnbk4

public class StartServiceMessage
{
}

public class StopServiceMessage
{

}

public class LocationMessage(Location value) : ValueChangedMessage<Location>(value)
{
}

public class LocationErrorMessage
{

}
