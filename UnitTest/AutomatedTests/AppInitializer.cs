using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.UITest;

namespace Tests {
    class AppInitializer {
        public static IApp StartApp(Platform platform) {

            return ConfigureApp.Android.ApkFile("ForumDEG.ForumDEG.apk").StartApp();

            //// TODO: If the iOS or Android app being tested is included in the solution 
            //// then open the Unit Tests window, right click Test Apps, select Add App Project
            //// and select the app projects that should be tested.
            //if (platform == Platform.Android) {
            //    return ConfigureApp
            //        .Android
            //    // TODO: Update this path to point to your Android app and uncomment the
            //    // code if the app is not included in the solution.
            //    .ApkFile ("ForumDEG.ForumDEG.apk")
            //        .StartApp();
            //}

            //return ConfigureApp
            //    .iOS
            //// TODO: Update this path to point to your iOS app and uncomment the
            //// code if the app is not included in the solution.
            //    .AppBundle("../../../ForumDEG.iOS/bin/iPhoneSimulator/Debug/ForumDEG.app")
            //    .StartApp();
        }

    }
}
