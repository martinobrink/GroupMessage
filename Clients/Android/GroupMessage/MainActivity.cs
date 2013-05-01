using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Util;
using Android.Telephony;
using Android.Provider;
using Android.Preferences;

namespace GroupMessage
{
	[Activity (Label = "GroupMessage", MainLauncher = true)]
	public class MainActivity : Activity
	{
		private const string TAG = "GroupMessage";
		private TextView textRegistrationStatus = null;
		private TextView textRegistrationId = null;
		private TextView textLastMsg = null;
		private Button buttonRegister = null;
		private bool registered = false;

		private ISharedPreferences Preferences { 
			get 
			{ 
				return GetSharedPreferences(this.PackageName, FileCreationMode.Private); 
			}
		} 

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate(bundle);

			SetContentView(Resource.Layout.Main);

			textRegistrationStatus = FindViewById<TextView>(Resource.Id.textRegistrationStatus);
			textRegistrationId = FindViewById<TextView>(Resource.Id.textRegistrationId);
			textLastMsg = FindViewById<TextView>(Resource.Id.textLastMessage);
			buttonRegister = FindViewById<Button>(Resource.Id.buttonRegister);

			PushClient.CheckDevice(this);
			PushClient.CheckManifest(this);

			CheckPhoneNumber();

			this.buttonRegister.Click += delegate {
				if (!registered) {
					Log.Info(TAG, "Registering...");
					PushClient.Register(this, PushHandlerBroadcastReceiver.SENDER_IDS);
				} else {
					Log.Info(TAG, "Unregistering...");
					PushClient.UnRegister(this);
				}
				
				RunOnUiThread(() =>
				{
					//Disable the button so that we can't click it again
					//until we get back to the activity from a notification
					this.buttonRegister.Enabled = false;
				});
			};		
		}

		protected override void OnResume()
		{
			base.OnResume();			
			updateView();
		}

		private void CheckPhoneNumber()
		{
			var phoneNumberFromPreferences = Preferences.GetString(Constants.PREF_PHONE_NUMBER, null);
			if (String.IsNullOrEmpty(phoneNumberFromPreferences)) {
				CreatePhoneNumberDialog();
			}
		}

		private void CreatePhoneNumberDialog()
		{
			var phoneNumberFromTelephonyManager = TelephonyManager.FromContext(this).Line1Number;
			var factory = LayoutInflater.From(this);
			var phoneNumberView = factory.Inflate(Resource.Layout.GetPhoneNumberDialog, null);
			var phoneNumberTextBox = (EditText)phoneNumberView.FindViewById(Resource.Id.textPhoneNumber);
			phoneNumberTextBox.Text = phoneNumberFromTelephonyManager;
			var builder = new AlertDialog.Builder (this);
			builder.SetTitle("Please enter your phone number:");
			builder.SetView(phoneNumberView);
			builder.SetPositiveButton("Ok", OkClicked);
			builder.Create().Show();
		}

		private void OkClicked(object sender, DialogClickEventArgs dialogClickEventArgs)
		{
			var updatedPhoneNumberTextBox = ((AlertDialog)sender).FindViewById(Resource.Id.textPhoneNumber) as EditText;
			SaveStringToPreferences(Constants.PREF_PHONE_NUMBER, updatedPhoneNumberTextBox.Text);
		}

		private void SaveStringToPreferences(String key, String value)
		{
			var editor = Preferences.Edit();
			editor.PutString(key, value);
			editor.Commit();
		}

		private void updateView()
		{
			//Get the stored latest registration id
			var registrationId = PushClient.GetRegistrationId(this);
			
			//If it's empty, we need to register
			if (string.IsNullOrEmpty(registrationId))
			{
				registered = false;
				this.textRegistrationStatus.Text = "Registered: No";
				this.textRegistrationId.Text = "Id: N/A";
				this.buttonRegister.Text = "Register...";
				
				Log.Info(TAG, "Not registered...");
			}
			else
			{
				registered = true;
				this.textRegistrationStatus.Text = "Registered: Yes";
				this.textRegistrationId.Text = "Id: " + registrationId;
				this.buttonRegister.Text = "Unregister...";
				
				Log.Info(TAG, "Already Registered: " + registrationId);
			}
			
			var preferences = GetSharedPreferences(this.PackageName, FileCreationMode.Private);
			this.textLastMsg.Text = "Last Msg: " + Preferences.GetString(Constants.PREF_LAST_MESSAGE, "N/A");
			
			//Enable the button as it was normally disabled
			this.buttonRegister.Enabled = true;
		}
	}
}