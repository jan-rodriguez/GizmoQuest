using UnityEngine;
using System.Collections;

public class Utils {

	//Check if running on a mobile device
	public static bool isMobile  = (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer);

}
