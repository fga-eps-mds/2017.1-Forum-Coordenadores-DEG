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

    private const string SettingsKey = "settings_key";
    private static readonly string SettingsDefault = string.Empty;

    //IsUserLoggedkey
    private const string IsUserLoggedkey = "isuserlogged_key";
    private static readonly bool IsUserLoggedDefault = false;

    //IsUserAdminkey
    private const string IsUserAdminkey = "isuseradmin_key";
    private static readonly bool IsUserAdminDefault = false;

    //IsUserCoordkey
    private const string IsUserCoordkey = "isusercoord_key";
    private static readonly bool IsUserCoordDefault = false;

    //UserIdkey
    private const string UserRegkey = "userreg_key";
    private static readonly string UserRegDefault = string.Empty;
    
    #endregion


    public static string GeneralSettings
    {
      get
      {
        return AppSettings.GetValueOrDefault<string>(SettingsKey, SettingsDefault);
      }
      set
      {
        AppSettings.AddOrUpdateValue<string>(SettingsKey, value);
      }
    }

    public static bool IsUserLogged {
      get{
        return AppSettings.GetValueOrDefault<bool>(IsUserLoggedkey, IsUserLoggedDefault);
      }
      set{
        AppSettings.AddOrUpdateValue<bool>(IsUserLoggedkey, value);
      }
    }

    public static bool IsUserAdmin {
      get{
        return AppSettings.GetValueOrDefault<bool>(IsUserAdminkey, IsUserAdminDefault);
      }
      set{
        AppSettings.AddOrUpdateValue<bool>(IsUserAdminkey, value);
      }
    }

    public static bool IsUserCoord {
      get{
        return AppSettings.GetValueOrDefault<bool>(IsUserCoordkey, IsUserCoordDefault);
      }
      set{
        AppSettings.AddOrUpdateValue<bool>(IsUserCoordkey, value);
      }
    }

    public static string UserReg {
      get{
        return AppSettings.GetValueOrDefault<string>(UserRegkey, UserRegDefault);
      }
      set{
        AppSettings.AddOrUpdateValue<string>(UserRegkey, value);
      }
    }
  }
}