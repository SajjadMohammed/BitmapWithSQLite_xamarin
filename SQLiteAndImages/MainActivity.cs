using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Widget;

using V7Toolbar = Android.Support.V7.Widget.Toolbar;

namespace SQLiteAndImages
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        Button insertButton, loadButton;
        EditText profileName;
        TextView profileId;
        ImageView profileImage;
        V7Toolbar mainToolbar;

        SqliteManager sqliteManager;
        Profile profile;
        Converter converter;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            // Declarations
            mainToolbar = FindViewById<V7Toolbar>(Resource.Id.mainToolbar);
            SetSupportActionBar(mainToolbar);
            insertButton = FindViewById<Button>(Resource.Id.insertButton);
            loadButton = FindViewById<Button>(Resource.Id.loadButton);
            profileId = FindViewById<TextView>(Resource.Id.profileId);
            profileName = FindViewById<EditText>(Resource.Id.profileName);
            profileImage = FindViewById<ImageView>(Resource.Id.profileImage);

            // Events
            insertButton.Click += InsertButton_ClickAsync;
            loadButton.Click += LoadButton_Click;

            // Initialization
            sqliteManager = new SqliteManager();
            converter = new Converter(this);
        }

        private async void LoadButton_Click(object sender, System.EventArgs e)
        {
            // loading data from database
            //var profile = await sqliteManager.GetProfilesAsync();
            //profileId.Text = profile[0].Id.ToString();
            //profileName.Text = profile[0].FullName;
            //
            //var bytes = profile[0].ProfileImage;

            // loading data by id
            var profile = await sqliteManager.GetProfileAsyncById(1);
            profileId.Text = profile.Id.ToString();
            profileName.Text = profile.FullName;
            //
            var bytes = profile.ProfileImage;
            var bmp = await converter.BytesToImageAsync(bytes);
            profileImage.SetImageBitmap(bmp);
        }

        private async void InsertButton_ClickAsync(object sender, System.EventArgs e)
        {
            if (string.IsNullOrEmpty(profileName.Text))
            {
                Toast.MakeText(this, "Write the ProfileName", ToastLength.Long).Show();
                return;
            }

            var profileImageBytes = await converter.ImageToBytesAsync(profileImage);
            profile = new Profile
            {
                FullName = profileName.Text,
                ProfileImage = profileImageBytes
            };

            // inserting data into database
            sqliteManager.InsertProfile(profile);

            //
            profileId.Text = profileName.Text = "";
        }
    }
}