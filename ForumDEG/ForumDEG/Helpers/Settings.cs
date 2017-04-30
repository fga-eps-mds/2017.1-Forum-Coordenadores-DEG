// Helpers/Settings.cs
using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace ForumDEG.Helpers
{
  /// <summary>
  /// This is the Settings static class that can be used in your Core solution or in any
  /// of your client applications. All settings are laid out the same exact way with getters
  /// and setters. 
  /// </summary>
  public static class Settings
  {
    private static ISettings AppSettings
    {
      get
      {
        return CrossSettings.Current;
      }
    }

    #region Setting Constants

    private const string UserIdKey = "userid_key";
    private static readonly int UserIdDefault = 0;

    private const string UserNameKey = "username_key";
    private static readonly string UserNameDefault = string.Empty;

    private const string IsAdminKey = "isadmin_key";
    private static readonly bool IsAdminDefault = false;

    private const string IsLoggedInKey = "islogged_key";
    private static readonly bool IsLoggedInDefault = false;


        #endregion


        public static string UserName {
            get {
                return AppSettings.GetValueOrDefault<string> (UserNameKey, UserNameDefault);
            }
            set {
                AppSettings.AddOrUpdateValue<string>(UserNameKey, value);
            }
        }

        public static int UserId {
            get {
                return AppSettings.GetValueOrDefault<int>(UserIdKey, UserIdDefault);
            }
            set {
                AppSettings.AddOrUpdateValue<int>(UserIdKey, value);
            }
        }

        public static bool IsAdmin {
            get {
                return AppSettings.GetValueOrDefault<bool>(IsAdminKey, IsAdminDefault);
            }
            set {
                AppSettings.AddOrUpdateValue<bool>(IsAdminKey, value);
            }
        }

        public static bool IsLoggedIn {
            get {
                return AppSettings.GetValueOrDefault<bool>(IsLoggedInKey, IsLoggedInDefault);
            }
            set {
                AppSettings.AddOrUpdateValue<bool>(IsLoggedInKey, value);
            }
        }

    }
}