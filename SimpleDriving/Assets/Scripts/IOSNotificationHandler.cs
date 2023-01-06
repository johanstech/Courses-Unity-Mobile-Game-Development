#if UNITY_IOS
using Unity.Notifications.iOS;
#endif
using UnityEngine;

public class IOSNotificationHandler : MonoBehaviour
{
#if UNITY_IOS
  const string DefaultCategory = "category_a";

  public void ScheduleNotification(int minutes)
  {
    iOSNotification notification = new iOSNotification
    {
      Title = "Energy Recharged!",
      Subtitle = "Your energy has recharged!",
      Body = "Your energy has been recharged, come back to play again!",
      ShowInForeground = true,
      ForegroundPresentationOption = (PresentationOption.Alert | PresentationOption.Sound),
      CategoryIdentifier = DefaultCategory,
      ThreadIdentifier = "thread1",
      Trigger = new iOSNotificationTimeIntervalTrigger
      {
        TimeInterval = new System.TimeSpan(0, minutes, 0),
        Repeats = false
      }
    };

    iOSNotificationCenter.ScheduleNotification(notification);
  }
#endif
}
